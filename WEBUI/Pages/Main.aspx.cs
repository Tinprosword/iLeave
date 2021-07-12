using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Main : BLL.CustomLoginTemplate
    {
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {}


        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {}

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            if (loginer.userInfo.moreEmployment)
            {
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "", "DW-iLeave", "~/pages/chooseEmployment.aspx?pid=" + loginer.userInfo.personid, true);
            }
            else
            {
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "", "DW-iLeave", "~/pages/chooseEmployment.aspx?pid=" + loginer.userInfo.personid, false);
            }
            DisplayMenu(BLL.CodeSetting.GetMenu());
            SetMultiLanguage();
        }

        private void DisplayMenu(string v)
        {
            this.menu1.Visible = this.menu2.Visible = this.menu3.Visible = this.menu4.Visible = this.menu5.Visible = true;
            if (!string.IsNullOrEmpty(v))
            {
                if (v.Contains("1"))
                {
                    this.menu1.Visible = false;
                }
                if (v.Contains("2"))
                {
                    this.menu2.Visible = false;
                }
                if (v.Contains("3"))
                {
                    this.menu3.Visible = false;
                }
                if (v.Contains("4"))
                {
                    this.menu4.Visible = false;
                }
                if (v.Contains("5"))
                {
                    this.menu5.Visible = false;
                }
                if (v.Contains("6"))
                {
                    this.menu6.Visible = false;
                }
            }
        }

        private void SetMultiLanguage()
        {
            this.lt_applyleaveabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_apply;
            this.lt_approal.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_approvalTitle;
            this.lt_calendarabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_calendar;
            this.lt_setting.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_setting;
            this.lt_check.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_check;
            this.lt_RosterInquiry.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_rosterInqury;
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        protected void Apply_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/apply.aspx");
        }

        protected void Application_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/myapplicationmain.aspx");
        }

        protected void Approval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/approval_wait.aspx?action=0&applicationtype=0");
        }

        protected void Canlendar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/calendar.aspx");
        }

        protected void Check_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/Check.aspx");
        }

        protected void Setting_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/setting.aspx");
        }

        protected void Change_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/chooseEmployment.aspx?pid=" + loginer.userInfo.personid + "&sourcetype=2");
        }
        protected void RosterInquiry_Click(object sender, EventArgs e)
        {
                
        }

    }
}