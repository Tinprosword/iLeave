using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Main : LSLibrary.WebAPP.PageTemplate_logined
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

        protected void Apply_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/pages/apply.aspx");
        }

        protected void Application_Click(object sender, ImageClickEventArgs e)
        {

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