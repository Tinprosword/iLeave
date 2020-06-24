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
        //session 和viewstate 使用总则，sesion 只使用于页面之间. 页面的所有数据靠viewstate, 获取不到的就手动用viewstate.
        private static string ViewState_LeaveListName = "leavelist";//页面第一次进入初始化一次, viewstate不用管理清除.
        public static string Session_pageName = "APPLYSESSION";//页面第一次进入时就清除。 离开页面的时候初始化.  sesion只用于页面间.

        #region [page event]

        protected override void InitPageVaralbal0()
        {
            //回调用法+匿名函数用法，单语句用匿名还是可以。否则尽量别用匿名函数，降低了复用性和之后的扩展。只限于单语句并无复用可能。
            //为了一个匿名函数写说明，还不如不用匿名函数，所以匿名函数完全可以禁止使用。
            this.OnF5Doit = () => { Response.Redirect("~/pages/apply.aspx"); };
        }

        protected override void InitPageDataOnEachLoad1()
        {
        }

        protected override void InitPageDataOnFirstLoad2()
        {
        }

        protected override void ResetUIOnEachLoad3()
        {
            this.lt_AlertJS.Text = "";
        }

        protected override void InitUIOnFirstLoad4()
        {
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "back")
            {
                //get session value
                MODEL.Apply.ApplyPage applypage = (MODEL.Apply.ApplyPage)LSLibrary.WebAPP.PageSessionHelper.GetValue(Session_pageName);

                //reload viewstate
                LSLibrary.WebAPP.ViewStateHelper.SetValue(applypage.LeaveList, ViewState_LeaveListName, ViewState);

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
            else
            {
                //init viewstate
                List<MODEL.Apply.LeaveData> LeaveList = new List<MODEL.Apply.LeaveData>();
                LSLibrary.WebAPP.ViewStateHelper.SetValue(LeaveList, ViewState_LeaveListName, ViewState);

                //init ui
                this.literal_applier.Text = loginer.loginID + "  " + loginer.userInfo.nickname;
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_menu_current, "~/pages/main.aspx");
                this.repeater_leave.DataSource = LeaveList;
                this.repeater_leave.DataBind();
            }
            SetMultiLanguage();
        }
        #endregion

        #region [module] upload pic

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            //save session
            MODEL.Apply.ApplyPage applyPage = new MODEL.Apply.ApplyPage();
            applyPage.LeaveTypeSelectValue = this.ddl_leavetype.SelectedValue;
            applyPage.applylabel = this.lt_applydays.Text;
            applyPage.balancelabel = this.lt_balancedays.Text;
            applyPage.datefrom = this.tb_from.Text;
            applyPage.dateto = this.tb_to.Text;
            applyPage.ddlsectionSelectvalue = this.dropdl_section.SelectedValue;
            applyPage.remarks = this.tb_remarks.Text;
            applyPage.LeaveList = LSLibrary.WebAPP.ViewStateHelper.GetValue<List<MODEL.Apply.LeaveData>>(ViewState_LeaveListName, ViewState);
            if (LSLibrary.WebAPP.PageSessionHelper.GetValue(Session_pageName) != null)
            {
                applyPage.uploadpic = ((MODEL.Apply.ApplyPage)LSLibrary.WebAPP.PageSessionHelper.GetValue(Session_pageName)).uploadpic;
            }
            else
            {
                applyPage.uploadpic = new List<MODEL.Apply.UploadPic>();
            }
            
            LSLibrary.WebAPP.PageSessionHelper.SetValue(applyPage,Session_pageName);
            Response.Redirect("~/pages/apply_upload.aspx", true);
        }

        #endregion

        #region [module] leave
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            List<MODEL.Apply.LeaveData> LeaveList = LSLibrary.WebAPP.ViewStateHelper.GetValue<List<MODEL.Apply.LeaveData>>(ViewState_LeaveListName,ViewState);
            if (LeaveList != null)
            {
                LeaveList.AddRange(getListSource(DateTime.Now, DateTime.Now));
                LSLibrary.WebAPP.ViewStateHelper.SetValue(LeaveList, ViewState_LeaveListName, ViewState);

                this.repeater_leave.DataSource = LeaveList;
                this.repeater_leave.DataBind();
            }
        }

        protected void delete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton senderObj = (ImageButton)sender;
            string strIndex = senderObj.CommandArgument;
            int intIndex = int.Parse(strIndex);

            List<MODEL.Apply.LeaveData> LeaveList = LSLibrary.WebAPP.ViewStateHelper.GetValue<List<MODEL.Apply.LeaveData>>(ViewState_LeaveListName, ViewState);
            if (LeaveList != null)
            {
                LeaveList.RemoveAt(intIndex);

                LSLibrary.WebAPP.ViewStateHelper.SetValue(LeaveList, ViewState_LeaveListName, ViewState);
                this.repeater_leave.DataSource = LeaveList;
                this.repeater_leave.DataBind();
            }
        }
        #endregion

        #region [module] apply
        protected void button_apply_Click(object sender, EventArgs e)
        {
            //clean session
            LSLibrary.WebAPP.PageSessionHelper.CleanValue(Session_pageName);
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
                data.Add(new MODEL.Apply.LeaveData("Admin","05-01周一", "AL", "FULL DAY", 0, 0));
                data.Add(new MODEL.Apply.LeaveData("Admin", "05-02周二", "AL", "FULL DAY", 0, 0));
                data.Add(new MODEL.Apply.LeaveData("Admin", "05-03周三", "AL", "FULL DAY", 0, 0));
                data.Add(new MODEL.Apply.LeaveData("Admin", "05-04周四", "AL", "FULL DAY", 0, 0));
                data.Add(new MODEL.Apply.LeaveData("Admin", "05-05周五", "AL", "FULL DAY", 0, 0));
            }
            return data;
        }
        #endregion

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            
        }
    }
}