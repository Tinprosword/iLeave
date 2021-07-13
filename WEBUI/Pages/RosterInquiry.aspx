<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="RosterInquiry.aspx.cs" Inherits="WEBUI.Pages.RosterInquiry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <ul id="myTabRoster" fixname="mytab"   class="nav nav-tabs lsf-clearPadding" runat="server">
	        <li id="myTabRoster_leawve" runat="server"><a style="padding-top:5px; padding-bottom:3px;" data-toggle="tab" onclick="window.location.href='rosterinquiry.aspx?action=0'"><asp:Literal ID="lt_new" runat="server" Text="Leave"></asp:Literal></a></li>
	        <li id="myTabRoster_roster" runat="server"><a style="padding-top:4px; padding-bottom:4px;" data-toggle="tab" onclick="window.location.href='rosterinquiry.aspx?action=1'"><asp:Literal ID="lt_mypending" runat="server" Text="Roster"/></a></li>
        </ul>
    </div>
    <div class="row" id="divSearch" runat="server">
        <div class ="col-xs-12" style="height:2px; padding:0px">&nbsp</div>
        <table class="col-xs-12 lsu-tablem1">
                <tr>
                    <td style="width:90px"><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                    <td colspan="3"><asp:TextBox ID="tb_name" runat="server"></asp:TextBox><asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click" /></td>
                </tr>
                <tr>
                    <td style="width:90px"><asp:Literal ID="lt_dateFrom" runat="server" Text="Date From" /></td>
                    <td style="width:90px"><asp:TextBox ID="tb_dateFrom" runat="server"  Width="100%"></asp:TextBox></td>
                    <td style="width:90px"><asp:Literal ID="lt_dateTo" runat="server" Text="Date To"/></td>
                    <td style="width:90px"><asp:TextBox ID="tb_dateTo" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Literal ID="Literal1" runat="server" Text="Zone"/></td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Text="abc" Value="abc"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td><asp:Literal ID="Literal2" runat="server" Text="Position"/></td>
                    <td><asp:DropDownList ID="DropDownList2" runat="server">
                            <asp:ListItem Text="abc" Value="abc"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
    </div>
    <div class="row" id="DivLeaveTab" runat="server">
        <asp:Repeater ID="rp_leave" runat="server">
            <ItemTemplate>leave</ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="row" id="DivRosterTab" runat="server" visible="false">
        <asp:Repeater ID="rp_roster" runat="server">
            <ItemTemplate>roster</ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>