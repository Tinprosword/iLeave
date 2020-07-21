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
        }


        [Serializable]
        public class LeaveData
        {
            public string name;
            public string date;
            public int sectionid;
            public int typeid;
            public int status;
            public DateTime DateTime;

            public LeaveData(string name, string date, int section, int typeid, int status, string statusstr,DateTime _dateTime)
            {
                this.name = name;
                this.date = date;
                this.typeid = typeid;
                this.sectionid = section;
                this.typeid = typeid;
                this.status = status;
                this.DateTime = _dateTime;
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

        public class StaffLeaveDetails
        {
            public int idField;
            public int employmentIDField;
            public string employmentNumberField;
            public string engNameField;
            public string chiNameField;
            public string typeField;
            public int typeIDField;
            public System.DateTime leaveFromField;
            public System.DateTime leaveToField;
            public double unitField;
            public string remarksField;
            public string deleteKeyField;
            public int sectionField;
            public string codeField;
            public System.DateTime dateField;
            public string workingHourPatternField;
            public double workingHourHalfDayField;
            public double workingHourAMField;
            public double workingHourPMField;
            public System.DateTime createDateField;
            public int requestIDField;
            public string holidayCodeField;
            public int leaveCalculationTypeIDField;
            public string leaveCalculationTypeDescField;
            public double leaveHoursField;
            public double totalWorkHoursField;
            public double sectionsField;
            public bool isHalfDayField;
            public int displaySectionField;
            public string displayUnitField;
            public string displaySectionCombinedField;
            public string securityGroupCodeField;
        }


    }
}