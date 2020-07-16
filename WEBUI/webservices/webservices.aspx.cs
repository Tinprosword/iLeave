using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace WEBUI.webservices
{
    public partial class webservices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["action"]== "ws")
                {
                    Response.Clear();
                    string wsport = getXml(LSLibrary.WebAPP.WebConfig.getValue("webServices"));
                    Response.Write(wsport);
                }
                else
                {
                    Response.Clear();
                    Response.Write("");
                    Response.End();
                }
            }
        }

        public string getXml(string a)
        {
            return "<result>"+a+"</result>";
        }
    }
}