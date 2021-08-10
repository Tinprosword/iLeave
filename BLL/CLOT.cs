using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CLOT
    {
        public static List<int> InsertCLOTRequests(List<MODEL.CLOT.CLOTItem> items, int createrUid, int applyerEID)
        {
            List<int> result = new List<int>();

            foreach (var item in items)
            {
                WebServiceLayer.WebReference_leave.StaffCLOTRequest tempData = new WebServiceLayer.WebReference_leave.StaffCLOTRequest();
                tempData.Date = item.date;
                tempData.TimeFrom = new DateTime(item.date.Year, item.date.Month, item.date.Day, item.fromhour, item.frommin, 0);
                tempData.TimeTo = new DateTime(item.date.Year, item.date.Month, item.date.Day, item.tohour, item.tominute, 0);
                tempData.Type = (int)item.type;
                tempData.EmploymentID = applyerEID;
                tempData.Remarks = item.remark;
                tempData.Hour = item.GetHours();


                var requestid= InsertCLOTRequest(tempData, createrUid, applyerEID);
                if (requestid > 0)
                {
                    result.Add(requestid);
                }
            }

            return result;
        }

        private static int InsertCLOTRequest(WebServiceLayer.WebReference_leave.StaffCLOTRequest request,int createUid,int applyerEID)
        {
            int result = -1;
            var tempResult = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.InsertCLOTRequest(request, createUid, "ILeave");
            string errorMsg = tempResult.ErrorMessage;
            int requestid = tempResult.ProcessID;
            if (string.IsNullOrWhiteSpace(errorMsg) && requestid>0)
            {
                int workInfoid= WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.CreateNewWorkflow_colt(WebServiceLayer.WebReference_leave.WorkflowTypeID.CLOT_APPLICATION, createUid, request.Remarks, requestid, applyerEID);
                if (workInfoid > 0)
                {
                    result = requestid;
                }
            }
            return result;
        }
    }
}