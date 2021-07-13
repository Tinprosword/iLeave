<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="RosterInquiry.aspx.cs" Inherits="WEBUI.Pages.RosterInquiry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <ul id="myTabRoster" fixname="mytab"   class="nav nav-tabs lsf-clearPadding" runat="server">
	        <li id="myTabRoster_leawve" runat="server"><a style="padding-top:5px; padding-bottom:3px;" data-toggle="tab" id="tab_leave" runat="server"><asp:Literal ID="lt_tableave" runat="server" Text="Leave"></asp:Literal></a></li>
	        <li id="myTabRoster_roster" runat="server"><a style="padding-top:4px; padding-bottom:4px;" data-toggle="tab" id="tab_roster" runat="server"><asp:Literal ID="lt_tabroster" runat="server" Text="Roster"/></a></li>
        </ul>
    </div>
    <div class="row" id="divSearch" runat="server">
        <div class ="col-xs-12" style="height:2px; padding:0px">&nbsp</div>
        <table class="col-xs-12 lsu-tablem1">
                <tr>
                    <td><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                    <td colspan="3"><asp:TextBox ID="tb_name" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click" BackColor="#2573a4" ForeColor="White" BorderWidth="0" Height="34px" Font-Size="16px" style="border-radius:5px 5px 5px 5px" /></td>
                </tr>
                <tr>
                    <td style="width:45px"><asp:Literal ID="lt_dateFrom" runat="server" Text="From" /></td>
                    <td style="width:135px"><asp:TextBox ID="tb_dateFrom" runat="server"  Width="100%"></asp:TextBox></td>
                    <td style="width:45px"><asp:Literal ID="lt_dateTo" runat="server" Text="To"/></td>
                    <td style="width:135px"><asp:TextBox ID="tb_dateTo" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><asp:Literal ID="lt_zone" runat="server" Text="Zone"/></td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="100%"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <td><asp:Literal ID="lt_position" runat="server" Text="Position"/></td>
                    <td><asp:DropDownList ID="DropDownList2" runat="server" Width="100%"></asp:DropDownList>
                    </td>
                </tr>
            </table>
    </div>
    <div class="row" id="DivLeaveTab" runat="server">
        <div class=" col-xs-12" style="height:2px"></div>
        <div class="col-xs-12 lsf-clearPadding" style="height:410px; overflow-y:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                <tr class="lss-bgcolor-blue" style="color:white">
                    <td class="col-xs-3" ><asp:Literal ID="lt_listname" runat="server"></asp:Literal></td>
                    <td class="col-xs-2"><asp:Literal ID="lt_listdate" runat="server"></asp:Literal></td>
                    <td class="col-xs-2" ><asp:Literal ID="lt_listshift" runat="server"></asp:Literal></td>
                    <td class="col-xs-2" ><asp:Literal ID="lt_listattend" runat="server"></asp:Literal></td>
                    <td class="col-xs-3" ><asp:Literal ID="lt_listRemark" runat="server"></asp:Literal></td>
                </tr>
                <asp:Repeater ID="rp_leave" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>">
                            <td rowspan="2"  style=" vertical-align:top"><%# GetNameByLanguage((WebServiceLayer.WebReference_leave.v_System_iLeave_Leave_List)Container.DataItem) %></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.v_System_iLeave_Leave_List)Container.DataItem).MM_DD %></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.v_System_iLeave_Leave_List)Container.DataItem).Shift_In %></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.v_System_iLeave_Leave_List)Container.DataItem).Attend_In %></td>
                            <td rowspan="2"   style=" vertical-align:top"><%#getRemark( ((WebServiceLayer.WebReference_leave.v_System_iLeave_Leave_List)Container.DataItem).Remark)%></td>
                        </tr>
                        <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>">
                            <td></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.v_System_iLeave_Leave_List)Container.DataItem).Shift_Out %></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.v_System_iLeave_Leave_List)Container.DataItem).Attend_Out %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="row" id="DivRosterTab" runat="server" visible="false">
        <div class=" col-xs-12" style="height:2px"></div>
        <div class="col-xs-12 lsf-clearPadding" style="height:410px; overflow-y:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                <tr class="lss-bgcolor-blue" style="color:white">
                    <td class="col-xs-3" ><asp:Literal ID="lt_list_name2" runat="server"></asp:Literal></td>
                    <td class="col-xs-2"><asp:Literal ID="lt_listdate2" runat="server"></asp:Literal></td>
                    <td class="col-xs-2" ><asp:Literal ID="lt_listshift2" runat="server"></asp:Literal></td>
                    <td class="col-xs-2" ><asp:Literal ID="lt_listattend2" runat="server"></asp:Literal></td>
                    <td class="col-xs-3" ><asp:Literal ID="lt_listremark2" runat="server"></asp:Literal></td>
                </tr>
                <asp:Repeater ID="rp_roster" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>">
                            <td rowspan="2" style=" vertical-align:top"><%# GetNameByLanguage((WebServiceLayer.WebReference_leave.v_System_iLeave_Roster_List)Container.DataItem) %></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.v_System_iLeave_Roster_List)Container.DataItem).MM_DD %></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.v_System_iLeave_Roster_List)Container.DataItem).Shift_In %></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.v_System_iLeave_Roster_List)Container.DataItem).Attend_In %></td>
                            <td rowspan="2"   style=" vertical-align:top"><%#getRemark( ((WebServiceLayer.WebReference_leave.v_System_iLeave_Roster_List)Container.DataItem).Remark)%></td>
                        </tr>
                        <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>">
                            <td></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.v_System_iLeave_Roster_List)Container.DataItem).Shift_Out %></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.v_System_iLeave_Roster_List)Container.DataItem).Attend_Out %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>