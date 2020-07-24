using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DAL
{
    public class Staff
    {
        public static WebReference_staff.BasicStaffInfo[] GetStaffid(int uid)
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            WebReference_staff.BasicStaffInfo[]  result = webServicesHelper.ws_staff.GetStaffIDByUserID(uid);
            return result;
        }
    }
}