using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    [Serializable]
    public class App_AttachmentInfo
    {
        public string originAttendance_RelatePath;
        public string reduceAttendance_Image_RelatePath;
        public string tempID;
        public string originAttendance_HRDBPath;

        public App_AttachmentInfo(string path, string _reduceImage, string _bigImageAbsolutePath)
        {
            this.originAttendance_RelatePath = path;
            this.tempID = System.DateTime.Now.ToString("yyyyMMddhhmmss");
            this.reduceAttendance_Image_RelatePath = _reduceImage;
            this.originAttendance_HRDBPath = _bigImageAbsolutePath;
        }

        public string GetFileName(int maxLength = 0)
        {
            return LSLibrary.FileUtil.SubFileName(originAttendance_RelatePath, maxLength, "");
        }

        public string Get_originAttendance_RealRelatePath()
        {
            string result = originAttendance_RelatePath.Substring(1);
            result = ".." + result;
            return result;
        }



    }



    public interface IPage_Attachment
    {
        List<App_AttachmentInfo> GetAttachment();
        void SetAttachment(List<App_AttachmentInfo> data);
    }
}