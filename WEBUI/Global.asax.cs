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
        protected void Application_Start(object sender, EventArgs e)
        {
            BLL.GlobalVariate.pageServer = Server;//把server工具类给bll.这样就不用每个页面都赋值,因为页面不是永远在,而application是一直存在
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