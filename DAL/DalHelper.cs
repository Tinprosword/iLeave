using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalHelper
    {
        public static string GetWebServices()
        {
            return LSLibrary.WebAPP.WebConfig.getValue("webServices");
        }


        public static string GetCookieStringOfSessionID(string sessionid)
        {
            string idname = LSLibrary.WebAPP.WebConfig.getValue("SessionIDName");

            string cookieStringOfSession = idname + "=" + sessionid;
            return cookieStringOfSession;
        }


        public class WebServicesHelper
        {
            private static WebServicesHelper webServicesHelper;

            public WebReference_User.UserManagementV2 ws_user = new WebReference_User.UserManagementV2();
            public System.Net.CookieContainer cookieContainer = new System.Net.CookieContainer();

            public static WebServicesHelper GetInstance()
            {
                if(webServicesHelper == null)
                {
                    webServicesHelper = new WebServicesHelper();
                }
                return webServicesHelper;
            }

            private WebServicesHelper()
            {
                ws_user.CookieContainer = cookieContainer;
            }
        }


    }
}