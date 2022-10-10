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
    public partial class Check : BLL.CustomLoginTemplate
    {
        //页面不需要任何成员变量，唯一的user info 变量，由session提供。
        //页面就2个按钮事件，和一个展示数据方法。 并且按钮事件后都需要调用。所以展示数据方法必须复用。

        //todo 0  !!! !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //發現ViewStateHelper.SetValue 要放到 PageLoad_InitUIOnFirstLoad4。否則會失效. 但是apply.aspx又可以， 
        //沒有找出原因。看了下流程，是因為page事件並沒有按照自己想像中的走，好想是因為變量引用問題，導致.net 需要這些變量，在前一個page 事件還沒走完，丟先調用了後一個頁面事件。
        //看來自己寫的page 框架，有問題。！！！！！有空要仔細測試下為什麼。

        private static string m_CheckinActionName = "Check";
        private static string m_ForceChekinActonName = "ForceCheck";
        private static string ViewState_PageName = "ViewState_PageNameaaa";
        private static string mGoolgeKey = "AIzaSyBct1Ksb5gAqLQMZREgskseovJW6RVYTWs";
        private static int mMapAPI = 1;//0:binggo 1.google


        #region pageevent
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        { }


        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            lb_msg.Visible = false;
            lb_msg2.Visible = false;
            this.lt_jsmobileGps.Text = "";
            this.lt_jsModelWindow.Text = "";
            this.lt_jsConfirmForce.Text = "";
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, new MODEL.Check.ViewState_page(), this.ViewState);

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
            var targetName = master.GetMyPostTargetname();
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
            if (targetname == m_ForceChekinActonName)
            {
                var tempView = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Check.ViewState_page>(ViewState_PageName, this.ViewState);
                tempView.mIsForceCheckin = true;
                LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, tempView, this.ViewState);
            }

            if (targetname == m_CheckinActionName || targetname==m_ForceChekinActonName)
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


                bool isMobile = false;
                bool isMobileValidCheckin = false;

                var lastItem = BLL.Other.GetAttendanceList(new string[] { loginer.userInfo.staffNumber }).OrderByDescending(x => x.CreateDate).FirstOrDefault();

                var value = master.GetMyPostBackArgumentByTargetname(targetname);

                isMobile = string.IsNullOrEmpty(value) == true ? false : true;

                WebServiceLayer.WebReference_leave.AttendanceRawData tempModer = null;

                if (!isMobile)// click on pc
                {
                    tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "IN", loginer.userInfo.staffNumber, centerfaceid, interfaceid, 1, loginer.userInfo.surname, deviceid, zoneCode, "", "", "", "");
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


                    
                    if(!string.IsNullOrEmpty(lata) && !string.IsNullOrEmpty(longa) && double.TryParse(lata,out lat) && double.TryParse(longa,out lon))
                    {
                        try
                        {
                            string url_map = "";
                            if (mMapAPI == 0)
                            {
                                url_map = BLL.Checkin.GetLocationUrl_bingo(LSLibrary.WebAPP.LanguageType.tc, lat, lon);
                                locationname = BLL.Checkin.GetAddFromUrl_Bingo(url_map);
                            }
                            else if (mMapAPI == 1)//主動防禦
                            {
                                url_map = BLL.Checkin.GetLocationUrl_Google(lat, lon, mGoolgeKey, LSLibrary.WebAPP.LanguageType.tc);
                                locationname = BLL.Checkin.GetAddFromUrl_Google(url_map);
                            }
                            else//被動防禦
                            {
                                url_map = BLL.Checkin.GetLocationUrl_Google(lat, lon, mGoolgeKey, LSLibrary.WebAPP.LanguageType.tc);
                                locationname = BLL.Checkin.GetAddFromUrl_Google(url_map);
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

                    isMobileValidCheckin = isValaidCheckin();
                    var viewstateaa = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Check.ViewState_page>(ViewState_PageName, this.ViewState);
                    if (isMobileValidCheckin || viewstateaa.mIsForceCheckin)
                    {
                        tempModer = BLL.Other.GenerateModel(System.DateTime.Now, loginer.userInfo.id, "IN", loginer.userInfo.staffNumber, centerfaceid, interfaceid, 1, loginer.userInfo.surname, "000", zoneCode, strlocation, locationname, macAddress, "");
                        if (viewstateaa.mIsForceCheckin)//強制打卡值能使用一次。
                        {
                            viewstateaa.mIsForceCheckin = false;
                            LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, viewstateaa, this.ViewState);
                        }
                    }
                    else
                    {
                        tempModer = null;
                        if (BLL.Other.GetEnableForceCheckif())
                        {
                            string js = "<script>forceCheckint('{0}','{1}','{2}')</script>";
                            js = string.Format(js, "Force check in?", m_ForceChekinActonName, value);
                            this.lt_jsConfirmForce.Text = js;
                        }
                    }
                }

                if (tempModer!=null)
                {
                    BLL.Other.InsertAttendanceRawData(new WebServiceLayer.WebReference_leave.AttendanceRawData[] { tempModer });

                    this.lb_msg.Visible = true;
                    this.lb_msg.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Commoncheckin + " : " + BLL.common.GetFormatTime(BLL.MultiLanguageHelper.GetChoose());
                    this.lb_msg2.Visible = true;

                    if (lastItem != null)
                    {
                        this.lb_msg2.Text = BLL.MultiLanguageHelper.GetLanguagePacket().CommonLastcheckin + " : " + BLL.common.GetFormatTime2(BLL.MultiLanguageHelper.GetChoose(), lastItem.CreateDate);
                    }
                }
            }

            //enalbe btn again
            this.bt_checkin.Enabled = true;
        }

        private bool isValaidCheckin()
        {
            //todo 0 check invaid checkin
            return true;
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
                //disnable btn first for double click
                this.bt_checkin.Enabled = false;
            }
            else
            {
                this.lt_jsModelWindow.Text = LSLibrary.WebAPP.MyJSHelper.CustomPost(m_CheckinActionName, "");// "<script>MyPostBack('" + m_CheckinActionName + "','')</script>";
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