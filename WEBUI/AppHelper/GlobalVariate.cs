using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.AppHelper
{
    public class GlobalVariate
    {
        //bll
        public static BLL.t_attendanceClockGoGo bll_t_Attendance = new BLL.t_attendanceClockGoGo();


        //global string
        public static string login_error = "invalid user and password.";
    }
}