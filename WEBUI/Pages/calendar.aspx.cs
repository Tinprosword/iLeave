using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{

    //action=choose  :1.mulchoose to prepage.
    public partial class calendar : BLL.CustomLoginTemplate
    {
        private readonly string css_select = "btnBox btnBlueBoxSelect";
        private readonly string css_unselect = "btnBox btnBlueBoxUnSelect";
        public StateBag myviewState;


        protected override void InitPageVaralbal0()
        {
            WEBUI.Pages.Apply prepage = PreviousPage as WEBUI.Pages.Apply;
            myviewState = ViewState;
        }

        protected override void InitPageDataOnEachLoad1()
        {
        }

        protected override void InitPageDataOnFirstLoad2()
        {
        }

        protected override void ResetUIOnEachLoad3()
        {
            this.Calendar1.DayRender += Calendar1_DayRender;

            string target= Request.Params["__EVENTTARGET"];
            string argument = Request.Params["__EVENTARGUMENT"]; 
            if (target!=null && target.Contains("Calendar1"))
            {
                try
                {
                    int date = int.Parse(argument);
                    Calendar1_SelectionChanged(this.Calendar1, date);
                }
                catch { }
            }
        }


        protected override void InitUIOnFirstLoad4()
        {
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "apply")
            {   //todo modify button title
                WEBUI.Pages.Apply prepage = PreviousPage as WEBUI.Pages.Apply;
                if (prepage != null)
                {
                    MODEL.Apply.ViewState_page applyPage2 = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, prepage.myviewState);
                    LSLibrary.WebAPP.ViewStateHelper.SetValue(applyPage2, Apply.ViewState_PageName, ViewState);
                    ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_button, BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_current, "~/pages/apply.aspx?action=backCalendar");
                }
                else
                {
                    Response.Redirect("~/pages/apply.aspx", true);
                }
            }
            else
            {
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_back, BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_current, "~/pages/main.aspx");
            }

            TableItemStyle obj = this.Calendar1.SelectedDayStyle;
            obj.BorderStyle = BorderStyle.Solid;
            obj.BorderColor = LSLibrary.MyColors.ParseColor("#bd4f8b");
            obj.BorderWidth = 2;
            obj.BackColor = LSLibrary.MyColors.ParseColor("#ffffff");
            obj.ForeColor = LSLibrary.MyColors.ParseColor("#000000");

            this.Calendar1.SelectedDate = System.DateTime.Now;
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginName, this.Calendar1.SelectedDate);
            this.repeater_leave.DataBind();
            SetupMultiLanguage();
        }


        private void SetupMultiLanguage()
        {
            this.btn_myself.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_me;
            this.btn_team.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_team;
            this.cb_leave.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_leave;
            this.cb_holiday.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_holiday;
            this.lt_approval.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_approved;
            this.lt_wait.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_containwait;
            this.lt_name.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_listname;
            this.lt_type.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_listtype;
            this.lt_section.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_listsection;
            this.lt_status.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_liststatus;
        }


        private void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.ToString("yyyy-MM-dd") == "2020-06-19")
            {
                e.Cell.BackColor = System.Drawing.Color.Black;
                e.Cell.ForeColor= LSLibrary.MyColors.ParseColor("#ffffff");
            }
            if (e.Day.Date.ToString("yyyy-MM-dd") == "2020-06-20")
            {
                e.Cell.BackColor = LSLibrary.MyColors.ParseColor("#f3e926");
                e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#ffffff");
            }
            if (e.Day.Date.ToString("yyyy-MM-dd") == "2020-06-21")
            {
                e.Cell.BackColor = LSLibrary.MyColors.ParseColor("#f3e926");
                e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#ffffff");
            }

            List<DateTime> chooseDate = GetChoose();
            if(chooseDate.Contains(e.Day.Date))
            {
                e.Cell.BackColor = LSLibrary.MyColors.ParseColor("#b12977");
                e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#ffffff");
            }
        }


        protected void Calendar1_SelectionChanged(Calendar calendar, int date)
        {
            System.DateTime guardTime = new DateTime(2020, 07, 21);//7507
            System.DateTime selectDate = guardTime.AddDays(date - 7507);
            calendar.SelectedDate = selectDate;
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginName,calendar.SelectedDate);
            this.repeater_leave.DataBind();

            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "apply")
            {
                ChooseDate(calendar.SelectedDate);
            }
        }

        protected void btn_myself_Click(object sender, EventArgs e)
        {
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginName, System.DateTime.Now);
            this.repeater_leave.DataBind();
            this.btn_myself.CssClass = css_select;
            this.btn_team.CssClass = css_unselect;
        }

        protected void btn_team_Click(object sender, EventArgs e)
        {
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginName,  System.DateTime.Now.AddDays(1));
            this.repeater_leave.DataBind();
            this.btn_myself.CssClass = css_unselect;
            this.btn_team.CssClass = css_select;
        }

        protected void cb_leave_CheckedChanged(object sender, EventArgs e)
        {
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginName, System.DateTime.Now.AddDays(1));
            this.repeater_leave.DataBind();
        }

        protected void cb_holiday_CheckedChanged(object sender, EventArgs e)
        {
            this.repeater_leave.DataSource = BLL.Calendar.getListSource(loginer.loginName, System.DateTime.Now.AddDays(1));
            this.repeater_leave.DataBind();
        }


        private List<DateTime> GetChoose()
        {
            List<DateTime> res = new List<DateTime>();
            MODEL.Apply.ViewState_page data = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, ViewState);

            if (data != null)
            {
                for (int i = 0; i < data.LeaveList.Count; i++)
                {
                    res.Add(data.LeaveList[i].LeaveDate);
                }
            }
            return res;
        }

        private void ChooseDate(DateTime dateTime)
        {
            MODEL.Apply.ViewState_page data = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, ViewState);

            if (data != null)
            {
                List<MODEL.Apply.LeaveData> selected = data.LeaveList;
                if (selected != null)
                {
                    var temp = selected.Find(x => x.LeaveDate == dateTime);
                    if (temp == null)
                    {
                        int leavetype = int.Parse(data.LeaveTypeSelectValue);
                        int sectiontype =int.Parse(data.ddlsectionSelectvalue);
                        string leavename = LSLibrary.WebAPP.ValueText.GetText(data.leavetype, int.Parse(data.LeaveTypeSelectValue));

                        var newitem = new MODEL.Apply.LeaveData(loginer.loginName, dateTime.ToString("MM-dd"), sectiontype, leavetype, (int)BLL.Application.ApprovalRequestStatus.WAIT_FOR_APPROVE, BLL.Application.ApprovalRequestStatus.WAIT_FOR_APPROVE.ToString(), dateTime, leavename, leavename);
                        data.LeaveList.Add(newitem);
                    }
                    else
                    {
                        data.LeaveList.Remove(temp);
                    }
                }
            }
        }




    }
}