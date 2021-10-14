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

            if (selectedYear != 0)
            {
                var pEmployment = mMyPayslip.Where(x => x.TaxYear == selectedYear.ToString()).FirstOrDefault();

                if (pEmployment != null)
                {
                    int pEmploymentID = pEmployment.EmploymentID;
                    var data = BLL.Other.GetTextationReportData(selectedYear, pEmploymentID, loginer.userInfo.id);

                    if (data != null && data.reportData != null && data.reportData.Length > 0 && data.msgtype == 1)
                    {
                        LSLibrary.HttpHelper.DownloadFile(data.reportData, "taxation.pdf", Server, Response);
                    }
                    else
                    {
                        string errorMsg = data.msgtype.ToString();
                        this.lb_msg.Text = "Error:" + data.msgtype;
                    }
                }
                else
                {
                    this.lb_msg.Text = "Error:no possible. year is not match.";
                }
            }
        }


    }
}