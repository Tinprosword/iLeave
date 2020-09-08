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

        protected override void PageLoad_ResetUIOnEachLoad3()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().setting_back, BLL.MultiLanguageHelper.GetLanguagePacket().setting_current, "~/pages/main.aspx");

            LoadLableLanguage(BLL.MultiLanguageHelper.GetLanguagePacket(BLL.MultiLanguageHelper.GetChoose()));
            BLL.Page.MyCookie myCookie = BLL.Page.MyCookieManage.GetCookie();

            this.lb_serveraddress.Text = myCookie.serverAddress;
            int intLanguagae = (int)myCookie.language;
            this.cb_languagea.SelectedValue = intLanguagae.ToString();
        }


        protected override void PageLoad_ResetUIOnEachLoad5()
        {
        }

            private void LoadLableLanguage(LSLibrary.WebAPP.BaseLanguage language)
        {
            this.lt_address.Text = language.setting_service;
            this.lt_language.Text = language.setting_language;
            this.lt_language0.Text= language.setting_logout;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected void cb_languagea_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnChangeSettingSendNotice(int.Parse(this.cb_languagea.SelectedValue), this.js_webview);
            LSLibrary.WebAPP.LanguageType chooseLanguage = (LSLibrary.WebAPP.LanguageType)int.Parse(this.cb_languagea.SelectedValue);
            BLL.Page.MyCookieManage.SetCookie_language(chooseLanguage);
            LSLibrary.WebAPP.BaseLanguage NewLanguage= BLL.MultiLanguageHelper.GetLanguagePacket(chooseLanguage);//todo 只有这个页面特殊，无法立即读cooike,因为是立即修改，还未写到cooike.
            LoadLableLanguage(NewLanguage);
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            LSLibrary.WebAPP.LoginManager.Logoff();
            BLL.User_wsref.GoBackToLogin();
        }


        private static void OnChangeSettingSendNotice(int languagetype,Literal literal)
        {
            string agent = HttpContext.Current.Request.UserAgent;

            LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.HttpContractHelper.GetClientType(agent);
            if (ClientType == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.android)//android
            {
                string js = LSLibrary.WebAPP.MyJSHelper.GetAndroidJs("savesetting", languagetype.ToString(), HttpContext.Current.Server);
                literal.Text = js;
            }
            else if (ClientType == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.iphone)//ios
            {
                string js = LSLibrary.WebAPP.MyJSHelper.GetIphoneJs("savesetting", languagetype.ToString(), HttpContext.Current.Server);
                literal.Text = js;
            }
            else//pc
            {
            }
        }
    }
}