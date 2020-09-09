using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI
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
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().approval_SubMenu_back, BLL.MultiLanguageHelper.GetLanguagePacket().approval_SubMenu_current, "~/pages/main.aspx");
        }


        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        { }


        protected void reject(object sender, EventArgs e)
        {
            Response.Redirect("approval.aspx?applicationType=2", true);
        }


        protected void wait(object sender, EventArgs e)
        {
            Response.Redirect("approval.aspx?applicationType=0", true);
        }

        protected void approved(object sender, EventArgs e)
        {
            Response.Redirect("approval.aspx?applicationType=1", true);
        }
    }
}