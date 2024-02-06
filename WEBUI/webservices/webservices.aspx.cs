using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.SessionState;

namespace WEBUI.webservices
{
    public partial class webservices : System.Web.UI.Page
    {
        public class MyPushnotiece
        {
            public MyPushnotiece(string title)
            {
                this.title = title;
            }

            public MyPushnotiece() { }

            public string title { get; set; }
        }

        private BLL.SystemParameters mSystemParameters;

        protected void Page_Load(object sender, EventArgs e)
        {
            mSystemParameters = BLL.SystemParameters.GetSysParameters();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Action"]))
                {
                    string Actionname = Request.QueryString["Action"];

                    if (Actionname == "ws")
                    {
                        Response.Clear();

                        Response.Write(getXml("ok"));
                        Response.End();
                    }
                    if (Actionname == "version")
                    {
                        Response.Clear();

                        Response.Write(BLL.Other.GetVersion());
                        Response.End();
                    }
                    else if (Actionname == "saveheight")
                    {
                        string height = Request.QueryString["sc"];
                        int intHeight = 0;
                        int.TryParse(height, out intHeight);
                        LSLibrary.WebAPP.PageSessionHelper.SetValue(intHeight, "sh");
                    }
                    else if (Actionname == "login")
                    {
                        //<Result>5322</Result>
                        //<SessionID>ipqkjy3jts4vzq45ukat4yqn</SessionID >
                        string UserName = "";
                        string PasswordHash = "";
                        if (!string.IsNullOrEmpty(Request["UserName"]) && !string.IsNullOrEmpty(Request["PasswordHash"]))
                        {
                            UserName = Request["UserName"];
                            PasswordHash = Request["PasswordHash"];
                            var loginresult = BLL.User_wsref.CheckLogin(UserName, PasswordHash);
                            Response.Clear();

                            string result = LSLibrary.XmlConvertor.ObjectToXml(loginresult, true);

                            Response.Write(result);
                            Response.End();
                        }
                    }
                    else if (Actionname == "login_simple")
                    {
                        //<Result>5322</Result>
                        //<SessionID>ipqkjy3jts4vzq45ukat4yqn</SessionID >
                        string UserName = "";
                        string Password = "";
                        if (!string.IsNullOrEmpty(Request["UserName"]) && !string.IsNullOrEmpty(Request["Password"]))
                        {
                            UserName = Request["UserName"];
                            Password = Request["Password"];
                            var loginresult = BLL.User_wsref.CheckLogin(UserName, Password);

                            Response.Clear();
                            Response.Write(loginresult.Result.ToString());
                            Response.End();
                        }
                        else
                        {
                            Response.Clear();
                            Response.Write("-99");
                            Response.End();
                        }
                    }
                    else if(Actionname=="common_action")
                    {
                        commonaction();
                    }
                    else
                    {
                        Response.Clear();
                        Response.Write(getXml(""));
                        Response.End();
                    }
                }
                else
                {
                    Response.Clear();
                    Response.Write(getXml(""));
                    Response.End();
                }
            }
        }

        public void commonaction()
        {
            string actionType = Request["action_type"];
            string actionKey = Request["acion_key"];
            string actionValue = Request["action_value"];
            if (actionType == "deviceid")
            {
                string username = actionKey;
                string devicedid = actionValue;

                if (string.IsNullOrEmpty(actionKey) || string.IsNullOrEmpty(actionValue))
                {
                    return;
                }

                LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType ClientType = LSLibrary.WebAPP.MobilWebHelper.GetClientTypeBy_RequestUserAgent();


                //todo 0. 这里判断anroid 有点问题。但是走这里的都是mobile .所以暂时这样处理。
                if (ClientType == LSLibrary.WebAPP.MobilWebHelper.Enum_ClientType.iphone)
                {
                    BLL.Announcement.DeviceID_InsertOrUpdateDeviceID(1, devicedid, username);
                }
                else
                {
                    BLL.Announcement.DeviceID_InsertOrUpdateDeviceID(2, devicedid, username);
                }
            }
            else if (actionType == "GetMyAndroidLocalPushNotice")
            {
                string uid = actionKey;

                if (string.IsNullOrEmpty(actionKey))
                {
                    return;
                }

                List<MyPushnotiece> notices = new List<MyPushnotiece>();

                var myloaclpush = BLL.Announcement.GetAndroidLocalPush(uid);
                if (myloaclpush != null && myloaclpush.Count() > 0)
                {
                    foreach (int theid in myloaclpush)
                    {
                        var theAnounce = BLL.Announcement.GetAnouncementByID(theid);
                        if (theAnounce != null)
                        {
                            string title = theAnounce.Subject;
                            notices.Add(new MyPushnotiece(title));

                            BLL.Announcement.SetPushed(theid, WebServiceLayer.WebReference_Ileave_Other.enum_CommonKeyValueTypeCode.Push_Already_Localandroid, uid);
                        }
                    }
                }

                Response.Clear();

                string ttresult = LSLibrary.XmlConvertor.ObjectToXml(notices, true);

                Response.Write(ttresult);


                Response.End();
            }
            else if (actionType == "systemparameter")
            {
                BLL.SystemParameters systemParameters = BLL.SystemParameters.GetSysParameters();
                if (systemParameters.mENABLE_LOGIN_2FA)
                {
                    Response.Write("1");
                    Response.End();
                }
                else
                {
                    Response.Write("0");
                    Response.End();
                }
            }
            else if (actionType == "getemailcode")
            {
                string code = LSLibrary.UnicodeHelper.RandomNumSupplier.GetCode_int(6);
                Response.Write(code);
                Response.End();
            }
            else if (actionType == "sendemail")
            {
                int result = BLL.Other.SendEmail_VerifyCode(actionKey, actionValue, mSystemParameters);
                Response.Write(result.ToString());
                Response.End();
            }
            else
            {
                Response.Write("");
                Response.End();
            }
        }

        public string getXml(string a)
        {
            return "<result>"+a+"</result>";
        }
    }
}