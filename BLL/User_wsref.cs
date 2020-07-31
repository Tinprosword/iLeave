using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;



namespace BLL
{
    public class User_wsref
    {
        public static MODEL.LoginResult CheckLogin(string uid, string password)
        {
            return DAL.User_ref.CheckLogin_webref(uid, password);
        }


        public static void CheckWsLogin()
        {
            if (!DAL.User_ref.isLogin())
            {
                GoBackToLogin();
            }
        }


        public static LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> GetLoginer()
        {
            return LSLibrary.WebAPP.LoginManager.GetLoinger<MODEL.UserInfo>();
        }


        public static void GoBackToLogin()
        {
            string agent = HttpContext.Current.Request.UserAgent;

            LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.HttpContractHelper.GetClientType(agent);
            if (ClientType == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.android)//android
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write(LSLibrary.WebAPP.MyJSHelper.GetAndroidJs("sys", "loginout", HttpContext.Current.Server));
                HttpContext.Current.Response.End();
            }
            else if (ClientType == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.iphone)//ios
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


        public static int test_add(int a, int b)
        {
            CheckWsLogin();
            return DAL.User_ref.test_add(a, b);
        }


        public static DAL.WebReference_User.PersonBaseinfo[] GetPersonBaseInfoByUid(int uid)
        {
            return DAL.User_ref.GetPersonBaseInfoByUid(uid);
        }

        public static DAL.WebReference_User.PersonBaseinfo[] GetPersonBaseinfos_validateEmploymentNow(int uid)
        {
            DAL.WebReference_User.PersonBaseinfo[] res = DAL.User_ref.GetPersonBaseinfos_validToday(uid);
            return res;
        }

        public static DAL.WebReference_User.PersonBaseinfo GetPersonBaseinfos_validateDefaultEmploymentNow(int uid)
        {
            DAL.WebReference_User.PersonBaseinfo result = null;
            DAL.WebReference_User.PersonBaseinfo[] res = GetPersonBaseinfos_validateEmploymentNow(uid);
            if (res != null && res.Count() > 0)
            {
                result = res[0];//todo 得到薪水更高的一个.
            }
            return result ;
        }


        public static DAL.WebReference_User.PersonBaseinfo GetPersonBaseinfoByEmploymentID(int uid, int employmentid)
        {
            DAL.WebReference_User.PersonBaseinfo result = null;
            DAL.WebReference_User.PersonBaseinfo[] res = DAL.User_ref.GetPersonBaseInfoByUid(uid);
            if (res != null && res.Count() > 0)
            {
                var tempres = res.Where(x => x.e_id == employmentid);
                if (tempres != null && tempres.Count() > 0)
                {
                    result = tempres.First();
                }
            }
            return result;
        }

    }
}