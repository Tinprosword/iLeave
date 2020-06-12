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
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "Home", "Calander", "~/pages/main.aspx");
            this.Calendar1.SelectedDate = System.DateTime.Now;
            TableItemStyle obj = this.Calendar1.SelectedDayStyle;
            obj.BorderStyle = BorderStyle.Solid;
            obj.BorderColor = LSLibrary.MyColors.ParseColor("#bd4f8b");
            obj.BorderWidth = 3;
            obj.BackColor= LSLibrary.MyColors.ParseColor("#ffffff");
            obj.ForeColor= LSLibrary.MyColors.ParseColor("#000000");

            int a = 4;
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

    }
}