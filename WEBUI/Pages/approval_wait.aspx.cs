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
    //todo 0 waiting approve .approved 是否要显示.  2.reject cancel 好像没有显示出来.显示了2条approved ,好像有点问题.
    //accumulate 的处理验证.
    public partial class approval_wait : BLL.CustomLoginTemplate
    {
        public static string qs_bigRange = "applicationType";//penging history.
        public static string qs_action = "action";//0.my mange data  1.mydata 
        private readonly string tip = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_serchTip;
        private int nametype = 1;

        protected int dataType_myselfOrMyManage = 0;
        protected GlobalVariate.LeaveBigRangeStatus bigRange = 0;

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString[qs_action]) && !string.IsNullOrEmpty(Request.QueryString[qs_bigRange]))
            {
                string strAction = Request.QueryString[qs_action];
                int.TryParse(strAction, out dataType_myselfOrMyManage);
                int intbig = 0;
                int.TryParse(Request.QueryString[qs_bigRange], out intbig);
                bigRange=(GlobalVariate.LeaveBigRangeStatus)intbig;

                nametype = BLL.CodeSetting.GetNameType(BLL.MultiLanguageHelper.GetChoose());
            }
            else
            {
                Response.Redirect("main.aspx", true);
            }
        }

        protected override void InitPage_OnFirstLoad2()
        {

        }

        
        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            lb_errormsg.Text = "";
            lb_errormsg.Visible = false;
            div_error.Visible = false;
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            SetupNavinigation();
            SetupSearchAndTab();
            SetupRepeater();
            MultplayLanguage();
            this.lt_jsscrolltop.Text = "<script>setCookie('st',0);</script>";
        }

        

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
        }

        private void MultplayLanguage()
        {
            this.lt_new.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_new;
            this.lt_mypending.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_pending;
            this.lt_myhistory.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_processed;

            this.lt_pending.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_pending;
            this.lt_processed.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_processed;
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupRepeater();
            this.lt_jsscrolltop.Text = "<script>var vv=setCookie('st',0);";
        }


        private void SetupNavinigation()
        {
            string CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().main_approvalTitle;
            if (dataType_myselfOrMyManage ==1)
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().main_applicationsTitle;
            }
            string backurl = "~/pages/main.aspx";
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, CurrentTitle, backurl, true);
        }


        private void SetupRepeater()
        {
            int year = int.Parse(this.ddl_year.SelectedValue);
            string name = this.tb_staff.Text.Trim() == tip ? "" : this.tb_staff.Text.Trim();
            GlobalVariate.LeaveBigRangeStatus currentBigRange = GetBigRange();

            List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> ds = null;
            if (dataType_myselfOrMyManage == 0)
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
            if (dataType_myselfOrMyManage == 0)
            {
                this.myTabApproval.Visible = true;
                this.myTabApply.Visible = false;


                this.myTabApproval_pending.Attributes.Remove("class");
                this.myTabApproval_history.Attributes.Remove("class");
                if (bigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval)
                {
                    this.myTabApproval_pending.Attributes.Add("class", "active");
                }
                else
                {
                    this.myTabApproval_history.Attributes.Add("class", "active");
                }
            }
            else
            {
                this.myTabApproval.Visible = false;
                this.myTabApply.Visible = true;

                this.myTabapply_new.Attributes.Remove("class");
                this.myTabapply_pending.Attributes.Remove("class");
                this.myTabapply_history.Attributes.Remove("class");

                if (bigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval)
                {
                    this.myTabapply_pending.Attributes.Add("class", "active");
                }
                else
                {
                    this.myTabapply_history.Attributes.Add("class", "active");
                }
            }
            //staff
            this.tb_staff.SetTip(tip);
            this.tb_staff.Visible = dataType_myselfOrMyManage == 0;
            this.ib_search.Visible = dataType_myselfOrMyManage == 0;
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
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.beyongdWait && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.APPROVE && action == 1)
            {
                result = true;
            }
            return result;
        }


        protected void btn_Click(object sender, EventArgs e)
        {
            string errormsg = "";
            bool callResult = true;
            string[] pas = ((Button)sender).CommandArgument.Split(new char[] { '|' });

            int btntype = int.Parse(pas[0]);
            int itemIndex = int.Parse(pas[1]);
            int requestId = int.Parse(pas[2]);

            string remarks1 = ((TextBox)this.rp_list.Items[itemIndex].FindControl("panel_admin_waitingApprove").FindControl("tb_waitapproveRemark")).Text;
            string remarks2 = ((TextBox)this.rp_list.Items[itemIndex].FindControl("panel_admin_waitingCancel").FindControl("tb_waitcancelRemark")).Text;


            if ((btntype==2 && string.IsNullOrEmpty(remarks1)) || btntype==4 && string.IsNullOrEmpty(remarks2))
            {
                this.lb_errormsg.Visible = true;
                this.lb_errormsg.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approval_needRemark;
            }
            else
            {
                string waitDiv = LSLibrary.WebAPP.httpHelper.WaitDiv_show(BLL.MultiLanguageHelper.GetLanguagePacket().Commonsubmit_success);
                Response.Write(waitDiv);
                Response.Flush();

                string successMsg = "";
                if (btntype == 1)//approve apply
                {
                    callResult = BLL.workflow.ApproveRequest_leave(requestId, loginer.userInfo.id, remarks1, out errormsg);
                    successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproveok);
                }
                else if (btntype == 2)//reject apply
                {
                    callResult = BLL.workflow.RejectRequest_leave(requestId, loginer.userInfo.id, remarks1, out errormsg);
                    successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproverej);
                }
                else if (btntype == 3)//approve cancel
                {
                    callResult = BLL.workflow.ApprovalCancelRequest_leave(requestId, loginer.userInfo.id, remarks2, out errormsg);
                    successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproveok);
                }
                else if (btntype == 4)//reject cancel
                {
                    callResult = BLL.workflow.RejectCancelRequest_leave(requestId, loginer.userInfo.id, remarks2, out errormsg);
                    successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproverej);
                }

                else if (btntype == 5)//withdraw
                {
                    callResult = BLL.workflow.WithDrawRequest_leave(requestId, loginer.userInfo.id, "", out errormsg);
                    successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgwithdraw);
                }
                else if (btntype == 6)//cancel
                {
                    var firstinfo = BLL.Leave.GetFirstRequestinfoa(requestId);
                    if (!firstinfo.hasCancel)
                    {
                        callResult = BLL.workflow.CancelRequest_leave(requestId, loginer.userInfo.id, "", out errormsg);
                        successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgcancel);
                    }
                    else
                    {
                        lb_errormsg.Text = "Can not cancel again";
                        lb_errormsg.Visible = true;
                        div_error.Visible = true;
                    }
                }
                SetupRepeater();

                if (callResult)
                {
                    Response.Write(successMsg + ".");
                }
                else
                {
                    Response.Write(errormsg);
                }
                Response.Flush();
                System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验

                this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();

                this.lt_jsscrolltop.Text = "<script>var vv=getCookie('st'); $('#maindata').scrollTop(vv);</script>";
            }
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
            return bigRange;
        }

        protected void ib_search_Click(object sender, ImageClickEventArgs e)
        {
            SetupRepeater();
        }

        public string GetStaffName(WebServiceLayer.WebReference_leave.LeaveRequestMaster leaveRequestMaster)
        {
            MODEL.UserName tempUser = new MODEL.UserName(leaveRequestMaster.p_Surname, leaveRequestMaster.p_Othername, leaveRequestMaster.p_Nickname, leaveRequestMaster.p_NameCH);
            return tempUser.GetDisplayName(nametype);
        }

        public string GetLeaveStatus(WebServiceLayer.WebReference_leave.LeaveRequestMaster leaveRequestMaster)
        {
            return BLL.Leave.GetLeaveStatusDesc(leaveRequestMaster.WorkflowTypeID,leaveRequestMaster.Status);
        }
    }
}