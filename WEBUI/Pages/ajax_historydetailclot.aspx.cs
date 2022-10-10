using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class ajax_historydetailclot : System.Web.UI.Page
    {
        //request
        private int requestID;
        private int staff;
        private int employmentNo;
        public int lan = -1;

        public int nametype = 1;

        public static int list_dataheight = 150;
        public static int historytitleHeight = 150;
        public static int totalHeight = 155 + list_dataheight + historytitleHeight;

        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //request variable
            requestID = int.Parse(Request["requestID"]);
            staff = int.Parse(Request["staff"]);
            employmentNo = int.Parse(Request["employmentNo"]);
            lan = int.Parse(Request["lan"]);

            //base info
            nametype = BLL.CodeSetting.GetNameType((LSLibrary.WebAPP.LanguageType)(lan));
            List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> detail = BLL.CLOT.GetCLOTDetail(requestID);
            double fulldayHours = BLL.CodeSetting.GetFulldayWorkHours(employmentNo);

            //balance
            double balanceValue = BLL.Leave.GetBalanceView_CLOT_balance(employmentNo);

            this.lt_balance.Text = BLL.common.GenerateCLOTDisplay(double.Parse((balanceValue).ToString("0.##")), fulldayHours,
                BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).applyCLOT_list_Hours2, BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).Common_label_Day);

            //apply
            double appSum = 0;
            for (int i = 0; i < detail.Count; i++)
            {
                appSum += detail[i].Hour;
            }

            this.lt_apply.Text = BLL.common.GenerateCLOTDisplay(double.Parse((appSum).ToString("0.##")), fulldayHours,
                BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).applyCLOT_list_Hours2, BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).Common_label_Day);



            //histroy
            List<WebServiceLayer.WebReference_leave.LeaveHistory> history = BLL.Leave.GetLeaveHistoryByRequest_clot(requestID);


            panel_history.Visible = history.Count == 0 ? false : true;
            this.rp_history.DataSource = history;
            this.rp_history.DataBind();

            //detail
            int actureHeight = panel_history.Visible == true ? list_dataheight : list_dataheight + list_dataheight + 30;
            this.divlist.Style.Add("height", actureHeight.ToString() + "px");
            this.rp_list.DataSource = detail;
            this.rp_list.DataBind();

            MultipleLanguage();
        }



        private void MultipleLanguage()
        {
            //multiplayLanguage


            string language_title = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approvalWait_CLOT_Title;
            string language_balance = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approvalWait_CLOT_Balance;
            string language_apply = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approvalWait_CLOT_applycount;

            string language_title2 = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_approvalHistory;

            string language_col1 = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_list_column1;
            string language_col2 = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).clot_Type;
            string language_col3 = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).clot_day;


            this.lt_leavedetail.Text = language_title;
            this.lt_bancetitle.Text = language_balance;
            this.lt_applycount.Text = language_apply;

            this.lt_historytitle.Text = language_title2;


            this.lt_col1.Text = language_col1;
            this.lt_col2.Text = language_col2;
            this.lt_col3.Text = language_col3;
        }

        
    }
}