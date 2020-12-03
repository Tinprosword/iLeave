﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEBUI.Pages
{
    public partial class ajax_historydetail2 : System.Web.UI.Page
    {
        private int requestID;
        private int leaveid;
        private int staff;
        private int employmentNo;
        public int nametype = 1;

        protected void Page_Load(object sender, EventArgs e)
        {// "requestID=" + requestID + "&leaveid=" + leaveid + "&staff=" + staff + "&employmentNo=" + employmentNo;

            var namtype = BLL.CodeSetting.GetSystemParameter(BLL.CodeSetting.staffNameFormat);
            int.TryParse(namtype, out nametype);

            if (!IsPostBack)
            {
                requestID = int.Parse(Request["requestID"]);
                leaveid = int.Parse(Request["leaveid"]);
                staff = int.Parse(Request["staff"]);
                employmentNo = int.Parse(Request["employmentNo"]);

                //balance
                double balance = BLL.Leave.GetCleanValue(leaveid, staff, employmentNo);
                string strBalance = balance == -99999 ? "--" : balance.ToString("0.###");
                this.lt_balance.Text = strBalance;


                //history
                List<WebServiceLayer.WebReference_leave.LeaveHistory> history = BLL.Leave.GetLeaveHistoryByRequest(requestID);
                panel_history.Visible = history.Count == 0 ? false : true;
                this.rp_history.DataSource = history;
                this.rp_history.DataBind();

                
            }
        }



        public string GetDisplayName(int uid,int nametype)
        {
            string result = "";
            var users = BLL.User_wsref.GetPersonBaseInfoByUid(uid).ToList();
            if (users != null && users.Count() > 0)
            {
                MODEL.UserName tempUserName = new MODEL.UserName(users[0].p_Surname, users[0].p_Othername, users[0].p_Nickname, users[0].p_NameCH);
                result=tempUserName.GetDisplayName(nametype);
            }

            return result;
        }

    }

}

        //            var namtype = BLL.CodeSetting.GetSystemParameter(BLL.CodeSetting.staffNameFormat);
        //            int intnameType = 1;
        //            int.TryParse(namtype, out intnameType);



        //            double balance = BLL.Leave.GetCleanValue(leaveid, staff, employmentNo);
        //            string strBalance = balance == -99999 ? "--" : balance.ToString("0.###");
        //            List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> detail = BLL.Leave.GetExtendLeaveDetailsByReuestID(requestID);
        //            List<WebServiceLayer.WebReference_leave.LeaveHistory> history = BLL.Leave.GetLeaveHistoryByRequest(requestID);

        //            double appSum = 0;
        //            for (int i = 0; i < detail.Count; i++)
        //            {
        //                appSum += BLL.GlobalVariate.sectionsUnit[detail[i].Section];
        //            }

        //            string leaveList = GenerateHtml(detail);

        //            string historyList = GenetratHistory(history,intnameType);

        //            string language_title = BLL.MultiLanguageHelper.GetLanguagePacket().approval_title;
        //            string language_title2 = BLL.MultiLanguageHelper.GetLanguagePacket().approval_approvalHistory;
        //            string language_col1 = BLL.MultiLanguageHelper.GetLanguagePacket().approval_list_column1;
        //            string language_col2 = BLL.MultiLanguageHelper.GetLanguagePacket().approval_list_column2;
        //            string language_col3 = BLL.MultiLanguageHelper.GetLanguagePacket().approval_list_column3;
        //            string language_balance = BLL.MultiLanguageHelper.GetLanguagePacket().approval_balance;
        //            string language_apply = BLL.MultiLanguageHelper.GetLanguagePacket().approval_applycount;

        //            //title
        //            string html1 = @"
        //                          <div id='showdiv' class='col-xs-12 lsf-clearPadding' style='display:none'>
        //                            <table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding'>
        //		                        <tr class='lss-bgcolor-blue  lsf-clearPadding' style='color:white; height:24px;'>
        //			                        <td class='col-xs-10' style='text-align:left'>[title1]</td><td class='col-xs-1'><img src='../Res/images/close.png'  style='width:27px; height:27px' onclick='closeWindow()'/></td>
        //		                        </tr>
        //	                        </table>
        //	                     <div class='center-box3;' style='float:right; margin-right:5px; padding-right:0px;margin-top:5px;padding-top:0px;' ></div>
        //                        <div style='height:2px'>&nbsp;</div>
        //";

        //            //balance
        //            string html2 = @"
        //                                <table class='col-xs-12 lsf-clearPadding' style='margin-bottom:9px;'>
        //			                        <tr><td class='col-xs-4 lsf-clearPadding;' style='padding-left:4px;padding-right:1px'>[balance]</td><td style='text-align:right;width:40px;'><div id='lbbalance'>{0}</div></td><td>&nbsp;&nbsp;&nbsp;Day(s)</td></tr>
        //			                        <tr><td style='padding-left:4px;padding-right:1px'>[apply]</td><td style='text-align:right'><div id='lbapply'>{1}</div></td><td>&nbsp;&nbsp;&nbsp;Day(s)</td></tr>
        //		                        </table>
        //";
        //            //history title
        //            string html3 = @"";
        //            //history list
        //            string html4 = @"{3}";
        //            //leave title
        //            string html5 = @"
        //                            <table class='col-xs-12 lsu-table-xs lsf-clearPadding'>
        //			                        <tr class='lss-bgcolor-blue' style='color:white; height:24px;'>
        //                                        <td>
        //                                            <table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding'><tr>
        //				                                <td class='col-xs-4' style='width:120px'>[col1]<asp:Literal ID='Literal1' runat='server'></asp:Literal></td>
        //				                                <td class='col-xs-4' style='width:120px'>[col2]<asp:Literal ID='Literal2' runat='server'></asp:Literal></td>
        //				                                <td class='col-xs-2' style='width:60px;text-align:right;'>[col3]<asp:Literal ID='Literal3' runat='server'></asp:Literal></td>
        //				                                <td class='col-xs-3'>&nbsp;</td>
        //                                            </tr></table>
        //                                        </td>
        //				                        <td class='col-xs-1' style='width:17px;'>&nbsp;</td>
        //			                        </tr>
        //		                        </table>
        //";
        //            //leave list
        //            string html6 = @"

        //                    <div class='col-xs-12 lsf-clearPadding' style='width:100%; height:150px; overflow-y:scroll; overflow-x:hidden;'>
        //			                        <table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding'>
        //{2}
        //			                        </table>
        //		                        </div>
        //	                        </div>
        //";

        //            string htmlAll = html1 + html2 + html3 + html4 + html5 + html6;

        //            string html = @"
        //                            <div id='showdiv' class='col-xs-12 lsf-clearPadding' style='display:none'>
        //	                        <table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding'>
        //		                        <tr class='lss-bgcolor-blue  lsf-clearPadding' style='color:white; height:24px;'>
        //			                        <td class='col-xs-10' style='text-align:left'>[title1]</td><td class='col-xs-1'><img src='../Res/images/close.png'  style='width:27px; height:27px' onclick='closeWindow()'/></td>
        //		                        </tr>
        //	                        </table>
        //	                        <div class='center-box3;' style='float:right; margin-right:5px; padding-right:0px;margin-top:5px;padding-top:0px;' ></div>

        //		                        <div style='height:2px'>&nbsp;</div>
        //		                        <table class='col-xs-12 lsf-clearPadding' style='margin-bottom:9px;'>
        //			                        <tr><td class='col-xs-4 lsf-clearPadding;' style='padding-left:4px;padding-right:1px'>[balance]</td><td style='text-align:right;width:40px;'><div id='lbbalance'>{0}</div></td><td>&nbsp;&nbsp;&nbsp;Day(s)</td></tr>
        //			                        <tr><td style='padding-left:4px;padding-right:1px'>[apply]</td><td style='text-align:right'><div id='lbapply'>{1}</div></td><td>&nbsp;&nbsp;&nbsp;Day(s)</td></tr>
        //		                        </table>
        //{3}

        //		                        <table class='col-xs-12 lsu-table-xs lsf-clearPadding'>
        //			                        <tr class='lss-bgcolor-blue' style='color:white; height:24px;'>
        //                                        <td>
        //                                            <table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding'><tr>
        //				                                <td class='col-xs-4' style='width:120px'>[col1]<asp:Literal ID='Literal1' runat='server'></asp:Literal></td>
        //				                                <td class='col-xs-4' style='width:120px'>[col2]<asp:Literal ID='Literal2' runat='server'></asp:Literal></td>
        //				                                <td class='col-xs-2' style='width:60px;text-align:right;'>[col3]<asp:Literal ID='Literal3' runat='server'></asp:Literal></td>
        //				                                <td class='col-xs-3'>&nbsp;</td>
        //                                            </tr></table>
        //                                        </td>
        //				                        <td class='col-xs-1' style='width:17px;'>&nbsp;</td>
        //			                        </tr>
        //		                        </table>

        //		                        <div class='col-xs-12 lsf-clearPadding' style='width:100%; height:150px; overflow-y:scroll; overflow-x:hidden;'>
        //			                        <table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding'>
        //{2}
        //			                        </table>
        //		                        </div>
        //	                        </div>
        //";

        //            string result = string.Format(html, strBalance, appSum, leaveList, historyList);

        //            result = result.Replace("[title1]", language_title);
        //            result = result.Replace("[title2]", language_title2);
        //            result = result.Replace("[col1]", language_col1);
        //            result = result.Replace("[col2]", language_col2);
        //            result = result.Replace("[col3]", language_col3);
        //            result = result.Replace("[balance]", language_balance);
        //            result = result.Replace("[apply]", language_apply);

        //            return result;


    //        private string GenerateHtml(List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> models)
    //        {
    //            StringBuilder result = new StringBuilder();
    //            string item = @"<tr style='{3}'>
    //                                <td class='col-xs-4' style='width:120px'>{0}</div></td>
    //					            <td class='col-xs-4' style='width:120px'>{1}</td>
    //					            <td class='col-xs-2' style='width:60px;text-align:right;'>{2}&nbsp;&nbsp;</td>
    //					            <td class='col-xs-3'></td></tr>";

    //            for (int i = 0; i < models.Count; i++)
    //            {
    //                result.Append(string.Format(item, ((DateTime)models[i].LeaveFrom).ToString("yyyy-MM-dd"), BLL.GlobalVariate.sections[models[i].Section], BLL.GlobalVariate.sectionsUnit[models[i].Section].ToString(), BLL.Leave.SetBackgroundColor(i)));
    //            }
    //            return result.ToString();
    //        }





    //        private string GenetratHistory(List<WebServiceLayer.WebReference_leave.LeaveHistory> history,int nametype)
    //        {
    //            string result = "";
    //            string wraper = @"<table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding' style='margin-bottom:2px;'>
    //			                        <tr class='lss-bgcolor-blue' style='color:white; height:24px;'>
    //				                        <td colspan='2' class='col-xs-12'>[title2]</td>
    //			                        </tr>
    //{0}
    //		                        </table>";


    //            string item1 = @"<tr style='height:22px;'>
    //                                <td>{5} </td>
    //				                <td style='width:99%'>{1}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{0}</td>
    //                            </tr>";

    //            string item1pls= "<tr style='height:28px;'><td></td><td style='padding-bottom:6px;'>Remarks:{3}</td></tr>";
    //;
    //            string item1Realtime = "";

    //            if (history.Count > 0)
    //            {
    //                StringBuilder tempresult = new StringBuilder();

    //                for (int i = 0; i < history.Count; i++)
    //                {
    //                    var users= BLL.User_wsref.GetPersonBaseInfoByUid(history[i].uid).ToList();
    //                    string userName = "";
    //                    if (users != null && users.Count > 0)
    //                    {
    //                        MODEL.UserName tempUserName = new MODEL.UserName(users[0].p_Surname, users[0].p_Othername, users[0].p_Nickname, users[0].p_NameCH);
    //                        userName = tempUserName.GetDisplayName(nametype);
    //                    }
    //                    if (!string.IsNullOrEmpty(history[i].Remark))
    //                    {
    //                        item1Realtime = item1 + item1pls;
    //                    }
    //                    else
    //                    {
    //                        item1Realtime = item1;
    //                    }
    //                    tempresult.Append(string.Format(item1Realtime, userName, history[i].ApplyDate.ToString("yyyy-MM-dd"), BLL.GlobalVariate.RequestActionDesc[(BLL.GlobalVariate.ApprovalRequestStatus)(int)history[i].Status], history[i].Remark, BLL.Leave.SetBackgroundColor(i), (i + 1).ToString()));
    //                }
    //                result = string.Format(wraper, tempresult.ToString());
    //            }
    //            return result.ToString();
    //        }
    //    }
