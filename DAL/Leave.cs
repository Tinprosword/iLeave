using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Leave
    {
        public static string LEAVE_DESC = "Leave Request";

        #region insert
        public static int InsertLeave(List<MODEL.Apply.apply_LeaveData> originDetail, int userid, int? staffid, string remarks, out WebReference_leave.StaffLeaveRequest[] details, out WebReference_leave.ErrorMessageInfo messageInfo)
        {
            int result = -1;//默认为一般错误
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            List<WebReference_leave.StaffLeaveRequest> detail = GenerateLeaveRequest(originDetail, userid);

            messageInfo = new WebReference_leave.ErrorMessageInfo();
            try
            {
                messageInfo = webServicesHelper.ws_leave.InsertOnlineLeaveApplicationRequest(detail.ToArray(), WebReference_leave.ApprovalRequestStatus.WAIT_FOR_APPROVE, userid, staffid);
                result = 0;
            }
            catch
            {
                result = -1;
            }
            details = detail.ToArray();
            return result;
        }
        public static int InsertAttanchMent()
        {
            return 0;
        }
        public static int InsertWorkflow(object details, int uid, int requestLeaveID, int employMentID)
        {
            int result = 0;
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            result = webServicesHelper.ws_leave.CreateNewRequest(null, WebReference_leave.WorkflowTypeID.LEAVE_APPLICATION, details, uid, LEAVE_DESC, "", "", "", requestLeaveID, employMentID);
            return result;
        }
        public static List<WebReference_leave.StaffLeaveRequest> GenerateLeaveRequest(List<MODEL.Apply.apply_LeaveData> originDetail, int uid)
        {
            List<WebReference_leave.StaffLeaveRequest> result = new List<WebReference_leave.StaffLeaveRequest>();

            for (int i = 0; i < originDetail.Count; i++)
            {
                int employmentID = GetEmployID(uid, originDetail[i].LeaveDate);

                if (employmentID > 0)
                {
                    originDetail[i].leavetypeid = originDetail[i].leavetypeid;

                    WebReference_leave.StaffLeaveRequest newItem = new WebReference_leave.StaffLeaveRequest();

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
        #endregion

        #region search

        #endregion

        #region process
        public static void ApproveRequest(DAL.WebReference_leave.MyWorkflowTask WorkflowTaskObject, DAL.WebReference_leave.WorkflowTypeID TaskType, object p_ApprovalRequest, int UserID, string Description, string FormulatedURL, string baseURL)
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            webServicesHelper.ws_leave.ApproveRequest(WorkflowTaskObject, TaskType, p_ApprovalRequest, UserID, Description, FormulatedURL, baseURL);
        }

        public static void CancelRequest(DAL.WebReference_leave.MyWorkflowTask WorkflowTaskObject, DAL.WebReference_leave.WorkflowTypeID TaskType, object p_ApprovalRequest, int UserID, string Description, string FormulatedURL, string baseURL)
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            webServicesHelper.ws_leave.CancelRequest(WorkflowTaskObject, TaskType, p_ApprovalRequest, UserID, Description, FormulatedURL, baseURL);
        }

        public static void RejectRequest(DAL.WebReference_leave.MyWorkflowTask WorkflowTaskObject, DAL.WebReference_leave.WorkflowTypeID TaskType, object p_ApprovalRequest, int UserID, string Description, string FormulatedURL, string baseURL)
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            webServicesHelper.ws_leave.RejectRequest(WorkflowTaskObject, TaskType, p_ApprovalRequest, UserID, Description, FormulatedURL, baseURL);
        }
        #endregion

        #region utinity
        public static int GetEmployID(int uid, DateTime date)
        {
            int result = 0;
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            WebReference_staff.EmploymentInfo info = webServicesHelper.ws_staff.GetEmploymentInfoByUserIDAndValidDate(uid, date);
            if (info != null)
            {
                result = info.EmploymentID;
            }
            return result;
        }
        private static int GetEmployHours(int employid)
        {//todo it
            return 8;
        }
        public static DAL.WebReference_leave.LeaveInfo[] GetLeaveInfoByStaffID(int staffid)
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            return webServicesHelper.ws_leave.GetAllLeaveTypeByStaffID(staffid);
        }

        #endregion

    }
}