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
                data.Add(new MODEL.Apply.LeaveData(uid,"05-01周一", "AL", "FULL DAY", 0,0));
                data.Add(new MODEL.Apply.LeaveData(uid, "05-02周二", "AL", "FULL DAY", 0, 0));
                data.Add(new MODEL.Apply.LeaveData(uid, "05-03周三", "AL", "FULL DAY", 0, 0));
                data.Add(new MODEL.Apply.LeaveData(uid, "05-04周四", "AL", "FULL DAY", 0, 0));
                data.Add(new MODEL.Apply.LeaveData(uid, "05-05周五", "AL", "FULL DAY", 0, 0));
            }
            return data;
        }


        public static List<MODEL.Apply.UploadPic> getAttendance(string uid, int applicationID)
        {
            List<MODEL.Apply.UploadPic> data = new List<MODEL.Apply.UploadPic>();
            for (int i = 0; i < 1; i++)
            {
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif"));
                data.Add(new MODEL.Apply.UploadPic("~/res/images/setting.gif"));
            }
            return data;
        }
    }
}