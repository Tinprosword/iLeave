using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class shortcut:System.Web.UI.Page
    {
        private string mQSurl_name = "url";
        private string mQSurl_value = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[mQSurl_name]))
            {
                mQSurl_value =  Request.QueryString[mQSurl_name];
                mQSurl_value = HttpUtility.HtmlEncode(mQSurl_value);
            }

            LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> loginer = LSLibrary.WebAPP.LoginManager.GetLoinger<MODEL.UserInfo>();
            if (loginer != null && loginer.userInfo != null)
            {
                Response.Redirect(mQSurl_value, true);
            }
            else
            {
                
            }
        }


        
    }
}