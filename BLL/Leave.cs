using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.WebReference_leave;

namespace BLL
{
    public class Leave
    {
        public static DAL.WebReference_leave.StaffLeaveRequest[] getLeaveAppliationsByStaffid(int[] staffid)
        {
            BLL.LoginManager.CheckWsLogin();
            return DAL.Leave.getLeaveAppliationsByStaffid(staffid);
        }

        public static DAL.WebReference_leave.StaffLeaveDetailInBatch[] GetOnlineStaffLeaveRecordByStaffIDBatchMode(int[] staffid,DateTime from,DateTime to)
        {
            BLL.LoginManager.CheckWsLogin();
            return DAL.Leave.GetOnlineStaffLeaveRecordByStaffIDBatchMode(staffid, from, to);
        }
    }
}