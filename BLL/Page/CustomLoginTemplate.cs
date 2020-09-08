using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class CustomLoginTemplate : LSLibrary.WebAPP.PageTemplate_logined
    {
        public LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> loginer;

        protected override void InitPage_OnBeforeF5RegisterEvent()
        {
            OnF5Doit = BLL.User_wsref.Onf5;
        }

        protected override void InitPageVaralbal0_OnsessionoutRegisterEvent()
        {
            LSLibrary.WebAPP.LoginManager.OnSessionTimeOutHandler = BLL.User_wsref.GoBackToLogin;
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            loginer = BLL.User_wsref.GetLoginer();
            base.Page_Init(sender, e);
        }
    }
}