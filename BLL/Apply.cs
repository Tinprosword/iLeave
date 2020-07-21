using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;

namespace BLL
{
    public class Apply
    {
        public static string reducePath = "reduce";


        public static List<string> UploadAttendance(HttpRequest httpRequest, string fpath, List<string> fileExtendsType, string NameAppendStr, out string errmsg, int filesizeM = 10)
        {
            List<string> res= LSLibrary.UploadFile.SaveFiles(httpRequest, fpath, fileExtendsType, System.DateTime.Now.ToString("yyyyMMdd"), out errmsg, filesizeM);
            for (int i = 0; i < res.Count; i++)
            {
                if (IsImagge(res[i]))
                {
                    LSLibrary.ImageThumbnail.ReducedImage(130, 130, fpath + "\\" + res[i], fpath + "\\"+reducePath+"\\" + res[i]);
                }
            }
            return res;
        }


        public static bool IsImagge(string filename)
        {
            string type = filename.Remove(0, filename.IndexOf('.') + 1);
            if (type == "jpg" || type == "png" | type == "gif" | type == "bmp")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static void InsertLeave(List<MODEL.Apply.LeaveData> originDetail, int userid, int staffid)
        {
            BLL.LoginManager.CheckWsLogin();
            DAL.Leave.InsertLeave(originDetail, userid, staffid);
        }


        public static List<LSLibrary.WebAPP.ValueText> GetLeaveType()
        {
            List<LSLibrary.WebAPP.ValueText> res = new List<LSLibrary.WebAPP.ValueText>();
            res.Add(new LSLibrary.WebAPP.ValueText(-1, "Please Select"));
            res.Add(new LSLibrary.WebAPP.ValueText(0, "AL"));
            res.Add(new LSLibrary.WebAPP.ValueText(1, "SL"));
            res.Add(new LSLibrary.WebAPP.ValueText(2, "SL2"));
            return res;
        }
    }
}