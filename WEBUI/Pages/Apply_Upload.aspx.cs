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


            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "&lt;Apply", "Attachment", "~/pages/Apply.aspx?action=back");
            this.repeater_attandance.DataSource = pics;
            this.repeater_attandance.DataBind();
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //upload pic and save to view state
            string errmsg;
            List<string> uploadFiles = uploadPic(out  errmsg);

            MODEL.Apply.ApplyPage applyPage = (MODEL.Apply.ApplyPage)LSLibrary.WebAPP.PageSessionHelper.GetValue(Apply.Session_pageName);
            
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                string reducePath = "~/" + BLL.GlobalVariate.path_uploadPic + "\\" + BLL.Apply.reducePath + "\\"+uploadFiles[i];
                string bigPath = "~/" + BLL.GlobalVariate.path_uploadPic +  "\\" + uploadFiles[i];

                if (!LSLibrary.FileUtil.FileIsExist( Server.MapPath(reducePath)))
                {
                    reducePath = "~/Res/images/file.png";
                }

                MODEL.Apply.UploadPic temppic= new MODEL.Apply.UploadPic(bigPath,reducePath);
                applyPage.uploadpic.Add(temppic);
            }
            LSLibrary.WebAPP.PageSessionHelper.SetValue(applyPage, Apply.Session_pageName);

            this.repeater_attandance.DataSource = applyPage.uploadpic;
            this.repeater_attandance.DataBind();
        }

        private List<string> uploadPic(out string errorMsg)
        {
            string absoluteDir = Server.MapPath("~/" + BLL.GlobalVariate.path_uploadPic);
            List<string> types = new List<string>(new string[] { "png", "gif" });
            List<string> files = BLL.Apply.UploadAttendance(Request, absoluteDir, types, System.DateTime.Now.ToString("yyyyMMdd"), out errorMsg);
            return files;
        }


        protected void button_apply_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/Apply.aspx?action=back", true);
        }


        protected void imagebutton_close_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton_close = (ImageButton)sender;
            string commandArgument = imageButton_close.CommandArgument;

            MODEL.Apply.ApplyPage applyPage = (MODEL.Apply.ApplyPage)LSLibrary.WebAPP.PageSessionHelper.GetValue(Apply.Session_pageName);
            List<MODEL.Apply.UploadPic> pics = applyPage.uploadpic;
            for (int i = 0; i < pics.Count; i++)
            {
                if (pics[i].tempID == commandArgument)
                {
                    pics.RemoveAt(i);
                }
            }

            this.repeater_attandance.DataSource = pics;
            this.repeater_attandance.DataBind();

            LSLibrary.WebAPP.PageSessionHelper.SetValue(applyPage, Apply.Session_pageName);
        }

    }
}