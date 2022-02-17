using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace WEBUI.Pages
{
    public partial class Check:BLL.CustomLoginTemplate
    {
        //页面不需要任何成员变量，唯一的user info 变量，由session提供。
        //页面就2个按钮事件，和一个展示数据方法。 并且按钮事件后都需要调用。所以展示数据方法必须复用。
        #region pageevent
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {}


        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            lb_msg.Visible = false;
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().main_check, "~/pages/main.aspx", true);
            OnMobileLoadUrl();

            SetupmultipleLanguage();

            this.lb_day.Text = System.DateTime.Today.ToString("yyyy-MM-dd");
            this.lb_time.Text = BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose());

            //ShowDateOnLable(System.DateTime.Now);
            //ShowInout(loginer.userInfo.eNoRefFirstEid, GetCurrentLableDate());
        }

        private static string GetLocationUrl(LSLibrary.WebAPP.LanguageType _cul,double lat,double lon)
        {
            string result = "";
            result= "http://dev.virtualearth.net/REST/v1/Locations/" + lat + "," + lon + "?o=json&key=AqviYV7wGGW6_Bx2Y1RIb_-w4eqXlS_GsgYTVuA_KYVMmUpnhfq3CvtpOjM9R6JQ&output=json";
            string culcode = "en-US";
            if (_cul == LSLibrary.WebAPP.LanguageType.sc)
            {
                culcode = "zh-Hans";
            }
            else if(_cul == LSLibrary.WebAPP.LanguageType.tc)
            {
                culcode = "zh-Hant";
            }
            result += "&c=" + culcode;
            return result;
        }

        private void OnMobileLoadUrl()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["action"]))
            {
                string action = Request.QueryString["action"];
                if (action == "mobile")
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["lat"]) && !string.IsNullOrEmpty(Request.QueryString["long"]) && !string.IsNullOrEmpty(Request.QueryString["inout"]))
                    {
                        double lat = double.Parse(Request.QueryString["lat"]);
                        double lon = double.Parse(Request.QueryString["long"]);
                        int inout = int.Parse(Request.QueryString["inout"]);

                        string locationname = "";
                        try
                        {
                            //string mapurl = "http://api.map.baidu.com/geocoder?output=json&coord_type=wgs84&location=" + lat + "," + lon + "&key=OGbSHmIkHo2qLybXmSG2mr8pZ4uypIok";
                            string mapurlBingo = GetLocationUrl(BLL.MultiLanguageHelper.GetChoose(), lat, lon);

                            System.Net.WebClient webClient = new WebClient();
                            webClient.Encoding = System.Text.Encoding.UTF8;
                            string jsonstr = webClient.DownloadString(mapurlBingo);
                            System.Xml.XmlDocument xmlDoc = LSLibrary.MyJson.UnSxml(jsonstr);

                            var addlist = xmlDoc.GetElementsByTagName("addressLine");
                            var locallist = xmlDoc.GetElementsByTagName("locality");
                            if (locallist != null && locallist.Count > 0)
                            {
                                locationname = locallist[0].InnerText;
                            }

                            if (addlist != null && addlist.Count > 0)
                            {
                                locationname = addlist[0].InnerText + " " + locationname;
                            }
                        }
                        catch
                        {
                            locationname = "";
                        }

                        if (inout == 0)
                        {
                            var tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "IN", loginer.userInfo.employNnumber, 22, 2, 1, loginer.userInfo.surname, "000", "01", locationname, locationname);
                            BLL.Other.InsertAttendanceRawData(new WebServiceLayer.WebReference_leave.AttendanceRawData[] { tempModer });

                            this.lb_msg.Visible = true;
                            this.lb_msg.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Commoncheckin + " : " + BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose());
                        }
                        else
                        {
                            var tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "OUT", loginer.userInfo.employNnumber, 22, 2, 1, loginer.userInfo.surname, "000", "01", locationname, locationname);
                            BLL.Other.InsertAttendanceRawData(new WebServiceLayer.WebReference_leave.AttendanceRawData[] { tempModer });

                            this.lb_msg.Visible = true;
                            this.lb_msg.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Commoncheckin + " : " + BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose());
                        }
                    }


                }
            }
        }

        private void SetupmultipleLanguage()
        {
            this.bt_checkin.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Commoncheckin;
        }


        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            this.lb_time.Text = BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose());
        }
        #endregion

        protected void OnClick_In(object sender, EventArgs e)
        {
            // insert record. show list
            string js = GetMPDJS("GPS", "0");
            if (js != "")
            {
                Response.Clear();
                Response.Write(js);
                Response.End();
            }
            else
            {
                var iguard = BLL.Other.GetIleavIGard();
                if (iguard != null)
                {
                    int interfaceid = iguard.InterfaceID ?? 0;
                    int centerfaceid = iguard.AttendanceInterfaceCenterID ?? 0;
                    string deviceid = iguard.DeviceID;

                    var tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "IN", loginer.userInfo.employNnumber,centerfaceid,interfaceid, 1, loginer.userInfo.surname,deviceid, "01", "", "");
                    BLL.Other.InsertAttendanceRawData(new WebServiceLayer.WebReference_leave.AttendanceRawData[] { tempModer });

                    this.lb_msg.Visible = true;
                    this.lb_msg.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Commoncheckin + " : " + BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose());
                }
            }

        }




        //由mobil 返回位置信息,並組成url ，頁面根據url 參數。處理數據。
        private static string GetMPDJS(string msgtype, string msgbody)
        {
            string agent = HttpContext.Current.Request.UserAgent;
            LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.MobilWebHelper.GetClientType(agent);

            var cookies = BLL.Page.MyCookieManage.GetCookie();

            string result = "";

            if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.android && cookies.isAppLogin == "1")//android
            {
                result = LSLibrary.WebAPP.MyJSHelper.SendMessageToAndroid(msgtype, msgbody, HttpContext.Current.Server);
            }
            else if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone && cookies.isAppLogin == "1")//ios
            {
                result = LSLibrary.WebAPP.MyJSHelper.SendMessageToIphone(msgtype, msgbody, HttpContext.Current.Server);
            }
            return result;
        }


        public static string SpecialLanguage(string inouttype)
        {
            string result = "IN";
            if (inouttype == "IN")
            {
                result = BLL.MultiLanguageHelper.GetLanguagePacket().check_in;
            }
            else if (inouttype == "OUT")
            {
                result = BLL.MultiLanguageHelper.GetLanguagePacket().check_out;
            }

            return result;
        }
    }
}