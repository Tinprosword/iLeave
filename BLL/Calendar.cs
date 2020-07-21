﻿using System;
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
                data.Add(new MODEL.Apply.LeaveData(uid, "05-01周一", 1, 2, 0, BLL.GlobalVariate.LeaveSatus[0], System.DateTime.Now));
                data.Add(new MODEL.Apply.LeaveData(uid, "05-01周一", 1, 2, 0, BLL.GlobalVariate.LeaveSatus[0], System.DateTime.Now));
            }
            return data;
        }
    }
}