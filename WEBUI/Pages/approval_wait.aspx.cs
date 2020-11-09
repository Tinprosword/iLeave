using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LSLibrary.WebAPP;

namespace WEBUI.Pages
{
    public partial class approval_wait : BLL.CustomLoginTemplate
    {
        public static string qs_bigRange = "applicationType";

        private readonly string tip = "Staff";
        private GlobalVariate.LeaveBigRangeStatus applicationType = GlobalVariate.LeaveBigRangeStatus.waitapproval;
        
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString[qs_bigRange]))
            {
                applicationType = GlobalVariate.LeaveBigRangeStatus.waitapproval;
            }
            else
            {
                Response.Redirect("main.aspx", true);
            }
        }

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            SetupNavinigation();
            SetupRepeat();
            this.tb_staff.SetTip(tip);
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {

        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupRepeat();
        }

        protected void tb_staff_TextChanged(object sender, EventArgs e)
        {
            SetupRepeat();
        }

        private void SetupNavinigation()
        {
            string CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().approvalmain_menu1;
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, CurrentTitle, "~/pages/approvalmain.aspx", true);
        }

        private void SetupRepeat()
        {
            int year = int.Parse(this.ddl_year.SelectedValue);
            string name = this.tb_staff.Text.Trim() == tip ? "" : this.tb_staff.Text.Trim();
            this.rp_list.DataSource = BLL.Leave.GetMyManageLeaveMaster(loginer.userInfo.id, applicationType, year, name);
            this.rp_list.DataBind();
        }
    }
}