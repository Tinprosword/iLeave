﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL
{
    public class common
    {
        public static string baseUrl = BLL.Other.GetWebSiteRootUrl(HttpContext.Current.Request);

        private static bool isdbug = true;
        private static int devMode = 1100;//1fix ;0,cofing dec 1 nofig;  3:nouse   4 nouse.

        private static LSLibrary.LogUtil logUtil = new LSLibrary.LogUtil( GlobalVariate.pageServer.MapPath("mylog.txt"));

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

    }
}