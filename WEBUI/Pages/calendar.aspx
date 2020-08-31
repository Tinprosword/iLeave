<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="calendar.aspx.cs" Inherits="WEBUI.Pages.calendar" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>
    <div class="row">
        <div class="col-xs-3" style="padding-right:0px;"><asp:Button ID="btn_myself" runat="server" Text="Me"  Width="100%" Height="32px" CssClass="btnBox btnBlueBoxSelect" OnClick="btn_myself_Click"/></div>
        <div class="col-xs-3" style="padding-left:0px;padding-right:0px;"><asp:Button ID="btn_team" runat="server" Text="Team"  Width="100%" Height="32px" CssClass="btnBox btnBlueBoxUnSelect" OnClick="btn_team_Click"  /></div>
        <div class="col-xs-6" style="padding-left:15px;height:32px;line-height:32px;padding-right:0px"><asp:RadioButton ID="cb_leave" runat="server" Text="假期" OnCheckedChanged="cb_leave_CheckedChanged" GroupName="leaveroster"  Checked="true"  AutoPostBack="true"/>&nbsp&nbsp&nbsp<asp:RadioButton ID="cb_holiday" GroupName="leaveroster" runat="server" Text="更期" OnCheckedChanged="cb_holiday_CheckedChanged" AutoPostBack="true"/></div>
        <div class="col-xs-12" style="margin-top:8px; margin-bottom:8px; font-weight:bold">
            <asp:DropDownList ID="ddlzone" runat="server" Width="310px" OnSelectedIndexChanged="unit_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-xs-12 lsf-center">
            <asp:Calendar ID="Calendar1" runat="server" Width="310px" Height="250px" Font-Size="Larger" SelectedDayStyle-BorderWidth="2"  SelectedDayStyle-BorderColor="#bd4f8b" SelectedDayStyle-BackColor="White" SelectedDayStyle-ForeColor="Black"></asp:Calendar>
        </div>
        <div class="col-xs-4"><asp:Image ID="Image1" runat="server" ImageUrl="~/Res/images/square.png" BackColor="Black" />&nbsp;<asp:Literal ID="lt_approval" runat="server">approval</asp:Literal></div>
        <div class="col-xs-8"><asp:Image ID="Image2" runat="server" ImageUrl="~/Res/images/square.png" BackColor="#f3e926" />&nbsp;<asp:Literal ID="lt_wait" runat="server">wait for approval</asp:Literal></div>

        <div class="col-xs-12 lss-color-blue" style=" font-weight:bold; font-size:18px;margin-top:15px"><asp:Label ID="Label1" runat="server" Text="lb_date">2020-01-01</asp:Label></div>
        <div class="col-xs-12" id="leaveDiv" runat="server" visible="true">
            <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
                <tr><td class="col-xs-2"><asp:Literal ID="lt_name" runat="server">Requestid</asp:Literal></td><td class="col-xs-3"><asp:Literal ID="lt_type" runat="server">Employment</asp:Literal></td><td class="col-xs-3"><asp:Literal ID="lt_section" runat="server">Type</asp:Literal></td><td class="col-xs-4"><asp:Literal ID="lt_status" runat="server">Status</asp:Literal></td></tr>
            </table>
            <div class="col-xs-12 lsf-clearPadding" style="height:150px; overflow:scroll;">
                <table class="col-xs-12 lsu-table-sm">
                    <asp:Repeater ID="repeater_leave" runat="server" EnableViewState="true">
                        <ItemTemplate>
                            <tr><td class="col-xs-2"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).RequestID %></td><td class="col-xs-2"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).EmploymentID %></td><td class="col-xs-4"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).WorkflowTypeID%></td><td class="col-xs-3"><%# ((BLL.GlobalVariate.ApprovalRequestStatus)(((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).masterStatus)).ToString() %></td></tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="col-xs-12" id="rosterDiv" runat="server" visible="false">
            <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
                <tr><td class="col-xs-2"><asp:Literal ID="Literal1" runat="server">Requestid</asp:Literal></td><td class="col-xs-3"><asp:Literal ID="Literal2" runat="server">Employment</asp:Literal></td><td class="col-xs-3"><asp:Literal ID="Literal3" runat="server">Type</asp:Literal></td><td class="col-xs-4"><asp:Literal ID="Literal4" runat="server">Status</asp:Literal></td></tr>
            </table>
            <div class="col-xs-12 lsf-clearPadding" style="height:150px; overflow:scroll;">
                <table class="col-xs-12 lsu-table-sm">
                    <asp:Repeater ID="rp_roster" runat="server" EnableViewState="true">
                        <ItemTemplate>
                            <tr><td class="col-xs-2"><%# ((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).DispayName %></td></tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server"></asp:Content>