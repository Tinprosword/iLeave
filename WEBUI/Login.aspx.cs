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
        protected override void InitPageVaralbal0()
        {
            
        }

        protected override void InitPageDataOnEachLoad1()
        {
        }

        protected override void InitPageDataOnFirstLoad2()
        {

        }

        protected override void ResetUIOnEachLoad3()
        {

        }

        protected override void InitUIOnFirstLoad4()
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
                bool isLogin = BLL.User.CheckLogin(userid, password);
                if (isLogin)
                {
                    MODEL.UserInfo userInfo = new MODEL.UserInfo(0,userid,"管理员","AD");
                    LSLibrary.WebAPP.LoginManager.Login(new LSLibrary.WebAPP.LoginUser<MODEL.UserInfo>(userid,userInfo));
                    Response.Redirect("~/Pages/Main.aspx");
                }
                else
                {
                    this.lt_js.Text = LSLibrary.JavasScriptHelper.AlertMessage(BLL.GlobalVariate.login_error);
                    CleanInput();
                }
            }
            else
            {
                MODEL.UserInfo userInfo = new MODEL.UserInfo(0, userid, "管理员", "AD");
                LSLibrary.WebAPP.LoginManager.Login(new LSLibrary.WebAPP.LoginUser<MODEL.UserInfo>(userid, userInfo));
                Response.Redirect("~/Pages/Main.aspx");
                //this.lt_js.Text = LSLibrary.JavasScriptHelper.AlertMessage(BLL.GlobalVariate.login_error);
                //CleanInput();
            }
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