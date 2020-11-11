using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class getHeight : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //保存高度到session.
            string js = "var height=document.documentElement.clientHeight;";
            //js += "alert(height);";
            js += "var url='../webservices/webservices.aspx?action=saveheight&sc='+height;";
            //js += "alert(url);";
            js += "$.get(url); ";
            js += "window.location.href='main.aspx';";
            lt_jsGetheight.Text = "<script>"+js+"</script>";

        }
    }
}