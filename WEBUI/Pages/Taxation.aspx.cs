using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Taxation : BLL.CustomLoginTemplate
    {
        WebServiceLayer.WebReference_leave.v_System_iLeave_Taxtion[] mMyPayslip;
        private static string taxpdfname = "taxation";

        #region
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            mMyPayslip = BLL.Other.GetTaxationBysid(loginer.userInfo.staffid ?? 0).ToArray(); ;
        }

        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            this.lb_msg.Text = "";
            this.LT_JSDOWNLOAD.Text = "";

            string downloadTip = BLL.MultiLanguageHelper.GetLanguagePacket().Common_downloadTip;
            string downloadTip2 = BLL.MultiLanguageHelper.GetLanguagePacket().Common_downloadTip2;


           

            if (string.IsNullOrEmpty(downloadTip2))
            {
                this.lt_js_showdown.Text = "<script> function showdownloadMsg() {$(\"#" + this.lb_downloadtip.ClientID + "\").text('" + downloadTip + "'); setTimeout('emptydownloadMsg()',40000);}</script>";
            }
            else
            {
                this.lt_js_showdown.Text = "<script> function showdownloadMsg() {$(\"#" + this.lb_downloadtip.ClientID + "\").text('" + downloadTip + "'); $(\"#" + this.lb_downloadtip2.ClientID + "\").text('" + downloadTip2 + "'); setTimeout('emptydownloadMsg()',40000);}</script>";
            }

          
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            MulLanguage();
            LoadUI();
        }



        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        { }

        #endregion



        private void LoadUI()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().main_Taxation, "~/pages/main.aspx", true);

            string companyName; int companyid; List<LSLibrary.WebAPP.ValueText<int>> dates;

            BLL.Other.GetTaxationBaseInfo(mMyPayslip, out companyName, out companyid, out dates);

            this.lb_companyName.Text = companyName;


            this.DropDownList1.DataSource = dates;
            this.DropDownList1.DataTextField = "mtext";
            this.DropDownList1.DataValueField = "mvalue";
            this.DropDownList1.DataBind();

            if (!string.IsNullOrEmpty(this.DropDownList1.SelectedValue))
            {
                showPayslispStatus(int.Parse(this.DropDownList1.SelectedValue));
            }
        }

        private void MulLanguage()
        {
            this.lt_company.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Paylist_lable_company;
            this.lt_date.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Taxation_label_year;
            this.lt_status.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Paylist_lable_status;
            this.btn_search.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Common_download;
            this.lt_replacement.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Taxation_label_replacement;
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showPayslispStatus(int.Parse(this.DropDownList1.SelectedValue));
        }

        private void showPayslispStatus(int date)
        {
            this.lb_status.Text = "";
            if (mMyPayslip != null && mMyPayslip.Count() > 0)
            {
                var theItem = mMyPayslip.Where(x => x.Staffid == loginer.userInfo.staffid && x.TaxYear == date.ToString()).FirstOrDefault();
                if (theItem != null)
                {
                    this.lb_status.Text = theItem.IsRelease == true ? "Release" : "Unrelease";
                }
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            int selectedYear = 0;
            int.TryParse(this.DropDownList1.SelectedValue, out selectedYear);

            bool replace = this.cb_replacement.Checked;


            if (selectedYear != 0)
            {
                var pEmployment = mMyPayslip.Where(x => x.TaxYear == selectedYear.ToString()).FirstOrDefault();

                if (pEmployment != null)
                {
                    int pEmploymentID = pEmployment.EmploymentID;
                    var data = BLL.Other.GetTextationReportData(selectedYear, pEmploymentID, loginer.userInfo.id,replace);

                    if (data != null && data.reportData != null && data.reportData.Length > 0 && data.msgtype == 1)
                    {
                        string agent = HttpContext.Current.Request.UserAgent;
                        LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.MobilWebHelper.GetClientType(agent);

                        var cookies = BLL.Page.MyCookieManage.GetCookie();


                        if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.android && cookies.isAppLogin == "1")//android
                        {
                            //0.delete prefile 1.SAVE TO FILE.and.get filename.
                            BLL.Other.DeleteOlderFiles(Server);
                            string tempFileName = taxpdfname + ".pdf";
                            string tempDatetime= System.DateTime.Now.ToString("yyyyMMddHHmmss");
                            string tempFileFolderPath = Server.MapPath("~/tempdownload") + LSLibrary.FileUtil.filepathflag + tempDatetime;
                            string tempFilePath = LSLibrary.FileUtil.GenerateFileName(tempFileFolderPath, tempFileName);
                            string tempFileURL = LSLibrary.WebAPP.httpHelper.GenerateURL("tempdownload/" + tempDatetime + "/" + tempFileName);
                            LSLibrary.FileUtil.CreateFile(tempFilePath, data.reportData);

                            string js = LSLibrary.WebAPP.MyJSHelper.SendMessageToAndroid("DOWNLOAD3", tempFileURL, HttpContext.Current.Server);
                            LT_JSDOWNLOAD.Text = js;
                        }
                        else if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone && cookies.isAppLogin == "1")//ios
                        {
                            BLL.Other.DeleteOlderFiles(Server);
                            string tempFileName = taxpdfname + ".pdf";
                            string tempDatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                            string tempFileFolderPath = Server.MapPath("~/tempdownload") + LSLibrary.FileUtil.filepathflag + tempDatetime;
                            string tempFilePath = LSLibrary.FileUtil.GenerateFileName(tempFileFolderPath, tempFileName);
                            string tempFileURL = LSLibrary.WebAPP.httpHelper.GenerateURL("tempdownload/" + tempDatetime + "/" + tempFileName);
                            LSLibrary.FileUtil.CreateFile(tempFilePath, data.reportData);

                            string js = LSLibrary.WebAPP.MyJSHelper.SendMessageToIphone("DOWNLOAD3", tempFileURL, HttpContext.Current.Server);
                            LT_JSDOWNLOAD.Text = js;
                        }
                        else//pc
                        {
                            LSLibrary.HttpHelper.DownloadFile(data.reportData, taxpdfname+".pdf", Server, Response);
                        }
                    }
                    else
                    {
                        string errorMsg = data.msgtype.ToString();
                        if (errorMsg == "-4")
                        {
                            errorMsg = "No Record Found";
                        }
                        this.lb_msg.Text = "Error:" + errorMsg;
                    }
                }
                else
                {
                    this.lb_msg.Text = "Error:no possible. year is not match.";
                }
            }

            
        }

        protected override void InitPage_OnNotFirstLoad2()
        {
           
        }

        protected override void PageLoad_InitUIOnNotFirstLoad4()
        {
            
        }
    }
}