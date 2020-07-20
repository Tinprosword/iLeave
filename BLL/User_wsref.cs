using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class User_wsref
    {
        public static MODEL.LoginResult CheckLogin(string uid, string password)
        {
            return DAL.User_ref.CheckLogin_webref(uid, password);
        }

        public static int test_add(int a, int b)
        {
            BLL.LoginManager.CheckWsLogin();
            return DAL.User_ref.test_add(a, b);
        }

    }
}