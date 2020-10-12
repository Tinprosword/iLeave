﻿using System;
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
                message = GlobalVariate.msg_emptyLeave + "\r\n";
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
                            message += originDetail[i].LeaveDate.ToString("MMdd") + " already applied!\r\n";
                            break;
                        }
                    }
                }
            }
            return result;
        }


        //>0 ok:request id. -1 check error -2.insert error
        public static int InsertLeave(List<MODEL.Apply.apply_LeaveData> originDetail, int userid, int employmentid, int? staffid, string remarks, ref string errorMsg)
        {
            BLL.User_wsref.CheckWsLogin();

            errorMsg = "";
            int result = -1;
            int checkResult = CheckBeforeApply(originDetail, ref errorMsg, employmentid);
            if (checkResult >= 0)
            {
                WebServiceLayer.WebReference_leave.StaffLeaveRequest[] details;
                WebServiceLayer.WebReference_leave.ErrorMessageInfo messageInfo;
                int insertResult = InsertLeaveData(originDetail, userid, employmentid, staffid, remarks, out details, out messageInfo);
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


        private static int InsertLeaveData(List<MODEL.Apply.apply_LeaveData> originDetail, int userid, int employmentid, int? staffid, string remarks, out WebServiceLayer.WebReference_leave.StaffLeaveRequest[] details, out WebServiceLayer.WebReference_leave.ErrorMessageInfo messageInfo)
        {
            int result = -1;//默认为一般错误
            WebServiceLayer.MyWebService.WebServicesHelper webServicesHelper = WebServiceLayer.MyWebService.WebServicesHelper.GetInstance();
            List<WebServiceLayer.WebReference_leave.StaffLeaveRequest> detail = GenerateLeaveRequest(originDetail, userid, employmentid);

            messageInfo = new WebServiceLayer.WebReference_leave.ErrorMessageInfo();
            try
            {
                messageInfo = webServicesHelper.ws_leave.InsertOnlineLeaveApplicationRequest(detail.ToArray(),  userid, staffid);
                result = messageInfo.ProcessID;
            }
            catch
            {
                result = -1;
            }
            details = detail.ToArray();
            return result;
        }


        private static List<WebServiceLayer.WebReference_leave.StaffLeaveRequest> GenerateLeaveRequest(List<MODEL.Apply.apply_LeaveData> originDetail, int uid, int employmentID)
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
                    newItem.FirstEmploymentID = 0;
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
                    newItem.Remarks = "";
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


        public static void InsertAttachment(List<MODEL.Apply.app_uploadpic> pics,int UploaderUid,int personid,int requestID)
        {
            for (int i = 0; i < pics.Count(); i++)
            {
                AttachmentInfo info = new AttachmentInfo();
                info.TypeID = 53;
                info.RelatedPartyID = personid;
                info.FunctionID = 0;
                info.Path = pics[i].bigImageAbsolutePath;
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
        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyLeaveMaster(int pid, GlobalVariate.LeaveBigRangeStatus status, DateTime? from)
        {
            List<LeaveRequestMaster> result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveMasterByPID(pid).ToList();
            if (status == GlobalVariate.LeaveBigRangeStatus.waitapproval)
            {
                result = result.Where(x =>  (x.Status == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && x.WorkflowTypeID==0) || ( x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)).ToList();
            }
            else if (status == GlobalVariate.LeaveBigRangeStatus.approvaled)
            {
                result = result.Where(x =>(x.WorkflowTypeID == 0 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.APPROVE )|| (x.WorkflowTypeID ==10&&   x.Status == (byte)GlobalVariate.ApprovalRequestStatus.CONFIRM_CANCEL)).ToList();
            }
            else if (status == GlobalVariate.LeaveBigRangeStatus.withdraw)
            {
                result = result.Where(x => (x.WorkflowTypeID == 0 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.CANCEL) || (x.WorkflowTypeID == 0 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.REJECT) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.REJECT)).ToList();
            }

            if (from != null)
            {
                result = result.Where(x => x.leavefrom >= from).ToList();
            }
            result=result.OrderByDescending(x => x.leavefrom).ToList();
            return result;
        }

        public static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetMyManageLeaveMaster(int uid, GlobalVariate.LeaveBigRangeStatus status, DateTime? from, string name)
        {
            List<LeaveRequestMaster> result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveMasterByApprovarUID(uid).ToList();
            if (result.Count() > 0)
            {
                if (status == GlobalVariate.LeaveBigRangeStatus.waitapproval)
                {
                    result = result.Where(x => (x.Status == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && x.WorkflowTypeID == 0) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)).ToList();
                }
                else if (status == GlobalVariate.LeaveBigRangeStatus.approvaled)
                {
                    result = result.Where(x => (x.WorkflowTypeID == 0 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.APPROVE) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.CONFIRM_CANCEL)).ToList();
                }
                else if (status == GlobalVariate.LeaveBigRangeStatus.withdraw)
                {
                    result = result.Where(x =>  (x.WorkflowTypeID == 0 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.REJECT) || (x.WorkflowTypeID == 10 && x.Status == (byte)GlobalVariate.ApprovalRequestStatus.REJECT)).ToList();
                }
                if (from != null)
                {
                    result = result.Where(x => x.leavefrom >= from).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    result = result.Where(x => x.uname.ToUpper().Contains(name.ToUpper())).ToList();
                }
                result = result.OrderByDescending(x => x.leavefrom).ToList();
            }
            return result;
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


        public static List<MODEL.Apply.app_uploadpic> getAttendance(string uid, int requestID,HttpServerUtility server)
        {
            List<WebServiceLayer.WebReference_leave.AttachmentInfo> attachments = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetAttachmentInfoByRequestID_Leave(requestID).ToList();

            List<MODEL.Apply.app_uploadpic> data = new List<MODEL.Apply.app_uploadpic>();
            for (int i = 0; i < attachments.Count; i++)
            {
                string filename = LSLibrary.FileUtil.GetFileName(attachments[i].Path);

                string bigFile = "~/" + BLL.Leave.picPath + "/" + filename;
                string reduceFile = "~/" + BLL.Leave.picPath + "/" + BLL.Leave.reducePath + "/" + filename;
                try
                {
                    common.CopyAttendanceAndReduce(attachments[i].Path, server.MapPath(bigFile), server.MapPath(reduceFile));
                    MODEL.Apply.app_uploadpic tempItem = GeneratePicModel(filename, server);
                    data.Add(tempItem);
                }
                catch
                {
                    string badfile = "~/Res/images/bad.png";
                    MODEL.Apply.app_uploadpic tempItem = GeneratePicModel(badfile, server);
                    data.Add(tempItem);
                }
            }
            return data;
        }

        public static MODEL.Apply.app_uploadpic GeneratePicModel(string filename,System.Web.HttpServerUtility server)
        {
            string bigFile = "~/" + BLL.Leave.picPath + "/" + filename;
            string reduceFile = "~/" + BLL.Leave.picPath + "/" + BLL.Leave.reducePath + "/" + filename;
            string reduceAbsolutionFile = server.MapPath(reduceFile);

            if (!LSLibrary.FileUtil.FileIsExist(reduceAbsolutionFile))
            {
                reduceFile = BLL.Leave.defaultPic;
            }

            MODEL.Apply.app_uploadpic temppic = new MODEL.Apply.app_uploadpic(bigFile, reduceFile, BLL.Leave.GetAttachmentAbsolutePath() + filename);
            return temppic;
        }

        #endregion



        #region unity
        
        public static List<LSLibrary.WebAPP.ValueText<int>> ConvertLeaveInfo2DropDownList(List<WebServiceLayer.WebReference_leave.t_Leave> source)
        {
            List<LSLibrary.WebAPP.ValueText<int>> result = new List<LSLibrary.WebAPP.ValueText<int>>();
            result.Add(new LSLibrary.WebAPP.ValueText<int>(leave_leaveid_nullSelect, "Please select"));
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
                var ddlSourceAppend = BLL.Leave.ConvertInt2DropDownList(sections);
                ddlSource.AddRange(ddlSourceAppend);
            }
            return ddlSource;
        }


        
        public static List<LSLibrary.WebAPP.ValueText<int>> GetDDLSectionsData(int leaveid, int employid)
        {
            List<LSLibrary.WebAPP.ValueText<int>> ddlSource = GetDDLSectionsDataNoSelect(leaveid, employid);
            ddlSource.Insert(0,new LSLibrary.WebAPP.ValueText<int>(leave_sections_nullSelect, "Please select"));
            return ddlSource;
        }

        public static List<WebServiceLayer.WebReference_leave.t_Leave> GetLeavesByStaffID(int sid)
        {
            BLL.User_wsref.CheckWsLogin();
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetAllLeaveTypeByStaffID(sid).ToList();
        }

        public static List<LSLibrary.WebAPP.ValueText<int>> ConvertInt2DropDownList(List<int> source)
        {
            List<LSLibrary.WebAPP.ValueText<int>> result = new List<LSLibrary.WebAPP.ValueText<int>>();
            for (int i = 0; i < source.Count(); i++)
            {
                if (BLL.GlobalVariate.sections.Keys.Contains(source[i]))
                {
                    LSLibrary.WebAPP.ValueText<int> item = new LSLibrary.WebAPP.ValueText<int>(source[i], BLL.GlobalVariate.sections[source[i]]);
                    result.Add(item);
                }
            }
            return result;
        }

        public static Dictionary<string, string> GetAllLeaveSimpleDesc()
        {
            Dictionary<string, string> simpleDesc = new Dictionary<string, string>();
            simpleDesc.Add("PR", "Paid Rest");
            simpleDesc.Add("ML", "Maternity");
            simpleDesc.Add("AL10", "Annual");
            simpleDesc.Add("AL07", "Annual");
            simpleDesc.Add("SL", "Sick");
            simpleDesc.Add("AL14", "Annual");
            simpleDesc.Add("IL", "Injury");
            simpleDesc.Add("NSL", "No-pay");
            simpleDesc.Add("AL12", "Annual");
            simpleDesc.Add("AL16", "Annual");
            simpleDesc.Add("AL18", "Annual");
            simpleDesc.Add("AL0", "Without");
            simpleDesc.Add("AL25", "Annual");
            simpleDesc.Add("NPL", "No Paid");
            simpleDesc.Add("SPL", "Special");
            simpleDesc.Add("PSL", "4/5 Sick");
            simpleDesc.Add("NR", "No Paid");
            simpleDesc.Add("ISL", "Injury");
            simpleDesc.Add("BL", "Business");
            simpleDesc.Add("JURL", "Jury");
            simpleDesc.Add("MARL", "Marriage");
            simpleDesc.Add("PATL", "Paternity");
            simpleDesc.Add("COML", "Compassionate");
            simpleDesc.Add("ILF", "Injury");
            simpleDesc.Add("PL", "No Paid");
            simpleDesc.Add("FSL", "全薪病假");
            simpleDesc.Add("CL", "Compensation");
            simpleDesc.Add("OD", "Outdoor");
            simpleDesc.Add("AL05", "Annual");
            simpleDesc.Add("AL15", "Annual");
            simpleDesc.Add("BODYCHECK", "Body");
            simpleDesc.Add("BIRTHDAY", "Birthday");
            simpleDesc.Add("FC", "Forgot Card");
            simpleDesc.Add("OW", "Out Work");
            simpleDesc.Add("BT", "Business");
            simpleDesc.Add("AL17", "Annual");
            simpleDesc.Add("AL21", "Annual");
            simpleDesc.Add("AL18A", "Annual");
            simpleDesc.Add("AL18P", "Annual");
            simpleDesc.Add("AL_DH", "Annual");
            simpleDesc.Add("AL_DM", "Annual");
            simpleDesc.Add("AL_EXEC", "Annual");
            simpleDesc.Add("AL_NONEG", "Annual");
            simpleDesc.Add("AL_G1", "Annual");
            simpleDesc.Add("AL_G2", "Annual");
            simpleDesc.Add("AL_S1", "Annual");
            simpleDesc.Add("AL_S2", "Annual");
            simpleDesc.Add("AL_S3", "Annual");
            simpleDesc.Add("TRAINING", "Training");

            return simpleDesc;
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

       
        public static double GetGrossValue(int leaveid,int staffid,int employid)
        {
            WebServiceLayer.WebReference_leave.LeaveBalanceType balanceType = GetLeaveBalanceType(leaveid);
            if (balanceType == LeaveBalanceType.accumulabel_sinceJoin_noalsl)
            {
                return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetGrossValue_SinceJoin_excludeALSL(leaveid, staffid);
            }
            else if (balanceType == LeaveBalanceType.accumulabel_sinceJoin_sl || balanceType == LeaveBalanceType.accumulabel_sinceJoin_al)
            {
                double[] temp = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetGrossValue_SinceJoin_ALSL(employid);
                if (balanceType == LeaveBalanceType.accumulabel_sinceJoin_sl)
                {
                    return temp[1];
                }
                else
                {
                    return temp[0];
                }
            }
            else if (balanceType == LeaveBalanceType.accumulabel_overridea)
            {
                return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetGrossValue_Override(leaveid, staffid);
            }
            else
            {
                return 0;
            }
        }

        public static double GetWaitValue(int leaveid, int staffid)
        {
            WebServiceLayer.WebReference_leave.LeaveBalanceType balanceType = GetLeaveBalanceType(leaveid);
            if (balanceType == LeaveBalanceType.accumulabel_sinceJoin_noalsl)
            {
                return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetWaitValue_SinceJoin_excludeALSL(leaveid, staffid);
            }
            else if (balanceType == LeaveBalanceType.accumulabel_sinceJoin_al)
            {
                return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetWaitValue_SinceJoin_ALSL(staffid,true);
            }
            else if (balanceType == LeaveBalanceType.accumulabel_sinceJoin_sl)
            {
                return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetWaitValue_SinceJoin_ALSL(staffid, false);//todo not right
            }
            else if (balanceType == LeaveBalanceType.accumulabel_overridea)
            {
                return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetWaitValue_Override(leaveid, staffid);//todo not right
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}