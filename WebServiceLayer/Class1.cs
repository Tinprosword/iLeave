using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServiceLayer
{
    public class MyWebService
    {
        public static WebServicesHelper GlobalWebServices = WebServicesHelper.GetInstance();

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


            public WebReference_codesettings.CodeSettingsV2 ws_codesetting = new WebReference_codesettings.CodeSettingsV2();
            public WebReference_user.UserManagementV2 ws_user = new WebReference_user.UserManagementV2();
            public WebReference_leave.LeaveManagementV2 ws_leave = new WebReference_leave.LeaveManagementV2();
            public WebReference_workflow.WorkflowV2 ws_workflow = new WebReference_workflow.WorkflowV2();



            public System.Net.CookieContainer cookieContainer = new System.Net.CookieContainer();


            public static WebServicesHelper GetInstance()
            {
                if (webServicesHelper == null)
                {
                    webServicesHelper = new WebServicesHelper();
                }
                return webServicesHelper;
            }

            private WebServicesHelper()
            {
                ws_user.CookieContainer = cookieContainer;
                ws_leave.CookieContainer = cookieContainer;
                ws_codesetting.CookieContainer = cookieContainer;
                ws_workflow.CookieContainer = cookieContainer;
            }
        }
    }
}