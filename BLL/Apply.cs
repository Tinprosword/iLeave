using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class Apply
    {
        public static string SESSION_DATELIST = "DATE";

        

        #region [innerclass]

       

        public class UploadPic
        {
            public string path;

            public UploadPic(string _path)
            {
                this.path = _path;
            }
        }

        public class LeaveData
        {
            public string date;
            public string type;
            public string section;
            public int typeid;

            public LeaveData(string date, string type, string section, int typeid)
            {
                this.date = date;
                this.type = type;
                this.section = section;
                this.typeid = typeid;
            }
        }
        #endregion
    }
}