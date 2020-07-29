using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Staff
    {
        public static DAL.WebReference_staff.BasicStaffInfo[] GetStaff(int uid)
        {
            BLL.LoginManager.CheckWsLogin();
            return DAL.Staff.GetStaffByid(uid);
        }

        public static int[] GetStaffids(int uid)
        {
            var ids= DAL.Staff.GetStaffByid(uid).Select(x => x.ID);
            if(ids!=null && ids.Count()>0)
            {
                return ids.ToArray();
            }
            else
            {
                return new int[0];
            }
        }

        //todo needcheck
        public static string GetNameByid(int uid)
        {
            string result = "";
            var res = GetStaff(uid);
            if(res!=null && res.Count()>0)
            {
                result=res[0].StaffEngName;
            }
            return result;
        }

    }
}