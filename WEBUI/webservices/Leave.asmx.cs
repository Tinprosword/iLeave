using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;

namespace WEBUI.webservices
{
    /// <summary>
    /// Leave 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Leave : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetLeaveDetail_html(int requestID, int leaveid, int staff, int employmentNo,int lan)
        {
            var baseUri = new Uri(HttpContext.Current.Request.Url, "/");
            string postdata = "requestID=" + requestID + "&leaveid=" + leaveid + "&staff=" + staff + "&employmentNo=" + employmentNo+"&lan="+lan;
            string rr = LSLibrary.HttpWebRequestHelper.HttpPost(baseUri + "pages/ajax_historydetail2.aspx", postdata, "");
            return rr;
        }


    }
}