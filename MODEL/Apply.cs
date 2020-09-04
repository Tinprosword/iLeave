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
            public string applylabel;
            public string balancelabel;
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
            public string bigImagepath;
            public string reduceImagePath;
            public string tempID;
            public string bigImageAbsolutePath;

            public app_uploadpic(string path, string _reduceImage,string _bigImageAbsolutePath)
            {
                this.bigImagepath = path;
                this.tempID = System.DateTime.Now.ToString("yyyyMMddhhmmss");
                this.reduceImagePath = _reduceImage;
                this.bigImageAbsolutePath = _bigImageAbsolutePath;
            }
        }

    }
}