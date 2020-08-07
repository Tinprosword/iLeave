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
        #region insert application
        private static int CheckBeforeApply()
        {
            return 0;
        }

        private static int InsertAttanchMent()
        {
            return 0;
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
                int insertResult = InsertLeaveData(originDetail, userid, employmentid, staffid, remarks, out details, out messageInfo);
                if (insertResult >= 0)
                {
                    int processID = messageInfo.ProcessID;
                    int employID = employmentid;
                    int workFlowResult = BLL.workflow.InsertWorkflow(details, userid, messageInfo.ProcessID, employID);
                    int attenchMentResult = InsertAttanchMent();
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


        private static int InsertLeaveData(List<MODEL.Apply.apply_LeaveData> originDetail, int userid, int employmentid, int? staffid, string remarks, out DAL.WebReference_leave.StaffLeaveRequest[] details, out DAL.WebReference_leave.ErrorMessageInfo messageInfo)
        {
            int result = -1;//默认为一般错误
            DAL.MyWebService.WebServicesHelper webServicesHelper = DAL.MyWebService.WebServicesHelper.GetInstance();
            List<DAL.WebReference_leave.StaffLeaveRequest> detail = GenerateLeaveRequest(originDetail, userid, employmentid);

            messageInfo = new DAL.WebReference_leave.ErrorMessageInfo();
            try
            {
                messageInfo = webServicesHelper.ws_leave.InsertOnlineLeaveApplicationRequest(detail.ToArray(), DAL.WebReference_leave.ApprovalRequestStatus.WAIT_FOR_APPROVE, userid, staffid);
                result = 0;
            }
            catch
            {
                result = -1;
            }
            details = detail.ToArray();
            return result;
        }


        private static List<DAL.WebReference_leave.StaffLeaveRequest> GenerateLeaveRequest(List<MODEL.Apply.apply_LeaveData> originDetail, int uid, int employmentID)
        {
            List<DAL.WebReference_leave.StaffLeaveRequest> result = new List<DAL.WebReference_leave.StaffLeaveRequest>();

            for (int i = 0; i < originDetail.Count; i++)
            {
                if (employmentID > 0)
                {
                    DAL.WebReference_leave.StaffLeaveRequest newItem = new DAL.WebReference_leave.StaffLeaveRequest();
                    newItem.CompareKey = null;
                    newItem.CreateDate = System.DateTime.Now;
                    newItem.DelegationToStaffID = null;
                    newItem.DeleteKey = originDetail[i].LeaveDate.ToString("yyyy-MM-dd") + "," + originDetail[i].leavetypeid.ToString();
                    newItem.Description = null;
                    newItem.EmploymentID = employmentID;
                    newItem.EmploymentNumber = null;
                    newItem.FirstEmploymentID = 0;
                    newItem.FirstEmploymentNumber = null;
                    newItem.HolidayCode = "";
                    newItem.ID = 0;
                    newItem.IsApproved = false;
                    newItem.LeaveCalculationTypeID = -1;
                    newItem.LeaveCalculationTypeDesc = "N/A";
                    newItem.LeaveDate = originDetail[i].LeaveDate;
                    newItem.LeaveHours = 0;
                    newItem.LeaveHoursFrom = originDetail[i].LeaveDate;
                    newItem.LeaveHoursTo = originDetail[i].LeaveDate;
                    newItem.LeaveID = originDetail[i].leavetypeid;
                    newItem.LeaveTypeName = originDetail[i].leavetypeDescription;
                    newItem.Name = null;
                    newItem.NameCH = null;
                    newItem.Remarks = "";
                    newItem.RequestID = 0;
                    newItem.Section = originDetail[i].sectionid;
                    newItem.Status = 0;
                    newItem.TotalWorkHours = GetEmployHours(employmentID);

                    if (newItem.Section == 0)
                    {
                        newItem.Unit = 1;
                        newItem.DisplayUnit = "1 D";
                        newItem.IsHalfDay = false;
                        newItem.DisplaySection = newItem.Section;
                    }
                    else if (newItem.Section == 1 || newItem.Section == 2)
                    {
                        newItem.Unit = 0.5;
                        newItem.DisplayUnit = "0.5 D";
                        newItem.IsHalfDay = true;
                        newItem.DisplaySection = newItem.Section;
                    }
                    else if (newItem.Section == 3)
                    {
                        newItem.Unit = 1;
                        newItem.DisplayUnit = "1 D";
                        newItem.IsHalfDay = false;
                        newItem.DisplaySection = newItem.Section;
                    }
                    result.Add(newItem);
                }
            }
            return result;
        }

        private static int GetEmployHours(int employid)
        {//todo it
            return 8;
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
        public static List<LSLibrary.WebAPP.ValueText<int>> ConvertLeaveInfo2DropDownList(List<DAL.WebReference_leave.t_Leave> source)
        {
            List<LSLibrary.WebAPP.ValueText<int>> result = new List<LSLibrary.WebAPP.ValueText<int>>();
            result.Add(new LSLibrary.WebAPP.ValueText<int>(0, "Please select"));
            for (int i = 0; i < source.Count(); i++)
            {
                LSLibrary.WebAPP.ValueText<int> item = new LSLibrary.WebAPP.ValueText<int>(source[i].ID, source[i].Code + " -" + source[i].Description);
                result.Add(item);
            }
            return result;
        }


        public static List<DAL.WebReference_leave.t_Leave> GetLeavesByStaffID(int sid)
        {
            BLL.User_wsref.CheckWsLogin();
            return DAL.MyWebService.GlobalWebServices.ws_leave.GetAllLeaveTypeByStaffID(sid).ToList();
        }

        #endregion
    }
}