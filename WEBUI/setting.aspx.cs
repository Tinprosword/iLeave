using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
{
    public partial class setting : LSLibrary.WebAPP.PageTemplate_Common
    {
        protected override void InitPage_OnEachLoadBeforeF5_1()
        {}

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_ResetUIOnEachLoad3()
        {}

        protected override void PageLoad_ResetUIOnEachLoad5()
        {}

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            LoadLableLanguage();
            this.tb_address.Text= LSLibrary.WebAPP.CookieHelper.GetCookie(BLL.GlobalVariate.COOKIE_SERVERADDRESS) ?? "";
            string check = LSLibrary.WebAPP.CookieHelper.GetCookie(BLL.GlobalVariate.COOKIE_HTTPS) ?? "";


            int intLanguagae = (int)BLL.MultiLanguageHelper.GetChoose();

            this.cb_languagea.SelectedValue = intLanguagae.ToString();
        }


        private void LoadLableLanguage()
        {
            this.lt_address.Text = BLL.MultiLanguageHelper.GetLanguagePacket().setting_service;
            this.lt_language.Text = BLL.MultiLanguageHelper.GetLanguagePacket().setting_language;
            this.Button1.Text = BLL.MultiLanguageHelper.GetLanguagePacket().setting_btn_ok;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            LSLibrary.WebAPP.CookieHelper.SetCookie(BLL.GlobalVariate.COOKIE_SERVERADDRESS, this.tb_address.Text,3600);
            BLL.MultiLanguageHelper.SaveChoose((LSLibrary.WebAPP.LanguageType) int.Parse(this.cb_languagea.SelectedValue));
            Response.Redirect("~/login.aspx");
        }

    }
}