using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class Apply
    {
        public class ApplyPage
        {
            public string LeaveTypeSelectValue;
            public string applylabel;
            public string balancelabel;
            public string datefrom;
            public string dateto;
            public string ddlsectionSelectvalue;
            public string remarks;

            public List<UploadPic> uploadpic;
            public List<LeaveData> LeaveList;
        }


        [Serializable]
        public class LeaveData
        {
            public string name;
            public string date;
            public string type;
            public string section;
            public int typeid;
            public int status;

            public LeaveData(string name, string date, string type, string section, int typeid, int status)
            {
                this.name = name;
                this.date = date;
                this.type = type;
                this.section = section;
                this.typeid = typeid;
                this.status = status;
            }
        }

        [Serializable]
        public class UploadPic
        {
            public string path;
            public string tempID;

            public UploadPic(string path)
            {
                this.path = path;
                this.tempID = System.DateTime.Now.ToString("yyyyMMddhhmmss");
            }
        }
    }
}