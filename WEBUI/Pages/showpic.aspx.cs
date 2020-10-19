using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class showpic : System.Web.UI.Page
    {
        private string picpath;
        protected void Page_Load(object sender, EventArgs e)
        {
            js.Text = "";
            if (!string.IsNullOrEmpty(Request.QueryString["path"]))
            {
                picpath = System.Web.HttpUtility.UrlDecode(Request.QueryString["path"]);
                this.Image1.ImageUrl = picpath;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            js.Text = LSLibrary.WebAPP.MyJSHelper.CloseNewPage();
        }
    }
}