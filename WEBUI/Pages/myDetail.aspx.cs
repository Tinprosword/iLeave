using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//todo 为什么取消假期,不是直接修改状态?
namespace WEBUI.Pages
{
    public partial class myDetail : BLL.CustomLoginTemplate
    {
        private int requestId;
        private int action;//0 manage myrequest 1:namage other person's request
        private WebServiceLayer.WebReference_leave.LeaveRequestMaster LeaveRequestMaster;
        private List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> LeaveRequestDetails;
        private WebServiceLayer.WebReference_leave.t_WorkflowInfo workflowinfo;
        private List<WebServiceLayer.WebReference_leave.t_WorkflowTask> Workflows;

        private bool isHandlerOfLeave = false;

        protected override void InitPageVaralbal0()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["requestid"]) && !string.IsNullOrEmpty(Request.QueryString["action"]))
            {
                requestId = int.Parse(Request.QueryString["requestid"]);
                action = int.Parse(Request.QueryString["action"]);
                LeaveRequestMaster = BLL.Leave.GetRequestMasterByRequestID(requestId);
                LeaveRequestDetails = BLL.Leave.GetExtendLeaveDetailsByReuestID(requestId);
                workflowinfo = BLL.workflow.Gett_WorkflowInfoByRequestID(requestId);
                if (workflowinfo != null)
                {
                    Workflows = BLL.workflow.Gett_WorkflowTaskByInfoID(workflowinfo.ID);

                    if (action == 1 )
                    {
                        isHandlerOfLeave = true;
                    }
                    else
                    {
                        isHandlerOfLeave = false;
                    }

                    if (LeaveRequestMaster != null && LeaveRequestDetails != null && LeaveRequestDetails.Count() > 0 && workflowinfo!=null)
                    { }
                    else
                    {
                        Response.Redirect("~/pages/main.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/pages/main.aspx");
                }
            }
            else
            {
                Response.Redirect("~/pages/main.aspx");
            }
        }

        protected override void ResetUIOnEachLoad5()
        {
        }

        protected override void InitPageDataOnEachLoad1()
        {
            
        }

        protected override void InitPageDataOnFirstLoad2()
        {}

        protected override void ResetUIOnEachLoad3()
        {}

        protected override void InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().application_detailback, BLL.MultiLanguageHelper.GetLanguagePacket().application_detailcurrent, "~/pages/myapplications.aspx");
            setupMainInfo();
            setupLeaveList();
            setupAttendance();
            SetupButtons();
            SetupMultiLanguage();
        }

        private void setupAttendance()
        {
            List<MODEL.Apply.app_uploadpic> attandance = BLL.Leave.getAttendance(loginer.loginName, 3);
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
            if (LeaveRequestMaster != null )
            {
                //todo get request info and user info ,and set value to lables
                this.lb_name.Text = LeaveRequestMaster.uname;
                this.lb_leave.Text = LeaveRequestMaster.minleaveCode;
                this.lb_status.Text = ((BLL.GlobalVariate.ApprovalRequestStatus)(LeaveRequestMaster.Status)).ToString();
                this.lb_from.Text = LeaveRequestMaster.leavefrom.ToString("yyyy-MM-dd");
                this.lb_to.Text = LeaveRequestMaster.leaveto.ToString("yyyy-MM-dd");
                this.lb_remark.Text = LeaveRequestMaster.remarks;
            }
        }

        private void SetupButtons()
        {
            this.wait_user.Visible = false;
            this.wait_admin.Visible = false;
            this.withdrawing_admin.Visible = false;
            this.approval_user.Visible = false;


            //根据状态图,按钮组合只有5种情况, 依据2个变量.所以4种情况不算多,可以全列出,用visable来控制.
            if (LeaveRequestMaster.Status == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && isHandlerOfLeave == true)
            {
                this.wait_admin.Visible = true;
            }
            else if (LeaveRequestMaster.Status == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && isHandlerOfLeave == false)
            {
                this.wait_user.Visible = true;
            }
            else if (LeaveRequestMaster.Status == (int)BLL.GlobalVariate.ApprovalRequestStatus.APPROVE && isHandlerOfLeave == false)
            {
                this.approval_user.Visible = true;
            }
            else if(LeaveRequestMaster.Status == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL && isHandlerOfLeave==true)
            {
                this.withdrawing_admin.Visible = true;
            }
        }

        private void SetupMultiLanguage()
        {
            this.lt_name.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_name;
            this.lt_status.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_status;
            this.lt_leave.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_leave;
            this.lt_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_apply;
            this.lt_date.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_date;
            this.lt_remarks.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_remarks;
            this.lt_balance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_balance;
            this.lt_listdate.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_date;
            this.lt_listtype.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_listtype;
            this.lt_listsection.Text= BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_listsection;
            this.lt_attendance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_attendance;
            this.lt_balance.Text= BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_balance;
        }

        protected void button_apply_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.ID == this.button_wait_admin_approval.ID)
            {
                if (isHandlerOfLeave)
                {
                    string errormsg;
                    var wfs = Workflows.Where(x => x.UserID == loginer.userInfo.id && x.InOutTypeID == 0 && x.WorkflowInfoID == workflowinfo.ID).ToArray();
                    if (wfs.Count() == 1)//get task need i approva
                    {
                        BLL.workflow.ApproveRequest_leave(requestId, wfs[0].ID, loginer.userInfo.id, out errormsg);
                    }
                }
            }
            else if (button.ID == this.button_wait_admin_reject.ID)
            {
                if (isHandlerOfLeave)
                {
                    string errormsg;
                    var wfs = Workflows.Where(x => x.UserID == loginer.userInfo.id && x.InOutTypeID == 1 && x.WorkflowInfoID == workflowinfo.ID).ToArray();
                    if (wfs.Count() == 1)//get task i send.
                    {
                        BLL.workflow.RejectRequest_leave(requestId, wfs[0].ID, loginer.userInfo.id, out errormsg);
                    }
                }
            }
            else if (button.ID == this.button_wait_user_Withdraw.ID)
            {
                string errormsg;
                var wfs = Workflows.Where(x => x.UserID == loginer.userInfo.id && x.InOutTypeID == 1 && x.WorkflowInfoID == workflowinfo.ID).ToArray();
                if (wfs.Count() == 1)//get task i send.
                {
                    BLL.workflow.WithDrawRequest_leave(requestId, wfs[0].ID, loginer.userInfo.id, out errormsg);
                }
            }
            else if (button.ID == this.button_approval_user_Cancel.ID)
            {
                string errormsg;
                BLL.workflow.CancelRequest_leave(requestId, loginer.userInfo.id, out errormsg);
            }
            else if (button.ID == this.button_Cancel_admin_approval.ID)
            {}
            else if (button.ID == this.button_Cancel_admin_Reject.ID)
            {}
        }

    }
}