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
                MODEL.LoginResult loginResult = BLL.User_wsref.CheckLogin(userid, password);
                bool isLogin = loginResult.Result > 0 ? true : false;
                if (isLogin)
                {
                    DAL.WebReference_User.PersonBaseinfo personBaseinfo = BLL.User_wsref.GetPersonBaseinfos_validateDefaultEmploymentNow(loginResult.Result);
                    MODEL.UserInfo userInfo;
                    if (personBaseinfo!=null)
                    {
                        userInfo = new MODEL.UserInfo(loginResult.Result, userid, "",  loginResult.SessionID, personBaseinfo.e_id, personBaseinfo.e_EmploymentNumber, personBaseinfo.s_id, personBaseinfo.s_StaffNumber);
                    }
                    else
                    {
                        userInfo = new MODEL.UserInfo(loginResult.Result, userid, "", loginResult.SessionID, null, null,null, null);
                    }
                    
                    LSLibrary.WebAPP.LoginManager.SetLoginer(new LSLibrary.WebAPP.LoginUser<MODEL.UserInfo>(userid,userInfo));
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