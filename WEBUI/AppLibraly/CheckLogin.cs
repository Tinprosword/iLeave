using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.AppLibraly
{
    public class CheckLogin : System.Web.UI.Page
    {
        protected virtual void Page_Init(object sender, EventArgs e)
        {
            //check weather user login and do some progress.
            if (LoginUser.IsLogin() == false)
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}