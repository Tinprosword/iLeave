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
            public ViewState_page(int activeTab, int selectedYear)
            {
                ActiveTab = activeTab;
                SelectedYear = selectedYear;
            }

            public int ActiveTab { get; set; }
            public int SelectedYear { get; set; }


        }

    }
}