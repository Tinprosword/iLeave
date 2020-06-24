using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace BLL
{
    public class GlobalVariate
    {
        //global string
        public static string login_error = "invalid user and password.";
        public static string path_uploadPic = "uploadPic";


        //cooki name
        public static readonly string COOKIE_SERVERADDRESS="cookie_serveraddress";
        public static readonly string COOKIE_HTTPS = "cookie_https";



        //用于js css文件的修改后自动重新下载.
        public static HttpServerUtility pageServer;
        public static string appcssLastmodify
        {
            get
            {
                string filePath= pageServer.MapPath("~/Res/App/appcss.css");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");
                
            }
            set { }
        }

        public static string applyjsLastmodify
        {
            get
            {
                string filePath = pageServer.MapPath("~/Res/App/apply.js");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");

            }
            set { }
        }

        public static string autoscalejsLastmodify
        {
            get
            {
                string filePath = pageServer.MapPath("~/Res/App/autoScale.js");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");

            }
            set { }
        }

        public static string commonjsLastmodify
        {
            get
            {
                string filePath = pageServer.MapPath("~/Res/App/CommonJS.js");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");

            }
            set { }
        }

        public static string myapplicationjsLastmodify
        {
            get
            {
                string filePath = pageServer.MapPath("~/Res/App/myapplication.js");
                return LSLibrary.FileUtil.GetLastWriteTime(filePath).ToString("yyyyMMddhhmmss");

            }
            set { }
        }

    }
}