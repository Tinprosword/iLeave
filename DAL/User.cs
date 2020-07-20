using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    //把webServices的方法，封装到这里，入参和返回值保持一致。
    public class User
    {
        public static bool IsLogin(string xmlBody)
        {
            return xmlBody.Contains("<ErrorMsg>NoSession<ErrorMsg>") ? false : true;
        }

        public static bool IsLogin(Exception ex)
        {
            return ex.Message.ToString().Contains("<ErrorMsg>NoSession<ErrorMsg>") ? false : true;
        }



        //public static MODEL.LoginResult CheckLogin(string login,string password)
        //{
        //    //1.获得address. 2.组合成webservices. 3.request webservices 4.do it
        //    string serverAddress = DalHelper.GetWebServices();
        //    string wsUrl = serverAddress + "/ServicesWithSession/UserManagementV2.asmx/AuthenticateUser";
        //    LSLibrary.HttpWebRequestHelper helper = new LSLibrary.HttpWebRequestHelper();
        //    string post = "UserName={0}&PasswordHash={1}";
        //    post = string.Format(post, login, LSLibrary.MD5Util.GetMD5_32(password).ToUpper());
        //    string xmlBody= helper.Post(wsUrl, post);


        //    DataSet ds = LSLibrary.XmlHelper.ConvertXMLToDataSet(xmlBody);
        //    MODEL.LoginResult loginResult = new MODEL.LoginResult();
        //    loginResult.Result =int.Parse(ds.Tables[0].Rows[0]["Result"].ToString());
        //    loginResult.SessionID = ds.Tables[0].Rows[0]["sessionID"].ToString();

        //    return loginResult;
        //}


        //public static int test_add(int a,int b,string sessionid)
        //{
        //    int res = 0;
        //    string cookieStringOfSession = DalHelper.GetCookieStringOfSessionID(sessionid);
        //    string serverAddress = DalHelper.GetWebServices();
        //    string wsUrl = serverAddress + "/ServicesWithSession/UserManagementV2.asmx/Test_ADD";

        //    string post = "a={0}&b={1}";
        //    post = string.Format(post, a, b);
        //    string xmlBody = LSLibrary.HttpWebRequestHelper.HttpPost(wsUrl, post, cookieStringOfSession);
        //    if(IsLogin(xmlBody))
        //    {
        //        LSLibrary.XmlHelper xmlHelper = new LSLibrary.XmlHelper(xmlBody,false);
        //        res =  int.Parse(xmlHelper.GetFirstElement());
        //    }
        //    else
        //    {
        //        throw new Exception("nosession");
        //    }

        //    return res;
        //}


        

    }
}