﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="ApplyCLOT.aspx.cs" Inherits="WEBUI.Pages.ApplyCLOT" %>
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
                    <asp:DropDownList style="height:24px" ID="ddl_leavetype" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="ddl_leavetype_SelectedIndexChanged">
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
                    </asp:DropDownList>:
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:DropDownList ID="DropDownList3" runat="server">
                    </asp:DropDownList>:
                    <asp:DropDownList ID="DropDownList4" runat="server">
                    </asp:DropDownList>
                    </td>
                    <td style="width:80px; vertical-align:bottom;padding-bottom:12px;"><asp:Button ID="btn_add" runat="server" Text="Add"  BackColor="#2573a4" ForeColor="White" BorderWidth="0" Height="34px" Font-Size="16px" Width="68px" style="border-radius:5px 5px 5px 5px" OnClick="btn_add_Click"/></td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_remarks" runat="server">Remarks</asp:Literal></td>
                <td style="vertical-align:bottom">
                    <asp:TextBox ID="tb_remarks" runat="server" Height="50px" Width="100%" TextMode="MultiLine" style="padding-bottom:0px;"></asp:TextBox>
                </td>
                <td style="width:80px; vertical-align:bottom;padding-bottom:12px;"><asp:Button ID="btn_apply" runat="server" Text="Submit" BackColor="#2573a4" ForeColor="White" BorderWidth="0" Height="34px" Font-Size="16px" Width="68px" style="border-radius:5px 5px 5px 5px" OnClick="btn_apply_Click"/></td>
            </tr>
        </table>
        <div class=" col-xs-12" style="height:16px; color:red;padding-left:15px;"><asp:Literal ID="literal_errormsga" runat="server" Visible="false"></asp:Literal></div>
        <div class=" col-xs-12" style="height:2px"></div>
        <div class="col-xs-12 lsf-clearPadding" style="height:260px; overflow-y:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                <tr class="lss-bgcolor-blue" style="color:white">
                    <td class="col-xs-3" ><asp:Literal ID="ltlistdate" runat="server">Date</asp:Literal></td>
                    <td class="col-xs-2" ><asp:Literal ID="ltlisttype" runat="server">Type</asp:Literal></td>
                    <td class="col-xs-4" ><asp:Literal ID="ltlistfromto" runat="server">Time</asp:Literal></td>
                    <td class="col-xs-2" ><asp:Literal ID="ltlisthours" runat="server">Hour(s)</asp:Literal></td>
                    <td class="col-xs-1" ></td>
                </tr>
                <asp:Repeater ID="repeater_clot" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <tr>
                        <td class="col-xs-3" ><asp:Literal ID="ltlistdate" runat="server" Text='<%#((MODEL.CLOT.CLOTItem)Container.DataItem).date.ToString("yyyy-MM-dd") %>'></asp:Literal></td>
                        <td class="col-xs-2" ><asp:Literal ID="ltlisttype" runat="server" Text='<%#((MODEL.CLOT.CLOTItem)Container.DataItem).type.ToString() %>'></asp:Literal></td>
                        <td class="col-xs-4" ><asp:Literal ID="ltlistfromto" runat="server" Text='<%#((MODEL.CLOT.CLOTItem)Container.DataItem).GetTimeRangeDesc() %>'></asp:Literal></td>
                        <td class="col-xs-2" ><asp:Literal ID="ltlisthours" runat="server" Text='<%#((MODEL.CLOT.CLOTItem)Container.DataItem).GetHours()%>'></asp:Literal></td>
                        <td style="text-align:right"><asp:ImageButton ID="delete" Width="30px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="30px" ImageUrl="~/Res/images/close.png" runat="server" OnClick="delete_Click" /></td>
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <asp:Literal ID="js_waitdiv" runat="server"/>
</asp:Content>