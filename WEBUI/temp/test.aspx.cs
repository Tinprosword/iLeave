using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //leavebase leavebase = new leavebase();

            //this.Label1.Text = leavebase.add(1, 3).ToString();
            //getbaseinfo();
            //showsite();

            DateTime dt = new DateTime(2021, 12, 31).AddMonths(-1);
            int a = 4;
        }

        private void showsite()
        {
            this.Label1.Text = BLL.Other.GetWebSiteRootUrl(Request);
        }

        private void getbaseinfo()
        {
            string url = "http://localhost:80/WEBUI/webservices/leave.asmx/GetCLOTDetail_html";

            int index= url.IndexOf("webservices");
            if (index > 0)
            {
                url = url.Substring(0, index);
            }

            if (url == "http://localhost:80/WEBUI/")
            {
                this.Label1.Text = "ok" + url;
            }
            else
            {
                this.Label1.Text = "error" + url;
            }
        }


    }



    public class leavebase
    {
        public int add(int a, int b)
        {
            leaveAdv leaveAdv = new leaveAdv();

            return leaveAdv.addAdv(a, b);
        }


    }

    public class leaveAdv
    {
        public leavebase lb = new leavebase();

        public int addAdv(int a, int b)
        {
            int res = lb.add(a, b);
            return res * 2;
        }

        public int addSingle(int a, int b)
        {
            return (a + b) *3;
        }
    }
}