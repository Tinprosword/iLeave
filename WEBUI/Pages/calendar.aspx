<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="calendar.aspx.cs" Inherits="WEBUI.Pages.calendar" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-2" style="padding-right:0px"><asp:Button ID="btn_myself" runat="server" Text="Me"  Width="100%" Height="32px" CssClass="btnBox btnBlueBoxSelect" OnClick="btn_myself_Click"/></div>
        <div class="col-xs-2" style="padding-left:0px;padding-right:0px"><asp:Button ID="btn_team" runat="server" Text="Team"  Width="100%" Height="32px" CssClass="btnBox btnBlueBoxUnSelect" OnClick="btn_team_Click"  /></div>
        <div class="col-xs-7" style="padding-left:25px;height:32px;line-height:32px;"><asp:CheckBox ID="cb_leave" runat="server" Text="假期" OnCheckedChanged="cb_leave_CheckedChanged"  AutoPostBack="true"/>&nbsp&nbsp&nbsp<asp:CheckBox ID="cb_holiday" runat="server" Text="更期" OnCheckedChanged="cb_holiday_CheckedChanged" AutoPostBack="true"/></div>
        <div class="col-xs-12" style="margin-top:8px; margin-bottom:8px; font-weight:bold">Unit A</div>
        <div class="col-xs-12 lsf-center">
            <asp:Calendar ID="Calendar1" runat="server" Width="310px" Height="260px" Font-Size="Larger" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
        </div>
        <div class="col-xs-4"><asp:Image ID="Image1" runat="server" ImageUrl="~/Res/images/square.png" BackColor="Black" />&nbsp Approved</div>
        <div class="col-xs-8"><asp:Image ID="Image2" runat="server" ImageUrl="~/Res/images/square.png" BackColor="#f3e926" />&nbsp Contain wait for approval</div>

        <div class="col-xs-12 lss-color-blue" style=" font-weight:bold; font-size:18px;margin-top:15px"><asp:Label ID="Label1" runat="server" Text="lb_date">2020-01-01</asp:Label></div>
        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-2">Name</td><td class="col-xs-3">Type</td><td class="col-xs-3">Section</td><td class="col-xs-4">Status</td></tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="height:200px; overflow:scroll;">
            <table class="col-xs-12 lsu-table-sm">
                <asp:Repeater ID="repeater_leave" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <tr><td class="col-xs-2"><%# ((MODEL.Apply.LeaveData)Container.DataItem).name %></td><td class="col-xs-3"><%#((MODEL.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((MODEL.Apply.LeaveData)Container.DataItem).section %></td><td class="col-xs-4"><%#((MODEL.Apply.LeaveData)Container.DataItem).status %></td></tr>
                    </ItemTemplate>
                    
                </asp:Repeater>
            </table>
        </div>
        <div class="col-xs-12" style="height:70px"></div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server"></asp:Content>