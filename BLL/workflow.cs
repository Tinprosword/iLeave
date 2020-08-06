using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class workflow
    {
        public static string LEAVE_DESC = "Leave Request";

        public static int InsertWorkflow(Ws.WebReference_leave.StaffLeaveRequest[] details, int uid, int requestLeaveID, int employMentID)
        {
            int result = 0;
            result = Ws.MyWebService.GlobalWebServices.ws_workflow.CreateNewRequest(null, Ws.WebReference_workflow.WorkflowTypeID.LEAVE_APPLICATION, details, uid, LEAVE_DESC, "", "", "", requestLeaveID, employMentID);
            return result;
        }
    }
}