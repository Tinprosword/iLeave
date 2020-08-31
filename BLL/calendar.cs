using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class calendar
    {
        public static List<WebServiceLayer.WebReference_leave.Employment_Contract> GetContractByEmployids(int[] eids)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetContractByEmployids(eids).ToList();
        }

        public static WebServiceLayer.WebReference_leave.v_System_Calendar[] GetRoster(DateTime date, List<int> employmentID)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_leave.GetRoster(date, employmentID.ToArray());
        }
    }
}