using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceLayer.WebReference_leave;
using System.Web;


namespace BLL
{
    public class Leave
    {
        public static string reducePath = "reduce";
        public static string picPath = "uploadPic";

        public static string defaultPic= "~/Res/images/file.png";
        public static int leave_sections_nullSelect = -1;
        public static int leave_leaveid_nullSelect = 0;

        #region insert application

        public static string GetAttachmentAbsolutePath()
        {
            string path= LSLibrary.WebAPP.WebConfig.getValue("attachmentUploadPath");
            return  path+ "\\mobil\\";
        }

        /// <summary>
        /// 0:ok -1:empty .-2 same apply day or section,-3 sp error -4 other error,-5 block.-6 overlap with clot.
        /// </summary>
        /// <param name="originDetail"></param>
        /// <param name="message"></param>
        /// <param name="eid"></param>
        /// <returns> 0:ok -1:empty .-2 same apply day or section,-3 sp error</returns>
        private static int CheckBeforeApply(List<MODEL.Apply.apply_LeaveData> originDetail, ref string message, int eid, int? staffid,List<WebServiceLayer.WebReference_codesettings.LeaveInfo> allLeaveInfo,bool hasAttachment,int myUID)
        {
            //check all logic.once fail skip.
            int result = 0;

            //check data is not empty.
            if (result == 0)
            {
                if (originDetail.Count == 0)
                {
                    result = -1;
                    message = MultiLanguageHelper.GetLanguagePacket().apply_msg_emptydate;
                    return result;
                }
            }

            // valiable
            double currentApply = MODEL.Apply.apply_LeaveData.GetCurrentApplyUnit(originDetail);
            bool isal = originDetail[0].IsAL();
            bool isSL = originDetail[0].IsSL();
            int leaveid = originDetail[0].leavetypeid;

            bool enfourceAttachment = false;
            double attachmentTolerance = 99;
            WebServiceLayer.WebReference_codesettings.LeaveInfo theLeaveInfo = allLeaveInfo.Where(x => x.ID == leaveid).FirstOrDefault();
            if (theLeaveInfo != null)
            {
                enfourceAttachment = theLeaveInfo.IsEnforceAttachment;
                attachmentTolerance = theLeaveInfo.AttachmentTolerance;
            }

            //check al/sl balance 1.adv
            if (result == 0)
            {
                if (isSL || isal)
                {
                    double availableBalance = BLL.Leave.GetAailabeValue_substractFutherAndWait(leaveid, staffid ?? 0, eid);
                    bool cananyday = theLeaveInfo.IsEnableAdvanceLeaveOnPortal;
                    bool canMoreTodayButLessYear = theLeaveInfo.IsEnableAdvanceLeaveOnYearEnd;
                    bool canapply = CheckALSLBalanceLimit(currentApply, availableBalance, cananyday, canMoreTodayButLessYear);
                    if (!canapply)
                    {
                        result = -4;
                        message = BLL.MultiLanguageHelper.GetLanguagePacket().Common_limitbalance;
                    }
                }
            }


            //check attachment.
            if (result == 0)
            {
                if (enfourceAttachment && currentApply > attachmentTolerance && hasAttachment==false)
                {
                    result = -4;
                    message = BLL.MultiLanguageHelper.GetLanguagePacket().Common_EnforceAttachment;
                }
            }


            //sp check
            if (result == 0)
            {
                for (int i = 0; i < originDetail.Count; i++)
                {
                    string uid = "0";
                    string lang = "en";
                    string unit = originDetail[i].GetUnit().ToString();
                    string[] spPs = new string[] { eid.ToString(), originDetail[i].leavetypeid.ToString(), originDetail[i].LeaveDate.ToString("yyyy-MM-dd"), unit, lang, uid };
                    string spCheckResult = BLL.Other.ExeStropFun((int)BLL.GlobalVariate.spFunctionid.leave_ADD_Portal, true, spPs);

                    if (!string.IsNullOrEmpty(spCheckResult))
                    {
                        result = -3;
                        message = spCheckResult;
                        break;
                    }
                }
            }

            //checked overlap.
            if (result == 0)
            {
                List<LeaveRequestDetail> requestDetails = getWaitingApproveAndApprovedByEIDS(new List<int> { eid });
                for (int i = 0; i < originDetail.Count; i++)
                {
                    var theSamedays = requestDetails.Where(x => (DateTime)x.LeaveFrom == originDetail[i].LeaveDate).ToList();

                    if (theSamedays.Count > 0)
                    {
                        var sameDayAndSameSection = theSamedays.Where(x => x.Section == originDetail[i].sectionid).ToList();

                        if (originDetail[i].sectionid == (int)GlobalVariate.sectionType.full || sameDayAndSameSection.Count() > 0)
                        {
                            result = -2;
                            message += string.Format(BLL.MultiLanguageHelper.GetLanguagePacket().common_msg_alappliend, originDetail[i].LeaveDate.ToString("MM-dd")) + "\r\n";
                            break;
                        }
                    }
                }
            }

            //overlap with clot
            if (result == 0)//byDaybyHour ,0 day, 1 hour.
            {
                var approvedAndWaitingCLOT = BLL.CLOT.GetMyCLOT_ApprovedAndWaitingByUID(myUID);
                var theShift = BLL.CodeSetting.GetShiftbyEid(eid);
                string errorMsg = CheckBeforeApply_overlapCLOT(approvedAndWaitingCLOT, originDetail, theShift);

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    result = -6;
                    message = errorMsg;
                }
            }

