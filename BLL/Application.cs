using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Application
    {
        public static List<MODEL.Apply.LeaveData> getListSource(string uid,int applicationID)
        {
            List<MODEL.Apply.LeaveData> data = new List<MODEL.Apply.LeaveData>();
            for (int i = 0; i < 1; i++)
            {
                data.Add(new MODEL.Apply.LeaveData(uid,"05-01周一", 1, 2 ,0, BLL.GlobalVariate.LeaveSatus[0],System.DateTime.Now,"Al"));
                data.Add(new MODEL.Apply.LeaveData(uid, "05-01周一", 1, 2, 0, BLL.GlobalVariate.LeaveSatus[0], System.DateTime.Now, "Al"));
                data.Add(new MODEL.Apply.LeaveData(uid, "05-01周一", 1, 2, 0, BLL.GlobalVariate.LeaveSatus[0], System.DateTime.Now, "Al"));
                data.Add(new MODEL.Apply.LeaveData(uid, "05-01周一", 1, 2, 0, BLL.GlobalVariate.LeaveSatus[0], System.DateTime.Now, "Al"));
                data.Add(new MODEL.Apply.LeaveData(uid, "05-01周一", 1, 2, 0, BLL.GlobalVariate.LeaveSatus[0], System.DateTime.Now, "Al"));
            }
            return data;
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