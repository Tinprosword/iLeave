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

            var tempResult= WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetCLOTDetail_UpdateCanceWaitingStatus(new int[] { requestID }).ToList();
            if (tempResult != null && tempResult.Count() > 0)
            {
                result = tempResult;
            }
            return result;
        }

        public static List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> GetMyCLOTUID(int firsteid, GlobalVariate.LeaveBigRangeStatus status, int year)
        {
            DateTime from = new DateTime(year, 1, 1);
            int dayCount = DateTime.DaysInMonth(year, 12);
            DateTime? to = new DateTime(year, 12, dayCount);
            return GetMyClOT_AllByUID(firsteid, status, from, to);
        }

        public static List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> GetMyClOTByRequestidUID(int firstEID, GlobalVariate.LeaveBigRangeStatus status, int requestid)
        {
            List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> result = GetMyClOT_AllByUID(firstEID, status, null, null);

            result = result.Where(x => x.ID == requestid).ToList();

            return result;
        }

        public static List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> GetMyClOT_AllByUID(int firstEID, GlobalVariate.LeaveBigRangeStatus status, DateTime? from, DateTime? to)
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
            result = result.OrderByDescending(x => x.TimeFrom).ThenByDescending(x => x.CreateDate).ToList();
            return result;
        }

        

        public static List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> GetMyManageClOT(int uid, GlobalVariate.LeaveBigRangeStatus status, int year, string name)
        {
            DateTime from = new DateTime(year, 1, 1);
            int dayCount = DateTime.DaysInMonth(year, 12);
            DateTime? to = new DateTime(year, 12, dayCount);
            return GetMyManageClOT(uid, status, from, to, name);
        }


        public static List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> GetMyManageClOTByRequestid(int uid, GlobalVariate.LeaveBigRangeStatus status,int requestid)
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

            result = result.Where(x => x.ID == requestid).ToList();

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


        #endregion

        #region insert
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
                tempData.Hour = item.GetHoursFromStringMember();
                tempData.Section = item.section;

                var requestid= InsertCLOTRequest(tempData, createrUid, applyerEID);
                if (requestid > 0)
                {
                    result.Add(requestid);
                }
                else
                {
                    result.Add(requestid);
                    break;
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
                string baseurl = workflow.GetTestBaseUrl();// BLL.Other.GetHRWebSiteRootUrl();
                int workInfoid= WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.CreateNewWorkflow_colt(WebServiceLayer.WebReference_leave.WorkflowTypeID.CLOT_APPLICATION, createUid, request.Remarks, requestid, applyerEID, baseurl);
                
                result = requestid;
                
            }
            return result;
        }

        public static string CheckOnApplyList(List<MODEL.CLOT.CLOTItem> data, double balance,int eid,LSLibrary.WebAPP.LanguageType languageType,int applygroupid)
        {
            string result = "";
            if (data == null || data.Count() == 0)
            {
                result = BLL.MultiLanguageHelper.GetLanguagePacket().Common_msg_CannotEmptyData;
            }


            //check balance
            if (result == "")
            {
                double ApplyTotal = MODEL.CLOT.CLOTItem.GetTotalUnit(data);
                if (ApplyTotal + balance<0)
                {
                    result = BLL.MultiLanguageHelper.GetLanguagePacket().Common_limitbalance;
                }
            }

            //check hasApplyGroup
            if (result == "")
            {
                if (applygroupid <= 0)
                {
                    result = BLL.MultiLanguageHelper.GetLanguagePacket().Common_groupisemp;
                }
            }


            //check predate
            if (result == "")
            {
                foreach (var tempItem in data)
                {
                    if (BLL.CLOT.checkIsOverlap(eid, tempItem.GetFrom(), tempItem.GetTo()) == 1)
                    {
                        result = tempItem.GetTimeRangeDesc() +" "+ BLL.MultiLanguageHelper.GetLanguagePacket().common_msg_overlap;
                        break;
                    }
                }
            }

            
            
            

            //sp chec @ParaEmploymentID varchar(6), @ParaType int, @ParaDateFrom datetime,@ParaDateTo datetime,@ParaRangeHours float= 0
            if (result == "")
          {
                for (int i = 0; i < data.Count; i++)
                {
                    DateTime from = new DateTime(data[i].date.Year, data[i].date.Month, data[i].date.Day, data[i].fromhour, data[i].frommin, 0);
                    DateTime to = new DateTime(data[i].date.Year, data[i].date.Month, data[i].date.Day, data[i].tohour, data[i].tominute, 0);
                    string splancode = BLL.GlobalVariate.GetSPLanguageCode(languageType);
                    string[] spPs = new string[] { eid.ToString(), ((int)data[i].type).ToString(), from.ToString("yyyy-MM-dd HH:mm:ss"), to.ToString("yyyy-MM-dd HH:mm:ss"), data[i].numberofHours.ToString(), splancode };
                    string spCheckResult = BLL.Other.ExeStropFun((int)BLL.GlobalVariate.spFunctionid.clot_add_portal, true, spPs);

                    if (!string.IsNullOrEmpty(spCheckResult))
                    {
                        result = spCheckResult;
                        break;
                    }
                }
            }
            
            return result;
        }
        #endregion

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

            string halfStr = "";
            if (clot.Section == 1)
            {
                halfStr = "AM";
            }
            else if (clot.Section == 2)
            {
                halfStr = "PM";
            }
            else if (clot.Section == 0)
            {
                halfStr = "FullDay";
            }

            return strdate + " " + time + " (" + hours + " " + BLL.MultiLanguageHelper.GetLanguagePacket().applyCLOT_list_Hours + " "+ halfStr+ ")";
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

        public static float CalculateNumberofHours(int fromhour, int tohour, int frommin, int tominute,DateTime day)
        {
            int h = tohour - fromhour;
            int m = tominute - frommin;
            int totalmin = h * 60 + m;
            float result = (float)(Math.Round((double)((double)totalmin / 60), 2));
            return result;
        }

        public static int checkIsOverlap(int eid, DateTime from, DateTime to)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.clot_CheckCLOTIsOverlap(eid, from, to);
        }

        public static List<StaffCLOTRequest> GetCLOTDetails(int[] ids)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetCLOTDetail_UpdateCanceWaitingStatus(ids).ToList();
        }

        public static void GetWorkHourInfoByShift(WebServiceLayer.WebReference_codesettings.Shift  theShift, out DateTime amfrom, out DateTime amto, out DateTime pmfrom, out DateTime pmto, out double amhours, out double pmhours, out double fullhours)
        {
            fullhours = 7.5; amhours = 3.5; pmhours = 4;
            amfrom = new System.DateTime(2022, 1, 1, 9, 0, 0);
            amto = new System.DateTime(2022, 1, 1, 12, 30, 0);
            pmfrom = new System.DateTime(2022, 1, 1, 2, 1, 0);
            pmto = new System.DateTime(2022, 1, 1, 18, 1, 0);

            if (theShift == null)
            {
                return;
            }

            fullhours = theShift.TotalWorkHour;
            amhours = theShift.AMWorkingHour;
            pmhours = theShift.PMWorkingHour;
            amfrom = theShift.BankOnTime;
            amto = theShift.LunchIn;
            pmfrom = theShift.LunchOut;
            pmto = theShift.BankOffTime;

            bool needResetAmPm = false;
            if (amto.ToString("yyyy-MM-dd") == "1900-01-01" || pmfrom.ToString("yyyy-MM-dd") == "1900-01-01")
            {
                needResetAmPm = true;
            }

            if (needResetAmPm)
            {
                amhours = fullhours / 2.0;
                pmhours = amhours;
                amto = amfrom.AddHours(amhours);
                pmfrom = amto;
            }
        }


    #endregion

        #region check
    //-1 hours is small zero ,或者不是数字 -4 override in Repeater.
    public static int CheckOnAddSingleItem(MODEL.CLOT.CLOTItem tempItem, MODEL.CLOT.ViewState_page dataview, int eid)
        {
            int result = 1;

            bool checkInRepeater = CheckOverlapInReapter_OnInsert(tempItem, dataview);
            if (checkInRepeater == true)
            {
                result = -4;
            }

            if (result == 1)
            {
                if (tempItem.GetHoursFromStringMember() <= 0)
                {
                    result = -1;
                }
            }

            return result;
        }

        public static bool CheckOverlapInReapter_OnInsert(MODEL.CLOT.CLOTItem currentItem, MODEL.CLOT.ViewState_page dataview)
        {
            bool result = false;
            var applyitmes = dataview.items;
            if (applyitmes != null && applyitmes.Count() > 0)
            {
                foreach (var item in applyitmes)
                {
                    if (LSLibrary.ValidateUtil.checkIsOverlap(currentItem.GetFrom(), currentItem.GetTo(), item.GetFrom(), item.GetTo()))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        #endregion
    }
}