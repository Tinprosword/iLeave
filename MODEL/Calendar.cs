using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public abstract class Calendar
    {
        //页面不方便保存的，需要回傳的數據.

        [Serializable]
        public class UserAssoZone
        {
            public int contractID { get; set; }
            public string zoneCode { get; set; }
        }

        [Serializable]
        public class ViewState_page
        {

            public List<UserAssoZone> mUserAssoZone { get; set; }

            public ViewState_page()
            {
                mUserAssoZone = new List<UserAssoZone>();
            }
            
        }
    }
}
