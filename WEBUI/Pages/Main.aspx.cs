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
        protected override void InitPage_OnEachLoadBeforeF5_1()
        {
        }


        protected override void PageLoad_ResetUIOnEachLoad5()
        {

        }

        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(false, "", "", "");
            SetMultiLanguage();
        }

        private void SetMultiLanguage()
        {
            this.lt_applyleaveabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_apply;
            this.lt_applicationsabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_applications;
            this.lt_approal.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_approval;
            this.lt_calendarabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_calendar;
            this.lt_downloadsplitabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_downplaylisp;
            this.lt_downloadtaxabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_downtax;
            this.lt_setting.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_setting;
        }

        protected override void PageLoad_ResetUIOnEachLoad3()
        {}

        protected void Apply_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/apply.aspx");
        }

        protected void Application_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/myapplications.aspx");
        }

        protected void Approval_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/approval.aspx");
        }

        protected void Canlendar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/calendar.aspx");
        }

        protected void Roster_Click(object sender, EventArgs e)
        {}

        protected void Money_Click(object sender, EventArgs e)
        {}

        protected void Tax_Click(object sender, EventArgs e)
        {}

        protected void lt_applyleaveabc_Click(object sender, EventArgs e)
        {}

        protected void Setting_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/setting.aspx");
        }

    }
}