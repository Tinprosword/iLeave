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


        #region page_girdview
        public class GirdViewData
        {
            public GirdViewData(string title, string content, List<Attachement> attachement, int seq,int _id)
            {
                this.title = title;
                this.content = content;
                Attachement = attachement;
                this.seq = seq;
                this.idInTable = _id;
            }

            public int idInTable { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public List<Attachement> Attachement { get; set; }
            public int seq { get; set; }
        }

        public class Attachement
        {
            public Attachement(string fileName, string filePath, DateTime fileCreateTime,int _id)
            {

                this.fileName = fileName;
                this.filePath = filePath;
                this.idInTable = _id;
                this.fileCreateTime = fileCreateTime;
            }

            public int idInTable { get; set; }
            public string fileName { get; set; }
            public string filePath { get; set; }
            public DateTime fileCreateTime { get; set; }


        }


        #endregion
    }
}