using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WEBUI.temp
{
    public partial class test2 : System.Web.UI.Page
    {

        public class XMLFields
        {
            public string smtpServerAdd;
            public string smtpPort;
            public string smtpSSl;
            public string smtpUserName;
            public string smtpPassword;
            public string smtpSleeptime;
            public string logfile;
            public string emailtfromName;
            public string emailFrom;
            public string isDwsAccount;
            public string isTestMode;

            public XMLFields(string smtpServerAdd, string smtpPort, string smtpSSl, string smtpUserName, string smtpPassword, string smtpSleeptime, string logfile, string emailtfromName, string emailFrom, string isDwsAccount, string isTestMode)
            {
                this.smtpServerAdd = smtpServerAdd;
                this.smtpPort = smtpPort;
                this.smtpSSl = smtpSSl;
                this.smtpUserName = smtpUserName;
                this.smtpPassword = smtpPassword;
                this.smtpSleeptime = smtpSleeptime;
                this.logfile = logfile;
                this.emailtfromName = emailtfromName;
                this.emailFrom = emailFrom;
                this.isDwsAccount = isDwsAccount;
                this.isTestMode = isTestMode;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.Page.MyCookie cookie = BLL.Page.MyCookieManage.GetCookie();
            var isrem = cookie.isRemember;

            cookie.isRemember = "0";
            BLL.Page.MyCookieManage.SetCookie(cookie);
            cookie = BLL.Page.MyCookieManage.GetCookie();
            var isrem2 = cookie.isRemember;

            cookie.isRemember = "1";
            BLL.Page.MyCookieManage.SetCookie(cookie);
            cookie = BLL.Page.MyCookieManage.GetCookie();
            var isrem3 = cookie.isRemember;

            var a = 4;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}