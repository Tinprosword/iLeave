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
    }

    public class MyCookieManage
    {
        public static readonly string COOKIE_SERVERADDRESS = "cookie_serveraddress";
        private static readonly string COOKIE_LANGUAGE = "LANGUAGE";
        private static readonly string COOKIE_IsRemember = "IsRemember";
        private static readonly string COOKIE_loginname = "COOKIE_loginname";
        private static readonly string COOKIE_pass = "COOKIE_pass";
        private static readonly string COOKIE_ISAPP = "COOKIE_ISAPP";

        public static MyCookie GetCookie()
        {
            //get each value .if contain null . init cookie  other return value
            string cookieValue = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_LANGUAGE);
            string address = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_SERVERADDRESS);
            string isremember = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_IsRemember);
            string name = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_loginname);
            string pass = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_pass);
            string isapp = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_ISAPP);


            MyCookie mycookie = new MyCookie();
            if (cookieValue == null || address == null || isremember == null || name == null || pass == null || isapp == null)
            {
                mycookie.language = LSLibrary.WebAPP.LanguageType.english;
                mycookie.serverAddress = "";
                mycookie.isRemember = "0";
                mycookie.loginname = "";
                mycookie.loginpsw = "";
                mycookie.isAppLogin = "0";
            }
            else
            {
                mycookie.language = (LSLibrary.WebAPP.LanguageType)int.Parse(cookieValue);
                mycookie.serverAddress = address;
                mycookie.isRemember = isremember;
                mycookie.loginname = name;
                mycookie.loginpsw = pass;
                mycookie.isAppLogin = isapp;
            }

            SetCookie(mycookie);

            return mycookie;
        }

        public static void SetCookie(MyCookie myCookie)
        {
            SetCookie_address(myCookie.serverAddress);
            SetCookie_language(myCookie.language);
            SetCookie_isRmember(myCookie.isRemember);
            SetCookie_name(myCookie.loginname);
            SetCookie_psw(myCookie.loginpsw);
            SetCookie_isapp(myCookie.isAppLogin);
        }

        private static void SetCookie_language(LSLibrary.WebAPP.LanguageType language)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_LANGUAGE, ((int)language).ToString(), 360);
        }

        private static void SetCookie_address(string address)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_SERVERADDRESS, address, 360);
        }

        private static void SetCookie_isRmember(string isrem)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_IsRemember, isrem, 360);
        }

        private static void SetCookie_name(string name)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_loginname, name, 360);
        }
        private static void SetCookie_psw(string paw)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_pass, paw, 360);
        }
        private static void SetCookie_isapp(string isapp)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_ISAPP, isapp, 360);
        }
    }
}