using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.AppLibraly
{
    //注意ResetUIOnEachLoad,只做一些清除工作, 会有很多工作其实是放入到 事件结尾处,由某个事件所带来的ui更新,并非每次都要
    public abstract class PageTemplate_logined: PageTemplate_Common
    {
        protected override void Page_Init(object sender, EventArgs e)
        {
            CheckLogin checkLogin = new CheckLogin();
            checkLogin.event_OnSessionTimeOut += CheckLogin_event_OnSessionTimeOut;
            checkLogin.CheckIsLogin();
            base.Page_Init(sender,e);
        }

        private void CheckLogin_event_OnSessionTimeOut()
        {
            Response.Redirect("~/login.aspx");
        }
    }


    public abstract class PageTemplate_Common : System.Web.UI.Page
    {
        protected virtual void Page_Init(object sender, EventArgs e)
        {
            InitPageDataOnEachLoad();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ResetUIOnEachLoad();
            if (!IsPostBack)
            {
                InitUIOnFirstLoad();
            }
        }

        protected abstract void InitPageDataOnEachLoad();
        protected abstract void InitUIOnFirstLoad();
        protected abstract void ResetUIOnEachLoad();
    }
}