﻿using System;
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
        public class ViewState_page
        {
            public string LeaveTypeSelectValue;
            public string ddlsectionSelectvalue;
            public string remarks;

            public List<App_AttachmentInfo> uploadpic;
            public List<apply_LeaveData> LeaveList;
            public List<LSLibrary.WebAPP.ValueText<int>> leavetype;

            public ViewState_page()
            {
                LeaveList = new List<MODEL.Apply.apply_LeaveData>();
                uploadpic = new List<MODEL.Apply.App_AttachmentInfo>();
                leavetype = new List<LSLibrary.WebAPP.ValueText<int>>();
            }

            public static double getDayfromSections(int sectionsValue)
            {
                if (sectionsValue == 0 || sectionsValue == 3)
                {
                    return 1;
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
                        result += getDayfromSections(LeaveList[i].sectionid);
                    }
                }
                return result;
            }
        }


        [Serializable]
        public class apply_LeaveData
        {
            public int sectionid;
            public int leavetypeid;
            public string leavetypeCode;
            public string leavetypeDescription;
            public DateTime LeaveDate;

            public apply_LeaveData(int leavetypeid, string leavetypeCode, string leavetypeDescription, int sectionid,  DateTime leaveDate)
            {
                this.sectionid = sectionid;
                this.leavetypeid = leavetypeid;
                this.leavetypeCode = leavetypeCode;
                this.leavetypeDescription = leavetypeDescription;
                LeaveDate = leaveDate;
            }
        }

        [Serializable]
        public class App_AttachmentInfo
        {
            public string originAttendance_RelatePath;
            public string reduceAttendance_Image_RelatePath;
            public string tempID;
            public string originAttendance_HRDBPath;

            public App_AttachmentInfo(string path, string _reduceImage,string _bigImageAbsolutePath)
            {
                this.originAttendance_RelatePath = path;
                this.tempID = System.DateTime.Now.ToString("yyyyMMddhhmmss");
                this.reduceAttendance_Image_RelatePath = _reduceImage;
                this.originAttendance_HRDBPath = _bigImageAbsolutePath;
            }

            public string GetFileName(int maxLength=0)
            {
                return LSLibrary.FileUtil.SubFileName(originAttendance_RelatePath, maxLength, "");
            }

            public string Get_originAttendance_RealRelatePath()
            {
                string result= originAttendance_RelatePath.Substring(1);
                result = ".." + result;
                return result;
            }



        }
    }
}