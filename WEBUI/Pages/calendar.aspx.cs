using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WEBUI.Pages
{
    //就2个主要任务。
    //1.初始化，或者换月的时候 .在calendar rendar事件时,根据请求显示不同的颜色。 2.点击单元格的时候， Calendar1_SelectionChanged 显示对应的请求列表。
    //3 附加一个请假页面过来，加载viewstate. 点击单元格保存数据。 在calendar rendar事件时,显示所有选择日期。
    public partial class calendar : BLL.CustomLoginTemplate
    {
        private readonly string css_select = "btnBox btnBlueBoxSelect";
        private readonly string css_unselect = "btnBox btnBlueBoxUnSelect";
        private bool isFromApply = false;
        private Dictionary<DateTime, int> allStatistic = null;

        #region pageevent
        protected override void InitPage_OnEachLoadAfterCheckSessionAndF5_1()
        {
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "apply")
            {
                isFromApply = true;
            }
        }


        protected override void InitPage_OnFirstLoad2()
        {}

        protected override void PageLoad_Reset_ReInitUIOnEachLoad3()
        {
            WEBUI.Pages.Apply prepage = PreviousPage as WEBUI.Pages.Apply;

            //1.intercept calendar 事件，以代替默认的changed事件（因为要捕捉cancel cell事件,而默认的不捕捉cancel事件）

            if (this.cb_leave.Checked)
            {
                RegisterCellColorEvent();
                RegisterClickCellEventAndSetCanlendarValue();
            }
            else if (this.cb_holiday.Checked)
            {
                RegisterClickCellEventAndSetCanlendarValue();
            }

            app_ok.Visible = isFromApply;
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            //1.navigation. 2.init viewstate 3 setupZone .5repeater.
            this.Calendar1.SelectedDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);
            this.Calendar1.VisibleDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);

            
            OnPrePageIsApplyInitViewState();
            SetupZone(loginer.userInfo.personid);
            SetupRepeater();
            SetupMultiLanguage();
        }

        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            setNavigation();//todo 因为viewstate无法保存event .所以只好放到这里来。所以不喜欢asp.net 模拟cs，但是又不能完全模拟，太恶心了.
        }

        #region inner function
        private void SetupRepeater()
        {
            List<int> eid = GetEmployIDs(GetIsMeOrTeam(), this.ddlzone.SelectedValue);

            if (this.cb_leave.Checked)
            {
                this.leaveDiv.Visible = true;
                this.rosterDiv.Visible = false;
                var repeaterSource = BLL.Leave.getListSource(this.Calendar1.SelectedDate, eid);
                this.repeater_leave.DataSource = repeaterSource;
                this.repeater_leave.DataBind();
            }
            else if (this.cb_holiday.Checked)
            {
                this.leaveDiv.Visible = false;
                this.rosterDiv.Visible = true;
                var repeaterSource = BLL.calendar.GetRoster(this.Calendar1.SelectedDate, eid);
                this.rp_roster.DataSource = repeaterSource;
                this.rp_roster.DataBind();
            }
        }

        private void RegisterCellColorEvent()
        {
            this.Calendar1.DayRender += Calendar1_DayRender;
            this.Calendar1.PreRender += Calendar1_PreRender;
        }

        private void UnRegisterCellColorEvent()
        {
            this.Calendar1.DayRender -= Calendar1_DayRender;
            this.Calendar1.PreRender -= Calendar1_PreRender;
        }

        private void RegisterClickCellEventAndSetCanlendarValue()
        {
            string target = Request.Params["__EVENTTARGET"];
            string argument = Request.Params["__EVENTARGUMENT"];
            if (target != null && target.Contains("Calendar1"))
            {
                if (argument.Contains("V"))
                {
                    int intdate = int.Parse(argument.Substring(1, argument.Length - 1));
                    DateTime date = TransferDate(intdate);
                    this.Calendar1.VisibleDate = date;
                }
                else
                {
                    int intdate = int.Parse(argument);
                    DateTime date = TransferDate(intdate);
                    Calendar1_SelectionChanged(this.Calendar1, date);
                }
            }
        }

        private void Calendar1_PreRender(object sender, EventArgs e)
        {
            bool isme = GetIsMeOrTeam();
            List<int> eid = GetEmployIDs(isme, this.ddlzone.SelectedValue);
            if (this.Calendar1.VisibleDate.Year > 1)
            {
                FillStatistic(eid, this.Calendar1.VisibleDate.Year, this.Calendar1.VisibleDate.Month);
            }
        }

        private List<int> GetEmployIDs(bool isme,string contractinfo)
        {
            //1.me or team 2.zone 3 get employment ids.
            var zoneArray = contractinfo.Split(new char[] { '|' });
            string contarctID = zoneArray[0];
            string zoneCode = zoneArray[1];
            int intContractId = int.Parse(contarctID);
            List<int> eid = new List<int>();
            var Employment = BLL.User_wsref.getEmploymentByZone(intContractId, zoneCode);
            if (isme)
            {
                eid = Employment.Where(x => x.StaffID == loginer.userInfo.staffid).Select(x => x.ID).ToList();
            }
            else
            {
                eid= Employment.Select(x => x.ID).ToList();
            }
            return eid;
        }

        private void setNavigation()
        {
            if (isFromApply)
            {
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_calendar_back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_calendar_current,null, true, BackEvent);
                this.ib_ok.Click += BackEvent;
            }
            else
            {
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_back, BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_current, "~/pages/main.aspx", true);
            }
        }

        private void BackEvent(object sender, ImageClickEventArgs e)
        {
            object myViewState = LSLibrary.WebAPP.ViewStateHelper.GetValue(WEBUI.Pages.Apply.ViewState_PageName, this.ViewState);
            LSLibrary.WebAPP.PageSessionHelper.SetValue(myViewState, BLL.GlobalVariate.Session_CanlendarToApply);
            Response.Redirect("~/pages/Apply.aspx?action=backCalendar", true);
        }

        private void SetupZone(int pid)
        {
            var result = BLL.User_wsref.GetPersonBaseInfoByPid(pid);
            List<int?> eids = BLL.User_wsref.FilterValidUser(result).Select(x => x.e_id).ToList();
            List<int> eids2 = new List<int>();
            foreach (int? ii in eids)
            {
                if (ii != null) eids2.Add((int)ii);
            }
            var contracts = BLL.calendar.GetContractByEmployids(eids2.ToArray());
            this.ddlzone.Items.Add(new ListItem("All Zone", "0|"));
            for (int i = 0; i < contracts.Count; i++)
            {
                this.ddlzone.Items.Add(new ListItem(contracts[i].Description + " - " + contracts[i].zonecode, contracts[i].contractid + "|" + contracts[i].zonecode));
            }
            this.ddlzone.SelectedIndex = 0;
        }

        private void OnPrePageIsApplyInitViewState()
        {
            if (isFromApply)
            {
                object PreViewstate = LSLibrary.WebAPP.PageSessionHelper.GetValueAndCleanSoon(BLL.GlobalVariate.Session_ApplyToCanlendar);
                if (PreViewstate != null)
                {
                    LSLibrary.WebAPP.ViewStateHelper.SetValue( Apply.ViewState_PageName, PreViewstate, ViewState);
                }
                else
                {
                    Response.Redirect("~/pages/apply.aspx", true);
                }
            }
        }

        private void FillStatistic(List<int> eid, int year, int month)
        {
            allStatistic = new Dictionary<DateTime, int>();
            if (month == 1)
            {
                List<int> monthStastic1 = BLL.Leave.GetMonthStatistic(year - 1, 12, eid.ToArray());
                GenerateDictionary(year - 1, 12, monthStastic1, allStatistic);
                List<int> monthStastic2 = BLL.Leave.GetMonthStatistic(year, month, eid.ToArray());
                GenerateDictionary(year, month, monthStastic2, allStatistic);
                List<int> monthStastic3 = BLL.Leave.GetMonthStatistic(year, month + 1, eid.ToArray());
                GenerateDictionary(year, month + 1, monthStastic3, allStatistic);
            }
            else if (month == 12)
            {
                List<int> monthStastic1 = BLL.Leave.GetMonthStatistic(year, month - 1, eid.ToArray());
                GenerateDictionary(year, month - 1, monthStastic1, allStatistic);
                List<int> monthStastic2 = BLL.Leave.GetMonthStatistic(year, month, eid.ToArray());
                GenerateDictionary(year, month, monthStastic2, allStatistic);
                List<int> monthStastic3 = BLL.Leave.GetMonthStatistic(year + 1, 1, eid.ToArray());
                GenerateDictionary(year + 1, 1, monthStastic3, allStatistic);
            }
            else
            {
                List<int> monthStastic1 = BLL.Leave.GetMonthStatistic(year, month - 1, eid.ToArray());
                GenerateDictionary(year, month - 1, monthStastic1, allStatistic);
                List<int> monthStastic2 = BLL.Leave.GetMonthStatistic(year, month, eid.ToArray());
                GenerateDictionary(year, month, monthStastic2, allStatistic);
                List<int> monthStastic3 = BLL.Leave.GetMonthStatistic(year, month + 1, eid.ToArray());
                GenerateDictionary(year, month + 1, monthStastic3, allStatistic);
            }
        }

        private void GenerateDictionary(int year, int month, List<int> result, Dictionary<DateTime, int> temp)
        {
            int days = System.DateTime.DaysInMonth(year, month);
            for (int i = 0; i < days; i++)
            {
                DateTime tempday = new DateTime(year, month, i + 1);
                temp[tempday] = result[i];
            }
        }


        private void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            //1.show approved 2.show wait 3.show selected dates. 4.show selected
            if (allStatistic.Keys.Contains(e.Day.Date))
            {
                if (allStatistic[e.Day.Date] == 1)//approvaed
                {
                    e.Cell.BackColor = System.Drawing.Color.Black;
                    e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#ffffff");
                }
                else if (allStatistic[e.Day.Date] == 2)//waiting
                {
                    e.Cell.BackColor = LSLibrary.MyColors.ParseColor("#f3e926");
                    e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#ffffff");
                }
            }

            List<DateTime> chooseDate = GetChooseFromViewState();//choose days
            if (chooseDate.Contains(e.Day.Date))
            {
                e.Cell.BackColor = LSLibrary.MyColors.ParseColor("#b12977");
                e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#ffffff");
            }
        }
        #endregion

        #endregion

        #region showleave data
        

        protected void Calendar1_SelectionChanged(Calendar calendar, DateTime date)
        {
            //1.set changedDate  2.show related request  3.save date to viewstate.
            this.Label1.Text = date.ToString("yyyy-MM-dd");
            calendar.SelectedDate = date;
            SetupRepeater();
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "apply")
            {
                SaveChooseToViewState(calendar.SelectedDate);
            }
        }

        protected void unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupRepeater();
        }

        protected void btn_myself_Click(object sender, EventArgs e)
        {
            this.btn_myself.CssClass = css_select;
            this.btn_team.CssClass = css_unselect;
            SetupRepeater();
        }

        protected void btn_team_Click(object sender, EventArgs e)
        {
            this.btn_myself.CssClass = css_unselect;
            this.btn_team.CssClass = css_select;
            SetupRepeater();
        }
        #endregion

        #region show holiday
        protected void cb_leave_CheckedChanged(object sender, EventArgs e)
        {
            RegisterCellColorEvent();
            SetupRepeater();
        }

        protected void cb_holiday_CheckedChanged(object sender, EventArgs e)
        {
            UnRegisterCellColorEvent();
            SetupRepeater();
        }
        #endregion

        #region viewstate and other
        private DateTime TransferDate(int date)
        {
            System.DateTime guardTime = new DateTime(2020, 07, 21);//7507
            System.DateTime selectDate = guardTime.AddDays(date - 7507);
            return selectDate;
        }

        private List<DateTime> GetChooseFromViewState()
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

        private void SaveChooseToViewState(DateTime dateTime)
        {
            MODEL.Apply.ViewState_page data = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, ViewState);

            if (data != null)
            {
                List<MODEL.Apply.apply_LeaveData> selected = data.LeaveList;
                if (selected != null)
                {
                    var temp = selected.Find(x => x.LeaveDate == dateTime);
                    if (temp == null)
                    {
                        int leaveId = int.Parse(data.LeaveTypeSelectValue);
                        int sectiontype =int.Parse(data.ddlsectionSelectvalue);
                        string leavename = LSLibrary.WebAPP.ValueText<int>.GetText(data.leavetype, int.Parse(data.LeaveTypeSelectValue));

                        var newitem = new MODEL.Apply.apply_LeaveData(leaveId, leavename, leavename, sectiontype, dateTime);
                        data.LeaveList.Add(newitem);
                    }
                    else
                    {
                        data.LeaveList.Remove(temp);
                    }
                }
            }
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

        private bool GetIsMeOrTeam()
        {
            return this.btn_myself.CssClass == css_select ? true : false;
        }
        #endregion
    }
}