using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace WEBUI.Pages
{
    //public static string qs_action = "action";//0.my mange data  1.mydata 
    //public static string qs_from = "from";//0.leave 1.clot 
    //public static string qs_requestid = "requestid";



    //外部链接，从这里开始。未登录分支登录后，又回到这里走登录的分支。 逻辑闭合，代码复用。nice.
    public partial class shortcut:System.Web.UI.Page
    {
        //userid^manageormy^leaveorclot^requestid
        public static string mQSurl_name = "url";


        private string mQSurl_value = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.lt_js.Text = "";

            string qsaction = Request.QueryString["action"];
            if (qsaction == "logout")
            {
                LSLibrary.WebAPP.LoginManager.Logoff();

                var myc = BLL.Page.MyCookieManage.GetCookie();
                myc.isRemember = "0";
                myc.loginname = "";
                myc.loginpsw = "";
                BLL.Page.MyCookieManage.SetCookie(myc);
            }

            if (!string.IsNullOrEmpty(Request.QueryString[mQSurl_name]))
            {
                mQSurl_value = Request.QueryString[mQSurl_name];
                bool isvalidurl = false;
                int uid = 0; int manageormy = 0; int leaveorclot = 0; int requestid = 0;

                List<string> qsList = mQSurl_value.Split(new char[] { '^' }, StringSplitOptions.None).ToList();

                if (qsList != null && qsList.Count() == 4)
                {
                    if (int.TryParse(qsList[0], out uid) && int.TryParse(qsList[1], out manageormy) && int.TryParse(qsList[2], out leaveorclot) && int.TryParse(qsList[3], out requestid))
                    {
                        isvalidurl = true;
                    }
                }
                if (isvalidurl)
                {

                    LSLibrary.WebAPP.LoginUser<MODEL.UserInfo> loginer = LSLibrary.WebAPP.LoginManager.GetLoinger<MODEL.UserInfo>();
                    if (loginer == null || loginer.userInfo == null)
                    {
                        Response.Redirect("../login.aspx?action=shortcut&url=" + HttpUtility.UrlEncode(mQSurl_value));
                    }
                    else
                    {

                        if (uid == loginer.userInfo.u_id)
                        {
                            var tempLink = GenearetLink(uid, manageormy, leaveorclot, requestid);
                            if (!string.IsNullOrEmpty(tempLink))
                            {
                                Response.Redirect(tempLink, true);
                            }
                            else
                            {
                                emptyRecord();
                            }
                        }
                        else
                        {
                            this.lt_js.Text = "<script>confirmloginout('此信息不屬於此賬戶，是否退出並重新登錄？ The record is not belong current account,do you want to logout and login with other accout.?','" + HttpUtility.UrlEncode(mQSurl_value) + "')</script>";
                        }

                    }
                }
                else
                {
                    invalidLink();
                }
            }
            else
            {
                invalidLink();
            }
        }

        private string GenerateJSFun()
        {
            StringBuilder temp = new StringBuilder();
            temp.AppendLine("<script>");

            temp.AppendLine("</script>");
            return temp.ToString();

        }

        private void invalidLink()
        {
            this.lt_js.Text = LSLibrary.JavasScriptHelper.AlertMessage_js("invalid link 無效鏈接.");
        }

        private void emptyRecord()
        {
            this.lt_js.Text = LSLibrary.JavasScriptHelper.AlertMessage_js("Recrod does not exist. 不存在此信息.");
        }

        private string GenearetLink(int uid,int manageOrMy,int leaveOrclot, int requestid)
        {
            BLL.GlobalVariate.LeaveBigRangeStatus pendingorhistory = BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval;
            bool checkRequestBelongUid = true;

            int personid = 0;
            var personinfo = BLL.User_wsref.GetPersonBaseInfoByUid(uid);
            if (personinfo != null && personinfo.Count()>= 1)
            {
                personid = personinfo[0].p_id;
            }

            List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> leave_wait = null;
            List<WebServiceLayer.WebReference_leave.LeaveRequestMaster> leave_history = null;

            List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> clot_wait = null;
            List<WebServiceLayer.WebReference_leave.StaffCLOTRequest> clot_history = null;


            if (leaveOrclot == 0)
            {
                if (manageOrMy == 0)//manage leave
                {
                    leave_wait = BLL.Leave.GetMyManageLeaveMasterByRequestid(uid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, requestid);
                    leave_history = BLL.Leave.GetMyManageLeaveMasterByRequestid(uid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, requestid);

                    if (leave_wait == null || leave_wait.Count() == 0)
                    {
                        pendingorhistory = BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait;
                    }
                    if (pendingorhistory == BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait && (leave_history == null || leave_history.Count() == 0))
                    {
                        checkRequestBelongUid = false;
                    }
                }
                else//my leave
                {
                    leave_wait = BLL.Leave.GetMyLeaveMasterByRequestID(personid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, requestid);
                    leave_history = BLL.Leave.GetMyLeaveMasterByRequestID(personid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, requestid);

                    if (leave_wait == null || leave_wait.Count() == 0)
                    {
                        pendingorhistory = BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait;
                    }
                    if (pendingorhistory == BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait && (leave_history == null || leave_history.Count() == 0))
                    {
                        checkRequestBelongUid = false;
                    }
                }
            }
            else
            {
                if (manageOrMy == 0)
                {
                    clot_wait = BLL.CLOT.GetMyManageClOTByRequestid(uid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, requestid);
                    clot_history = BLL.CLOT.GetMyManageClOTByRequestid(uid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, requestid);
                }
                else
                {
                    clot_wait = BLL.CLOT.GetMyClOTByRequestidUID(uid, BLL.GlobalVariate.LeaveBigRangeStatus.waitapproval, requestid);
                    clot_history = BLL.CLOT.GetMyClOTByRequestidUID(uid, BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait, requestid);
                }
                

                if (clot_wait == null || clot_wait.Count() == 0)
                {
                    pendingorhistory = BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait;
                }

                if (pendingorhistory == BLL.GlobalVariate.LeaveBigRangeStatus.beyongdWait && (clot_history == null || clot_history.Count() == 0))
                {
                    checkRequestBelongUid = false;
                }
            }


            if (checkRequestBelongUid)
            {
                //Response.Redirect("~/pages/approval_wait.aspx?action=0&applicationtype=0&from=0");
                string tempurl = "approval_wait.aspx?action={0}&applicationtype={1}&from={2}&requestid={3}";
                tempurl = string.Format(tempurl, manageOrMy, (int)pendingorhistory, leaveOrclot, requestid);
                return tempurl;
            }
            else
            {
                return "";
            }

            
            
        }
    }
}