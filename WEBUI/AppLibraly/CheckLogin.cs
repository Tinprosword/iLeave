using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.AppLibraly
{
    public class CheckLogin
    {
        public delegate void OnSessionTimeOut();
        public event OnSessionTimeOut event_OnSessionTimeOut;

        public void CheckIsLogin()
        {
            //check weather user login and do some progress.
            if (LoginUser.IsLogin() == false)
            {
                if (event_OnSessionTimeOut != null)
                {
                    event_OnSessionTimeOut();
                }
            }
        }
    }
}