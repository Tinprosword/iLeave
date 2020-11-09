using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebServiceLayer.MyModel
{
    public class LeaveMaster
    {
        private WebServiceLayer.WebReference_leave.LeaveRequestMaster data;

        public LeaveMaster(WebServiceLayer.WebReference_leave.LeaveRequestMaster _data)
        {
            data = _data;
        }

        public string Info_GetFromto()
        {
            string result = "{0} To {1}({2} Day)";
            result= string.Format(result, data.leavefrom.ToString("yyyy-MM-dd"), data.leaveto.ToString("yyyy-MM-dd"), data.totaldays);
            return result;
        }

        public string Info_GetBalance()
        {
            string result = "";
            result = data.maxLeaveCode + ":x Day";
            return result;
        }

        public string Info_GetApplydate()
        {
            string result = "Apply Date:" + data.createDate.ToString("yyyy-MM-dd");
            return result;
        }

        public string Info_GetAttachment()
        {
            string result = data.paths == null ? "" : data.paths;
            return result;
        }

        public bool Info_BShowPanel(int usertype,int bigrange,int requestBigrange)
        {

            return true;
        }
    }
}