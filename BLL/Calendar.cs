using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Calendar
    {
        public static List<MODEL.Apply.LeaveData> getListSource(string uid,DateTime dt)
        {
            List<MODEL.Apply.LeaveData> data = new List<MODEL.Apply.LeaveData>();
            int modday = dt.Day % 5;
            for (int i = 0; i < modday; i++)
            {
                data.Add(new MODEL.Apply.LeaveData(uid, "05-01周一", "AL", "FULL DAY", 0, 0, BLL.GlobalVariate.LeaveType[0]));
                data.Add(new MODEL.Apply.LeaveData(uid, "05-02周二", "AL", "FULL DAY", 0, 1, BLL.GlobalVariate.LeaveType[1]));
            }
            return data;
        }
    }
}