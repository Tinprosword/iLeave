﻿using System;
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
            string strAction = Request.QueryString[qs_activeTab];
            int qs_activetab_value;
            int.TryParse(strAction, out qs_activetab_value);

            //init pageViewState.
            InitPageViewState(qs_activetab_value);

            SetupNavinigation();
            SetupSearchAndTab((MODEL.Announcement.enum_Announce_tabs)mViewState_Page.ActiveTab);
            SetupRepeater((MODEL.Announcement.enum_Announce_tabs)mViewState_Page.ActiveTab, int.Parse(this.ddl_year.SelectedValue), loginer.userInfo.employID ?? 0);

            MultplayLanguage();
        }

        private void InitPageViewState(int qs_activetab_value)
        {
            var unReadList = BLL.Announcement.Announce_GetUnReadAnnouncement(loginer.userInfo.firsteid ?? 0);
            List<int> unreadIDS = new List<int>();
            if (unReadList != null && unReadList.Count > 0)
            {
                unreadIDS = unReadList.Select(x => x.ID).ToList();
            }
            mViewState_Page = new MODEL.Announcement.ViewState_page(qs_activetab_value, System.DateTime.Now.Year, unreadIDS);
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
            this.lb_notice.Text = BLL.MultiLanguageHelper.GetLanguagePacket().announcement_NoticeBoard;
            this.lb_policy.Text = BLL.MultiLanguageHelper.GetLanguagePacket().announcement_Policy;
            this.lb_procedure.Text = BLL.MultiLanguageHelper.GetLanguagePacket().announcement_Procedure;
            this.btn_readAll.Text = BLL.MultiLanguageHelper.GetLanguagePacket().announcement_allReaded;
        }

        private void SetupRepeater(MODEL.Announcement.enum_Announce_tabs activeTab,int year,int Feid)
        {
            var MyAnnouncements = BLL.Announcement.GetAnouncementByFEIDType(Feid,activeTab,year);
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
            int startYear = BLL.Announcement.GetAnouncementFirstYear(loginer.userInfo.firsteid ?? 0);
            this.ddl_year.Items.Add(new ListItem("-All-", "-1"));

            for (int i = System.DateTime.Now.Year; i >= startYear; i--)
            {
                this.ddl_year.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            this.ddl_year.SelectedValue = "-1";
            mViewState_Page.SelectedYear = int.Parse(this.ddl_year.SelectedValue);
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


        public string rp_announctment_displayTitle(WebServiceLayer.WebReference_Ileave_Other.t_Announcement record)
        {
            string result = "";
            if (record != null)
            {
                if (mViewState_Page.UnReadAnnounceID.Contains(record.ID))
                {
                    return " ("+BLL.MultiLanguageHelper.GetLanguagePacket().Common_New+")";
                }
                else
                {
                    return "";
                }
            }
            return result;
        }

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


       


        protected void lb_title_Click(object sender, EventArgs e)
        {
            LinkButton lb_anncount = (LinkButton)sender;
            int aid = 0;
            int.TryParse(lb_anncount.CommandArgument, out aid);
            if (aid > 0)
            {
                Response.Redirect("announcementdetail.aspx?id=" + aid);
            }
        }

        protected void btn_readAll_Click(object sender, EventArgs e)
        {
            List<int> unreads = mViewState_Page.UnReadAnnounceID;
            if (unreads != null && unreads.Count > 0)
            {
                foreach (var theID in unreads)
                {
                    BLL.Announcement.Announce_ReadAnncount(theID, loginer.userInfo.firsteid??0);
                }
            }
            //reload rp .但是因为是前端绑定。不需要重载了。
            mViewState_Page.UnReadAnnounceID = new List<int>();
        }

    }
}