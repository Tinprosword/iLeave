using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.t_attendanceClockGoGo t_Attendance_bll = new BLL.t_attendanceClockGoGo();
                DataTable dt= t_Attendance_bll.GetAllList();

                Model.t_attendanceClockGoGo clockGoGo = new Model.t_attendanceClockGoGo();
                clockGoGo.cardType = "type";
                clockGoGo.createTime = DateTime.Now;
                clockGoGo.date = "2019-01-01";
                clockGoGo.employmentCode = "new1";
                clockGoGo.gpsLat = 12.3m;
                clockGoGo.gpsLng = 13.4m;
                clockGoGo.id = null;
                clockGoGo.isBiometric = true;
                clockGoGo.time = "2019-01-01";
                clockGoGo.workspotCode = "abc";
                clockGoGo.workspotName = "hibig";

                //t_Attendance_bll.Add(clockGoGo);
                int a = 3;
            }
        }
    }
}