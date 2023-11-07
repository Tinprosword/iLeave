﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.UI.HtmlControls;

namespace WEBUI.Pages
{
    public partial class Check : BLL.CustomLoginTemplate
    {
        //页面不需要任何成员变量，唯一的user info 变量，由session提供。
        //页面就2个按钮事件，和一个展示数据方法。 并且按钮事件后都需要调用。所以展示数据方法必须复用。

        private static string ViewState_PageName = "ViewState_PageNameaaa";

        public enum enum_checkActionCode
        {
            nowifi_nolatlon_nogpslocation=0,
            nowifi_latlon_nogpslocation = 1,
            nowifi_latlon_gpslocation = 2,
            wifi_nolatlon_nogpslocation = 3,
            wifi_latlon_nogpslocation = 4,
            wifi_latlon_gpslocation = 5,
            NA = 6,
        }

        #region pageevent
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        { }

        protected override void InitPage_OnNotFirstLoad2()
        { }

        protected override void PageLoad_InitUIOnNotFirstLoad4()
        { }

        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            lb_msg_current.Visible = false;
            lb_msg2_pre.Visible = false;
            this.lt_jsModelWindow.Text = "";
            this.lb_commonmsg.Visible = false;
            this.lb_commonmsg.Text = "";
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, new MODEL.Check.ViewState_page(), this.ViewState);

            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().main_check, "~/pages/main.aspx", true);

            SetupmultipleLanguage();

            bool isApp = BLL.SystemEnviroment.isFromMobilApp(Request, BLL.Page.MyCookieManage.GetCookie());

            this.lb_day.Text = System.DateTime.Today.ToString("yyyy-MM-dd");
            this.lb_time.Text = BLL.common.GetFormatTime_currentTime(BLL.MultiLanguageHelper.GetChoose());

            var rpdate = BLL.calendar.GetRoster(System.DateTime.Today, new List<int> { loginer.userInfo.employID ?? 0 }).OrderBy(x => x.Time);
            this.rp_shifts.DataSource = rpdate;
            this.rp_shifts.DataBind();

            panel_appmsg.Visible = isApp;

            ShowCurrentCheckTime(false, null);
            ShowLastTimeCheckTime(false);

            if (isApp)
            {
                string js = GetMPDJS("GPS", "0");
                if (!string.IsNullOrEmpty(js))
                {
                    //可能是 app 的JS 调用会破坏 页面的JS，所以必须 放入到单独的文件中。
                    this.lt_jsTimerRequestMobileLocation.Text = "<script src=\"../Res/App/check_timerLocation.js\"></script>";
                }
            }
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            this.lb_time.Text = BLL.common.GetFormatTime_currentTime(BLL.MultiLanguageHelper.GetChoose());
            //WEBUI.Controls.leave master = (WEBUI.Controls.leave)this.Master;
            //var targetName = master.GetMyPostTargetname();
            //ProcessMyPostbackEvent(targetName);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            int a = 4;
        }

        #endregion


        #region postback event

        protected void OnClick_In(object sender, EventArgs e)
        {
            this.bt_checkin.Enabled = false;//避免多次打卡
            bool hasMultiShift = this.rp_shifts.Items.Count >= 2;
            if (hasMultiShift)
            {
                this.lt_jsModelWindow.Text = "<script>$('#modal_shifts').modal();</script>";
            }
            else
            {
                ProgressCheckIn_PreData(sender, e);
            }
        }

        protected void btn_model2_ok_Click(object sender, EventArgs e)
        {
            ProgressCheckIn_PreData(sender, e);
        }

        private void ProgressCheckIn_PreData(object sender, EventArgs e)
        {

            bool isApp = false;

            var souceCode= BLL.SystemEnviroment.GetClientSource(Request, BLL.Page.MyCookieManage.GetCookie());
            isApp = BLL.SystemEnviroment.isFromMobilApp(souceCode);

            if (isApp)
            {
                //获得控件数据。
                //保存到数据库。
                string wifiname = this.hf_back_wifiname.Value;
                string latlon = this.hf_back_gpslatlon.Value;
                string gpslocationName = this.hf_back_gpsDecode.Value;

                bool isInvalidData = this.hf_back_invaliddata.Value == "" ? false : true;

                bool hasgps = false;
                string lata = "";
                string longa = "";
                double lat = 0;
                double lon = 0;
                string[] valueArray = latlon.Split(new char[] { '|' }, StringSplitOptions.None);
                if (valueArray != null && valueArray.Count() == 2)
                {
                    lata = valueArray[0];
                    longa = valueArray[1];
                    hasgps = double.TryParse(lata, out lat) && double.TryParse(longa, out lon);
                }

                bool haswifi = wifiname == "" ? false : true;
                bool decodeGps = gpslocationName == "" ? false : true;

                
                enum_checkActionCode theActionCode = enum_checkActionCode.nowifi_nolatlon_nogpslocation;
                if (!isInvalidData)
                {
                    this.lb_commonmsg.Visible = true;
                    this.lb_commonmsg.Text = BLL.MultiLanguageHelper.GetLanguagePacket().common_msg_InvalidData;
                }
                else
                {
                    if (haswifi == false && hasgps == false)
                    {
                        theActionCode = enum_checkActionCode.nowifi_nolatlon_nogpslocation;
                    }
                    else if (haswifi == false && hasgps == true)
                    {
                        if (decodeGps)
                        {
                            theActionCode = enum_checkActionCode.nowifi_latlon_gpslocation;
                        }
                        else
                        {
                            theActionCode = enum_checkActionCode.nowifi_latlon_nogpslocation;
                        }
                    }
                    else if (haswifi == true && hasgps == false)
                    {
                        theActionCode = enum_checkActionCode.wifi_nolatlon_nogpslocation;
                    }
                    else//has wifi and gps
                    {
                        if (decodeGps)
                        {
                            theActionCode = enum_checkActionCode.wifi_latlon_gpslocation;
                        }
                        else
                        {
                            theActionCode = enum_checkActionCode.wifi_latlon_nogpslocation;
                        }
                    }
                    ProgressCheckIn(true, theActionCode, wifiname, lat, lon, gpslocationName);
                }
            }
            else
            {
                ProgressCheckIn(false, enum_checkActionCode.NA, null, 0, 0, null);
            }
        }

        private void ProgressCheckIn(bool isapp, enum_checkActionCode actionCode, string wifiname, double lat, double lon, string gpsLocationName)
        {
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
            if (isapp)
            {
                deviceid = "000";
            }


            WebServiceLayer.WebReference_leave.AttendanceRawData tempModer = null;

            if (!isapp)// click on pc or browser
            {
                tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "IN", loginer.userInfo.staffNumber, centerfaceid, interfaceid, 1, loginer.userInfo.surname, deviceid, zoneCode, "", "", "", "");
            }
            else//click on app
            {
                string strlocation = "";
                if (actionCode == enum_checkActionCode.nowifi_latlon_gpslocation || actionCode == enum_checkActionCode.nowifi_latlon_nogpslocation || actionCode == enum_checkActionCode.wifi_latlon_gpslocation || actionCode == enum_checkActionCode.wifi_latlon_nogpslocation)
                {
                    strlocation = lat.ToString() + "|" + lon.ToString();
                }
                tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "IN", loginer.userInfo.staffNumber, centerfaceid, interfaceid, 1, loginer.userInfo.surname, deviceid, zoneCode, strlocation, gpsLocationName, wifiname, "");
            }

            if (tempModer != null)
            {
                BLL.Other.InsertAttendanceRawData(new WebServiceLayer.WebReference_leave.AttendanceRawData[] { tempModer });
                ShowCurrentCheckTime(true, System.DateTime.Now);
                ShowLastTimeCheckTime(true);
            }
            //enalbe btn again
            this.bt_checkin.Enabled = true;
        }

        //load->empty.  btn->now.
        private void ShowCurrentCheckTime(bool isClickCheck,DateTime? date)
        {
            if (isClickCheck && date!=null)
            {
                string lb_msg_msg = BLL.MultiLanguageHelper.GetLanguagePacket().Commoncheckin + " : " + BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose(), date.Value);

                this.lb_msg_current.Visible = true;
                this.lb_msg_current.Text = lb_msg_msg;
            }
            else
            {
                this.lb_msg_current.Visible = false;
                this.lb_msg_current.Text = "";
            }
        }

        //load->first btn->second
        private void ShowLastTimeCheckTime(bool isClickCheck)
        {
            var allitem = BLL.Other.GetAttendanceList(new string[] { loginer.userInfo.staffNumber }).OrderByDescending(x => x.CreateDate).ToList();
            WebServiceLayer.WebReference_leave.AttendanceRawData lastItem = null;
            if (isClickCheck)
            {
                if (allitem != null && allitem.Count() >= 2)
                {
                    lastItem = allitem[1];
                }
            }
            else
            {
                if (allitem != null && allitem.Count() >= 1)
                {
                    lastItem = allitem[0];
                }
            }
            if (lastItem != null)
            {
                this.lb_msg2_pre.Visible = true;
                this.lb_msg2_pre.Text = BLL.MultiLanguageHelper.GetLanguagePacket().CommonLastcheckin + " : " + BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose(), lastItem.CreateDate);
            }
        }

        #region storeProcedure to check .not use
        //if (!isMobile)
        //{

        //}
        //else
        //{
        //    decimal lat = 0; decimal lng = 0;
        //    WebServiceLayer.MyModel.AttendanceRawData mymodel_rawdate = new WebServiceLayer.MyModel.AttendanceRawData(tempModer);
        //    mymodel_rawdate.getlatlng(out lat, out lng);

        //    var lng_ileave = BLL.MultiLanguageHelper.GetChoose();
        //    string checkMsg = BLL.Checkin.CheckCheckin(loginer.userInfo.id, loginer.userInfo.employID ?? 0, tempModer.WifiAddress, lat, lng, tempModer.LogDateTime, BLL.MultiLanguageHelper.Convertohrfromileave(lng_ileave).ToString());
        //    if (!string.IsNullOrEmpty(checkMsg.Trim()))
        //    {
        //        lb_msg_msg = checkMsg;
        //    }
        //}
        #endregion


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

        #region old function
        //private void ProcessMyPostbackEvent(string actionName,string actionValue)
        //{

        //    if (actionName == m_CheckinActionName || actionName==m_ForceChekinActonName || actionName==m_CheckinActionErrorName)
        //    {

        //        string zoneCode = GetShiftcodeFromRepeater();
        //        if (string.IsNullOrEmpty(zoneCode))
        //        {
        //            zoneCode = "01";
        //        }


        //        var iguard = BLL.Other.GetIleavIGard();
        //        int interfaceid = 0;
        //        int centerfaceid = 0;
        //        string deviceid = "";

        //        if (iguard != null)
        //        {
        //            interfaceid = iguard.InterfaceID ?? 0;
        //            centerfaceid = iguard.AttendanceInterfaceCenterID ?? 0;
        //            deviceid = iguard.DeviceID;
        //        }


        //        bool isMobile = false;
        //        bool isMobileValidCheckin = false;

        //        var lastItem = BLL.Other.GetAttendanceList(new string[] { loginer.userInfo.staffNumber }).OrderByDescending(x => x.CreateDate).FirstOrDefault();

        //        isMobile = string.IsNullOrEmpty(actionValue) == true ? false : true;

        //        WebServiceLayer.WebReference_leave.AttendanceRawData tempModer = null;

        //        if (!isMobile)// click on pc
        //        {
        //            tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "IN", loginer.userInfo.staffNumber, centerfaceid, interfaceid, 1, loginer.userInfo.surname, deviceid, zoneCode, "", "", "", "");
        //        }
        //        else//click on mobile.
        //        {
        //            string lata = "";
        //            string longa = "";

        //            double lat = 0;
        //            double lon = 0;
        //            string locationname = "";
        //            string macAddress = "";

        //            string[] valueArray = actionValue.Split(new char[] { '|' }, StringSplitOptions.None);
        //            if(valueArray!=null && valueArray.Count()>=2)
        //            {
        //                lata = valueArray[0];
        //                longa = valueArray[1];

        //                if(valueArray.Count()>=3)
        //                {
        //                    macAddress = valueArray[2];
        //                }
        //            }



        //            if(!string.IsNullOrEmpty(lata) && !string.IsNullOrEmpty(longa) && double.TryParse(lata,out lat) && double.TryParse(longa,out lon))
        //            {
        //                try
        //                {
        //                    string url_map = "";
        //                    if (mMapAPI == 0)
        //                    {
        //                        url_map = BLL.Checkin.GetLocationUrl_bingo(LSLibrary.WebAPP.LanguageType.tc, lat, lon);
        //                        locationname = BLL.Checkin.GetAddFromUrl_Bingo(url_map);
        //                    }
        //                    else if (mMapAPI == 1)//主動防禦
        //                    {
        //                        url_map = BLL.Checkin.GetLocationUrl_Google(lat, lon, mGoolgeKey, LSLibrary.WebAPP.LanguageType.tc);
        //                        locationname = BLL.Checkin.GetAddFromUrl_Google(url_map);
        //                    }
        //                    else//被動防禦
        //                    {
        //                        url_map = BLL.Checkin.GetLocationUrl_Google(lat, lon, mGoolgeKey, LSLibrary.WebAPP.LanguageType.tc);
        //                        locationname = BLL.Checkin.GetAddFromUrl_Google(url_map);
        //                    }
        //                }
        //                catch
        //                {
        //                    locationname = "";
        //                }
        //            }

        //            string strlocation = "";
        //            if (lat!=0 && lon!=0)
        //            {
        //                strlocation = lat.ToString() + "|" + lon.ToString();
        //            }

        //            isMobileValidCheckin = isValaidCheckin();
        //            var viewstateaa = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Check.ViewState_page>(ViewState_PageName, this.ViewState);
        //            if (isMobileValidCheckin)
        //            {
        //                tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "IN", loginer.userInfo.staffNumber, centerfaceid, interfaceid, 1, loginer.userInfo.surname, "000", zoneCode, strlocation, locationname, macAddress, "");
        //            }
        //            else
        //            {
        //                tempModer = null;
        //            }

        //        }

        //        if (tempModer!=null)
        //        {
        //            BLL.Other.InsertAttendanceRawData(new WebServiceLayer.WebReference_leave.AttendanceRawData[] { tempModer });

        //            //1.if mobile ,need get checkmsg and replace raw message.
        //            string lb_msg_msg = BLL.MultiLanguageHelper.GetLanguagePacket().Commoncheckin + " : " + BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose());
        //            if (!isMobile)
        //            {

        //            }
        //            else
        //            {
        //                decimal lat = 0; decimal lng = 0;
        //                WebServiceLayer.MyModel.AttendanceRawData mymodel_rawdate = new WebServiceLayer.MyModel.AttendanceRawData(tempModer);
        //                mymodel_rawdate.getlatlng(out lat, out lng);

        //                var lng_ileave = BLL.MultiLanguageHelper.GetChoose();
        //                string checkMsg = BLL.Checkin.CheckCheckin(loginer.userInfo.id, loginer.userInfo.employID ?? 0, tempModer.WifiAddress, lat, lng, tempModer.LogDateTime, BLL.MultiLanguageHelper.Convertohrfromileave(lng_ileave).ToString());
        //                if (!string.IsNullOrEmpty(checkMsg.Trim()))
        //                {
        //                    lb_msg_msg = checkMsg;
        //                }
        //            }
        //            this.lb_msg.Visible = true;
        //            this.lb_msg.Text = lb_msg_msg;



        //            if (lastItem != null)
        //            {
        //                this.lb_msg2.Visible = true;
        //                this.lb_msg2.Text = BLL.MultiLanguageHelper.GetLanguagePacket().CommonLastcheckin + " : " + BLL.common.GetFormatTime2(BLL.MultiLanguageHelper.GetChoose(), lastItem.CreateDate);
        //            }


        //        }
        //    }

        //    //enalbe btn again
        //    this.bt_checkin.Enabled = true;
        //}

        //always insert .so the result is true always now.
        #endregion

        private bool isValaidCheckin()
        {
            return true;
        }

        private void SetupmultipleLanguage()
        {
            this.bt_checkin.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Commoncheckin;

            this.hf_nowifi.Value = BLL.MultiLanguageHelper.GetLanguagePacket().check_nowifi;
            this.hf_nogps.Value = BLL.MultiLanguageHelper.GetLanguagePacket().check_nogps;
            this.hf_cantconvertGps.Value = BLL.MultiLanguageHelper.GetLanguagePacket().check_cantdeocegps;

            this.lb_locationname.Text= BLL.MultiLanguageHelper.GetLanguagePacket().check_wainting;
            this.lb_wifi.Text = BLL.MultiLanguageHelper.GetLanguagePacket().check_wainting;
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
                result = LSLibrary.WebAPP.MyJSHelper.SendMessageToAndroid(msgtype, msgbody, HttpContext.Current.Server,false);
            }
            else if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone && cookies.isAppLogin == "1")//ios
            {
                result = LSLibrary.WebAPP.MyJSHelper.SendMessageToIphone(msgtype, msgbody, HttpContext.Current.Server,false);
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