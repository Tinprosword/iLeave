using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class CLOT
    {
        [Serializable]
        public class ViewState_page:IPage_Attachment
        {
            public List<CLOTItem> items;
            private List<MODEL.App_AttachmentInfo> attachments;
            public string ddl_typeSelected;
            public string inputdate;
            public string ddlfromh;
            public string ddlfromto;
            public string ddltoh;
            public string ddltom;
            public string numberofhour;
            public string remark;


            public ViewState_page()
            {
                items = new List<CLOTItem>();
                attachments = new List<App_AttachmentInfo>();
            }

            public List<App_AttachmentInfo> GetAttachment()
            {
                return attachments;
            }

            public void SetAttachment(List<App_AttachmentInfo> data)
            {
                attachments = data;
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
            public string numberofHours;

            public string GetTimeRangeDesc()
            {
                return GetTimeRangeDesc(fromhour, tohour, frommin, tominute);
            }

            public static string GetTimeRangeDesc(int fh,int th,int fm,int tm)
            {
                //2:20-22:30
                string format_string = "{0}:{1}-{2}:{3}";
                return string.Format(format_string, fh.ToString("00"), fm.ToString("00"), th.ToString("00"), tm.ToString("00"));
            }

            public DateTime GetFrom()
            {
                return new DateTime(date.Year, date.Month, date.Day, fromhour,frommin,00);
            }

            public DateTime GetTo()
            {
                return new DateTime(date.Year, date.Month, date.Day, tohour, tominute,00);
            }

            public float GetHoursFromTextBox()
            {
                float result = 0;
                bool tempCheck = checkHoursValid();
                if (tempCheck)
                {
                    result = float.Parse(numberofHours);
                }
                return result;
            }

            public bool checkHoursValid()
            {
                bool result = false;
                if (!string.IsNullOrEmpty(numberofHours))
                {
                    bool tempHoursCheck = LSLibrary.ValidateUtil.IsDecimal(numberofHours);
                    bool tempHoursCheck2 = LSLibrary.ValidateUtil.IsNumber(numberofHours);
                    if (tempHoursCheck == true || tempHoursCheck2 == true)
                    {
                        result = true;
                    }
                }

                return result;
            }
        }
    }
}