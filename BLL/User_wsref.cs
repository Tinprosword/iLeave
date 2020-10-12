using System.Linq;
using System.Web;


namespace BLL
{
    public class User_wsref
    {
        public static int test_add(int a, int b)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.Test_ADD(a, b);
        }

        public static WebServiceLayer.WebReference_user.LoginResult CheckLogin(string uid, string password)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.AuthenticateUser(uid, LSLibrary.MD5Util.GetMD5_32(password).ToUpper());
        }


        public static void CheckWsLogin()
        {
            if (!WebServiceLayer.MyWebService.GlobalWebServices.ws_user.IsLogin())
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

                    HttpContext.Current.Response.Write(LSLibrary.WebAPP.MyJSHelper.SendMessageToAndroid("sys", "loginout", HttpContext.Current.Server));
                
            }
            else if (ClientType == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.iphone)//ios
            {
                HttpContext.Current.Response.Write(LSLibrary.WebAPP.MyJSHelper.SendMessageToIphone("sys", "loginout", HttpContext.Current.Server));
            }
            else//pc
            {
                HttpContext.Current.Response.Redirect("~/login.aspx",true);
            }
        }


        public static void Onf5()
        {
            HttpContext.Current.Response.Redirect("~/login.aspx");
        }


        public static WebServiceLayer.WebReference_user.t_Person GerPersonByuid(int uid)
        {
            WebServiceLayer.WebReference_user.t_Person result = null;
            var temp= WebServiceLayer.MyWebService.GlobalWebServices.ws_user.Base_GetListt_Person("userid=" + uid);
            result = temp.Count() == 1 ? temp[0] : null;
            return result;
        }


        public static WebServiceLayer.WebReference_user.PersonBaseinfo[] GetPersonBaseInfoByLikeName(string Containname)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfo("p_Nickname like '%" + Containname + "%' or p_Surname like '%" + Containname + "%' or p_othername like '%" + Containname + "%' or p_namech like '%" + Containname + "%' or (p_Surname+' '+p_Othername) like '%"+Containname+"%'");
        }


        public static WebServiceLayer.WebReference_user.PersonBaseinfo[] GetPersonBaseInfoByPid(int pid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfoByPid(pid);
        }

        public static WebServiceLayer.WebReference_user.PersonBaseinfo[] FilterValidUser(WebServiceLayer.WebReference_user.PersonBaseinfo[] data)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.FilterValidUser(data);
        }

        public static WebServiceLayer.WebReference_user.PersonBaseinfo GetPersonBaseinfos_validateDefaultEmploymentNow(int pid)
        {
            WebServiceLayer.WebReference_user.PersonBaseinfo result = null;
            WebServiceLayer.WebReference_user.PersonBaseinfo[] res = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfo_ValidateEmploymentForToday(pid);
            if (res != null && res.Count() > 0)
            {
                var item = res.Where(x => x.e_IsMain!=null && x.e_IsMain==true).OrderBy(x=>x.e_FirstEmploymentID).ToList();//最早申请的staff 中的main employment 作为主employment.
                result = item.Count() >0 ? item[0] : null;
            }
            return result ;
        }


        public static void SaveInfoToSession(string userid, WebServiceLayer.WebReference_user.LoginResult loginResult)
        {
            WebServiceLayer.WebReference_user.t_Person person = BLL.User_wsref.GerPersonByuid(loginResult.Result);

            WebServiceLayer.WebReference_user.PersonBaseinfo personBaseinfo = BLL.User_wsref.GetPersonBaseinfos_validateDefaultEmploymentNow(person.ID);
            MODEL.UserInfo userInfo = null;
            if (personBaseinfo != null)
            {
                userInfo = new MODEL.UserInfo(loginResult.Result, userid, "", loginResult.SessionID, personBaseinfo.e_id, personBaseinfo.e_EmploymentNumber, personBaseinfo.s_id, personBaseinfo.s_StaffNumber, personBaseinfo.p_id,personBaseinfo.p_Surname,personBaseinfo.p_Othername);
            }
            else
            {
                userInfo = new MODEL.UserInfo(loginResult.Result, userid, "", loginResult.SessionID, null, null, null, null, person.ID,"","");
            }

            LSLibrary.WebAPP.LoginManager.SetLoginer(new LSLibrary.WebAPP.LoginUser<MODEL.UserInfo>(userid, userInfo));
        }


        #region auto
        public static WebServiceLayer.WebReference_user.t_Employment getEmploymentByid(int id)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.Base_Gett_Employment(new WebServiceLayer.WebReference_user.t_Employment() { ID = id });
        }

        public static WebServiceLayer.WebReference_user.t_Employment[] getEmploymentByZone(int contractid, string zone)
        {
            WebServiceLayer.WebReference_user.t_Employment[] result = new WebServiceLayer.WebReference_user.t_Employment[0];
            if (contractid == 0)
            {
                result= WebServiceLayer.MyWebService.GlobalWebServices.ws_user.Base_GetListt_Employment("");
            }
            else if (contractid != 0 && zone == "")
            {
                result= WebServiceLayer.MyWebService.GlobalWebServices.ws_user.Base_GetListt_Employment("contractid=" + contractid);
            }
            else if (contractid != 0 && zone != "")
            {
                result= WebServiceLayer.MyWebService.GlobalWebServices.ws_user.Base_GetListt_Employment("contractid=" + contractid + " and zonecode='" + zone + "'");
            }
            return result; 
        }
        #endregion

    }
}