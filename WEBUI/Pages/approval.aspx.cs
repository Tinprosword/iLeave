using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace WEBUI.Pages
{
    public partial class approval : BLL.CustomLoginTemplate
    {
        private string applicationType = "0";
        protected override void InitPage_OnEachLoadBeforeF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["applicationType"]))
            {
                applicationType = Request.QueryString["applicationType"];
            }
            else
            {
                Response.Redirect("Main.aspx");
            }
        }

        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_ResetUIOnEachLoad3()
        {
            
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            SetupNavinigation();
            SetupMultiLanguage();
            this.tb_date.Text = System.DateTime.Now.ToString("yyyy-MM-01");
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text);
            this.repeater_myapplications.DataBind();
        }

        private void SetupNavinigation()
        {
            string CurrentTitle = "";
            if (applicationType == "0")
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().application_List_waitCurrent;
            }
            else if (applicationType == "1")
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().application_List_approvedCurrent;
            }
            else
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().application_List_rejectCurrent;
            }

            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().approval_List_wait_apporved_reject_Back, CurrentTitle, "~/pages/approvalmain.aspx");
        }


        private void SetupMultiLanguage()
        {
        }

        protected override void PageLoad_ResetUIOnEachLoad5()
        {
        }

        private List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetDatasource(GlobalVariate.LeaveBigRangeStatus leaveBigRangeStatus,int approverUid,string datestrFrom)
        {
            DateTime? dateFrom = null;
            if (!string.IsNullOrEmpty(datestrFrom))
            {
                dateFrom = DateTime.Parse(datestrFrom);
            }
            return BLL.Leave.GetMyManageLeaveMaster(loginer.userInfo.id, leaveBigRangeStatus, dateFrom, "");
        }

        private BLL.GlobalVariate.LeaveBigRangeStatus getStatus()
        {
            BLL.GlobalVariate.LeaveBigRangeStatus result = BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval;
            if (applicationType == "0")
            {
                result = BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval;
            }
            else if (applicationType == "1")
            {
                result = BLL.GlobalVariate.LeaveBigRangeStatus.approvaled;
            }
            else
            {
                result = BLL.GlobalVariate.LeaveBigRangeStatus.withdraw;
            }

            return result;
        }

        protected void btn_wait_Click(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id,this.tb_date.Text);
            this.repeater_myapplications.DataBind();
        }

        protected void btn_approved_Click(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text);
            this.repeater_myapplications.DataBind();
        }

        protected void btn_rejectWith_Click(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text);
            this.repeater_myapplications.DataBind();
        }

        protected void tb_date_TextChanged(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text);
            this.repeater_myapplications.DataBind();
        }

        protected void lb_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            string requestid = link.CommandArgument;

            Response.Redirect("~/Pages/myDetail.aspx?applicationType="+applicationType+"&action=1&requestid=" + requestid, true);
        }

    }
}