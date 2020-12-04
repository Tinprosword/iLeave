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
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, BLL.MultiLanguageHelper.GetLanguagePacket().setting_current, "~/pages/main.aspx", true);

            LoadLableLanguage(BLL.MultiLanguageHelper.GetLanguagePacket(BLL.MultiLanguageHelper.GetChoose()));
            BLL.Page.MyCookie myCookie = BLL.Page.MyCookieManage.GetCookie();

            this.lb_serveraddress.Text = myCookie.serverAddress;
            int intLanguagae = (int)myCookie.language;
            this.cb_languagea.SelectedValue = intLanguagae.ToString();


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
        {}

        private void LoadLableLanguage(LSLibrary.WebAPP.BaseLanguage language)
        {
            this.lt_address.Text = language.setting_service;
            this.lt_language.Text = language.setting_language;
            this.lt_changeServer.Text = language.setting_changeLink;
        }

        //多平台同步。
        protected void cb_languagea_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnChangeSettingSendNotice(int.Parse(this.cb_languagea.SelectedValue), this.js_webview);
            LSLibrary.WebAPP.LanguageType chooseLanguage = (LSLibrary.WebAPP.LanguageType)int.Parse(this.cb_languagea.SelectedValue);

            var myc = BLL.Page.MyCookieManage.GetCookie();
            myc.language = chooseLanguage;
            BLL.Page.MyCookieManage.SetCookie(myc);

            LSLibrary.WebAPP.BaseLanguage NewLanguage= BLL.MultiLanguageHelper.GetLanguagePacket(chooseLanguage);
            LoadLableLanguage(NewLanguage);
        }

        private static void OnChangeSettingSendNotice(int languagetype,Literal literal)
        {
            string agent = HttpContext.Current.Request.UserAgent;

            LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.HttpContractHelper.GetClientType(agent);
            if (ClientType == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.android)//android
            {
                string js = LSLibrary.WebAPP.MyJSHelper.SendMessageToAndroid("savesetting", languagetype.ToString(), HttpContext.Current.Server);
                literal.Text = js;
            }
            else if (ClientType == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.iphone)//ios
            {
                string js = LSLibrary.WebAPP.MyJSHelper.SendMessageToIphone("savesetting", languagetype.ToString(), HttpContext.Current.Server);
                literal.Text = js;
            }
            else//pc
            {}
        }

        protected void btn_changeserver_Click(object sender, ImageClickEventArgs e)
        {
            BLL.User_wsref.MPG_GoBackToSign();
        }


        //protected void changeUser_Click(object sender, ImageClickEventArgs e)
        //{
        //    Response.Redirect("~/Pages/chooseEmployment.aspx?pid=" + loginer.userInfo.personid + "&sourcetype=2");
        //}

    }
}