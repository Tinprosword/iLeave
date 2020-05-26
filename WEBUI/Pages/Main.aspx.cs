using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Main : AppLibraly.PageTemplate_logined
    {
        protected override void InitPageDataOnEachLoad()
        {
        }

        protected override void InitUIOnFirstLoad()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(false, "", "","");
        }

        protected override void ResetUIOnEachLoad()
        {
        }

        protected void apply_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/pages/apply.aspx");
        }

        protected void application_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void approval_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void roster_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void money_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void tax_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}