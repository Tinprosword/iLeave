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
        //2. myself my team 4.canleder datetime=> show detail (^)[not version 1]
        //todo 1.server address secretery, 3.unit dropdown list  5.app's icon 
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
                    LoadUI(applypage);
                }
            }
            else if (Request.QueryString["action"] != null && Request.QueryString["action"] == "backCalendar")
            {
                Pages.calendar prepage = PreviousPage as Pages.calendar;
                if(prepage!=null)
                {
                    MODEL.Apply.ViewState_page applypage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, prepage.myviewState);
                    LSLibrary.WebAPP.ViewStateHelper.SetValue(applypage, ViewState_PageName, ViewState);
                    LoadUI(applypage);
                }
            }
            else
            {
                //init ui
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_current, "~/pages/main.aspx");
                this.literal_applier.Text = loginer.loginID + "  " + loginer.userInfo.nickname;

                List<LSLibrary.WebAPP.ValueText> typedata=BLL.Apply.GetLeaveType();
                this.ddl_leavetype.DataSource = typedata;
                this.ddl_leavetype.DataValueField = "mvalue";
                this.ddl_leavetype.DataTextField = "mtext";
                this.ddl_leavetype.DataBind();
                this.repeater_leave.DataSource = new List<MODEL.Apply.LeaveData>();
                this.repeater_leave.DataBind();

                //set viewstate
                MODEL.Apply.ViewState_page viewState_Page = new MODEL.Apply.ViewState_page();
                viewState_Page.LeaveList = new List<MODEL.Apply.LeaveData>();
                viewState_Page.uploadpic = new List<MODEL.Apply.UploadPic>();
                viewState_Page.leavetype = new List<LSLibrary.WebAPP.ValueText>();
                viewState_Page.leavetype = typedata;

                LSLibrary.WebAPP.ViewStateHelper.SetValue(viewState_Page, ViewState_PageName, ViewState);

            }
            SetMultiLanguage();
        }

        private void LoadUI(MODEL.Apply.ViewState_page applypage)
        {
            //init ui
            this.ddl_leavetype.DataSource = applypage.leavetype;
            this.ddl_leavetype.DataValueField = "mvalue";
            this.ddl_leavetype.DataTextField = "mtext";
            this.ddl_leavetype.DataBind();

            this.literal_applier.Text = loginer.loginID + "  " + loginer.userInfo.nickname;
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

        #region [module] upload pic
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            //save viewstate' other data
            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            applyPage.LeaveTypeSelectValue = this.ddl_leavetype.SelectedValue;
            applyPage.applylabel = this.lt_applydays.Text;
            applyPage.balancelabel = this.lt_balancedays.Text;
            applyPage.ddlsectionSelectvalue = this.dropdl_section.SelectedValue;
            applyPage.remarks = this.tb_remarks.Text;
            LSLibrary.WebAPP.ViewStateHelper.SetValue(applyPage, ViewState_PageName, ViewState);
        }
        #endregion

        #region [module] leave
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {

            //save viewstate' other data
            MODEL.Apply.ViewState_page applyPage = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState);
            applyPage.LeaveTypeSelectValue = this.ddl_leavetype.SelectedValue;
            applyPage.applylabel = this.lt_applydays.Text;
            applyPage.balancelabel = this.lt_balancedays.Text;
            applyPage.ddlsectionSelectvalue = this.dropdl_section.SelectedValue;
            applyPage.remarks = this.tb_remarks.Text;
            LSLibrary.WebAPP.ViewStateHelper.SetValue(applyPage, ViewState_PageName, ViewState);


            Server.Transfer("~/Pages/calendar.aspx?action=apply",false);

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
            List<MODEL.Apply.LeaveData> LeaveList = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(ViewState_PageName, ViewState).LeaveList;
            BLL.Apply.InsertLeave(LeaveList, loginer.userInfo.id, -1);
            Response.Redirect("~/pages/main.aspx");
        }
        #endregion

        #region [common function]
        private void SetMultiLanguage()
        {
            this.lt_name.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_name;
            this.lt_leave.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_leave;
            this.lt_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_apply;
            this.lt_balance.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_banlance;
            //this.lt_date.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_date;
            this.lt_section.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_section;
            this.lt_remarks.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_remarks;
            this.ltlistdate.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_list_data;
            this.ltlisttype.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_list_type;
            this.lt_listsection.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_list_section;
            this.button_apply.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_button;
        }
        
        #endregion

    }
}