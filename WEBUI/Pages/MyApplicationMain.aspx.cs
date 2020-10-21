using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class MyApplicationMain : BLL.CustomLoginTemplate
    {
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {}

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        { }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            this.lt_applyleaveabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().mymyapplicationmain_wait;
            this.lt_applicationsabc.Text = BLL.MultiLanguageHelper.GetLanguagePacket().mymyapplicationmain_approved;
            this.lt_approal.Text = BLL.MultiLanguageHelper.GetLanguagePacket().mymyapplicationmain_rejected;
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, BLL.MultiLanguageHelper.GetLanguagePacket().main_approval, "~/pages/main.aspx", true);
        }

        

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {}

      
        protected void reject(object sender, EventArgs e)
        {
            Response.Redirect("myapplications.aspx?applicationType=2", true);
        }


        protected void wait(object sender, EventArgs e)
        {
            Response.Redirect("myapplications.aspx?applicationType=0", true);
        }

        protected void approved(object sender, EventArgs e)
        {
            Response.Redirect("myapplications.aspx?applicationType=1", true);
        }
    }
}