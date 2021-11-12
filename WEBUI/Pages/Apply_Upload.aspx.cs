﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    //主要用到了session .把数据传递给原来页面。
    public partial class Apply_Upload : BLL.CustomLoginTemplate
    {
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {}

        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

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
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().apply_upload_current, null,true, BackEvent);
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
            List<string> uploadBigFiles = uploadAttachmentAndReduce(bigAbsolutionPath, out errmsg);

            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, ViewState);

            for (int i = 0; i < uploadBigFiles.Count; i++)
            {
                MODEL.Apply.App_AttachmentInfo temppic = BLL.Leave.GenerateAttachmentModel(uploadBigFiles[i], Server);
                applyPage.uploadpic.Add(temppic);
            }
            LSLibrary.WebAPP.ViewStateHelper.SetValue(Apply.ViewState_PageName, applyPage, ViewState);

            this.repeater_attandance.DataSource = applyPage.uploadpic;
            this.repeater_attandance.DataBind();
        }


        private List<string> uploadAttachmentAndReduce(string absolutePath, out string errorMsg)
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

        protected void linkbtn_file_Click(object sender, EventArgs e)
        {
            string filePath = "";
            string augument = "";
            if (sender is LinkButton)
            {
                LinkButton linkButton = (LinkButton)sender;
                filePath = Server.MapPath(linkButton.CommandArgument);
                augument = linkButton.CommandArgument;
            }
            else
            {
                ImageButton imageButton = (ImageButton)sender;
                filePath = Server.MapPath(imageButton.CommandArgument);
                augument = imageButton.CommandArgument;
            }
            
            bool isimage= LSLibrary.FileUtil.IsImagge(System.IO.Path.GetFileName(filePath));

            if (isimage)
            {
                Response.Redirect("showpic2.aspx?path=" + HttpUtility.HtmlEncode(augument));
            }
            else
            {
                Response.Redirect(augument);
            }
        }
    }
}