using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class myDetail : BLL.CustomLoginTemplate
    {
        private  int requestId;
        private  int uid;
        private  List<MODEL.Apply.LeaveData> LeaveList;
        private  int requestStatus;
        private bool isHandlerOfLeave;

        protected override void InitPageVaralbal0()
        {
        }

        //每次都获取,如果是耗时的数据,那么获取之后放入到viewstatus中,整个程序非全局数据不使用session. 
        //成员变量不要随意使用,不要作为某个函数的临时变量.设定他们就是作为页面的只读填充数据,不要修改!!!!!!!!!!!!!!!!有需要变量,自行在函数中,建立临时变量.
        protected override void InitPageDataOnEachLoad1()
        {
            if (string.IsNullOrEmpty(Request.QueryString["requestid"]) == false)
            {
                requestId = int.Parse(Request.QueryString["requestid"]);
                uid = 16;//todo get it by requestid;
                isHandlerOfLeave = true;//todo get it by requestid.
                LeaveList = BLL.Application.getLeaveDetails(requestId, uid);
                if(LeaveList!=null && LeaveList.Count()>0)
                {
                    requestStatus = LeaveList[0].status;
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
            List<MODEL.Apply.UploadPic> attandance = BLL.Application.getAttendance(loginer.loginName, 3);
            if (attandance != null)
            {
                this.repeater_pic.DataSource = attandance;
                this.repeater_pic.DataBind();
            }
        }

        private void setupLeaveList()
        {
            if (LeaveList != null)
            {
                this.repeater_leave.DataSource = LeaveList;
                this.repeater_leave.DataBind();
            }
        }

        private void setupMainInfo()
        {
            if (LeaveList != null && LeaveList.Count() > 0)
            {
                //todo get request info and user info ,and set value to lables
                this.lb_name.Text = "UserName";
                this.lb_leave.Text = "SL";
                this.lb_status.Text = ((BLL.Application.ApprovalRequestStatus)(requestStatus)).ToString();
            }
        }

        private void SetupButtons()
        {
            this.wait_user.Visible = false;
            this.wait_admin.Visible = false;
            this.wait_useradmin.Visible = false;
            this.withdrawing_admin.Visible = false;
            this.approval_user.Visible = false;

            //根据状态图,按钮组合只有5种情况, 依据2个变量.所以4种情况不算多,可以全列出,用visable来控制.
            if (requestStatus == (int)BLL.Application.ApprovalRequestStatus.WAIT_FOR_APPROVE && uid == loginer.userInfo.id && isHandlerOfLeave == true)
            {
                this.wait_useradmin.Visible = true;
            }
            else if (requestStatus == (int)BLL.Application.ApprovalRequestStatus.WAIT_FOR_APPROVE && uid != loginer.userInfo.id && isHandlerOfLeave == true)
            {
                this.wait_admin.Visible = true;
            }
            else if (requestStatus == (int)BLL.Application.ApprovalRequestStatus.WAIT_FOR_APPROVE && uid == loginer.userInfo.id && isHandlerOfLeave == false)
            {
                this.wait_user.Visible = true;
            }
            else if (requestStatus == (int)BLL.Application.ApprovalRequestStatus.APPROVE && uid == loginer.userInfo.id)
            {
                this.approval_user.Visible = true;
            }
            else if(requestStatus== (int)BLL.Application.ApprovalRequestStatus.WAIT_FOR_CANCEL && isHandlerOfLeave==true)
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
            switch (button.ID)
            {
                case "button_wait_useradmin_cancel":
                    {

                        break;
                    }
                case "button_wait_useradmin_approval":
                    {
                        break;
                    }
                case "button_wait_useradmin_reject":
                    {
                        break;
                    }
                case "button_wait_user_cancel":
                    {
                        break;
                    }
                case "button_wait_admin_approval":
                    {
                        break;
                    }
                case "button_wait_admin_reject":
                    {
                        break;
                    }
                case "button_approval_user_withdraw":
                    {
                        break;
                    }
                case "button_withdrawing_admin_ok":
                    {
                        break;
                    }
                case "button_withdrawing_admin_no":
                    {
                        break;
                    }
            }
        }


    }
}