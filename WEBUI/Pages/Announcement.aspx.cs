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

        
        #region page Event
        protected override void InitPage_OnBeforeF5RegisterEvent()
        {
        }

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
        {}
        protected override void InitPage_OnNotFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {}

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            //init pageViewState.
            string strAction = Request.QueryString[qs_activeTab];
            int qs_activetab_value;
            int.TryParse(strAction, out qs_activetab_value);
            mViewState_Page = new MODEL.Announcement.ViewState_page(qs_activetab_value, System.DateTime.Now.Year);

            SetupNavinigation();
            SetupSearchAndTab((MODEL.Announcement.enum_Announce_tabs)mViewState_Page.ActiveTab);
            SetupRepeater((MODEL.Announcement.enum_Announce_tabs)mViewState_Page.ActiveTab, int.Parse(this.ddl_year.SelectedValue),loginer.userInfo.employID??0);

            MultplayLanguage();
        }

        protected override void PageLoad_InitUIOnNotFirstLoad4()
        {
            //load pageViewState.
            mViewState_Page = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Announcement.ViewState_page>(ViewState_PageName, ViewState);
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        { }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            LSLibrary.WebAPP.ViewStateHelper.SetValue(ViewState_PageName, mViewState_Page, ViewState);
        }

        private void MultplayLanguage()
        {
            this.lb_notice.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_new;
            this.lb_policy.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_pending;
            this.lb_procedure.Text = BLL.MultiLanguageHelper.GetLanguagePacket().apply_processed;
        }

        private void SetupRepeater(MODEL.Announcement.enum_Announce_tabs activeTab,int year,int Feid)
        {
            List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement> MyAnnouncements = new List<WebServiceLayer.WebReference_Ileave_Other.t_Announcement>();
            MyAnnouncements = BLL.Other.GetAnouncementByFEIDType(Feid,activeTab,year);
            this.rp_announctment.DataSource = MyAnnouncements;
            this.rp_announctment.DataBind();
            //this.lb_msg.Text += "repeater:" + activeTab.ToString() + ". year:" + year.ToString() + ". \r\n </br>";
        }

        private void SetupSearchAndTab(MODEL.Announcement.enum_Announce_tabs activeTab)
        {
            SetupSearchAndTab_Tab(activeTab);
            SetupSearchAndTab_DDLYear();
        }

        private void SetupSearchAndTab_DDLYear()
        {
            int startYear = 2021;//todo get earylest year of msg.

            for (int i = startYear; i <= System.DateTime.Now.Year; i++)
            {
                this.ddl_year.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            this.ddl_year.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void SetupSearchAndTab_Tab(MODEL.Announcement.enum_Announce_tabs activeTab)
        {
            this.myTab_Notice.Attributes.Remove("class");
            this.myTab_policy.Attributes.Remove("class");
            this.myTab_Procedure.Attributes.Remove("class");


            if (activeTab == MODEL.Announcement.enum_Announce_tabs.Procedure)
            {
                this.myTab_Procedure.Attributes.Add("class", "active");

            }
            else if (activeTab == MODEL.Announcement.enum_Announce_tabs.POLICY)
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
            MODEL.Announcement.enum_Announce_tabs activeTab = MODEL.Announcement.enum_Announce_tabs.NOTICE;

            if (LB_Sender.ID == "lb_policy")
            {
                activeTab = MODEL.Announcement.enum_Announce_tabs.POLICY;
            }
            else if (LB_Sender.ID == "lb_procedure")
            {
                activeTab = MODEL.Announcement.enum_Announce_tabs.Procedure;
            }
            else
            {
                activeTab = MODEL.Announcement.enum_Announce_tabs.NOTICE;
            }
            mViewState_Page.ActiveTab = (int)activeTab;

            SetupSearchAndTab_Tab((MODEL.Announcement.enum_Announce_tabs)mViewState_Page.ActiveTab);

            SetupRepeater((MODEL.Announcement.enum_Announce_tabs)mViewState_Page.ActiveTab,mViewState_Page.SelectedYear,loginer.userInfo.employID??0);
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
            DropDownList ddl_year = (DropDownList)sender;
            mViewState_Page.SelectedYear = int.Parse(ddl_year.SelectedValue);
            SetupRepeater((MODEL.Announcement.enum_Announce_tabs)mViewState_Page.ActiveTab, mViewState_Page.SelectedYear, loginer.userInfo.employID??0);
        }
    }
}