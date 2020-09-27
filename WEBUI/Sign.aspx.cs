using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class Sign  :BLL.CustomCommonTemplate
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
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
                //todo decode ,cooike: password
                BLL.Page.MyCookieManage.SetCookie_address(address);
                Response.Redirect("Sign.aspx",true);
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

        protected override void InitPage_OnBeforeF5RegisterEvent()
        {}

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {}

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {}

        protected override void PageLoad_InitUIOnFirstLoad4()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {}

    }
}