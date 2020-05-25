using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class User
    {
        public static bool CheckLogin(string uid, string password)
        {
            return DataDemo.checklogin(uid, password);
        }
    }
}