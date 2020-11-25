using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LSLibrary.WebAPP;
using System.Text;

namespace WEBUI.Pages
{
    public partial class approval_wait : BLL.CustomLoginTemplate
    {
        public static string qs_bigRange = "applicationType";
        public static string qs_action = "action";
        private readonly string tip = "Search Staff";

        protected int actionType = 0;

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString[qs_action]))
            {
                string strAction = Request.QueryString[qs_action];
                int.TryParse(strAction, out actionType);
            }
            else
            {
                Response.Redirect("main.aspx", true);
            }

            string targetName = ((WEBUI.Controls.leave)this.Master).GetMyPostTargetname();
            if (targetName == "p")
            {
                this.Page.LoadComplete += Page_LoadComplete;
            }
            else if(targetName == "h")
            {
                this.Page.LoadComplete += Page_LoadComplete2;
            }
        }

        private void Page_LoadComplete(object sender, EventArgs e)
        {
            this.myTabApproval_pending.Attributes.Add("class", "active");
            this.myTabApproval_history.Attributes.Remove("class");

            SetupRepeater();
        }

        private void Page_LoadComplete2(object sender, EventArgs e)
        {
            this.myTabApproval_history.Attributes.Add("class", "active");
            this.myTabApproval_pending.Attributes.Remove("class");

            SetupRepeater();
        }

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {}

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            SetupNavinigation();
            SetupSearchAndTab();
            SetupRepeater();
            
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {}

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupRepeater();
        }

        protected void tb_staff_TextChanged(object sender, EventArgs e)
        {
            SetupRepeater();
        }

        private void SetupNavinigation()
        {
            string CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().main_approvalTitle;
            if (actionType ==1)
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().main_applicationsTitle;
            }
            string backurl = "~/pages/main.aspx";
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, CurrentTitle, backurl, true);
        }


        private void SetupRepeater()
        {
            int year = int.Parse(this.ddl_year.SelectedValue);
            string name = this.tb_staff.Text.Trim() == tip ? "" : this.tb_staff.Text.Trim();
            GlobalVariate.LeaveBigRangeStatus currentBigRange = GetBigRange();

            List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> ds = null;
            if (actionType == 0)
            {
                ds = BLL.Leave.GetMyManageLeaveMaster(loginer.userInfo.id, currentBigRange, year, name);
            }
            else
            {
                ds = BLL.Leave.GetMyLeaveMaster(loginer.userInfo.personid, currentBigRange, year);
            }

            this.rp_list.DataSource = ds;
            this.rp_list.DataBind();
        }


        private void SetupSearchAndTab()
        {
            //tab
            if (actionType == 0)
            {
                this.myTabApproval.Visible = true;
                this.myTabApply.Visible = false;

                this.myTabApproval_pending.Attributes.Add("class", "active");
                this.myTabApproval_history.Attributes.Remove("class");
            }
            else
            {
                this.myTabApproval.Visible = false;
                this.myTabApply.Visible = true;
            }
            //staff
            this.tb_staff.SetTip(tip);
            this.tb_staff.Visible = actionType == 0;
            //statues
            this.DropDownList1.Visible = actionType == 1;
        }

        public bool BShow_WaitApplyPanel(GlobalVariate.LeaveBigRangeStatus myBigRange, byte states,int action)
        {
            bool result = false;
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && action==0)
            {
                result = true;
            }
            return result;
        }

        public bool BShow_WaitCancelPanel(GlobalVariate.LeaveBigRangeStatus myBigRange, byte states,int action)
        {
            bool result = false;
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL && action==0)
            {
                result = true;
            }
            return result;
        }

        public bool BShow_UserWaitingPanel(GlobalVariate.LeaveBigRangeStatus myBigRange, byte states, int action)
        {
            bool result = false;
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && action == 1)
            {
                result = true;
            }
            return result;
        }

        public bool BShow_UserApprovedPanel(GlobalVariate.LeaveBigRangeStatus myBigRange, byte states, int action)
        {
            bool result = false;
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.approvaled && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.APPROVE && action == 1)
            {
                result = true;
            }
            return result;
        }


        protected void btn_Click(object sender, EventArgs e)
        {
            string waitDiv = LSLibrary.WebAPP.httpHelper.WaitDiv_show(BLL.MultiLanguageHelper.GetLanguagePacket().submit_success);
            Response.Write(waitDiv);
            Response.Flush();
            
            string errormsg;
            bool callResult = true;
            string[] pas = ((Button)sender).CommandArgument.Split(new char[] { '|' });

            int btntype = int.Parse(pas[0]);
            int itemIndex = int.Parse(pas[1]);
            int requestId = int.Parse(pas[2]);

            string remarks1 = ((TextBox)this.rp_list.Items[itemIndex].FindControl("panel_admin_waitingApprove").FindControl("tb_waitapproveRemark")).Text;
            string remarks2 = ((TextBox)this.rp_list.Items[itemIndex].FindControl("panel_admin_waitingCancel").FindControl("tb_waitcancelRemark")).Text;

            if (btntype == 1)//approve apply
            {
                callResult=BLL.workflow.ApproveRequest_leave(requestId, loginer.userInfo.id, remarks1, out errormsg);
            }
            else if (btntype == 2)//reject apply
            {
                callResult = BLL.workflow.RejectRequest_leave(requestId, loginer.userInfo.id, remarks1, out errormsg);
            }
            else if (btntype == 3)//approve cancel
            {
                callResult = BLL.workflow.ApprovalCancelRequest_leave(requestId, loginer.userInfo.id, remarks2, out errormsg);
            }
            else if (btntype == 4)//reject cancel
            {
                callResult = BLL.workflow.RejectCancelRequest_leave(requestId, loginer.userInfo.id, remarks2, out errormsg);
            }
            else if (btntype == 5)//withdraw
            {
                callResult = BLL.workflow.WithDrawRequest_leave(requestId, loginer.userInfo.id, "", out errormsg);
            }
            else if (btntype == 6)//cancel
            {
                callResult = BLL.workflow.CancelRequest_leave(requestId, loginer.userInfo.id, "", out errormsg);
            }
            SetupRepeater();

            this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
        }


        public string GetAttachmentHtml(int requestid)
        {
            List<MODEL.Apply.App_AttachmentInfo> result= BLL.Leave.getAttendanceModel(loginer.userInfo.loginName, requestid, Server);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Count; i++)
            {
                string filename = LSLibrary.FileUtil.GetFileName(result[i].originAttendance_RelatePath);
                if (LSLibrary.FileUtil.IsImagge(filename))
                {
                    sb.Append("<a href='showpic2.aspx?path=" + result[i].originAttendance_RelatePath + "'>" + result[i].GetFileName(10) + "</a>&nbsp;");
                }
                else
                {
                    sb.Append("<a href=" + result[i].Get_originAttendance_RealRelatePath() + ">" + result[i].GetFileName(10) + "</a>&nbsp;");
                }
            }
            return sb.ToString();
        }

        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            SetupRepeater();
        }


        protected void Approvalpending_ServerClick(object sender, EventArgs e)
        {
            SetupRepeater();
        }

        protected void ApprovalHistory_ServerClick(object sender, EventArgs e)
        {
            SetupRepeater();
        }

        protected GlobalVariate.LeaveBigRangeStatus GetBigRange()
        {
            GlobalVariate.LeaveBigRangeStatus currentBigRange = GlobalVariate.LeaveBigRangeStatus.waitapproval;
            if (actionType == 0)
            {
                currentBigRange = string.IsNullOrEmpty(this.myTabApproval_pending.Attributes["class"]) ? GlobalVariate.LeaveBigRangeStatus.beyongdWait : GlobalVariate.LeaveBigRangeStatus.waitapproval;
            }
            else
            {
                currentBigRange = (GlobalVariate.LeaveBigRangeStatus)int.Parse(this.DropDownList1.SelectedValue);
            }
            return currentBigRange;
        }
    }
}