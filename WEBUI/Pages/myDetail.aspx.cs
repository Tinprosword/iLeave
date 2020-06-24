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

        protected override void InitPageVaralbal0()
        {
        }

        protected override void InitPageDataOnEachLoad1()
        {
        }

        protected override void InitPageDataOnFirstLoad2()
        {
        }

        protected override void ResetUIOnEachLoad3()
        {
        }

        protected override void InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().application_detailback, BLL.MultiLanguageHelper.GetLanguagePacket().application_detailcurrent, "~/pages/myapplications.aspx");
            List<MODEL.Apply.LeaveData> LeaveList = BLL.Application.getListSource(loginer.loginID, 3);
            if (LeaveList != null)
            {
                this.repeater_leave.DataSource = LeaveList;
                this.repeater_leave.DataBind();
            }

            List<MODEL.Apply.UploadPic> attandance = BLL.Application.getAttendance(loginer.loginID, 3);
            if (attandance != null)
            {
                this.repeater_pic.DataSource = attandance;
                this.repeater_pic.DataBind();
            }

            SetupMultiLanguage();
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
            this.lt_status.Text = "Cancel";
        }

    }
}