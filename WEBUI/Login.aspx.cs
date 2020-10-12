using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class Login :BLL.CustomCommonTemplate
    {

        protected override void InitPage_OnBeforeF5RegisterEvent()
        {}

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            BLL.Page.MyCookie myCookie = BLL.Page.MyCookieManage.GetCookie();
            if (myCookie == null || myCookie.serverAddress == "")
            {
                Response.Redirect("sign.aspx");
            }
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {}

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {}

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            LoadLableLanguage(BLL.MultiLanguageHelper.GetLanguagePacket());

            BLL.Page.MyCookie cookie = BLL.Page.MyCookieManage.GetCookie();
            string isremember = cookie.isRemember;
            LSLibrary.WebAPP.LanguageType language = cookie.language;

            this.rbl_language.SelectedValue = ((int)language).ToString();
            this.cb_remember.Checked = (isremember == "1" ? true : false);

            
            if (!string.IsNullOrEmpty(isremember) && isremember == "1")
            {
                ProgressLogin(cookie.loginname, cookie.loginpsw);
            }
        }

        private void ProgressLogin(string userid,string password)
        {
            if (string.IsNullOrWhiteSpace(userid) == false && string.IsNullOrWhiteSpace(password) == false)
            {
                WebServiceLayer.WebReference_user.LoginResult loginResult = BLL.User_wsref.CheckLogin(userid, password);
                bool isLogin = loginResult.Result > 0 ? true : false;
                if (isLogin)
                {
                    if (this.cb_remember.Checked)
                    {
                        BLL.Page.MyCookieManage.SetCookie_isRmember("1");
                        BLL.Page.MyCookieManage.SetCookie_name(userid);
                        BLL.Page.MyCookieManage.SetCookie_psw(password);
                    }

                    //check weather more employment?
                    BLL.User_wsref.SaveInfoToSession(userid, loginResult);
                    Response.Redirect("~/Pages/Main.aspx");
                }
                else
                {
                    this.lt_js.Text = LSLibrary.JavasScriptHelper.AlertMessage(BLL.GlobalVariate.login_error);
                    CleanInput();
                }
            }
            else
            { }
        }


        private void LoadLableLanguage(LSLibrary.WebAPP.BaseLanguage baseLanguage)
        {
            this.lt_user.Text = baseLanguage.login_user;
            this.lt_password.Text = baseLanguage.login_password;
            this.Button1.Text= baseLanguage.login_loginbtn;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string userid = this.tb_user.Text.Trim();
            string password = this.tb_password.Text.Trim();

            ProgressLogin(userid, password);
        }

        private void CleanInput()
        {
            this.tb_user.Text = "";
            this.tb_password.Text = "";
        }

        protected void cb_remember_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.cb_remember.Checked)
            {
                BLL.Page.MyCookieManage.SetCookie_isRmember("0");
            }
            else
            {
                BLL.Page.MyCookieManage.SetCookie_isRmember("1");
            }
        }

        protected void rbl_language_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectlang = (LSLibrary.WebAPP.LanguageType)int.Parse(this.rbl_language.SelectedValue);
            BLL.Page.MyCookieManage.SetCookie_language(selectlang);
            LSLibrary.WebAPP.BaseLanguage baseLanguage = BLL.MultiLanguageHelper.GetLanguagePacket(selectlang);
            LoadLableLanguage(baseLanguage);
        }

    }
}