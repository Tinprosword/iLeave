using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Apply : AppLibraly.PageTemplate_logined
    {
        protected override void InitPageDataOnEachLoad()
        {
        }

        protected override void InitUIOnFirstLoad()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "Home", "Apply","~/pages/main.aspx");
        }

        protected override void ResetUIOnEachLoad()
        {

        }
    }
}