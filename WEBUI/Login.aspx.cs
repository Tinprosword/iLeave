using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class Login : LSLibrary.WebAPP.PageTemplate_Common
    {
        protected override void InitPage_OnEachLoadBeforeF5_1()
        {
            BLL.Page.MyCookie myCookie = BLL.Page.MyCookieManage.GetCookie();
            if (myCookie == null || myCookie.serverAddress == "")
            {
                Response.Redirect("sign.aspx");
            }
        }

        protected override void PageLoad_ResetUIOnEachLoad5()
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
            LoadLableLanguage();
            this.appcss.Href += "?lastmodify="+BLL.GlobalVariate.appcssLastmodify;
        }


        private void LoadLableLanguage()
        {
            this.lt_user.Text = BLL.MultiLanguageHelper.GetLanguagePacket().login_user;
            this.lt_password.Text = BLL.MultiLanguageHelper.GetLanguagePacket().login_password;
            this.Button1.Text= BLL.MultiLanguageHelper.GetLanguagePacket().login_loginbtn;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userid = this.tb_user.Text.Trim();
            string password = this.tb_password.Text.Trim();

            if (string.IsNullOrWhiteSpace(userid) == false && string.IsNullOrWhiteSpace(password) == false)
            {
                WebServiceLayer.WebReference_user.LoginResult loginResult = BLL.User_wsref.CheckLogin(userid, password);
                bool isLogin = loginResult.Result > 0 ? true : false;
                if (isLogin)
                {
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
            {}
        }

        private void CleanInput()
        {
            this.tb_user.Text = "";
            this.tb_password.Text = "";
        }


        protected void Btn_setting_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/setting.aspx");
        }

        
    }
}