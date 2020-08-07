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

        public static int InsertWorkflow(object details, int uid, int requestLeaveID, int employMentID)
        {
            int result = 0;
            result = DAL.MyWebService.GlobalWebServices.ws_workflow.CreateNewRequest(null, DAL.WebReference_workflow.WorkflowTypeID.LEAVE_APPLICATION, (object)details, uid, LEAVE_DESC, "", "", "", requestLeaveID, employMentID);
            return result;
        }


    }
}