<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="approval.aspx.cs" Inherits="WEBUI.Pages.approval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>
    <div class="row" style="padding-bottom:10px;margin-top:10px;">
        <div class="col-xs-12;" style="padding-left:7px;">
            <table>
                <tr style="line-height:5px"><td colspan="3"> </td></tr>
                <tr>
                    <td style="padding-left:1px;padding-right:5px;"><asp:Literal ID="Literal1" runat="server">Name</asp:Literal></td>
                    <td><asp:TextBox ID="tb_name"   runat="server" Width="86px"  AutoPostBack="true" OnTextChanged="tb_name_TextChanged"></asp:TextBox></td>
                    <td style="padding-left:1px;padding-right:5px; padding-left:5px"><asp:Literal ID="ltdatefrom" runat="server">From</asp:Literal></td>
                    <td><asp:TextBox ID="tb_date"  data-date-format="yyyy-mm-dd" fixname="datefrom" runat="server" Width="86px"  AutoPostBack="true" OnTextChanged="tb_date_TextChanged"></asp:TextBox></td>
                </tr>
                <tr style="line-height:5px"><td colspan="3"> </td></tr>
            </table>
        </div>
    </div>

    <div class="row">
        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr>
                <td class="col-xs-4"><asp:Literal ID="lt_listdate" runat="server">Date</asp:Literal></td>
                <td class="col-xs-3"><asp:Literal ID="lt_listuser" runat="server">User</asp:Literal></td>
                <td class="col-xs-2"><asp:Literal ID="lt_unit" runat="server">Unit</asp:Literal></td>
                <td class="col-xs-3">Type</td>
            </tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="height:400px; overflow:scroll;padding-left:1px;padding-right:0px">
            <table class="col-xs-12 lsu-table-xs" style="font-size:13px">
                <asp:Repeater ID="repeater_myapplications" runat="server">
                    <ItemTemplate>
                        <tr style="height:42px;<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>" onclick="MyPostBack('detail',<%#Eval("RequestID") %>)">
                            <td class="col-xs-4" style="font-weight:bold"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).leavefrom.ToString("yyyy-MM-dd")%>-><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).leaveto.ToString("MM-dd")%></td>
                            <td style="width:23%"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).uname %></td>
                            <td style="width:12%"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).totaldays %> D</td>
                            <td class="col-xs-3"><%#BLL.workflow.GetTypeDesc( ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).WorkflowTypeID,((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).minleaveCode,true) %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>

    <div class="modalWindow" fixname="myModalee">
        <div class="modalContent">
            <div onclick="HiddenDivByfixname('myModalee')" style="color:red;font-size:20px; float:right">x>x</div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <script src="../Res/App/approval.js?lastmodify=<%=BLL.GlobalVariate.myapprovaljsLastmodify %>"></script>
</asp:Content>