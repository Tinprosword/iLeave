using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Other
    {
        public static WebServiceLayer.WebReference_leave.AttendanceRawData[] GetAttendanceList(string[] refInfo)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetAttendanceByExternRef(refInfo);
        }


        public static WebServiceLayer.WebReference_leave.ILeaveIGuard GetIleavIGard()
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetIGuard("ileave");
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

        public static WebServiceLayer.WebReference_leave.ReportCommonData GetTextationReportData(int year,int eid,int uid_operater)
        {
            return  WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.LoadTaxationReport(year,eid,uid_operater);
        }

        public static WebServiceLayer.WebReference_leave.AttendanceRawData GenerateModel(DateTime logdate,int uid,string _Type,string _ExternalRef,int _AttendanceInterfaceCenterID,
            int _InterfaceID,int? _RemoteIdent,string _StaffName,string _DeviceID,string _Zone,string _GpsLocation,string _GpsLocationName)
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


            return result;
        }


        public static string GetWebSiteRootUrl(System.Web.HttpRequest Request)
        {
            string result = "";
            string sysUrl= BLL.CodeSetting.GetSystemParameter(BLL.CodeSetting.SystemParameter_baseurl);
            if (string.IsNullOrEmpty(sysUrl))
            {
                string theUrl = Request.Url.ToString();

                string[] ileavesitename = { "ILEAVE", "DW-ILEAVE","TEMP", "LEAVE", "IHR-ILEAVE", "HR-ILEAVE" };
                foreach (string sitename in ileavesitename)
                {
                    int p_intStart = theUrl.ToUpper().IndexOf("/"+sitename+"/");
                    if (p_intStart > -1)
                    {
                        result = theUrl.Substring(0, p_intStart) + "/UI/";
                        break;
                    }
                }
            }
            else
            {
                result = sysUrl;
            }
            return result;
        }


    }

}