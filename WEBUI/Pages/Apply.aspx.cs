﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Apply :AppHelper.CustomLoginTemplate
    {
        private List<LeaveData> datesCache;
        private List<UploadPic> uploadPicCache;//

        private string SESSION_DATELIST = "DATE";
        private string SESSION_UPLOADPIC = "PIC";

        protected override void InitPageDataOnEachLoad()
        {
            datesCache = (List<LeaveData>)LSLibrary.WebAPP.PageSessionHelper.GetValue(SESSION_DATELIST);
            uploadPicCache= (List<UploadPic>)LSLibrary.WebAPP.PageSessionHelper.GetValue(SESSION_UPLOADPIC);
        }

        protected override void InitPageDataOnFirstLoad()
        {
            LSLibrary.WebAPP.PageSessionHelper.CleanValue(SESSION_DATELIST);
            LSLibrary.WebAPP.PageSessionHelper.SetValue(new List<LeaveData>(), SESSION_DATELIST);
            LSLibrary.WebAPP.PageSessionHelper.CleanValue(SESSION_UPLOADPIC);
            LSLibrary.WebAPP.PageSessionHelper.SetValue(new List<UploadPic>(), SESSION_UPLOADPIC);
        }

        protected override void ResetUIOnEachLoad()
        {
            this.lt_AlertJS.Text = "";
        }

        protected override void InitUIOnFirstLoad()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, "Home", "Apply", "~/pages/main.aspx");
            this.literal_applier.Text = loginer.loginID + "  " + loginer.userInfo.nickname;
            this.tb_from.Attributes["onclick"] += ClientScript.GetPostBackEventReference(btn_from, null);
            this.tb_to.Attributes["onclick"] += ClientScript.GetPostBackEventReference(btn_to, null);
        }

        

        protected void button_uploadpic_Click(object sender, EventArgs e)
        {
            this.lt_model_upload.Text = LSLibrary.JavasScriptHelper.CustomJS("$('#modal_upload').modal('show')");
        }

        protected void btn_closemodel_ServerClick(object sender, EventArgs e)
        {
            this.lt_model_upload.Text = LSLibrary.JavasScriptHelper.CustomJS("$('#modal_upload').modal('hide')");
        }

        protected void btn_uploadpic_ServerClick(object sender, EventArgs e)
        {
            //uplodad pics and update pic list
            List<string> picsPath = new List<string>();

            uploadPic();
            this.lt_model_upload.Text= LSLibrary.JavasScriptHelper.CustomJS("$('#modal_upload').modal('hide')");
        }

        protected void btn_from_Click(object sender, EventArgs e)
        {
            this.lt_model_datafrom.Text = LSLibrary.JavasScriptHelper.CustomJS("$('#modal_calendarfrom').modal('show')");
        }

        protected void btn_to_Click(object sender, EventArgs e)
        {
            this.lt_model_datato.Text = LSLibrary.JavasScriptHelper.CustomJS("$('#modal_calendarto').modal('show')");
        }

        protected void calendar_SelectionChanged(object sender, EventArgs e)
        {
            this.tb_from.Text = this.calendarfrom.SelectedDate.ToString("yyyy-MM-dd");
            this.lt_model_datafrom.Text = LSLibrary.JavasScriptHelper.CustomJS("$('#modal_calendarfrom').modal('hide')");
        }

        protected void calendar_SelectionChanged2(object sender, EventArgs e)
        {
            this.tb_to.Text = this.calendarto.SelectedDate.ToString("yyyy-MM-dd");
            this.lt_model_datato.Text = LSLibrary.JavasScriptHelper.CustomJS("$('#modal_calendarto').modal('hide')");
            this.literal_balance.Text = BLL.DataDemo.GetBalance(this.tb_to.Text)+" as of "+tb_to.Text;
        }

        protected void button_addleave_Click(object sender, EventArgs e)
        {
            datesCache.AddRange(getListSource(DateTime.Now, DateTime.Now));
            LSLibrary.WebAPP.PageSessionHelper.SetValue(datesCache, SESSION_DATELIST);
            this.repeater_leave.DataSource = datesCache;
            this.repeater_leave.DataBind();
        }


        protected void delete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton senderObj = (ImageButton)sender;
            string strIndex = senderObj.CommandArgument;
            int intIndex = int.Parse(strIndex);

            datesCache.RemoveAt(intIndex);
            LSLibrary.WebAPP.PageSessionHelper.SetValue(datesCache, SESSION_DATELIST);

            this.repeater_leave.DataSource = datesCache;
            this.repeater_leave.DataBind();
        }


        private void uploadPic()
        {
            string absoluteDir = Server.MapPath("~/" + AppHelper.GlobalVariate.path_uploadPic);

            List<string> types = new List<string>(new string[] { "png", "gif" });
            string errorMsg;

            List<string> files= LSLibrary.UploadFile.SaveFiles(Request, absoluteDir, types, System.DateTime.Now.ToString("yyyyMMdd"),out errorMsg);

            foreach (string file in files)
            {
                string imagepath = "~/"+WEBUI.AppHelper.GlobalVariate.path_uploadPic+"/" + file;
                uploadPicCache.Add(new UploadPic(imagepath));
            }

            this.repeater_pic.DataSource = uploadPicCache;
            this.repeater_pic.DataBind();
            LSLibrary.WebAPP.PageSessionHelper.SetValue(uploadPicCache, SESSION_UPLOADPIC);

            if (string.IsNullOrWhiteSpace(errorMsg)==false)
            {
                this.lt_AlertJS.Text = LSLibrary.JavasScriptHelper.AlertMessage(errorMsg);
            }
        }


        private List<LeaveData> getListSource(DateTime from, DateTime to)
        {
            List<LeaveData> data = new List<LeaveData>();
            data.Add(new LeaveData("05-01周一", "AL", "FULL DAY", 0));
            data.Add(new LeaveData("05-02周二", "AL", "FULL DAY", 0));
            data.Add(new LeaveData("05-03周三", "AL", "FULL DAY", 0));
            data.Add(new LeaveData("05-04周四", "AL", "FULL DAY", 0));
            data.Add(new LeaveData("05-05周五", "AL", "FULL DAY", 0));
            return data;
        }


        public class LeaveData
        {
            public string date;
            public string type;
            public string section;
            public int typeid;

            public LeaveData(string date, string type, string section, int typeid)
            {
                this.date = date;
                this.type = type;
                this.section = section;
                this.typeid = typeid;
            }
        }

        public class UploadPic
        {
            public string path;

            public UploadPic(string _path)
            {
                this.path = _path;
            }
        }

        protected void button_apply_Click(object sender, EventArgs e)
        {
            LSLibrary.WebAPP.PageSessionHelper.CleanValue(SESSION_DATELIST);
            LSLibrary.WebAPP.PageSessionHelper.CleanValue(SESSION_UPLOADPIC);
            Response.Redirect("~/pages/main.aspx");
        }


        protected void btn_close_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton senderObj = (ImageButton)sender;
            string strIndex = senderObj.CommandArgument;
            int intIndex = int.Parse(strIndex);
            if (intIndex <= uploadPicCache.Count - 1)
            {
                uploadPicCache.RemoveAt(intIndex);
                LSLibrary.WebAPP.PageSessionHelper.SetValue(uploadPicCache, SESSION_UPLOADPIC);

                this.repeater_pic.DataSource = uploadPicCache;
                this.repeater_pic.DataBind();
            }
        }

    }
}