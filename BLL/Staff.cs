using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Staff
    {
        public static DAL.WebReference_staff.BasicStaffInfo[] GetStaffid(int uid)
        {
            BLL.LoginManager.CheckWsLogin();
            return DAL.Staff.GetStaffid(uid);
        }

        public static int[] GetStaffids(int uid)
        {
            var ids= DAL.Staff.GetStaffid(uid).Select(x => x.ID);
            if(ids!=null && ids.Count()>0)
            {
                return ids.ToArray();
            }
            else
            {
                return new int[0];
            }
        }
    }
}