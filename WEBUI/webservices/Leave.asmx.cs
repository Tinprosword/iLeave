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
            var baseUri = GetWSBaseURI(HttpContext.Current.Request.Url.OriginalString);

            var url = baseUri + "pages/ajax_historydetail2.aspx";
            string postdata = "requestID=" + requestID + "&leaveid=" + leaveid + "&staff=" + staff + "&employmentNo=" + employmentNo+"&lan="+lan;
            string rr = LSLibrary.HttpWebRequestHelper.HttpPost(url, postdata, "");
            return rr;
        }


        [WebMethod]
        public string GetCLOTDetail_html(int requestID, int leaveid, int staff, int employmentNo, int lan)
        {
            var baseUri = GetWSBaseURI(HttpContext.Current.Request.Url.OriginalString);


            var url = baseUri + "pages/ajax_historydetailCLOT.aspx";
            string postdata = "requestID=" + requestID + "&leaveid=" + leaveid + "&staff=" + staff + "&employmentNo=" + employmentNo + "&lan=" + lan;
            string rr = LSLibrary.HttpWebRequestHelper.HttpPost(url, postdata, "");
            return rr;
        }

        [WebMethod]
        public string GetMapLocationName(double lat,double lon)
        {
            string result = "";
            if (lat == 0.1 && lon == 0.1)
            {
                return "Location A";
            }
            else
            {
                string mGoolgeKey = "AIzaSyBct1Ksb5gAqLQMZREgskseovJW6RVYTWs";
                string url_map = "";

                url_map = BLL.Checkin.GetLocationUrl_Google(lat, lon, mGoolgeKey, LSLibrary.WebAPP.LanguageType.tc);
                result = BLL.Checkin.GetAddFromUrl_Google(url_map);
                if (string.IsNullOrEmpty(result))
                {
                    result= BLL.Checkin.GetAddFromUrl_Bingo(url_map);
                }
            }
            return result;
        }

        private string GetWSBaseURI(string url)
        {
            string result = url;

            int index = url.IndexOf("webservices");
            if (index > 0)
            {
                result = url.Substring(0, index);
            }

            return result;
        }


    }
}