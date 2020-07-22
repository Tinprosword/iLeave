using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Leave
    {
        public static void InsertLeave(List<MODEL.Apply.LeaveData> originDetail,int userid,int staffid,string remarks)
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            List<WebReference_leave.StaffLeaveDetails> detail = new List<WebReference_leave.StaffLeaveDetails>();
            for(int i=0;i<originDetail.Count;i++)
            {
                WebReference_leave.StaffLeaveDetails newItem = new WebReference_leave.StaffLeaveDetails();
                newItem.ApprovalStatus = 0;
                newItem.ChiName = null;
                newItem.Code = null;
                newItem.CreateDate = System.DateTime.Now;
                newItem.Date = System.DateTime.Now;
                newItem.DeleteKey = "2020-07-02,11";//from date, type id.
                newItem.DisplaySection = 0;
                newItem.DisplaySectionCombined = null;
                newItem.DisplayUnit = "1 D";//1d: 1 day
                newItem.EmploymentID = 19747;//19747
                newItem.EmploymentNumber = null;
                newItem.EngName = null;
                newItem.HolidayCode = "";
                newItem.ID = 0;
                newItem.IsHalfDay = false;
                newItem.LeaveCalculationTypeDesc = "N/A";
                newItem.LeaveCalculationTypeID = -1;
                newItem.LeaveFrom = originDetail[i].DateTime;
                newItem.LeaveHours = 0;
                newItem.LeaveTo = originDetail[i].DateTime;
                newItem.Remarks = remarks;
                newItem.RequestID = 0;
                newItem.Section = 0;
                newItem.Sections = 0;
                newItem.SecurityGroupCode = null;
                newItem.TotalWorkHours = 8;
                newItem.Type = originDetail[i].typename;
                newItem.TypeID = originDetail[i].typeid;
                newItem.Unit = 1;
                newItem.WorkingHourAM = 0;
                newItem.WorkingHourHalfDay = 0;
                newItem.WorkingHourPattern = null;
                newItem.WorkingHourPM = 0;

                detail.Add(newItem);
            }
            try
            {
                WebReference_leave.ErrorMessageInfo error = webServicesHelper.ws_leave.InsertStaffLeaveDetails(detail.ToArray(), userid, staffid);
            }
            catch
            {

            }
            int a = 4;
        }
    }
}