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
        private readonly string css_select = "btnBox btnBlueBoxSelect";
        private readonly string css_unselect = "btnBox btnBlueBoxUnSelect";


        protected override void InitPageVaralbal0()
        {
        }

        protected override void InitPageDataOnEachLoad1()
        {
        }

        protected override void InitPageDataOnFirstLoad2()
        {
            this.btn_approved.CssClass = css_unselect;
            this.btn_wait.CssClass = css_select;
            this.btn_rejectWith.CssClass = css_unselect;
        }

        protected override void ResetUIOnEachLoad3()
        { 
        }

        protected override void InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().application_back, BLL.MultiLanguageHelper.GetLanguagePacket().application_current, "~/pages/main.aspx");
            SetupMultiLanguage();

            this.repeater_myapplications.DataSource = GetDatasource(this.RadioButton1.Checked, getStatus(), loginer.userInfo.id, this.tb_date.Text, this.tb_name.Text);
            this.repeater_myapplications.DataBind();
        }


        private void SetupMultiLanguage()
        {
            this.btn_approved.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_approved;
            this.btn_rejectWith.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_rejected;
            this.btn_wait.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_wait;
            this.lt_name.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_name;
            this.ltdatefrom.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_datefrom;
        }


        protected void btn_wait_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_unselect;
            this.btn_wait.CssClass = css_select;
            this.btn_rejectWith.CssClass = css_unselect;
            this.repeater_myapplications.DataSource = GetDatasource(this.RadioButton1.Checked, getStatus(), loginer.userInfo.id, this.tb_date.Text, this.tb_name.Text);
            this.repeater_myapplications.DataBind();
        }


        protected void btn_approved_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_select;
            this.btn_wait.CssClass = css_unselect;
            this.btn_rejectWith.CssClass = css_unselect;


            this.repeater_myapplications.DataSource = GetDatasource(this.RadioButton1.Checked, getStatus(), loginer.userInfo.id, this.tb_date.Text, this.tb_name.Text);
            this.repeater_myapplications.DataBind();
        }

        protected void btn_rejectWith_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_unselect;
            this.btn_wait.CssClass = css_unselect;
            this.btn_rejectWith.CssClass = css_select;

            this.repeater_myapplications.DataSource = GetDatasource(this.RadioButton1.Checked, getStatus(), loginer.userInfo.id, this.tb_date.Text, this.tb_name.Text);
            this.repeater_myapplications.DataBind();
        }

        protected void lb_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            string requestid = link.CommandArgument;
            Response.Redirect("~/Pages/myDetail.aspx?requestid=" + requestid, true);
        }

        protected void tb_name_TextChanged(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource(this.RadioButton1.Checked, getStatus(), loginer.userInfo.id, this.tb_date.Text, this.tb_name.Text);
            this.repeater_myapplications.DataBind();
        }

        protected void tb_date_TextChanged1(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource(this.RadioButton1.Checked, getStatus(), loginer.userInfo.id, this.tb_date.Text, this.tb_name.Text);
            this.repeater_myapplications.DataBind();
        }

        //0 wait 1.approva 2.reject.
        private int getStatus()
        {
            int result = 0;
            if (this.btn_approved.CssClass == css_select)
            {
                result = 1;
            }
            else if (this.btn_wait.CssClass == css_select)
            {
                result = 0;
            }
            else
            {
                result = 2;
            }

            return result;
        }


        //chooseStatus:1 ,apporve ,0 wait .2 reject
        private static List<DAL.WebReference_leave.LeaveRequestMaster> GetDatasource(bool ismyself, int chooseStatus,int uid, string datestrFrom,string username)
        {
            List<DAL.WebReference_leave.LeaveRequestMaster> result = null;
            DateTime? dateFrom = null;
            if (!string.IsNullOrEmpty(datestrFrom))
            {
                dateFrom = DateTime.Parse(datestrFrom);
            }

            if (ismyself)
            {
                //根据uid,需要的状态 , date, 得到list.
            
                result= BLL.Leave.GetLeaveMaster(uid, chooseStatus, dateFrom);
            }
            else
            {
                //根据uid,时间，name ，获得我所负责的 wait approval 's list.

                result = BLL.Leave.GetLeaveMaster_IncludeMyWorkFlow(uid, dateFrom);
            }
            return result;
        }


    }
}