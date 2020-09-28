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
            string address = this.tb_Address.Text.Trim();
            if (address != "")
            {
                BLL.Page.MyCookieManage.SetCookie_address(address);
                GoLogin();
            }
        }

        private void GoLogin()
        {
            BLL.Page.MyCookie myCookie = BLL.Page.MyCookieManage.GetCookie();
            if (myCookie.serverAddress != null && myCookie.serverAddress != "")
            {
                Response.Redirect("login.aspx");
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