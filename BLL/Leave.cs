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
        #region insert application
        private static int CheckBeforeApply()
        {
            return 0;
        }

        private static int InsertAttanchMent()
        {
            return 0;
        }

        //0 ok. -1 check error -2.insert error
        public static int InsertLeave(List<MODEL.Apply.apply_LeaveData> originDetail, int userid, int employmentid, int? staffid, string remarks, out string errorMsg)
        {
            BLL.User_wsref.CheckWsLogin();

            errorMsg = "";
            int result = -1;
            int checkResult = CheckBeforeApply();
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
                    int attenchMentResult = InsertAttanchMent();
                    result = 0;
                }
                else
                {
                    errorMsg = "";
                    result = -2;
                }
            }
            else
            {
                errorMsg = "";
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
                result = 0;
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

        private static int GetEmployHours(int employid)
        {
            //todo it
            return 8;
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

        public static List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> GetExtendLeaveDetailsByDate(DateTime dt,int[] employmentids)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetExtendLeaveDetailsByDate(dt, employmentids).ToList();
        }

        public static List<int> GetMonthStatistic(int year, int month, int[] employmentids)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetMonthStatistic(year, month, employmentids).ToList();
        }


        public static List<MODEL.Apply.app_uploadpic> getAttendance(string uid, int applicationID)
        {
            List<MODEL.Apply.app_uploadpic> data = new List<MODEL.Apply.app_uploadpic>();
            for (int i = 0; i < 1; i++)
            {
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
            }
            return data;
        }
        #endregion

        public static List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> getListSource(DateTime dt,List<int> employids)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetExtendLeaveDetailsByDate(dt, employids.ToArray()).ToList();
        }

        #region unity
        public static List<LSLibrary.WebAPP.ValueText<int>> ConvertLeaveInfo2DropDownList(List<WebServiceLayer.WebReference_leave.t_Leave> source)
        {
            List<LSLibrary.WebAPP.ValueText<int>> result = new List<LSLibrary.WebAPP.ValueText<int>>();
            result.Add(new LSLibrary.WebAPP.ValueText<int>(0, "Please select"));
            for (int i = 0; i < source.Count(); i++)
            {
                LSLibrary.WebAPP.ValueText<int> item = new LSLibrary.WebAPP.ValueText<int>(source[i].ID, source[i].Code + " -" + source[i].Description);
                result.Add(item);
            }
            return result;
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

        #endregion
    }
}