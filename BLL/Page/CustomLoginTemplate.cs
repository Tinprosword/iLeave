using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


////注意ResetUIOnEachLoad,只做一些清除工作, 会有很多工作其实是放入到 事件结尾处,由某个事件所带来的ui更新,并非每次都要

namespace BLL
{
    public abstract class CustomLoginTemplate : LSLibrary.WebAPP.PageTemplate_Common
    {
        public LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> loginer;

        protected override void InitPage_OnBeforeF5RegisterEvent()
        {
            OnF5Doit = BLL.User_wsref.Onf5;
        }

        protected  void InitPageVaralbal0_OnsessionoutRegisterEvent()
        {
            LSLibrary.WebAPP.LoginManager.OnSessionTimeOutHandler = BLL.User_wsref.GoBackToLogin;
        }

        protected override void Page_Init(object sender, EventArgs e)
        {
            loginer = BLL.User_wsref.GetLoginer();

            InitPage_OnBeforeF5RegisterEvent();
            isF5 = CheckF5();
            InitPageVaralbal0_OnsessionoutRegisterEvent();

            var  mycookie=  BLL.Page.MyCookieManage.GetCookie();

            LSLibrary.WebAPP.LoginManager.CheckIsLogin();
            InitPage_OnEachLoadAfterCheckSessionAndF5_1();
            if (!IsPostBack)
            {
                InitPage_OnFirstLoad2();
            }

            base.Page_Init(sender, e);
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            CustomLoginTemplate.SetPageLanguage(this);
            CustomLoginTemplate.ResetFormWhenPC(this);
            base.Page_Load(sender, e);
        }

        public static void ResetFormWhenPC(System.Web.UI.Page page)
        {
            string agent = HttpContext.Current.Request.UserAgent;
            LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.HttpContractHelper.GetClientType(agent);

            if (ClientType == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.pc)
            {
                page.Form.Style.Add("width", "364px");
                page.Form.Style.Add("margin-left", "40%");
                page.Form.Style.Add("border", "2px solid #808080");
                page.Form.Style.Add("min-height", "640px");
                page.Form.Style.Add("margin-top", "40px");
            }
        }

        public static void SetPageLanguage(System.Web.UI.Page page)
        {
            LSLibrary.WebAPP.LanguageType type = LSLibrary.WebAPP.LanguageType.english;
            try
            {
                type = BLL.Page.MyCookieManage.GetCookie().language;
            }
            catch { }
            if (type == LSLibrary.WebAPP.LanguageType.sc)
            {
                page.Culture = "zh-CN";
            }
            else if (type == LSLibrary.WebAPP.LanguageType.tc)
            {
                page.Culture = "zh-TW";
            }
            else
            {
                page.Culture = "en-GB";
            }
        }
    }

    public abstract class CustomCommonTemplate : LSLibrary.WebAPP.PageTemplate_Common
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            CustomLoginTemplate.ResetFormWhenPC(this);
            base.Page_Load(sender, e);
        }
    }
}