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
        private static LSLibrary.LogUtil logUtil = new LSLibrary.LogUtil( GlobalVariate.pageServer.MapPath("mylog.txt"));

        public static bool canReduceImage(string image)
        {
            return IsImagge(image);
        }

        public static bool IsImagge(string filename)
        {
            string type = filename.Remove(0, filename.IndexOf('.') + 1);
            if (type == "jpg" || type == "png" | type == "gif" | type == "bmp" | type== "jpeg")
            {
                return true;
            }
            else
            {
                return false;
            }
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

        private static bool isdbug=false;
        public static void WriteLog(System.Diagnostics.StackFrame sf , string log)
        {
            if (isdbug)
            {
                logUtil.WriteLog(log, sf);
            }
        }


        public static void RegisterMyPostback(string eventName,HttpRequest Request,EventHandler eventHandler,object sender,EventArgs e)
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

    }
}