using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Main : AppHelper.CustomLoginTemplate
    {
        protected override void InitPageDataOnEachLoad()
        {
        }

        protected override void InitPageDataOnFirstLoad()
        {
        }

        protected override void InitUIOnFirstLoad()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(false, "", "","");
        }

        protected override void ResetUIOnEachLoad()
        {
        }

        protected void Apply_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/pages/apply.aspx");
        }

        protected void Application_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/pages/myapplications.aspx");
        }

        protected void Approval_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Roster_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Money_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void Tax_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}