using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Apply_Upload : BLL.CustomLoginTemplate
    {


        protected override void InitPageDataOnEachLoad1()
        {
        }

        protected override void InitPageDataOnFirstLoad2()
        {
        }

        protected override void ResetUIOnEachLoad3()
        {
        }

        protected override void InitUIOnFirstLoad4()
        {
            MODEL.Apply.ApplyPage applyPage = (MODEL.Apply.ApplyPage)LSLibrary.WebAPP.PageSessionHelper.GetValue(Apply.Session_pageName);
            List<MODEL.Apply.UploadPic> pics = applyPage.uploadpic;


            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "Apply", "Attachment", "~/pages/Apply.aspx?action=back");
            this.repeater_attandance.DataSource = pics;
            this.repeater_attandance.DataBind();
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //upload pic and save to view state
            MODEL.Apply.ApplyPage applyPage = (MODEL.Apply.ApplyPage)LSLibrary.WebAPP.PageSessionHelper.GetValue(Apply.Session_pageName);
            applyPage.uploadpic.Add(new MODEL.Apply.UploadPic());
            applyPage.uploadpic.Add(new MODEL.Apply.UploadPic());
            LSLibrary.WebAPP.PageSessionHelper.SetValue(applyPage, Apply.Session_pageName);



            this.repeater_attandance.DataSource = applyPage.uploadpic;
            this.repeater_attandance.DataBind();
        }
    }
}