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
    }

    public class MyCookieManage
    {
        public static readonly string COOKIE_SERVERADDRESS = "cookie_serveraddress";
        private static readonly string COOKIE_LANGUAGE = "LANGUAGE";

        public static MyCookie GetCookie()
        {
            LSLibrary.WebAPP.LanguageType languageType = LSLibrary.WebAPP.LanguageType.english;
            string cookieValue = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_LANGUAGE);
            if (string.IsNullOrWhiteSpace(cookieValue) == false)
            {
                try
                {
                    languageType = (LSLibrary.WebAPP.LanguageType)(int.Parse(cookieValue));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            string address = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_SERVERADDRESS);
            address = string.IsNullOrWhiteSpace(address) ? "" : address;

            MyCookie result = new MyCookie();
            result.language = languageType;
            result.serverAddress = address;

            return result;
        }

        public static void SetCookie(MyCookie myCookie)
        {
            SetCookie_address(myCookie.serverAddress);
            SetCookie_language(myCookie.language);
        }

        public static void SetCookie_language(LSLibrary.WebAPP.LanguageType language)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_LANGUAGE, ((int)language).ToString(), 360);
        }

        public static void SetCookie_address(string address)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_SERVERADDRESS, address, 360);
        }
    }
}