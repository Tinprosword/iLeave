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
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["applicationType"]))
            {
                applicationType = Request.QueryString["applicationType"];
            }
            else
            {
                Response.Redirect("Main.aspx");
            }

            string requestid= ((WEBUI.Controls.leave)this.Master).GetMyPostBackArgumentByTargetname("detail");
            if (!string.IsNullOrEmpty(requestid) )
            {
                Response.Redirect("~/Pages/myDetail.aspx?applicationType=" + applicationType + "&action=1&requestid=" + requestid, true);
            }
        }

        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            SetupNavinigation();
            SetupMultiLanguage();
            this.tb_date.Text = System.DateTime.Now.ToString("yyyy-MM-01");
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text, this.tb_name.Text.Trim());
            this.repeater_myapplications.DataBind();
        }

        private void SetupNavinigation()
        {
            string CurrentTitle = "";
            if (applicationType == "0")
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().approvalmain_menu1;
            }
            else if (applicationType == "1")
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().approvalmain_menu2;
            }
            else
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().approvalmain_menu3;
            }

            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, CurrentTitle, "~/pages/approvalmain.aspx", true);
        }


        private void SetupMultiLanguage()
        {
            this.ltdatefrom.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approval_from;
            this.lt_name.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approval_name;
            this.lt_listdate.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approval_list_column1;
            this.lt_listuser.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approval_list_column2;
            this.lt_unit.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approval_list_column3;
            this.lt_type.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approval_list_column4;
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {}

        private List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetDatasource(GlobalVariate.LeaveBigRangeStatus leaveBigRangeStatus,int approverUid,string datestrFrom,string userName)
        {
            DateTime? dateFrom = null;
            if (!string.IsNullOrEmpty(datestrFrom))
            {
                dateFrom = DateTime.Parse(datestrFrom);
            }
            return BLL.Leave.GetMyManageLeaveMaster(loginer.userInfo.id, leaveBigRangeStatus, dateFrom, userName);
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

        protected void tb_date_TextChanged(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text,this.tb_name.Text.Trim());
            this.repeater_myapplications.DataBind();
        }

        protected void tb_name_TextChanged(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text, this.tb_name.Text.Trim());
            this.repeater_myapplications.DataBind();
        }

        
    }
}