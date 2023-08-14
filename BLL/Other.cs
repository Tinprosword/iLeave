using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebServiceLayer;

namespace BLL
{
    public class Other
    {
        #region anncount
        public static int GetAnouncementFirstYear(int firsteid)
        {
            int result = System.DateTime.Now.Year - 1;

            var tempResult = GetAnouncementByFEID(firsteid);

            if (tempResult != null && tempResult.Count > 0)
            {
                tempResult = tempResult.OrderBy(x => x.CreateDate).ToList();
                result=tempResult[0].CreateDate.Year;
            }

            return result;
        }

        public static List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement> GetAnouncementByFEIDType(int firsteid,MODEL.Announcement.enum_Announce_tabs type,int year)
        {
            List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement> result = new List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement>();
            var tempresult = GetAnouncementByFEID(firsteid);
            if (tempresult != null && tempresult.Count()>0)
            {
                tempresult = tempresult.Where(x => x.TypeID == (int)type
                && int.Parse(System.DateTime.Now.ToString("yyyyMMdd")) >= int.Parse(x.ValidDateFrom.ToString("yyyyMMdd"))
                && int.Parse(System.DateTime.Now.ToString("yyyyMMdd")) <= int.Parse(x.ValidDateTo.ToString("yyyyMMdd")) 
                && x.Status == 1).OrderBy(x => x.SortSeq).ToList();
                if (year != -1)
                {
                    tempresult = tempresult.Where(x => x.CreateDate.Year == year).ToList();
                }
                result = tempresult;
            }
            return result;
        }


        private static List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement> GetAnouncementByFEID(int firsteid)
        {
            return MyWebService.GlobalWebServices.ws_Ileave_Other.Announce_GetAnnouncementByFirstEid(firsteid).ToList();
        }
        #endregion


        public static string GetVersion()
        {
            return "1.6.0";
        }

        public static void UpdateCookieAfterLoginByIsRemeber(bool isremember,string _loginUserName,string _password)
        {
            if (isremember)
            {
                var cookie = BLL.Page.MyCookieManage.GetCookie();
                cookie.isRemember = "1";
                cookie.loginname = _loginUserName;
                cookie.loginpsw = _password;
                BLL.Page.MyCookieManage.SetCookie(cookie);
            }
            else
            {
                var cookie = BLL.Page.MyCookieManage.GetCookie();
                cookie.isRemember = "0";
                cookie.loginname = "";
                cookie.loginpsw = "";
                BLL.Page.MyCookieManage.SetCookie(cookie);
            }
        }

        public static WebServiceLayer.WebReference_leave.AttendanceRawData[] GetAttendanceList(string[] refInfo)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetAttendanceByExternRef(refInfo);
        }


