using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class CLOTHistory:BLL.CustomLoginTemplate
    {
        private int action = 0;

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["action"]))
            {
                string strAction = Request.QueryString["action"];
                int.TryParse(strAction, out action);
            }
        }

        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            MulLanguage();
            LoadUI();
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            CLOTTab.SetEvent((WEBUI.Controls.leave)Master);
        }



        private void LoadUI()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().main_CLOTHistory, "~/pages/main.aspx", true);
            CLOTTab.SetupControls();
            int index = action == 0 ? 1 : 2;
            CLOTTab.showTabActive(index);
        }


        private void MulLanguage()
        {

        }

    }
}