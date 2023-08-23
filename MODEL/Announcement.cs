using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class Announcement
    {

        #region enum
        public enum enum_Announce_tabs
        {
            NOTICE = 1,
            POLICY = 2,
            Procedure = 3
        }
        #endregion

        [Serializable]
        public class ViewState_page
        {
            public ViewState_page(int activeTab, int selectedYear,List<int> _unread)
            {
                ActiveTab = activeTab;
                SelectedYear = selectedYear;
                UnReadAnnounceID = _unread;
            }

            public int ActiveTab { get; set; }
            public int SelectedYear { get; set; }
            public List<int> UnReadAnnounceID { get; set; }

        }


        public static List<LSLibrary.keyValueCommon> GetAttachmentType()
        {
            List<LSLibrary.keyValueCommon> result = new List<LSLibrary.keyValueCommon>();
            result.Add(new LSLibrary.keyValueCommon(100, "Address Proof"));
            result.Add(new LSLibrary.keyValueCommon(101, "Bank Account Document"));
            result.Add(new LSLibrary.keyValueCommon(102, "REQUIRED_PERSON_ATTACHMENT_FOR_CREATE_STAFF"));
            result.Add(new LSLibrary.keyValueCommon(103, "Employment Contract"));
            result.Add(new LSLibrary.keyValueCommon(104, "Insurance"));
            result.Add(new LSLibrary.keyValueCommon(105, "Safety"));
            result.Add(new LSLibrary.keyValueCommon(106, "Others"));
            return result;
        }

    }
}