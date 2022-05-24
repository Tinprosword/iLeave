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
        private string queryAction = "";
        protected override void InitPage_OnBeforeF5RegisterEvent()
        {}

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["action"]))
            {
                queryAction = Request.QueryString["action"];
            }
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {}

        protected override void InitPage_OnFirstLoad2()
        {}


        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            LoadLableLanguage(BLL.MultiLanguageHelper.GetLanguagePacket());

            BLL.Page.MyCookie cookie = BLL.Page.MyCookieManage.GetCookie();
            string isremember = cookie.isRemember;
            LSLibrary.WebAPP.LanguageType language = cookie.language;

            DisplayLanguage(language);

            //this.cb_remember.Checked = (isremember == "1" ? true : false);


            if (!string.IsNullOrEmpty(isremember) && isremember == "1" && queryAction == "")
            {
                ProgressLogin(cookie.loginname, cookie.loginpsw);
            }
            else
            {
                if (queryAction != "")
                {
                    if (!string.IsNullOrEmpty(isremember) && isremember == "1")
                    {
                        this.tb_u1.Text = cookie.loginname;
                        this.tb_p1.Text = cookie.loginpsw;
                    }
                    else
                    {
                        this.tb_u1.Text = "";
                        this.tb_p1.Text = "";
                    }
                }
                else
                {
                    this.tb_u1.Text = "";
                    this.tb_p1.Text = "";
                }
            }

            //reset isapp=0
            cookie.isAppLogin = "0";
            BLL.Page.MyCookieManage.SetCookie(cookie);

            this.Page.SetFocus(this.Button1);
        }

        private void DisplayLanguage(LSLibrary.WebAPP.LanguageType tt)
        {
            this.lb_eng.CssClass = "loginUnSelect";
            this.lb_sc.CssClass = "loginUnSelect";
            this.lb_tc.CssClass = "loginUnSelect";
            if (tt == LSLibrary.WebAPP.LanguageType.english)
            {
                this.lb_eng.CssClass = "loginSelect";
            }
            else if (tt == LSLibrary.WebAPP.LanguageType.sc)
            {
                this.lb_sc.CssClass = "loginSelect";
            }
            else
            {
                this.lb_tc.CssClass = "loginSelect";
            }
            this.tb_u1.Attributes.Add("placeholder", BLL.MultiLanguageHelper.GetLanguagePacket(tt).Common_user);
            this.tb_p1.Attributes.Add("placeholder", BLL.MultiLanguageHelper.GetLanguagePacket(tt).Common_password);
        }

        private void ProgressLogin(string userid,string password)
        {
            if (string.IsNullOrWhiteSpace(userid) == false && string.IsNullOrWhiteSpace(password) == false)
            {
                WebServiceLayer.WebReference_user.LoginResult loginResult = BLL.User_wsref.CheckLogin(userid, password);
                bool isLogin = loginResult.Result > 0 ? true : false;
                if (isLogin)
                {
                    var cookie = BLL.Page.MyCookieManage.GetCookie();
                    cookie.isRemember = "0";
                    BLL.Page.MyCookieManage.SetCookie(cookie);
                    //if (this.cb_remember.Checked)
                    //{
                    //    var cookie = BLL.Page.MyCookieManage.GetCookie();
                    //    cookie.isRemember = "1";
                    //    cookie.loginname = userid;
                    //    cookie.loginpsw = password;
                    //    BLL.Page.MyCookieManage.SetCookie(cookie);
                    //}

                    cookie.isRemember = "1";
                    cookie.loginname = userid;
                    cookie.loginpsw = password;
                    BLL.Page.MyCookieManage.SetCookie(cookie);

                    MODEL.UserInfo userInfo= BLL.User_wsref.GetAndSaveInfoToSession(userid, loginResult);
                    if (userInfo != null)
                    {
                        Response.Redirect("~/Pages/chooseEmployment.aspx?pid=" + userInfo.personid+"&sourcetype=1");
                    }
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
            //this.lt_user.Text = baseLanguage.login_user;
            //this.lt_password.Text = baseLanguage.login_password;
            this.Button1.Text= baseLanguage.login_loginbtn;
            //this.lt_remember2.Text = baseLanguage.login_remember;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string userid = this.tb_u1.Text.Trim();
            string password = this.tb_p1.Text.Trim();

            ProgressLogin(userid, password);
        }

        private void CleanInput()
        {
            this.tb_u1.Text = "";
            this.tb_p1.Text = "";
        }

        //protected void cb_remember_CheckedChanged(object sender, EventArgs e)
        //{
        //    //if (!this.cb_remember.Checked)
        //    //{
        //        var myc= BLL.Page.MyCookieManage.GetCookie();
        //        myc.isRemember = "0";
        //        BLL.Page.MyCookieManage.SetCookie(myc);
        //    //}
        //    //else
        //    //{
        //    //    var myc = BLL.Page.MyCookieManage.GetCookie();
        //    //    myc.isRemember = "1";
        //    //    BLL.Page.MyCookieManage.SetCookie(myc);
        //    //}
        //}

        protected void lb_eng_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;

            LSLibrary.WebAPP.LanguageType selectlang = (LSLibrary.WebAPP.LanguageType)int.Parse(lb.CommandArgument);

            var myc = BLL.Page.MyCookieManage.GetCookie();
            myc.language = selectlang;
            BLL.Page.MyCookieManage.SetCookie(myc);

            LSLibrary.WebAPP.BaseLanguage baseLanguage = BLL.MultiLanguageHelper.GetLanguagePacket(selectlang);
            LoadLableLanguage(baseLanguage);
            DisplayLanguage(selectlang);
        }
    }
}