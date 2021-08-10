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
        private int dataType = 0;//0 manage  1 myself
        private int dataRange = 0;//0 pending 1.histroy

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["dataType"]))
            {
                string strAction = Request.QueryString["dataType"];
                int.TryParse(strAction, out dataType);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["dataRange"]))
            {
                string strRange = Request.QueryString["dataRange"];
                int.TryParse(strRange, out dataRange);
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
            bool ismanage = dataType == 0 ? true : false;
            CLOTTab.SetupControls(ismanage);
            int index = dataRange == 0 ? 1 : 2;
            CLOTTab.showTabActive(index);
        }


        private void MulLanguage()
        {

        }

    }
}