            //check block date. can not apply earlier today except sl.
            if (result == 0)
            {
                if (BLL.SystemParameters.GetSysParameters().mBLOCK_BACKDATE_APPLY)
                {
                    if (!isSL)
                    {
                        List<DateTime> DateList = originDetail.Select(x => x.LeaveDate).ToList();
                        bool needBlock = isContainEarlierToday(DateList);

                        if (needBlock)
                        {
                            result = -5;
                            message += BLL.MultiLanguageHelper.GetLanguagePacket().Common_block_application + "\r\n";
                        }
                    }
                }
            }

            return result;
        }

        private static string CheckBeforeApply_overlapCLOT(List<StaffCLOTRequest> tobeCheckedCLOTs, List<MODEL.Apply.apply_LeaveData> tobeCheckLeave, WebServiceLayer.WebReference_codesettings.Shift EmployShift)
        {
            string errorMSG = "";
            //检查每一个。1.section,section 2.section hours 3. hour section . 4.hour hour ,有一个错误，那么马上出错。跳出循环。
            //1.正常。2，section转为hours. 无法转那么就冲突。3 section转为hours. 无法转那么就冲突， 4.hour ,hour 不能重叠。
            //CLOT,2023-01-01 1:1:1 , time is override.
            //string tempError = "{0} ," + BLL.MultiLanguageHelper.GetLanguagePacket().common_msg_overlap;

            foreach (var theLeave in tobeCheckLeave)
            {
                foreach (var theCLOT in tobeCheckedCLOTs)
                {
                    bool isOverLap = false;
                    if (isOverLap)
                    {
                        errorMSG = "";
                        break;
                    }
                }
            }

            //var sameDayLeave = originDetail.Where(x => x.LeaveDate == tobeCheckCLOT.Date).ToList();
            //if (sameDayLeave != null && sameDayLeave.Count() > 0)
            //{
            //    bool isOverlapWithclot = false;

            //    if (isOverlapWithclot)
            //    {
            //        result = -6;
            //        string tempCLOTINFO = "CLOT," + tobeCheckCLOT.Date.ToString("yyyy-MM-dd") + " " + tobeCheckCLOT.TimeFrom.Value.ToString("HH:mm") + "-" + tobeCheckCLOT.TimeTo.Value.ToString("HH:mm") + " .";
            //        message += string.Format(tempError, tempCLOTINFO);
            //        break;
            //    }
            //}

            return errorMSG;
        }

        public static bool isContainEarlierToday(List<DateTime> leaveTime)
        {
            bool result = false;
            
            leaveTime = leaveTime.Where(x => x < System.DateTime.Now.Date).ToList();
            if (leaveTime != null && leaveTime.Count() > 0)
            {
                result = true;
            }

            return result;
        }

        public static List<DateTime> ConvertDateListNull(List<DateTime?> dlist)
        {
            List<DateTime> result = new List<DateTime>();
            if (dlist != null && dlist.Count() > 0)
            {
                foreach (var theItem in dlist)
                {
                    if (theItem != null)
                    {
                        result.Add(theItem??System.DateTime.MinValue);
                    }
                }
            }

            return result;
        }


