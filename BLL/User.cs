using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class User
    {
        public static LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> loginer = LSLibrary.WebAPP.LoginManager.GetLoinger<MODEL.UserInfo>();

        public static bool CheckLogin(string uid, string password)
        {
            return DataDemo.checklogin(uid, password);
        }
    }
}