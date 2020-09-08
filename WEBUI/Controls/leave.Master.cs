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
        public void SetupNaviagtion(bool isVisitable,string backLink,string title,string url)
        {
            this.Navigation.Visible = isVisitable;
            this.lable_navigation.Text = backLink;
            this.label_title.Text = title;
            this.linkNavigation.PostBackUrl = url;
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