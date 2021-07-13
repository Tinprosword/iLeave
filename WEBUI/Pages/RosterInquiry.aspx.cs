using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class RosterInquiry : BLL.CustomLoginTemplate
    {
        private int actionType = 0;

        #region page event
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            string strActionType = Request.QueryString["action"];
            if (!string.IsNullOrEmpty(strActionType))
            {
                int.TryParse(strActionType, out actionType);
            }
            else
            {
                Response.Clear();
                Response.Write(LSLibrary.WebAPP.MyJSHelper.AlertMessageAndGoto("Invalid url parameter!,", "main.aspx"));
                Response.End();
            }
        }

        protected override void InitPage_OnFirstLoad2()
        {
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            LoadUI();
        }

        

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
        }

        #endregion


        private void SetupTab()
        {
            //tab
            if (actionType == 1)
            {
                this.myTabRoster_leawve.Attributes.Remove("class");
                this.myTabRoster_roster.Attributes.Remove("class");
                this.myTabRoster_roster.Attributes.Add("class", "active");

                this.DivLeaveTab.Visible = false;
                this.DivRosterTab.Visible = true;
                this.rp_leave.DataSource = null;
                this.rp_leave.DataBind();
                this.rp_roster.DataSource = null;
                this.rp_roster.DataBind();
            }
            else
            {
                this.myTabRoster_roster.Attributes.Remove("class");
                this.myTabRoster_leawve.Attributes.Remove("class");
                this.myTabRoster_leawve.Attributes.Add("class", "active");

                this.DivLeaveTab.Visible = true;
                this.DivRosterTab.Visible = false;
                this.rp_leave.DataSource = null;
                this.rp_leave.DataBind();
                this.rp_roster.DataSource = null;
                this.rp_roster.DataBind();
            }
        }


        private void LoadUI()
        {
            ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, BLL.MultiLanguageHelper.GetLanguagePacket().main_rosterInqury, "~/pages/main.aspx", true);
            SetupTab();

        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            int firstEmploymentID = -1;
            string zoneCode = "";
            string positionCode = "";
            DateTime? startDate = null;
            DateTime? endDate = null;

            if (actionType == 0)
            {
                string[] data = { "a", "b" };
                this.rp_leave.DataSource = data;
                this.rp_leave.DataBind();
            }
            else if(actionType == 1)
            {
                string[] data = { "a", "b" };
                this.rp_roster.DataSource = data;
                this.rp_roster.DataBind();
            }
        }

    }
}