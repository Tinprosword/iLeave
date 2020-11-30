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
            SetMultiLanguage();
        }

        private void SetMultiLanguage()
        {
            this.lt_applyleaveabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_apply;
            this.lt_approal.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_approvalTitle;
            this.lt_calendarabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_calendar;
            //this.lt_downloadsplitabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_downplaylisp;
            //this.lt_downloadtaxabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_downtax;
            this.lt_setting.Text = BLL.MultiLanguageHelper.GetLanguagePacket().main_setting;
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

        protected void Change_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/chooseEmployment.aspx?pid=" + loginer.userInfo.personid + "&sourcetype=2");
        }
    }
}