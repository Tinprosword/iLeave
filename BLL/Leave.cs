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

        //-1:empty .-2 same apply day or section
        private static int CheckBeforeApply(List<MODEL.Apply.apply_LeaveData> originDetail,ref string message,int eid)
        {
            int result = 0;
            if (originDetail.Count == 0)
            {
                result = -1;
                message = MultiLanguageHelper.GetLanguagePacket().apply_msg_emptydate;
            }
            else
            {
                List<LeaveRequestDetail> requestDetails = getWaitingApproveAndApprovedByEIDS(new List<int> { eid });
                for (int i = 0; i < originDetail.Count; i++)
                {
                    var theSamedays = requestDetails.Where(x => (DateTime)x.LeaveFrom == originDetail[i].LeaveDate).ToList();
                    
                    if (theSamedays.Count > 0)
                    {
                        var sameDayAndSameSection = theSamedays.Where(x => x.Section == originDetail[i].sectionid).ToList();

                        if (originDetail[i].sectionid == (int)GlobalVariate.sectionType.full || sameDayAndSameSection.Count()>0)
                        {
                            result = -2;
                            message += originDetail[i].LeaveDate.ToString("MM-dd") + " already applied!\r\n";
                            break;
                        }
                    }
                }
            }
            return result;
        }


        //>0 ok:request id. -1 check error -2.insert error
        public static int InsertLeave(List<MODEL.Apply.apply_LeaveData> originDetail, int userid, int employmentid, int? staffid, string remarks, ref string errorMsg,int fid)
        {

            errorMsg = "";
            int result = -1;
            int checkResult = CheckBeforeApply(originDetail, ref errorMsg, employmentid);
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
                    newItem.LeaveHours = 0;
                    newItem.LeaveHoursFrom = originDetail[i].LeaveDate;
                    newItem.LeaveHoursTo = originDetail[i].LeaveDate;
                    newItem.LeaveID = originDetail[i].leavetypeid;
                    newItem.LeaveTypeName = originDetail[i].leavetypeDescription;
                    newItem.Name = null;
                    newItem.NameCH = null;
                    newItem.Remarks = remark;
                    newItem.RequestID = 0;
                    newItem.Section = originDetail[i].sectionid;
                    newItem.Status = 0;
                    newItem.TotalWorkHours = GetEmployHours(employmentID);

                    if (newItem.Section == 0)
                    {
                        newItem.Unit = 1;
                        newItem.DisplayUnit = "1 D";
                        newItem.IsHalfDay = false;
                        newItem.DisplaySection = newItem.Section;
                    }
                    else if (newItem.Section == 1 || newItem.Section == 2)
                    {
                        newItem.Unit = 0.5;
                        newItem.DisplayUnit = "0.5 D";
                        newItem.IsHalfDay = true;
                        newItem.DisplaySection = newItem.Section;
                    }
                    else if (newItem.Section == 3)
                    {
                        newItem.Unit = 1;
                        newItem.DisplayUnit = "1 D";
                        newItem.IsHalfDay = false;
                        newItem.DisplaySection = newItem.Section;
                    }
                    result.Add(newItem);
                }
            }
            return result;
        }

        private static double GetEmployHours(int employid)
        {
            double result = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetTotalWorkHours(employid);
            return result;
        }


        public static void InsertAttachment(List<MODEL.Apply.App_AttachmentInfo> pics,int UploaderUid,int personid,int requestID)
        {
            for (int i = 0; i < pics.Count(); i++)
            {
                AttachmentInfo info = new AttachmentInfo();
                info.TypeID = 53;
                info.RelatedPartyID = personid;
                info.FunctionID = 0;
                info.Path = pics[i].originAttendance_HRDBPath;
                info.ModifiedDate = System.DateTime.Now;
                info.ExpiryDate = new DateTime(1900, 1, 1);
                info.NoticePeriod = -1;
                info.PayrollPeriodID = -1;
                info.Status = 2;
                info.RelatedRequestID = requestID;
                info.WorkFlowTypeID = 0;
                info.Remarks = "";
           
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.InsertAttachmentInfo(info, UploaderUid);
            }
        }

        #endregion

        #region search application
        public static FirstRequestInfo GetFirstRequestinfoa(int requestid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetFirstRequestInfo(requestid);
        }

        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyLeaveMaster(int pid, GlobalVariate.LeaveBigRangeStatus status, DateTime? from, DateTime? to)
        {
            List<LeaveRequestMaster> result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveMasterByPID(pid).ToList();
            int[] firstRequestId = result.Where(x => x.WorkflowTypeID == 0).Select(x => x.RequestID).ToArray();


            if (status == GlobalVariate.LeaveBigRangeStatus.waitapproval)
            {
                result = result.Where(x => (x.Status == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && x.WorkflowTypeID == 0) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)).ToList();
            }
            //else if (status == GlobalVariate.LeaveBigRangeStatus.approvaled)
            //{
            //    result = result.Where(x => (x.WorkflowTypeID == 0 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.APPROVE) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.CONFIRM_CANCEL)).ToList();
            //}
            //else if (status == GlobalVariate.LeaveBigRangeStatus.withdraw)
            //{
            //    result = result.Where(x => (x.WorkflowTypeID == 0 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.CANCEL) || (x.WorkflowTypeID == 0 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.REJECT) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.REJECT)).ToList();
            //}
            else if (status == GlobalVariate.LeaveBigRangeStatus.beyongdWait)
            {
                result = result.Where(x => x.Status != (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && x.WorkflowTypeID == 0).ToList();
            }

            if (from != null)
            {
                result = result.Where(x => x.leavefrom >= from).ToList();
            }
            if (to != null)
            {
                result = result.Where(x => x.leaveto <= to).ToList();
            }
            result =result.OrderByDescending(x => x.leavefrom).ThenByDescending(x=>x.createDate).ToList();
            return result;
        }



        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyManageLeaveMaster(int uid, GlobalVariate.LeaveBigRangeStatus status, DateTime? from, string name,DateTime? to)
        {
            List<LeaveRequestMaster> result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveMasterByApprovarUID(uid).ToList();
            if (result.Count() > 0)
            {
                if (status == GlobalVariate.LeaveBigRangeStatus.waitapproval)
                {
                    result = result.Where(x => (x.Status == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && x.WorkflowTypeID == 0) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)).ToList();
                }
                //else if (status == GlobalVariate.LeaveBigRangeStatus.approvaled)
                //{
                //    result = result.Where(x => (x.WorkflowTypeID == 0 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.APPROVE) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.CONFIRM_CANCEL)).ToList();
                //}
                //else if (status == GlobalVariate.LeaveBigRangeStatus.withdraw)
                //{
                //    result = result.Where(x => (x.WorkflowTypeID == 0 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.REJECT) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.REJECT)).ToList();
                //}
                else if (status == GlobalVariate.LeaveBigRangeStatus.beyongdWait)
                {
                    result= result.Where(x => x.Status != (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && x.WorkflowTypeID == 0).ToList();
                }
                if (from != null)
                {
                    result = result.Where(x => x.leavefrom >= from).ToList();
                }
                if (to != null)
                {
                    result = result.Where(x => x.leaveto <= to).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    result = result.Where(x => MODEL.UserName.IsNameLike(x.p_Surname + " " + x.p_Othername, x.p_NameCH, name) == true).ToList();
                }
                result = result.OrderByDescending(x => x.leavefrom).ThenByDescending(x=>x.createDate).ToList();
            }
            return result;
        }

        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyLeaveMaster(int pid, GlobalVariate.LeaveBigRangeStatus status, int year)
        {
            DateTime from = new DateTime(year, 1, 1);
            int dayCount = DateTime.DaysInMonth(year, 12);
            DateTime? to = new DateTime(year, 12, dayCount);
            return GetMyLeaveMaster(pid, status, from,to);
        }


        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyManageLeaveMaster(int uid, GlobalVariate.LeaveBigRangeStatus status, int year, string name)
        {
            DateTime from = new DateTime(year, 1, 1);
            int dayCount = DateTime.DaysInMonth(year, 12);
            DateTime? to = new DateTime(year,12, dayCount);
            return GetMyManageLeaveMaster(uid, status, from, name, to);
        }




        



        public static WebServiceLayer.WebReference_leave.LeaveRequestMaster GetRequestMasterByRequestID(int requestid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveMasterByReuestID(requestid);
        }

        public static List<WebServiceLayer.WebReference_leave.t_StaffLeaveRequestDetail> GetLeaveDetailsByReuestID(int requestid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveDetailsByReuestID(requestid).ToList();
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


        public static List<MODEL.Apply.App_AttachmentInfo> getAttendanceModel(string uid, int requestID,HttpServerUtility server)
        {
            List<WebServiceLayer.WebReference_leave.AttachmentInfo> attachments = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetAttachmentInfoByRequestID_Leave(requestID).ToList();

            List<MODEL.Apply.App_AttachmentInfo> data = new List<MODEL.Apply.App_AttachmentInfo>();
            for (int i = 0; i < attachments.Count; i++)
            {
                string dbpath = attachments[i].Path;
                data.Add(CopyHr2leaveAndGenearteModel(dbpath, server));
            }
            return data;
        }

        public static MODEL.Apply.App_AttachmentInfo CopyHr2leaveAndGenearteModel(string absolutePath,HttpServerUtility server)
        {
            MODEL.Apply.App_AttachmentInfo tempItem = null;

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


        public static MODEL.Apply.App_AttachmentInfo GenerateAttachmentModel(string filename,System.Web.HttpServerUtility server)
        {
            string bigFile = "~/" + BLL.Leave.picPath + "/" + filename;
            string reduceFile = "~/" + BLL.Leave.picPath + "/" + BLL.Leave.reducePath + "/" + filename;
            string reduceAbsolutionFile = server.MapPath(reduceFile);

            if (!LSLibrary.FileUtil.FileIsExist(reduceAbsolutionFile))
            {
                reduceFile = BLL.Leave.defaultPic;
            }

            MODEL.Apply.App_AttachmentInfo temppic = new MODEL.Apply.App_AttachmentInfo(bigFile, reduceFile, BLL.Leave.GetAttachmentAbsolutePath() + filename);
            return temppic;
        }

        #endregion


        #region unity
        
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

        public static List<WebServiceLayer.WebReference_leave.t_Leave> GetLeavesByStaffID(int sid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetAllLeaveTypeByStaffID(sid).ToList();
        }

        public static List<LSLibrary.WebAPP.ValueText<int>> ConvertInt2SectionDropDownList(List<int> source)
        {
            List<LSLibrary.WebAPP.ValueText<int>> result = new List<LSLibrary.WebAPP.ValueText<int>>();
            for (int i = 0; i < source.Count(); i++)
            {
                if (true)
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
        public static double GetCleanValue(int leaveid,int staffid,int employid)
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

        public static double GetWaitValue(int leaveid, int staffid,int employid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetWaiting(staffid, employid, leaveid);
        }


        public static WebServiceLayer.WebReference_leave.t_Leave GetLeaveByid(WebServiceLayer.WebReference_leave.t_Leave leave)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.Base_Gett_Leave(leave);
        }

        public static List<WebServiceLayer.WebReference_leave.t_Leave> GetAllLeave()
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.Base_GetListt_Leave("").ToList();
        }


        public static List<WebServiceLayer.WebReference_leave.LeaveHistory> GetLeaveHistoryByRequest(int requestid)
        {
            List<WebServiceLayer.WebReference_leave.LeaveHistory> result= WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveHistory(requestid).ToList();
            if (result != null && result.Count() > 0)
            {
                result.RemoveAt(0);
            }
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
                    result = BLL.MultiLanguageHelper.GetLanguagePacket().approval_Cancelled;
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