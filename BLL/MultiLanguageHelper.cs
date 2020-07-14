using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MultiLanguageHelper
    {
        private static readonly string COOKIE_LANGUAGE = "LANGUAGE";
        public static LSLibrary.WebAPP.LanguageType GetChoose()
        {
            LSLibrary.WebAPP.LanguageType type = LSLibrary.WebAPP.LanguageType.english;
            string cookieValue = LSLibrary.WebAPP.CookieHelper.GetCookie(COOKIE_LANGUAGE);
            if (string.IsNullOrWhiteSpace(cookieValue) == false)
            {
                try
                {
                    type = (LSLibrary.WebAPP.LanguageType)(int.Parse(cookieValue));
                }
                catch(Exception ex) {
                    throw ex;
                }
            }
            return type;
        }


        public static void SaveChoose(LSLibrary.WebAPP.LanguageType type)
        {
            int strvalue = (int)type;
            LSLibrary.WebAPP.CookieHelper.SetCookie(COOKIE_LANGUAGE, strvalue.ToString(), 30);
        }

        public static LSLibrary.WebAPP.BaseLanguage GetLanguagePacket()
        {
            return LSLibrary.WebAPP.MulitiLanguageFactory.GetLanguagePacket(GetChoose());
        }
        public static LSLibrary.WebAPP.BaseLanguage GetLanguagePacket(LSLibrary.WebAPP.LanguageType type)
        {
            return LSLibrary.WebAPP.MulitiLanguageFactory.GetLanguagePacket(type);
        }
    }
}