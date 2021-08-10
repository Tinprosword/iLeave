﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class CLOT
    {
        [Serializable]
        public class ViewState_page
        {
            public List<CLOTItem> items;

            public ViewState_page()
            {
                items = new List<CLOTItem>();
            }
        }

        public enum enum_clotType
        {
            [System.ComponentModel.Description("Overtime")]
            OT =0,
            
            [System.ComponentModel.Description("Compenstation Leave")]
            CL =1
        }



        [Serializable]
        public class CLOTItem
        {
            public enum_clotType type;
            public System.DateTime date;
            public int fromhour;
            public int frommin;
            public int tohour;
            public int tominute;
            public string remark;


            public string GetTimeRangeDesc()
            {
                //2:20-22:30
                string format_string = "{0}:{1}-{2}:{3}";
                return string.Format(format_string, fromhour.ToString("00"), frommin.ToString("00"), tohour.ToString("00"), tominute.ToString("00"));
            }

            public float GetHours()
            {
                int h = tohour - fromhour;
                int m = tominute - frommin+1;
                int totalmin = h * 60 + m;
                return (float)(Math.Round((double)((double)totalmin / 60),2));
            }
        }
    }
}