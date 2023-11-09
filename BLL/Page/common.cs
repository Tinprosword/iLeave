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

        private static LSLibrary.LogUtil logUtil = new LSLibrary.LogUtil(GlobalVariate.pageServer.MapPath("mylog.txt"));


        //loginer.userInfo.loginName
        //<div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
        public static string GetAttachmentHtml(List<MODEL.App_AttachmentInfo> result)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Count; i++)
            {
                string filename = LSLibrary.FileUtil.GetFileName(result[i].originAttendance_RelatePath);
                if (LSLibrary.FileUtil.IsImagge(filename))
                {
                    string tempItem = "<div style=\"display:inline-block;margin-right:5px; \"><a href=\"showpic2.aspx?path={0}\"><img style=\"width: 30px; height: 30px; \" src=\"{1}\" /></a></div>";
                    string originPicPath = result[i].GetOriginFileName(0);
                    string smallPicPath = result[i].GetReduceFileName();
                    originPicPath = LSLibrary.WebAPP.httpHelper.GenerateURL("uploadpic\\" + originPicPath);
                    smallPicPath = LSLibrary.WebAPP.httpHelper.GenerateURL("uploadpic\\reduce\\" + smallPicPath);
                    tempItem = string.Format(tempItem, originPicPath, smallPicPath);
                    sb.Append(tempItem);
                }
                else
                {
                    string tempItem = "<div style=\"display:inline-block; \"><a href=\"{0}\"><img style=\"width: 20px; height: 20px; \" src=\"{1}\" /></a></div>";
                    string originPicPath = result[i].GetOriginFileName(0);
                    string smallPicPath = result[i].GetReduceFileName();
                    originPicPath = LSLibrary.WebAPP.httpHelper.GenerateURL("uploadpic\\" + originPicPath);
                    smallPicPath = LSLibrary.WebAPP.httpHelper.GenerateURL("res\\images\\" + smallPicPath);
                    tempItem = string.Format(tempItem, originPicPath, smallPicPath);
                    sb.Append(tempItem);
                }
            }
            return sb.ToString();
        }


        public static void copyFileTo(string filePath, string descPath, HttpServerUtility server)
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
            List<string> res = LSLibrary.UploadFile.SaveFiles(httpRequest, fpath, fileExtendsType, System.DateTime.Now.ToString("yyMMddss"), out errmsg, filesizeM);
            for (int i = 0; i < res.Count; i++)
            {
                if (common.canReduceImage(res[i]))
                {
                    LSLibrary.ImageThumbnail.ReducedImage(400, 400, fpath + "\\" + res[i], fpath + "\\" + Leave.reducePath + "\\" + res[i]);
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


        public static void WriteLog(System.Diagnostics.StackFrame sf, string log)
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


        public static void CheckMyPostback(string eventName, HttpRequest Request, EventHandler eventHandler, object sender, EventArgs e)
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

        public static void OnClickAttachment(string relativePath, HttpResponse Response, HttpServerUtility Server)
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

        public static string ShowJsFunction(string msg, string eventName)
        {
            string js = "return ShowMessage('" + msg + "','" + eventName + "');";
            return js;
        }

        public static void SetToHiddenInput(string value)
        {

        }


        public static string GetFormatTime_currentTime(LSLibrary.WebAPP.LanguageType language)
        {
            DateTime date = System.DateTime.Now;
            var ampm = date.Hour >= 12 ? "PM" : "AM";
            string strTime = date.ToString("hh:mm") + " " + ampm;

            return strTime;
        }

        public static string GetFormatTime(LSLibrary.WebAPP.LanguageType language, DateTime datetime)
        {
            var ampm = datetime.Hour >= 12 ? "PM" : "AM";
            string strTime = datetime.ToString("hh:mm") + " " + ampm + " " + datetime.ToString("yyyy-MM-dd");

            return strTime;
        }


        public static void setDivMinHeight(string sessionname, System.Web.UI.HtmlControls.HtmlGenericControl div)
        {
            string agent = HttpContext.Current.Request.UserAgent;
            LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.MobilWebHelper.GetClientType(agent);

            if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.android || ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone)//android
            {
                try
                {
                    int sh = (int)System.Web.HttpContext.Current.Session[sessionname];
                    if (sh > 585)
                    {
                        div.Style.Remove("min-height");
                        div.Style.Add("min-height", (sh - 33).ToString() + "px");//本来减去banner 53 ,但是好像 手机的状态栏，会多出20.所以高度要更高20.所以少见20.
                    }
                }
                catch { }
            }
        }

        public static string isShortcutQSAndGetDecodeURL(HttpRequest request)
        {
            string result = "";
            if (!string.IsNullOrEmpty(request.QueryString["action"]))
            {
                string queryAction = request.QueryString["action"];

                if (queryAction.ToLower() == "shortcut")
                {
                    if (!string.IsNullOrEmpty(request.QueryString["url"]))
                    {
                        result = request.QueryString["url"];
                    }
                }
            }

            return result;
        }

        public static string GenerateCLOTDisplay(double clotHours, double fullDayHours, string hoursStr, string dayStr)
        {
            string result = "";

            double days = (double)(clotHours / fullDayHours);
            days = roundDown_halforint(days, 0.5f);
            //string displayFormat = "{0}{1} ({2}{3})";
            result = string.Format("{0,-3:0.##}", clotHours) + hoursStr + "&nbsp;&nbsp;&nbsp;(" + string.Format("{0,-3:0.##}", days) + dayStr + ")";
            //result = string.Form§at(displayFormat, string.Format("{0,-3:0.##}", clotHours), hoursStr, string.Format("{0,-3:0.##}", days), dayStr);
            return result;
        }

        public static string GenerateCLOTDisplayDay(double clotHours, double fullDayHours, string dayStr, int decimalType)
        {
            string result = "";
            if (fullDayHours != 0)
            {
                double days = (double)(clotHours / fullDayHours);
                if (decimalType == 1)
                {
                    days = (double)System.Math.Round((Decimal)days, 1);
                }
                else
                {
                    days = (double)System.Math.Round((Decimal)days, 1);
                }
                string strFormat = "({0} {1})";
                result = string.Format(strFormat, days, dayStr);
            }
            else
            {
                string strFormat = "({0} {1})";
                result = string.Format(strFormat, "--", dayStr);
            }
            return result;
        }

        public static bool cooike_isLocalZone()
        {
            bool result = false;
            var myCooike = BLL.Page.MyCookieManage.GetCookie();
            if (myCooike != null && !string.IsNullOrEmpty(myCooike.LocalPCzodeCode) && myCooike.LocalPCzodeCode != "0")
            {
                result = true;
            }
            return result;
        }

        public static bool cookie_isautologinout()
        {
            bool result = false;
            var myCooike = BLL.Page.MyCookieManage.GetCookie();
            if (myCooike != null && !string.IsNullOrEmpty(myCooike.LocalPCzodeAutoLogout) && myCooike.LocalPCzodeAutoLogout == "1")
            {
                result = true;
            }
            return result;
        }

        public static string GetLocalZone()
        {
            string result = "";
            var myCooike = BLL.Page.MyCookieManage.GetCookie();
            if (myCooike != null && !string.IsNullOrEmpty(myCooike.LocalPCzodeCode) && myCooike.LocalPCzodeCode != "0")
            {
                result = myCooike.LocalPCzodeCode;
            }
            return result;
        }

        public static double roundDown_halforint(double value, float roundupValue)
        {
            double result = value;

            int intTermination = (int)value;
            double floatTermination = value - intTermination;
            floatTermination = Math.Round(floatTermination, 3);

            if (roundupValue == 0.5)
            {
                //1.>=0<0.5 =>0 . 2  >=0.5=>0.5   .3 .<0 >-0.5 =>0  4. <=-0.5 =>-0.5
                if (floatTermination == 0)
                {
                    result = intTermination;
                }
                else if (floatTermination > 0 && floatTermination < 0.5)
                {
                    result = intTermination;
                }
                else if (floatTermination >= 0.5)
                {
                    result = intTermination + 0.5;
                }
                else if (floatTermination < 0 && floatTermination >= -0.5)
                {
                    result = intTermination - 0.5;
                }
                else if (floatTermination <= -0.5)
                {
                    result = intTermination - 1;
                }

                else
                {
                    result = intTermination;
                }
            }
            else if (roundupValue == 1)
            {
                result = intTermination;
            }

            return result;
        }
    }
}