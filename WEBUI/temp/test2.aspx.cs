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