using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Announcement : BLL.CustomLoginTemplate
    {
        public static string ViewState_PageName = "ViewState_PageName";
        public static string qs_activeTab = "ActiveTab";


        public MODEL.Announcement.ViewState_page mViewState_Page = null;

        #region enum
        public enum enum_Announce_tabs
        {
            NOTICE=1,
            POLICY=2,
            Procedure=3
        }
        #endregion

        #region page Event

        protected override void InitPage_OnNotFirstLoad2()
        { }

        protected override void PageLoad_InitUIOnNotFirstLoad4()
        { }

        protected override void InitPage_OnBeforeF5RegisterEvent()
        { }

        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (!string.IsNullOrEmpty(Request.QueryString[qs_activeTab]))
            {
                string strAction = Request.QueryString[qs_activeTab];
                int qs_activetab_value;
                int.TryParse(strAction, out qs_activetab_value);

                //check querystring.
                if (qs_activetab_value == -1)
                {
                    Response.Redirect("main.aspx", true);
                }
            }
            else
            {
                Response.Redirect("main.aspx", true);
            }
        }

        protected override void InitPage_OnFirstLoad2()
        {
            LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, mViewState_Page, ViewState);
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            if(!IsPostBack)
            mViewState_Page = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Announcement.ViewState_page>(ViewState_PageName, ViewState);

            if (mViewState_Page == null)
            {
                string strAction = Request.QueryString[qs_activeTab];
                int qs_activetab_value;
                int.TryParse(strAction, out qs_activetab_value);

                mViewState_Page = new MODEL.Announcement.ViewState_page(qs_activetab_value, System.DateTime.Now.Year);
            }
            else
            {
                
            }
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            SetupNavinigation();
            SetupSearchAndTab((enum_Announce_tabs)mViewState_Page.ActiveTab);

            SetupRepeater((enum_Announce_tabs)mViewState_Page.ActiveTab, int.Parse(this.ddl_year.SelectedValue),loginer.userInfo.employID??0);
            MultplayLanguage();
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        { }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            int a = 4;
            LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, mViewState_Page, ViewState);
        }

        private void MultplayLanguage()
        {
            this.lb_notice.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_new;
            this.lb_policy.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_pending;
            this.lb_procedure.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_processed;
        }

        private void SetupRepeater(enum_Announce_tabs activeTab,int year,int eid)
        {
            //
            this.lb_msg.Text += "show rep"+activeTab.ToString() + "\r\n";
        }

        private void SetupSearchAndTab(enum_Announce_tabs activeTab)
        {
            SetupSearchAndTab_Tab(activeTab);
            SetupSearchAndTab_DDLYear();
        }

        private void SetupSearchAndTab_DDLYear()
        {
            List<int> yearRange = BLL.Leave.GetDefaultYearRange();
            for (int i = yearRange[0]; i <= yearRange[1]; i++)
            {
                this.ddl_year.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            this.ddl_year.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void SetupSearchAndTab_Tab(enum_Announce_tabs activeTab)
        {
            this.myTab_Notice.Attributes.Remove("class");
            this.myTab_policy.Attributes.Remove("class");
            this.myTab_Procedure.Attributes.Remove("class");


            if (activeTab == enum_Announce_tabs.Procedure)
            {
                this.myTab_Procedure.Attributes.Add("class", "active");

            }
            else if (activeTab == enum_Announce_tabs.POLICY)
            {
                this.myTab_policy.Attributes.Add("class", "active");
            }
            else
            {
                this.myTab_Notice.Attributes.Add("class", "active");
            }
        }



        private void SetupNavinigation()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().CommonBack, BLL.MultiLanguageHelper.GetLanguagePacket().announcement_Title, "~/pages/main.aspx", true);
        }



        #endregion

        #region click event
        protected void TABOnClick(object sender, EventArgs e)
        {
            LinkButton LB_Sender = (LinkButton)sender;
            enum_Announce_tabs activeTab = enum_Announce_tabs.NOTICE;

            if (LB_Sender.ID == "lb_policy")
            {
                activeTab = enum_Announce_tabs.POLICY;
            }
            else if (LB_Sender.ID == "lb_procedure")
            {
                activeTab = enum_Announce_tabs.Procedure;
            }
            else
            {
                activeTab = enum_Announce_tabs.NOTICE;
            }
            mViewState_Page.ActiveTab = (int)activeTab;
            LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, mViewState_Page, ViewState);

            SetupSearchAndTab_Tab((enum_Announce_tabs)mViewState_Page.ActiveTab);

            SetupRepeater((enum_Announce_tabs)mViewState_Page.ActiveTab,mViewState_Page.SelectedYear,loginer.userInfo.employID??0);
        }

        protected void lb_policy_Click(object sender, EventArgs e)
        {
            TABOnClick(sender, e);
        }

        protected void lb_procedure_Click(object sender, EventArgs e)
        {
            TABOnClick(sender, e);
        }

        protected void lb_notice_Click(object sender, EventArgs e)
        {
            TABOnClick(sender, e);
        }
        #endregion

        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}