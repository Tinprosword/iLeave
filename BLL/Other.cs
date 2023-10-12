using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebServiceLayer;

namespace BLL
{
    public class Announcement
    {
        //public class PushInfo
        //{
        //    public int AnnouncementID;
        //    public List<string> DevicesIDs;

        //    public PushInfo(int announcementID, List<string> devicesIDs)
        //    {
        //        AnnouncementID = announcementID;
        //        DevicesIDs = devicesIDs;
        //    }
        //}

        #region push notice
        public static List<int> GetAndroidLocalPush(string username)
        {
            //1.isLocal 2.get need push announceid 3.is me ,4 not push
            List<int> result = new List<int>();
            bool isgooglePush = android_googlePushNotice();
            if (!isgooglePush)
            {
                result = GetOneUnPushID();
                if (result != null && result.Count() > 0)
                {
                    var myPushed = MyWebService.GlobalWebServices.ws_Ileave_Other.GetPushedAnnounceIDs_AndroidLocal(username);
                    result = result.Where(x => myPushed.Contains(x.ToString()) == false).ToList();

                    if (result != null && result.Count() > 0)
                    {
                        var myAnnounce = MyWebService.GlobalWebServices.ws_Ileave_Other.Announce_GetAnnouncementByUsername(username).ToList();
                        var myAnnounce_id = myAnnounce.Select(x => x.ID).ToList();

                        result = result.Where(x => myAnnounce_id.Contains(x)).ToList();
                    }
                }
            }
            return result;
        }



        public static void PushNotice(HttpServerUtility theServer)
        {
            List<int> announceIDs = GetOneUnPushID();

            WebServiceLayer.MyWebService.GlobalWebServices.ws_Ileave_Other.Notice_ClearInvalid();

            foreach (int announceID in announceIDs)
            {
                List<string> deviceids_ios = GetUnPushAllDeviceIDs_ios(announceID);
                List<string> deviceids_android = GetUnPushAllDeviceIDs_android(announceID);

                var announce = GetAnouncementByID(announceID);
                string announce_title = announce.Subject;


                foreach (string thedeviceid in deviceids_ios)
                {
                    if (thedeviceid.Contains("error:") == false)//跳过错误的deviceids
                    {
                        try
                        {
                            pushIOSNotice(announce_title, thedeviceid, theServer);
                            SetPushed(announceID, WebServiceLayer.WebReference_Ileave_Other.enum_CommonKeyValueTypeCode.Push_Already_ios, thedeviceid);
                        }
                        catch (Exception exxx)
                        {
                            BLL.common.WriteLog(exxx.Message);
                        }
                    }
                }
                bool isServerTimeOut = false;

                if (!isServerTimeOut)
                {
                    foreach (string thedeviceid in deviceids_android)
                    {
                        if (thedeviceid.Contains("error:") == false)//跳过错误的deviceids
                        {
                            try
                            {
                                int result = pushAndroidNotice(announce_title, thedeviceid);
                                if (result == 1)//成功就插入记录。
                                {
                                    SetPushed(announceID, WebServiceLayer.WebReference_Ileave_Other.enum_CommonKeyValueTypeCode.Push_Already_android, thedeviceid);
                                }
                                else if (result == -1)//timeout 超时的话，下面的也不做。
                                {
                                    break;
                                }
                                else//其他错误，跳过，做下一个。
                                {
                                    continue;
                                }
                            }
                            catch (Exception exxx)
                            {
                                BLL.common.WriteLog(exxx.Message);
                            }
                        }
                    }
                }
            }
        }

        public static void pushIOSNotice(string title, string deviceid, HttpServerUtility theServer)
        {
            //todo push call python
            //C:\Users\Administrator\source\repos\WebIleave\WEBUI\pythonPro\python-3.8.2rc2-embed-amd64\python.exe C:\Users\Administrator\source\repos\WebIleave\WEBUI\pythonPro\apns\anps.py "a b c" 05eb62ba07a11d74f322066ed37c3a21926a47e8b3fd2d6b9deb2090cde723e0 C:\Users\Administrator\source\repos\WebIleave\WEBUI\pythonPro\apns\ileave_apns_develop.p8
            string pythonPath = "";
            string pythonFunctionFilename = "";

            pythonPath = theServer.MapPath("~/pythonpro/") + "python-3.8.2rc2-embed-amd64\\python.exe";
            pythonFunctionFilename= theServer.MapPath("~/pythonpro/") + "apns\\anps.py";
            string p8path= theServer.MapPath("~/pythonpro/") + "apns\\ileave_apns_develop.p8";


            title = safePythnParatemter(title);
            LSLibrary.pythenHelper.RunPythonScript_iosPush(title, deviceid, pythonPath, pythonFunctionFilename, p8path);
        }

        private static string safePythnParatemter(string par)
        {
            return par;
        }

