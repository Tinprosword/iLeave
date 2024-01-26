
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class CodeSetting
    {
        public static string SystemParameter_showleaveCode = "HIDE_LEAVECODE_IN_LEAVE_CALENDAR";
        public static string SystemParameter_baseurl = "ILEAVE_HRBASEURL";
        public static string SystemParameter_ILEAVE_APPNAME = "ILEAVE_APPNAME";

        public static WebServiceLayer.WebReference_codesettings.LeaveInfo[] GetAllLeaveInfo()
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetAllLeaveInfo();
        }

        public static List<WebServiceLayer.WebReference_Ileave_Other.t_Contract_Zone> CodeSetting_GetAllZone()
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_Ileave_Other.CodeSetting_GetAllZone().ToList();
        }

        public static List<string> GetCompanyDeployCode()
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetCompanyCode().ToList();
        }

        public static List<int> GetSections(int position, int leave)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetLeaveSections(leave, position).ToList();
        }

        public static string GetSystemParameter(string name)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetSystemParameter(name);
        }

        public static bool AllowHourly(int leaveid, int position)
        {
            bool result = false;

            List<int> sections = BLL.CodeSetting.GetSections(position, leaveid);
            if (sections != null && sections.Count() > 0)
            {
                if (sections.Contains(4))
                {
                    result= true;
                }
            }


            return result;
                
        }


        /// <summary>
        /// 1 eng 6or other ch.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
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

        public static string GetMenu()
        {
            string result = "";
            result = WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetSystemParameter("ILEAVEHIDDENMENUS");
            return result;
        }

        public static WebServiceLayer.WebReference_codesettings.Shift GetShiftbyid(int id)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetShiftInfoByID(id);
        }

        public static double GetRealTotal(DateTime startDate1, DateTime endDate1, DateTime startDate2, DateTime endDate2)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetRealTotal(startDate1, endDate1, startDate2, endDate2);
        }


        public static WebServiceLayer.WebReference_codesettings.Shift GetShiftbyEid(int eid)
        {
            WebServiceLayer.WebReference_codesettings.Shift result = null;

            var einfo = BLL.User_wsref.getEmploymentByid(eid);
            if (einfo != null)
            {
                var shift = BLL.CodeSetting.GetShiftbyid(einfo.ShiftID);
                if (shift != null)
                {
                    result = shift;
                }
            }

            return result;
        }


        public static double GetFulldayWorkHours(int eid)
        {
            double result = 8;

            var theShift = BLL.CodeSetting.GetShiftbyEid(eid);
            if (theShift != null)
            {
                result = theShift.TotalWorkHour;
            }
            return result;
        }


    }
}