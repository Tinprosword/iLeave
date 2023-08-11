using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class Announcement
    {
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