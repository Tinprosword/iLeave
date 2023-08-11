using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Article :BLL.CustomLoginTemplate
    {


        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
           
        }

        protected override void InitPage_OnNotFirstLoad2()
        { }

        protected override void PageLoad_InitUIOnNotFirstLoad4()
        { }


        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            string  title = BLL.MultiLanguageHelper.GetLanguagePacket().setting_appinfo;
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, title, "~/pages/setting.aspx", true);
        }


        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {

        }


    }
}