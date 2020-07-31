using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.WebReference_leave;
using System.Web;

namespace BLL
{
    public class Leave
    {
        public static string reducePath = "reduce";

        #region insert application
        private static int CheckBeforeApply()
        {
            return 0;
        }

        public static List<string> UploadAttendance(HttpRequest httpRequest, string fpath, List<string> fileExtendsType, string NameAppendStr, out string errmsg, int filesizeM = 10)
        {
            List<string> res = LSLibrary.UploadFile.SaveFiles(httpRequest, fpath, fileExtendsType, System.DateTime.Now.ToString("yyyyMMdd"), out errmsg, filesizeM);
            for (int i = 0; i < res.Count; i++)
            {
                if (common.IsImagge(res[i]))
                {
                    LSLibrary.ImageThumbnail.ReducedImage(130, 130, fpath + "\\" + res[i], fpath + "\\" + reducePath + "\\" + res[i]);
                }
            }
            return res;
        }

        //0 ok. -1 check error -2.insert error
        public static int InsertLeave(List<MODEL.Apply.apply_LeaveData> originDetail, int userid, int employmentid, int? staffid, string remarks, out string errorMsg)
        {
            BLL.User_wsref.CheckWsLogin();

            errorMsg = "";
            int result = -1;
            int checkResult = CheckBeforeApply();
            if (checkResult >= 0)
            {
                DAL.WebReference_leave.StaffLeaveRequest[] details;
                DAL.WebReference_leave.ErrorMessageInfo messageInfo;
                int insertResult = DAL.Leave.InsertLeave(originDetail, userid, employmentid, staffid, remarks, out details, out messageInfo);
                if (insertResult >= 0)
                {
                    int processID = messageInfo.ProcessID;
                    int employID = employmentid;
                    int workFlowResult = DAL.Leave.InsertWorkflow(details, userid, messageInfo.ProcessID, employID);
                    int attenchMentResult = DAL.Leave.InsertAttanchMent();
                    result = 0;
                }
                else
                {
                    errorMsg = "";
                    result = -2;
                }
            }
            else
            {
                errorMsg = "";
                result = -1;
            }
            return result;
        }


        #endregion

        #region search application

        public static List<DAL.WebReference_leave.LeaveRequestMaster> GetLeaveMaster(int uid)
        {
            return new List<DAL.WebReference_leave.LeaveRequestMaster>();
        }

        public static List<DAL.WebReference_leave.LeaveRequestMaster> GetLeaveMaster(int uid,int status, DateTime? from)
        {
            return new List<DAL.WebReference_leave.LeaveRequestMaster>();
        }

        public static List<DAL.WebReference_leave.LeaveRequestMaster> GetLeaveMaster_IncludeMyWorkFlow(int uid,DateTime? datefrom)
        {
            return new List<LeaveRequestMaster>();//todo
        }



        public static List<MODEL.Apply.apply_LeaveData> getLeaveDetails(int requestid, int uid)
        {
            string username = "";//todo get name

            DateTime leaveFrom = System.DateTime.Now;
            List<MODEL.Apply.apply_LeaveData> data = new List<MODEL.Apply.apply_LeaveData>();
            DAL.WebReference_leave.StaffLeaveRequest[] leaves = null;// todo BLL.Leave.getLeaveAppliationsByRequestID(requestid);
            for (int i = 0; i < leaves.Count(); i++)
            {
                string strDate = leaves[i].LeaveDate.ToString("MM-dd");
                int section = leaves[i].Section;
                int typeid = leaves[i].LeaveID;
                int status = leaves[i].Status;
                string statusName = ((DAL.WebReference_leave.ApprovalRequestStatus)leaves[i].Status).ToString();
                DateTime date = leaves[i].LeaveDate;
                string typecode = leaves[i].LeaveTypeName;
                string typeDesc = leaves[i].LeaveTypeName;//todo 

                data.Add(new MODEL.Apply.apply_LeaveData(typeid, typecode, typeDesc, section,  date));
            }
            return data;
        }

