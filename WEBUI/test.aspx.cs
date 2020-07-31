using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class test : System.Web.UI.Page
    {
        private string viewname = "tt";
        protected void Page_Load(object sender, EventArgs e)
        {
            var empty= LSLibrary.WebAPP.ViewStateHelper.GetValue(viewname, this.ViewState);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LSLibrary.WebAPP.ViewStateHelper.SetValue("aa", viewname, this.ViewState);
            var empty = LSLibrary.WebAPP.ViewStateHelper.GetValue(viewname, this.ViewState);
        }
    }
}