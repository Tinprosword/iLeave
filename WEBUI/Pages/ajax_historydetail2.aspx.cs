﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class ajax_historydetail2 : System.Web.UI.Page
    {
        private int requestID;
        private int leaveid;
        private int staff;
        private int employmentNo;
        public int lan = 1;

        public static int list_dataheight = 150;
        public static int historytitleHeight = 150;
        public static int totalHeight = 155 + list_dataheight + historytitleHeight;

        public int nametype = 1;

        public string GetLeaveStatus(WebServiceLayer.WebReference_leave.LeaveRequestDetail data)
        {
            return BLL.Leave.GetLeaveStatusDesc(data.WorkflowTypeID, data.Status);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            nametype = BLL.CodeSetting.GetNameType(BLL.MultiLanguageHelper.GetChoose());// BLL.CodeSetting.GetSystemParameter(BLL.CodeSetting.staffNameFormat);

            if (!IsPostBack)
            {
                requestID = int.Parse(Request["requestID"]);
                leaveid = int.Parse(Request["leaveid"]);
                staff = int.Parse(Request["staff"]);
                employmentNo = int.Parse(Request["employmentNo"]);
                lan = int.Parse(Request["lan"]);

                List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> detail = BLL.Leave.GetExtendLeaveDetailsByReuestID(requestID);

                //balance
                double balance = BLL.Leave.GetAailabeValue_substractFutherAndWait(leaveid, staff, employmentNo);
                string strBalance = balance == -99999 ? "--" : balance.ToString("0.###");
                this.lt_balance.Text = strBalance;
                //apply
                double appSum = 0;
                for (int i = 0; i < detail.Count; i++)
                {
                    appSum += detail[i].Unit;
                }
                this.lt_apply.Text=appSum.ToString("0.###");


                //history
                List<WebServiceLayer.WebReference_leave.LeaveHistory> history = BLL.Leave.GetLeaveHistoryByRequest_leawve(requestID);
                panel_history.Visible = history.Count == 0 ? false : true;
                this.rp_history.DataSource = history;
                this.rp_history.DataBind();

                //detail
                int actureHeight = panel_history.Visible == true ? list_dataheight : list_dataheight + list_dataheight + 30;
                this.divlist.Style.Add("height", actureHeight.ToString()+"px");
                this.rp_list.DataSource = detail;
                this.rp_list.DataBind();

                //multiplayLanguage
                string language_title = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_title;
                string language_title2 = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_approvalHistory;
                string language_col1 = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_list_column1;
                string language_col2 = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_list_column2;
                string language_col3 = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_list_column3;
                string language_balance = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_balance;
                string language_apply = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_applycount;
                this.lt_leavedetail.Text = language_title;
                this.lt_historytitle.Text = language_title2;
                this.lt_col1.Text = language_col1;
                this.lt_col2.Text = language_col2;
                this.lt_col3.Text = language_col3;
                this.lt_colstatus.Text = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).application_detail_status;
                this.lt_bancetitle.Text = language_balance;
                this.lt_applycount.Text = language_apply;
                this.lt_days2.Text = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_days;
                this.lt_days.Text = BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_days;
            }
        }


    }
}