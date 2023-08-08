using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Announcement : BLL.CustomLoginTemplate
    {
        #region page Event
        protected override void InitPage_OnBeforeF5RegisterEvent()
        { }

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
        }

        protected override void InitPage_OnFirstLoad2()
        { }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        { }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_current, "~/pages/main.aspx", true);
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        { }

        #endregion
    }
}