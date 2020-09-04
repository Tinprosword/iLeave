﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class myapplications : BLL.CustomLoginTemplate
    {
        private readonly string css_select = "btnBox btnBlueBoxSelect";
        private readonly string css_unselect = "btnBox btnBlueBoxUnSelect";


        protected override void InitPage_OnEachLoadBeforeF5_1()
        {
        }



        protected override void PageLoad_ResetUIOnEachLoad5()
        {
        }

        protected override void InitPage_OnFirstLoad2()
        {
            this.btn_approved.CssClass = css_unselect;
            this.btn_wait.CssClass = css_select;
            this.btn_rejectWith.CssClass = css_unselect;
        }

        protected override void PageLoad_ResetUIOnEachLoad3()
        { 
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().application_back, BLL.MultiLanguageHelper.GetLanguagePacket().application_current, "~/pages/main.aspx");
            SetupMultiLanguage();
            ResetSelectTab();
            this.repeater_myapplications.DataSource = GetDatasource( getStatus(), loginer.userInfo.id, this.tb_date.Text, loginer.userInfo.personid);
            this.repeater_myapplications.DataBind();
        }

        private void ResetSelectTab()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["selectedtab"]))
            {
                int selecttab = int.Parse(Request.QueryString["selectedtab"]);
                if (selecttab == (int)BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval)
                {
                    btn_wait_Click(null, null);
                }
                else if (selecttab == (int)BLL.GlobalVariate.LeaveBigRangeStatus.approvaled)
                {
                    btn_approved_Click(null, null);
                }
                else
                {
                    btn_rejectWith_Click(null, null);
                }
            }
        }


        private void SetupMultiLanguage()
        {
            this.btn_approved.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_approved;
            this.btn_rejectWith.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_rejected;
            this.btn_wait.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_wait;
            this.ltdatefrom.Text = BLL.MultiLanguageHelper.GetLanguagePacket().application_datefrom;
        }


        protected void btn_wait_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_unselect;
            this.btn_wait.CssClass = css_select;
            this.btn_rejectWith.CssClass = css_unselect;
            this.repeater_myapplications.DataSource = GetDatasource(getStatus(), loginer.userInfo.id, this.tb_date.Text, loginer.userInfo.personid);
            this.repeater_myapplications.DataBind();
        }


        protected void btn_approved_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_select;
            this.btn_wait.CssClass = css_unselect;
            this.btn_rejectWith.CssClass = css_unselect;


            this.repeater_myapplications.DataSource = GetDatasource( getStatus(), loginer.userInfo.id, this.tb_date.Text,  loginer.userInfo.personid);
            this.repeater_myapplications.DataBind();
        }

        protected void btn_rejectWith_Click(object sender, EventArgs e)
        {
            this.btn_approved.CssClass = css_unselect;
            this.btn_wait.CssClass = css_unselect;
            this.btn_rejectWith.CssClass = css_select;

            this.repeater_myapplications.DataSource = GetDatasource( getStatus(), loginer.userInfo.id, this.tb_date.Text,  loginer.userInfo.personid);
            this.repeater_myapplications.DataBind();
        }

        protected void lb_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            string requestid = link.CommandArgument;
            Response.Redirect("~/Pages/myDetail.aspx?action=0&selectedtab="+(int)getStatus()+"&requestid=" + requestid, true);
        }

        protected void tb_date_TextChanged1(object sender, EventArgs e)
        {
            this.repeater_myapplications.DataSource = GetDatasource( getStatus(), loginer.userInfo.id, this.tb_date.Text,  loginer.userInfo.personid);
            this.repeater_myapplications.DataBind();
        }


        private BLL.GlobalVariate.LeaveBigRangeStatus getStatus()
        {
            BLL.GlobalVariate.LeaveBigRangeStatus result = BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval;
            if (this.btn_approved.CssClass == css_select)
            {
                result = BLL.GlobalVariate.LeaveBigRangeStatus.approvaled;
            }
            else if (this.btn_wait.CssClass == css_select)
            {
                result = BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval;
            }
            else
            {
                result = BLL.GlobalVariate.LeaveBigRangeStatus.withdraw;
            }

            return result;
        }


        //chooseStatus:1 ,apporve ,0 wait .2 reject
        private static List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> GetDatasource( BLL.GlobalVariate.LeaveBigRangeStatus chooseStatus, int uid, string datestrFrom,  int pid)
        {
            List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> result = null;
            DateTime? dateFrom = null;
            if (!string.IsNullOrEmpty(datestrFrom))
            {
                dateFrom = DateTime.Parse(datestrFrom);
            }
            result = BLL.Leave.GetMyLeaveMaster(pid, chooseStatus, dateFrom);
            return result;
        }

    }
}