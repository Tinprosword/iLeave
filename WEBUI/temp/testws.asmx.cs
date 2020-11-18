using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

namespace WEBUI.temp
{
    /// <summary>
    /// testws 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class testws : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            System.Threading.Thread.Sleep(5000);
            return "Hello World";
        }

        [WebMethod]
        public int add(int a, int b)
        {
            return a + b;
        }


        [WebMethod]
        public book getbook()
        {
            book temp = new book();
            temp.id = 3;
            temp.name = "abc";
            return temp;
        }

        [WebMethod]
        public List<book> getbooks(int count)
        {
            List<book> result = new List<book>();

            for (int i = 0; i < count; i++)
            {
                book temp = new book();
                temp.id = i;
                temp.name = "abc" + i.ToString();

                result.Add(temp);
            }
            return result;
        }


        [WebMethod]
        public string teststring(string a, string b)
        {
            return a + b;
        }
    }

    public class book
    {
        public int id;
        public string name;
    }
}
