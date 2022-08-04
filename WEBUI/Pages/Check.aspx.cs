using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.UI.HtmlControls;

namespace WEBUI.Pages
{
    public partial class Check:BLL.CustomLoginTemplate
    {
        //页面不需要任何成员变量，唯一的user info 变量，由session提供。
        //页面就2个按钮事件，和一个展示数据方法。 并且按钮事件后都需要调用。所以展示数据方法必须复用。

        private static string ms_onclickCheckname = "Check";

        #region pageevent
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {}


        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            lb_msg.Visible = false;
            lb_msg2.Visible = false;
            this.lt_jsmobileGps.Text = "";
            this.lt_jsModelWindow.Text = "";
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().main_check, "~/pages/main.aspx", true);

            SetupmultipleLanguage();

            this.lb_day.Text = System.DateTime.Today.ToString("yyyy-MM-dd");
            this.lb_time.Text = BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose());

            var rpdate = BLL.calendar.GetRoster(System.DateTime.Today, new List<int> { loginer.userInfo.employID ?? 0 }).OrderBy(x => x.Time);
            this.rp_shifts.DataSource = rpdate;
            this.rp_shifts.DataBind();
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            this.lb_time.Text = BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose());
            WEBUI.Controls.leave master = (WEBUI.Controls.leave)this.Master;
            var targetName= master.GetMyPostTargetname();
            ProcessMyPostbackEvent(targetName);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            int a = 4;
        }

        #endregion


        #region postback event

        protected void OnClick_In(object sender, EventArgs e)
        {
            bool hasMultiShift = this.rp_shifts.Items.Count >= 2;
            if (hasMultiShift)
            {
                this.lt_jsModelWindow.Text = "<script>$('#modal_shifts').modal();</script>";
            }
            else
            {
                ProgressCheckIn();
            }
        }

        protected void btn_model2_ok_Click(object sender, EventArgs e)
        {
            ProgressCheckIn();
        }

        #endregion


        #region private function
        public string GenerateCheckedString(int index)
        {
           return index == 0 ? "1" : "0";
        }


        private string GetShiftcodeFromRepeater()
        {
            string result = "";

            foreach (RepeaterItem item in this.rp_shifts.Items)
            {
                HtmlInputRadioButton economic = (HtmlInputRadioButton)item.FindControl("rd_shift");
                if (economic.Checked)
                {
                    result = economic.Value.ToString();
                }
            }
            return result;
        }



        private void ProcessMyPostbackEvent(string targetname)
        {
            WEBUI.Controls.leave master = (WEBUI.Controls.leave)this.Master;

            string zoneCode = GetShiftcodeFromRepeater();
            if (string.IsNullOrEmpty(zoneCode))
            {
                zoneCode = "01";
            }


            var iguard = BLL.Other.GetIleavIGard();
            int interfaceid = 0;
            int centerfaceid = 0;
            string deviceid = "";

            if (iguard != null)
            {
                interfaceid = iguard.InterfaceID ?? 0;
                centerfaceid = iguard.AttendanceInterfaceCenterID ?? 0;
                deviceid = iguard.DeviceID;
            }

            if (targetname== ms_onclickCheckname)
            {
                var lastItem = BLL.Other.GetAttendanceList(new string[] { loginer.userInfo.staffNumber }).OrderByDescending(x => x.CreateDate).FirstOrDefault();

                var value = master.GetMyPostBackArgumentByTargetname(targetname);
                if(string.IsNullOrEmpty(value))// click on pc
                {
                    var tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "IN", loginer.userInfo.staffNumber, centerfaceid, interfaceid, 1, loginer.userInfo.surname, deviceid, zoneCode, "", "","","");
                    BLL.Other.InsertAttendanceRawData(new WebServiceLayer.WebReference_leave.AttendanceRawData[] { tempModer });
                }
                else//click on mobile.
                {
                    string lata = "";
                    string longa = "";

                    double lat = 0;
                    double lon = 0;
                    string locationname = "";
                    string macAddress = "";

                    string[] valueArray = value.Split(new char[] { '|' }, StringSplitOptions.None);
                    if(valueArray!=null && valueArray.Count()>=2)
                    {
                        lata = valueArray[0];
                        longa = valueArray[1];

                        if(valueArray.Count()>=3)
                        {
                            macAddress = valueArray[2];
                        }
                    }


                    
                    if (!string.IsNullOrEmpty(lata) && !string.IsNullOrEmpty(longa) && double.TryParse(lata,out lat) && double.TryParse(longa,out lon))
                    {
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
                    }

                    string strlocation = "";
                    if (lat!=0 && lon!=0)
                    {
                        strlocation = lat.ToString() + "|" + lon.ToString();
                    }

                    var tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "IN", loginer.userInfo.staffNumber, centerfaceid, interfaceid, 1, loginer.userInfo.surname, "000", zoneCode, strlocation, locationname, macAddress, "");
                    BLL.Other.InsertAttendanceRawData(new WebServiceLayer.WebReference_leave.AttendanceRawData[] { tempModer });
                }


                this.lb_msg.Visible = true;
                this.lb_msg.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Commoncheckin + " : " + BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose());
                this.lb_msg2.Visible = true;

                if (lastItem != null)
                {
                    this.lb_msg2.Text = BLL.MultiLanguageHelper.GetLanguagePacket().CommonLastcheckin + " : " + BLL.common.GetFormatTime2(BLL.MultiLanguageHelper.GetChoose(),lastItem.CreateDate);
                }
            }
        }


        private static string GetLocationUrl(LSLibrary.WebAPP.LanguageType _cul, double lat, double lon)
        {
            string result = "";
            result = "http://dev.virtualearth.net/REST/v1/Locations/" + lat + "," + lon + "?o=json&key=AqviYV7wGGW6_Bx2Y1RIb_-w4eqXlS_GsgYTVuA_KYVMmUpnhfq3CvtpOjM9R6JQ&output=json";
            string culcode = "en-US";
            if (_cul == LSLibrary.WebAPP.LanguageType.sc)
            {
                culcode = "zh-Hans";
            }
            else if (_cul == LSLibrary.WebAPP.LanguageType.tc)
            {
                culcode = "zh-Hant";
            }
            result += "&c=" + culcode;
            return result;
        }

        private void SetupmultipleLanguage()
        {
            this.bt_checkin.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Commoncheckin;
        }

        private void ProgressCheckIn()
        {
            string js = GetMPDJS("GPS", "0");
            if (js != "")
            {
                this.lt_jsmobileGps.Text = js;
            }
            else
            {
                this.lt_jsModelWindow.Text = "<script>MyPostBack('"+ ms_onclickCheckname + "','')</script>";
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

        #endregion

        protected void rp_shifts_Load(object sender, EventArgs e)
        {
            int a = 4;
        }
    }
}