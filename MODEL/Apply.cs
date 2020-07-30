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

            public List<UploadPic> uploadpic;
            public List<LeaveData> LeaveList;
            public List<LSLibrary.WebAPP.ValueText<int>> leavetype;
        }


        [Serializable]
        public class LeaveData
        {
            public string name;
            public string date;
            public int sectionid;
            public int leavetypeid;
            public string leavetypeCode;
            public string leavetypeDescription;
            public int status;
            public DateTime LeaveDate;

            public LeaveData(string name, string date, int section, int typeid, int status, string statusstr,DateTime _dateTime, string _typecode,string typedesc)
            {
                this.name = name;
                this.date = date;
                this.leavetypeid = typeid;
                this.sectionid = section;
                this.leavetypeid = typeid;
                this.status = status;
                this.LeaveDate = _dateTime;
                leavetypeCode = _typecode;
                leavetypeDescription = typedesc;
            }
        }

        [Serializable]
        public class UploadPic
        {
            public string path;
            public string tempID;
            public string reduceImage;

            public UploadPic(string path, string _reduceImage)
            {
                this.path = path;
                this.tempID = System.DateTime.Now.ToString("yyyyMMddhhmmss");
                this.reduceImage = _reduceImage;
            }
        }


    }
}