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
            string comDeployCode = "";
            var loginer= BLL.User_wsref.GetLoginer();
            if (loginer != null)
            {
                 comDeployCode = loginer.userInfo.companyDeployCode;
            }
            return LSLibrary.WebAPP.MulitiLanguageFactory.GetLanguagePacket(GetChoose(),comDeployCode);
        }
        public static LSLibrary.WebAPP.BaseLanguage GetLanguagePacket(LSLibrary.WebAPP.LanguageType type)
        {
            string comDeployCode = "";
            var loginer = BLL.User_wsref.GetLoginer();
            if (loginer != null)
            {
                comDeployCode = loginer.userInfo.companyDeployCode;
            }
            return LSLibrary.WebAPP.MulitiLanguageFactory.GetLanguagePacket(type,comDeployCode);
        }

        public static string GetLanguageByEn(string en)
        {
            string result = en;

            LSLibrary.WebAPP.LanguageType ltype = GetChoose();
            result = LSLibrary.WebAPP.FixMulLanguage.GetLanguageByEn(en, ltype);

            return result;
        }
    }
}