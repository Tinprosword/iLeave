using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class workflow
    {
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
        public static string LEAVE_DESC = "Leave Request";

        #region insert
        public static int InsertWorkflow(WebServiceLayer.WebReference_leave.StaffLeaveRequest[] details, int uid, int requestLeaveID, int employMentID)
        {
            int result = 0;
            result = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.CreateNewRequest(null, WebServiceLayer.WebReference_leave.WorkflowTypeID.LEAVE_APPLICATION, details, uid, LEAVE_DESC, "", "", "", requestLeaveID, employMentID);
            return result;
        }
        #endregion

        #region update
        public static bool ApproveRequest_leave(int requestid,int HandlerUID,out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_ApproveRequest_leave();
            if (check > 0)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.ApproveRequest_leave(requestid, HandlerUID);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool RejectRequest_leave(int requestid, int HandlerUID, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_RejectRequest_leave();
            if (check > 0)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.RejectRequest_leave(requestid, HandlerUID);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool WithDrawRequest_leave(int requestid , int HandlerUID, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_WithDrawRequest_leave();
            if (check > 0)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.WithDrawRequest_leave(requestid, HandlerUID);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool CancelRequest_leave(int requestid, int HandlerUID, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_WithDrawRequest_leave();
            if (check > 0)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.CancelRequest_leave(requestid, HandlerUID);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool ApprovalCancelRequest_leave(int requestid, int HandlerUID, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_ApprovalCancelRequeste();
            if (check > 0)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.ApproveCancelRequest_leave(requestid, HandlerUID);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }


        public static bool RejectCancelRequest_leave(int requestid, int HandlerUID, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_RejectCancelRequest();
            if (check > 0)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.RejectCancelRequest_leave(requestid, HandlerUID);
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

        private static int Check_ApproveRequest_leave()
        {
            return 1;
        }
        private static int Check_RejectRequest_leave()
        {
            return 1;
        }
        private static int Check_WithDrawRequest_leave()
        {
            return 1;
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

        public static string GetTypeDesc(int? type, string code,bool discardLeave)
        {
            string codeSimpleDesc = BLL.Leave.GetAllLeaveSimpleDesc()[code];

            if (string.IsNullOrEmpty(codeSimpleDesc))
            {
                codeSimpleDesc = code;
            }

            string leave = discardLeave == true ? "" : " leave";


            if (type != null && type == 0)
            {
                codeSimpleDesc = "Apply " + codeSimpleDesc + leave;
            }
            else if (type != null && type == 10)
            {
                codeSimpleDesc = "Cancel " + codeSimpleDesc + leave;
            }
            return codeSimpleDesc;
        }
        #endregion
    }
}