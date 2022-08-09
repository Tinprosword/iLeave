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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                    else if(Actionname== "saveheight")
                    {
                        string height = Request.QueryString["sc"];
                        int intHeight = 0;
                        int.TryParse(height,out intHeight);
                        LSLibrary.WebAPP.PageSessionHelper.SetValue(intHeight, "sh");
                    }
                    else if(Actionname=="login")
                    {
                        //<Result>5322</Result>
                        //<SessionID>ipqkjy3jts4vzq45ukat4yqn</SessionID >
                        string UserName = "";
                        string PasswordHash = "";
                        if(!string.IsNullOrEmpty( Request["UserName"]) && !string.IsNullOrEmpty(Request["PasswordHash"]))
                        {
                            UserName = Request["UserName"];
                            PasswordHash = Request["PasswordHash"];
                            var loginresult= BLL.User_wsref.CheckLogin(UserName, PasswordHash);
                            Response.Clear();

                            string result= LSLibrary.XmlConvertor.ObjectToXml(loginresult, true);

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

        public string getXml(string a)
        {
            return "<result>"+a+"</result>";
        }
    }
}