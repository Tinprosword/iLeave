using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MultiLanguageHelper
    {
        public static LSLibrary.WebAPP.LanguageType GetChoose()
        {
            return (LSLibrary.WebAPP.LanguageType)Page.MyCookieManage.GetCookie().language;
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