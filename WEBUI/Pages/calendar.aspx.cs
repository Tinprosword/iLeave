using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class calendar : BLL.CustomLoginTemplate
    {
        private readonly string css_select = "btnBox btnBlueBoxSelect";
        private readonly string css_unselect = "btnBox btnBlueBoxUnSelect";

        protected override void InitPageDataOnEachLoad1()
        {
            
        }

        protected override void InitPageDataOnFirstLoad2()
        {
            
        }

        protected override void ResetUIOnEachLoad3()
        {
            this.Calendar1.DayRender += Calendar1_DayRender;
        }

        protected override void InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "&lt;Home", "Calander", "~/pages/main.aspx");

            TableItemStyle obj = this.Calendar1.SelectedDayStyle;
            obj.BorderStyle = BorderStyle.Solid;
            obj.BorderColor = LSLibrary.MyColors.ParseColor("#bd4f8b");
            obj.BorderWidth = 2;
            obj.BackColor= LSLibrary.MyColors.ParseColor("#ffffff");
            obj.ForeColor= LSLibrary.MyColors.ParseColor("#000000");

            this.Calendar1.SelectedDate = System.DateTime.Now;
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginID,this.Calendar1.SelectedDate);
            this.repeater_leave.DataBind();
        }


        private void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.ToString("yyyy-MM-dd") == "2020-06-19")
            {
                e.Cell.BackColor = System.Drawing.Color.Black;
                e.Cell.ForeColor= LSLibrary.MyColors.ParseColor("#ffffff");
            }
            if (e.Day.Date.ToString("yyyy-MM-dd") == "2020-06-20")
            {
                e.Cell.BackColor = LSLibrary.MyColors.ParseColor("#f3e926");
                e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#ffffff");
            }
            if (e.Day.Date.ToString("yyyy-MM-dd") == "2020-06-21")
            {
                e.Cell.BackColor = LSLibrary.MyColors.ParseColor("#f3e926");
                e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#ffffff");
            }
        }


        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Calendar calendar = (Calendar)sender;
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginID,calendar.SelectedDate);
            this.repeater_leave.DataBind();
        }

        protected void btn_myself_Click(object sender, EventArgs e)
        {
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginID, System.DateTime.Now);
            this.repeater_leave.DataBind();
            this.btn_myself.CssClass = css_select;
            this.btn_team.CssClass = css_unselect;
        }

        protected void btn_team_Click(object sender, EventArgs e)
        {
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginID,  System.DateTime.Now.AddDays(1));
            this.repeater_leave.DataBind();
            this.btn_myself.CssClass = css_unselect;
            this.btn_team.CssClass = css_select;
        }

        protected void cb_leave_CheckedChanged(object sender, EventArgs e)
        {
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginID, System.DateTime.Now.AddDays(1));
            this.repeater_leave.DataBind();
        }

        protected void cb_holiday_CheckedChanged(object sender, EventArgs e)
        {
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginID, System.DateTime.Now.AddDays(1));
            this.repeater_leave.DataBind();
        }
    }
}