using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.temp
{
    public partial class testMaster2 : BLL.CustomLoginTemplate
    {
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            
        }

        protected override void InitPage_OnFirstLoad2()
        {
            LSLibrary.WebAPP.ViewStateHelper.SetValue("aa", new MODEL.Apply.ViewState_page(), ViewState);
        }

        

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {

        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            int a = 4;
        }
    }
}