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
    }

    public class MyCookieManage
    {
        public static readonly string  COOKIE_SERVERADDRESS = "cookie_serveraddress";
        private static readonly string COOKIE_LANGUAGE = "LANGUAGE";
        private static readonly string COOKIE_IsRemember = "IsRemember";

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

            string isremember= LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_IsRemember);
            isremember= string.IsNullOrWhiteSpace(isremember) ? "" : isremember;

            MyCookie result = new MyCookie();
            result.language = languageType;
            result.serverAddress = address;
            result.isRemember = isremember;

            return result;
        }

        public static void SetCookie(MyCookie myCookie)
        {
            SetCookie_address(myCookie.serverAddress);
            SetCookie_language(myCookie.language);
            SetCookie_isRmember(myCookie.isRemember);
        }

        public static void SetCookie_language(LSLibrary.WebAPP.LanguageType language)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_LANGUAGE, ((int)language).ToString(), 360);
        }

        public static void SetCookie_address(string address)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_SERVERADDRESS, address, 360);
        }

        public static void SetCookie_isRmember(string isrem)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_IsRemember, isrem, 360);
        }
    }
}