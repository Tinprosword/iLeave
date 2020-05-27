//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace WEBUI.Pages
//{
//    //1写流程.  2.写最佳实践(要注意ResetUIOnEachLoad,只做一些清除工作, 会有很多工作其实是放入到 事件结尾处,由某个事件所带来的ui更新,并非每次都要)
//    public partial class Test : AppLibraly.PageTemplate_logined
//    {
//        private List<Model.t_attendanceClockGoGo> data = null;

//        protected override void InitPageDataOnEachLoad()
//        {
//            int realcount = 0;
//            int realpagecount = 0;
//            DataTable dataa = AppHelper.GlobalVariate.bll_t_Attendance.GetPage("", "autoid", "order by autoid desc", 1, 10, out realcount, out realpagecount);
//            data = AppHelper.GlobalVariate.bll_t_Attendance.DataTableToList(dataa);
//        }

//        protected override void ResetUIOnEachLoad()
//        {
            
//        }

//        protected override void InitUIOnFirstLoad()
//        {
//            this.rpt_data.DataSource = data;
//            this.rpt_data.DataBind();
//        }

//        protected void Button1_Click(object sender, EventArgs e)
//        {
//            string employmentCode = "";
//            employmentCode = this.tb_employmentCode.Text.Trim();
//            if (string.IsNullOrEmpty(employmentCode) == false)
//            {

//                Model.t_attendanceClockGoGo clockGoGo = new Model.t_attendanceClockGoGo();
//                clockGoGo.cardType = "type";
//                clockGoGo.createTime = DateTime.Now;
//                clockGoGo.date = "2019-01-01";
//                clockGoGo.employmentCode = employmentCode;
//                clockGoGo.gpsLat = 12.3m;
//                clockGoGo.gpsLng = 13.4m;
//                clockGoGo.id = null;
//                clockGoGo.isBiometric = true;
//                clockGoGo.time = "2019-01-01";
//                clockGoGo.workspotCode = "wo";
//                clockGoGo.workspotName = "hibig";

//                AppHelper.GlobalVariate.bll_t_Attendance.Add(clockGoGo);

//                this.rpt_data.DataSource = data;
//                this.rpt_data.DataBind();
//            }
//        }

//        protected void Button2_Click(object sender, EventArgs e)
//        {
//            AppLibraly.LoginManager.Logoff();
//            Response.Redirect("test.aspx");
//        }

//    }
//}