using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web;

namespace WEBUI.AppLibraly
{
    public class LoginUser
    {
        public int ID;
        public string LoginID;

        private static readonly string SESSIONNAME_USER = "USER";

        public LoginUser(int iD, string loginID)
        {
            ID = iD;
            LoginID = loginID;
        }

        public static void Login(LoginUser user)
        {
            HttpContext.Current.Session[SESSIONNAME_USER] = user;
        }

        public static void Logoff()
        {
            HttpContext.Current.Session.Remove(SESSIONNAME_USER);
        }

        public static bool IsLogin()
        {
            return HttpContext.Current.Session[SESSIONNAME_USER] != null;
        }
    }
}