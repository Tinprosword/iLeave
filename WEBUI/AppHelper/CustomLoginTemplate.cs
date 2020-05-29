using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.AppHelper
{
    public abstract class CustomLoginTemplate:LSLibrary.WebAPP.PageTemplate_logined
    {
        public LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> loginer;
        protected override void Page_Init(object sender, EventArgs e)
        {
            loginer = BLL.User.GetLoginer();
            base.Page_Init(sender, e);
        }
    }
}