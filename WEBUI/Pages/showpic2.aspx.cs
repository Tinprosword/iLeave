using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class showpic2 :BLL.CustomLoginTemplate
    {
        private string picpath;



        protected override void InitPage_OnBeforeF5RegisterEvent()
        { }

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        { }

        protected override void InitPage_OnFirstLoad2()
        { }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        { }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["path"]))
            {
                picpath = System.Web.HttpUtility.UrlDecode(Request.QueryString["path"]);
                this.Image1.ImageUrl = picpath;
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["dbpath"]))
            {
                var model = BLL.Leave.GetOneAttendance(picpath, Server);
                this.Image1.ImageUrl = model.bigImageRelatepath;
            }

            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(false, BLL.MultiLanguageHelper.GetLanguagePacket().Back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_current, "~/pages/main.aspx", true,null,true);
        }
    }
}