using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.WebReference_leave;

namespace BLL
{
    public class Leave
    {

        public static void ApproveRequest(DAL.WebReference_leave.MyWorkflowTask WorkflowTaskObject, DAL.WebReference_leave.WorkflowTypeID TaskType, object p_ApprovalRequest, int UserID, string Description, string FormulatedURL, string baseURL)
        {
            BLL.LoginManager.CheckWsLogin();
            DAL.Leave.ApproveRequest(WorkflowTaskObject, TaskType, p_ApprovalRequest, UserID, Description, FormulatedURL, baseURL);
        }


        public static void CancelRequest(DAL.WebReference_leave.MyWorkflowTask WorkflowTaskObject, DAL.WebReference_leave.WorkflowTypeID TaskType, object p_ApprovalRequest, int UserID, string Description, string FormulatedURL, string baseURL)
        {
            BLL.LoginManager.CheckWsLogin();
            DAL.Leave.CancelRequest(WorkflowTaskObject, TaskType, p_ApprovalRequest, UserID, Description, FormulatedURL, baseURL);
        }


        public static void RejectRequest(DAL.WebReference_leave.MyWorkflowTask WorkflowTaskObject, DAL.WebReference_leave.WorkflowTypeID TaskType, object p_ApprovalRequest, int UserID, string Description, string FormulatedURL, string baseURL)
        {
            BLL.LoginManager.CheckWsLogin();
            DAL.Leave.RejectRequest(WorkflowTaskObject, TaskType, p_ApprovalRequest, UserID, Description, FormulatedURL, baseURL);
        }



    }
}