        //>0 ok:request id. -1 check error -2.insert error
        public static int InsertLeave(List<MODEL.Apply.apply_LeaveData> originDetail, int userid, int employmentid, int? staffid, string remarks, ref string errorMsg,int fid,bool hasAttachment)
        {

            errorMsg = "";
            int result = -1;

            List<WebServiceLayer.WebReference_codesettings.LeaveInfo> allLeaveInfo = BLL.CodeSetting.GetAllLeaveInfo().ToList();
            int checkResult = CheckBeforeApply(originDetail, ref errorMsg, employmentid, staffid, allLeaveInfo, hasAttachment, userid);
            if (checkResult >= 0)
            {
                WebServiceLayer.WebReference_leave.StaffLeaveRequest[] details;
                WebServiceLayer.WebReference_leave.ErrorMessageInfo messageInfo;
                int insertResult = InsertLeaveData(originDetail, userid, employmentid, staffid, remarks,fid, out details, out messageInfo);
                if (insertResult >= 0)
                {
                    int processID = messageInfo.ProcessID;
                    int employID = employmentid;
                    int workFlowResult = BLL.workflow.InsertWorkflow(details, userid, messageInfo.ProcessID, employID);
                    result = insertResult;

                    if (result == -8)
                    {
                        errorMsg = "No Apply Group";
                    }

                    //insert pn
                    List<int> pnids = BLL.WorkflowPN.InsertPN_ApplyLeave(insertResult);
                }
                else
                {
                    errorMsg = errorMsg + " .";
                    result = -2;
                }
            }
            else
            {
                errorMsg = errorMsg + " .";
                result = -1;
            }
            return result;
        }


        private static int InsertLeaveData(List<MODEL.Apply.apply_LeaveData> originDetail, int userid, int employmentid, int? staffid, string remarks,int fid, out WebServiceLayer.WebReference_leave.StaffLeaveRequest[] details, out WebServiceLayer.WebReference_leave.ErrorMessageInfo messageInfo)
        {
            int result = -1;//默认为一般错误
            WebServiceLayer.MyWebService.WebServicesHelper webServicesHelper = WebServiceLayer.MyWebService.WebServicesHelper.GetInstance();
            List<WebServiceLayer.WebReference_leave.StaffLeaveRequest> detail = GenerateLeaveRequest(originDetail, userid, employmentid,remarks,fid);

            messageInfo = new WebServiceLayer.WebReference_leave.ErrorMessageInfo();
            try
            {
                messageInfo = webServicesHelper.ws_leave.InsertRequest(true, detail.ToArray(), userid, staffid);
                result = messageInfo.ProcessID;
            }
            catch
            {
                result = -1;
            }
            details = detail.ToArray();
            return result;
        }


