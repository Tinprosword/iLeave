<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="ApplyCLOT.aspx.cs" Inherits="WEBUI.Pages.ApplyCLOT" %>
<%@ Register Src="~/Controls/CLOTTab.ascx" TagPrefix="uc1" TagName="CLOTTab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:CLOTTab runat="server" ID="CLOTTab" />
    <div class="row" id="mainpage">
        <div class ="col-xs-12" style="height:2px; padding:0px">&nbsp</div>
        <table class="col-xs-12 lsu-tablem1">
            <tr>
                <td style="width:74px"><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                <td colspan="2">
                    <div style="float:left;"><asp:Literal ID="literal_applier" runat="server"></asp:Literal></div>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_leave" runat="server">Leave</asp:Literal></td>
                <td colspan="2">
                    <asp:DropDownList style="height:24px" ID="ddl_leavetype" runat="server" Width="90%">
                        <asp:ListItem Text="CL" Value="0"></asp:ListItem>
                        <asp:ListItem Text="OT" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td><asp:Literal ID="lt_apply" runat="server">Apply</asp:Literal></td>
                <td colspan="2">
                    <asp:label ID="lt_applydays" runat="server" Width="50px"> </asp:label>
                    <asp:label ID="lt_balance" runat="server" Width="68px">Banlance</asp:label>
                    <asp:label ID="lt_balancedays" runat="server"> </asp:label>&nbsp;<asp:label Font-Size="14px" ID="lt_balancedetail" runat="server"></asp:label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="lt_date" runat="server">Date</asp:Literal>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tb_date" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_time" runat="server">Time</asp:Literal></td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Text="00" Value="0"></asp:ListItem>
                    </asp:DropDownList>:
                    <asp:DropDownList ID="DropDownList2" runat="server">
                        <asp:ListItem Text="00" Value="00"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList3" runat="server">
                        <asp:ListItem Text="00" Value="0"></asp:ListItem>
                    </asp:DropDownList>:
                    <asp:DropDownList ID="DropDownList4" runat="server">
                        <asp:ListItem Text="00" Value="00"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td style="width:80px; vertical-align:bottom;padding-bottom:12px;"><asp:Button ID="btn_add" runat="server" Text="Add"  BackColor="#2573a4" ForeColor="White" BorderWidth="0" Height="34px" Font-Size="16px" style="border-radius:5px 5px 5px 5px"/></td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_remarks" runat="server">Remarks</asp:Literal></td>
                <td style="vertical-align:bottom">
                    <asp:TextBox ID="tb_remarks" runat="server" Height="50px" Width="100%" TextMode="MultiLine" style="padding-bottom:0px;"></asp:TextBox>
                </td>
                <td style="width:80px; vertical-align:bottom;padding-bottom:12px;"><asp:Button ID="btn_apply" runat="server" Text="Submit" BackColor="#2573a4" ForeColor="White" BorderWidth="0" Height="34px" Font-Size="16px" style="border-radius:5px 5px 5px 5px"/></td>
            </tr>
        </table>
        <div class=" col-xs-12" style="height:16px; color:red;padding-left:15px;"><asp:Literal ID="literal_errormsga" runat="server" Visible="false"></asp:Literal></div>
        <div class=" col-xs-12" style="height:2px"></div>
        <div class="col-xs-12 lsf-clearPadding" style="height:260px; overflow-y:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                <tr class="lss-bgcolor-blue" style="color:white">
                    <td class="col-xs-3" style="width:18%"><asp:Literal ID="ltlistdate" runat="server"></asp:Literal></td>
                    <td class="col-xs-4" style="width:44%"><asp:Literal ID="ltlisttype" runat="server"></asp:Literal></td>
                    <td class="col-xs-4" style="width:70px"><asp:Literal ID="lt_listsection" runat="server"></asp:Literal></td>
                    <td class="col-xs-1" style="width:30px">&nbsp;</td>
                </tr>
                <asp:Repeater ID="repeater_leave" runat="server" EnableViewState="true">
                    <ItemTemplate>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>