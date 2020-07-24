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


        //-1.一般插入错误.0,成功.
        public static int InsertLeave(List<MODEL.Apply.LeaveData> originDetail, int userid, int? staffid, string remarks, out WebReference_leave.StaffLeaveRequest[] details, out WebReference_leave.ErrorMessageInfo messageInfo)
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


        public static List<WebReference_leave.StaffLeaveRequest> GenerateLeaveRequest(List<MODEL.Apply.LeaveData> originDetail, int uid)
        {
            List<WebReference_leave.StaffLeaveRequest> result = new List<WebReference_leave.StaffLeaveRequest>();

            for (int i = 0; i < originDetail.Count; i++)
            {
                int employmentID = GetEmployID(uid, originDetail[i].LeaveDate);

                if (employmentID > 0)
                {
                    originDetail[i].typeid = originDetail[i].typeid;

                    WebReference_leave.StaffLeaveRequest newItem = new WebReference_leave.StaffLeaveRequest();

                    newItem.CompareKey = null;
                    newItem.CreateDate = System.DateTime.Now;
                    newItem.DelegationToStaffID = null;
                    newItem.DeleteKey = originDetail[i].LeaveDate.ToString("yyyy-MM-dd") + "," + originDetail[i].typeid.ToString();
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
                    newItem.LeaveID = originDetail[i].typeid;
                    newItem.LeaveTypeName = originDetail[i].typeDescription;
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


        public static WebReference_leave.StaffLeaveRequest[] getLeaveAppliationsByStaffid(int[] staffid)
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            WebReference_leave.StaffLeaveRequest[] result= webServicesHelper.ws_leave.GetOnlineStaffLeaveRecordByStaffID(staffid);
            return result;
        }


        public static WebReference_leave.StaffLeaveDetailInBatch[] GetOnlineStaffLeaveRecordByStaffIDBatchMode(int[] staffid,DateTime from,DateTime to)
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            WebReference_leave.StaffLeaveDetailInBatch[] result= webServicesHelper.ws_leave.GetOnlineStaffLeaveRecordByStaffIDBatchMode(staffid, from, to);
            return result;
        }

        public enum LeaveSectionCustom
        {
            FULLDAY = 0,
            AM = 1,
            PM = 2,
            Section3 = 3,
            Hour = 4,
        }

    }
}