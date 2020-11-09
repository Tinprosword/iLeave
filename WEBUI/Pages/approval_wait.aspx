﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="approval_wait.aspx.cs" Inherits="WEBUI.Pages.approval_wait" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>
    <div class="row" style="padding-bottom:10px;margin-top:10px; height:23px;">
        <div class="col-xs-4" style="padding-left:15px; width:80px">
            <asp:DropDownList ID="ddl_year" runat="server" Width="60px" Height="26px" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="2018">2018</asp:ListItem>
                <asp:ListItem Value="2019">2019</asp:ListItem>
                <asp:ListItem Value="2020" Selected="True">2020</asp:ListItem>
                <asp:ListItem Value="2021">2021</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-xs-4">
            <asp:TextBox ID="tb_staff" runat="server" AutoPostBack="true"  OnTextChanged="tb_staff_TextChanged"></asp:TextBox>
        </div>
    </div>
    <div class="row" style="padding-bottom:0px;margin-top:10px; height:510px;overflow-y:scroll">
        <asp:Repeater ID="rp_list" runat="server">
            <ItemTemplate>
                <div class="col-xs-12" style=" line-height:8px;text-align:center;padding:0px;  margin:0px; padding-top:1px; padding-bottom:4px">
                    <label class="lsf-clearPadding" style="padding:0px;  margin:0px;height:1px;background-color:dimgray; width:90%; padding-left:3px; padding-right:3px;"></label></div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_name" runat="server" Font-Bold="true"><%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).uname%></asp:Label></div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_date" runat="server"><%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetFromto()%></asp:Label></div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_leave" runat="server" Text="Annual Leave Balance:2.0"></asp:Label></div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_applydate" runat="server" Text="Apply Date:2019-02-05"></asp:Label></div>
                <div class="col-xs-12 divheighter">
                    <asp:Label ID="lb_attachment" runat="server" Text="Attachment:"></asp:Label>
                    <a href="a.jpg"><%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetAttachment()%></a>
                </div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_remark" runat="server" Text="Remark"></asp:Label></div>
                <asp:Panel ID="panel_waitingApprove" runat="server" Visible="<%#BLL.GlobalVariate.testvalue %>">
                    <div class="col-xs-12">
                        <asp:TextBox ID="tb_waitapproveRemark" runat="server" TextMode="MultiLine" Width="98%" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5" style="float:left; padding-left:5px;">
                            <asp:Button ID="btn_approve_approve" runat="server" Text="Approve" style="border:2px solid #8da9cd;background-color:white; width:100px;"  CommandArgument="<%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>" />
                        </div>
                        <div class="col-xs-5" style="float:right; text-align:right;padding-left:5px;">
                            <asp:Button ID="btn_approve_reject" runat="server" Text="Reject" style="border:2px solid #cd7a7a;background-color:white;width:100px;" CommandArgument="<%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_waitingCancel" runat="server" Visible="<%#BLL.GlobalVariate.testvalue %>">
                    <div class="col-xs-12">
                        <asp:TextBox ID="tb_waitcancelRemark" runat="server" TextMode="MultiLine" Width="98%" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5" style="float:left; padding-left:5px;">
                            <asp:Button ID="btn_cancel_approve" runat="server" Text="Approve" style="border:2px solid #8da9cd;background-color:white; width:100px;"  CommandArgument="<%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>" />
                        </div>
                        <div class="col-xs-5" style="float:right; text-align:right;padding-left:5px;">
                            <asp:Button ID="btn_cancel_reject" runat="server" Text="Reject" style="border:2px solid #cd7a7a;background-color:white;width:100px;" CommandArgument="<%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>" />
                        </div>
                    </div>
                </asp:Panel>
                <div class="col-xs-12" style="height:20px;">&nbsp;</div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server"></asp:Content>