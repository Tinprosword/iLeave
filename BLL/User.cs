using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class User
    {


        public static int CheckLogin(string uid, string password)
        {
            return DAL.User.CheckLogin(uid, password);
        }

        public static LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> GetLoginer()
        {
            return LSLibrary.WebAPP.LoginManager.GetLoinger<MODEL.UserInfo>();
        }


        public static void OnLoginOff()
        {
            string agent = HttpContext.Current.Request.UserAgent;

            LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.HttpContractHelper.GetClientType(agent);
            if(ClientType== LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.android)//android
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write(LSLibrary.WebAPP.MyJSHelper.GetAndroidJs("sys", "loginout", HttpContext.Current.Server));
                HttpContext.Current.Response.End();
            }
            else if(ClientType== LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.iphone)//ios
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write(LSLibrary.WebAPP.MyJSHelper.GetIphoneJs("sys", "loginout", HttpContext.Current.Server));
                HttpContext.Current.Response.End();
            }
            else//pc
            {
                HttpContext.Current.Response.Redirect("~/login.aspx");
            }
        }
    }
}