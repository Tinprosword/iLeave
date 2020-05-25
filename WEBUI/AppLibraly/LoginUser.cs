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

        public LoginUser(int iD, string loginID)
        {
            ID = iD;
            LoginID = loginID;
        }
    }
}