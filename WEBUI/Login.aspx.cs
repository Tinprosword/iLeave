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
        protected override void InitPageDataOnEachLoad()
        {
        }

        protected override void InitUIOnFirstLoad()
        {
        }

        protected override void ResetUIOnEachLoad()
        {
            
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
                    MODEL.UserInfo userInfo = new MODEL.UserInfo();
                    LSLibrary.WebAPP.LoginManager.Login(new LSLibrary.WebAPP.LoginUser<MODEL.UserInfo>(userid,userInfo));
                    Response.Redirect("~/Pages/Main.aspx");
                }
                else
                {
                    this.lt_js.Text = LSLibrary.JavasScriptHelper.AlertMessage(AppHelper.GlobalVariate.login_error);
                    CleanInput();
                }
            }
            else
            {
                this.lt_js.Text = LSLibrary.JavasScriptHelper.AlertMessage(AppHelper.GlobalVariate.login_error);
                CleanInput();
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