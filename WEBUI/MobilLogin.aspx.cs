using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class MobilLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.Form["Type"];
            if(action=="login")
            {
                string loginID = Request.Form["Uid"];//login id
                string id = Request.Form["id"];
                int intID = int.Parse(id);
                string language = Request.Form["language"];
                int intLanguage = 0;
                int.TryParse(language, out intLanguage);
                string https= Request.Form["https"];
                bool bhttps = https == "1" ? true : false;
                string address = Request.Form["server"];
                string sessionID = Request.Form["sessionid"];
                string password = Request.Form["password"];
                string isremember = Request.Form["isremember"];

                BLL.Page.MyCookie myCookie = new BLL.Page.MyCookie();
                myCookie.language = (LSLibrary.WebAPP.LanguageType)intLanguage;
                myCookie.serverAddress = address;
                myCookie.isRemember = isremember;
                myCookie.loginname = loginID;
                myCookie.loginpsw = password;

                BLL.Page.MyCookieManage.SetCookie(myCookie);
                

                WebServiceLayer.WebReference_user.LoginResult loginResult = BLL.User_wsref.CheckLogin(loginID, password);//这个登陆是完全没有必要的.但是为了免去手写sessionid.就再登陆一次.让系统(web services)自动写sessionid.如果后期了解正确写sessionid的方法 ,可以去掉.
                if (loginResult.Result > 0)
                {
                    MODEL.UserInfo userInfo = BLL.User_wsref.GetAndSaveInfoToSession(loginID, loginResult);
                    if (userInfo != null)
                    {
                        Response.Redirect("~/Pages/chooseEmployment.aspx?pid=" + userInfo.personid + "&sourcetype=1");
                    }
                }
                else
                {
                    BLL.User_wsref.GoBackToLogin();
                }
            }
        }
    }
}