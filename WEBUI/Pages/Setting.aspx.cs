using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Setting : BLL.CustomLoginTemplate
    {
        protected override void InitPageVaralbal0()
        {
        }

        protected override void InitPageDataOnEachLoad1()
        {
        }

        protected override void InitPageDataOnFirstLoad2()
        {
        }

        protected override void ResetUIOnEachLoad3()
        {
        }

        protected override void InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().setting_back, BLL.MultiLanguageHelper.GetLanguagePacket().setting_current, "~/pages/main.aspx");

            LoadLableLanguage();
            this.lb_serveraddress.Text = LSLibrary.WebAPP.CookieHelper.GetCookie(BLL.GlobalVariate.COOKIE_SERVERADDRESS) ?? "";
            string check = LSLibrary.WebAPP.CookieHelper.GetCookie(BLL.GlobalVariate.COOKIE_HTTPS) ?? "";
            this.cb.Checked = check.ToUpper() == "TRUE" ? true : false;
            int intLanguagae = (int)BLL.MultiLanguageHelper.GetChoose();
            this.cb_languagea.SelectedValue = intLanguagae.ToString();
        }


        private void LoadLableLanguage()
        {
            this.lt_address.Text = BLL.MultiLanguageHelper.GetLanguagePacket().setting_service;
            this.lt_language.Text = BLL.MultiLanguageHelper.GetLanguagePacket().setting_language;
            this.lt_language0.Text= BLL.MultiLanguageHelper.GetLanguagePacket().setting_logout;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void cb_languagea_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.User.OnChangeSetting(int.Parse(this.cb_languagea.SelectedValue));
            BLL.MultiLanguageHelper.SaveChoose((LSLibrary.WebAPP.LanguageType)int.Parse(this.cb_languagea.SelectedValue));
            Response.Redirect("~/pages/setting.aspx");
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            LSLibrary.WebAPP.LoginManager.Logoff();
            BLL.User.OnLoginOff();
        }

    }
}