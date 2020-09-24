﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="calendar.aspx.cs" Inherits="WEBUI.Pages.calendar" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>
    <div class="row">
        <div class="col-xs-3" style="padding-right:0px;"><asp:Button ID="btn_myself" runat="server" Text="Me"  Width="100%" Height="32px" CssClass="btnBox btnBlueBoxSelect" OnClick="btn_myself_Click"/></div>
        <div class="col-xs-3" style="padding-left:0px;padding-right:0px;"><asp:Button ID="btn_team" runat="server" Text="Team"  Width="100%" Height="32px" CssClass="btnBox btnBlueBoxUnSelect" OnClick="btn_team_Click"  /></div>
        <div class="col-xs-6" style="padding-left:15px;height:32px;line-height:32px;padding-right:0px"><asp:RadioButton ID="cb_leave" runat="server" Text="假期" OnCheckedChanged="cb_leave_CheckedChanged" GroupName="leaveroster"  Checked="true"  AutoPostBack="true"/>&nbsp&nbsp&nbsp<asp:RadioButton ID="cb_holiday" GroupName="leaveroster" runat="server" Text="更期" OnCheckedChanged="cb_holiday_CheckedChanged" AutoPostBack="true"/></div>
        <div class="col-xs-12" style="margin-top:8px; margin-bottom:8px;">
            <asp:DropDownList ID="ddlzone" runat="server"  CssClass="col-xs-7" OnSelectedIndexChanged="unit_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>&nbsp&nbsp&nbsp
            Name&nbsp<asp:TextBox Width="58px" ID="tb_name" runat="server" OnTextChanged="tb_name_TextChanged" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-xs-12 lsf-center">
            <asp:Calendar ID="Calendar1" runat="server" Width="310px" Height="250px" Font-Size="Larger" SelectedDayStyle-BorderWidth="2"  SelectedDayStyle-BorderColor="#bd4f8b" SelectedDayStyle-BackColor="White" SelectedDayStyle-ForeColor="Black"></asp:Calendar>
        </div>
        <div class="row" id="divTip" runat="server" visible="true">
            <div class="col-xs-4"><asp:Image ID="Image1" runat="server" ImageUrl="~/Res/images/square.png" BackColor="Black" />&nbsp;<asp:Literal ID="lt_approval" runat="server">approval</asp:Literal></div>
            <div class="col-xs-7"><asp:Image ID="Image2" runat="server" ImageUrl="~/Res/images/square.png" BackColor="#f3e926" />&nbsp;<asp:Literal ID="lt_wait" runat="server">wait for approval</asp:Literal></div>
        </div>
        <div class="col-xs-9 lss-color-blue" style=" font-weight:bold; font-size:18px;margin-top:15px"><asp:Label ID="Label1" runat="server" Text="lb_date">2020-04-07</asp:Label></div>
        <div class="col-xs-2 lss-color-blue" style="margin-top:10px" id="app_ok" runat="server" visible="false"><asp:ImageButton ID="ib_ok" runat="server" ImageUrl="~/Res/images/ok.png" Width="30px" Height="30px" /></div>
        <div class="col-xs-12" id="leaveDiv" runat="server" visible="true">
            <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
                <tr>
                    <td class="col-xs-3"><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                    <td class="col-xs-3"><asp:Literal ID="lt_type" runat="server">Section</asp:Literal></td>
                    <td class="col-xs-2"><asp:Literal ID="lt_section" runat="server">Type</asp:Literal></td>
                    <td class="col-xs-4"><asp:Literal ID="lt_status" runat="server">Status</asp:Literal></td>
                </tr>
            </table>
            <div class="col-xs-12 lsf-clearPadding" style="height:150px; overflow:scroll;">
                <table class="col-xs-12 lsu-table-xs">
                    <asp:Repeater ID="repeater_leave" runat="server" EnableViewState="true">
                        <ItemTemplate>
                            <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>">
                                <td class="col-xs-3"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).displayname %></td>
                                <td class="col-xs-3"><%# BLL.workflow.GetWorkFlowTypeName( ((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).WorkflowTypeID) %></td>
                                <td class="col-xs-2"><%# BLL.GlobalVariate.sections[((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).Section] %></td>
                                <td class="col-xs-4"><%# BLL.GlobalVariate.RequestDesc[(BLL.GlobalVariate.ApprovalRequestStatus)(((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).masterStatus)] %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="col-xs-12" id="rosterDiv" runat="server" visible="false">
            <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
                <tr><td class="col-xs-3"><asp:Literal ID="lt_displayname" runat="server">Name</asp:Literal></td>
                    <td class="col-xs-3"><asp:Literal ID="lt_shiftCode" runat="server">Shift</asp:Literal></td>
                    <td class="col-xs-3"><asp:Literal ID="lt_time" runat="server">Time</asp:Literal></td>
                    <td class="col-xs-3"><asp:Literal ID="lt_remark" runat="server">Remark</asp:Literal></td>
                </tr>
            </table>
            <div class="col-xs-12 lsf-clearPadding" style="height:150px; overflow:scroll;">
                <table class="col-xs-12 lsu-table-xs">
                    <asp:Repeater ID="rp_roster" runat="server" EnableViewState="true">
                        <ItemTemplate>
                            <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>">
                                <td class="col-xs-3"><%# ((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).DispayName %></td>
                                <td class="col-xs-3"><%# ((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).ShiftCode %></td>
                                <td class="col-xs-3"><%# ((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).Time %></td>
                                <td class="col-xs-3"><%# ((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).Remark %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server"></asp:Content>