        public static WebServiceLayer.WebReference_leave.ILeaveIGuard GetIleavIGard()
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetIGuard("ileave");
        }

        public static DateTime GetEstimateDate(int eid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetDefaultYearEndDate(eid);
        }


        public static bool InsertAttendanceRawData(WebServiceLayer.WebReference_leave.AttendanceRawData[] data)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.InsertAttendance(data);
        }

        public static WebServiceLayer.WebReference_leave.PositionInfo[] GetPositions()
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetPosition();
        }

        public static WebServiceLayer.WebReference_leave.v_System_iLeave_Security[] GetSecurity(bool all, int sid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetRosterInquiry_Security(all, sid);
        }

        public static WebServiceLayer.WebReference_leave.v_System_iLeave_Leave_List[] GetRoster_leavelist(string name, string[] zoneCode, string[] positionCode, System.DateTime datefrom, System.DateTime dateto)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetRosterInquiry_leave(name, zoneCode, positionCode, datefrom, dateto);
        }

        public static WebServiceLayer.WebReference_leave.v_System_iLeave_Roster_List[] GetRoster_Rosterlist(string name, string[] zoneCode, string[] positionCode, System.DateTime datefrom, System.DateTime dateto)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetRosterInquiry_Roster(name, zoneCode, positionCode, datefrom, dateto);
        }

        public static WebServiceLayer.WebReference_leave.UserInfo GetUser(int pid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetUserinfo(pid);
        }

        public static void GetPaylistBaseInfo(WebServiceLayer.WebReference_leave.v_System_iLeave_Payslip[] data,out string company,out int cid,out List<LSLibrary.WebAPP.ValueText<int>> date)
        {
            company = "";
            cid = 0;
            date = new List<LSLibrary.WebAPP.ValueText<int>>();

            if (data != null && data.Count() > 0)
            {
                company = data[0].CompanyName;
                cid = data[0].CompanyID;

                var strDates = data.Select(x => x.PayrollTrailMonth).ToArray();
                if (strDates != null && strDates.Count() > 0)
                {
                    int tempDate = 0;
                    string tempStrDate = "";
                    foreach (string item in strDates)
                    {
                        if (int.TryParse(item, out tempDate))
                        {
                            if (tempDate.ToString().Length == 6)
                            {
                                tempStrDate = tempDate.ToString().Substring(0, 4) + "-" + tempDate.ToString().Substring(4, 2);
                                date.Add(new LSLibrary.WebAPP.ValueText<int>(tempDate, tempStrDate));
                            }
                        }
                    }
                }
            }
            date = date.OrderByDescending(x => x.mvalue).ToList();
        }



        public static void GetTaxationBaseInfo(WebServiceLayer.WebReference_leave.v_System_iLeave_Taxtion[] data, out string company, out int cid, out List<LSLibrary.WebAPP.ValueText<int>> date)
        {
            company = "";
            cid = 0;
            date = new List<LSLibrary.WebAPP.ValueText<int>>();

            if (data != null && data.Count() > 0)
            {
                company = data[0].Name;
                cid = data[0].CompanyID;

                var strDates = data.Select(x => new { value = x.TaxYear ,text=x.Year_Range }).ToArray();
                if (strDates != null && strDates.Count() > 0)
                {
                    int tempDate = 0;
                    foreach (var item in strDates)
                    {
                        if (int.TryParse(item.value, out tempDate))
                        {
                            if (tempDate.ToString().Length == 4)
                            {
                                date.Add(new LSLibrary.WebAPP.ValueText<int>(tempDate, item.text));
                            }
                        }
                    }
                }
            }
            date = date.OrderByDescending(x => x.mvalue).ToList();
        }


        public static WebServiceLayer.WebReference_leave.v_System_iLeave_Payslip[] GetPayslipBysid(int sid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetPayslipBySid(sid);
        }

        public static WebServiceLayer.WebReference_leave.v_System_iLeave_Taxtion[] GetTaxationBysid(int sid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetTaxationBySid(sid);
        }

        public static WebServiceLayer.WebReference_leave.PaySlipReportObject GetPayslipReportData(int staffid,int year,int month,int uid_operater)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.LoadPaySlipReport(staffid, uid_operater, year, month);
        }

        public static WebServiceLayer.WebReference_leave.ReportCommonData GetTextationReportData(int year,int eid,int uid_operater,bool replace)
        {
            return  WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.LoadTaxationReport(year,eid,uid_operater,replace);
        }

        public static WebServiceLayer.WebReference_leave.AttendanceRawData GenerateModel(DateTime logdate,int uid,string _Type,string _ExternalRef,int _AttendanceInterfaceCenterID,
            int _InterfaceID,int? _RemoteIdent,string _StaffName,string _DeviceID,string _Zone,string _GpsLocation,string _GpsLocationName,string wifiadd,string wifiname)
        {
            WebServiceLayer.WebReference_leave.AttendanceRawData result = new WebServiceLayer.WebReference_leave.AttendanceRawData();
            result.LogDateTime = logdate;
            result.Type = _Type;
            result.ExternalRef = _ExternalRef;
            result.InterfaceID = _InterfaceID;
            result.AttendanceInterfaceCenterID = _AttendanceInterfaceCenterID;
            result.RemoteIdent = _RemoteIdent;
            result.StaffName = _StaffName;
            result.DeviceID = _DeviceID;
            result.Zone = _Zone;
            result.CreateDate = DateTime.Now;
            result.CreateUser = uid;
            result.GpsLocation = _GpsLocation;
            result.GpsLocationName = _GpsLocationName;
            result.WifiAddress = wifiadd;
            result.WifiInfo = wifiname;
            result.LeaveDocumentPath = "";
            result.LeaveDocument2Path = "";
            return result;
        }


        public static string GetHRWebSiteRootUrl()
        {
            string result = "";

            if (System.Web.HttpContext.Current==null ||  System.Web.HttpContext.Current.Request == null)
            {
                return "";
            }

            string sysUrl= BLL.CodeSetting.GetSystemParameter(BLL.CodeSetting.SystemParameter_baseurl);
            if (string.IsNullOrEmpty(sysUrl))
            {
                string theUrl = LSLibrary.IISHelper.GetWebSiteBaseUrlWithFolderFlag(System.Web.HttpContext.Current);
                //去掉  2个//和中间的字符。

                theUrl=theUrl.Remove(theUrl.Length - 1);
                int p_indexaa = theUrl.LastIndexOf("/");

                result = theUrl.Substring(0, p_indexaa) + "/UI/";
            }
            else
            {
                result = sysUrl;
            }
            return result;
        }


        public static string ExeStropFun(int functionid, bool isbefore, string[] para)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.ExecuteFunctionStoreProc(functionid, isbefore, para);
        }

        //删除临时文件夹中。创建8小时后的文件.
        public static void DeleteOlderFiles(System.Web.HttpServerUtility server)
        {
            try
            {
                string floderPath = server.MapPath("../tempdownload");
                LSLibrary.FileUtil.DeleteFilesWhenCreateTimeBefore(floderPath, 8);
            }
            catch { }
        }

        public static bool GetEnableForceCheckif()
        {
            return false;
        }
    }

}