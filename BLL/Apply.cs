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
        private static string SESSION_DATELIST = "DATE";

        #region session manage
        public static void InitPageSession_DateList()
        {
            LSLibrary.WebAPP.PageSessionHelper.CleanValue(SESSION_DATELIST);
            LSLibrary.WebAPP.PageSessionHelper.SetValue(new List<LeaveData>(), SESSION_DATELIST);
        }

        public static List<LeaveData> GetPageSession_DateList()
        {
            return (List<LeaveData>)LSLibrary.WebAPP.PageSessionHelper.GetValue(SESSION_DATELIST);
        }

        public static void SetPageSession_DateList(List<LeaveData> datesCache)
        {
            LSLibrary.WebAPP.PageSessionHelper.SetValue(datesCache, SESSION_DATELIST);
        }

        public static void RemovePageSession_DateList()
        {
            LSLibrary.WebAPP.PageSessionHelper.CleanValue(SESSION_DATELIST);
        }
        #endregion

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