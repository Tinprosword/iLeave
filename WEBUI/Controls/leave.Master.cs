using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Controls
{
    public partial class leave : System.Web.UI.MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.appcss.Href += "?lastmodify=" + BLL.GlobalVariate.appcssLastmodify;
            }
        }

        public void SetupNaviagtion(bool isVisitable,string backLink,string title,string url,bool showback, ImageClickEventHandler ClickEvent=null)
        {
            this.Navigation.Visible = isVisitable;
            
            this.label_title.Text = title;
            if (string.IsNullOrEmpty(url))
            {this.ib_back.Click += ClickEvent;}
            else
            {this.ib_back.PostBackUrl = url;}

            this.ib_back.Visible = showback;
        }

        public string GetMyPostBackArgumentByTargetname(string targetName)
        {
            string result = null;
            if (!string.IsNullOrEmpty(Request.Form["mypostback_target"]) && Request.Form["mypostback_target"] == targetName)
            {
                result = Request.Form["mypostback_argument"];
            }
            return result;
        }

    }
}