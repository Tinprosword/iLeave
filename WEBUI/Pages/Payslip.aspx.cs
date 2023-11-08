using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Payslip : BLL.CustomLoginTemplate
    {
        WebServiceLayer.WebReference_leave.v_System_iLeave_Payslip[] mMyPayslip;
        #region
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            mMyPayslip = BLL.Other.GetPayslipBysid(loginer.userInfo.staffid ?? 0);
        }

        protected override void InitPage_OnFirstLoad2()
        {
            
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            LT_JSDOWNLOAD.Text = "";


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
        {
        }

        #endregion


        private void LoadUI()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().main_Payslip, "~/pages/main.aspx", true);

            string companyName;int companyid;List<LSLibrary.WebAPP.ValueText<int>> dates;

            BLL.Other.GetPaylistBaseInfo(mMyPayslip, out companyName, out companyid,out dates);

            this.lb_companyName.Text = companyName;


            this.DropDownList1.DataSource = dates;
            this.DropDownList1.DataTextField = "mtext";
            this.DropDownList1.DataValueField = "mvalue";
            this.DropDownList1.DataBind();

            if (!string.IsNullOrEmpty(this.DropDownList1.SelectedValue))
            {
                showPayslispStatusAndHiddenBtn(int.Parse(this.DropDownList1.SelectedValue));
            }
        }

        private void MulLanguage()
        {
            this.lt_company.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Paylist_lable_company;
            this.lt_date.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Paylist_lable_month;
            this.lt_status.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Paylist_lable_status;
            this.btn_search.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Common_download;
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showPayslispStatusAndHiddenBtn(int.Parse(this.DropDownList1.SelectedValue));
        }


        private void showPayslispStatusAndHiddenBtn(int date)
        {
            this.btn_search.Visible = true;
            this.lb_status.Text = "";
            if (mMyPayslip != null && mMyPayslip.Count() > 0)
            {
                var theItem= mMyPayslip.Where(x => x.Staffid == loginer.userInfo.staffid && x.PayrollTrailMonth == date.ToString()).FirstOrDefault();
                if (theItem != null)
                {
                    this.btn_search.Visible = theItem.IsLock == true ? true : false;
                    this.lb_status.Text = theItem.IsLock==true? BLL.MultiLanguageHelper.GetLanguagePacket().Paylist_label_download: BLL.MultiLanguageHelper.GetLanguagePacket().Paylist_lable_pending;
                }
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            string taxpdfname = "Payslip";

            int companyid = 0;
            var employee= BLL.User_wsref.GetPersonBaseInfoByUid(loginer.userInfo.u_id).FirstOrDefault();
            if (employee != null)
            {
                companyid=employee.s_CompanyID??0;
            }
            int selectedYear = int.Parse(this.DropDownList1.SelectedValue.Substring(0,4));
            int selectMonth= int.Parse(this.DropDownList1.SelectedValue.Substring(4, 2));
            var data= BLL.Other.GetPayslipReportData(loginer.userInfo.staffid??0, selectedYear, selectMonth,loginer.userInfo.u_id);
            if (data != null && data.ReportDocumentArray != null && data.ReportDocumentArray.Length > 0)
            {
                string agent = HttpContext.Current.Request.UserAgent;
                LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.MobilWebHelper.GetClientType(agent);

                var cookies = BLL.Page.MyCookieManage.GetCookie();


                if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.android && cookies.isAppLogin == "1")//android
                {
                    BLL.Other.DeleteOlderFiles(Server);
                    string tempFileName = taxpdfname + ".pdf";
                    string tempDatetime = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    string tempFileFolderPath = Server.MapPath("~/tempdownload") + LSLibrary.FileUtil.filepathflag + tempDatetime;
                    string tempFilePath = LSLibrary.FileUtil.GenerateFileName(tempFileFolderPath, tempFileName);
                    string tempFileURL = LSLibrary.WebAPP.httpHelper.GenerateURL("tempdownload/" + tempDatetime + "/" + tempFileName);
                    LSLibrary.FileUtil.CreateFile(tempFilePath, data.ReportDocumentArray);

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
                    LSLibrary.FileUtil.CreateFile(tempFilePath, data.ReportDocumentArray);

                    string js = LSLibrary.WebAPP.MyJSHelper.SendMessageToIphone("DOWNLOAD3", tempFileURL, HttpContext.Current.Server);
                    LT_JSDOWNLOAD.Text = js;
                }
                else//pc
                {
                   LSLibrary.HttpHelper.DownloadFile(data.ReportDocumentArray, taxpdfname + ".pdf", Server, Response);
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