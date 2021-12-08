using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class Apply
    {
        [Serializable]
        public class ajax_data_apply
        {
            public ViewState_page pagedata;
            public int loginid;
        }

        [Serializable]
        public class ViewState_page: IPage_Attachment
        {
            public string LeaveTypeSelectValue;
            public string ddlsectionSelectvalue;
            public bool hasHour;
            public int bydayorHour;
            public string remarks;
            public DateTime? from;
            public DateTime? to;
            public double totalHours;

            private List<App_AttachmentInfo> uploadpic;
            public List<apply_LeaveData> LeaveList;
            public List<LSLibrary.WebAPP.ValueText<int>> leavetype;

            public ViewState_page()
            {
                LeaveList = new List<MODEL.Apply.apply_LeaveData>();
                uploadpic = new List<MODEL.App_AttachmentInfo>();
                leavetype = new List<LSLibrary.WebAPP.ValueText<int>>();
            }

            public static double getDayfromSectionsByDay(int sectionsValue)
            {
                if (sectionsValue == 0)
                {
                    return 1;
                }
                else if (sectionsValue == 3)
                {
                    return 1.5;
                }
                else if (sectionsValue == 1 || sectionsValue == 2)
                {
                    return 0.5;
                }
                else
                {
                    return 0;
                }
            }


            public double getApplying()
            {
                double result = 0;

                if (LeaveList != null)
                {
                    for (int i = 0; i < LeaveList.Count(); i++)
                    {
                        result += getDayfromSectionsByDay(LeaveList[i].sectionid);
                    }
                }
                return result;
            }

            public List<App_AttachmentInfo> GetAttachment()
            {
                return uploadpic;
            }

            public void SetAttachment(List<App_AttachmentInfo> data)
            {
                uploadpic = data;
            }
        }


        [Serializable]
        public class apply_LeaveData
        {
            public int leavetypeid;
            public string leavetypeCode;
            public string leavetypeDescription;
            public DateTime LeaveDate;
            public int sectionid;
            public DateTime? LeaveHourFrom;
            public DateTime? LeaveHourTo;
            public double totalHours;
            public int byDaybyHour;

            //byday
            public apply_LeaveData(int leavetypeid, string leavetypeCode, string leavetypeDescription, int sectionid,  DateTime leaveDate)
            {
                this.sectionid = sectionid;
                this.leavetypeid = leavetypeid;
                this.leavetypeCode = leavetypeCode;
                this.leavetypeDescription = leavetypeDescription;
                LeaveDate = leaveDate;
                byDaybyHour = 0;
                LeaveHourFrom = null;
                LeaveHourTo = null;
                totalHours = 0;
            }

            //byhour
            public apply_LeaveData(int leavetypeid, string leavetypeCode, string leavetypeDescription, DateTime leaveDate,DateTime f,DateTime t,double total)
            {
                this.sectionid = 4;//hour
                this.leavetypeid = leavetypeid;
                this.leavetypeCode = leavetypeCode;
                this.leavetypeDescription = leavetypeDescription;
                LeaveDate = leaveDate;
                byDaybyHour = 1;
                LeaveHourFrom = new DateTime(leaveDate.Year, leaveDate.Month, leaveDate.Day, f.Hour, f.Minute, 0);
                LeaveHourTo = new DateTime(leaveDate.Year, leaveDate.Month, leaveDate.Day, t.Hour, t.Minute, 0);
                totalHours = total;
            }

            public double GetUnitByDay()
            {
                double result = 0;

                result = ViewState_page.getDayfromSectionsByDay(sectionid);

                return result;
            }
        }

        
    }
}