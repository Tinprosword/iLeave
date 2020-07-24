using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Application
    {
        public static List<MODEL.Apply.LeaveBatch> getListBatch(LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> user)
        {
            return new List<MODEL.Apply.LeaveBatch>();
        }



        public static List<MODEL.Apply.LeaveData> getListSource(LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> user)
        {
            List<MODEL.Apply.LeaveData> data = new List<MODEL.Apply.LeaveData>();

            int[] staffids = BLL.Staff.GetStaffids(user.userInfo.id);

            DAL.WebReference_leave.StaffLeaveRequest[] leaves= BLL.Leave.getLeaveAppliationsByStaffid(staffids);

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

        public static List<MODEL.Apply.LeaveData> getListSourceByStatus(LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> user, int applicationID, DAL.WebReference_leave.ApprovalRequestStatus status)
        {
            List<MODEL.Apply.LeaveData> allLeave = getListSource(user);
            List<MODEL.Apply.LeaveData> results= allLeave.Where(x => x.status == (int)status).ToList();
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