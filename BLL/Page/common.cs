using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class common
    {

        private static bool isdbug = false;
        private static int devMode = 1100;//1fix ;0,cofing dec 1 nofig;  3:nouse   4 nouse.

        private static LSLibrary.LogUtil logUtil = new LSLibrary.LogUtil( GlobalVariate.pageServer.MapPath("mylog.txt"));

        //loginer.userInfo.loginName
        public static string GetAttachmentHtml(int requestid,HttpServerUtility server,string loginname, GlobalVariate.AttachType attachtype)
        {
            List<MODEL.App_AttachmentInfo> result = BLL.Leave.getAttendanceModel(loginname, requestid, server,  attachtype);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Count; i++)
            {
                string filename = LSLibrary.FileUtil.GetFileName(result[i].originAttendance_RelatePath);
                if (LSLibrary.FileUtil.IsImagge(filename))
                {
                    sb.Append("<a href='showpic2.aspx?path=" + result[i].originAttendance_RelatePath + "'>" + result[i].GetFileName(10) + "</a>&nbsp;");
                }
                else
                {
                    sb.Append("<a href=" + result[i].Get_originAttendance_RealRelatePath() + ">" + result[i].GetFileName(10) + "</a>&nbsp;");
                }
            }
            return sb.ToString();
        }




        public static void copyFileTo(string filePath, string descPath,HttpServerUtility server)
        {
            string absfilepath = server.MapPath(filePath);
            System.IO.Directory.CreateDirectory(System.IO.Directory.GetParent(descPath).ToString());
            LSLibrary.FileUtil.Copy(absfilepath, descPath);
        }

        public static string GetAttachmentNumberPath(int number)
        {
            string result = "";

            if (number > 0 && number < 10)
            {
                result = "~/res/images/c" + number.ToString() + ".png";
            }
            else if (number >= 10)
            {
                result = "~/res/images/c9m.png";
            }

            return result;
        }


        public static bool canReduceImage(string image)
        {
            return LSLibrary.FileUtil.IsImagge(image);
        }

        public static List<string> UploadAttendanceAndReduce(HttpRequest httpRequest, string fpath, List<string> fileExtendsType, string NameAppendStr, out string errmsg, int filesizeM = 10)
        {
            List<string> res = LSLibrary.UploadFile.SaveFiles(httpRequest, fpath, fileExtendsType, System.DateTime.Now.ToString("yyyyMMdd"), out errmsg, filesizeM);
            for (int i = 0; i < res.Count; i++)
            {
                if (common.canReduceImage(res[i]))
                {
                    LSLibrary.ImageThumbnail.ReducedImage(130, 130, fpath + "\\" + res[i], fpath + "\\" + Leave.reducePath + "\\" + res[i]);
                }
            }
            return res;
        }

        public static void CopyAttendanceAndReduce(string sourceFileAbsolutePath, string originFolderAbsolutePath, string reduceFolderAbsolutePath)
        {
            string filename = LSLibrary.FileUtil.GetFileName(sourceFileAbsolutePath);

            if (!System.IO.File.Exists(originFolderAbsolutePath + "\\" + filename))
            {
                LSLibrary.FileUtil.Copy(sourceFileAbsolutePath, originFolderAbsolutePath);
                if (common.canReduceImage(filename))
                {
                    LSLibrary.ImageThumbnail.ReducedImage(130, 130, originFolderAbsolutePath, reduceFolderAbsolutePath);
                }
            }
        }

        
        public static void WriteLog(System.Diagnostics.StackFrame sf , string log)
        {
            if (isdbug)
            {
                logUtil.WriteLog(log, sf);
            }
        }

        public static void WriteLog(string log)
        {
            if (isdbug)
            {
                logUtil.WriteLog(log);
            }
        }


        public static void CheckMyPostback(string eventName,HttpRequest Request,EventHandler eventHandler,object sender,EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["mypostback_target"]))
            {
                string traget = Request.Form["mypostback_target"];
                if (traget == eventName)
                {
                    eventHandler(sender, e);
                }
            }
        }

        public static void CheckMyPostbackEventNameStart(string eventName, HttpRequest Request, EventHandler eventHandler, object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["mypostback_target"]))
            {
                string traget = Request.Form["mypostback_target"];
                if (traget.StartsWith(eventName))
                {
                    sender = traget;
                    eventHandler(sender, e);
                }
            }
        }

        public static void OnClickAttachment(string relativePath,HttpResponse Response,HttpServerUtility Server)
        {
            string filePath = Server.MapPath(relativePath);
            bool isimage = LSLibrary.FileUtil.IsImagge(System.IO.Path.GetFileName(filePath));
            if (isimage)
            {
                Response.Redirect("showpic2.aspx?path=" + HttpUtility.HtmlEncode(relativePath));
            }
            else
            {
                Response.Redirect(relativePath);
            }
        }

        public static string ShowJsFunction(string msg,string eventName)
        {
            string js = "return ShowMessage('" + msg + "','"+eventName+"');";
            return js;
        }

        public static void SetToHiddenInput(string value)
        {

        }


        public static string GetFormatTime(LSLibrary.WebAPP.LanguageType language)
        {
            DateTime date = System.DateTime.Now;
            var ampm = date.Hour >= 12 ? "PM" : "AM";
            string strTime = date.ToString("hh:mm") + " " + ampm;

            return strTime;
        }

    }
}