using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class approvalMain :BLL.CustomLoginTemplate
    {
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        { }

        protected override void InitPage_OnFirstLoad2()
        { }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        { }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, BLL.MultiLanguageHelper.GetLanguagePacket().main_approval, "~/pages/main.aspx", true);
            MultipleLanguage();
        }

        private void MultipleLanguage()
        {
            this.lt_applicationsabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approvalmain_menu2;
            this.lt_applyleaveabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approvalmain_menu1;
            this.lt_approal.Text = BLL.MultiLanguageHelper.GetLanguagePacket().approvalmain_menu3;
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        { }


        protected void wait(object sender, EventArgs e)
        {
            Response.Redirect("approval_wait.aspx?" + approval_wait.qs_bigRange + "=0", true);
        }
        protected void approved(object sender, EventArgs e)
        {
            Response.Redirect("approval_wait.aspx?" + approval_wait.qs_bigRange + "=1", true);
        }
        protected void reject(object sender, EventArgs e)
        {
            Response.Redirect("approval_wait.aspx?" + approval_wait.qs_bigRange + "=2", true);
        }

    }
}