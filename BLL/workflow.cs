using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceLayer.WebReference_leave;

namespace BLL
{
    public class workflow
    {
        public static string GetTestBaseUrl()
        {
            return BLL.Other.GetHRWebSiteRootUrl();
            //return "http://localhost/WEBUI/";
        }


        public static Dictionary<int, string> names
        {
            get
            {
                Dictionary<int, string> names = new Dictionary<int, string>();
                names.Add(0, "Apply leave");
                names.Add(10, "Cancel leave");
                return names;
            }
        }
        //public static string LEAVE_DESC = "";

        #region insert
        public static int InsertWorkflow(WebServiceLayer.WebReference_leave.StaffLeaveRequest[] details, int uid, int requestLeaveID, int employMentID)
        {
            int result = 0;
            string baseUrl = GetTestBaseUrl();
            if (System.Web.HttpContext.Current!=null)
            {
                baseUrl=GetTestBaseUrl();

                common.WriteLog("getrooturl:" + baseUrl);
            }
            
            result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.CreateNewWorkflow(null, WebServiceLayer.WebReference_leave.WorkflowTypeID.LEAVE_APPLICATION, details, uid, "", "",baseUrl  , "", requestLeaveID, employMentID);
            return result;
        }
        #endregion

        #region update
        public static bool ApproveRequest_leave(int requestid,int HandlerUID,string remark, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = ApproveRequest_leave_Check();
            if (check > 0)
            {
                string baseurl = GetTestBaseUrl();
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.ApproveRequest_leave(requestid, HandlerUID,remark, baseurl);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool RejectRequest_leave(int requestid, int HandlerUID,string remarks, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_RejectRequest_leave();
            if (check > 0)
            {
                string baseurl = GetTestBaseUrl();
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.RejectRequest_leave(requestid, HandlerUID, remarks, baseurl);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool WithDrawRequest_leave(int requestid , int HandlerUID,string remark, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            List<LeaveRequestDetail> details = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetExtendLeaveDetailsByReuestID(requestid).ToList();
            var check = WithDrawRequest_leave_Check(details);
            if (check.mResult)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.WithDrawRequest_leave(requestid, HandlerUID,remark);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = check.mMessage;
            }
            return result;
        }

        public static LSLibrary.WebAPP.CodeHelper.CommonReturnResult<int> CancelRequest_leave(int requestid, int HandlerUID,string remark)
        {
            LSLibrary.WebAPP.CodeHelper.CommonReturnResult<int> result = new LSLibrary.WebAPP.CodeHelper.CommonReturnResult<int>(0, "");
            List<LeaveRequestDetail> details = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetExtendLeaveDetailsByReuestID(requestid).ToList();
            var check = CancelRequest_leave_check(details);
            if (check.mResult)
            {
                string baseurl = GetTestBaseUrl();
                //return cancel Requestid.
                int cancelRequestID=WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.CancelRequest_leave(requestid, HandlerUID, remark,baseurl);
                if (cancelRequestID > 0)
                {
                    result.mResult = cancelRequestID;
                    result.mMessage = "";
                }
                else
                {
                    result.mResult = 0;
                    result.mMessage = "Cancel Fail.\r\n";
                }
            }
            else
            {
                result.mResult = 0;
                result.mMessage = check.mMessage;
            }
            return result;
        }

        public static bool ApprovalCancelRequest_leave(int requestid, int HandlerUID, string remark, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_ApprovalCancelRequeste();
            if (check > 0)
            {
                string baseurl = GetTestBaseUrl();
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.ApproveCancelRequest_leave(requestid, HandlerUID, remark, baseurl);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }


        public static bool RejectCancelRequest_leave(int requestid, int HandlerUID, string remark, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_RejectCancelRequest();
            if (check > 0)
            {
                string baseurl = GetTestBaseUrl();
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.RejectCancelRequest_leave(requestid, HandlerUID,remark, baseurl);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        private static int Check_RejectCancelRequest()
        {
            return 1;
        }

        private static int Check_ApprovalCancelRequeste()
        {
            return 1;
        }

        private static int ApproveRequest_leave_Check()
        {
            return 1;
        }
        private static int Check_RejectRequest_leave()
        {
            return 1;
        }
        private static LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool> WithDrawRequest_leave_Check(List<LeaveRequestDetail> details)
        {
            LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool> result = new LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool>(true, "");

            //check block
            if (BLL.SystemParameters.GetSysParameters().mBLOCK_BACKDATE_WITHDRAW)
            {
                var datelist = details.Select(x => x.LeaveTo).ToList();
                result.mResult = !BLL.Leave.isContainEarlierToday(BLL.Leave.ConvertDateListNull(datelist));
                if (result.mResult == false)
                {
                    result.mMessage += BLL.MultiLanguageHelper.GetLanguagePacket().Common_block_withdraw + "\r\n";
                }
            }

            //check other.


            return result;
        }

        private static LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool> CancelRequest_leave_check(List<LeaveRequestDetail> details)
        {
            LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool> result = new LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool>(true, "");

            //check block
            if (BLL.SystemParameters.GetSysParameters().mBLOCK_BACKDATE_CANCELLATION)
            {
                var datelist = details.Select(x => x.LeaveTo).ToList();
                result.mResult = !BLL.Leave.isContainEarlierToday(BLL.Leave.ConvertDateListNull(datelist));
                if (result.mResult == false)
                {
                    result.mMessage += BLL.MultiLanguageHelper.GetLanguagePacket().Common_block_cancel + "\r\n";
                }
            }

            //check other.


            return result;
        }
        #endregion

        #region search
        public static List<WebServiceLayer.WebReference_leave.t_WorkflowTask> Gett_WorkflowTaskByInfoID(int infoID)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.Gett_WorkflowTaskByInfoID(infoID).ToList();
        }

        public static WebServiceLayer.WebReference_leave.t_WorkflowInfo GetWorkInfoByID(int infoid)
        {
            WebServiceLayer.WebReference_leave.t_WorkflowInfo getItem = new WebServiceLayer.WebReference_leave.t_WorkflowInfo();
            getItem.ID = infoid;
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.Base_Gett_WorkflowInfo(getItem);
        }

        #endregion

        #region other
        public static String GetWorkFlowTypeName(int? type)
        {
            return type==null?"": names[(int)type];
        }
        #endregion

        #region update clot
        public static bool ApproveRequest_leave_clot(int requestid, int HandlerUID, string remark, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_ApproveRequest_leave_clot();
            if (check > 0)
            {
                string baseurl = GetTestBaseUrl();
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.ApproveRequest_clotv2(requestid, HandlerUID, remark,baseurl);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool RejectRequest_leave_clot(int requestid, int HandlerUID, string remarks, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_RejectRequest_leave_clot();
            if (check > 0)
            {
                string baseurl = GetTestBaseUrl();
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.RejectRequest_clotv2(requestid, HandlerUID, remarks,baseurl);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool WithDrawRequest_leave_clot(int requestid, int HandlerUID, string remark, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";

            var tempClotList = BLL.CLOT.GetCLOTDetail(requestid);
            LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool> checkResult = WithDrawRequest_leave_clot_Check(tempClotList);
            if (checkResult.mResult)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.WithDrawRequest_clot(requestid, HandlerUID, remark);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = checkResult.mMessage;
            }
            return result;
        }

        public static bool CancelRequest_leave_clot(int requestid, int HandlerUID, string remark, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";

            var tempClotList = BLL.CLOT.GetCLOTDetail(requestid);
            LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool> checkResult = CancelRequest_leave_clot_check(tempClotList);
            if (checkResult.mResult)
            {
                string baseurl = GetTestBaseUrl();
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.CancelCLOT(requestid, remark, HandlerUID, baseurl);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = checkResult.mMessage;
            }
            return result;
        }

        public static bool ApprovalCancelRequest_leave_clot(int requestid, int HandlerUID, string remark, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_ApprovalCancelRequeste_clot();
            if (check > 0)
            {
                string baseurl = GetTestBaseUrl();
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.ApproveRequest_Cancelclot(requestid, HandlerUID, remark, baseurl);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool RejectCancelRequest_leave_clot(int requestid, int HandlerUID, string remark, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_RejectCancelRequest_clot();
            if (check > 0)
            {
                string baseurl = GetTestBaseUrl();
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.RejectRequest_Cancelclot(requestid, HandlerUID, remark,baseurl);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        private static int Check_RejectCancelRequest_clot()
        {
            return 1;
        }

        private static int Check_ApprovalCancelRequeste_clot()
        {
            return 1;
        }

        private static int Check_ApproveRequest_leave_clot()
        {
            return 1;
        }

        private static int Check_RejectRequest_leave_clot()
        {
            return 1;
        }

        private static LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool> WithDrawRequest_leave_clot_Check(List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> details)
        {
            LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool> result = new LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool>(true, "");

            if (BLL.SystemParameters.GetSysParameters().mBLOCK_BACKDATE_WITHDRAW)
            {
                if (details != null && details.Count() > 0)
                {
                    var dateList = details.Where(x => x.Type == (int)MODEL.CLOT.enum_clotType.CL).Select(x => x.Date).ToList();
                    bool isEarlier = BLL.Leave.isContainEarlierToday(dateList);
                    if (isEarlier)
                    {
                        result.mResult = false;
                        result.mMessage = BLL.MultiLanguageHelper.GetLanguagePacket().Common_block_withdraw + "\r\n";
                    }
                }
            }

            return result;
        }

        private static LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool> CancelRequest_leave_clot_check(List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> details)
        {
            LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool> result = new LSLibrary.WebAPP.CodeHelper.CommonReturnResult<bool>(true, "");

            if (BLL.SystemParameters.GetSysParameters().mBLOCK_BACKDATE_CANCELLATION)
            {
                if (details != null && details.Count() > 0)
                {
                    var dateList = details.Where(x => x.Type == (int)MODEL.CLOT.enum_clotType.CL).Select(x => x.Date).ToList();
                    bool isEarlier = BLL.Leave.isContainEarlierToday(dateList);
                    if (isEarlier)
                    {
                        result.mResult = false;
                        result.mMessage = BLL.MultiLanguageHelper.GetLanguagePacket().Common_block_cancel + "\r\n";
                    }
                }
            }

            return result;
        }
        #endregion
    }
}