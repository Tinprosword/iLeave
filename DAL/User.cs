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
        public static int CheckLogin(string login,string password)
        {
            //1.获得address. 2.组合成webservices. 3.request webservices 4.do it
            int res = -1;
            string serverAddress = DalHelper.GetWebServices();
            string wsUrl = serverAddress + "/UserManagement.asmx/AuthenticateUser";
            LSLibrary.HttpWebRequestHelper helper = new LSLibrary.HttpWebRequestHelper();
            string post = "UserName={0}&PasswordHash={1}";
            post = string.Format(post, login, LSLibrary.MD5Util.GetMD5_32(password).ToUpper());
            string xmlBody= helper.Post(wsUrl, post);

            LSLibrary.XmlHelper xmlHelper = new LSLibrary.XmlHelper(xmlBody, false);
            string strRes = xmlHelper.GetFirstElement();
            res = int.Parse(strRes);
            return res;
        }

    }
}