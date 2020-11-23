using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WEBUI.webservices
{
    /// <summary>
    /// Leave 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Leave : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetLeaveDetail(int requestID,string leaveCode,string staff,string employmentNo)
        {
            Data_GetLeaveDetail data = new Data_GetLeaveDetail();

            data.balance = 5;
            data.detail = BLL.Leave.GetExtendLeaveDetailsByReuestID(26063);

            return LSLibrary.MyJson.SObj(data);
        }

        [WebMethod]
        public Data_GetLeaveDetail testws()
        {
            var obj= new Data_GetLeaveDetail();
            obj.balance = 6;
            return obj;
        }

        [WebMethod]
        public string testws2()
        {
            return "hi ws";
        }

        public class Data_GetLeaveDetail
        {
            public double balance;
            public List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> detail;
        }
    }
}