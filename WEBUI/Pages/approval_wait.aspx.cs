﻿using BLL;
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
        //这3个参数，控制了数据的来源，和对应的操作面板的显示.
        //action=1 时.   clot,leave 取决于 qs_from?        action=1时，clot leave取决于 页面控件radio. from 只是一个站位参数，默认为1.

        
        public static string qs_action = "action";//0.my mange data  1.mydata 
        public static string qs_bigRange = "applicationType";//0:penging. 3:history.
        public static string qs_from = "from";//0.leave 1.clot 
        public static string qs_requestid = "requestid";

        public static int attachmentNewLine = 6;

        public static string sessionname_rdl = "rdl";

        private readonly string tip = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_serchTip;
        private int nametype = 1;

        protected int dataType_myselfOrMyManage = 0;
        protected GlobalVariate.LeaveBigRangeStatus bigRange = 0;
        protected int from = 0;
        protected int mRequestid = 0;


        public static int getrdlvalue(System.Web.SessionState.HttpSessionState ss)
        {
            int result = 0;
            if (ss[sessionname_rdl] != null)
            {
                result =(int) ss[sessionname_rdl];
            }
            return result;
        }

        public static void setrdlvalue(System.Web.SessionState.HttpSessionState ss,int value)
        {
            ss[sessionname_rdl] = value;
        }


        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
   
            if (!string.IsNullOrEmpty(Request.QueryString[qs_action]) && !string.IsNullOrEmpty(Request.QueryString[qs_bigRange]) && !string.IsNullOrEmpty(Request.QueryString[qs_from]))
            {
                string strAction = Request.QueryString[qs_action];
                int.TryParse(strAction, out dataType_myselfOrMyManage);
                int intbig = 0;
                int.TryParse(Request.QueryString[qs_bigRange], out intbig);
                bigRange=(GlobalVariate.LeaveBigRangeStatus)intbig;
                string strfrom = Request.QueryString[qs_from];
                int.TryParse(strfrom, out from);
                string strRequestid = Request.QueryString[qs_requestid];
                int.TryParse(strRequestid, out mRequestid);

                nametype = BLL.CodeSetting.GetNameType(BLL.MultiLanguageHelper.GetChoose());
            }
            else
            {
                Response.Redirect("main.aspx", true);
            }
        }

        protected override void InitPage_OnNotFirstLoad2()
        {

        }

        protected override void PageLoad_InitUIOnNotFirstLoad4()
        {

        }

        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            lb_errormsg.Text = "";
            lb_errormsg.Visible = false;
            div_error.Visible = false;
            js_error.Text = "";
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

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int tempBatchNum = -1;

            int.TryParse(this.lb_checkedNumber.Text, out tempBatchNum);

            if (tempBatchNum == 0)
            {
                this.btn_batchApprover.Enabled = false;
                this.btn_batchApprover.Attributes.CssStyle.Remove("background-color");
                this.btn_batchApprover.BackColor = System.Drawing.Color.LightGray;
            }
            else if (tempBatchNum > 0)
            {
                this.btn_batchApprover.Enabled = true;
                this.btn_batchApprover.Attributes.CssStyle.Remove("background-color");
                this.btn_batchApprover.BackColor = System.Drawing.Color.White;
            }
        }

        private void MultplayLanguage()
        {
            this.lt_new.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_new;
            this.lt_mypending.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_pending;
            this.lt_myhistory.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_processed;
            this.lt_estimation.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_estimation;

            this.lt_pending.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_pending;
            this.lt_processed.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_processed;

            this.cb_batch.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approvalWait_CLOT_batchCheck;
            this.btn_batchApprover.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approvalWait_CLOT_batchApprove;
            this.lb_batchSelected.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approvalWait_CLOT_batchSelected;

            foreach (ListItem theItem in this.rbl_sourceType.Items)
            {
                if (theItem.Value == "0")
                {
                    theItem.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_op_leave;
                }
                else if (theItem.Value == "1")
                {
                    theItem.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_op_CLOT;
                }
            }
        }

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupRepeater();
            this.lt_jsscrolltop.Text = "<script>var vv=setCookie('st',0);";
            UI_ClearApproveBatch();
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
            int sourceType = 0;

            //applyer
            if (dataType_myselfOrMyManage == 1)
            {
                if (from == 0)
                {
                    sourceType = 0;
                }
                else if (from == 1)
                {
                    sourceType = 1;
                }
            }
            else//approver
            {
                try
                {
                    sourceType = int.Parse(this.rbl_sourceType.SelectedValue);
                }
                catch
                {
                    this.rp_clot.Visible = false;
                    this.rp_list.Visible = false;
                    return;//hidden 了 leave and cl/ot. 所以直接不显示repeater.
                }
            }

            this.rp_clot.Visible = false;
            this.rp_list.Visible = false;

            GlobalVariate.LeaveBigRangeStatus currentBigRange = GetBigRange();
            int year = int.Parse(this.ddl_year.SelectedValue);
            string name = this.tb_staff.Text.Trim() == tip ? "" : this.tb_staff.Text.Trim();

            if (sourceType == 0)//LEAVE
            {
                this.rp_list.Visible = true;
                
                List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> ds = null;

                if (mRequestid > 0)
                {
                    if (dataType_myselfOrMyManage == 0)
                    {
                        ds = BLL.Leave.GetMyManageLeaveMasterByRequestid(loginer.userInfo.u_id, currentBigRange,mRequestid);
                    }
                    else
                    {
                        ds = BLL.Leave.GetMyLeaveMasterByRequestID(loginer.userInfo.personid, currentBigRange,mRequestid);
                    }

                    if (ds.Count() == 0)
                    {
                        SetupRepeater_NOtExistRequest();
                    }
                }
                else
                {
                    if (dataType_myselfOrMyManage == 0)
                    {
                        ds = BLL.Leave.GetMyManageLeaveMaster(loginer.userInfo.u_id, currentBigRange, year, name);
                    }
                    else
                    {
                        ds = BLL.Leave.GetMyLeaveMaster(loginer.userInfo.personid, currentBigRange, year);
                    }
                }


                this.rp_list.DataSource = ds;
                this.rp_list.DataBind();
            }
            else//CLOT: 1.ME OR MANAGE 2.PENGDING OR History.
            {
                this.rp_clot.Visible = true;

                List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> ds = new List<WebServiceLayer.WebReference_leave.StaffCLOTRequest>();


                if (mRequestid > 0)
                {
                    if (dataType_myselfOrMyManage == 1)
                    {
                        ds = BLL.CLOT.GetMyClOTByRequestidUID(loginer.userInfo.u_id, currentBigRange, mRequestid);
                    }
                    else if (dataType_myselfOrMyManage == 0)
                    {
                        ds = BLL.CLOT.GetMyManageClOTByRequestid(loginer.userInfo.u_id, currentBigRange, mRequestid);
                    }

                    if (ds.Count() == 0)
                    {
                        SetupRepeater_NOtExistRequest();
                    }
                }
                else
                {
                    if (dataType_myselfOrMyManage == 1)
                    {
                        ds = BLL.CLOT.GetMyCLOTUID(loginer.userInfo.u_id, currentBigRange, year);
                    }
                    else if (dataType_myselfOrMyManage == 0)
                    {
                        ds = BLL.CLOT.GetMyManageClOT(loginer.userInfo.u_id, currentBigRange, year, name);
                    }
                }

                ds = ds.OrderByDescending(x => x.Date).ThenByDescending(x => x.TimeFrom==null?0:x.TimeFrom.Value.Hour).ToList();
                this.rp_clot.DataSource = ds;
                this.rp_clot.DataBind();
            }
        }

        private void SetupRepeater_NOtExistRequest()
        {
            string message = BLL.MultiLanguageHelper.GetLanguagePacket().Commonfailure;
            this.js_error.Text = LSLibrary.WebAPP.MyJSHelper.AlertMessageAndGoto(message,"main.aspx");
        }


        private void SetupSearchAndTab()
        {
            //year
            List<int> yearRange = BLL.Leave.GetDefaultYearRange();
            for (int i = yearRange[0]; i <= yearRange[1]; i++)
            {
                this.ddl_year.Items.Add(new ListItem(i.ToString(),i.ToString()));
            }
            this.ddl_year.SelectedValue = DateTime.Now.Year.ToString();
            if (mRequestid > 0)
            {
                this.ddl_year.Visible = false;
            }

            //tab
            if (dataType_myselfOrMyManage == 0)//manage
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
            else//applyer.
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
                if (from == 0)
                {
                    this.myTabapply_es.Visible = true;
                }
                else
                {
                    this.myTabapply_es.Visible = false;
                }
            }
            if (mRequestid > 0)
            {
                this.myTabApproval.Visible = false;
                this.myTabApply.Visible = false;
            }


            //staff
            this.tb_staff.SetTip(tip);
            this.tb_staff.Visible = dataType_myselfOrMyManage == 0;
            this.ib_search.Visible = dataType_myselfOrMyManage == 0;
            if (mRequestid > 0)
            {
                this.tb_staff.Visible = false;
                this.ib_search.Visible = false;
            }


            //radioOption
            if (dataType_myselfOrMyManage==1)//myself
            {
                this.rbl_sourceType.Visible = false;
            }
            else
            {
                this.rbl_sourceType.Visible = true;
                var hiddenMenu = BLL.CodeSetting.GetMenu();
                if (hiddenMenu.Contains("1") == true && hiddenMenu.Contains("7") == false)
                {
                    this.rbl_sourceType.Items.RemoveAt(0);
                    this.rbl_sourceType.SelectedIndex = 0;
                }
                else if (hiddenMenu.Contains("1") == false && hiddenMenu.Contains("7") == true)
                {
                    this.rbl_sourceType.Items.RemoveAt(1);
                    this.rbl_sourceType.SelectedIndex = 0;
                }
                else if (hiddenMenu.Contains("1") == true && hiddenMenu.Contains("7") == true)
                {
                    this.rbl_sourceType.Items.Clear();
                }
                else
                {
                    if (mRequestid > 0)
                    {
                        this.rbl_sourceType.SelectedValue = from.ToString();//requestid ,存在那么每次都要重新加载radio.
                        this.rbl_sourceType.Visible = false;
                    }
                    else
                    {
                        this.rbl_sourceType.SelectedValue = getrdlvalue(Session).ToString();//没有request,读session.保证tab，改变也可以存储 clot or leave.
                    }
                    
                }
            }

            //approve batch
            this.div_batchApprovea.Visible = false;
            if (dataType_myselfOrMyManage == 0 && GetBigRange() == GlobalVariate.LeaveBigRangeStatus.waitapproval)
            {
                this.div_batchApprovea.Visible = true;
            }
        }

        #region leave panel
        public bool BShow_BatchApprove(GlobalVariate.LeaveBigRangeStatus myBigRange,  int action)
        {
            bool result = false;

            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval && action == 0)
            {
                result = true;
            }
            return result;
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
        #endregion

        #region clot panel
        public bool BShow_WaitApplyPanel_clot(GlobalVariate.LeaveBigRangeStatus myBigRange, int states, int action)
        {
            bool result = false;
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && action == 0)
            {
                result = true;
            }
            return result;
        }

        public bool BShow_WaitCancelPanel_clot(GlobalVariate.LeaveBigRangeStatus myBigRange, int states, int action)
        {
            bool result = false;
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL && action == 0)
            {
                result = true;
            }
            return result;
        }

        public bool BShow_UserWaitingPanel_clot(GlobalVariate.LeaveBigRangeStatus myBigRange, int states, int action)
        {
            bool result = false;
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE && action == 1)
            {
                result = true;
            }
            return result;
        }

        public bool BShow_UserApprovedPanel_clot(GlobalVariate.LeaveBigRangeStatus myBigRange, int states, int action)
        {
            bool result = false;
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.beyongdWait && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.APPROVE && action == 1)
            {
                result = true;
            }
            return result;
        }
        #endregion


        protected void btn_Click(object sender, EventArgs e)
        {
            string errormsg = "";
            string successMsg = "";
            bool callResult = true;

            string[] pas = ((Button)sender).CommandArgument.Split(new char[] { '|' });

            int btntype = int.Parse(pas[0]);
            int itemIndex = int.Parse(pas[1]);
            int requestId = int.Parse(pas[2]);

            string remarks1 = ((TextBox)this.rp_list.Items[itemIndex].FindControl("panel_admin_waitingApprove").FindControl("tb_waitapproveRemark")).Text;
            string remarks2 = ((TextBox)this.rp_list.Items[itemIndex].FindControl("panel_admin_waitingCancel").FindControl("tb_waitcancelRemark")).Text;


            string waitDiv = LSLibrary.WebAPP.httpHelper.WaitDiv_show(BLL.MultiLanguageHelper.GetLanguagePacket().Commonsubmit_success);
            Response.Write(waitDiv);
            Response.Flush();


            if (btntype == 1)//approve apply
            {
                callResult = BLL.workflow.ApproveRequest_leave(requestId, loginer.userInfo.u_id, remarks1, out errormsg);
                successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproveok);
            }
            else if (btntype == 2)//reject apply
            {
                if (string.IsNullOrEmpty(remarks1))
                {
                    callResult = false;
                    errormsg = BLL.MultiLanguageHelper.GetLanguagePacket().approval_needRemark;
                }
                else
                {

                    callResult = BLL.workflow.RejectRequest_leave(requestId, loginer.userInfo.u_id, remarks1, out errormsg);
                    successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproverej);
                }
            }
            else if (btntype == 3)//approve cancel
            {
                callResult = BLL.workflow.ApprovalCancelRequest_leave(requestId, loginer.userInfo.u_id, remarks2, out errormsg);
                successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproveok);
            }
            else if (btntype == 4)//reject cancel
            {
                callResult = BLL.workflow.RejectCancelRequest_leave(requestId, loginer.userInfo.u_id, remarks2, out errormsg);
                successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproverej);
            }

            else if (btntype == 5)//withdraw
            {
                callResult = BLL.workflow.WithDrawRequest_leave(requestId, loginer.userInfo.u_id, "", out errormsg);
                successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgwithdraw);
            }
            else if (btntype == 6)//cancel
            {
                var theWorkflow = BLL.Leave.GetWorkflowByRequestID(requestId, (int)BLL.GlobalVariate.WorkflowTypeID.LEAVE_APPLICATION);

                if (theWorkflow == null)
                {
                    callResult = false;
                    errormsg = BLL.MultiLanguageHelper.GetLanguagePacket().approvalWaitcannotCancel;
                }
                else
                {
                    int posibalCancelID = BLL.Leave.GetPossibalCancelRequestid(requestId);

                    if (posibalCancelID == 0)
                    {
                        var tempResult = BLL.workflow.CancelRequest_leave(requestId, loginer.userInfo.u_id, "");
                        int rid = tempResult.mResult;
                        callResult = rid <= 0 ? false : true;
                        if (callResult)
                        {
                            successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgcancel);
                        }
                        else
                        {
                            errormsg = tempResult.mMessage;
                        }
                    }
                    else
                    {
                        callResult = false;
                        errormsg = "Can not cancel again";
                    }
                }


            }
            if (mRequestid > 0)
            {
                string url = "?action={0}&applicationtype=3&from={1}";
                url = string.Format(url, dataType_myselfOrMyManage, from);

                if (callResult)
                {
                    //直接跳转，简化逻辑。
                    Response.Flush();
                    System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验
                    this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                    this.lt_jsscrolltop.Text = LSLibrary.WebAPP.MyJSHelper.Goto(url);
                }
                else
                {
                    Response.Flush();
                    System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验
                    this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                    ShowErrorMsgInUI(errormsg);
                }
            }
            else
            {
                SetupRepeater();

                if (callResult)
                {
                    Response.Flush();
                    System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验
                    this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                    this.lt_jsscrolltop.Text = "<script>var vv=getCookie('st'); $('#maindata').scrollTop(vv);</script>";
                }
                else
                {
                    Response.Flush();
                    System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验
                    ShowErrorMsgInUI(errormsg);
                    this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                    this.lt_jsscrolltop.Text = "<script>var vv=getCookie('st'); $('#maindata').scrollTop(vv);</script>";
                }
            }
        }

        private void ShowErrorMsgInUI(string errorMSG)
        {
            lb_errormsg.Text = errorMSG;
            lb_errormsg.Visible = true;
            div_error.Visible = true;
        }

        protected void btn_Click_clot(object sender, EventArgs e)
        {
            string errormsg = "";
            string successMsg = "";
            bool callResult = true;
            string[] pas = ((Button)sender).CommandArgument.Split(new char[] { '|' });

            int btntype = int.Parse(pas[0]);
            int itemIndex = int.Parse(pas[1]);
            int requestId = int.Parse(pas[2]);

            string remarks1 = ((TextBox)this.rp_clot.Items[itemIndex].FindControl("panel_admin_waitingApprove").FindControl("tb_waitapproveRemark")).Text;
            string remarks2 = ((TextBox)this.rp_clot.Items[itemIndex].FindControl("panel_admin_waitingCancel").FindControl("tb_waitcancelRemark")).Text;



            string waitDiv = LSLibrary.WebAPP.httpHelper.WaitDiv_show(BLL.MultiLanguageHelper.GetLanguagePacket().Commonsubmit_success);
            Response.Write(waitDiv);
            Response.Flush();


            if (btntype == 1)//approve apply
            {
                callResult = BLL.workflow.ApproveRequest_leave_clot(requestId, loginer.userInfo.u_id, remarks1, out errormsg);
                successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproveok);
            }
            else if (btntype == 2)//reject apply
            {
                if (string.IsNullOrEmpty(remarks1))
                {
                    callResult = false;
                    errormsg = BLL.MultiLanguageHelper.GetLanguagePacket().approval_needRemark;
                }
                else
                {
                    callResult = BLL.workflow.RejectRequest_leave_clot(requestId, loginer.userInfo.u_id, remarks1, out errormsg);
                    successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproverej);
                }

            }
            else if (btntype == 3)//approve cancel
            {
                callResult = BLL.workflow.ApprovalCancelRequest_leave_clot(requestId, loginer.userInfo.u_id, remarks2, out errormsg);
                successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproveok);
            }
            else if (btntype == 4)//reject cancel
            {
                if (string.IsNullOrEmpty(remarks2))
                {
                    callResult = false;
                    errormsg = BLL.MultiLanguageHelper.GetLanguagePacket().approval_needRemark;
                }
                else
                {
                    callResult = BLL.workflow.RejectCancelRequest_leave_clot(requestId, loginer.userInfo.u_id, remarks2, out errormsg);
                    successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgapproverej);
                }
            }

            else if (btntype == 5)//withdraw
            {
                callResult = BLL.workflow.WithDrawRequest_leave_clot(requestId, loginer.userInfo.u_id, "", out errormsg);
                successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgwithdraw);
            }
            else if (btntype == 6)//cancel
            {
                var theWorkflow = BLL.Leave.GetWorkflowByRequestID(requestId, (int)BLL.GlobalVariate.WorkflowTypeID.CLOT_APPLICATION);

                if (theWorkflow == null)
                {
                    callResult = false;
                    errormsg = BLL.MultiLanguageHelper.GetLanguagePacket().approvalWaitcannotCancel;
                }
                else
                {
                    callResult = BLL.workflow.CancelRequest_leave_clot(requestId, loginer.userInfo.u_id, "", out errormsg);
                    successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_msgcancel);
                }
            }

            if (mRequestid > 0)
            {
                //直接跳转，简化逻辑。
                string url = "?action={0}&applicationtype=3&from={1}";
                url = string.Format(url, dataType_myselfOrMyManage, from);

                if (callResult)
                {
                    Response.Flush();
                    System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验
                    this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                    this.lt_jsscrolltop.Text = LSLibrary.WebAPP.MyJSHelper.Goto(url);
                }
                else
                {
                    Response.Flush();
                    System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验

                    this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                    ShowErrorMsgInUI(errormsg);
                }
                
            }
            else
            {
                SetupRepeater();

                if (callResult)
                {
                    Response.Flush();
                    System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验
                    this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                    this.lt_jsscrolltop.Text = "<script>var vv=getCookie('st'); $('#maindata').scrollTop(vv);</script>";
                }
                else
                {
                    Response.Flush();
                    System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验
                    ShowErrorMsgInUI(errormsg);
                    this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                    this.lt_jsscrolltop.Text = "<script>var vv=getCookie('st'); $('#maindata').scrollTop(vv);</script>";
                }
                
            }
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
            UI_ClearApproveBatch();
        }

        public string GetStaffName(WebServiceLayer.WebReference_leave.LeaveRequestMaster leaveRequestMaster)
        {
            MODEL.UserName tempUser = new MODEL.UserName(leaveRequestMaster.p_Surname, leaveRequestMaster.p_Othername, leaveRequestMaster.p_Nickname, leaveRequestMaster.p_NameCH);
            string tt= tempUser.GetDisplayName(nametype);
            return LSLibrary.StringUtil.SubstringSP(tt, 17, "...");
        }

        public int GetStaffid(WebServiceLayer.WebReference_leave.LeaveRequestMaster leaveRequestMaster)
        {
            int result = 0;
            string extent = leaveRequestMaster.extend;
            string sid= LSLibrary.StringUtil.SplitExtentString(extent, ',', 9);
            if (string.IsNullOrEmpty(sid))
            {
                int.TryParse(sid, out result);
            }
            return result;
        }

        public string GetStaffName(WebServiceLayer.WebReference_leave.StaffCLOTRequest staffclot)
        {
            string surname = "";
            string nextName = "";
            string[] splitname = staffclot.Name.Split(new char[] { ' ' });
            if(splitname!=null && splitname.Count()>=2)
            {
                surname = splitname[0];
                for (int i = 1; i < splitname.Count(); i++)
                {
                    nextName += splitname[i] + " ";
                }
            }
            MODEL.UserName tempUser = new MODEL.UserName(surname, nextName, "", staffclot.NameCH);
            string tt= tempUser.GetDisplayName(nametype);
            return LSLibrary.StringUtil.SubstringSP(tt, 17, "...");
        }

        public string GetLeaveStatus(WebServiceLayer.WebReference_leave.LeaveRequestMaster leaveRequestMaster)
        {
            return BLL.Leave.GetLeaveStatusDesc(leaveRequestMaster.WorkflowTypeID,leaveRequestMaster.Status);
        }

        public string ShowClotStatus(WebServiceLayer.WebReference_leave.StaffCLOTRequest cLOTRequest)
        {
            return BLL.Leave.GetClotStatusDesc(cLOTRequest.Status);
        }

        public string ShowClotName(int type)
        {
            if (type == 0)
            {
                return BLL.MultiLanguageHelper.GetLanguagePacket().Common_label_OT;
            }
            else
            {
                return BLL.MultiLanguageHelper.GetLanguagePacket().Common_label_CL;
            }
        }

        public string showNewLink()
        {
            if (from==0)
            {
                return "window.location.href='apply.aspx'";
            }
            else
            {
                return "window.location.href='applyclot.aspx'";
            }
        }

        


        protected void rbl_sourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            setrdlvalue(Session, int.Parse(this.rbl_sourceType.SelectedValue));
            SetupRepeater();
            UI_ClearApproveBatch();
        }

        protected void rp_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //1get item index. 2 get ref data. 3.get attach data. 4.do something.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //set tr_left tr_right\
                //var trleft = e.Item.FindControl("tr_left");
                //var trright = e.Item.FindControl("tr_right");
                //if (trleft == null || trright == null)
                //{
                //    return;
                //}

                if (e.Item.DataItem is WebServiceLayer.WebReference_leave.LeaveRequestMaster == false)
                {
                    return;
                }

                WebServiceLayer.WebReference_leave.LeaveRequestMaster thedata = (WebServiceLayer.WebReference_leave.LeaveRequestMaster)e.Item.DataItem;

                List<MODEL.App_AttachmentInfo> attachments = BLL.Leave.getAttendanceModel(loginer.loginName, thedata.RequestID, Server, GlobalVariate.AttachType.leave);
                if (attachments == null)
                {
                    return;
                }

                //if (attachments.Count >= 8)
                //{
                //    trleft.Visible = true;
                //    trright.Visible = true;
                //}
                //else
                //{
                //    trleft.Visible = false;
                //    trright.Visible = false;
                //}


                //set attach list
                var ltattachment = e.Item.FindControl("lt_attachlist");
                if (ltattachment == null || ltattachment is Literal == false)
                {
                    return;
                }


                var ltattachment_lt = (Literal)ltattachment;
                ltattachment_lt.Text = BLL.common.GetAttachmentHtml(attachments);

                ////set scroll function for each item.
                //var btnleft = e.Item.FindControl("btnleft");
                //var btnright = e.Item.FindControl("tr_right");
                //var divcontent = e.Item.FindControl("fullf");

                //if (btnleft == null || btnright == null || divcontent == null || trleft.Visible == false || trright.Visible==false)
                //{}
                //else
                //{
                //    string scrollJs = "SetSroll(\"#{0}\", \"#{1}\", \"#{2}\", {3});";
                //    scrollJs = string.Format(scrollJs, btnleft.ClientID, btnright.ClientID, divcontent.ClientID, 40);
                //    lt_jsScroll.Text += scrollJs;
                //}
                rp_clot_list_ItemDataBound_changedivHeight(e, "fullf", attachments.Count);
            }

        }

        private void rp_clot_list_ItemDataBound_changedivHeight(RepeaterItemEventArgs e,string divname,int attachemntcount)
        {
            var divone = e.Item.FindControl(divname);

            if (divone != null)
            {
                if (attachemntcount >= attachmentNewLine)
                {
                    if (divone is System.Web.UI.HtmlControls.HtmlControl)
                    {
                        System.Web.UI.HtmlControls.HtmlControl dd = (System.Web.UI.HtmlControls.HtmlControl)divone;
                        dd.Style.Remove("height");
                        dd.Style.Add("height", "45px");
                    }
                    else
                    {
                        System.Web.UI.HtmlControls.HtmlControl dd = (System.Web.UI.HtmlControls.HtmlControl)divone;
                        dd.Style.Remove("height");
                        dd.Style.Add("height", "30px");
                    }
                }
            }
        }


        protected void rp_clot_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //1get item index. 2 get ref data. 3.get attach data. 4.do something.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.DataItem is WebServiceLayer.WebReference_leave.StaffCLOTRequest == false)
                {
                    return;
                }

                WebServiceLayer.WebReference_leave.StaffCLOTRequest thedata = (WebServiceLayer.WebReference_leave.StaffCLOTRequest)e.Item.DataItem;

                List<MODEL.App_AttachmentInfo> attachments = BLL.Leave.getAttendanceModel(loginer.loginName, thedata.ID, Server, GlobalVariate.AttachType.clot);
                if (attachments == null)
                {
                    return;
                }


                //set attach list
                var ltattachment = e.Item.FindControl("lt_attachlist");
                if (ltattachment == null || ltattachment is Literal == false)
                {
                    return;
                }


                var ltattachment_lt = (Literal)ltattachment;
                ltattachment_lt.Text = BLL.common.GetAttachmentHtml(attachments);

                rp_clot_list_ItemDataBound_changedivHeight(e, "fullf", attachments.Count);
            }
        }

        protected void cb_batch_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_batch.Checked==false)
            {
                List<CheckBox> cbs_leave = UI_GetAllLeaveCheckBox("cb_leave", this.rp_list);
                List<CheckBox> cbs_clot = UI_GetAllLeaveCheckBox("cb_clot", this.rp_clot);

                foreach (CheckBox theItem in cbs_leave)
                {
                    theItem.Checked = false;
                }
                foreach (CheckBox theItem in cbs_clot)
                {
                    theItem.Checked = false;
                }

                this.lb_checkedNumber.Text = "0";
                this.cb_batch.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approvalWait_CLOT_batchCheck;
            }
            else
            {
                List<CheckBox> cbs_leave = UI_GetAllLeaveCheckBox("cb_leave",this.rp_list);
                List<CheckBox> cbs_clot = UI_GetAllLeaveCheckBox("cb_clot",this.rp_clot);

                if (rbl_sourceType.SelectedIndex == 0)
                {
                    foreach (CheckBox theItem in cbs_leave)
                    {
                        theItem.Checked = true;
                    }
                    foreach (CheckBox theItem in cbs_clot)
                    {
                        theItem.Checked = false;
                    }
                    this.lb_checkedNumber.Text = cbs_leave.Count.ToString();
                }
                else
                {
                    foreach (CheckBox theItem in cbs_leave)
                    {
                        theItem.Checked = false;
                    }
                    foreach (CheckBox theItem in cbs_clot)
                    {
                        theItem.Checked = true;
                    }
                    this.lb_checkedNumber.Text = cbs_clot.Count.ToString();
                }

                this.cb_batch.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approvalWait_CLOT_batchUnCheck;
            }
        }

        private void UI_ClearApproveBatch()
        {
            this.cb_batch.Checked = false;
            cb_batch_CheckedChanged(this.cb_batch, new EventArgs());
        }

        private List<CheckBox> UI_GetAllLeaveCheckBox(string checkboxid,Repeater rp)
        {
            List<CheckBox> result = new List<CheckBox>();
            foreach (RepeaterItem theitem in rp.Items)
            {
                CheckBox cb = ((CheckBox)theitem.FindControl(checkboxid));
                if (cb != null)
                {
                    result.Add(cb);
                }
            }
            
            return result;
        }

        protected void cb_leave_CheckedChanged(object sender, EventArgs e)
        {
            cb_itemChanged((CheckBox)sender);
        }

        protected void cb_clot_CheckedChanged(object sender, EventArgs e)
        {
            cb_itemChanged((CheckBox)sender);
        }

        private void cb_itemChanged(CheckBox cb)
        {
            bool checkeda = cb.Checked;
            int numberNow = -1;
            int.TryParse(this.lb_checkedNumber.Text, out numberNow);
            if (numberNow >= 0)
            {
                if (checkeda)
                {
                    numberNow++;
                }
                else
                {
                    numberNow--;
                }
                if (numberNow >= 0)
                {
                    this.lb_checkedNumber.Text = numberNow.ToString();
                }
            }
            this.lt_jsscrolltop.Text = "<script>var vv=getCookie('st'); $('#maindata').scrollTop(vv);</script>";
        }

        protected void btn_batchApprover_Click(object sender, EventArgs e)
        {
            //1.get leave or clot type. 2.get item index-> get selected requestid. 3 approve all  one by one.
            int LeaveOrCLOT = this.rbl_sourceType.SelectedIndex;

            string ErrorMsg = "";

            string waitDiv = LSLibrary.WebAPP.httpHelper.WaitDiv_show(BLL.MultiLanguageHelper.GetLanguagePacket().Commonsubmit_success);
            Response.Write(waitDiv);
            Response.Flush();

            if (LeaveOrCLOT == 0)//leave
            {
                List<int> requestids = new List<int>();
                List<int> statuss = new List<int>();
                UI_GetSelectedLeave(out requestids, out statuss);

                if (requestids != null && statuss != null && requestids.Count == statuss.Count)
                {
                    for (int i = 0; i < requestids.Count; i++)
                    {
                        int requestId = requestids[i];
                        int theStatus = statuss[i];
                        int approveOrCancel = -1;//0 approve 1.approve cancel
                        if (theStatus == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE)
                        {
                            approveOrCancel = 0;
                        }
                        else if (theStatus == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)
                        {
                            approveOrCancel = 1;
                        }

                        string errormsg = "";
                        bool callResult = true;
                        if (approveOrCancel == 0)
                        {
                            callResult = BLL.workflow.ApproveRequest_leave(requestId, loginer.userInfo.u_id, "", out errormsg);
                        }
                        else if (approveOrCancel == 1)
                        {
                            callResult = BLL.workflow.ApprovalCancelRequest_leave(requestId, loginer.userInfo.u_id, "", out errormsg);
                        }
                        if (!callResult)
                        {
                            ErrorMsg += errormsg + "\r\n";
                        }
                    }
                }
            }
            else
            {
                List<int> requestids = new List<int>();
                List<int> statuss = new List<int>();

                UI_GetSelectedCLOT(out requestids, out statuss);

                if (requestids != null && statuss != null && requestids.Count == statuss.Count)
                {
                    for (int i = 0; i < requestids.Count; i++)
                    {
                        int requestId = requestids[i];
                        int theStatus = statuss[i];
                        int approveOrCancel = -1;//0 approve 1.approve cancel

                        if (theStatus == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE)
                        {
                            approveOrCancel = 0;
                        }
                        else if (theStatus == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)
                        {
                            approveOrCancel = 1;
                        }

                        string errormsg = "";
                        bool callResult = true;
                        if (approveOrCancel == 0)
                        {
                            callResult = BLL.workflow.ApproveRequest_leave_clot(requestId, loginer.userInfo.u_id, "", out errormsg);
                        }
                        else if (approveOrCancel == 1)
                        {
                            callResult = BLL.workflow.ApprovalCancelRequest_leave_clot(requestId, loginer.userInfo.u_id, "", out errormsg);
                        }
                        if (!callResult)
                        {
                            ErrorMsg += errormsg + "\r\n";
                        }
                    }
                }
            }

            SetupRepeater();
            UI_ClearApproveBatch();
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                Response.Flush();
                System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验
                this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
            }
            else
            {
                Response.Flush();
                System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验
                ShowErrorMsgInUI(ErrorMsg);
                this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
            }
        }

        private void UI_GetSelectedLeave(out List<int> requestid,out List<int> status)
        {
            requestid = new List<int>();
            status = new List<int>();

            foreach (RepeaterItem theitem in this.rp_list.Items)
            {
                CheckBox cb = ((CheckBox)theitem.FindControl("cb_leave"));
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        HiddenField HFRequestID = ((HiddenField)theitem.FindControl("hf_leave_requestid"));
                        HiddenField HFStatus = ((HiddenField)theitem.FindControl("hf_leave_requeststatus"));

                        int tempid = -1; int tempstatus = -1;
                        int.TryParse(HFRequestID.Value, out tempid);
                        int.TryParse(HFStatus.Value, out tempstatus);

                        if (tempid > 0 && tempstatus > 0)
                        {
                            requestid.Add(tempid);
                            status.Add(tempstatus);
                        }
                    }
                }
            }

        }


        private void UI_GetSelectedCLOT(out List<int> requestid, out List<int> status)
        {
            requestid = new List<int>();
            status = new List<int>();

            foreach (RepeaterItem theitem in this.rp_clot.Items)
            {
                CheckBox cb = ((CheckBox)theitem.FindControl("cb_clot"));
                if (cb != null)
                {
                    if (cb.Checked)
                    {
                        HiddenField HFRequestID = ((HiddenField)theitem.FindControl("hf_clot_requestid"));
                        HiddenField HFStatus = ((HiddenField)theitem.FindControl("hf_clot_requeststatus"));

                        int tempid = -1; int tempstatus = -1;
                        int.TryParse(HFRequestID.Value, out tempid);
                        int.TryParse(HFStatus.Value, out tempstatus);

                        if (tempid > 0 && tempstatus > 0)
                        {
                            requestid.Add(tempid);
                            status.Add(tempstatus);
                        }
                    }
                }
            }
        }


    }
}