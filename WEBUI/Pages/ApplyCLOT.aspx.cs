using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//todo 0  申请一个，好像 waitBalance 不会加上去??
//clot 计划结余是：正在申请+wait.   balance:没有today还是future 的概念。都是future.
namespace WEBUI.Pages
{
    public partial class ApplyCLOT : BLL.CustomLoginTemplate
    {
        private readonly string NAME_OF_PAGE_VIEW = "NAME_OF_PAGE_VIEW";

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
            this.lt_js_prg.Text = "";
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            LSLibrary.WebAPP.ViewStateHelper.SetValue(NAME_OF_PAGE_VIEW, new MODEL.CLOT.ViewState_page(), this.ViewState);

            MulLanguage();

            if (Request.QueryString["action"] != null && (Request.QueryString["action"] == "back"))
            {
                //1.get prepage's viewstate 2.set viewstate 3.loadUi.
                object preViewState = null;
                if (Request.QueryString["action"] == "back")
                {
                    preViewState = LSLibrary.WebAPP.PageSessionHelper.GetValueAndCleanSoon(BLL.GlobalVariate.Session_uploadtoclot);
                }

                LSLibrary.WebAPP.ViewStateHelper.SetValue(NAME_OF_PAGE_VIEW, preViewState, ViewState);

                LoadUI();
                LoadUIFromViewState_WhenBackFromOtherPage();
                this.lt_js_prg.Text = LSLibrary.WebAPP.MyJSHelper.CustomPost("", "");//避免有害刷新，所以手动post,引导无害刷新。
                
            }
            else
            {
                LoadUI();
            }


            
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

            this.btn_add.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_ADD;
            this.btn_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_Submit;

            this.lt_hours.Text= BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_NOOFHours;
        }

        private void GetCLOTSection(out int cls, out int ots)
        {
            cls = (int)BLL.GlobalVariate.CLSection.none;
            ots = (int)BLL.GlobalVariate.OTSection.none;

            var allPositions = BLL.Other.GetPositions();
            var myEmploymentInfo = BLL.User_wsref.getEmploymentByid(loginer.userInfo.employID ?? 0);

            WebServiceLayer.WebReference_leave.PositionInfo loginerPosition = null;
            if (allPositions != null)
            {
                var tempP = allPositions.Where(x => x.ID == myEmploymentInfo.PositionID).ToList();
                if (tempP != null && tempP.Count() == 1)
                {
                    loginerPosition = tempP[0];
                }
            }
            if (loginerPosition != null)
            {
                cls = loginerPosition.minCLType;
                ots = loginerPosition.minOTType;
            }

        }

