﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="approval_wait.aspx.cs" Inherits="WEBUI.Pages.approval_wait" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <ul id="myTabApproval" class="nav nav-tabs lsf-clearPadding" runat="server">
	        <li id="myTabApproval_pending" runat="server"><a style="padding-top:5px; padding-bottom:3px;" data-toggle="tab" runat="server" id="Approvalpending" onclick="window.location.href='approval_wait.aspx?action=0&applicationtype=0&from=3'"><asp:Literal ID="lt_pending" runat="server" Text="Pending"/></a></li>
	        <li id="myTabApproval_history" runat="server"><a style="padding-top:4px; padding-bottom:4px;" data-toggle="tab" runat="server" id="ApprovalHistory" onclick="window.location.href='approval_wait.aspx?action=0&applicationtype=3&from=3'"><asp:Literal ID="lt_processed" runat="server" Text="History"/></a></li>
        </ul>
        <ul id="myTabApply" class="nav nav-tabs lsf-clearPadding" runat="server">
	        <li id="myTabapply_new" runat="server"><a style="padding-top:5px; padding-bottom:3px;" data-toggle="tab" onclick="<%=showNewLink() %>"><asp:Literal ID="lt_new" runat="server" Text="New"></asp:Literal></a></li>
	        <li id="myTabapply_pending" runat="server"><a style="padding-top:4px; padding-bottom:4px;" data-toggle="tab" onclick="window.location.href='approval_wait.aspx?action=1&applicationtype=0&from=<%=from%>'"><asp:Literal ID="lt_mypending" runat="server" Text="Pending"/></a></li>
            <li id="myTabapply_history" runat="server"><a style="padding-top:4px; padding-bottom:4px;" data-toggle="tab" onclick="window.location.href='approval_wait.aspx?action=1&applicationtype=3&from=<%=from%>'"><asp:Literal ID="lt_myhistory" runat="server" Text="History"/></a></li>
        </ul>
    </div>
    <div id="ajaxContainer" class="col-xs-12 lsf-clearPadding"></div>
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>
    <div class="row" style="margin-top:10px;">
        <div class="col-xs-4" style="padding-left:15px; width:80px">
            <asp:DropDownList ID="ddl_year" runat="server"  Height="26px" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="2018">2018</asp:ListItem>
                <asp:ListItem Value="2019">2019</asp:ListItem>
                <asp:ListItem Value="2020" >2020</asp:ListItem>
                <asp:ListItem Value="2021" Selected="True">2021</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-xs-4" style="width:160px; padding-right:2px">
            <asp:TextBox ID="tb_staff" Width="100%"  runat="server"></asp:TextBox>
        </div>
        <div class="col-xs-1 lsf-clearPadding" style="width:30px;"><asp:ImageButton ID="ib_search" OnClick="ib_search_Click"  ImageUrl="~/Res/images/search.png" runat="server" Width="28px" Height="26px" /></div>
        <div class="col-xs-12" style="height:16px" id="div_error" runat="server" visible="false">
            <asp:Label ID="lb_errormsg" runat="server" class="col-xs-12" style="color:red; height:12px">hiabc</asp:Label>
        </div>
    </div>
    <div class="row" style="padding-bottom:0px;margin-top:1px; height:500px;overflow-y:scroll" id="maindata" onscroll="setScrollTop()">
        <asp:Repeater ID="rp_list" runat="server">
            <ItemTemplate>
                <div class="col-xs-12" style=" line-height:8px;text-align:center;padding:0px;  margin:0px; padding-top:1px; padding-bottom:4px" onclick="MyPostBack('detail',<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID %>)">
                    <label class="lsf-clearPadding" style="padding:0px;  margin:0px;height:1px;background-color:dimgray; width:90%; padding-left:3px; padding-right:3px;"></label>
                </div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_name" runat="server"><%#GetStaffName((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem)%></asp:Label><div style="float:right"><asp:Label ID="lb_status" runat="server"><%#GetLeaveStatus((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem)%></asp:Label>&nbsp; <a style="cursor:pointer;color:White;background-color:#2573A4;font-size:16px;height:34px; padding:4px;" onclick="SingleResult('../webservices/leave.asmx/GetLeaveDetail_html',{requestID:<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>,leaveid:<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).MinLeaveID%>,staff:<%#(int)loginer.userInfo.staffid %>,employmentNo:<%#(int)loginer.userInfo.employID%>,lan:<%#(int)BLL.MultiLanguageHelper.GetChoose()%>},'string',onGetData)"><%=BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_link_detail %></a></div></div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_date" runat="server"><%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetFromto(BLL.MultiLanguageHelper.GetLanguagePacket().approval_To,BLL.MultiLanguageHelper.GetLanguagePacket().approval_Day)%></asp:Label></div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_leave" runat="server"><%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetBalance()%></asp:Label></div>
                <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_applydate %>:<%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetApplydate()%></div>
                <div class="col-xs-12 " style="margin-bottom:4px;">
                    <%--<div class="col-xs-4 lsf-clearPadding" style="width:40px;">--%><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_applyRemark %>:<%--</div>--%><%--<div class="col-xs-8 lsf-clearPadding">--%><%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetApprovalRemark()%><%--</div>--%></div>
                <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_attachment %>:<%# GetAttachmentHtml(   ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID  )%></div>
                <asp:Panel ID="panel_admin_waitingApprove" runat="server" Visible="<%#BShow_WaitApplyPanel(GetBigRange(),((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_approverRemark %>:</div>
                    <div class="col-xs-12">
                        <asp:TextBox ID="tb_waitapproveRemark" runat="server" TextMode="MultiLine" Width="98%" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5" style="float:left; padding-left:5px;">
                            <asp:Button ID="btn_approve_approve" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_approve %>" style="border:2px solid #8da9cd;background-color:white; width:100px;" OnClick="btn_Click" CommandArgument=<%#"1|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                        <div class="col-xs-5" style="float:right; text-align:right;padding-left:5px;">
                            <asp:Button ID="btn_approve_reject" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_reject %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"2|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_admin_waitingCancel" runat="server" Visible="<%#BShow_WaitCancelPanel(GetBigRange(),((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_approverRemark %>:</div>
                    <div class="col-xs-12">
                        <asp:TextBox ID="tb_waitcancelRemark" runat="server" TextMode="MultiLine" Width="98%" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5" style="float:left; padding-left:5px;">
                            <asp:Button ID="btn_cancel_approve" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_approve %>" style="border:2px solid #8da9cd;background-color:white; width:100px;" OnClick="btn_Click"  CommandArgument=<%#"3|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                        <div class="col-xs-5" style="float:right; text-align:right;padding-left:5px;">
                            <asp:Button ID="btn_cancel_reject" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_reject %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"4|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_user_waiting" runat="server" Visible="<%#BShow_UserWaitingPanel(GetBigRange(),((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-5" style="float:left; padding-left:15px;">
                        <asp:Button ID="Button2" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_withdraw %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"5|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_user_approved" runat="server" Visible="<%#BShow_UserApprovedPanel(GetBigRange(),((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-5" style="float:left; padding-left:15px;">
                        <asp:Button ID="Button1" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_cancel %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"6|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                    </div>
                </asp:Panel>
                <div class="col-xs-12" style="height:5px;">&nbsp;</div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <asp:Literal ID="js_waitdiv" runat="server"></asp:Literal>
    <script src="../Res/App/applywait.js"></script>
    <asp:Literal ID="lt_jsscrolltop" runat="server"></asp:Literal>

    <script>
    function setScrollTop()
    {
        var topvalue = $('#maindata').scrollTop();
        setCookie("st", topvalue);
    }
    </script>
</asp:Content>