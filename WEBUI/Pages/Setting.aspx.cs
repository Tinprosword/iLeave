using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Setting : BLL.CustomLoginTemplate
    {
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
        }



        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().setting_current, "~/pages/main.aspx", true);

            MultipleLanguage(BLL.MultiLanguageHelper.GetLanguagePacket(BLL.MultiLanguageHelper.GetChoose()));
            BLL.Page.MyCookie myCookie = BLL.Page.MyCookieManage.GetCookie();


            int intLanguagae = (int)myCookie.language;


            if (myCookie.isAppLogin =="1")
            {
                this.panel_changeServer.Visible = true;
            }
            else
            {
                this.panel_changeServer.Visible = false;
            }
        }


        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {

        }



        private void MultipleLanguage(LSLibrary.WebAPP.BaseLanguage language)
        {
            lb_changeserver.Text = language.setting_changeLink;
            this.lb_account.Text = language.setting_account;
            this.lb_othersetting.Text = language.setting_otherSetting;
            this.lb_out.Text = language.seting_logout2;
            this.bt_out.Text= language.seting_logout2;
            this.lb_versionname.Text = language.setting_ver;
            this.lb_info.Text = language.setting_appinfo;
            this.lb_privary.Text = language.setting_appprivary;
        }


       

        private static void ChangeSettingSendNotice(int languagetype,Literal literal)
        {
            string agent = HttpContext.Current.Request.UserAgent;
            LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.MobilWebHelper.GetClientType(agent);

            var cookies= BLL.Page.MyCookieManage.GetCookie();

            if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.android && cookies.isAppLogin=="1")//android
            {
                string js = LSLibrary.WebAPP.MyJSHelper.SendMessageToAndroid("savesetting", languagetype.ToString(), HttpContext.Current.Server);
                literal.Text = js;
            }
            else if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone && cookies.isAppLogin == "1")//ios
            {
                string js = LSLibrary.WebAPP.MyJSHelper.SendMessageToIphone("savesetting", languagetype.ToString(), HttpContext.Current.Server);
                literal.Text = js;
            }
            else//pc
            {

            }
        }

        protected void btn_changeserver_Click(object sender, EventArgs e)
        {
            BLL.User_wsref.MPG_GoBackToSign();
        }


        protected void lb_english_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            int language = 0;
            if(lb==this.lb_cn)
            {
                language = 1;
            }
            else if(lb==this.lb_trans)
            {
                language = 2;
            }

            //保存到其他平台
            ChangeSettingSendNotice(language, this.js_webview);

            //cooike 也需要保存
            var myc = BLL.Page.MyCookieManage.GetCookie();
            myc.language = (LSLibrary.WebAPP.LanguageType)language;
            BLL.Page.MyCookieManage.SetCookie(myc);

            LSLibrary.WebAPP.BaseLanguage NewLanguage = BLL.MultiLanguageHelper.GetLanguagePacket(myc.language);
            MultipleLanguage(NewLanguage);
        }

        protected void bt_out_Click(object sender, EventArgs e)
        {
            LSLibrary.WebAPP.LoginManager.Logoff();
            BLL.User_wsref.MPG_GoBackToLogin();
        }

        protected void lb_out_Click(object sender, EventArgs e)
        {
            LSLibrary.WebAPP.LoginManager.Logoff();
            BLL.User_wsref.MPG_GoBackToLogin();
        }

        protected void lb_info_Click(object sender, EventArgs e)
        {
            string url = string.Format("article.aspx");
            Response.Redirect(url);
        }

        protected void lb_privary_Click(object sender, EventArgs e)
        {
            string url = string.Format("article_pripary.aspx");
            Response.Redirect(url);
        }
    }
}