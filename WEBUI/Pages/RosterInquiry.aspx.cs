using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class RosterInquiry : BLL.CustomLoginTemplate
    {
        private int actionType = 0;

        private WebServiceLayer.WebReference_leave.v_System_iLeave_Security[] mV_System_ILeave_Securities = new WebServiceLayer.WebReference_leave.v_System_iLeave_Security[0];
        private WebServiceLayer.WebReference_leave.UserInfo mLoginerOtherInfo = null;

        #region page event
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            string strActionType = Request.QueryString["action"];
            if (!string.IsNullOrEmpty(strActionType))
            {
                int.TryParse(strActionType, out actionType);
            }
            else
            {
                Response.Clear();
                Response.Write(LSLibrary.WebAPP.MyJSHelper.AlertMessageAndGoto("Invalid url parameter!,", "main.aspx"));
                Response.End();
            }

            mV_System_ILeave_Securities = BLL.Other.GetSecurity(true,0);
            mLoginerOtherInfo = BLL.Other.GetUser(loginer.userInfo.personid);
        }

        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            MulLanguage();
            LoadUI();
        }

        
        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
        }

        #endregion


        private void MulLanguage()
        {
            this.lt_listname.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_name;
            this.lt_listdate.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_mmdd;
            this.lt_listshift.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_shift;
            this.lt_listattend.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_attend;
            this.lt_listRemark.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_Remark;


            this.lt_list_name2.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_name;
            this.lt_listdate2.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_mmdd;
            this.lt_listshift2.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_shift;
            this.lt_listattend2.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_attend;
            this.lt_listremark2.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_Remark;

            this.lt_name.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_name;
            this.lt_dateFrom.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_from;
            this.lt_dateTo.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_to;
            this.lt_zone.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_zone;
            this.lt_position.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_position;

            this.btn_search.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_Search;

            lt_tableave.Text= BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_leave;
            lt_tabroster.Text = BLL.MultiLanguageHelper.GetLanguagePacket().rosterIQ_Roster;
        }


        private void SetupTab()
        {
            //tab
            if (actionType == 1)
            {
                this.myTabRoster_leawve.Attributes.Remove("class");
                this.myTabRoster_roster.Attributes.Remove("class");
                this.myTabRoster_roster.Attributes.Add("class", "active");

                this.DivLeaveTab.Visible = false;
                this.DivRosterTab.Visible = true;
                this.rp_leave.DataSource = null;
                this.rp_leave.DataBind();
                this.rp_roster.DataSource = null;
                this.rp_roster.DataBind();
            }
            else
            {
                this.myTabRoster_roster.Attributes.Remove("class");
                this.myTabRoster_leawve.Attributes.Remove("class");
                this.myTabRoster_leawve.Attributes.Add("class", "active");

                this.DivLeaveTab.Visible = true;
                this.DivRosterTab.Visible = false;
                this.rp_leave.DataSource = null;
                this.rp_leave.DataBind();
                this.rp_roster.DataSource = null;
                this.rp_roster.DataBind();
            }

            //onclick="window.location.href='rosterinquiry.aspx?action=1'"
            this.tab_leave.Attributes.Add("onclick", "window.location.href='rosterinquiry.aspx?action=0&s=1'");
            this.tab_roster.Attributes.Add("onclick", "window.location.href='rosterinquiry.aspx?action=1&s=2'");
        }




        private void LoadUI()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, BLL.MultiLanguageHelper.GetLanguagePacket().main_rosterInqury, "~/pages/main.aspx", true);
            SetupTab();
            this.tb_name.Text = loginer.userInfo.surname + " " + loginer.userInfo.firstname;
            this.tb_dateFrom.Text = DateTime.Now.ToString("yyyy-MM-01");
            int days = System.DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            this.tb_dateTo.Text = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(days - 1).ToString("yyyy-MM-dd");

            SetupZone(mV_System_ILeave_Securities);
            SetupPosition(this.DropDownList1.SelectedValue.Trim(),mV_System_ILeave_Securities);

            if (mLoginerOtherInfo.MobileUserLevel == 1)
            {
                this.tb_name.ReadOnly = true;
                this.tb_name.Enabled = false;
                this.DropDownList1.Enabled = false;
                this.DropDownList2.Enabled = false;
            }
        }

        private void SetupPosition(string zoneCode, WebServiceLayer.WebReference_leave.v_System_iLeave_Security[] data)
        {
            string[] loginerZone = data.Where(x => x.StaffID == loginer.userInfo.staffid).Select(x => x.ZoneCode).ToArray();
            var tempData = data.Where(x => loginerZone.Contains(x.ZoneCode));


            if (!string.IsNullOrEmpty(zoneCode))
            {
                tempData = tempData.Where(x => x.ZoneCode == zoneCode).ToArray();
            }
            var tempSorce = tempData.Select(x => new { PositionCode = x.PostionCode }).Distinct().ToArray();
            this.DropDownList2.DataSource = tempSorce;
            this.DropDownList2.DataTextField = "PositionCode";
            this.DropDownList2.DataValueField = "PositionCode";
            this.DropDownList2.DataBind();
            this.DropDownList2.Items.Insert(0, new ListItem("  --  ", ""));
        }

        private void SetupZone(WebServiceLayer.WebReference_leave.v_System_iLeave_Security[] data)
        {
            var tempData = data.Where(x => x.StaffID == loginer.userInfo.staffid).ToArray();

            var tempResult = tempData.Select(x => new { ZoneCode = x.ZoneCode }).Distinct().ToArray();

            this.DropDownList1.DataSource = tempResult;
            this.DropDownList1.DataTextField = "ZoneCode";
            this.DropDownList1.DataValueField = "ZoneCode";
            this.DropDownList1.DataBind();
            this.DropDownList1.Items.Insert(0, new ListItem("  --  ", ""));
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            string name = this.tb_name.Text.Trim();

            string zoneCode = this.DropDownList1.SelectedValue;
            string positionCode = this.DropDownList2.SelectedValue;

            DateTime startDate = DateTime.Parse(DateTime.Now.ToString("yyyy-01-01"));
            DateTime endDate = DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).AddYears(1).AddDays(-1);
            DateTime.TryParse(this.tb_dateFrom.Text,out startDate);
            DateTime.TryParse(this.tb_dateTo.Text, out endDate);



            if (actionType == 0)
            {
                var data = BLL.Other.GetRoster_leavelist(name,zoneCode,positionCode,startDate,endDate);
                this.rp_leave.DataSource = data;
                this.rp_leave.DataBind();
            }
            else if(actionType == 1)
            {
                var data = BLL.Other.GetRoster_Rosterlist(name, zoneCode, positionCode, startDate, endDate);
                this.rp_roster.DataSource = data;
                this.rp_roster.DataBind();
            }
        }

        protected string getRemark(string remark)
        {
            int length = remark.Length;
            int maxleng = System.Math.Min(length, 10);

            return remark.Substring(0, maxleng);

        }


        protected string GetNameByLanguage(WebServiceLayer.WebReference_leave.v_System_iLeave_Leave_List data)
        {
            var lanuageType = BLL.MultiLanguageHelper.GetChoose();
            if (lanuageType == LSLibrary.WebAPP.LanguageType.english)
            {
                return data.English_Name;
            }
            else
            {
                return data.Chinese_Name;
            }
        }

        protected string GetNameByLanguage(WebServiceLayer.WebReference_leave.v_System_iLeave_Roster_List data)
        {
            var lanuageType = BLL.MultiLanguageHelper.GetChoose();
            if (lanuageType == LSLibrary.WebAPP.LanguageType.english)
            {
                return data.English_Name;
            }
            else
            {
                return data.Chinese_Name;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string zoneCode = this.DropDownList1.SelectedValue.Trim();
            SetupPosition(zoneCode, mV_System_ILeave_Securities);

            this.tb_name.Text = "";
        }
    }
}