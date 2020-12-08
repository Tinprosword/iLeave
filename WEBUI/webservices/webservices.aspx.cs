using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.SessionState;

namespace WEBUI.webservices
{
    public partial class webservices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Action"]))
                {
                    string Actionname = Request.QueryString["Action"];

                    if (Actionname == "ws")
                    {
                        Response.Clear();

                        Response.Write(getXml(WebServiceLayer.MyWebService.GetDecodeWebServicesAddress()));
                        Response.End();
                    }
                    else
                    {
                        
                    }
                }
                else
                {
                    Response.Clear();
                    Response.Write(getXml(""));
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