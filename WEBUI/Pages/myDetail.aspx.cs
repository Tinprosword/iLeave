using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WEBUI.Pages
{
    //1.action 判断用户  2.根据用户类别和请求状态 显示不同的按钮操作.  3. 可能会存在需要查询worktaskid 的时候,这个情况:根据请求类型和状态来获得worktaskid.来调用方法.
    public partial class myDetail : BLL.CustomLoginTemplate
    {
        private int action;
        private int requestId;
        private string applicationType;
        private WebServiceLayer.WebReference_leave.LeaveRequestMaster LeaveRequestMaster;
        private List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> LeaveRequestDetails;
        private WebServiceLayer.WebReference_leave.t_WorkflowInfo RelatedWorkInfo;
        private List<WebServiceLayer.WebReference_leave.t_WorkflowTask> RelatedWorkDetails;

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["requestid"]) && !string.IsNullOrEmpty(Request.QueryString["action"]) && !string.IsNullOrEmpty(Request.QueryString["applicationType"]))
            {
                requestId = int.Parse(Request.QueryString["requestid"]);
                action = int.Parse(Request.QueryString["action"]);
                applicationType = Request.QueryString["applicationType"];
                LeaveRequestMaster = BLL.Leave.GetRequestMasterByRequestID(requestId);
                LeaveRequestDetails = BLL.Leave.GetExtendLeaveDetailsByReuestID(requestId);
                if (LeaveRequestMaster.workinfoID != null)
                {
                    RelatedWorkInfo = BLL.workflow.GetWorkInfoByID((int)LeaveRequestMaster.workinfoID);
                    RelatedWorkDetails = BLL.workflow.Gett_WorkflowTaskByInfoID((int)LeaveRequestMaster.workinfoID);
                }
            }
            else
            {
                Response.Redirect("~/pages/main.aspx");
            }
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {}

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            this.lt_js.Text = "";
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            SetupNavigation();
            setupMainInfo();
            setupLeaveList();
            setupAttendance(requestId);
            SetupButtons();
            SetupMultiLanguage();
        }

        private void SetupNavigation()
        {
            string backTtile = "";
            string currentTitle = "";
            string backUrl = "";

            currentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().application_detailcurrent;
            if (applicationType == "0")
            {
                backTtile = BLL.MultiLanguageHelper.GetLanguagePacket().Back;
            }
            else if (applicationType == "1")
            {
                backTtile = BLL.MultiLanguageHelper.GetLanguagePacket().Back;
            }
            else
            {
                backTtile = BLL.MultiLanguageHelper.GetLanguagePacket().Back;
            }
            if (action == 0)
            {
                backUrl = "~/pages/myapplications.aspx?applicationType=" + applicationType;
            }
            else
            {
                backUrl = "~/pages/approval.aspx?applicationType=" + applicationType;
            }
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, backTtile, currentTitle, backUrl, true);
        }

        private void setupAttendance(int rqid)
        {
            List<MODEL.Apply.app_uploadpic> attandance = BLL.Leave.getAttendance(loginer.loginName, rqid,Server);
            if (attandance != null)
            {
                this.repeater_pic.DataSource = attandance;
                this.repeater_pic.DataBind();
            }
        }

        private void setupLeaveList()
        {
            if (LeaveRequestDetails != null)
            {
                this.repeater_leave.DataSource = LeaveRequestDetails;
                this.repeater_leave.DataBind();
            }
        }

        private void setupMainInfo()
        {
            if (LeaveRequestMaster != null)
            {
                WebServiceLayer.WebReference_leave.t_Leave theLeave = new WebServiceLayer.WebReference_leave.t_Leave();
                theLeave.ID = LeaveRequestMaster.MinLeaveID;
                theLeave = BLL.Leave.GetLeaveByid(theLeave);
                string leaveDesc = theLeave == null ? LeaveRequestMaster.minleaveCode : theLeave.Description;

                this.lb_name.Text = LeaveRequestMaster.uname;
                this.lb_leave.Text = leaveDesc;
                this.lb_status.Text = BLL.GlobalVariate.RequestDesc[(BLL.GlobalVariate.ApprovalRequestStatus)(int)LeaveRequestMaster.Status];
                this.lb_from.Text = LeaveRequestMaster.leavefrom.ToString("yyyy-MM-dd");
                this.lb_to.Text = LeaveRequestMaster.leaveto.ToString("yyyy-MM-dd");
                this.lb_remark.Text = LeaveRequestMaster.remarks;

                double cleanValue = BLL.Leave.GetCleanValue(LeaveRequestMaster.MinLeaveID, (int)loginer.userInfo.staffid, (int)loginer.userInfo.employID);
                this.lb_balancedays.Text = cleanValue == -99999 ? "--" : cleanValue.ToString("0.##") + " D";
            }
        }

        private void SetupButtons()
        {
            this.waitingApproval_admin.Visible = false;
            this.waitingApproval_user.Visible = false;
            this.waitingCanceling_admin.Visible = false;
            this.approved_user.Visible = false;


            //根据状态图,按钮组合只有5种情况, 依据2个变量.所以4种情况不算多,可以全列出,用visable来控制.
            if (action == 0)
            {
                if (LeaveRequestMaster.Status == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE)
                {
                    this.waitingApproval_user.Visible = true;
                }
                else if (LeaveRequestMaster.Status == (int)BLL.GlobalVariate.ApprovalRequestStatus.APPROVE)
                {
                    this.approved_user.Visible = true;
                }
            }
            else
            {
                if (LeaveRequestMaster.Status == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE)
                {
                    this.waitingApproval_admin.Visible = true;
                }
                else if (LeaveRequestMaster.Status == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)
                {
                    this.waitingCanceling_admin.Visible = true;
                }
            }
        }

        private void SetupMultiLanguage()
        {
            this.lt_name.Text = BLL.MultiLanguageHelper.GetLanguagePacket().myapplications_name;
            this.lt_status.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_status;
            this.lt_leave.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_leave;
            //this.lt_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_apply;
            this.lt_date.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_date;
            this.lt_remarks.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_remarks;
            this.lt_balance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_balance;
            this.lt_listdate.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_date;
            this.lt_listtype.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_listtype;
            this.lt_listsection.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_listsection;
            this.lt_attendance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_attendance;
            this.lt_balance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_balance;

            this.button_wait_admin_approval.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_approve;
            this.button_wait_admin_reject.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_reject;

            this.button_Cancel_admin_approval.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_approve;
            this.button_Cancel_admin_Reject.Text= BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_reject;
            this.button_wait_user_Withdraw.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_withdraw;
            this.button_approval_user_Cancel.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_cancel;

        }

        protected void button_apply_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Boolean result = true;
            string errormsg = "";
            string tipmsg = "";
            string backurl = "";

            if (button.ID == this.button_wait_user_Withdraw.ID)
            {
                result=BLL.workflow.WithDrawRequest_leave(requestId, loginer.userInfo.id,"", out errormsg);
                tipmsg = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgwithdraw;
                backurl = "myapplications.aspx?applicationType=0";
            }
            else if (button.ID == this.button_approval_user_Cancel.ID)
            {
                result = BLL.workflow.CancelRequest_leave(requestId, loginer.userInfo.id,"", out errormsg);
                tipmsg = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgcancel;
                backurl = "myapplications.aspx?applicationType=1";
            }
            else if (button.ID == this.button_wait_admin_approval.ID)
            {
                string remarks = this.tb_wait_admin_remarks.Text.Trim();
                result = BLL.workflow.ApproveRequest_leave(requestId, loginer.userInfo.id, remarks, out errormsg);
                tipmsg = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproveok;
                backurl = "approval.aspx?applicationType=0";
                
            }
            else if (button.ID == this.button_wait_admin_reject.ID)
            {
                string remarks = this.tb_wait_admin_remarks.Text.Trim();
                result = BLL.workflow.RejectRequest_leave(requestId, loginer.userInfo.id, remarks, out errormsg);
                tipmsg = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproverej;
                backurl = "approval.aspx?applicationType=0";
                
            }
            else if (button.ID == this.button_Cancel_admin_approval.ID)
            {
                string remarks = this.tb_canceladmin_remarks.Text.Trim();
                result = BLL.workflow.ApprovalCancelRequest_leave(requestId, loginer.userInfo.id,remarks, out errormsg);
                tipmsg = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproveok;
                backurl = "approval.aspx?applicationType=0";
                
            }
            else if (button.ID == this.button_Cancel_admin_Reject.ID)
            {
                string remarks = this.tb_canceladmin_remarks.Text.Trim();
                result = BLL.workflow.RejectCancelRequest_leave(requestId, loginer.userInfo.id, remarks, out errormsg);
                tipmsg = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproverej;
                backurl = "approval.aspx?applicationType=0";
                
            }
            if (result)
            {
                Response.Redirect(backurl);
                //LSLibrary.WebAPP.httpHelper.ResponseRedirectDalay(2.3f, backurl, Response);
            }
            else
            {
                ((WEBUI.Controls.leave)this.Master).SetupMsg(BLL.MultiLanguageHelper.GetLanguagePacket().failure, 2000, WEBUI.Controls.leave.msgtype.info);
            }
        }
        
        protected void linkbtn_file_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string filePath = Server.MapPath(linkButton.CommandArgument);
            bool isimage = BLL.common.IsImagge(System.IO.Path.GetFileName(filePath));
            if (isimage)
            {
                Response.Redirect("showpic2.aspx?path=" + HttpUtility.HtmlEncode(linkButton.CommandArgument));
            }
            else
            {
                Response.Redirect(linkButton.CommandArgument);
            }
        }
    }
}