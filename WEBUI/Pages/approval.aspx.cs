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
        private readonly string css_select = "btnBox btnBlueBoxSelect";
        private readonly string css_unselect = "btnBox btnBlueBoxUnSelect";


        protected override void InitPageVaralbal0()
        {
        }

        protected override void InitPageDataOnEachLoad1()
        {
        }

        protected override void InitPageDataOnFirstLoad2()
        {
            this.btn_approved.CssClass = css_unselect;
            this.btn_wait.CssClass = css_select;
            this.btn_rejectWith.CssClass = css_unselect;
        }

        protected override void ResetUIOnEachLoad3()
        {
            
        }

        protected override void InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().approval_back, BLL.MultiLanguageHelper.GetLanguagePacket().approval_current, "~/pages/main.aspx");
            SetupMultiLanguage();

            this.tb_date.Text = System.DateTime.Now.ToString("yyyy-MM-01");
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id,this.tb_date.Text);
            this.repeater_myapplications.DataBind();
        }

        
        private void SetupMultiLanguage()
        {
        }

        protected override void ResetUIOnEachLoad5()
        {
        }

        private List<BLL.workflow.Worktask_leave> GetDatasource(GlobalVariate.LeaveBigRangeStatus leaveBigRangeStatus,int approverUid,string datestrFrom)
        {
            DateTime? dateFrom = null;
            if (!string.IsNullOrEmpty(datestrFrom))
            {
                dateFrom = DateTime.Parse(datestrFrom);
            }
            return BLL.workflow.GetWorktask_leave(leaveBigRangeStatus, approverUid, dateFrom);
        }

        private BLL.GlobalVariate.LeaveBigRangeStatus getStatus()
        {
            BLL.GlobalVariate.LeaveBigRangeStatus result = BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval;
            if (this.btn_approved.CssClass == css_select)
            {result = BLL.GlobalVariate.LeaveBigRangeStatus.approvaled;}
            else if (this.btn_wait.CssClass == css_select)
            {result = BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval;}
            else
            {result = BLL.GlobalVariate.LeaveBigRangeStatus.withdraw;}

            return result;
        }

        protected void btn_wait_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_unselect;
            this.btn_wait.CssClass = css_select;
            this.btn_rejectWith.CssClass = css_unselect;
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id,this.tb_date.Text);
            this.repeater_myapplications.DataBind();
        }

        protected void btn_approved_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_select;
            this.btn_wait.CssClass = css_unselect;
            this.btn_rejectWith.CssClass = css_unselect;


            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text);
            this.repeater_myapplications.DataBind();
        }

        protected void btn_rejectWith_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_unselect;
            this.btn_wait.CssClass = css_unselect;
            this.btn_rejectWith.CssClass = css_select;

            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text);
            this.repeater_myapplications.DataBind();
        }

        protected void tb_date_TextChanged(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text);
            this.repeater_myapplications.DataBind();
        }
    }
}