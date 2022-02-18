using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Article_pripary : BLL.CustomLoginTemplate
    {


        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {

        }



        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            string title = BLL.MultiLanguageHelper.GetLanguagePacket().setting_appprivary;
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, title, "~/pages/setting.aspx", true);

            var language = BLL.MultiLanguageHelper.GetChoose();


            this.content_eng.Visible = false;
            this.content_cn.Visible = false;
            this.content_tran.Visible = false;
            if (language==LSLibrary.WebAPP.LanguageType.english)
            {
                this.content_eng.Visible = true;
            }
            else if (language == LSLibrary.WebAPP.LanguageType.sc)
            {
                this.content_cn.Visible = true;
            }
            else if (language == LSLibrary.WebAPP.LanguageType.tc)
            {
                this.content_tran.Visible = true;
            }
        }


        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {

        }

    }
}