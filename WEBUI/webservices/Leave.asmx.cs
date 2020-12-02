using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;

namespace WEBUI.webservices
{
    /// <summary>
    /// Leave 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Leave : System.Web.Services.WebService
    {
        [WebMethod]
        public string GetLeaveDetail_html(int requestID, int leaveid, int staff, int employmentNo)
        {
            var namtype = BLL.CodeSetting.GetSystemParameter(BLL.CodeSetting.staffNameFormat);
            int intnameType = 1;
            int.TryParse(namtype, out intnameType);



            double balance = BLL.Leave.GetCleanValue(leaveid, staff, employmentNo);
            string strBalance = balance == -99999 ? "--" : balance.ToString("0.###");
            List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> detail = BLL.Leave.GetExtendLeaveDetailsByReuestID(requestID);
            List<WebServiceLayer.WebReference_leave.LeaveHistory> history = BLL.Leave.GetLeaveHistoryByRequest(requestID);

            double appSum = 0;
            for (int i = 0; i < detail.Count; i++)
            {
                appSum += BLL.GlobalVariate.sectionsUnit[detail[i].Section];
            }

            string leaveList = GenerateHtml(detail);

            string historyList = GenetratHistory(history,intnameType);

            string language_title = BLL.MultiLanguageHelper.GetLanguagePacket().approval_title;
            string language_title2 = BLL.MultiLanguageHelper.GetLanguagePacket().approval_approvalHistory;
            string language_col1 = BLL.MultiLanguageHelper.GetLanguagePacket().approval_list_column1;
            string language_col2 = BLL.MultiLanguageHelper.GetLanguagePacket().approval_list_column2;
            string language_col3 = BLL.MultiLanguageHelper.GetLanguagePacket().approval_list_column3;
            string language_balance = BLL.MultiLanguageHelper.GetLanguagePacket().approval_balance;
            string language_apply = BLL.MultiLanguageHelper.GetLanguagePacket().approval_applycount;

            string html = @"
                            <div id='showdiv' class='col-xs-12 lsf-clearPadding' style='display:none'>
	                        <table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding'>
		                        <tr class='lss-bgcolor-blue  lsf-clearPadding' style='color:white; height:24px;'>
			                        <td class='col-xs-10' style='text-align:left'>[title1]</td><td class='col-xs-1'><img src='../Res/images/close.png'  style='width:27px; height:27px' onclick='closeWindow()'/></td>
		                        </tr>
	                        </table>
	                        <div class='center-box3;' style='float:right; margin-right:5px; padding-right:0px;margin-top:5px;padding-top:0px;' ></div>
	                        <div class='col-xs-12 lsf-clearPadding'>
		                        <div style='height:2px'>&nbsp;</div>
		                        <table class='col-xs-12 lsf-clearPadding' style='margin-bottom:9px;'>
			                        <tr><td class='col-xs-4 lsf-clearPadding;' style='padding-left:4px;padding-right:1px'>[balance]</td><td style='text-align:right;width:40px;'><div id='lbbalance'>{0}</div></td><td>&nbsp;&nbsp;&nbsp;Day(s)</td></tr>
			                        <tr><td style='padding-left:4px;padding-right:1px'>[apply]</td><td style='text-align:right'><div id='lbapply'>{1}</div></td><td>&nbsp;&nbsp;&nbsp;Day(s)</td></tr>
		                        </table>
{3}
		                        <table class='col-xs-12 lsu-table-xs lsf-clearPadding'>
			                        <tr class='lss-bgcolor-blue' style='color:white; height:24px;'>
                                        <td>
                                            <table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding'><tr>
				                                <td class='col-xs-4' style='width:120px'>[col1]<asp:Literal ID='Literal1' runat='server'></asp:Literal></td>
				                                <td class='col-xs-4' style='width:120px'>[col2]<asp:Literal ID='Literal2' runat='server'></asp:Literal></td>
				                                <td class='col-xs-2' style='width:60px;text-align:right;'>[col3]<asp:Literal ID='Literal3' runat='server'></asp:Literal></td>
				                                <td class='col-xs-3'>&nbsp;</td>
                                            </tr></table>
                                        </td>
				                        <td class='col-xs-1' style='width:17px;'>&nbsp;</td>
			                        </tr>
		                        </table>
		                        <div class='col-xs-12 lsf-clearPadding' style='width:100%; height:150px; overflow-y:scroll; overflow-x:hidden;'>
			                        <table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding'>
{2}
			                        </table>
		                        </div>
	                        </div>
                        </div>";

            string result = string.Format(html, strBalance, appSum, leaveList, historyList);

            result = result.Replace("[title1]", language_title);
            result = result.Replace("[title2]", language_title2);
            result = result.Replace("[col1]", language_col1);
            result = result.Replace("[col2]", language_col2);
            result = result.Replace("[col3]", language_col3);
            result = result.Replace("[balance]", language_balance);
            result = result.Replace("[apply]", language_apply);

            return result;
        }


        private string GenerateHtml(List<WebServiceLayer.WebReference_leave.LeaveRequestDetail> models)
        {
            StringBuilder result = new StringBuilder();
            string item = @"<tr style='{3}'>
                                <td class='col-xs-4' style='width:120px'>{0}</div></td>
					            <td class='col-xs-4' style='width:120px'>{1}</td>
					            <td class='col-xs-2' style='width:60px;text-align:right;'>{2}&nbsp;&nbsp;</td>
					            <td class='col-xs-3'></td></tr>";

            for (int i = 0; i < models.Count; i++)
            {
                result.Append(string.Format(item, ((DateTime)models[i].LeaveFrom).ToString("yyyy-MM-dd"), BLL.GlobalVariate.sections[models[i].Section], BLL.GlobalVariate.sectionsUnit[models[i].Section].ToString(), BLL.Leave.SetBackgroundColor(i)));
            }
            return result.ToString();
        }





        private string GenetratHistory(List<WebServiceLayer.WebReference_leave.LeaveHistory> history,int nametype)
        {
            string result = "";
            string wraper = @"<table class='col-xs-12 lsu-table-xs4padding lsf-clearPadding' style='margin-bottom:15px;'>
			                        <tr class='lss-bgcolor-blue' style='color:white; height:24px;'>
				                        <td colspan='20' class='col-xs-12'>[title2]</td>
			                        </tr>
{0}
		                        </table>";


            string item1 = @"<tr style='height:15px;'>
				            <td colspan='3'>{5}. {0} {2} on {1}</td></tr>
				            <tr><td colspan='4'><div style='border:solid 1px #696969;padding-left:4px;margin-left:16px;margin-right:4px'>{3}</div></td></tr>";

            string item2 = @"<tr style='height:15px;'>
				            <td colspan='3'>{5}. {0} {2} {1}</td></tr>";

            if (history.Count > 0)
            {
                StringBuilder tempresult = new StringBuilder();

                for (int i = 0; i < history.Count; i++)
                {
                    var users= BLL.User_wsref.GetPersonBaseInfoByUid(history[i].uid).ToList();
                    string userName = "";
                    if (users != null && users.Count > 0)
                    {
                        MODEL.UserInfo approverInfo = new MODEL.UserInfo((int)users[0].u_id, "", users[0].p_Nickname, "", users[0].e_id, users[0].e_EmploymentNumber, users[0].s_id, users[0].s_StaffNumber, users[0].p_id, users[0].p_Surname, users[0].p_Othername, users[0].p_NameCH, 0, 0, true,false);

                        userName = approverInfo.GetDisplayName(nametype);
                    }
                    if (!string.IsNullOrEmpty(history[i].Remark))
                    {
                        tempresult.Append(string.Format(item1, userName, history[i].ApplyDate.ToString("yyyy-MM-dd"), BLL.GlobalVariate.RequestActionDesc[(BLL.GlobalVariate.ApprovalRequestStatus)(int)history[i].Status], history[i].Remark, BLL.Leave.SetBackgroundColor(i), (i + 1).ToString()));
                    }
                    else
                    {
                        tempresult.Append(string.Format(item2, userName, history[i].ApplyDate.ToString("yyyy-MM-dd"), BLL.GlobalVariate.RequestActionDesc[(BLL.GlobalVariate.ApprovalRequestStatus)(int)history[i].Status], history[i].Remark, BLL.Leave.SetBackgroundColor(i), (i + 1).ToString()));
                    }
                }
                result = string.Format(wraper, tempresult.ToString());
            }
            return result.ToString();
        }
    }
}