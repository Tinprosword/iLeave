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
                showPayslispStatus(int.Parse(this.DropDownList1.SelectedValue));
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
            showPayslispStatus(int.Parse(this.DropDownList1.SelectedValue));
        }

        private void showPayslispStatus(int date)
        {
            this.lb_status.Text = "";
            if (mMyPayslip != null && mMyPayslip.Count() > 0)
            {
                var theItem= mMyPayslip.Where(x => x.Staffid == loginer.userInfo.staffid && x.PayrollTrailMonth == date.ToString()).FirstOrDefault();
                if (theItem != null)
                {
                    this.lb_status.Text = theItem.IsLock==true? BLL.MultiLanguageHelper.GetLanguagePacket().Paylist_label_download: BLL.MultiLanguageHelper.GetLanguagePacket().Paylist_lable_pending;
                }
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            bool realDownload = false;
            if (!realDownload)
            {
                string filePath = Server.MapPath("../res/payslip.pdf");
                LSLibrary.HttpHelper.DownloadFile(filePath, "payslip.pdf", Server, Response);
            }
            else
            {
                //1.get file   2.download it. 3.delete file
                //1.1 get file's bin 1.2 convert to pdf
                string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".pdf";
                string filePath = Server.MapPath("~/res/" + filename);

                int companyid = 0;
                var employee= BLL.User_wsref.GetPersonBaseInfoByUid(loginer.userInfo.id).FirstOrDefault();
                if (employee != null)
                {
                    companyid=employee.s_CompanyID??0;
                }
                int selectedYear = int.Parse(this.DropDownList1.SelectedValue.Substring(0,4));
                int selectMonth= int.Parse(this.DropDownList1.SelectedValue.Substring(4, 2));
                var data= BLL.Other.GetPayslipReportData(companyid, loginer.userInfo.staffid??0, selectedYear, selectMonth);

                
                LSLibrary.HttpHelper.DownloadFile(filePath, filePath, Server, Response);
                //todo 0 delete yesterterday file.
            }
        }
    }
}