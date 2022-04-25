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
            ////保存高度到session.
            ////string js = "var height1=document.documentElement.clientHeight;";
            ////js += "var height2=document.documentElement.offsetHeight;";
            ////js += "var height3=document.documentElement.scrollHeight;";
            //string js = "var height=window.screen.availHeight;";
            //js += "var width=window.screen.availWidth;";
            ////js += "var height5=window.screen.height;";
            //js += "var url='../webservices/webservices.aspx?action=saveheight&sc='+height+'&sw='+width;";
            ////js += "alert(width);";
            ////js += "alert(height);";
            ////js += "alert(url);";
            //js += "$.get(url); ";
            //js += "window.location.href='main.aspx';";
            //lt_jsGetheight.Text = "<script>" + js + "</script>";

            Response.Redirect("~/pages/main.aspx");
        }
    }
}