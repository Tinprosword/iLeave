using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.AppLibraly
{
    public class LoginManager
    {
        public delegate void OnSessionTimeOut();
        public static event OnSessionTimeOut event_OnSessionTimeOut;

        private static readonly string SESSIONNAME_USER = "USER";

        public static void CheckIsLogin()
        {
            //check weather user login and do some progress.
            if (IsLogin() == false)
            {
                if (event_OnSessionTimeOut != null)
                {
                    event_OnSessionTimeOut();
                }
            }
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