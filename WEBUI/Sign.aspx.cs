using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class Sign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GoLogin();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //1.check address 2.decode address 3.keep address to cookie and  response redirect url.
            string address = this.tb_Address.Text.Trim();
            if (address != "")
            {
                //decode
                BLL.Page.MyCookieManage.SetCookie_address(address);
                GoLogin();
            }

        }

        private void GoLogin()
        {
            BLL.Page.MyCookie myCookie = BLL.Page.MyCookieManage.GetCookie();
            if (myCookie.serverAddress != null && myCookie.serverAddress != "")
            {
                string url = "http://" + myCookie.serverAddress + "/login.aspx";
                Response.Redirect(url);
            }
        }
    }
}