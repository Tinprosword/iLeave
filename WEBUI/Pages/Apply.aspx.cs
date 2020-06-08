using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Apply : BLL.CustomLoginTemplate
    {
        #region [page event]
        protected override void InitPageDataOnEachLoad1()
        {
            
        }

        protected override void InitPageDataOnFirstLoad2()
        {
            LSLibrary.WebAPP.PageSessionHelper.CleanValue(BLL.Apply.SESSION_DATELIST);
            LSLibrary.WebAPP.PageSessionHelper.SetValue(new List<BLL.Apply.LeaveData>(), BLL.Apply.SESSION_DATELIST);
        }

        protected override void ResetUIOnEachLoad3()
        {
            this.lt_AlertJS.Text = "";
            
        }

        protected override void InitUIOnFirstLoad4()
        {
            this.literal_applier.Text = loginer.loginID + "  " + loginer.userInfo.nickname;
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "Home", "Apply", "~/pages/main.aspx");
        }
        #endregion

        #region [module] upload pic

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            //save session
            //reponse.redirct("abc.aspx");
        }

        //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        //{
        //    this.lt_model_upload.Text = LSLibrary.JavasScriptHelper.CustomJS("$('#modal_upload').modal('show')");
        //}

        //protected void btn_closemodel_ServerClick(object sender, EventArgs e)
        //{
        //    this.lt_model_upload.Text = LSLibrary.JavasScriptHelper.CustomJS("$('#modal_upload').modal('hide')");
        //}

        //protected void btn_uploadpic_ServerClick(object sender, EventArgs e)
        //{
        //    //uplodad pics and update pic list
        //    List<string> picsPath = new List<string>();

        //    uploadPic();
        //    this.lt_model_upload.Text= LSLibrary.JavasScriptHelper.CustomJS("$('#modal_upload').modal('hide')");
        //}

        //private void uploadPic()
        //{
        //    string absoluteDir = Server.MapPath("~/" + BLL.GlobalVariate.path_uploadPic);

        //    List<string> types = new List<string>(new string[] { "png", "gif" });
        //    string errorMsg;

        //    List<string> files = LSLibrary.UploadFile.SaveFiles(Request, absoluteDir, types, System.DateTime.Now.ToString("yyyyMMdd"), out errorMsg);

        //    foreach (string file in files)
        //    {
        //        string imagepath = "~/" + BLL.GlobalVariate.path_uploadPic + "/" + file;
        //        uploadPicCache.Add(new UploadPic(imagepath));
        //    }

        //    this.repeater_pic.DataSource = uploadPicCache;
        //    this.repeater_pic.DataBind();
        //    LSLibrary.WebAPP.PageSessionHelper.SetValue(uploadPicCache, SESSION_UPLOADPIC);

        //    if (string.IsNullOrWhiteSpace(errorMsg) == false)
        //    {
        //        this.lt_AlertJS.Text = LSLibrary.JavasScriptHelper.AlertMessage(errorMsg);
        //    }
        //}

        //protected void btn_close_Click(object sender, ImageClickEventArgs e)
        //{
        //    ImageButton senderObj = (ImageButton)sender;
        //    string strIndex = senderObj.CommandArgument;
        //    int intIndex = int.Parse(strIndex);
        //    if (intIndex <= uploadPicCache.Count - 1)
        //    {
        //        uploadPicCache.RemoveAt(intIndex);
        //        LSLibrary.WebAPP.PageSessionHelper.SetValue(uploadPicCache, SESSION_UPLOADPIC);

        //        this.repeater_pic.DataSource = uploadPicCache;
        //        this.repeater_pic.DataBind();
        //    }
        //}
        #endregion

        #region [module] leave
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            List<BLL.Apply.LeaveData> datesCache = (List<BLL.Apply.LeaveData>)LSLibrary.WebAPP.PageSessionHelper.GetValue(BLL.Apply.SESSION_DATELIST);
            datesCache.AddRange(getListSource(DateTime.Now, DateTime.Now));
            LSLibrary.WebAPP.PageSessionHelper.SetValue(datesCache, BLL.Apply.SESSION_DATELIST);
            this.repeater_leave.DataSource = datesCache;
            this.repeater_leave.DataBind();
        }

        protected void delete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton senderObj = (ImageButton)sender;
            string strIndex = senderObj.CommandArgument;
            int intIndex = int.Parse(strIndex);

            List<BLL.Apply.LeaveData> datesCache = (List<BLL.Apply.LeaveData>)LSLibrary.WebAPP.PageSessionHelper.GetValue(BLL.Apply.SESSION_DATELIST);
            datesCache.RemoveAt(intIndex);
            LSLibrary.WebAPP.PageSessionHelper.SetValue(datesCache, BLL.Apply.SESSION_DATELIST);

            this.repeater_leave.DataSource = datesCache;
            this.repeater_leave.DataBind();
        }
        #endregion

        #region [module] apply
        protected void button_apply_Click(object sender, EventArgs e)
        {
            LSLibrary.WebAPP.PageSessionHelper.CleanValue(BLL.Apply.SESSION_DATELIST);
            Response.Redirect("~/pages/main.aspx");
        }
        #endregion

        #region [common function]
        private List<BLL.Apply.LeaveData> getListSource(DateTime from, DateTime to)
        {
            List<BLL.Apply.LeaveData> data = new List<BLL.Apply.LeaveData>();
            for (int i = 0; i < 25; i++)
            {
                data.Add(new BLL.Apply.LeaveData("05-01周一", "AL", "FULL DAY", 0));
                data.Add(new BLL.Apply.LeaveData("05-02周二", "AL", "FULL DAY", 0));
                data.Add(new BLL.Apply.LeaveData("05-03周三", "AL", "FULL DAY", 0));
                data.Add(new BLL.Apply.LeaveData("05-04周四", "AL", "FULL DAY", 0));
                data.Add(new BLL.Apply.LeaveData("05-05周五", "AL", "FULL DAY", 0));
            }
            return data;
        }
        #endregion
    }
}