using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Test :  Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Label1.Text = GetHours(3, 1, 1, 1).ToString();
            int a = 4;
        }

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        public float GetHours(int tohour,int fromhour,int tominute,int frommin)
        {
            int h = tohour - fromhour;
            int m = tominute - frommin + 1;
            int totalmin = h * 60 + m;
            return (float)(Math.Round((double)((double)totalmin / 60), 2));
        }

    }
}