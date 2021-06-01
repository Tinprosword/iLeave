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
        private readonly string searchTip = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_serchTip;
        private readonly string css_select = "btnBox btnBlueBoxSelect";
        private readonly string css_unselect = "btnBox btnBlueBoxUnSelect";
        private bool isFromApply = false;
        private Dictionary<DateTime, int> allStatistic = null;
        private const string viewstate_statisc = "statistic";

        private readonly System.Drawing.Color apporvedcolor = System.Drawing.Color.FromArgb(0, 255, 0);
        private readonly System.Drawing.Color waitcolor = System.Drawing.Color.FromArgb(255, 255, 00); // System.Drawing.Color.FromArgb(234, 224, 56);//f3e926;  eae013
        private readonly System.Drawing.Color selectedColor = System.Drawing.Color.FromArgb(173, 216, 230);   //#2573a4  add8e6
        private readonly System.Drawing.Color btnokBGColor = System.Drawing.Color.FromArgb(88, 141, 167);//588da7

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
                this.Calendar1.PreRender += Calendar1_GetStatistic;
                this.Calendar1.DayRender += Calendar_displayStatistic;
                this.Calendar1.DayRender += Calendar_displayViewStateCells;
                this.Calendar1.DayRender += Calendar_displaySelectCell;

                ProcessEvent_ClickCellOrClickNextMonth();
            }
            else if (this.cb_holiday.Checked)
            {
                this.Calendar1.PreRender += Calendar1_GetStatistic;
                this.Calendar1.DayRender += Calendar_displayStatistic;
                this.Calendar1.DayRender += Calendar_displayViewStateCells;
                this.Calendar1.DayRender += Calendar_displaySelectCell;
                ProcessEvent_ClickCellOrClickNextMonth();
            }

            app_ok.Visible = isFromApply;
        }

        protected override void PageLoad_InitUIOnFirstLoad4()
        {
            //1.navigation. 2.init viewstate 3 setupZone .5repeater.
            this.Calendar1.SelectedDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);
            this.Calendar1.VisibleDate = new System.DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, System.DateTime.Now.Day);

            this.Image1.BackColor = apporvedcolor;
            this.Image2.BackColor = waitcolor;
            this.btn_ok.BackColor = btnokBGColor;
            

            OnPrePageIsApplyInitViewState();
            LSLibrary.WebAPP.MyJSHelper.SetTextBoxTip(tb_name,searchTip);
            SetupZone(loginer.userInfo.personid);
            SetupRepeater();
            SetupMultiLanguage();
        }


        protected override void PageLoad_Reset_ReInitUIOnEachLoad5()
        {
            setNavigation();//todo 因为viewstate无法保存event .所以只好放到这里来。所以不喜欢asp.net.  模拟cs，但是又不能完全模拟，太恶心了.
        }


        #region inner function
        private void SetupRepeater()
        {
            List<int> eid = GetEmployIDs(GetIsMeOrTeam(), this.ddlzone.SelectedValue,this.tb_name.Text.Trim());

            if (this.cb_leave.Checked)
            {
                //this.divTip.Visible = true;
                this.leaveDiv.Visible = true;
                this.rosterDiv.Visible = false;
                var repeaterSource = BLL.Leave.getWaitingApproveAndApprovedByEIDS_Date(this.Calendar1.SelectedDate, eid);
                this.repeater_leave.DataSource = repeaterSource;
                this.repeater_leave.DataBind();
            }
            else if (this.cb_holiday.Checked)
            {
                //this.divTip.Visible = false;
                this.leaveDiv.Visible = false;
                this.rosterDiv.Visible = true;
                var repeaterSource = BLL.calendar.GetRoster(this.Calendar1.SelectedDate, eid);
                this.rp_roster.DataSource = repeaterSource;
                this.rp_roster.DataBind();
            }
        }





        private void ProcessEvent_ClickCellOrClickNextMonth()
        {
            string target = Request.Params["__EVENTTARGET"];
            string argument = Request.Params["__EVENTARGUMENT"];
            if (target != null && target.Contains("Calendar1"))
            {
                if (argument.Contains("V"))//
                {
                    int intdate = int.Parse(argument.Substring(1, argument.Length - 1));
                    DateTime date = TransferDate(intdate);
                    this.Calendar1.VisibleDate = date;
                    ViewState[viewstate_statisc] = null;
                }
                else
                {
                    int intdate = int.Parse(argument);
                    DateTime date = TransferDate(intdate);
                    Calendar1_CellChanged(this.Calendar1, date);
                }
            }
        }

        private void Calendar1_GetStatistic(object sender, EventArgs e)
        {
            if (ViewState[viewstate_statisc] == null)
            {
                bool isme = GetIsMeOrTeam();
                List<int> eid = GetEmployIDs(isme, this.ddlzone.SelectedValue, this.tb_name.Text.Trim());
                if (this.Calendar1.VisibleDate.Year > 1)
                {
                    FillStatistic(eid, this.Calendar1.VisibleDate.Year, this.Calendar1.VisibleDate.Month);
                }
                ViewState[viewstate_statisc] = allStatistic;
            }
            else
            {
                allStatistic =(Dictionary<DateTime, int>) ViewState[viewstate_statisc];
            }
        }

        private List<int> GetEmployIDs(bool isme,string contractinfo,string name)
        {
            //1.me or team 2.zone 3 get employment ids.
            name = name == searchTip ? "" : name;
            var zoneArray = contractinfo.Split(new char[] { '|' });
            string contarctID = zoneArray[0];
            string zoneCode = zoneArray[1];
            int intContractId = int.Parse(contarctID);
            List<int> eid = new List<int>();
            var Employment = BLL.User_wsref.getEmploymentByZone(intContractId, zoneCode);
            if (isme)
            {
                var sids = BLL.User_wsref.GetStaffsByUid(loginer.userInfo.personid);
                eid = Employment.Where(x => sids.Contains(x.StaffID)).Select(x => x.ID).ToList();
            }
            else
            {
                eid = Employment.Select(x => x.ID).ToList();
                if (!string.IsNullOrEmpty(this.tb_name.Text))
                {
                    var likenamesids = BLL.User_wsref.GetPersonBaseInfoByLikeName(name).Select(x => x.e_id).ToList();
                    eid = eid.Where(x => likenamesids.Contains(x)).ToList();
                }
            }
            return eid;
        }

        private void setNavigation()
        {
            if (isFromApply)
            {
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, BLL.MultiLanguageHelper.GetLanguagePacket().apply_calendar_current,null, true, BackEvent);
                this.btn_ok.Click += BackEvent;
            }
            else
            {
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().Back, BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_current, "~/pages/main.aspx", true);
            }
        }

        private void BackEvent(object sender, EventArgs e)
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
                this.ddlzone.Items.Add(new ListItem(contracts[i].Code +":"+ contracts[i].zonecode+ " - " + contracts[i].zonedescription , contracts[i].contractid + "|" + contracts[i].zonecode));
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
            allStatistic = new Dictionary<DateTime, int>();//初始并填充数据.
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


        private void Calendar_displayStatistic(object sender, DayRenderEventArgs e)
        {
            //1.show approved 2.show wait 3.show selected dates. 4.show selected
            if (allStatistic.Keys.Contains(e.Day.Date))
            {
                if (allStatistic[e.Day.Date] == 1)//approvaed
                {
                    e.Cell.BackColor = apporvedcolor;
                    e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#000000");
                }
                else if (allStatistic[e.Day.Date] == 2)//waiting
                {
                    e.Cell.BackColor = waitcolor;
                    e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#000000");
                }
            }

            
        }

        private void Calendar_displaySelectCell(object sender, DayRenderEventArgs e)
        {
            //SelectedDayStyle - BorderWidth = "2"  SelectedDayStyle - BorderColor = "#bd4f8b" SelectedDayStyle - BackColor = "White" SelectedDayStyle - ForeColor = "Black"
            if (Calendar1.SelectedDate == e.Day.Date)
            {
                if (e.Cell.ForeColor == System.Drawing.Color.White)//非特殊日子
                {
                    e.Cell.BackColor = System.Drawing.Color.White;
                    e.Cell.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    e.Cell.BackColor = e.Cell.BackColor;
                    e.Cell.ForeColor = e.Cell.ForeColor;
                }
                e.Cell.BorderWidth = 2;
                e.Cell.BorderStyle = BorderStyle.Solid;
                e.Cell.BorderColor = System.Drawing.Color.Black;
            }
        }

        private void Calendar_displayViewStateCells(object sender, DayRenderEventArgs e)
        {
            List<DateTime> chooseDate = GetChooseFromViewState();//choose days
            if (chooseDate.Contains(e.Day.Date))
            {
                e.Cell.BackColor = selectedColor;
                e.Cell.ForeColor = LSLibrary.MyColors.ParseColor("#000000");
            }
        }
        #endregion

        #endregion

        #region showleave data
        protected void Calendar1_CellChanged(Calendar calendar, DateTime date)
        {
            //1.set changedDate  2.show related request  3.save date to viewstate.
            this.Label1.Text = date.ToString("yyyy-MM-dd");
            calendar.SelectedDate = date;
            SetupRepeater();
            if (isFromApply)
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
            ViewState[viewstate_statisc] = null;
            this.btn_myself.CssClass = css_select;
            this.btn_team.CssClass = css_unselect;
            SetupRepeater();
        }

        protected void tb_name_TextChanged(object sender, EventArgs e)
        {
            SetupRepeater();
        }

        protected void btn_team_Click(object sender, EventArgs e)
        {
            ViewState[viewstate_statisc] = null;
            this.btn_myself.CssClass = css_unselect;
            this.btn_team.CssClass = css_select;
            SetupRepeater();
        }
        #endregion

        #region show holiday
        protected void cb_leave_CheckedChanged(object sender, EventArgs e)
        {
            //this.Calendar1.PreRender += Calendar1_GetStatistic;
            //this.Calendar1.DayRender += Calendar_displayStatistic;
            //this.Calendar1.DayRender += Calendar_displayViewStateCells;
            //this.Calendar1.DayRender += Calendar_displaySelectCell;

            SetupRepeater();
        }

        protected void cb_holiday_CheckedChanged(object sender, EventArgs e)
        {
            //this.Calendar1.PreRender -= Calendar1_GetStatistic;
            //this.Calendar1.DayRender -= Calendar_displayStatistic;
            //this.Calendar1.DayRender -= Calendar_displayViewStateCells;
            //this.Calendar1.DayRender -= Calendar_displaySelectCell;

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

        //todo 0这里应该有,是否跳过,周末和假期的设定.并且考虑把是否和与请假的日期对比功能放到这里来.虽然需要远程检测,但是流程上更统一,简洁.
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
                        data.LeaveList = data.LeaveList.OrderBy(x => x.LeaveDate).ToList();
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
            this.lt_section.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_listsection;
            this.lt_status.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_liststatus;
            this.btn_ok.Text= BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_add;
            this.lt_displayname.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_listname;
            this.lt_shiftCode.Text = BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_list2_shift;
            this.lt_remark.Text= BLL.MultiLanguageHelper.GetLanguagePacket().cancendar_list2_remark;
        }

        private bool GetIsMeOrTeam()
        {
            return this.btn_myself.CssClass == css_select ? true : false;
        }
        #endregion

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}