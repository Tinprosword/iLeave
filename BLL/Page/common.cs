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

        public static List<string> UploadAttendance(HttpRequest httpRequest, string fpath, List<string> fileExtendsType, string NameAppendStr, out string errmsg, int filesizeM = 10)
        {
            List<string> res = LSLibrary.UploadFile.SaveFiles(httpRequest, fpath, fileExtendsType, System.DateTime.Now.ToString("yyyyMMdd"), out errmsg, filesizeM);
            for (int i = 0; i < res.Count; i++)
            {
                if (common.IsImagge(res[i]))
                {
                    LSLibrary.ImageThumbnail.ReducedImage(130, 130, fpath + "\\" + res[i], fpath + "\\" + Leave.reducePath + "\\" + res[i]);
                }
            }
            return res;
        }
    }
}