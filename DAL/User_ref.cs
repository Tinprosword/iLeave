using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User_ref
    {
        public static int test_add(int a, int b)
        {
            int res = 0;
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            res = webServicesHelper.ws_user.Test_ADD(a, b);
            return res;
        }

        public static MODEL.LoginResult CheckLogin_webref(string login, string password)
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            WebReference_User.LoginResult loginResult = webServicesHelper.ws_user.AuthenticateUser(login, LSLibrary.MD5Util.GetMD5_32(password).ToUpper());
            MODEL.LoginResult res = new MODEL.LoginResult();
            res.Result = loginResult.Result;
            res.SessionID = loginResult.SessionID;
            return res;
        }

        public static bool isLogin()
        {
            DalHelper.WebServicesHelper webServicesHelper = DalHelper.WebServicesHelper.GetInstance();
            return webServicesHelper.ws_user.IsLogin();
        }

    }
}