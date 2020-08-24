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
        public static List<WebServiceLayer.WebReference_leave.t_WorkflowTask> Gett_WorkflowTaskByInfoID(int infoID)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.Gett_WorkflowTaskByInfoID(infoID).ToList();
        }


        public static List<Worktask_leave> GetWorktask_leave(GlobalVariate.LeaveBigRangeStatus status, int approvalUid ,DateTime? from=null)
        {
            List<Worktask_leave> result = new List<Worktask_leave>();
            if (status == GlobalVariate.LeaveBigRangeStatus.approvaled)
            {
                result = GetApproaled(approvalUid);
            }
            else if (status == GlobalVariate.LeaveBigRangeStatus.waitapproval)
            {
                result = GetWait(approvalUid);
            }
            else if (status == GlobalVariate.LeaveBigRangeStatus.withdraw)
            {
                result = GetReject(approvalUid);
            }

            if (from != null)
            {
                result = result.Where(x => x.leaveRequestMaster.leavefrom >= from).ToList();
            }

            return result;
        }



        private static List<Worktask_leave> GetApproaled(int uid, DateTime? from = null)
        {
            var worktasks= WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetMyMaxStepWorkflowByUID_Approaled(uid);
            var result = composeWorktask_leaveByWorkflow(worktasks);
            return result;
        }

        private static List<Worktask_leave> GetWait(int uid, DateTime? from = null)
        {
            var worktasks = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetMyMaxStepWorkflowByUID_WaitForApproval(uid);
            var result = composeWorktask_leaveByWorkflow(worktasks);
            return result;
        }

        private static List<Worktask_leave> GetReject(int uid, DateTime? from = null)
        {
            var worktasks = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetMyMaxStepWorkflowByUID_Reject(uid);
            var result = composeWorktask_leaveByWorkflow(worktasks);
            return result;
        }

        private static List<Worktask_leave> composeWorktask_leaveByWorkflow(WebServiceLayer.WebReference_leave.WorkInfo_Worktask[] worktasks)
        {
            List<Worktask_leave> result = new List<Worktask_leave>();

            for (int i = 0; i < worktasks.Count(); i++)
            {
                WebServiceLayer.WebReference_leave.LeaveRequestMaster temp = WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetLeaveMasterByReuestID(worktasks[i].RequestID);
                Worktask_leave item = new Worktask_leave();
                item.worktask = worktasks[i];
                item.leaveRequestMaster = temp;
                result.Add(item);
            }
            return result;
        }

        #endregion

        public class Worktask_leave
        {
            public WebServiceLayer.WebReference_leave.WorkInfo_Worktask worktask;
            public WebServiceLayer.WebReference_leave.LeaveRequestMaster leaveRequestMaster;
        }
    }
}