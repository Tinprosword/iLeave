using DAL.WebReference_codesetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CodeSetting
    {
        public static LeaveInfo[] GetLeaveInfo()
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            return webServicesHelper.ws_codesetting.GetLeaveInfo();
        }
    }
}