        public static List<MODEL.Apply.app_uploadpic> getAttendance(string uid, int applicationID)
        {
            List<MODEL.Apply.app_uploadpic> data = new List<MODEL.Apply.app_uploadpic>();
            for (int i = 0; i < 1; i++)
            {
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.app_uploadpic("~/res/images/setting.gif", "~/res/images/setting.gif"));
            }
            return data;
        }
        #endregion


        #region process application
        public static void ApproveRequest(DAL.WebReference_leave.MyWorkflowTask WorkflowTaskObject, DAL.WebReference_leave.WorkflowTypeID TaskType, object p_ApprovalRequest, int UserID, string Description, string FormulatedURL, string baseURL)
        {
            BLL.User_wsref.CheckWsLogin();
            DAL.Leave.ApproveRequest(WorkflowTaskObject, TaskType, p_ApprovalRequest, UserID, Description, FormulatedURL, baseURL);
        }


        public static void CancelRequest(DAL.WebReference_leave.MyWorkflowTask WorkflowTaskObject, DAL.WebReference_leave.WorkflowTypeID TaskType, object p_ApprovalRequest, int UserID, string Description, string FormulatedURL, string baseURL)
        {
            BLL.User_wsref.CheckWsLogin();
            DAL.Leave.CancelRequest(WorkflowTaskObject, TaskType, p_ApprovalRequest, UserID, Description, FormulatedURL, baseURL);
        }


        public static void RejectRequest(DAL.WebReference_leave.MyWorkflowTask WorkflowTaskObject, DAL.WebReference_leave.WorkflowTypeID TaskType, object p_ApprovalRequest, int UserID, string Description, string FormulatedURL, string baseURL)
        {
            BLL.User_wsref.CheckWsLogin();
            DAL.Leave.RejectRequest(WorkflowTaskObject, TaskType, p_ApprovalRequest, UserID, Description, FormulatedURL, baseURL);
        }
        #endregion


        public static List<MODEL.Apply.apply_LeaveData> getListSource(string uid, DateTime dt)
        {
            List<MODEL.Apply.apply_LeaveData> data = new List<MODEL.Apply.apply_LeaveData>();
            int modday = dt.Day % 5;
            for (int i = 0; i < modday; i++)
            {
                //data.Add(new MODEL.Apply.apply_LeaveData(uid, "05-01周一", 1, 2, (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE.ToString(), System.DateTime.Now, "Al", "Al"));
                //data.Add(new MODEL.Apply.apply_LeaveData(uid, "05-01周一", 1, 2, (int)BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE, BLL.GlobalVariate.ApprovalRequestStatus.WAIT_FOR_APPROVE.ToString(), System.DateTime.Now, "Al", "Al"));
            }
            return data;
        }

        #region unity
        public static List<LSLibrary.WebAPP.ValueText<int>> ConvertLeaveInfo2DropDownList(LeaveInfo[] source)
        {
            List<LSLibrary.WebAPP.ValueText<int>> result = new List<LSLibrary.WebAPP.ValueText<int>>();
            result.Add(new LSLibrary.WebAPP.ValueText<int>(0, "Please select"));
            for(int i=0;i<source.Count();i++)
            {
                LSLibrary.WebAPP.ValueText<int> item = new LSLibrary.WebAPP.ValueText<int>(source[i].ID, source[i].Code+" -"+source[i].Description);
                result.Add(item);
            }
            result.Add(new LSLibrary.WebAPP.ValueText<int>(0, "Please select"));
            return result;
        }

        public static DAL.WebReference_leave.LeaveInfo[] GetLeaveInfoByStaffID(int staffid)
        {
            BLL.User_wsref.CheckWsLogin();
            return DAL.Leave.GetLeaveInfoByStaffID(staffid);
        }

        #endregion

    }
}