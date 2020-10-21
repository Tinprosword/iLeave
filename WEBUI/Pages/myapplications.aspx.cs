using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class myapplications : BLL.CustomLoginTemplate
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

            string requestid = ((WEBUI.Controls.leave)this.Master).GetMyPostBackArgumentByTargetname("detail");
            if (!string.IsNullOrEmpty(requestid))
            {
                Response.Redirect("~/Pages/myDetail.aspx?applicationType=" + applicationType + "&action=0&requestid=" + requestid, true);
            }
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
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

            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text, loginer.userInfo.personid);
            this.repeater_myapplications.DataBind();
        }

        private void SetupNavinigation()
        {
            string CurrentTitle = "";
            if (applicationType == "0")
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().mymyapplicationmain_wait; ;
            }
            else if (applicationType == "1")
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().mymyapplicationmain_approved;
            }
            else
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().mymyapplicationmain_rejected;
            }

            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, CurrentTitle, "~/pages/myapplicationMain.aspx", true);
        }

        private void SetupMultiLanguage()
        {
            this.ltdatefrom.Text = BLL.MultiLanguageHelper.GetLanguagePacket().myapplications_datefrom;
            this.lt_listdate.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_listdate;
            this.lt_listUint.Text= BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_listuint;
            this.lt_listtype.Text= BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_listtype;
        }


        protected void btn_wait_Click(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text, loginer.userInfo.personid);
            this.repeater_myapplications.DataBind();
        }


        protected void btn_approved_Click(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource( getStatus(), loginer.userInfo.id, this.tb_date.Text,  loginer.userInfo.personid);
            this.repeater_myapplications.DataBind();
        }

        protected void btn_rejectWith_Click(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource( getStatus(), loginer.userInfo.id, this.tb_date.Text,  loginer.userInfo.personid);
            this.repeater_myapplications.DataBind();
        }


        protected void tb_date_TextChanged1(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource( getStatus(), loginer.userInfo.id, this.tb_date.Text,  loginer.userInfo.personid);
            this.repeater_myapplications.DataBind();
        }


        private BLL.GlobalVariate.LeaveBigRangeStatus getStatus()
        {
            BLL.GlobalVariate.LeaveBigRangeStatus result = BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval;
            if (applicationType=="0")
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


        //chooseStatus:1 ,apporve ,0 wait .2 reject
        private static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetDatasource( BLL.GlobalVariate.LeaveBigRangeStatus chooseStatus, int uid, string datestrFrom,  int pid)
        {
            List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> result = null;
            DateTime? dateFrom = null;
            if (!string.IsNullOrEmpty(datestrFrom))
            {
                dateFrom = DateTime.Parse(datestrFrom);
            }
            result = BLL.Leave.GetMyLeaveMaster(pid, chooseStatus, dateFrom);
            return result;
        }

    }
}