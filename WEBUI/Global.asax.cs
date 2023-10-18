using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace WEBUI
{
    public class Global : System.Web.HttpApplication
    {
        private System.Threading.Timer g_schuduleCheck_1min = null;
        private string patherror = "";
        private string pathPython = "";

        protected void Application_Start(object sender, EventArgs e)
        {
            BLL.GlobalVariate.pageServer = Server;//把server工具类给bll.这样就不用每个页面都赋值,因为页面不是永远在,而application是一直存在
            StartGenerateFileSchedule();
        }

        private void StartGenerateFileSchedule()
        {
            LSLibrary.logHelper.WriteFILEToWebLOG("Application_Start: shedule:", patherror);
            patherror=Server.MapPath("~/ErrorLogs/");
            int periodMilliSeconds = 1000 * 62;//10s
            //new System.Threading.Thread(FindShedules);
            g_schuduleCheck_1min = new System.Threading.Timer(new System.Threading.TimerCallback(FindShedules), 2, 0,periodMilliSeconds);
        }

        private void FindShedules(object obj)
        {
            LSLibrary.logHelper.WriteFILEToWebLOG("loop: shedule:",patherror);
            BLL.Announcement.PushNotice(BLL.GlobalVariate.pageServer);
        }

        protected void Session_Start(object sender, EventArgs e)
        {}

        protected void Application_BeginRequest(object sender, EventArgs e)
        {}

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {}

        protected void Application_Error(object sender, EventArgs e)
        {
            LSLibrary.WebAPP.WebConfig.Global_Application_Error(Server, Request, Response, "~/pages/errorpage.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {}

        protected void Application_End(object sender, EventArgs e)
        {}
    }
}