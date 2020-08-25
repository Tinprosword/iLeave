﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WEBUI.Pages
{
    public partial class myDetail : BLL.CustomLoginTemplate
    {
        private int requestId;
        private WebServiceLayer.WebReference_leave.LeaveRequestMaster LeaveRequestMaster;
        private List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> LeaveRequestDetails;

        protected override void InitPageVaralbal0()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["requestid"]))
            {
                requestId = int.Parse(Request.QueryString["requestid"]);
                LeaveRequestMaster = BLL.Leave.GetRequestMasterByRequestID(requestId);
                LeaveRequestDetails = BLL.Leave.GetExtendLeaveDetailsByReuestID(requestId);
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
        { }

        protected override void ResetUIOnEachLoad3()
        { }

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
            if (LeaveRequestMaster != null)
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
            this.approval_user.Visible = false;


            //根据状态图,按钮组合只有5种情况, 依据2个变量.所以4种情况不算多,可以全列出,用visable来控制.
            if (LeaveRequestMaster.Status == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE)
            {
                this.wait_user.Visible = true;
            }
            else if (LeaveRequestMaster.Status == (int)BLL.GlobalVariate.ApprovalRequestStatus.APPROVE)
            {
                this.approval_user.Visible = true;
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
            this.lt_listsection.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_listsection;
            this.lt_attendance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_attendance;
            this.lt_balance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_balance;
        }

        protected void button_apply_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.ID == this.button_wait_user_Withdraw.ID)
            {
                string errormsg;
                BLL.workflow.WithDrawRequest_leave(requestId, loginer.userInfo.id, out errormsg);
            }
            else if (button.ID == this.button_approval_user_Cancel.ID)
            {
                string errormsg;
                BLL.workflow.CancelRequest_leave(requestId, loginer.userInfo.id, out errormsg);
            }
        }


    }
}