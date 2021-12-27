﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class estimation : BLL.CustomLoginTemplate
    {
        #region page event
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {

        }

        protected override void InitPage_OnFirstLoad2()
        {

        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {

        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            SetupMultiLanguage();
            SetupUI();
        }

        

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {

        }
        #endregion


        #region function

        private void SetupUI()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().apply_estimation, "~/pages/main.aspx", true);
            //ddl bind
            ListItem[] datasource = new ListItem[2];
            datasource[0]=(new ListItem(BLL.MultiLanguageHelper.GetLanguagePacket().Common_label_AnnualLeave, "0"));
            datasource[1]=(new ListItem(BLL.MultiLanguageHelper.GetLanguagePacket().Common_label_SickLeave, "1"));
            this.DropDownList1.Items.Clear();
            this.DropDownList1.Items.AddRange(datasource);

            DateTime esDate = BLL.Other.GetEstimateDate(loginer.userInfo.employID??0);
            this.tb_date.Text = esDate.ToString("yyyy-MM-dd");
        }

        private void SetupMultiLanguage()
        {
            this.lt_new.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_new;
            this.lt_mypending.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_pending;
            this.lt_myhistory.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_processed;
            this.lt_estimation.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_estimation;

            this.lt_type.Text = BLL.MultiLanguageHelper.GetLanguagePacket().type;
            this.lt_date.Text = BLL.MultiLanguageHelper.GetLanguagePacket().date;
            this.bt_estimation.Text = BLL.MultiLanguageHelper.GetLanguagePacket().btn_es;
            this.lb_days.Text = BLL.MultiLanguageHelper.GetLanguagePacket().Common_label_Days;
        }
        #endregion

        protected void bt_estimation_Click(object sender, EventArgs e)
        {
            string leaveType = this.DropDownList1.SelectedValue;
            DateTime asofdate = System.DateTime.Now;
            DateTime.TryParse(this.tb_date.Text, out asofdate);
            if (leaveType == "0")
            {
                double albalance = BLL.Leave.GetEstimation(loginer.userInfo.firsteid ?? 0, asofdate);
                this.lb_msg.Text = albalance.ToString();
            }
            else
            {
                double slbalance = BLL.Leave.GetSLEstimation(loginer.userInfo.firsteid ?? 0, asofdate);
                this.lb_msg.Text = slbalance.ToString();
            }
        }
    }

}