using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class myapplications:BLL.CustomLoginTemplate
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

            this.repeater_myapplications.DataSource = new string[3];
            this.repeater_myapplications.DataBind();
        }

        protected override void ResetUIOnEachLoad3()
        {

        }

        protected override void InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().application_back , BLL.MultiLanguageHelper.GetLanguagePacket().application_current, "~/pages/main.aspx");
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

            this.repeater_myapplications.DataSource = new string[3];
            this.repeater_myapplications.DataBind();
        }

        protected void btn_approved_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_select;
            this.btn_wait.CssClass = css_unselect;
            this.btn_rejectWith.CssClass = css_unselect;

            this.repeater_myapplications.DataSource = new string[2];
            this.repeater_myapplications.DataBind();
        }

        protected void btn_rejectWith_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_unselect;
            this.btn_wait.CssClass = css_unselect;
            this.btn_rejectWith.CssClass = css_select;

            this.repeater_myapplications.DataSource = new string[20];
            this.repeater_myapplications.DataBind();
        }

        protected void lb_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/myDetail.aspx?appid=1", true);
        }

        protected void tb_date_TextChanged(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = new string[6];
            this.repeater_myapplications.DataBind();
        }

        protected void tb_name_TextChanged(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = new string[this.tb_name.Text.Length];
            this.repeater_myapplications.DataBind();
        }

        protected void tb_date_TextChanged1(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = new string[3];
            this.repeater_myapplications.DataBind();
        }
    }
}