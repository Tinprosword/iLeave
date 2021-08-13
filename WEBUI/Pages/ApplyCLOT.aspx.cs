﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class ApplyCLOT : BLL.CustomLoginTemplate
    {
        private readonly string NAME_OF_PAGE_VIEW = "NAME_OF_PAGE_VIEW";
        private float mBalance = 0;

        #region page event
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
        }


        protected override void InitPage_OnFirstLoad2()
        { }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            literal_errormsga.Visible = false;
            literal_errormsga.Text = "";
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            LSLibrary.WebAPP.ViewStateHelper.SetValue(NAME_OF_PAGE_VIEW, new MODEL.CLOT.ViewState_page(), this.ViewState);
            MulLanguage();
            LoadUI();
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            //CLOTTab.SetEvent((WEBUI.Controls.leave)Master);
        }
        #endregion


        #region page event


        #endregion

        private void MulLanguage()
        {
            this.lt_new.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_new;
            this.lt_mypending.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_pending;
            this.lt_myhistory.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_processed;

            this.lt_name.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_name;
            this.lt_leave.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_type;
            this.lt_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_apply;
            this.lt_balance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_banlance;
            this.lt_date.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_date;
            this.lt_time.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_timefrom;
            this.lt_remarks.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_remarks;
            this.lt_date.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_list_date;

            this.ltlistdate.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_date;
            this.ltlisttype.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_list_type;
            this.ltlistfromto.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_list_TimeRange;
            this.ltlisthours.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_list_Hours;
        }

        private void LoadUI()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().main_applyCLOT, "~/pages/main.aspx", true);


            int intNameType = BLL.CodeSetting.GetNameType(BLL.MultiLanguageHelper.GetChoose());
            MODEL.UserName tempUserName = new MODEL.UserName(loginer.userInfo.surname, loginer.userInfo.firstname, loginer.userInfo.nickname, loginer.userInfo.namech);
            this.literal_applier.Text = tempUserName.GetDisplayName(intNameType);

            var ddldata = LSLibrary.EnumHelper.GetValueText(typeof(MODEL.CLOT.enum_clotType));
            this.ddl_leavetype.DataSource = ddldata;
            this.ddl_leavetype.DataTextField = LSLibrary.EnumHelper.NAME_Enum_valueTextDesc_DESC;
            this.ddl_leavetype.DataValueField = LSLibrary.EnumHelper.NAME_Enum_valueTextDesc_VALUE;
            this.ddl_leavetype.DataBind();

            this.lt_applydays.Text = "--";
            this.lt_balancedays.Text = "--";
            double cleanValue = BLL.Leave.GetCleanValue(-9, (int)loginer.userInfo.staffid, (int)loginer.userInfo.employID);
            this.lt_balancedays.Text = cleanValue.ToString("0.##") + " " + BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_list_Hours2;
            RefleshApplyBalance();

            this.tb_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

            DropDownList1.Items.Clear();
            DropDownList3.Items.Clear();
            for (int i = 0; i < 24; i++)
            {
                this.DropDownList1.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
                this.DropDownList3.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
            }

            DropDownList2.Items.Clear();
            DropDownList4.Items.Clear();
            for (int i = 0; i < 60; i++)
            {
                this.DropDownList2.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
                this.DropDownList4.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
            }


            this.tb_remarks.Text=GetDefaultRemark();



            SetupReport();
        }

        private void SetupReport()
        {
            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);
            if (dataview != null && dataview.items != null)
            {
                this.repeater_clot.DataSource = dataview.items;
            }
            else
            {
                this.repeater_clot.DataSource = new List<MODEL.CLOT.CLOTItem>();
            }
            this.repeater_clot.DataBind();
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            //1check :day , from to, 2. add items to viewstate.  3.reload repeater.
            



            int validData = -1;
            DateTime theday = System.DateTime.Now;
            int fromh =int.Parse( this.DropDownList1.SelectedValue);
            int fromm = int.Parse(this.DropDownList2.SelectedValue);
            int toh = int.Parse(this.DropDownList3.SelectedValue);
            int tom = int.Parse(this.DropDownList4.SelectedValue);
            bool bvalidday = DateTime.TryParse(this.tb_date.Text, out theday);
            int type = int.Parse(ddl_leavetype.SelectedValue);
            string remark = this.tb_remarks.Text.Trim();

            MODEL.CLOT.CLOTItem tempItem = new MODEL.CLOT.CLOTItem();
            tempItem.date = theday;
            tempItem.fromhour = fromh;
            tempItem.tohour = toh;
            tempItem.frommin = fromm;
            tempItem.tominute = tom;
            tempItem.type = (MODEL.CLOT.enum_clotType)type;
            tempItem.remark = remark;

            validData = CheckValid(tempItem);
            if (validData > 0 && bvalidday)
            {
                var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);

                dataview.items.Add(tempItem);

                LSLibrary.WebAPP.ViewStateHelper.SetValue(NAME_OF_PAGE_VIEW, dataview, this.ViewState);
            }
            else if (validData == -1)
            {
                literal_errormsga.Visible = true;
                literal_errormsga.Text = "Time must be more than zero.";
            }

            SetupReport();
            RefleshApplyBalance();
        }

        private int CheckValid(MODEL.CLOT.CLOTItem tempItem)
        {
            int result = 1;
            if (tempItem.GetHours() <= 0)
            {
                result = -1;
            }
            return result;
        }

        protected void delete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            string strIndex = imageButton.CommandArgument;

            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);

            dataview.items.RemoveAt(int.Parse(strIndex));

            LSLibrary.WebAPP.ViewStateHelper.SetValue(NAME_OF_PAGE_VIEW, dataview, this.ViewState);

            SetupReport();
        }

        protected void btn_apply_Click(object sender, EventArgs e)
        {
            //1.check valid data.2.insert each request.
            string waitCode = LSLibrary.WebAPP.httpHelper.WaitDiv_show(BLL.MultiLanguageHelper.GetLanguagePacket().Commonsubmit_success);
            Response.Write(waitCode);
            Response.Flush();


            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);
            bool validData = CheckData(dataview.items, mBalance);

            if (validData)
            {
                List<int> requestidArray= BLL.CLOT.InsertCLOTRequests(dataview.items, loginer.userInfo.id, loginer.userInfo.employID??0);


                string successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().apply_msgapplyok);
                Response.Write(successMsg + ".");
                Response.Flush();
                System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验

                this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                this.PreRenderComplete += Apply_PreRenderComplete;
            }
        }

        private void Apply_PreRenderComplete(object sender, EventArgs e)
        {
            ApplyCLOT page = (ApplyCLOT)sender;
            page.Response.Clear();
            page.Response.Write(LSLibrary.WebAPP.MyJSHelper.Goto("approval_wait.aspx?action=1&applicationtype=0&from=2"));
            page.Response.End();
        }

        private static bool CheckData(List<MODEL.CLOT.CLOTItem> data, float balance)
        {
            return true;
        }

        private string GetDefaultRemark()
        {
            int type = 0;
            string str = this.ddl_leavetype.SelectedValue;
            int.TryParse(str, out type);
            if (type == (int)MODEL.CLOT.enum_clotType.CL)
            {
                return "Apply for CL";
            }
            else
            {
                return "Apply for OT";
            }
        }

        protected void ddl_leavetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tb_remarks.Text= GetDefaultRemark();
        }

        //js传递apply sum 标签的值 ,如果为空表示没有做任何处理 . 否则有数据,那么传递不同的参数向js function.
        //checkNewTab: alter message ,action(ismanage),bigrange(pengding,histroy),from (0:leave 1 colot)
        public string showPendEvent()
        {
            if (!IsPostBack)
            {
                return "return checkNewTab('',1,0,1)";
            }
            else
            {
                return "return checkNewTab('" + BLL.MultiLanguageHelper.GetLanguagePacket().apply_msg_tab + "',1,0,1)";
            }
        }

        public string showhisEvent()
        {
            if (!IsPostBack)
            {
                return "return checkNewTab('',1,3,0)";
            }
            else
            {
                return "return checkNewTab('" + BLL.MultiLanguageHelper.GetLanguagePacket().apply_msg_tab + "',1,3,1)";
            }
        }


        private void RefleshApplyBalance()
        {
            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);
            if (dataview != null && dataview.items != null)
            {
                float totalHour = 0;
                foreach (var item in dataview.items)
                {
                    totalHour += item.GetHours();
                }
                this.lt_applydays.Text = totalHour.ToString()+" "+ BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_list_Hours2;
            }
            else
            {
                this.lt_applydays.Text = "--";
            }
        }
    }
}