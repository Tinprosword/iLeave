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
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {}

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {}

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            object PreViewstate = LSLibrary.WebAPP.PageSessionHelper.GetValueAndCleanSoon(BLL.GlobalVariate.Session_ApplyToUpload);
            if (PreViewstate != null)
            {
                LSLibrary.WebAPP.ViewStateHelper.SetValue( Apply.ViewState_PageName, PreViewstate, ViewState);
            }
            else
            {
                Response.Redirect("~/pages/apply.aspx", true);
            }

            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, ViewState);
            this.repeater_attandance.DataSource = applyPage.uploadpic;
            this.repeater_attandance.DataBind();
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_upload_back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_upload_current, null,true, BackEvent);
        }


        private void BackEvent(object sender, ImageClickEventArgs e)
        {
            object myViewState = LSLibrary.WebAPP.ViewStateHelper.GetValue(WEBUI.Pages.Apply.ViewState_PageName,this.ViewState);
            LSLibrary.WebAPP.PageSessionHelper.SetValue(myViewState, BLL.GlobalVariate.Session_UploadToApply);
            Response.Redirect("~/pages/Apply.aspx?action=back", true);
        }

        protected void Upload_Click(object sender, ImageClickEventArgs e)
        {
            //upload pic and save to view state
            string bigAbsolutionPath = Server.MapPath("../" + BLL.Leave.picPath);

            string errmsg;
            List<string> uploadBigFiles = uploadPicAndReduce(bigAbsolutionPath, out errmsg);

            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, ViewState);

            for (int i = 0; i < uploadBigFiles.Count; i++)
            {
                MODEL.Apply.app_uploadpic temppic = BLL.Leave.GeneratePicModel(uploadBigFiles[i], Server);
                applyPage.uploadpic.Add(temppic);
            }
            LSLibrary.WebAPP.ViewStateHelper.SetValue(Apply.ViewState_PageName, applyPage, ViewState);

            this.repeater_attandance.DataSource = applyPage.uploadpic;
            this.repeater_attandance.DataBind();
        }


        private List<string> uploadPicAndReduce(string absolutePath, out string errorMsg)
        {
            List<string> types = null;//all format is ok.
            List<string> files = BLL.common.UploadAttendanceAndReduce(Request, absolutePath, types, System.DateTime.Now.ToString("yyyyMMdd"), out errorMsg);
            return files;
        }


        protected void button_apply_Click(object sender, EventArgs e)
        {}

        protected void imagebutton_close_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton_close = (ImageButton)sender;
            string commandArgument = imageButton_close.CommandArgument;

            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, ViewState);

            for (int i = 0; i < applyPage.uploadpic.Count; i++)
            {
                if (applyPage.uploadpic[i].tempID == commandArgument)
                {
                    applyPage.uploadpic.RemoveAt(i);
                }
            }

            this.repeater_attandance.DataSource = applyPage.uploadpic;
            this.repeater_attandance.DataBind();

            LSLibrary.WebAPP.ViewStateHelper.SetValue(Apply.ViewState_PageName, applyPage, ViewState);
        }

        private void onf5()
        {
            Response.Redirect("~/pages/apply_upload.aspx");
        }

        protected void button_apply_Click1(object sender, EventArgs e)
        {
            BackEvent(sender, null);
        }
    }
}