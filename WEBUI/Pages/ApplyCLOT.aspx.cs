using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class ApplyCLOT : BLL.CustomLoginTemplate
    {
        

        #region page event
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
        }


        protected override void InitPage_OnFirstLoad2()
        { }

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
        #endregion


        private void MulLanguage()
        {
            
        }

        private void LoadUI()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, BLL.MultiLanguageHelper.GetLanguagePacket().main_applyCLOT, "~/pages/main.aspx", true);
            CLOTTab.SetupControls();
            CLOTTab.showTabActive(0);
        }
    }
}