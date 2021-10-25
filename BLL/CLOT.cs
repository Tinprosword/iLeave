using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceLayer.WebReference_leave;

namespace BLL
{
    public class CLOT
    {
        #region getclot
        public static List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> GetCLOTDetail(int requestID)
        {
            List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> result = new List<StaffCLOTRequest>();

            var tempResult= WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetCLOTDetail(new int[] { requestID }).ToList();
            if (tempResult != null && tempResult.Count() > 0)
            {
                result = tempResult;
            }
            return result;
        }

        public static List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> GetMyCLOT(int firsteid, GlobalVariate.LeaveBigRangeStatus status, int year)
        {
            DateTime from = new DateTime(year, 1, 1);
            int dayCount = DateTime.DaysInMonth(year, 12);
            DateTime? to = new DateTime(year, 12, dayCount);
            return GetMyClOT(firsteid, status, from, to);
        }

        public static List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> GetMyClOT(int firstEID, GlobalVariate.LeaveBigRangeStatus status, DateTime? from, DateTime? to)
        {
            List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> result = new List<StaffCLOTRequest>();
            if (status == GlobalVariate.LeaveBigRangeStatus.waitapproval)
            {
                result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetMyWaitingCLOT(firstEID).ToList();
            }
            else if (status == GlobalVariate.LeaveBigRangeStatus.beyongdWait)
            {
                result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetMyBeyondWaitingCLOT(firstEID).ToList();
            }

            if (from != null)
            {
                result = result.Where(x => x.Date >= from).ToList();
            }
            if (to != null)
            {
                result = result.Where(x => x.Date <= to).ToList();
            }

            return result;
        }

        public static List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> GetMyManageClOT(int uid, GlobalVariate.LeaveBigRangeStatus status, DateTime? from, DateTime? to, string name)
        {
            List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> result = new List<StaffCLOTRequest>();
            if (status == GlobalVariate.LeaveBigRangeStatus.waitapproval)
            {
                result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetMyManageWaitingCLOT(uid).ToList();
            }
            else if (status == GlobalVariate.LeaveBigRangeStatus.beyongdWait)
            {
                result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetMyManageBeyondWaitingCLOT(uid).ToList();
            }

            if (from != null)
            {
                result = result.Where(x => x.Date >= from).ToList();
            }
            if (to != null)
            {
                result = result.Where(x => x.Date <= to).ToList();
            }

            if (!string.IsNullOrEmpty(name))
            {
                result = result.Where(x => MODEL.UserName.IsNameLike(x.Name,x.NameCH,name )==true).ToList();
            }

            return result;
        }

        public static List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> GetMyManageClOT(int uid, GlobalVariate.LeaveBigRangeStatus status, int year, string name)
        {
            DateTime from = new DateTime(year, 1, 1);
            int dayCount = DateTime.DaysInMonth(year, 12);
            DateTime? to = new DateTime(year, 12, dayCount);
            return GetMyManageClOT(uid, status, from, to, name);
        }
        #endregion


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
                int workInfoid= WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.CreateNewWorkflow_colt(WebServiceLayer.WebReference_leave.WorkflowTypeID.CLOT_APPLICATION, createUid, request.Remarks, requestid, applyerEID, common.baseUrl);
                if (workInfoid > 0)
                {
                    result = requestid;
                }
            }
            return result;
        }


        #region other function
        public static string showCLOTTime(WebServiceLayer.WebReference_leave.StaffCLOTRequest clot)
        {
            string strdate = clot.Date.ToString("yyyy-MM-dd");
            string time = "";
            if (clot.TimeFrom == null || clot.TimeTo == null)
            {
                time = MODEL.CLOT.CLOTItem.GetTimeRangeDesc(0, 0, 0, 0);
            }
            else
            {
                time = MODEL.CLOT.CLOTItem.GetTimeRangeDesc(clot.TimeFrom.Value.Hour, clot.TimeTo.Value.Hour, clot.TimeFrom.Value.Minute, clot.TimeTo.Value.Minute);
            }
            string hours = "";
           
            hours = clot.Hour.ToString();

            return strdate + " " + time + " (" + hours + " " + BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_list_Hours + ")";
        }

        public static string showCLOTTimev2(WebServiceLayer.WebReference_leave.StaffCLOTRequest clot)
        {
            string time = "";
            if (clot.TimeFrom == null || clot.TimeTo == null)
            {
                time = MODEL.CLOT.CLOTItem.GetTimeRangeDesc(0, 0, 0, 0);
            }
            else
            {
                time = MODEL.CLOT.CLOTItem.GetTimeRangeDesc(clot.TimeFrom.Value.Hour, clot.TimeTo.Value.Hour, clot.TimeFrom.Value.Minute, clot.TimeTo.Value.Minute);
            }
            string hours = "";

            hours = clot.Hour.ToString();

            return   time + " (" + hours + " h)";
        }
        #endregion
    }
}