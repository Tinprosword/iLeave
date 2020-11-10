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
        private readonly string tip = "Staff";
        protected GlobalVariate.LeaveBigRangeStatus theBigrange = GlobalVariate.LeaveBigRangeStatus.waitapproval;
        
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString[qs_bigRange]))
            {
                string bigrange = Request.QueryString[qs_bigRange];
                if (bigrange == "0")
                {
                    theBigrange = GlobalVariate.LeaveBigRangeStatus.waitapproval;
                }
                else if (bigrange == "1")
                {
                    theBigrange = GlobalVariate.LeaveBigRangeStatus.approvaled;
                }
                else
                {
                    theBigrange = GlobalVariate.LeaveBigRangeStatus.withdraw;
                }
            }
            else
            {
                Response.Redirect("main.aspx", true);
            }
        }

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {}

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            SetupNavinigation();
            SetupRepeat();
            this.tb_staff.SetTip(tip);
        }

        private void SetupButtonEvent()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {}

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupRepeat();
        }

        protected void tb_staff_TextChanged(object sender, EventArgs e)
        {
            SetupRepeat();
        }

        private void SetupNavinigation()
        {
            string CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().approvalmain_menu1;
            if (theBigrange == GlobalVariate.LeaveBigRangeStatus.approvaled)
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().approvalmain_menu2;
            }
            else if (theBigrange == GlobalVariate.LeaveBigRangeStatus.withdraw)
            {
                CurrentTitle = BLL.MultiLanguageHelper.GetLanguagePacket().approvalmain_menu3;
            }
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, CurrentTitle, "~/pages/approvalmain.aspx", true);
        }


        private void SetupRepeat()
        {
            int year = int.Parse(this.ddl_year.SelectedValue);
            string name = this.tb_staff.Text.Trim() == tip ? "" : this.tb_staff.Text.Trim();
            this.rp_list.DataSource = BLL.Leave.GetMyManageLeaveMaster(loginer.userInfo.id, theBigrange, year, name);
            this.rp_list.DataBind();
        }


        public bool BShow_WaitApplyPanel(GlobalVariate.LeaveBigRangeStatus myBigRange,byte states)
        {
            bool result = false;
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE)
            {
                result = true;
            }
            return result;
        }

        public bool BShow_WaitCancelPanel(GlobalVariate.LeaveBigRangeStatus myBigRange, byte states)
        {
            bool result = false;
            if (myBigRange == GlobalVariate.LeaveBigRangeStatus.waitapproval && states == (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_CANCEL)
            {
                result = true;
            }
            return result;
        }

        public bool BShow_OtherApplyPanel(GlobalVariate.LeaveBigRangeStatus myBigRange)
        {
            bool result = false;
            if (myBigRange != GlobalVariate.LeaveBigRangeStatus.waitapproval)
            {
                result = true;
            }
            return result;
        }


        protected void btn_Click(object sender, EventArgs e)
        {
            string errormsg;
            Button btn = (Button)sender;
            int itemIndex = int.Parse(btn.Attributes["itemIndex"]);
            int btntype = int.Parse(btn.Attributes["btnType"]);

            int requestId = int.Parse(btn.Attributes["requestID"]);
            string remarks1 = ((TextBox)this.rp_list.Items[itemIndex].FindControl("panel_waitingApprove").FindControl("tb_waitapproveRemark")).Text;
            string remarks2 = ((TextBox)this.rp_list.Items[itemIndex].FindControl("panel_waitingCancel").FindControl("tb_waitcancelRemark")).Text;

            if (btntype == 1)//approve apply
            {
                BLL.workflow.ApproveRequest_leave(requestId, loginer.userInfo.id, remarks1, out errormsg);
            }
            else if (btntype == 2)//reject apply
            {
                BLL.workflow.RejectRequest_leave(requestId, loginer.userInfo.id, remarks1, out errormsg);
            }
            else if (btntype == 3)//approve cancel
            {
                BLL.workflow.ApprovalCancelRequest_leave(requestId, loginer.userInfo.id, remarks2, out errormsg);
            }
            else if (btntype == 4)//reject cancel
            {
                BLL.workflow.RejectCancelRequest_leave(requestId, loginer.userInfo.id, remarks2, out errormsg);
            }

            SetupRepeat();
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

    }
}