        //-1.error_timeout.  0.error_other    1.success
        public static int pushAndroidNotice(string msgContent, string deviceid)
        {
            int result = 1;
            string sendResult= LSLibrary.HttpWebRequestHelper.HttpPost_Josn("https://fcm.googleapis.com/fcm/send", "AAAAvJufqGw:APA91bHN5s-7tbRL4VrrSmo_HGycigWZwvqf7z5xo_Ee8k-GFcJ4JOD-QtZ-efPpu-PUevGON1KzvzmockvyitjK_1zuR3G6fl1XdPct7U3cAUrYf78xV0Sb9gkzPb7LJL3N6qRE-Va-", "810064783468", deviceid,msgContent,3000);

            
            if (sendResult.ToUpper().Contains("TIMEOUT"))
            {
                result = -1;
            }
            else if (sendResult.ToUpper().Contains("ERROR"))
            {
                result = 0;
            }
            else//todo  0 这里会有其他失败，现在全当作成功，那么就会导致有时候失败发送，当成成功。不会再次发送。
            {
                result = 1;
            }
            return result;
        }

        public static List<int> GetOneUnPushID()
        {
            List<int> result = new List<int>();
            result = MyWebService.GlobalWebServices.ws_Ileave_Other.GetOneUnPushID().ToList();
            return result;
        }

        //!null &==1 false
        public static bool android_googlePushNotice()
        {
            bool result = true;
            var tempResult= LSLibrary.WebAPP.WebConfig.getValue("androidLocalPush");
            if (!string.IsNullOrEmpty(tempResult) && tempResult=="1")
            {
                result = false;
            }
            return result;
        }

        
        public static List<string> GetUnPushAllDeviceIDs_ios(int anncountid)
        {
            List<string> result = new List<string>();
            result = MyWebService.GlobalWebServices.ws_Ileave_Other.GetUnPushAllDeviceIDs_ios(anncountid).ToList();
            return result;
        }

        public static List<string> GetUnPushAllDeviceIDs_android(int anncountid)
        {
            List<string> result = new List<string>();
            result = MyWebService.GlobalWebServices.ws_Ileave_Other.GetUnPushAllDeviceIDs_Android(anncountid).ToList();
            return result;
        }

        public static void SetPushed(int announceid, WebServiceLayer.WebReference_Ileave_Other.enum_CommonKeyValueTypeCode androidOrIos,string deviceid)
        {
            MyWebService.GlobalWebServices.ws_Ileave_Other.SetPushed(announceid, androidOrIos, deviceid);
        }


        #endregion

        public static void DeviceID_InsertOrUpdateDeviceID(int iosOrAndroid ,string deviceid,string username)
        {
            MyWebService.GlobalWebServices.ws_Ileave_Other.DeviceID_InsertOrUpdateDeviceID(iosOrAndroid, deviceid, username);
        }

        public static string GetFileName(string filePath)
        {
            return MyWebService.GlobalWebServices.ws_Ileave_Other.Attachment_GetFileName(filePath);
        }

        public static byte[] GetByteByAttachmentid(int aid)
        {
            return MyWebService.GlobalWebServices.ws_Ileave_Other.Attachment_GetAttachmentByte(aid);
        }

        public static int GetAnouncementFirstYear(int firsteid)
        {
            int result = System.DateTime.Now.Year - 1;

            var tempResult = GetAnouncementByFEID(firsteid);

            if (tempResult != null && tempResult.Count > 0)
            {
                tempResult = tempResult.OrderBy(x => x.CreateDate).ToList();
                result = tempResult[0].CreateDate.Year;
            }

            return result;
        }



        public static List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement> GetAnouncementByFEIDType(int firsteid, MODEL.Announcement.enum_Announce_tabs type, int year)
        {
            List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement> result = new List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement>();
            var tempresult = GetAnouncementByFEID(firsteid);
            if (tempresult != null && tempresult.Count() > 0)
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


        public static List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement> GetAnouncementByFEID(int firsteid)
        {
            return MyWebService.GlobalWebServices.ws_Ileave_Other.Announce_GetAnnouncementByFirstEid(firsteid).ToList();
        }

        public static WebServiceLayer.WebReference_Ileave_Other.t_Announcement GetAnouncementByID(int aid)
        {
            return MyWebService.GlobalWebServices.ws_Ileave_Other.Announce_GetAnnouncementByAnncounceID(aid);
        }

        public static List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement> Announce_GetUnReadAnnouncement(int firstEID)
        {
            return MyWebService.GlobalWebServices.ws_Ileave_Other.Announce_GetUnReadAnnouncement(firstEID).ToList();
        }

        public static void Announce_ReadAnncount(int announceID, int firsteid)
        {
            MyWebService.GlobalWebServices.ws_Ileave_Other.Announce_ReadAnnounce(firsteid, announceID);
        }
    }

    public class Other
    {
        #region anncount

        

        
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

    public class Attachment
    {
        public static List<WebServiceLayer.WebReference_Ileave_Other.t_Attachment> GetAttachementByAnnounceID(int announceid)
        {
            return MyWebService.GlobalWebServices.ws_Ileave_Other.Announce_GetAttachementByAnnounceID(announceid).ToList();
        }

        public static WebServiceLayer.WebReference_Ileave_Other.t_Attachment GetAttachementByattID(int aid)
        {
            return MyWebService.GlobalWebServices.ws_Ileave_Other.GetAttachementByattID(aid);
        }

        public static string Attachment_GetFileName(string filePath)
        {
            return MyWebService.GlobalWebServices.ws_Ileave_Other.Attachment_GetFileName(filePath);
        }
    }

    public class WorkflowPN
    {
        public static List<int> InsertPN_ApplyLeave(int requestid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_Ileave_workflowPN.InsertPN_ApplyLeave(requestid).ToList();
        }
    }
}