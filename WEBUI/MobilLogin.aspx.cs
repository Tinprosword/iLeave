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
                string uid = Request.Form["Uid"];
                string id = Request.Form["id"];
                string language = Request.Form["language"];
                int intLanguage = 0;
                int.TryParse(language, out intLanguage);
                string https= Request.Form["https"];
                bool bhttps = https == "1" ? true : false;
                string address = Request.Form["server"];
                string sessionID = Request.Form["sessionid"];
                string password = Request.Form["password"];


                LSLibrary.WebAPP.CookieHelper.SetCookie(BLL.GlobalVariate.COOKIE_SERVERADDRESS, address, 3600);
                LSLibrary.WebAPP.CookieHelper.SetCookie(BLL.GlobalVariate.COOKIE_HTTPS, bhttps.ToString(), 3600);
                BLL.MultiLanguageHelper.SaveChoose((LSLibrary.WebAPP.LanguageType)intLanguage);

                DAL.WebReference_User.PersonBaseinfo personBaseinfo = BLL.User_wsref.GetPersonBaseinfos_validateDefaultEmploymentNow(int.Parse(id));
                MODEL.UserInfo userInfo;
                if (personBaseinfo != null)
                {
                    userInfo = new MODEL.UserInfo(int.Parse(id), uid, "", sessionID, personBaseinfo.e_id, personBaseinfo.e_EmploymentNumber, personBaseinfo.s_id, personBaseinfo.s_StaffNumber);
                }
                else
                {
                    userInfo = new MODEL.UserInfo(int.Parse(id), uid, "", sessionID, null, null, null, null);
                }

                LSLibrary.WebAPP.LoginManager.SetLoginer(new LSLibrary.WebAPP.LoginUser<MODEL.UserInfo>(uid, userInfo));

                MODEL.LoginResult loginResult= BLL.User_wsref.CheckLogin(uid, password);//这个登陆是完全没有必要的.但是为了免去手写sessionid.就再登陆一次.让系统自动写sessionid.如果后期了解正确写sessionid的方法 ,可以去掉.
                if (loginResult.Result > 0)
                {
                    Response.Redirect("~/Pages/Main.aspx");
                }
                else
                {
                    BLL.User_wsref.GoBackToLogin();
                }
            }
        }


    }
}