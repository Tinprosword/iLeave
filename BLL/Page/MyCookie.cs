using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Page
{
    public class MyCookie
    {
        public string serverAddress;
        public LSLibrary.WebAPP.LanguageType language;
        public string isRemember;
        public string loginname;
        public string loginpsw;
        public string isAppLogin;
        public string wait_scrollTop;
        public string LocalPCzodeCode;
    }

    public class MyCookieManage
    {
        public static readonly string COOKIE_SERVERADDRESS = "cookie_serveraddress";
        private static readonly string COOKIE_LANGUAGE = "LANGUAGE";
        private static readonly string COOKIE_IsRemember = "IsRemember";
        private static readonly string COOKIE_loginname = "COOKIE_loginname";
        private static readonly string COOKIE_pass = "COOKIE_pass";
        private static readonly string COOKIE_ISAPP = "COOKIE_ISAPP";
        private static readonly string COOKIE_WST = "COOKIE_WST";
        private static readonly string COOKIE_LocalPCzodeCode = "COOKIE_LocalPCzodeCode";



        public static MyCookie GetCookie()
        {
            //get each value .if contain null . init cookie  other return value
            string cookieValue = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_LANGUAGE);
            string address = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_SERVERADDRESS);
            string isremember = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_IsRemember);
            string name = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_loginname);
            string pass = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_pass);
            string isapp = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_ISAPP);
            string wst = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_WST);
            string localzone= LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_LocalPCzodeCode);


            MyCookie mycookie = new MyCookie();
            if (cookieValue == null || address == null || isremember == null || name == null || pass == null || isapp == null)
            {
                mycookie.language = LSLibrary.WebAPP.LanguageType.english;
                mycookie.serverAddress = "";
                mycookie.isRemember = "0";
                mycookie.loginname = "";
                mycookie.loginpsw = "";
                mycookie.isAppLogin = "0";
                mycookie.wait_scrollTop = "0|0|0|0";
                mycookie.LocalPCzodeCode = "0";
            }
            else
            {
                mycookie.language = (LSLibrary.WebAPP.LanguageType)int.Parse(cookieValue);
                mycookie.serverAddress = address;
                mycookie.isRemember = isremember;
                mycookie.loginname = name;
                mycookie.loginpsw = pass;
                mycookie.isAppLogin = isapp;
                mycookie.wait_scrollTop = wst;
                mycookie.LocalPCzodeCode = localzone;
            }

           // SetCookie(mycookie);

            return mycookie;
        }


        public static void SetCookie(MyCookie myCookie)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_LANGUAGE, ((int)myCookie.language).ToString(), 360);
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_SERVERADDRESS, myCookie.serverAddress, 360);
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_IsRemember, myCookie.isRemember, 360);
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_loginname, myCookie.loginname, 360);
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_pass, myCookie.loginpsw, 360);
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_ISAPP, myCookie.isAppLogin, 360);
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_WST, myCookie.wait_scrollTop, 360);
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_LocalPCzodeCode, myCookie.LocalPCzodeCode, 360);
        }


    }
}