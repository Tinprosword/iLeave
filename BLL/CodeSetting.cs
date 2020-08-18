
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CodeSetting
    {
        public static List<int> GetSections(int position, int leave)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_codesetting.GetLeaveSections(leave, position).ToList();
        }
    }
}