using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public abstract class Check
    {
        //页面数据，可以用hidden control ,来保存， 但是统一用 viewstate 更统一，方便统一管理.
        [Serializable]
        public class ViewState_page
        {
            public string testid { get; set; }

            public string _zoneCode { get; set; }

            public ViewState_page()
            {
                _zoneCode = "01";//default is 01
            }

            public string GetZone()
            {
                return _zoneCode;
            }
            public void SetZone(string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _zoneCode = value;
                }
            }


        }
    }
}