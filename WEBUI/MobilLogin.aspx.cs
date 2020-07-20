﻿using System;
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


                LSLibrary.WebAPP.CookieHelper.SetCookie(BLL.GlobalVariate.COOKIE_SERVERADDRESS, address, 3600);
                LSLibrary.WebAPP.CookieHelper.SetCookie(BLL.GlobalVariate.COOKIE_HTTPS, bhttps.ToString(), 3600);
                BLL.MultiLanguageHelper.SaveChoose((LSLibrary.WebAPP.LanguageType)intLanguage);

                MODEL.UserInfo userInfo = new MODEL.UserInfo(int.Parse(id), uid, "", "", sessionID);
                LSLibrary.WebAPP.LoginManager.SetLoginer(new LSLibrary.WebAPP.LoginUser<MODEL.UserInfo>(uid, userInfo));
                Response.Redirect("~/Pages/Main.aspx");
            }
        }


    }
}