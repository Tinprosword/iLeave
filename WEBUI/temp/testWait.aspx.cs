using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.temp
{
    public partial class testWait : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            initJavascript();
            Response.Flush();

            System.Threading.Thread.Sleep(2000);
            this.Label1.Text = "处理结束";
            //this.ClientScript.RegisterStartupScript(this.GetType(), "0", "remove_loading()", true);
            //this.aa.Text = "<script>var targelem = document.getElementById('loader_container');targelem.style.display='none';targelem.style.visibility='hidden';</script>";
            this.ClientScript.RegisterStartupScript(this.GetType(), "0", "var targelem = document.getElementById('loader_container');targelem.style.display='none';targelem.style.visibility='hidden';", true);
        }


        public static void initJavascript()
        {
            HttpContext.Current.Response.Write("<style>");
            HttpContext.Current.Response.Write("#loader_container {text-align:center; position:absolute; top:40%; width:100%; left: 0;}");
            HttpContext.Current.Response.Write("#loader {font-family:Tahoma, Helvetica, sans; font-size:11.5px; color:#000000; background-color:#FFFFFF; padding:10px 0 16px 0; margin:0 auto; display:block; width:130px; border:1px solid #5a667b; text-align:left; z-index:2;}");
            HttpContext.Current.Response.Write("#progress {height:5px; font-size:1px; width:1px; position:relative; top:1px; left:0px; background-color:#8894a8;}");
            HttpContext.Current.Response.Write("#loader_bg {background-color:#e4e7eb; position:relative; top:8px; left:8px; height:7px; width:113px; font-size:1px;}");
            HttpContext.Current.Response.Write("</style>");
            HttpContext.Current.Response.Write("<div id=loader_container>");
            HttpContext.Current.Response.Write("<div id=loader>");
            HttpContext.Current.Response.Write("<div align=center>页面正在加载中 ...</div>");
            HttpContext.Current.Response.Write("<div id=loader_bg><div id=progress> </div></div>");
            HttpContext.Current.Response.Write("</div></div>");


            HttpContext.Current.Response.Flush();
        }


    }
}