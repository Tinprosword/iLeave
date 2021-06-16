﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CodeSetting
    {
        public static string staffNameFormat = "LEAVE_CALENDAR_STAFF_NAME_FORMAT_iLeave";

        public static List<int> GetSections(int position, int leave)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetLeaveSections(leave, position).ToList();
        }

        public static string GetSystemParameter(string name)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetSystemParameter(name);
        }

        public static int GetNameType(LSLibrary.WebAPP.LanguageType language)
        {
            if (language == LSLibrary.WebAPP.LanguageType.english)
            {
                return 1;
            }
            else
            {
                return 6;
            }
        }

    }
}