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

        //0 ok. -1 check error -2.insert error
        public static int InsertLeave(List<MODEL.Apply.LeaveData> originDetail, int userid, int? staffid,string remarks,out string errorMsg)
        {
            BLL.LoginManager.CheckWsLogin();

            errorMsg = "";
            int result = -1;
            int checkResult = CheckBeforeApply();
            if (checkResult>= 0)
            {
                DAL.WebReference_leave.StaffLeaveRequest[] details;
                DAL.WebReference_leave.ErrorMessageInfo messageInfo;
                int insertResult=DAL.Leave.InsertLeave(originDetail, userid, staffid, remarks,out details,out messageInfo);
                if(insertResult>=0)
                {
                    int processID = messageInfo.ProcessID;
                    int employID = GetEmpolyMentid(userid, details[0].LeaveDate);
                    int workFlowResult = DAL.Leave.InsertWorkflow(details, userid, messageInfo.ProcessID, employID);
                    int attenchMentResult = DAL.Leave.InsertAttanchMent();
                    result = 0;
                }
                else
                {
                    errorMsg = "";
                    result = -2;
                }
            }
            else
            {
                errorMsg = "";
                result = -1;
            }
            return result;
        }

        private static int CheckBeforeApply()
        {
            return 0;
        }

        public static int GetEmpolyMentid(int uid,DateTime dt)
        {
            return DAL.Leave.GetEmployID(uid,dt);
        }


        //todo move loginc to ws bll. and check leave logic.
        public static List<LSLibrary.WebAPP.ValueText> GetLeaveType()
        {
            DAL.WebReference_codesetting.LeaveInfo[] array= BLL.CodeSetting.GetLeaveInfo();
            List<LSLibrary.WebAPP.ValueText> res = new List<LSLibrary.WebAPP.ValueText>();
            res.Add(new LSLibrary.WebAPP.ValueText(-1, "Please Select"));
            for (int i = 0; i < array.Count(); i++)
            {
                res.Add(new LSLibrary.WebAPP.ValueText(array[i].ID, array[i].Code));
            }
            return res;
        }

    }
}