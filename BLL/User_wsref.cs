using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Generic;

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
            WebServiceLayer.WebReference_user.LoginResult result = new WebServiceLayer.WebReference_user.LoginResult();
            result.Result = 0;
            result.SessionID = "";
            bool LDAP = LSLibrary.WebAPP.WebConfig.getValue("EnableSSO") == "0" ? false : true;
            if (LDAP)
            {
                result = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.AuthenticateUserAD(uid, password);
                if (result.Result < 1)
                {
                    result = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.AuthenticateUser(uid, LSLibrary.MD5Util.GetMD5_32(password).ToUpper());
                }
            }
            else
            {
                result= WebServiceLayer.MyWebService.GlobalWebServices.ws_user.AuthenticateUser(uid, LSLibrary.MD5Util.GetMD5_32(password).ToUpper());
            }
            return result;
        }


        public static LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> GetLoginer()
        {
            return LSLibrary.WebAPP.LoginManager.GetLoinger<MODEL.UserInfo>();
        }


        public static void MPG_GoBackToLogin()
        {
            var cookie= BLL.Page.MyCookieManage.GetCookie();
            MorePlaygroundAction("sys", "loginout", "sys", "loginout", "~/login.aspx?action=userloginout",cookie.isAppLogin=="1");
        }


        public static void MPG_GoBackToSign()
        {
            MorePlaygroundAction("sys", "signin", "sys", "signin", "",true);
        }



        public static void MorePlaygroundAction(string AndroidMsgtype,string androidMsgValue, string appleMsgtype, string appleMsgValue, string pclink,bool isapp)
        {
            string agent = HttpContext.Current.Request.UserAgent;

            LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.HttpContractHelper.GetClientType(agent);
            if (ClientType == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.android && isapp)//android
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write(LSLibrary.WebAPP.MyJSHelper.SendMessageToAndroid(AndroidMsgtype, androidMsgValue, HttpContext.Current.Server));
                HttpContext.Current.Response.End();
            }
            else if (ClientType == LSLibrary.WebAPP.HttpContractHelper.Enum_ClientType.iphone && isapp)//ios
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write(LSLibrary.WebAPP.MyJSHelper.SendMessageToAndroid(appleMsgtype, appleMsgValue, HttpContext.Current.Server));
                HttpContext.Current.Response.End();
            }
            else//pc
            {
                if (!string.IsNullOrEmpty(pclink))
                {
                    HttpContext.Current.Response.Redirect(pclink, true);
                }
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

        public static WebServiceLayer.WebReference_user.PersonBaseinfo[] GetPersonBaseInfoByUid(int uid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfo("u_id=" + uid);
        }

        public static WebServiceLayer.WebReference_user.PersonBaseinfo[] GetPersonBaseInfoByLikeName(string Containname)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfo("p_Nickname like '%" + Containname + "%' or p_Surname like '%" + Containname + "%' or p_othername like '%" + Containname + "%' or p_namech like '%" + Containname + "%' or (p_Surname+' '+p_Othername) like '%"+Containname+"%'");
        }

        public static WebServiceLayer.WebReference_user.PersonBaseinfo[] GetPersonBaseInfoByLikeStaffNo(string staffno)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfo("s_StaffNumber like '%" + staffno + "%'");
        }


        public static WebServiceLayer.WebReference_user.PersonBaseinfo[] GetPersonBaseInfoByPid(int pid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfoByPid(pid);
        }

        public static List<int> GetStaffsByUid(int pid)
        {
            WebServiceLayer.WebReference_user.PersonBaseinfo[] res = GetPersonBaseInfoByPid(pid);
            HashSet<int> result = new HashSet<int>();
            foreach (WebServiceLayer.WebReference_user.PersonBaseinfo p in res)
            {
                result.Add((int)p.s_id);
            }
            return result.ToList();
        }

        public static WebServiceLayer.WebReference_user.PersonBaseinfo[] FilterValidUser(WebServiceLayer.WebReference_user.PersonBaseinfo[] data)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.FilterValidUser(data);
        }


        public static WebServiceLayer.WebReference_user.PersonBaseinfo GetDefaultLoginEmploymentAndEids(int pid,out List<int> eidsRefLoginemploymentFirsteid,out List<string> enosRefLoginEmploymentFirstID)
        {
            WebServiceLayer.WebReference_user.PersonBaseinfo result = null;
            eidsRefLoginemploymentFirsteid = new List<int>();
            enosRefLoginEmploymentFirstID = new List<string>();
            WebServiceLayer.WebReference_user.PersonBaseinfo[] allinfos = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetPersonBaseInfoByPid(pid);
            WebServiceLayer.WebReference_user.PersonBaseinfo[] canlogins = WebServiceLayer.MyWebService.GlobalWebServices.ws_user.FilterCanLoginUser(allinfos);
            canlogins = canlogins.OrderByDescending(x => x.e_id).ToArray();
            if (canlogins != null && canlogins.Count() > 0)
            {
                result = canlogins[0];
                int? firsteid = result.e_FirstEmploymentID;
                if (firsteid != null)
                {
                    var tempeids = allinfos.Where(x => x.e_FirstEmploymentID == firsteid).ToList();
                    if (tempeids != null && tempeids.Count() > 0)
                    {
                        foreach (var temipitem in tempeids)
                        {
                            if (temipitem != null)
                            {
                                if (temipitem.e_id != null && temipitem.e_EmploymentNumber != null)
                                {
                                    eidsRefLoginemploymentFirsteid.Add((int)temipitem.e_id);
                                    enosRefLoginEmploymentFirstID.Add(temipitem.e_EmploymentNumber);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        //no person .no employmnet 无法登陆.
        public static MODEL.UserInfo GetAndSaveInfoToSession(string userid, WebServiceLayer.WebReference_user.LoginResult loginResult)
        {
            WebServiceLayer.WebReference_user.t_Person person = BLL.User_wsref.GerPersonByuid(loginResult.Result);
            WebServiceLayer.WebReference_user.PersonBaseinfo personEmplyment = null;
            MODEL.UserInfo userInfo = null;
            if (person != null)
            {
                List<int> eids;
                List<string> enumbers;
                personEmplyment = BLL.User_wsref.GetDefaultLoginEmploymentAndEids(person.ID,out eids,out enumbers);
                var ccodelist=BLL.CodeSetting.GetCompanyDeployCode();
                string ccode = "";
                if (ccodelist != null && ccodelist.Count()>0)
                {
                    ccode = ccodelist[0];
                }
                userInfo = SaveInfoToSession(personEmplyment, loginResult.SessionID,eids,enumbers,ccode);
            }
            return userInfo;
        }

        public static MODEL.UserInfo SaveInfoToSession(WebServiceLayer.WebReference_user.PersonBaseinfo personBaseinfo,string sessinonID,List<int> eids,List<string> enos,string ccode)
        {
            MODEL.UserInfo userInfo = null;
            if (personBaseinfo != null && personBaseinfo.e_id!=null)
            {
                
                userInfo = new MODEL.UserInfo((int)personBaseinfo.u_id, personBaseinfo.u_Username, personBaseinfo.p_Nickname, sessinonID, personBaseinfo.e_id, personBaseinfo.e_EmploymentNumber, personBaseinfo.s_id, personBaseinfo.s_StaffNumber, personBaseinfo.p_id, personBaseinfo.p_Surname, personBaseinfo.p_Othername,personBaseinfo.p_NameCH,0,0,false,eids,enos,personBaseinfo.e_FirstEmploymentID,personBaseinfo.s_CompanyID,ccode);
                LSLibrary.WebAPP.LoginManager.SetLoginer(new LSLibrary.WebAPP.LoginUser<MODEL.UserInfo>(personBaseinfo.u_Username, userInfo));
            }
            else if(personBaseinfo != null && personBaseinfo.e_id == null)
            {
                userInfo = new MODEL.UserInfo((int)personBaseinfo.u_id, personBaseinfo.u_Username, personBaseinfo.p_Nickname, sessinonID, null, null, null, null, personBaseinfo.p_id, "", "",personBaseinfo.p_NameCH,0,0,false,eids,enos,null,null,null);
                LSLibrary.WebAPP.LoginManager.SetLoginer(new LSLibrary.WebAPP.LoginUser<MODEL.UserInfo>(personBaseinfo.u_Username, userInfo));
            }
            return userInfo;
        }

        public static WebServiceLayer.WebReference_user.EmployDetail GetEmployDetailByeid(int eid)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.GetEmployDetailByEid(eid);
        }


        public static void ChangeInfoToSession(int employid,string employnumber, int sid, string snumber,bool more)
        {
            LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> loginer = LSLibrary.WebAPP.LoginManager.GetLoinger<MODEL.UserInfo>();
            loginer.userInfo.employID = employid;
            loginer.userInfo.employNnumber = employnumber;
            loginer.userInfo.staffid = sid;
            loginer.userInfo.staffNumber = snumber;
            loginer.userInfo.moreEmployment = more;
            LSLibrary.WebAPP.LoginManager.SetLoginer(loginer);
        }

        public static void ChangeSessionHeight(int sh,int sw)
        {
            LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> loginer = LSLibrary.WebAPP.LoginManager.GetLoinger<MODEL.UserInfo>();
            loginer.userInfo.ScreenHeight = sh;
            loginer.userInfo.ScreenWidth = sw;
            LSLibrary.WebAPP.LoginManager.SetLoginer(loginer);
        }

        public static string GetDisplayName(int uid, int nametype)
        {
            string result = "";
            var users =GetPersonBaseInfoByUid(uid).ToList();
            if (users != null && users.Count() > 0)
            {
                MODEL.UserName tempUserName = new MODEL.UserName(users[0].p_Surname, users[0].p_Othername, users[0].p_Nickname, users[0].p_NameCH);
                result = tempUserName.GetDisplayName(nametype);
            }

            return result;
        }

        #region auto
        public static WebServiceLayer.WebReference_user.t_Employment getEmploymentByid(int id)
        {
            return WebServiceLayer.MyWebService.GlobalWebServices.ws_user.Base_Gett_Employment(new WebServiceLayer.WebReference_user.t_Employment() { ID = id });
        }


        public static WebServiceLayer.WebReference_user.t_Employment[] getEmploymentByZone(List<MODEL.Calendar.UserAssoZone> userAssos)
        {
            List<WebServiceLayer.WebReference_user.t_Employment> result = new List<WebServiceLayer.WebReference_user.t_Employment>();

            if (userAssos != null && userAssos.Count>0)
            { 
                for (int i = 0; i < userAssos.Count(); i++)
                {
                    WebServiceLayer.WebReference_user.t_Employment[] tempResult = getEmploymentByZone(userAssos[i].contractID, userAssos[i].zoneCode);
                    result.AddRange(tempResult);
                }
            }

            return result.ToArray();
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