using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.temp
{
    public partial class testaaa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public string GetWebSiteBaseUrlWithFolderFlag()
        {
            //Return variable declaration
            var appPath = string.Empty;

            //Getting the current context of HTTP request
            var context = HttpContext.Current;

            //Checking the current context content
            if (context != null)
            {
                //Formatting the fully qualified website url/name
                string TempScheme = context.Request.Url.Scheme;
                string TempHost = context.Request.Url.Host;
                string TempPort = context.Request.Url.Port.ToString();
                if (TempScheme.ToUpper() == "HTTP" && TempPort == "80")
                {
                    TempPort = "";
                }
                else if (TempScheme.ToUpper() == "HTTPS" && TempPort == "443")
                {
                    TempPort = "";
                }

                string TempAppPath = context.Request.ApplicationPath;

                appPath = string.Format("{0}://{1}{2}{3}", TempScheme, TempHost, TempPort, TempAppPath);

            }

            if (!appPath.EndsWith("/"))
            {
                appPath += "/";
            }

            return appPath;
        }
        
    }
}