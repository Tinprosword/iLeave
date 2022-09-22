﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;

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
        private static string ViewState_PageName = "PageView";
        //repeater 中的leave section.因為不固定，所以，，，不記得為什麼要作為一個全局變量了。謹慎添加單獨的頁面全局變量，盡可能放入到ViewState_PageName中。保持程序簡潔性!!!!!!!
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
            this.literal_errormsga.Text = "";
            this.literal_errormsga.Visible = false;
            this.repeater_leave.ItemDataBound += Repeater_leave_ItemDataBound;
            this.lt_js_prg.Text = "";
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
                applypage.LeaveList = applypage.LeaveList.ToList();
                LoadUI(applypage.leavetype, applypage.LeaveTypeSelectValue, applypage.ddlsectionSelectvalue, applypage.remarks, applypage.LeaveList, applypage.GetAttachment().Count(), applypage.hasHour, applypage.bydayorHour, applypage.from, applypage.to, applypage.totalHours);
                IsLeaveTypeEnable();
                this.lt_js_prg.Text = LSLibrary.WebAPP.MyJSHelper.CustomPost("", "");//避免有害刷新，所以手动post,引导无害刷新。
            }
            else
            {
                List<WebServiceLayer.WebReference_leave.t_Leave> res = BLL.Leave.GetLeavesByStaffID((int)loginer.userInfo.staffid);
                List<LSLibrary.WebAPP.ValueText<int>> typedata = BLL.Leave.ConvertLeaveInfo2DropDownList(res);
                LoadUI(typedata, "0", "-1", "", new List<MODEL.Apply.apply_LeaveData>(), 0, false, 0, null, null, 0);
                //set viewstate
                SavePageDataToViewState(false, true, false, null, typedata, null);
            }
            SetMultiLanguage();
        }


        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
        }

        private void LoadUI(List<LSLibrary.WebAPP.ValueText<int>> leveTypeData, string leaveTypeSelectedValue, string ddlSectionSelected, string remarks, List<MODEL.Apply.apply_LeaveData> leaveDays, int numberofAttachment, bool hashour, int bydayorbyhout, DateTime? f, DateTime? t, double totalH)
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_current, "~/pages/main.aspx", true);

            int intNameType = BLL.CodeSetting.GetNameType(BLL.MultiLanguageHelper.GetChoose());

            MODEL.UserName tempUserName = new MODEL.UserName(loginer.userInfo.surname, loginer.userInfo.firstname, loginer.userInfo.nickname, loginer.userInfo.namech);
            this.literal_applier.Text = tempUserName.GetDisplayName(intNameType);

            LSLibrary.WebAPP.ValueTextHelper.BindDropdownlist<int>(this.ddl_leavetype, leveTypeData);
            this.ddl_leavetype.SelectedValue = leaveTypeSelectedValue;


            LoadLeaveSectionAndTime(int.Parse(this.ddl_leavetype.SelectedValue), bydayorbyhout, f, t, totalH, ddlSectionSelected);
            RefleshApplyBalance(int.Parse(this.ddl_leavetype.SelectedValue));

            string numberPath = BLL.common.GetAttachmentNumberPath(numberofAttachment);
            this.ib_counta.ImageUrl = numberPath;
            this.ib_counta.Visible = !string.IsNullOrEmpty(numberPath);

            this.tb_remarks.Text = remarks;

            this.repeater_leave.DataSource = leaveDays;
            this.repeater_leave.DataBind();

            //this.button_apply.OnClientClick = "return ShowMessage('" + BLL.MultiLanguageHelper.GetLanguagePacket().submit_success + "','aa')";//OnClientClick='return ShowMessage(<%=BLL.GlobalVariate.submit_success%>,"aa")'
        }





        private void Repeater_leave_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //1.clean and bind data DDL. 2. rp ddl 因为数据源不是固定的.所以
            RepeaterItem item = e.Item;
            MODEL.Apply.apply_LeaveData itemdata = (MODEL.Apply.apply_LeaveData)item.DataItem;

            DropDownList ddl = (DropDownList)item.FindControl("rp_dropdl_section");
            Label lb = (Label)item.FindControl("rp_lb_byhour");

            if (itemdata.byDaybyHour == 0)
            {
                ddl.Visible = true;
                lb.Visible = false;

                ddl.Items.Clear();

                int leaveid = int.Parse(this.ddl_leavetype.SelectedValue);
                if (RPITEM_LeaveListSections == null)
                {
                    RPITEM_LeaveListSections = BLL.Leave.GetDDLSectionsDataNoSelect(leaveid, (int)loginer.userInfo.employID);
                }
                LSLibrary.WebAPP.ValueTextHelper.BindDropdownlist<int>(ddl, RPITEM_LeaveListSections);


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
            else
            {
                ddl.Visible = false;
                lb.Visible = true;
                lb.Text = MODEL.CLOT.CLOTItem.GetTimeRangeDesc(itemdata.LeaveHourFrom.Value.Hour, itemdata.LeaveHourTo.Value.Hour, itemdata.LeaveHourFrom.Value.Minute, itemdata.LeaveHourTo.Value.Minute);
            }
        }

        #endregion

        #region [module] on click upload pic
        //1.放整页的数据到session.2.
        protected void Upload_Click(object sender, ImageClickEventArgs e)
        {
            //save viewstate' other data
            SavePageDataToViewState(false, false, false, null, null, null);
            LSLibrary.WebAPP.PageSessionHelper.SetValue(this.ViewState[ViewState_PageName], BLL.GlobalVariate.Session_ApplyToUpload);
            string url = "~/Pages/Apply_Upload.aspx?{0}={1}&{2}={3}&{4}={5}";
            string backurl = System.Web.HttpUtility.UrlEncode("~/pages/Apply.aspx?action=back");
            url = string.Format(url, Apply_Upload.url_GetsessionName, BLL.GlobalVariate.Session_ApplyToUpload, Apply_Upload.url_BacksessionName, BLL.GlobalVariate.Session_UploadToApply, Apply_Upload.url_backUrlname, backurl);
            Response.Redirect(url, true);
        }
        #endregion

        //0day 1.hour
        private int GetByDayOrHourFromUI()
        {
            int result = 0;
            if (tr_radio.Visible == false)
            {
                result = 0;
            }
            else
            {
                int radioSelecte = int.Parse(this.radio_ishour.SelectedValue);
                result = radioSelecte == 0 ? 0 : 1;
            }

            return result;
        }

        #region [module] leave
        private bool CheckUIOnCalendar()
        {
            bool result = false;
            int dayOrHour = GetByDayOrHourFromUI();
            bool dayisNotOK = (dayOrHour == 0 && (ddl_leavetype.SelectedValue == "0" || dropdl_section.SelectedValue == "-1"));
            bool totalisok = LSLibrary.CString.isDouble(this.tb_total.Text);
            bool hourIsNotOK = (dayOrHour == 1 && (ddl_leavetype.SelectedValue == "0" || this.tb_total.Text == "" || this.tb_total.Text == "0" || totalisok == false));

            if (dayisNotOK || hourIsNotOK)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        protected void Canlendar_Click(object sender, ImageClickEventArgs e)
        {
            int dayOrHour = GetByDayOrHourFromUI();

            bool checkUIInput = CheckUIOnCalendar();

            if (checkUIInput == false)
            {
                this.literal_errormsga.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_msgselect;
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

                LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, pagedate, ViewState);
                this.repeater_leave.DataSource = pagedate.LeaveList;
                this.repeater_leave.DataBind();
            }
            IsLeaveTypeEnable();
            RefleshApplyBalance(leaveid);
            this.lt_js_prg.Text = LSLibrary.WebAPP.MyJSHelper.CustomPost("", "");
        }

        protected void ddl_leavetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            int leaveid = int.Parse(this.ddl_leavetype.SelectedValue);
            if (leaveid == 0)
            {
                LoadLeaveSectionAndTime(leaveid, 0, null, null, 0, "-1");
            }
            else
            {
                LoadLeaveSectionAndTime(leaveid, 0, null, null, 0, "0");
            }
            RefleshApplyBalance(leaveid);
        }

        private void LoadTime_init()
        {
            DropDownList1.Items.Clear();
            DropDownList3.Items.Clear();
            for (int i = 0; i < 24; i++)
            {
                this.DropDownList1.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
                this.DropDownList3.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
            }

            DropDownList2.Items.Clear();
            DropDownList4.Items.Clear();

            int hourUnit = BLL.SystemParameters.GetSysParameters().mLeaveHourUnit;
            if (hourUnit == 15)
            {
                this.DropDownList2.Items.Add(new ListItem(0.ToString("00"), 0.ToString()));
                this.DropDownList2.Items.Add(new ListItem(15.ToString("00"), 15.ToString()));
                this.DropDownList2.Items.Add(new ListItem(30.ToString("00"), 30.ToString()));
                this.DropDownList2.Items.Add(new ListItem(45.ToString("00"), 45.ToString()));

                this.DropDownList4.Items.Add(new ListItem(0.ToString("00"), 0.ToString()));
                this.DropDownList4.Items.Add(new ListItem(15.ToString("00"), 15.ToString()));
                this.DropDownList4.Items.Add(new ListItem(30.ToString("00"), 30.ToString()));
                this.DropDownList4.Items.Add(new ListItem(45.ToString("00"), 45.ToString()));
            }
            else if (hourUnit == 30)
            {
                this.DropDownList2.Items.Add(new ListItem(0.ToString("00"), 0.ToString()));
                this.DropDownList2.Items.Add(new ListItem(30.ToString("00"), 30.ToString()));

                this.DropDownList4.Items.Add(new ListItem(0.ToString("00"), 0.ToString()));
                this.DropDownList4.Items.Add(new ListItem(30.ToString("00"), 30.ToString()));
            }
            else
            {
                this.DropDownList2.Items.Add(new ListItem(0.ToString("00"), 0.ToString()));
                this.DropDownList2.Items.Add(new ListItem(15.ToString("00"), 15.ToString()));
                this.DropDownList2.Items.Add(new ListItem(30.ToString("00"), 30.ToString()));
                this.DropDownList2.Items.Add(new ListItem(45.ToString("00"), 45.ToString()));

                this.DropDownList4.Items.Add(new ListItem(0.ToString("00"), 0.ToString()));
                this.DropDownList4.Items.Add(new ListItem(15.ToString("00"), 15.ToString()));
                this.DropDownList4.Items.Add(new ListItem(30.ToString("00"), 30.ToString()));
                this.DropDownList4.Items.Add(new ListItem(45.ToString("00"), 45.ToString()));
            }
        }


        private void LoadLeaveSectionAndTime(int leaveID, int byDayByhour, DateTime? from, DateTime? to, double total, string Selectsection)
        {
            //1.leaveid=0==>init  2.
            List<LSLibrary.WebAPP.ValueText<int>> ddlSource = BLL.Leave.GetDDLSectionsData(leaveID, (int)loginer.userInfo.employID);
            LSLibrary.WebAPP.ValueTextHelper.BindDropdownlist<int>(this.dropdl_section, ddlSource);

            LSLibrary.WebAPP.ValueTextHelper.BindRadiolist<int>(this.radio_ishour, BLL.Leave.GetRadioData());


            LoadTime_init();

            bool AllowHour = false;
            var tempaaa = BLL.User_wsref.GetPersonBaseInfoByPid(loginer.userInfo.personid);
            if (tempaaa != null && tempaaa.Count() > 0)
            {
                AllowHour = BLL.Leave.AllowHour(leaveID, tempaaa[0].s_PositionID ?? 0);
            }


            if (leaveID == 0 || AllowHour == false)
            {
                //hiden radio. hiden time
                tr_radio.Visible = false;
                tr_section.Visible = true;
                tr_time.Visible = false;
            }
            else
            {
                //show radio. default show setcion
                tr_radio.Visible = true;
                tr_section.Visible = true;
                tr_time.Visible = false;
            }

            this.dropdl_section.SelectedValue = Selectsection;

            //fill data
            if (AllowHour)
            {
                this.radio_ishour.SelectedValue = byDayByhour.ToString();
                if (this.radio_ishour.SelectedValue == "1" && from != null && to != null)
                {
                    this.tr_time.Visible = true;
                    this.tr_section.Visible = false;

                    this.DropDownList1.SelectedValue = from.Value.Hour.ToString();
                    this.DropDownList2.SelectedValue = from.Value.Minute.ToString();
                    this.DropDownList3.SelectedValue = to.Value.Hour.ToString();
                    this.DropDownList4.SelectedValue = to.Value.Minute.ToString();

                    this.tb_total.Text = total.ToString();
                }
                else
                {
                    this.tr_time.Visible = false;
                    this.tr_section.Visible = true;
                }
            }
        }

        private void RefleshApplyBalance(int leaveid)
        {
            if (leaveid == BLL.Leave.leave_leaveid_nullSelect)
            {
                this.lt_balancedays.Text = "";
                this.lt_applydays.Text = "";
            }
            else
            {
                MODEL.Apply.ViewState_page pagedate = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);

                double applying = pagedate.getApplying();

                BLL.Leave.UpdateTodayLeaveBalanceToTable(loginer.userInfo.employID ?? 0);

                double cleanValue = BLL.Leave.GetAailabeValue_substractFutherAndWait(leaveid, (int)loginer.userInfo.staffid, (int)loginer.userInfo.employID);

                this.lt_balancedays.Text = cleanValue == -99999 ? "--" : cleanValue.ToString("0.##") + " " + BLL.MultiLanguageHelper.GetLanguagePacket().Common_D;
                this.lt_applydays.Text = applying.ToString("0.##") + " " + BLL.MultiLanguageHelper.GetLanguagePacket().Common_D;
            }
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
            string waitCode = LSLibrary.WebAPP.httpHelper.WaitDiv_show(BLL.MultiLanguageHelper.GetLanguagePacket().Commonsubmit_success);
            Response.Write(waitCode);
            Response.Flush();

            //1,获得数据   2,调用ws,进行插入.  3.并把图片放置到制定目录，并插入到数据库
            List<MODEL.Apply.apply_LeaveData> LeaveList = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState).LeaveList;
            List<MODEL.App_AttachmentInfo> pics = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState).GetAttachment();
            string errorMsg = "";
            bool hasAttachment = false;
            if (pics != null && pics.Count() > 0)
            {
                hasAttachment = true;
            }
            int reslut = BLL.Leave.InsertLeave(LeaveList, loginer.userInfo.id, (int)loginer.userInfo.employID, null, this.tb_remarks.Text.Trim(), ref errorMsg, loginer.userInfo.firsteid ?? 0, hasAttachment);
            if (reslut >= 0)
            {
                for (int i = 0; i < pics.Count; i++)
                {
                    BLL.common.copyFileTo(pics[i].originAttendance_RelatePath, pics[i].originAttendance_HRDBPath, Server);
                }
                BLL.Leave.InsertAttachment(pics, loginer.userInfo.id, loginer.userInfo.personid, reslut, BLL.GlobalVariate.AttachmentUploadType.LEAVE_CERTIFICATE, BLL.GlobalVariate.WorkflowTypeID.LEAVE_APPLICATION);

                string successMsg = LSLibrary.WebAPP.httpHelper.WaitDiv_EndShow(BLL.MultiLanguageHelper.GetLanguagePacket().apply_msgapplyok);
                Response.Write(successMsg + ".");
                Response.Flush();
                System.Threading.Thread.Sleep(50);//休眠2秒,获得较好显示体验

                this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                this.PreRenderComplete += Apply_PreRenderComplete;
            }
            else
            {
                this.literal_errormsga.Visible = true;
                this.literal_errormsga.Text = errorMsg;
                this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
            }
        }

        private void Apply_PreRenderComplete(object sender, EventArgs e)
        {
            Apply page = (Apply)sender;
            page.Response.Clear();
            page.Response.Write(LSLibrary.WebAPP.MyJSHelper.Goto("approval_wait.aspx?action=1&applicationtype=0&from=0"));
            page.Response.End();
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
            this.lt_new.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_new;
            this.lt_mypending.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_pending;
            this.lt_myhistory.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_processed;
            this.btn_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_button;

            this.lt_estimation.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_estimation;
            this.lt_time.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_time;


        }

        private void SavePageDataToViewState(bool owlist, bool owtype, bool owpics, List<MODEL.Apply.apply_LeaveData> leavelist, List<LSLibrary.WebAPP.ValueText<int>> leavetype, List<MODEL.App_AttachmentInfo> uploadPics)
        {
            //1.get viewstate 2.save 
            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);

            applyPage.LeaveTypeSelectValue = this.ddl_leavetype.SelectedValue;

            applyPage.hasHour = this.tr_radio.Visible;
            int byhourOrDAY = GetByDayOrHourFromUI();
            if (byhourOrDAY == 0)
            {
                applyPage.ddlsectionSelectvalue = this.dropdl_section.SelectedValue;
                applyPage.bydayorHour = 0;
                applyPage.from = null;
                applyPage.to = null;
                applyPage.totalHours = 0;
            }
            else
            {
                applyPage.ddlsectionSelectvalue = "0";
                applyPage.bydayorHour = byhourOrDAY;
                applyPage.from = new DateTime(1900, 1, 1, int.Parse(this.DropDownList1.SelectedValue), int.Parse(DropDownList2.SelectedValue), 0);
                applyPage.to = new DateTime(1900, 1, 1, int.Parse(this.DropDownList3.SelectedValue), int.Parse(DropDownList4.SelectedValue), 0);
                applyPage.totalHours = double.Parse(this.tb_total.Text);
            }
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
                applyPage.SetAttachment(uploadPics);
            }



            LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, applyPage, ViewState);
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


        //js传递apply sum 标签的值 ,如果为空表示没有做任何处理 . 否则有数据,那么传递不同的参数向js function.
        //checkNewTab: alter message ,action(ismanage),bigrange(pengding,histroy),from (0:leave 1 colot)
        public string showPendEvent()
        {
            if (!IsPostBack)
            {
                return "return checkNewTab('',1,0,0)";
            }
            else
            {

                var clientType = LSLibrary.WebAPP.MobilWebHelper.GetClientType(HttpContext.Current.Request.UserAgent);
                var cookies = BLL.Page.MyCookieManage.GetCookie();

                if (clientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone && cookies.isAppLogin == "1" && BLL.GlobalVariate.iosDebug)
                {
                    return "return checkNewTab('',1,1,0)";
                }
                else
                {
                    return "return checkNewTab('" + BLL.MultiLanguageHelper.GetLanguagePacket().apply_msg_tab + "',1,0,0)";
                }


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

                var clientType = LSLibrary.WebAPP.MobilWebHelper.GetClientType(HttpContext.Current.Request.UserAgent);
                var cookies = BLL.Page.MyCookieManage.GetCookie();

                if (clientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone && cookies.isAppLogin == "1" && BLL.GlobalVariate.iosDebug)
                {
                    return "return checkNewTab('',1,3,0)";
                }
                else
                {
                    return "return checkNewTab('" + BLL.MultiLanguageHelper.GetLanguagePacket().apply_msg_tab + "',1,3,0)";
                }

            }
        }

        public string showesEvent()
        {
            if (!IsPostBack)
            {
                return "return checkOtherPage('','estimation.aspx')";
            }
            else
            {
                var clientType = LSLibrary.WebAPP.MobilWebHelper.GetClientType(HttpContext.Current.Request.UserAgent);
                var cookies = BLL.Page.MyCookieManage.GetCookie();

                if (clientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone && cookies.isAppLogin == "1" && BLL.GlobalVariate.iosDebug)
                {
                    return "return checkOtherPage('','estimation.aspx')";
                }
                else
                {
                    return "return checkOtherPage('" + BLL.MultiLanguageHelper.GetLanguagePacket().apply_msg_tab + "','estimation.aspx')";
                }

            }
        }


        #endregion

        #region keep value for ajax
        protected void Page_PreRender()
        {
            MODEL.Apply.ViewState_page pagedate = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            MODEL.Apply.ajax_data_apply ajaxdata = new MODEL.Apply.ajax_data_apply();
            ajaxdata.pagedata = pagedate;
            ajaxdata.loginid = loginer.userInfo.employID ?? 0;
            ((WEBUI.Controls.leave)this.Master).SetPageState(LSLibrary.MyJson.SObj(ajaxdata));
        }

        #endregion

        protected void radio_ishour_SelectedIndexChanged(object sender, EventArgs e)
        {
            int hourOrDay = int.Parse(this.radio_ishour.SelectedValue);
            if (hourOrDay == 0)
            {
                this.tr_section.Visible = true;
                this.tr_time.Visible = false;
            }
            else
            {
                this.tr_section.Visible = false;
                this.tr_time.Visible = true;
            }
        }

        protected void DropDownList1_TextChanged(object sender, EventArgs e)
        {
            int fromh = int.Parse(this.DropDownList1.SelectedValue);
            int fromm = int.Parse(this.DropDownList2.SelectedValue);
            int toh = int.Parse(this.DropDownList3.SelectedValue);
            int tom = int.Parse(this.DropDownList4.SelectedValue);


            double totalHours = BLL.Leave.GetRealTotalHours(fromh, toh, fromm, tom, loginer.userInfo.employID ?? 0,-1);

            this.tb_total.Text = totalHours.ToString();
        }
    }
}