        private static List<WebServiceLayer.WebReference_leave.StaffLeaveRequest> GenerateLeaveRequest(List<MODEL.Apply.apply_LeaveData> originDetail, int uid, int employmentID,string remark,int firsteid)
        {
            List<WebServiceLayer.WebReference_leave.StaffLeaveRequest> result = new List<WebServiceLayer.WebReference_leave.StaffLeaveRequest>();

            for (int i = 0; i < originDetail.Count; i++)
            {
                if (employmentID > 0)
                {
                    WebServiceLayer.WebReference_leave.StaffLeaveRequest newItem = new WebServiceLayer.WebReference_leave.StaffLeaveRequest();
                    newItem.CompareKey = null;
                    newItem.CreateDate = System.DateTime.Now;
                    newItem.DelegationToStaffID = null;
                    newItem.DeleteKey = originDetail[i].LeaveDate.ToString("yyyy-MM-dd") + "," + originDetail[i].leavetypeid.ToString();
                    newItem.Description = null;
                    newItem.EmploymentID = employmentID;
                    newItem.EmploymentNumber = null;
                    newItem.FirstEmploymentID = firsteid;
                    newItem.FirstEmploymentNumber = null;
                    newItem.HolidayCode = "";
                    newItem.ID = 0;
                    newItem.IsApproved = false;
                    newItem.LeaveCalculationTypeID = -1;
                    newItem.LeaveCalculationTypeDesc = "N/A";
                    newItem.LeaveDate = originDetail[i].LeaveDate;
                    newItem.LeaveID = originDetail[i].leavetypeid;
                    newItem.LeaveTypeName = originDetail[i].leavetypeDescription;
                    newItem.Name = null;
                    newItem.NameCH = null;
                    newItem.Remarks = remark;
                    newItem.RequestID = 0;
                    
                    newItem.Status = 0;
                    newItem.TotalWorkHours = originDetail[i].workHours;

                    if (originDetail[i].sectionid == 0)
                    {
                        newItem.Section = originDetail[i].sectionid;
                        newItem.Unit = originDetail[i].GetUnit();
                        newItem.IsHalfDay = false;
                        newItem.DisplaySection = newItem.Section;
                        newItem.LeaveHours = 0;
                        newItem.LeaveHoursFrom = originDetail[i].LeaveDate;
                        newItem.LeaveHoursTo = originDetail[i].LeaveDate;
                    }
                    else if (originDetail[i].sectionid == 1 || originDetail[i].sectionid == 2)
                    {
                        newItem.Section = originDetail[i].sectionid;
                        newItem.Unit = originDetail[i].GetUnit();
                        newItem.IsHalfDay = true;
                        newItem.DisplaySection = newItem.Section;
                        newItem.LeaveHours = 0;
                        newItem.LeaveHoursFrom = originDetail[i].LeaveDate;
                        newItem.LeaveHoursTo = originDetail[i].LeaveDate;
                    }
                    else if (originDetail[i].sectionid == 3)
                    {
                        newItem.Section = originDetail[i].sectionid;
                        newItem.Unit = originDetail[i].GetUnit();
                        newItem.IsHalfDay = false;
                        newItem.DisplaySection = newItem.Section;
                        newItem.LeaveHours = 0;
                        newItem.LeaveHoursFrom = originDetail[i].LeaveDate;
                        newItem.LeaveHoursTo = originDetail[i].LeaveDate;
                    }
                    else if (originDetail[i].sectionid == 4)
                    {
                        newItem.Section = 0;//hr put 0.so ileave put 0 also.
                        newItem.Unit = originDetail[i].GetUnit();

                        newItem.IsHalfDay = false;
                        newItem.DisplaySection = 0;
                        newItem.LeaveHours = originDetail[i].totalHours;
                        newItem.LeaveHoursFrom = originDetail[i].LeaveHourFrom.Value;
                        newItem.LeaveHoursTo = originDetail[i].LeaveHourTo.Value;
                    }

                    newItem.DisplayUnit = newItem.Unit.ToString() + " D";


                    result.Add(newItem);
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromh"></param>
        /// <param name="toh"></param>
        /// <param name="fromm"></param>
        /// <param name="tom"></param>
        /// <param name="eid"></param>
        /// <param name="section">-1 hours, 0:full day ,1 am 2 pm</param>
        /// <returns></returns>
        public static double GetRealTotalHours(int fromh,int toh,int fromm,int tom,int eid,int section)
        {
            DateTime theday = System.DateTime.Now;
            double totalHours = 0;

            if (toh < fromh || (toh == fromh && tom < fromm))//over right.
            {
                double totalHours1 = BLL.CLOT.CalculateNumberofHoursToDayEnd(fromh, fromm);
                double totalHours2 = BLL.CLOT.CalculateNumberofHours(0, toh, 0, tom);

                var einfo = BLL.User_wsref.getEmploymentByid(eid);
                if (einfo != null)
                {
                    var shift = BLL.CodeSetting.GetShiftbyid(einfo.ShiftID);

                    if (shift != null)
                    {//少了一个夜班的吃中饭。因为代码不好修改。就漏了这个处理，也是因为夜班，不太可能吃夜班开始那天的午饭。
                        DateTime f1v = new DateTime(1900, 1, 1, 0, 0, 0);
                        DateTime t1v = new DateTime(1900, 1, 1, toh, tom, 0);

                        DateTime f2 = new DateTime(1900, 1, 1, shift.LunchIn.Hour, shift.LunchIn.Minute, 0);
                        DateTime t2 = new DateTime(1900, 1, 1, shift.LunchOut.Hour, shift.LunchOut.Minute, 0);
                        totalHours2 = BLL.CodeSetting.GetRealTotal(f1v, t1v, f2, t2);
                    }
                }

                totalHours = totalHours1 + totalHours2;
            }
            else
            {
                totalHours = BLL.CLOT.CalculateNumberofHours(fromh, toh, fromm, tom);

                var einfo = BLL.User_wsref.getEmploymentByid(eid);
                if (einfo != null)
                {
                    var shift = BLL.CodeSetting.GetShiftbyid(einfo.ShiftID);
                    if (shift != null)
                    {
                        //1.用shift 修正一次:去除午餐時間。
                        DateTime f1 = new DateTime(1900, 1, 1, fromh, fromm, 0);
                        DateTime t1 = new DateTime(1900, 1, 1, toh, tom, 0);
                        DateTime f2 = new DateTime(1900, 1, 1, shift.LunchIn.Hour, shift.LunchIn.Minute, 0);
                        DateTime t2 = new DateTime(1900, 1, 1, shift.LunchOut.Hour, shift.LunchOut.Minute, 0);
                        totalHours = BLL.CodeSetting.GetRealTotal(f1, t1, f2, t2);


                        //2.如果是am,pm ,fullday, 要另外修正： am,pm 固定為totalWorkHours 的一半.
                        if (section == 0)
                        {
                            totalHours = shift.TotalWorkHour;
                        }
                        else if (section == 1)
                        {
                            totalHours = shift.TotalWorkHour / 2f;
                        }
                        else if (section == 2)
                        {
                            totalHours = shift.TotalWorkHour / 2f;
                        }
                    }
                }
            }
            return totalHours;
        }


        public static double GetEmployHours(int employid)
        {
            double result = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetTotalWorkHours(employid);
            return result;
        }


        public static void InsertAttachment(List<MODEL.App_AttachmentInfo> pics,int UploaderUid,int personid,int requestID,BLL.GlobalVariate.AttachmentUploadType UploadType, BLL.GlobalVariate.WorkflowTypeID workflowID)
        {
            for (int i = 0; i < pics.Count(); i++)
            {
                AttachmentInfo info = new AttachmentInfo();
                info.TypeID =(int)UploadType;
                info.RelatedPartyID = personid;
                info.FunctionID = 0;
                info.Path = pics[i].originAttendance_HRDBPath;
                info.ModifiedDate = System.DateTime.Now;
                info.ExpiryDate = new DateTime(1900, 1, 1);
                info.NoticePeriod = -1;
                info.PayrollPeriodID = -1;
                info.Status = 2;
                info.RelatedRequestID = requestID;
                info.WorkFlowTypeID = (int)workflowID;
                info.Remarks = "";
           
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.InsertAttachmentInfo(info, UploaderUid);
            }
        }

        #endregion

        #region search application
        public static int[] CheckLeaveExist(int staffid, System.DateTime date, int leaveid, int sectionid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.CheckLeaveExist(staffid, date, leaveid, sectionid);
        }

        public static void UpdateTodayLeaveBalanceToTable(int eid)
        {
            WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.UpdateTodayLeaveBalanceToTable(eid);
        }

        public static FirstRequestInfo GetFirstRequestinfoa(int requestid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetFirstRequestInfo(requestid);
        }

        public static int GetPossibalCancelRequestid(int requestid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetPossibalCancelRequestid(requestid);
        }

        public static WebServiceLayer.WebReference_leave.t_WorkflowInfo GetWorkflowByRequestID(int requestid, int workflowid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetWorkinfoByRequest(requestid, workflowid);
        }

        #region my leave
        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyLeaveMaster_all(int pid, GlobalVariate.LeaveBigRangeStatus status)
        {
            List<LeaveRequestMaster> result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveMasterByPID(pid).ToList();
            int[] firstRequestId = result.Where(x => x.WorkflowTypeID == 0).Select(x => x.RequestID).ToArray();


            if (status == GlobalVariate.LeaveBigRangeStatus.waitapproval)
            {
                result = result.Where(x => (x.Status == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && x.WorkflowTypeID == 0) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)).ToList();
            }
            else if (status == GlobalVariate.LeaveBigRangeStatus.beyongdWait)
            {
                var tempresult = result.Where(x => (x.WorkflowTypeID == 0 && x.Status != (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE) || (x.workinfoID == null)).ToList();
                GetMyLeaveMaster_fixStatus(tempresult, result);
                result = tempresult;
            }

            result = result.OrderByDescending(x => x.leavefrom).ThenByDescending(x => x.createDate).ToList();
            return result;
        }



        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyLeaveMaster(int pid, GlobalVariate.LeaveBigRangeStatus status, int year)
        {
            List<LeaveRequestMaster> result = GetMyLeaveMaster_all(pid, status);
            result = result.Where(x => x.leavefrom.Year == year || x.leaveto.Year == year).ToList();
            result =result.OrderByDescending(x => x.leavefrom).ThenByDescending(x=>x.createDate).ToList();
            return result;
        }

        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyLeaveMasterByRequestID(int pid, GlobalVariate.LeaveBigRangeStatus status,int requestid)
        {
            List<LeaveRequestMaster> result = GetMyLeaveMaster_all(pid, status);

            result = result.Where(x => x.RequestID==requestid).ToList();
            result = result.OrderByDescending(x => x.leavefrom).ThenByDescending(x => x.createDate).ToList();
            return result;
        }

        private static void GetMyLeaveMaster_fixStatus(List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> needFixData, List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> allData)
        {
            foreach (var tempItem in needFixData)
            {
                var hasCancelItem = allData.Where(x => x.WorkflowTypeID == 10 && x.employmentID == tempItem.employmentID && x.leavefrom == tempItem.leavefrom && x.leaveto == tempItem.leaveto).FirstOrDefault();
                if (hasCancelItem != null)
                {
                    if (tempItem.Status == 4)//数据库设计的缺陷，cancel request id不能和之前requestid 的关联起来。所以暂时把4的都当作是 confirm canceled.
                    {
                        tempItem.WorkflowTypeID = 10;
                        tempItem.Status = hasCancelItem.Status;
                    }
                }
            }
        }

        #endregion

        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyManageLeaveMasterAll(int uid, GlobalVariate.LeaveBigRangeStatus status)
        {
            List<LeaveRequestMaster> result = new List<LeaveRequestMaster>();

            if (status == GlobalVariate.LeaveBigRangeStatus.waitapproval)
            {
                result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveMaster_MyManageWaitingByApprovarUID(uid).ToList();
            }

            else if (status == GlobalVariate.LeaveBigRangeStatus.beyongdWait)
            {
                result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveMaster_MyManageBeyondWaitingByApprovarUIDv2(uid, 0).ToList();
            }

            
            result = result.OrderByDescending(x => x.leavefrom).ThenByDescending(x => x.createDate).ToList();

            return result;
        }


        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyManageLeaveMaster(int uid, GlobalVariate.LeaveBigRangeStatus status, int year, string name)
        {
            List<LeaveRequestMaster> result = GetMyManageLeaveMasterAll(uid, status);

            result = result.Where(x => x.leavefrom.Year == year || x.leaveto.Year == year).ToList();

            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(x => MODEL.UserName.IsNameLike(x.p_Surname + " " + x.p_Othername, x.p_NameCH, name) == true).ToList();
            }
            result = result.OrderByDescending(x => x.leavefrom).ThenByDescending(x => x.createDate).ToList();

            return result;
        }

        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyManageLeaveMasterByRequestid(int uid, GlobalVariate.LeaveBigRangeStatus status,int requestid)
        {
            List<LeaveRequestMaster> result = GetMyManageLeaveMasterAll(uid, status);

            result = result.Where(x => x.RequestID == requestid).ToList();
            result = result.OrderByDescending(x => x.leavefrom).ThenByDescending(x => x.createDate).ToList();

            return result;
        }


        public static WebServiceLayer.WebReference_leave.LeaveRequestMaster GetRequestMasterByRequestID(int requestid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveMasterByReuestID(requestid);
        }

      


        public static List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> GetExtendLeaveDetailsByReuestID(int requestid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetExtendLeaveDetailsByReuestID(requestid).ToList();
        }

        public static List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> getWaitingApproveAndApprovedByEIDS(List<int> employids)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetExtendLeaveDetails_waitApproveAndApprovedByEIDS(employids.ToArray()).ToList();
        }

        public static List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> getWaitingApproveAndApprovedByEIDS_Date(DateTime dt, List<int> employids)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetExtendLeaveDetails_waitApproveAndApprovedBYEidAndDate(dt, employids.ToArray()).ToList();
        }

