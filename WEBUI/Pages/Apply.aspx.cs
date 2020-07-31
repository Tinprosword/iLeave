﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Apply : BLL.CustomLoginTemplate
    {
        //todo page页面的多层继承写的不错，可以总结下了。
        //todo type 多选。导致section多县。导致vlist可以修改？？？
        //todo 1.balance 2.sections 3.list's section. 4.upload pics. 5. 应该添加一个页面，让用户切换 employmentid.

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
            
            if (!loginer.userInfo.hasValidEmploynumber())
            {
                Response.Clear();
                Response.Write(LSLibrary.WebAPP.MyJSHelper.AlertMessageAndGoto("invalid employment ID,", "main.aspx"));
                Response.End();
            }
        }

        protected override void InitPageDataOnFirstLoad2()
        {
            initPageViewState();
        }

        protected override void ResetUIOnEachLoad3()
        {
            this.lt_AlertJS.Text = "";
            this.literal_errormsga.Text = "";
            this.literal_errormsga.Visible = false;
            this.repeater_leave.ItemDataBound += Repeater_leave_ItemDataBound;
        }

        private void Repeater_leave_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            DropDownList ddl = (DropDownList)item.FindControl("rp_dropdl_section");
            MODEL.Apply.apply_LeaveData itemdata = (MODEL.Apply.apply_LeaveData)item.DataItem;
            for (int i = 0; i < ddl.Items.Count;i++)
            {
                if (itemdata.sectionid.ToString()== ddl.Items[i].Value)
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
                //get viewstate value
                Apply_Upload prepage = PreviousPage as Apply_Upload;
                if (prepage != null)
                {
                    MODEL.Apply.ViewState_page applypage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, prepage.myviewState);
                    LSLibrary.WebAPP.ViewStateHelper.SetValue(applypage, ViewState_PageName, ViewState);
                    LoadUIFromViewState(applypage);
                }
            }
            else if (Request.QueryString["action"] != null && Request.QueryString["action"] == "backCalendar")
            {
                Pages.calendar prepage = PreviousPage as Pages.calendar;
                if(prepage!=null)
                {
                    MODEL.Apply.ViewState_page applypage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, prepage.myviewState);
                    LSLibrary.WebAPP.ViewStateHelper.SetValue(applypage, ViewState_PageName, ViewState);
                    LoadUIFromViewState(applypage);
                }
            }
            else
            {
                //init ui
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_current, "~/pages/main.aspx");
                this.literal_applier.Text = loginer.loginName + "  " + loginer.userInfo.employNnumber;

                DAL.WebReference_User.PersonBaseinfo baseinfos = BLL.User_wsref.GetPersonBaseinfoByEmploymentID(loginer.userInfo.id, (int)loginer.userInfo.employID);
                DAL.WebReference_leave.LeaveInfo[] res = BLL.Leave.GetLeaveInfoByStaffID((int)baseinfos.s_id);
                List<LSLibrary.WebAPP.ValueText<int>> typedata = BLL.Leave.ConvertLeaveInfo2DropDownList(res);
                LSLibrary.WebAPP.ValueTextHelper.BindDropdownlist<int>(this.ddl_leavetype, typedata);

                this.repeater_leave.DataSource = new List<MODEL.Apply.apply_LeaveData>();
                this.repeater_leave.DataBind();

                //set viewstate
                SavePageDataToViewState(false, true, false, null, typedata, null);
            }
            SetMultiLanguage();
        }

        private void LoadUIFromViewState(MODEL.Apply.ViewState_page applypage)
        {
            //init ui
            LSLibrary.WebAPP.ValueTextHelper.BindDropdownlist<int>(this.ddl_leavetype, applypage.leavetype);

            this.literal_applier.Text = loginer.loginName + "  " + loginer.userInfo.employNnumber;
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_current, "~/pages/main.aspx");
            this.ddl_leavetype.SelectedValue = applypage.LeaveTypeSelectValue.ToString();
            this.repeater_leave.DataSource = applypage.LeaveList;
            
            this.lt_applydays.Text = applypage.applylabel;
            this.lt_balancedays.Text = applypage.balancelabel;
            this.dropdl_section.SelectedValue = applypage.ddlsectionSelectvalue;
            this.tb_remarks.Text = applypage.remarks;

            this.repeater_leave.DataBind();
        }

        #endregion

        #region [module] on click upload pic
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            //save viewstate' other data
            SavePageDataToViewState(false,false,false,null,null,null);
        }

        
        #endregion

        #region [module] leave
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            if (ddl_leavetype.SelectedValue == "-1")
            {
                this.literal_errormsga.Text = "*";
            }
            else
            {
                SavePageDataToViewState(false, false, false, null, null, null);
                Server.Transfer("~/Pages/calendar.aspx?action=apply", false);
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

            string abc = senderObj.SelectedItem.Value;

            MODEL.Apply.ViewState_page pagedate = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            if (pagedate.LeaveList != null)
            {
                pagedate.LeaveList[intIndex].sectionid = int.Parse(abc);

                LSLibrary.WebAPP.ViewStateHelper.SetValue(pagedate, ViewState_PageName, ViewState);
                this.repeater_leave.DataSource = pagedate.LeaveList;
                this.repeater_leave.DataBind();
            }
        }
        #endregion

        #region [module] apply
        protected void button_apply_Click(object sender, EventArgs e)
        {
            //1,获得数据   2,调用ws,进行插入.  
            List<MODEL.Apply.apply_LeaveData> LeaveList = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState).LeaveList;
            string errorMsg = "";
            int reslut= BLL.Leave.InsertLeave(LeaveList, loginer.userInfo.id,(int)loginer.userInfo.employID,  null,this.tb_remarks.Text.Trim(),out errorMsg);
            if (reslut >= 0)
            {
                Response.Redirect("~/pages/main.aspx");
            }
            else
            {
                this.literal_errormsga.Visible = true;
                this.literal_errormsga.Text = "Error:"+errorMsg;
            }
        }
        #endregion

        #region [common function]
        private void SetMultiLanguage()
        {
            this.lt_name.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_name;
            this.lt_leave.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_leave;
            this.lt_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_apply;
            this.lt_balance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_banlance;
            this.lt_section.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_section;
            this.lt_remarks.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_remarks;
            this.ltlistdate.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_list_data;
            this.ltlisttype.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_list_type;
            this.lt_listsection.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_list_section;
            this.button_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_button;
        }

        private void initPageViewState()
        {
            MODEL.Apply.ViewState_page viewState_Page = new MODEL.Apply.ViewState_page();
            viewState_Page.LeaveList = new List<MODEL.Apply.apply_LeaveData>();
            viewState_Page.uploadpic = new List<MODEL.Apply.app_uploadpic>();
            viewState_Page.leavetype = new List<LSLibrary.WebAPP.ValueText<int>>();

            LSLibrary.WebAPP.ViewStateHelper.SetValue(viewState_Page, ViewState_PageName, ViewState);
        }

        private void SavePageDataToViewState(bool owlist,bool owtype,bool owpics, List<MODEL.Apply.apply_LeaveData> leavelist,List<LSLibrary.WebAPP.ValueText<int>> leavetype,List<MODEL.Apply.app_uploadpic> uploadPics)
        {
            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            applyPage.LeaveTypeSelectValue = this.ddl_leavetype.SelectedValue;
            applyPage.applylabel = this.lt_applydays.Text;
            applyPage.balancelabel = this.lt_balancedays.Text;
            applyPage.ddlsectionSelectvalue = this.dropdl_section.SelectedValue;
            applyPage.remarks = this.tb_remarks.Text;
            if (owlist)
            {
                applyPage.LeaveList = leavelist;
            }
            if (owtype)
            {
                applyPage.leavetype = leavetype;
            }
            if (owpics)
            {
                applyPage.uploadpic = uploadPics;
            }

            LSLibrary.WebAPP.ViewStateHelper.SetValue(applyPage, ViewState_PageName, ViewState);
        }
        #endregion
    }
}