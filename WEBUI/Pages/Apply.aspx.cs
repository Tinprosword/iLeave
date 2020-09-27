﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public static class ExtendPage
    {
        public static System.Diagnostics.StackFrame getStackFrame(this System.Web.UI.Page page)
        {
            return new System.Diagnostics.StackFrame(true);
        }
    }

    public partial class Apply : BLL.CustomLoginTemplate
    {
        //todo upload file in mobil.
        //todo 一个页面只能触发一次js？

        //todo how to get balance ?
        //todo how to deal with discard session when  freflsh ?

        //todo 需要download 附件.    

        //todo more than one sequance when approval??
        //todo page:change staff and member.
        //todo page页面的多层继承写的不错，可以总结下了。
        //todo employment hours

        public static string ViewState_PageName = "PageView";
        public List<LSLibrary.WebAPP.ValueText<int>> RPITEM_LeaveListSections;


        #region [page event]
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!loginer.userInfo.hasValidEmploynumber())
            {
                Response.Clear();
                Response.Write(LSLibrary.WebAPP.MyJSHelper.AlertMessageAndGoto("Invalid employmentID!,", "main.aspx"));
                Response.End();
            }
        }

        protected override void InitPage_OnFirstLoad2()
        {
            LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, new MODEL.Apply.ViewState_page(), ViewState);
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            this.lt_AlertJS.Text = "";
            this.literal_errormsga.Text = "";
            this.literal_errormsga.Visible = false;
            this.repeater_leave.ItemDataBound += Repeater_leave_ItemDataBound;
        }


        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            if (Request.QueryString["action"] != null && (Request.QueryString["action"] == "back" || Request.QueryString["action"] == "backCalendar"))
            {
                //1.get prepage's viewstate 2.set viewstate 3.loadUi.
                object preViewState = null;
                if (Request.QueryString["action"] == "back")
                {
                    preViewState = LSLibrary.WebAPP.PageSessionHelper.GetValueAndCleanSoon(BLL.GlobalVariate.Session_UploadToApply);
                }
                else if (Request.QueryString["action"] == "backCalendar")
                {
                    preViewState = LSLibrary.WebAPP.PageSessionHelper.GetValueAndCleanSoon(BLL.GlobalVariate.Session_CanlendarToApply);
                }
                LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, preViewState, ViewState);
                MODEL.Apply.ViewState_page applypage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, this.ViewState);
                LoadUI(applypage.leavetype, applypage.LeaveTypeSelectValue,  applypage.ddlsectionSelectvalue, applypage.remarks, applypage.LeaveList);
                IsLeaveTypeEnable();
            }
            else
            {
                List<WebServiceLayer.WebReference_leave.t_Leave> res = BLL.Leave.GetLeavesByStaffID((int)loginer.userInfo.staffid);
                List<LSLibrary.WebAPP.ValueText<int>> typedata = BLL.Leave.ConvertLeaveInfo2DropDownList(res);
                LoadUI(typedata, "0", "-1", "", new List<MODEL.Apply.apply_LeaveData>());
                //set viewstate
                SavePageDataToViewState(false, true, false, null, typedata, null);
            }
            SetMultiLanguage();
        }

        private void LoadUI(List<LSLibrary.WebAPP.ValueText<int>> leveTypeData,string leaveTypeSelectedValue,string ddlSectionSelected,string remarks,List<MODEL.Apply.apply_LeaveData> leaveDays)
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_current,"~/pages/main.aspx", true);

            this.literal_applier.Text = loginer.userInfo.GetDisplayName()+" (" + loginer.userInfo.employNnumber+")";

            LSLibrary.WebAPP.ValueTextHelper.BindDropdownlist<int>(this.ddl_leavetype, leveTypeData);
            this.ddl_leavetype.SelectedValue = leaveTypeSelectedValue;
            ddl_leavetype_SelectedIndexChanged(this.ddl_leavetype, new EventArgs());

            this.dropdl_section.SelectedValue = ddlSectionSelected;

            this.tb_remarks.Text = remarks;

            this.repeater_leave.DataSource = leaveDays;
            this.repeater_leave.DataBind();
        }



        private void Repeater_leave_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //1.clean and bind data DDL. 2. rp ddl 因为数据源不是固定的.所以
            RepeaterItem item = e.Item;
            DropDownList ddl = (DropDownList)item.FindControl("rp_dropdl_section");
            ddl.Items.Clear();

            int leaveid = int.Parse(this.ddl_leavetype.SelectedValue);
            if (RPITEM_LeaveListSections == null)
            {
                RPITEM_LeaveListSections = BLL.Leave.GetDDLSectionsDataNoSelect(leaveid, (int)loginer.userInfo.employID);
            }
            LSLibrary.WebAPP.ValueTextHelper.BindDropdownlist<int>(ddl, RPITEM_LeaveListSections);

            MODEL.Apply.apply_LeaveData itemdata = (MODEL.Apply.apply_LeaveData)item.DataItem;
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (itemdata.sectionid.ToString() == ddl.Items[i].Value)
                {
                    ddl.Items[i].Selected = true;
                }
                else
                {
                    ddl.Items[i].Selected = false;
                }
            }
        }

        #endregion

        #region [module] on click upload pic
        protected void Upload_Click(object sender, ImageClickEventArgs e)
        {
            //save viewstate' other data
            SavePageDataToViewState(false, false, false, null, null, null);
            LSLibrary.WebAPP.PageSessionHelper.SetValue(this.ViewState[ViewState_PageName], BLL.GlobalVariate.Session_ApplyToUpload);
            Response.Redirect("~/Pages/Apply_Upload.aspx",true);
        }
        #endregion

        #region [module] leave
        protected void Canlendar_Click(object sender, ImageClickEventArgs e)
        {
            if (ddl_leavetype.SelectedValue=="0" || dropdl_section.SelectedValue=="-1")
            {
                this.literal_errormsga.Text = BLL.GlobalVariate.msg_chooseLeaveAndSection;
                this.literal_errormsga.Visible = true;
            }
            else
            {
                SavePageDataToViewState(false, false, false, null, null, null);
                LSLibrary.WebAPP.PageSessionHelper.SetValue(this.ViewState[ViewState_PageName], BLL.GlobalVariate.Session_ApplyToCanlendar);
                Response.Redirect("~/Pages/calendar.aspx?action=apply", true);
            }
        }

        protected void delete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton senderObj = (ImageButton)sender;
            string strIndex = senderObj.CommandArgument;
            int intIndex = int.Parse(strIndex);
            int leaveid = int.Parse(this.ddl_leavetype.SelectedValue);

            MODEL.Apply.ViewState_page pagedate = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            if (pagedate.LeaveList != null)
            {
                pagedate.LeaveList.RemoveAt(intIndex);

                LSLibrary.WebAPP.ViewStateHelper.SetValue( ViewState_PageName, pagedate, ViewState);
                this.repeater_leave.DataSource = pagedate.LeaveList;
                this.repeater_leave.DataBind();
            }
            IsLeaveTypeEnable();
            RefleshApplyBalance(leaveid);
        }

        protected void ddl_leavetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            int leaveid = int.Parse(this.ddl_leavetype.SelectedValue);
            List<LSLibrary.WebAPP.ValueText<int>> ddlSource = BLL.Leave.GetDDLSectionsData(leaveid, (int)loginer.userInfo.employID);
            LSLibrary.WebAPP.ValueTextHelper.BindDropdownlist<int>(this.dropdl_section, ddlSource);

            RefleshApplyBalance(leaveid);
        }


        private void RefleshApplyBalance(int leaveid)
        {
            MODEL.Apply.ViewState_page pagedate = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);

            double applying = pagedate.getApplying();
            double grossvalue = BLL.Leave.GetGrossValue(leaveid, (int)loginer.userInfo.staffid,(int)loginer.userInfo.employID);
            double waiting = BLL.Leave.GetWaitValue(leaveid, (int)loginer.userInfo.staffid);
            double cleanvalue = grossvalue - waiting - applying;
            this.lt_balancedays.Text = cleanvalue.ToString("0.##") + " D (" + grossvalue.ToString("0.##") + "-" + waiting.ToString("0.##") + "-" + applying.ToString("0.##") + ")";
            this.lt_applydays.Text = applying.ToString("0.##") + " D";
        }

        protected void rp_dropdl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList senderObj = (DropDownList)sender;
            string strIndex = senderObj.Attributes["fix"];
            int intIndex = int.Parse(strIndex);
            int leaveid = int.Parse(this.ddl_leavetype.SelectedValue);

            string abc = senderObj.SelectedItem.Value;

            MODEL.Apply.ViewState_page pagedate = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            if (pagedate.LeaveList != null)
            {
                pagedate.LeaveList[intIndex].sectionid = int.Parse(abc);
                LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, pagedate, ViewState);
                //todo just refresh balance.

            }

            RefleshApplyBalance(leaveid);
        }

        
        #endregion

        #region [module] apply
        protected void button_apply_Click(object sender, EventArgs e)
        {
            //1,获得数据   2,调用ws,进行插入.  3.并把图片放置到制定目录，并插入到数据库
            List<MODEL.Apply.apply_LeaveData> LeaveList = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState).LeaveList;
            List<MODEL.Apply.app_uploadpic> pics = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState).uploadpic;
            string errorMsg = "";
            int reslut = BLL.Leave.InsertLeave(LeaveList, loginer.userInfo.id, (int)loginer.userInfo.employID, null, this.tb_remarks.Text.Trim(), ref errorMsg);
            if (reslut >= 0)
            {
                for (int i = 0; i < pics.Count; i++)
                {
                    copyFileTo(pics[i].bigImagepath, pics[i].bigImageAbsolutePath);
                }
                BLL.Leave.InsertAttachment(pics, loginer.userInfo.id, loginer.userInfo.personid, reslut);
                Response.Redirect("~/pages/main.aspx");
            }
            else
            {
                this.literal_errormsga.Visible = true;
                this.literal_errormsga.Text = "Error:" + errorMsg;
            }
        }

        private void copyFileTo(string filePath,string descPath)
        {
            string absfilepath = Server.MapPath(filePath);
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetParent(descPath).ToString());
            LSLibrary.FileUtil.Copy(absfilepath, descPath);
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

        private void SavePageDataToViewState(bool owlist, bool owtype, bool owpics, List<MODEL.Apply.apply_LeaveData> leavelist, List<LSLibrary.WebAPP.ValueText<int>> leavetype, List<MODEL.Apply.app_uploadpic> uploadPics)
        {
            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            applyPage.LeaveTypeSelectValue = this.ddl_leavetype.SelectedValue;

            applyPage.ddlsectionSelectvalue = this.dropdl_section.SelectedValue;
            BLL.common.WriteLog(this.getStackFrame(), "select:" + applyPage.ddlsectionSelectvalue);
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

            LSLibrary.WebAPP.ViewStateHelper.SetValue( ViewState_PageName, applyPage, ViewState);
        }

        private void IsLeaveTypeEnable()
        {
            if (repeater_leave.Items.Count > 0)
            {
                this.ddl_leavetype.Enabled = false;
            }
            else
            {
                this.ddl_leavetype.Enabled = true;
            }
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            
        }
        #endregion

    }
}