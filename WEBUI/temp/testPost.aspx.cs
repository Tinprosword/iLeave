using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class testPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var listabc = BLL.Other.GetHRWebSiteRootUrl();
            Response.Write(listabc);
        }

        public void POST()
        {
            int a = 0, b = 0, c = 0;
            string postData = null;
            System.Net.HttpWebRequest request = default(System.Net.HttpWebRequest);
            System.IO.Stream requestStream = default(System.IO.Stream);
            byte[] postBytes = null;
            //封装参数  
            postData = "a=" + a + "&b=" + b + "&c=" + c;
            string url = "http://" + Request.Url.Authority + "/WebForm1.aspx";
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.Timeout = 10000;
            request.Method = "POST";
            request.AllowAutoRedirect = false;

            requestStream = request.GetRequestStream();
            postBytes = System.Text.Encoding.ASCII.GetBytes(postData.ToString());

            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
        }
    }


    
}