        public static List<int> GetMonthStatistic(int year, int month, int[] employmentids)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetMonthStatistic(year, month, employmentids).ToList();
        }


        public static List<MODEL.App_AttachmentInfo> getAttendanceModel(string uid, int requestID, HttpServerUtility server, GlobalVariate.AttachType attachtype)
        {
            List<WebServiceLayer.WebReference_leave.AttachmentInfo> attachments = new List<AttachmentInfo>();
            if (attachtype == GlobalVariate.AttachType.leave)
            {
                attachments = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetAttachmentInfoByRequestID_Leave(requestID).ToList();
            }
            else
            {
                attachments = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetAttachmentInfoByRequestID_clot(requestID).ToList();
            }

            List<MODEL.App_AttachmentInfo> data = new List<MODEL.App_AttachmentInfo>();
            for (int i = 0; i < attachments.Count; i++)
            {
                string dbpath = attachments[i].Path;
                data.Add(CopyHr2leaveAndGenearteModel(dbpath, server));
            }
            return data;
        }

        public static MODEL.App_AttachmentInfo CopyHr2leaveAndGenearteModel(string absolutePath,HttpServerUtility server)
        {
            MODEL.App_AttachmentInfo tempItem = null;

            string filename = LSLibrary.FileUtil.GetFileName(absolutePath);

            string bigFile = "~/" + BLL.Leave.picPath + "/" + filename;
            string reduceFile = "~/" + BLL.Leave.picPath + "/" + BLL.Leave.reducePath + "/" + filename;
            try
            {
                common.CopyAttendanceAndReduce(absolutePath, server.MapPath(bigFile), server.MapPath(reduceFile));
                tempItem = GenerateAttachmentModel(filename, server);
            }
            catch
            {
                string badfile = "~/Res/images/bad.png";
                tempItem = GenerateAttachmentModel(badfile, server);
            }
            return tempItem;
        }


        public static MODEL.App_AttachmentInfo GenerateAttachmentModel(string filename,System.Web.HttpServerUtility server)
        {
            string bigFile = "~/" + BLL.Leave.picPath + "/" + filename;
            string reduceFile = "~/" + BLL.Leave.picPath + "/" + BLL.Leave.reducePath + "/" + filename;
            string reduceAbsolutionFile = server.MapPath(reduceFile);

            if (!LSLibrary.FileUtil.FileIsExist(reduceAbsolutionFile))
            {
                reduceFile = BLL.Leave.defaultPic;
            }

            MODEL.App_AttachmentInfo temppic = new MODEL.App_AttachmentInfo(bigFile, reduceFile, BLL.Leave.GetAttachmentAbsolutePath() + filename);
            return temppic;
        }

        #endregion


        #region unity

        public static bool CheckALSLBalanceLimit(double currentApply, double availabelToday, bool canAndyday, bool canMoreYearEnd)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.checkALSL_ApplyingISok(currentApply, availabelToday, canAndyday, canMoreYearEnd);
        }


        public static bool AllowHour(int leaveID, int PositionID)
        {
            return BLL.CodeSetting.AllowHourly(leaveID, PositionID);
        }


        private static List<int> GetLeaveAndClotYearRange()
        {
            List<int> result = new List<int>();
            try
            {
                result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveAndCLOTYearRange().ToList();
            }
            catch
            {
                result.Clear();
                int defaultYear = DateTime.Now.Year;

                result.Add(defaultYear - 8);
                result.Add(defaultYear +1);

                result.Add(defaultYear - 8);
                result.Add(defaultYear + 1);
            }

            return result;
        }

        public static List<int> GetLeaveYearRange()
        {
            List<int> result = new List<int>();
            List<int> result2 = GetLeaveAndClotYearRange();

            result.Add(result2[0]);
            result.Add(result2[1]);

            return result;
        }

        public static List<int> GetClotYearRange()
        {
            List<int> result = new List<int>();
            List<int> result2 = GetLeaveAndClotYearRange();

            result.Add(result2[2]);
            result.Add(result2[3]);

            return result;
        }

        public static List<int> GetDefaultYearRange()
        {
            List<int> result = new List<int>();
            List<int> result2 = GetLeaveAndClotYearRange();

            result.Add(result2.Min());
            result.Add(result2.Max());

            return result;
        }

        public static List<LSLibrary.WebAPP.ValueText<int>> ConvertLeaveInfo2DropDownList(List<WebServiceLayer.WebReference_leave.t_Leave> source)
        {
            List<LSLibrary.WebAPP.ValueText<int>> result = new List<LSLibrary.WebAPP.ValueText<int>>();
            result.Add(new LSLibrary.WebAPP.ValueText<int>(leave_leaveid_nullSelect, BLL.MultiLanguageHelper.GetLanguagePacket().CommonPleaseSelect));
            for (int i = 0; i < source.Count(); i++)
            {
                LSLibrary.WebAPP.ValueText<int> item = new LSLibrary.WebAPP.ValueText<int>(source[i].ID,source[i].Description);
                result.Add(item);
            }
            return result;
        }

        public static List<LSLibrary.WebAPP.ValueText<int>> GetDDLSectionsDataNoSelect(int leaveid, int employid)
        {
            List<LSLibrary.WebAPP.ValueText<int>> ddlSource = new List<LSLibrary.WebAPP.ValueText<int>>();
            
            if (leaveid != 0)
            {
                WebServiceLayer.WebReference_user.t_Employment t_Employment = BLL.User_wsref.getEmploymentByid(employid);
                int position = t_Employment.PositionID;
                List<int> sections = BLL.CodeSetting.GetSections(position, leaveid);
                var ddlSourceAppend = BLL.Leave.ConvertInt2SectionDropDownList(sections);
                ddlSource.AddRange(ddlSourceAppend);
            }
            return ddlSource;
        }

        public static List<LSLibrary.WebAPP.ValueText<int>> GetDDLSectionsData(int leaveid, int employid)
        {
            List<LSLibrary.WebAPP.ValueText<int>> ddlSource = GetDDLSectionsDataNoSelect(leaveid, employid);
            ddlSource.Insert(0,new LSLibrary.WebAPP.ValueText<int>(leave_sections_nullSelect, BLL.MultiLanguageHelper.GetLanguagePacket().CommonPleaseSelect));
            return ddlSource;
        }

        public static List<LSLibrary.WebAPP.ValueText<int>> GetRadioData()
        {
            List<LSLibrary.WebAPP.ValueText<int>> ddlSource = new List<LSLibrary.WebAPP.ValueText<int>>();
            ddlSource.Insert(0, new LSLibrary.WebAPP.ValueText<int>(0, BLL.MultiLanguageHelper.GetLanguagePacket().apply_byday+"　　　　"));
            ddlSource.Insert(1, new LSLibrary.WebAPP.ValueText<int>(1, BLL.MultiLanguageHelper.GetLanguagePacket().apply_byhour));
            return ddlSource;
        }

        public static List<WebServiceLayer.WebReference_leave.t_Leave> GetLeavesByStaffID(int sid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetAllLeaveTypeByStaffID(sid).ToList();
        }

        public static List<LSLibrary.WebAPP.ValueText<int>> ConvertInt2SectionDropDownList(List<int> source)
        {
            List<LSLibrary.WebAPP.ValueText<int>> result = new List<LSLibrary.WebAPP.ValueText<int>>();
            for (int i = 0; i < source.Count(); i++)
            {
                if (source[i]<=3)//
                {
                    LSLibrary.WebAPP.ValueText<int> item = new LSLibrary.WebAPP.ValueText<int>(source[i], BLL.GlobalVariate.GetSectionMultLanguage(source[i]));
                    result.Add(item);
                }
            }
            return result;
        }


        public static string SetBackgroundColor(int index)
        {
            if (index % 2 != 0)
            {
                return "background-color:aliceblue";
            }
            else
            {
                return "";
            }
        }

        public static LeaveBalanceType GetLeaveBalanceType(int leaveid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveBalanceType(leaveid);
        }

        /// <summary>
        /// 已經減去了wait 的數量。
        /// </summary>
        /// <param name="leaveid"></param>
        /// <param name="staffid"></param>
        /// <param name="employid"></param>
        /// <returns></returns>
        public static double GetAailabeValue_substractFutherAndWait(int leaveid,int staffid,int employid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetGross(staffid, employid, leaveid);
        }

        public static double GetEstimation(int firsteid,DateTime asofdate)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.EstimationAnnualLeave(firsteid, asofdate);
        }

        public static double GetSLEstimation(int firstEid, DateTime asodate)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.EstimationSickLeave(firstEid, asodate);
        }

        public static vSystemLeaveBalance[] GetBalanceFromView(int eid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveBalanceFromViewByEmployid(eid);
        }

        public static double GetBalanceView_CLOT_balance(int eid)
        {
            double result = 0;
            var data= WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveBalanceFromViewByEmployid(eid);
            var theItem = data.Where(x => x.LeaveCode.ToUpper() == "OT").FirstOrDefault();
            if (theItem != null)
            {
                result = theItem.Balance;
            }
            return result;
        }

        public static double GetBalanceView_CLOT_Wait(int eid)
        {
            double result = 0;
            var data = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveBalanceFromViewByEmployid(eid);
            var theItem = data.Where(x => x.LeaveCode.ToUpper() == "OT").FirstOrDefault();
            if (theItem != null)
            {
                result = theItem.WaitBal;
            }
            return result;
        }

        public static double GetWaitValue(int leaveid, int staffid,int employid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetWaiting(staffid, employid, leaveid);
        }


        public static WebServiceLayer.WebReference_leave.t_Leave GetLeaveByid(WebServiceLayer.WebReference_leave.t_Leave leave)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.Base_Gett_Leave(leave);
        }


        public static List<WebServiceLayer.WebReference_leave.LeaveHistory> GetLeaveHistoryByRequest_clot(int requestid)
        {
            List<WebServiceLayer.WebReference_leave.LeaveHistory> result= WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveHistory_clot(requestid).ToList();
            return result;
        }

        public static List<WebServiceLayer.WebReference_leave.LeaveHistory> GetLeaveHistoryByRequest_leawve(int requestid)
        {
            List<WebServiceLayer.WebReference_leave.LeaveHistory> result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveHistory_leave(requestid).ToList();
            return result;
        }


        //勉强的假期状态（非请求）
        //type=0
        //1.waiting approval
        //2.approved
        //3.rejected
        //4.Canceled

        //type = 10
        //3.approved
        //5.waiting approval
        //6.Canceled
        public static string GetLeaveStatusDesc(int? workflowid,int masterStatus)
        {
            string result = "";
            if (workflowid == 10)
            {
                if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)
                {
                    result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_WaitForCancel;
                }
                else if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.CONFIRM_CANCEL)
                {
                    result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_ConfirmCancelled;
                }
                else if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.REJECT)
                {
                    result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_Approved;
                }
                else
                {
                    result = "Other";
                }
            }
            else
            {
                if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE)
                {
                    result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_WaitForApproval;
                }
                else if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.CANCEL)
                {
                    result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_Cancelled;
                }
                else if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.APPROVE)
                {
                    result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_Approved;
                }
                else if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.REJECT)
                {
                    result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_Rejected;
                }
                else
                {
                    result = "Other";
                }
            }
            return result;
        }

        public static string GetClotStatusDesc(int masterStatus)
        {
            string result = "";
           
            if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE)
            {
                result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_WaitForApproval;
            }
            else if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.CANCEL)
            {
                result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_Cancelled;
            }
            else if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.APPROVE)
            {
                result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_Approved;
            }
            else if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.REJECT)
            {
                result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_Rejected;
            }
            else if (masterStatus == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)
            {
                result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_WaitForCancel;
            }
            else
            {
                result = "Other";
            }
            
            return result;
        }

        #endregion


    }
}