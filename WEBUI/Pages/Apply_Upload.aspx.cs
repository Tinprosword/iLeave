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
        public StateBag myviewState;
        protected override void InitPageVaralbal0()
        {
            WEBUI.Pages.Apply prepage = PreviousPage as WEBUI.Pages.Apply;//todo 查选为什么必须放到第一行,否则会线程中止.
            myviewState = ViewState;
            OnF5Doit = onf5;//回调这里的处理方式，刷新提交就重新载入吧。
        }

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
            WEBUI.Pages.Apply prepage = PreviousPage as WEBUI.Pages.Apply;
            if (prepage != null)
            {
                MODEL.Apply.ViewState_page applyPage2 = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, prepage.myviewState);
                LSLibrary.WebAPP.ViewStateHelper.SetValue(applyPage2, Apply.ViewState_PageName, ViewState);
            }
            else
            {
                Response.Redirect("~/pages/apply.aspx", true);
            }



            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "&lt;Apply", "Attachment", "~/pages/Apply.aspx?action=back");

            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, ViewState);
            this.repeater_attandance.DataSource = applyPage.uploadpic;
            this.repeater_attandance.DataBind();
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            //upload pic and save to view state
            string errmsg;
            List<string> uploadFiles = uploadPic(out errmsg);

            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, ViewState);

            for (int i = 0; i < uploadFiles.Count; i++)
            {
                string reducePath = "~/" + BLL.GlobalVariate.path_uploadPic + "\\" + BLL.Apply.reducePath + "\\" + uploadFiles[i];
                string bigPath = "~/" + BLL.GlobalVariate.path_uploadPic + "\\" + uploadFiles[i];

                if (!LSLibrary.FileUtil.FileIsExist(Server.MapPath(reducePath)))
                {
                    reducePath = "~/Res/images/file.png";
                }

                MODEL.Apply.UploadPic temppic = new MODEL.Apply.UploadPic(bigPath, reducePath);
                applyPage.uploadpic.Add(temppic);
            }
            LSLibrary.WebAPP.ViewStateHelper.SetValue(applyPage, Apply.ViewState_PageName, ViewState);

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

        }


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

            LSLibrary.WebAPP.ViewStateHelper.SetValue(applyPage, Apply.ViewState_PageName, ViewState);
        }


        private void onf5()
        {
            Response.Redirect("~/pages/apply_upload.aspx");
        }

    }
}