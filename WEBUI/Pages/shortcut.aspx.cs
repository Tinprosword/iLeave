using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    //public static string qs_action = "action";//0.my mange data  1.mydata 
    //public static string qs_bigRange = "applicationType";//0:penging. 3:history.
    //public static string qs_from = "from";//0.leave 1.clot 
    //public static string qs_requestid = "requestid";



    //外部链接，从这里开始。未登录分支登录后，又回到这里走登录的分支。 逻辑闭合，代码复用。nice.
    public partial class shortcut:System.Web.UI.Page
    {
        //url样式：approval_wait.aspx?action=0&applicationtype=0&from=0&requestid=35218
        public static string mQSurl_name = "url";
        private string mQSurl_value = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString[mQSurl_name]))
            {
                mQSurl_value =  Request.QueryString[mQSurl_name];

                LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> loginer = LSLibrary.WebAPP.LoginManager.GetLoinger<MODEL.UserInfo>();
                if (loginer != null && loginer.userInfo != null)
                {
                    Response.Redirect(mQSurl_value, true);
                }
                else
                {
                    Response.Redirect("../login.aspx?action=shortcut&url=" + HttpUtility.UrlEncode(mQSurl_value));
                }
            }
        }
    }
}