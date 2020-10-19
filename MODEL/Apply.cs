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
        public class ViewState_page
        {
            public string LeaveTypeSelectValue;
            public string ddlsectionSelectvalue;
            public string remarks;

            public List<app_uploadpic> uploadpic;
            public List<apply_LeaveData> LeaveList;
            public List<LSLibrary.WebAPP.ValueText<int>> leavetype;

            public ViewState_page()
            {
                LeaveList = new List<MODEL.Apply.apply_LeaveData>();
                uploadpic = new List<MODEL.Apply.app_uploadpic>();
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
        public class app_uploadpic
        {
            public string bigImageRelatepath;
            public string reduceImageRelatePath;
            public string tempID;
            public string bigImageHrTempAbsolutePath;

            public app_uploadpic(string path, string _reduceImage,string _bigImageAbsolutePath)
            {
                this.bigImageRelatepath = path;
                this.tempID = System.DateTime.Now.ToString("yyyyMMddhhmmss");
                this.reduceImageRelatePath = _reduceImage;
                this.bigImageHrTempAbsolutePath = _bigImageAbsolutePath;
            }

            public string GetFileName(int maxLength=0)
            {
                string result = "";
                if (!string.IsNullOrEmpty(bigImageRelatepath))
                {
                    result = LSLibrary.FileUtil.GetFileNameNoExtension(bigImageRelatepath);
                    if (maxLength != 0 && result.Length>maxLength)
                    {
                        result = result.Substring(0, maxLength);
                    }
                    result += LSLibrary.FileUtil.GetExtension(bigImageRelatepath);
                }
                return result;
            }
        }
    }
}