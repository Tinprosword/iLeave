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


    }
}