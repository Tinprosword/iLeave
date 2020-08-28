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
        public StateBag myviewState;
        private bool isFromApply = false;
        private Dictionary<DateTime, int> allStatistic = new Dictionary<DateTime, int>();

        #region pageevent
        protected override void InitPageVaralbal0()
        {
            WEBUI.Pages.Apply prepage = PreviousPage as WEBUI.Pages.Apply;
            myviewState = ViewState;
        }

        

        protected override void InitPageDataOnEachLoad1()
        {}

        protected override void InitPageDataOnFirstLoad2()
        {}

        protected override void ResetUIOnEachLoad3()
        {
            //1.check page source 2.register control render 3.intercept calendar 事件，以代替默认的changed事件（因为要捕捉cancel cell事件） 4.生成统计信息
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "apply")
            {
                isFromApply = true;
            }

            this.Calendar1.DayRender += Calendar1_DayRender;

            string target = Request.Params["__EVENTTARGET"];
            string argument = Request.Params["__EVENTARGUMENT"];
            if (target != null && target.Contains("Calendar1"))
            {
                if (argument.Contains("V"))
                {
                    int intdate = int.Parse(argument.Substring(1,argument.Length-1));
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


        protected override void InitUIOnFirstLoad4()
        {
            Debug.Print("InitUIOnFirstLoad4");
            //1.init viewstate 1.5 setupZone  2.navigation 3.repeater.
            this.Calendar1.SelectedDate = System.DateTime.Now;//todo month
            this.Calendar1.VisibleDate = System.DateTime.Now;

            
            OnPrePageIsApplyInitViewState();
            setNavigation();
            SetupZone();

            List<int> eid = GetEmployIDs();
            if (this.Calendar1.VisibleDate.Year > 1)//todo 上面的方法，使连续的，为什么活不到直，需要这里再一次？
            {
                FillStatistic(eid, this.Calendar1.VisibleDate.Year, this.Calendar1.VisibleDate.Month);
            }

            this.repeater_leave.DataSource = BLL.Leave.getListSource(this.Calendar1.SelectedDate, eid);
            this.repeater_leave.DataBind();

            SetupMultiLanguage();
        }

        protected override void ResetUIOnEachLoad5()
        {
            List<int> eid = GetEmployIDs();
            if (this.Calendar1.VisibleDate.Year > 1)
            {
                FillStatistic(eid, this.Calendar1.VisibleDate.Year, this.Calendar1.VisibleDate.Month);
            }
        }

        #region inner function
        private void setNavigation()
        {
            if (isFromApply)
            {
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().apply_button, BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_current, "~/pages/apply.aspx?action=backCalendar");
            }
            else
            {
                ((WEBUI.Controls.leave)this.Master).SetupNaviagtion(true, BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_back, BLL.MultiLanguageHelper.GetLanguagePacket().canlendar_current, "~/pages/main.aspx");
            }
        }

        private void SetupZone()
        {
            var result = BLL.User_wsref.GetPersonBaseInfoByPid(loginer.userInfo.personid);
            List<int?> eids = BLL.User_wsref.FilterValidUser(result).Select(x => x.e_id).ToList();
            List<int> eids2 = new List<int>();
            foreach (int? ii in eids)
            {
                if (ii != null) eids2.Add((int)ii);
            }
            var contracts = BLL.calendar.GetContractByEmployids(eids2.ToArray());
            this.ddlzone.Items.Add(new ListItem("All Zone", "0|0"));
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
                WEBUI.Pages.Apply prepage = PreviousPage as WEBUI.Pages.Apply;
                if (prepage != null)
                {
                    MODEL.Apply.ViewState_page applyPage2 = LSLibrary.WebAPP.ViewStateHelper.GetValue<MODEL.Apply.ViewState_page>(Apply.ViewState_PageName, prepage.myviewState);
                    LSLibrary.WebAPP.ViewStateHelper.SetValue(applyPage2, Apply.ViewState_PageName, ViewState);
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
        private List<int> GetEmployIDs()
        {
            //1.me or team 2.zone 3 get employment ids.
            bool isme = GetIsMeOrTeam();
            var zoneArray = this.ddlzone.SelectedValue.Split(new char[] { '|' });
            string contarctID = zoneArray[0];
            string zoneCode = zoneArray[1];
            int intContractId = int.Parse(contarctID);
            List<int> eid = new List<int>();
            if (isme)
            {
                var myEmployment= BLL.User_wsref.getEmploymentByZone(intContractId, zoneCode);
                eid = myEmployment.Select(x => x.ID).ToList();
            }
            return eid;
        }

        protected void Calendar1_SelectionChanged(Calendar calendar, DateTime date)
        {
            //1.set changedDate  2.show related request  3.save date to viewstate.
            calendar.SelectedDate = date;
            List<int> eid = GetEmployIDs();
            this.repeater_leave.DataSource = BLL.Leave.getListSource(calendar.SelectedDate, eid);
            this.repeater_leave.DataBind();

            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "apply")
            {
                SaveChooseToViewState(calendar.SelectedDate);
            }
        }

        protected void unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> eid = GetEmployIDs();
            this.repeater_leave.DataSource = BLL.Leave.getListSource(this.Calendar1.SelectedDate, eid);
            this.repeater_leave.DataBind();
        }

        protected void btn_myself_Click(object sender, EventArgs e)
        {
            List<int> eid = GetEmployIDs();
            this.repeater_leave.DataSource = BLL.Leave.getListSource(this.Calendar1.SelectedDate,eid);
            this.repeater_leave.DataBind();
            this.btn_myself.CssClass = css_select;
            this.btn_team.CssClass = css_unselect;
        }

        protected void btn_team_Click(object sender, EventArgs e)
        {
            List<int> eid = GetEmployIDs();
            this.repeater_leave.DataSource = BLL.Leave.getListSource(this.Calendar1.SelectedDate, eid);
            this.repeater_leave.DataBind();
            this.btn_myself.CssClass = css_unselect;
            this.btn_team.CssClass = css_select;
        }

        protected void cb_leave_CheckedChanged(object sender, EventArgs e)
        {
            List<int> eid = GetEmployIDs();
            this.repeater_leave.DataSource = BLL.Leave.getListSource(this.Calendar1.SelectedDate, eid);
            this.repeater_leave.DataBind();
        }


        #endregion

        #region show holiday
        protected void cb_holiday_CheckedChanged(object sender, EventArgs e)
        {

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