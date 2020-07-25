using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Application
    {
        //ui 不要去 访问dal,所以在bll中,采用这种方式来映射webservice的枚举类型,也不能直接复制类型过来,违背 唯一依赖原则.这样,如果WS修改后,这里最起码会编译错误.
        public static int status_approve = (int)DAL.WebReference_leave.ApprovalRequestStatus.APPROVE;
        public static int status_cancel = (int)DAL.WebReference_leave.ApprovalRequestStatus.CANCEL;
        public static int status_CONFIRM_CANCEL = (int)DAL.WebReference_leave.ApprovalRequestStatus.CONFIRM_CANCEL;
        public static int status_NEW = (int)DAL.WebReference_leave.ApprovalRequestStatus.NEW;
        public static int status_REJECT = (int)DAL.WebReference_leave.ApprovalRequestStatus.REJECT;
        public static int status_SENDEMAIL = (int)DAL.WebReference_leave.ApprovalRequestStatus.SENDEMAIL;
        public static int status_WAIT_FOR_APPROVE = (int)DAL.WebReference_leave.ApprovalRequestStatus.WAIT_FOR_APPROVE;
        public static int status_WAIT_FOR_CANCEL = (int)DAL.WebReference_leave.ApprovalRequestStatus.WAIT_FOR_CANCEL;



        public static List<MODEL.Apply.LeaveBatch> getLeaveBatch(int uid)
        {
            DateTime leaveFrom = System.DateTime.Parse("1900-01-01");
            DateTime leaveTo = System.DateTime.Parse("3000-01-01");
            return getLeaveBatch(uid, leaveFrom, leaveTo);
        }

        public static List<MODEL.Apply.LeaveBatch> getLeaveBatch(int uid, DateTime from)
        {
            DateTime leaveTo = System.DateTime.Parse("3000-01-01");
            return getLeaveBatch(uid, from, leaveTo);
        }

        public static List<MODEL.Apply.LeaveBatch> getLeaveBatch(int uid, DateTime from ,DateTime to)
        {
            List<MODEL.Apply.LeaveData> data = new List<MODEL.Apply.LeaveData>();
            int[] staffids = BLL.Staff.GetStaffids(uid);
            DAL.WebReference_leave.StaffLeaveDetailInBatch[] leaves = BLL.Leave.GetOnlineStaffLeaveRecordByStaffIDBatchMode(staffids, from, to);
            return ConverWSToLocalModal(leaves);
        }

        private static List<MODEL.Apply.LeaveBatch> ConverWSToLocalModal(DAL.WebReference_leave.StaffLeaveDetailInBatch[] DataSource)
        {
            List<MODEL.Apply.LeaveBatch> result = new List<MODEL.Apply.LeaveBatch>();
            if (DataSource != null && DataSource.Count() > 0)
            {
                for (int i = 0; i < DataSource.Count(); i++)
                {
                    int RequestID = DataSource[i].RequestID;
                    string name = DataSource[i].EngName;
                    string date = DataSource[i].StartDate.ToString("yyy-MM-dd") + "_" + DataSource[i].EndDate.ToString("yyy-MM-dd") + "(" + DataSource[i].NoOfDays + "D)";
                    int sectionid = 0;
                    int typeid = DataSource[i].TypeID;
                    string typeCode = DataSource[i].Type;
                    string typeDescription = DataSource[i].Type;//todo
                    int status = DataSource[i].Status;
                    string statusStr = ((DAL.WebReference_leave.ApprovalRequestStatus)(DataSource[i].Status)).ToString();
                    MODEL.Apply.LeaveBatch TempItem = new MODEL.Apply.LeaveBatch(name, date, sectionid, typeid, status, statusStr, typeCode, typeDescription,RequestID);
                    result.Add(TempItem);
                }
            }
            return result;
        }


        public static List<MODEL.Apply.LeaveData> getLeaveDetails(LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> user,int leaveid)
        {
            DateTime leaveFrom = System.DateTime.Now;
            List<MODEL.Apply.LeaveData> data = new List<MODEL.Apply.LeaveData>();
            int[] staffids = BLL.Staff.GetStaffids(user.userInfo.id);
            DAL.WebReference_leave.StaffLeaveRequest[] leaves= BLL.Leave.getLeaveAppliationsByStaffid(staffids);
            DAL.WebReference_leave.StaffLeaveDetailInBatch[] leaves2 = BLL.Leave.GetOnlineStaffLeaveRecordByStaffIDBatchMode(staffids, leaveFrom, leaveFrom.AddDays(365));
            for (int i = 0; i < leaves.Count(); i++)
            {
                string strDate = leaves[i].LeaveDate.ToString("MM-dd");
                int section = leaves[i].Section;
                int typeid = leaves[i].LeaveID;
                int status = leaves[i].Status;
                string statusName = ((DAL.WebReference_leave.ApprovalRequestStatus)leaves[i].Status).ToString();
                DateTime date = leaves[i].LeaveDate;
                string typecode = leaves[i].LeaveTypeName;
                string typeDesc= leaves[i].LeaveTypeName;//todo 

                data.Add(new MODEL.Apply.LeaveData(user.userInfo.loginName,strDate, section, typeid ,status, statusName, date, typecode, typeDesc));
            }
            return data;
        }

        public static List<MODEL.Apply.LeaveBatch> getLeaveBatchByStatus(int uid, int applicationID, DAL.WebReference_leave.ApprovalRequestStatus status)
        {
            List<MODEL.Apply.LeaveBatch> allLeave = getLeaveBatch(uid);
            List<MODEL.Apply.LeaveBatch> results= allLeave.Where(x => x.status == (int)status).ToList();
            return allLeave;
        }


        public static List<MODEL.Apply.UploadPic> getAttendance(string uid, int applicationID)
        {
            List<MODEL.Apply.UploadPic> data = new List<MODEL.Apply.UploadPic>();
            for (int i = 0; i < 1; i++)
            {
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif", "~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif", "~/res/images/setting.gif"));
            }
            return data;
        }


      


    }
}