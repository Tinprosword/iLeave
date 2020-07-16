using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Apply : BLL.CustomLoginTemplate
    {
        //todo page页面的多层继承写的不错，可以总结下了。
        public static string ViewState_PageName = "PageView";
        public StateBag myviewState;

        #region [page event]

        protected override void InitPageVaralbal0()
        {
            Apply_Upload prepage = PreviousPage as Apply_Upload;
            this.OnF5Doit = () => { Response.Redirect("~/pages/apply.aspx"); };
        }

        protected override void InitPageDataOnEachLoad1()
        {
            myviewState = ViewState;
        }

        protected override void InitPageDataOnFirstLoad2()
        {
        }

        protected override void ResetUIOnEachLoad3()
        {
            this.lt_AlertJS.Text = "";

            this.repeater_leave.ItemDataBound += Repeater_leave_ItemDataBound;
        }

        private void Repeater_leave_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            DropDownList ddl = (DropDownList)item.FindControl("rp_dropdl_section");
            MODEL.Apply.LeaveData itemdata = (MODEL.Apply.LeaveData)item.DataItem;
            for (int i = 0; i < ddl.Items.Count;i++)
            {
                if (itemdata.section== ddl.Items[i].Text)
                {
                    ddl.Items[i].Selected = true;
                }
                else
                {
                    ddl.Items[i].Selected = false;
                }
            }
        }

        protected override void InitUIOnFirstLoad4()
        {
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "back")
            {
                //get session value
                Apply_Upload prepage = PreviousPage as Apply_Upload;
                if (prepage != null)
                {
                    MODEL.Apply.ViewState_page applypage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, prepage.myviewState);
                    LSLibrary.WebAPP.ViewStateHelper.SetValue(applypage, ViewState_PageName, ViewState);

                    //init ui
                    this.literal_applier.Text = loginer.loginID + "  " + loginer.userInfo.nickname;
                    ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_current, "~/pages/main.aspx");
                    this.ddl_leavetype.SelectedValue = applypage.LeaveTypeSelectValue.ToString();
                    this.repeater_leave.DataSource = applypage.LeaveList;
                    this.lt_applydays.Text = applypage.applylabel;
                    this.lt_balancedays.Text = applypage.balancelabel;
                    this.tb_from.Text = applypage.datefrom;
                    this.tb_to.Text = applypage.dateto;
                    this.dropdl_section.SelectedValue = applypage.ddlsectionSelectvalue;
                    this.tb_remarks.Text = applypage.remarks;

                    this.repeater_leave.DataBind();
                }

            }
            else
            {
                //init viewstate
                if (ViewState[ViewState_PageName] == null)
                {
                    MODEL.Apply.ViewState_page viewState_Page = new MODEL.Apply.ViewState_page();
                    viewState_Page.LeaveList = new List<MODEL.Apply.LeaveData>();
                    viewState_Page.uploadpic = new List<MODEL.Apply.UploadPic>();
                    LSLibrary.WebAPP.ViewStateHelper.SetValue(viewState_Page, ViewState_PageName, ViewState);
                }

                //init ui
                this.literal_applier.Text = loginer.loginID + "  " + loginer.userInfo.nickname;
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_current, "~/pages/main.aspx");
                this.repeater_leave.DataSource = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState).LeaveList;
                this.repeater_leave.DataBind();
            }
            SetMultiLanguage();
        }

        #endregion

        #region [module] upload pic
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            //save viewstate' other data
            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            applyPage.LeaveTypeSelectValue = this.ddl_leavetype.SelectedValue;
            applyPage.applylabel = this.lt_applydays.Text;
            applyPage.balancelabel = this.lt_balancedays.Text;
            applyPage.datefrom = this.tb_from.Text;
            applyPage.dateto = this.tb_to.Text;
            applyPage.ddlsectionSelectvalue = this.dropdl_section.SelectedValue;
            applyPage.remarks = this.tb_remarks.Text;
            LSLibrary.WebAPP.ViewStateHelper.SetValue(applyPage, ViewState_PageName, ViewState);
        }
        #endregion

        #region [module] leave
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            MODEL.Apply.ViewState_page pagedate = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            if (pagedate.LeaveList != null)
            {
                pagedate.LeaveList.AddRange(getListSource(DateTime.Now, DateTime.Now));

                LSLibrary.WebAPP.ViewStateHelper.SetValue(pagedate, ViewState_PageName, ViewState);
                this.repeater_leave.DataSource = pagedate.LeaveList;
                this.repeater_leave.DataBind();
            }
        }

        protected void delete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton senderObj = (ImageButton)sender;
            string strIndex = senderObj.CommandArgument;
            int intIndex = int.Parse(strIndex);

            MODEL.Apply.ViewState_page pagedate = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            if (pagedate.LeaveList != null)
            {
                pagedate.LeaveList.RemoveAt(intIndex);

                LSLibrary.WebAPP.ViewStateHelper.SetValue(pagedate, ViewState_PageName, ViewState);
                this.repeater_leave.DataSource = pagedate.LeaveList;
                this.repeater_leave.DataBind();
            }
        }


        protected void rp_dropdl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList senderObj = (DropDownList)sender;
            string strIndex = senderObj.Attributes["fix"];
            int intIndex = int.Parse(strIndex);

            string abc = senderObj.SelectedItem.Text;

            MODEL.Apply.ViewState_page pagedate = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            if (pagedate.LeaveList != null)
            {
                pagedate.LeaveList[intIndex].section = abc;

                LSLibrary.WebAPP.ViewStateHelper.SetValue(pagedate, ViewState_PageName, ViewState);
                this.repeater_leave.DataSource = pagedate.LeaveList;
                this.repeater_leave.DataBind();
            }
        }
        #endregion

        #region [module] apply
        protected void button_apply_Click(object sender, EventArgs e)
        {
            ////1,获得数据   2,调用ws,进行插入.  3,clean session.
            //List<MODEL.Apply.LeaveData> LeaveList = LSLibrary.WebAPP.ViewStateHelper.GetValue<List<MODEL.Apply.LeaveData>>(ViewState_LeaveListName, ViewState);



            ////clean session
            //LSLibrary.WebAPP.PageSessionHelper.CleanValue(Session_pageName);
            
            //Response.Redirect("~/pages/main.aspx");
        }
        #endregion

        #region [common function]
        private void SetMultiLanguage()
        {
            this.lt_name.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_name;
            this.lt_leave.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_leave;
            this.lt_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_apply;
            this.lt_balance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_banlance;
            this.lt_date.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_date;
            this.lt_section.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_section;
            this.lt_remarks.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_remarks;
            this.ltlistdate.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_list_data;
            this.ltlisttype.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_list_type;
            this.lt_listsection.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_list_section;
            this.button_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_button;
        }

        private List<MODEL.Apply.LeaveData> getListSource(DateTime from, DateTime to)
        {
            List<MODEL.Apply.LeaveData> data = new List<MODEL.Apply.LeaveData>();
            for (int i = 0; i <1; i++)
            {
                data.Add(new MODEL.Apply.LeaveData("Admin","05-01周一", "AL", "FULL DAY", 0, 0,BLL.GlobalVariate.LeaveType[0]));
                data.Add(new MODEL.Apply.LeaveData("Admin", "05-02周二", "AL", "FULL DAY", 0, 1, BLL.GlobalVariate.LeaveType[1]));
                data.Add(new MODEL.Apply.LeaveData("Admin", "05-03周三", "AL", "FULL DAY", 0, 2, BLL.GlobalVariate.LeaveType[2]));
                data.Add(new MODEL.Apply.LeaveData("Admin", "05-04周四", "AL", "FULL DAY", 0, 0, BLL.GlobalVariate.LeaveType[0]));
                data.Add(new MODEL.Apply.LeaveData("Admin", "05-05周五", "AL", "FULL DAY", 0, 0, BLL.GlobalVariate.LeaveType[0]));
            }
            return data;
        }
        #endregion

    }
}