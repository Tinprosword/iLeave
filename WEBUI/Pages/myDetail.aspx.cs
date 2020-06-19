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
        protected override void InitPageDataOnEachLoad1()
        {
            
        }

        protected override void InitPageDataOnFirstLoad2()
        {
            
        }

        protected override void InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "&lt;Applications", "Detail", "~/pages/myapplications.aspx");
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
        }

        protected override void ResetUIOnEachLoad3()
        {
            
        }

        protected void button_apply_Click(object sender, EventArgs e)
        {
            this.Label_status.Text = "Cancel";
        }
    }
}