        private void LoadUI()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().main_applyCLOT, "~/pages/main.aspx", true);

            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);
            GetCLOTSection(out dataview.clSectionType_InitOnpageload_UpdateAlways, out dataview.otSectionType_InitOnpageload_UpdateAlways);
            LSLibrary.WebAPP.ViewStateHelper.SetValue(NAME_OF_PAGE_VIEW, dataview, this.ViewState);

            int intNameType = BLL.CodeSetting.GetNameType(BLL.MultiLanguageHelper.GetChoose());
            MODEL.UserName tempUserName = new MODEL.UserName(loginer.userInfo.surname, loginer.userInfo.firstname, loginer.userInfo.nickname, loginer.userInfo.namech);
            this.literal_applier.Text = tempUserName.GetDisplayName(intNameType);

            var ddldata = LSLibrary.EnumHelper.GetValueText_multipLanguage(typeof(MODEL.CLOT.enum_clotType),BLL.MultiLanguageHelper.GetChoose());
            this.ddl_leavetype.DataSource = ddldata;
            this.ddl_leavetype.DataTextField = LSLibrary.EnumHelper.NAME_Enum_valueTextDesc_TEXT;
            this.ddl_leavetype.DataValueField = LSLibrary.EnumHelper.NAME_Enum_valueTextDesc_VALUE;
            this.ddl_leavetype.DataBind();

            this.lt_applydays.Text = "--";
            this.lt_balancedays.Text = "--";

            double balanceValue = BLL.Leave.GetBalanceView_CLOT_balance(loginer.userInfo.employID??0);
            this.lt_balancedays.Text = (balanceValue).ToString("0.##") + " " + BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_list_Hours2;
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
            bool onlyHalfHour = BLL.SystemParameters.mCLOTOnlyHalfHours();
            
            if (onlyHalfHour && (dataview.otSectionType_InitOnpageload_UpdateAlways==(int)BLL.GlobalVariate.OTSection.none || dataview.otSectionType_InitOnpageload_UpdateAlways == (int)BLL.GlobalVariate.OTSection.hour)  && (dataview.clSectionType_InitOnpageload_UpdateAlways == (int)BLL.GlobalVariate.CLSection.none || dataview.clSectionType_InitOnpageload_UpdateAlways == (int)BLL.GlobalVariate.CLSection.hour))
            {
                this.DropDownList2.Items.Add(new ListItem(0.ToString("00"), 0.ToString()));
                this.DropDownList2.Items.Add(new ListItem(30.ToString("00"), 30.ToString()));

                this.DropDownList4.Items.Add(new ListItem(0.ToString("00"), 0.ToString()));
                this.DropDownList4.Items.Add(new ListItem(30.ToString("00"), 30.ToString()));
            }
            else
            {
                for (int i = 0; i < 60; i++)
                {
                    this.DropDownList2.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
                    this.DropDownList4.Items.Add(new ListItem(i.ToString("00"), i.ToString()));
                }
            }

            this.tb_remarks.Text=GetDefaultRemark();

            //section 控件必须等type ,hours, totalHouse 等控件初始化之后才进行。所以放到这里。
            ProcessSectinInfo((MODEL.CLOT.enum_clotType)int.Parse(this.ddl_leavetype.SelectedValue),(BLL.GlobalVariate.CLSection) dataview.clSectionType_InitOnpageload_UpdateAlways,(BLL.GlobalVariate.OTSection) dataview.otSectionType_InitOnpageload_UpdateAlways);

            int numberofAttachment = dataview.GetAttachment().Count();
            string numberPath = BLL.common.GetAttachmentNumberPath(numberofAttachment);
            this.ib_counta.ImageUrl = numberPath;
            this.ib_counta.Visible = !string.IsNullOrEmpty(numberPath);

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
            tempItem.numberofHours = this.tb_hours.Text;

            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);
            validData = BLL.CLOT.CheckOnAddSingleItem(tempItem,dataview,loginer.userInfo.employID??0);
            if (validData > 0 && bvalidday)
            {
                //还需要检测此条加入后的有效性。复用call onclicapply 的验证。从逻辑上来说是可以复用。如果后期出现onClickApply 单独的检测，先判断
                //是否逻辑不清晰，真有必要，onClickApply 可以用2个方法。复用是必须的。
                List<MODEL.CLOT.CLOTItem> tempList = new List<MODEL.CLOT.CLOTItem>();
                tempList.AddRange(dataview.items);
                tempList.Add(tempItem);

                double balanceValue = BLL.Leave.GetBalanceView_CLOT_balance(loginer.userInfo.employID ?? 0);
                var einfo = BLL.User_wsref.getEmploymentByid(loginer.userInfo.employID ?? 0);
                string errormsg= BLL.CLOT.CheckOnApplyList(tempList, balanceValue, loginer.userInfo.employID ?? 0,BLL.MultiLanguageHelper.GetChoose(),einfo.ApprovalGroupID??0);
                if (!string.IsNullOrEmpty(errormsg))
                {
                    literal_errormsga.Visible = true;
                    literal_errormsga.Text = errormsg;
                }
                else
                {
                    dataview.items.Add(tempItem);

                    LSLibrary.WebAPP.ViewStateHelper.SetValue(NAME_OF_PAGE_VIEW, dataview, this.ViewState);
                }
            }
            else if (validData == -1)
            {
                literal_errormsga.Visible = true;
                literal_errormsga.Text = BLL.MultiLanguageHelper.GetLanguagePacket().common_msg_bigzero;
            }
            else if (validData == -2)
            {
                literal_errormsga.Visible = true;
                literal_errormsga.Text = BLL.MultiLanguageHelper.GetLanguagePacket().common_msg_numbertyp;
            }
            else if (validData == -3)
            {
                literal_errormsga.Visible = true;
                literal_errormsga.Text = BLL.MultiLanguageHelper.GetLanguagePacket().common_msg_overlap;
            }
            else if (validData == -4)
            {
                literal_errormsga.Visible = true;
                literal_errormsga.Text = BLL.MultiLanguageHelper.GetLanguagePacket().common_msg_overlapCurrentapplying;
            }

            SetupReport();
            RefleshApplyBalance();
        }

        

        
        

        protected void delete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imageButton = (ImageButton)sender;
            string strIndex = imageButton.CommandArgument;

            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);

            dataview.items.RemoveAt(int.Parse(strIndex));

            LSLibrary.WebAPP.ViewStateHelper.SetValue(NAME_OF_PAGE_VIEW, dataview, this.ViewState);

            SetupReport();
            RefleshApplyBalance();
        }

        protected void btn_apply_Click(object sender, EventArgs e)
        {
            //1.check valid data.2.insert each request.
            string waitCode = LSLibrary.WebAPP.httpHelper.WaitDiv_show(BLL.MultiLanguageHelper.GetLanguagePacket().Commonsubmit_success);
            Response.Write(waitCode);
            Response.Flush();


            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);
            double balanceValue = BLL.Leave.GetBalanceView_CLOT_balance(loginer.userInfo.employID ?? 0);
            var einfo= BLL.User_wsref.getEmploymentByid(loginer.userInfo.employID??0);
            string errorMsg = BLL.CLOT.CheckOnApplyList(dataview.items, balanceValue, loginer.userInfo.employID ?? 0, BLL.MultiLanguageHelper.GetChoose(),einfo.ApprovalGroupID??0);
            bool validData = string.IsNullOrEmpty(errorMsg);
            if (validData)
            {
                List<int> requestidArray = BLL.CLOT.InsertCLOTRequests(dataview.items, loginer.userInfo.id, loginer.userInfo.employID ?? 0);

                bool hasError = requestidArray.Where(x => x <= 0).ToList().Count() > 0 ? true : false;
                if (hasError)
                {
                    this.literal_errormsga.Visible = true;
                    this.literal_errormsga.Text = "Some data is error on insert.";
                    this.js_waitdiv.Text = LSLibrary.WebAPP.httpHelper.WaitDiv_close();
                    return;
                }

                //insert attachment
                List<MODEL.App_AttachmentInfo> pics = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.IPage_Attachment>(NAME_OF_PAGE_VIEW, ViewState).GetAttachment();
                for (int i = 0; i < pics.Count; i++)
                {
                    BLL.common.copyFileTo(pics[i].originAttendance_RelatePath, pics[i].originAttendance_HRDBPath, Server);
                }
                for (int i = 0; i < requestidArray.Count; i++)
                {
                    BLL.Leave.InsertAttachment(pics, loginer.userInfo.id, loginer.userInfo.personid, requestidArray[i], BLL.GlobalVariate.AttachmentUploadType.LEAVE_CERTIFICATE, BLL.GlobalVariate.WorkflowTypeID.CLOT_APPLICATION);
                }

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
            ApplyCLOT page = (ApplyCLOT)sender;
            page.Response.Clear();
            page.Response.Write(LSLibrary.WebAPP.MyJSHelper.Goto("approval_wait.aspx?action=1&applicationtype=0&from=1"));
            page.Response.End();
        }

        private string GetDefaultRemark()
        {
            MODEL.CLOT.enum_clotType tt = GetCLOTType();
            if (tt == MODEL.CLOT.enum_clotType.CL)
            {
                return BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_REMARK_CL;
            }
            else
            {
                return BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_REMARK_OT;
            }
        }

        private MODEL.CLOT.enum_clotType GetCLOTType()
        {
            int type = 0;
            string str = this.ddl_leavetype.SelectedValue;
            int.TryParse(str, out type);
            if (type == (int)MODEL.CLOT.enum_clotType.CL)
            {
                return MODEL.CLOT.enum_clotType.CL;
            }
            else
            {
                return MODEL.CLOT.enum_clotType.OT;
            }
        }

        protected void ddl_leavetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tb_remarks.Text= GetDefaultRemark();
            MODEL.CLOT.enum_clotType tt = GetCLOTType();

            var pageData = (MODEL.CLOT.ViewState_page)LSLibrary.WebAPP.ViewStateHelper.GetValue(NAME_OF_PAGE_VIEW, this.ViewState);
            ProcessSectinInfo(tt,(BLL.GlobalVariate.CLSection)pageData.clSectionType_InitOnpageload_UpdateAlways,(BLL.GlobalVariate.OTSection)pageData.otSectionType_InitOnpageload_UpdateAlways);
        }

        private void ProcessSectinInfo(MODEL.CLOT.enum_clotType clOrOT,BLL.GlobalVariate.CLSection cLSection,BLL.GlobalVariate.OTSection oTSection)
        {
            //1.init secion 2. set defaul item 3. onSectionChanged.
            int sectionType = 0;//0 hour, 1,half 2.fullday.
            if (clOrOT == MODEL.CLOT.enum_clotType.OT)
            {
                if (oTSection == BLL.GlobalVariate.OTSection.half)
                {
                    sectionType = 1;
                }
                else if (oTSection == BLL.GlobalVariate.OTSection.full)
                {
                    sectionType = 2;
                }
            }
            else if (clOrOT == MODEL.CLOT.enum_clotType.CL)
            {
                if (cLSection == BLL.GlobalVariate.CLSection.half)
                {
                    sectionType = 1;
                }
                else if (cLSection == BLL.GlobalVariate.CLSection.full)
                {
                    sectionType = 2;
                }
            }

            this.tr_secion.Visible = false;
            if (sectionType == 1 || sectionType == 2)
            {
                this.tr_secion.Visible = true;
                if (sectionType == 2)
                {
                    this.ddl_section.Items.Clear();
                    this.ddl_section.Items.Add(new ListItem("Full", "0"));
                    this.ddl_section.SelectedIndex = 0;
                }

                if (sectionType == 1)
                {
                    this.ddl_section.Items.Clear();
                    this.ddl_section.Items.Add(new ListItem("Full", "0"));
                    this.ddl_section.Items.Add(new ListItem("AM", "1"));
                    this.ddl_section.Items.Add(new ListItem("PM", "2"));
                    this.ddl_section.SelectedIndex = 0;
                }
            }
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
                return "return checkNewTab('',1,3,1)";
            }
            else
            {
                return "return checkNewTab('" + BLL.MultiLanguageHelper.GetLanguagePacket().apply_msg_tab + "',1,3,1)";
            }
        }


        private void RefleshApplyBalance()
        {
            double waitingValue = BLL.Leave.GetBalanceView_CLOT_Wait(loginer.userInfo.employID??0);

            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);
            if (dataview != null && dataview.items != null)
            {
                double totalHour = MODEL.CLOT.CLOTItem.GetTotalUnit(dataview.items);

                totalHour = waitingValue + totalHour;

                this.lt_applydays.Text = totalHour.ToString()+" "+ BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_list_Hours2;
            }
            else
            {
                this.lt_applydays.Text = "--";
            }
        }

        //RE-Calculate number of hours.
        protected void OnFromTO_TextChanged(object sender, EventArgs e)
        {
            int fromh = int.Parse(this.DropDownList1.SelectedValue);
            int fromm = int.Parse(this.DropDownList2.SelectedValue);
            int toh = int.Parse(this.DropDownList3.SelectedValue);
            int tom = int.Parse(this.DropDownList4.SelectedValue);


            DateTime theday = System.DateTime.Now;
            bool bvalidday = DateTime.TryParse(this.tb_date.Text, out theday);
            if (bvalidday)
            {
                double hours = BLL.Leave.GetRealTotalHours(fromh, toh, fromm, tom, loginer.userInfo.employID??0);
                this.tb_hours.Text = hours.ToString();
            }
        }

        protected void image_btn_Click(object sender, ImageClickEventArgs e)
        {
            SaveViewStateFromUI_WhenGotoOtherPage();
            LSLibrary.WebAPP.PageSessionHelper.SetValue(this.ViewState[NAME_OF_PAGE_VIEW], BLL.GlobalVariate.Session_clottoupload);
            string url = "~/Pages/Apply_Upload.aspx?{0}={1}&{2}={3}&{4}={5}";
            string backurl = System.Web.HttpUtility.UrlEncode("~/pages/Applyclot.aspx?action=back");
            url = string.Format(url, Apply_Upload.url_GetsessionName, BLL.GlobalVariate.Session_clottoupload, Apply_Upload.url_BacksessionName, BLL.GlobalVariate.Session_uploadtoclot, Apply_Upload.url_backUrlname, backurl);
            Response.Redirect(url, true);
        }

        private void SaveViewStateFromUI_WhenGotoOtherPage()
        {
            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);

            dataview.ddl_typeSelected = this.ddl_leavetype.SelectedValue;
            dataview.inputdate = this.tb_date.Text;
            dataview.ddlfromh = this.DropDownList1.SelectedValue;
            dataview.ddlfromto = this.DropDownList2.SelectedValue;
            dataview.ddltoh = this.DropDownList3.SelectedValue;
            dataview.ddltom = this.DropDownList4.SelectedValue;
            dataview.numberofhour = this.tb_hours.Text;
            dataview.remark = this.tb_remarks.Text;

            LSLibrary.WebAPP.ViewStateHelper.SetValue(NAME_OF_PAGE_VIEW, dataview, this.ViewState);
        }


        private void LoadUIFromViewState_WhenBackFromOtherPage()
        {
            var dataview = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.CLOT.ViewState_page>(NAME_OF_PAGE_VIEW, this.ViewState);

            this.ddl_leavetype.SelectedValue = dataview.ddl_typeSelected;
            this.tb_date.Text = dataview.inputdate;
            this.DropDownList1.SelectedValue = dataview.ddlfromh;
            this.DropDownList2.SelectedValue = dataview.ddlfromto;
            this.DropDownList3.SelectedValue = dataview.ddltoh;
            this.DropDownList4.SelectedValue = dataview.ddltom;
            this.tb_hours.Text = dataview.numberofhour;
            this.tb_remarks.Text = dataview.remark;
        }


        protected void ddl_section_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedSection = int.Parse(this.ddl_section.SelectedValue);

            double fullWorkHours = 7.5; double amWorkhours = 3.5; double pmWorkHours = 4;
            DateTime amFrom = new System.DateTime(2022, 1, 1, 9, 0, 0); DateTime amTo = new System.DateTime(2022, 1, 1, 12, 30, 0);
            DateTime pmFrom = new System.DateTime(2022, 1, 1, 2, 1, 0); DateTime pmTo = new System.DateTime(2022, 1, 1, 18, 1, 0);

            var einfo = BLL.User_wsref.getEmploymentByid(loginer.userInfo.employID??0);
            if (einfo != null)
            {
                var shift = BLL.CodeSetting.GetShiftbyid(einfo.ShiftID);

                BLL.CLOT.GetWorkHourInfoByShift(shift, out amFrom, out amTo, out pmFrom, out pmTo, out amWorkhours, out pmWorkHours, out fullWorkHours);
            }

            if (selectedSection == 0)
            {
                this.DropDownList1.SelectedValue = amFrom.Hour.ToString();
                this.DropDownList2.SelectedValue = amFrom.Minute.ToString();
                this.DropDownList3.SelectedValue = pmTo.Hour.ToString();
                this.DropDownList4.SelectedValue = pmTo.Minute.ToString();
            }
            else if (selectedSection == 1)
            {
                this.DropDownList1.SelectedValue = amFrom.Hour.ToString();
                this.DropDownList2.SelectedValue = amFrom.Minute.ToString();
                this.DropDownList3.SelectedValue = amTo.Hour.ToString();
                this.DropDownList4.SelectedValue = amTo.Minute.ToString();
            }
            else if (selectedSection == 2)
            {
                this.DropDownList1.SelectedValue = pmFrom.Hour.ToString();
                this.DropDownList2.SelectedValue = pmFrom.Minute.ToString();
                this.DropDownList3.SelectedValue = pmTo.Hour.ToString();
                this.DropDownList4.SelectedValue = pmTo.Minute.ToString();
            }

            OnFromTO_TextChanged(null, null);
        }
    }
}