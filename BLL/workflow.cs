using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class workflow
    {
        //todo   waitcancel -> ok.  master need update status.
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
        public static bool ApproveRequest_leave(int requestid,int workflowtaskid,int HandlerUID,out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_ApproveRequest_leave();
            if (check > 0)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.ApproveRequest_leave(workflowtaskid, requestid, HandlerUID);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool RejectRequest_leave(int requestid, int workflowtaskid, int HandlerUID, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_RejectRequest_leave();
            if (check > 0)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.RejectRequest_leave(workflowtaskid, requestid, HandlerUID);
                result = true;
            }
            else
            {
                result = false;
                errorMsg = "";
            }
            return result;
        }

        public static bool WithDrawRequest_leave(int requestid, int workflowtaskid, int HandlerUID, out string errorMsg)
        {
            bool result = false;
            errorMsg = "";
            int check = Check_WithDrawRequest_leave();
            if (check > 0)
            {
                WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.WithDrawRequest_leave(workflowtaskid, requestid, HandlerUID);
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
        public static WebServiceLayer.WebReference_leave.t_WorkflowInfo Gett_WorkflowInfoByRequestID(int requestid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.Gett_WorkflowInfoByRequestID(requestid);
        }

        public static List<WebServiceLayer.WebReference_leave.t_WorkflowTask> Gett_WorkflowTaskByInfoID(int infoID)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.Gett_WorkflowTaskByInfoID(infoID).ToList();
        }
        #endregion
    }
}