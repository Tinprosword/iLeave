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
        public static int test_add(int a, int b)
        {
            return DAL.MyWebService.GlobalWebServices.ws_user.Test_ADD(a, b);
        }

        public static DAL.WebReference_User.LoginResult CheckLogin(string uid, string password)
        {
            return DAL.MyWebService.GlobalWebServices.ws_user.AuthenticateUser(uid, LSLibrary.MD5Util.GetMD5_32(password).ToUpper());
        }


        public static void CheckWsLogin()
        {
            if (!DAL.MyWebService.GlobalWebServices.ws_user.IsLogin())
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


        public static DAL.WebReference_User.t_Person GerPersonByuid(int uid)
        {
            DAL.WebReference_User.t_Person result = null;
            var temp= DAL.MyWebService.GlobalWebServices.ws_user.Base_GetListt_Person("userid=" + uid);
            result = temp.Count() == 1 ? temp[0] : null;
            return result;
        }



        public static DAL.WebReference_User.PersonBaseinfo[] GetPersonBaseInfoByPid(int pid)
        {
            DAL.WebReference_User.PersonBaseinfo result = null;
            return DAL.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfoByPid(pid);
        }



        public static DAL.WebReference_User.PersonBaseinfo GetPersonBaseinfos_validateDefaultEmploymentNow(int pid)
        {
            DAL.WebReference_User.PersonBaseinfo result = null;
            DAL.WebReference_User.PersonBaseinfo[] res = DAL.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfo_ValidateEmploymentForToday(pid);
            if (res != null && res.Count() > 0)
            {
                result = res[0];//todo 得到薪水更高的一个.
            }
            return result ;
        }


        public static void SaveInfoToSession(string userid, DAL.WebReference_User.LoginResult loginResult)
        {
            DAL.WebReference_User.t_Person person = BLL.User_wsref.GerPersonByuid(loginResult.Result);

            DAL.WebReference_User.PersonBaseinfo personBaseinfo = BLL.User_wsref.GetPersonBaseinfos_validateDefaultEmploymentNow(person.ID);
            MODEL.UserInfo userInfo = null;
            if (personBaseinfo != null)
            {
                userInfo = new MODEL.UserInfo(loginResult.Result, userid, "", loginResult.SessionID, personBaseinfo.e_id, personBaseinfo.e_EmploymentNumber, personBaseinfo.s_id, personBaseinfo.s_StaffNumber, personBaseinfo.p_id);
            }
            else
            {
                userInfo = new MODEL.UserInfo(loginResult.Result, userid, "", loginResult.SessionID, null, null, null, null, person.ID);
            }

            LSLibrary.WebAPP.LoginManager.SetLoginer(new LSLibrary.WebAPP.LoginUser<MODEL.UserInfo>(userid, userInfo));
        }

    }
}