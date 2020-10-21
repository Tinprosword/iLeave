<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="approval.aspx.cs" Inherits="WEBUI.Pages.approval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>
    <div class="row" style="padding-bottom:10px;margin-top:10px;">
        <div class="col-xs-12;" style="padding-left:7px;">
            <table>
                <tr style="line-height:5px"><td colspan="3"> </td></tr>
                <tr>
                    <td style="padding-left:1px;padding-right:5px;"><asp:Literal ID="lt_name2" runat="server">Name</asp:Literal></td>
                    <td><asp:TextBox ID="tb_name"   runat="server" Width="86px"  AutoPostBack="true" OnTextChanged="tb_name_TextChanged"></asp:TextBox></td>
                    <td style="padding-left:1px;padding-right:5px; padding-left:5px"><asp:Literal ID="ltdatefrom" runat="server">From</asp:Literal></td>
                    <td><asp:TextBox ID="tb_date"  data-date-format="yyyy-mm-dd" fixname="datefrom" runat="server" Width="86px"  AutoPostBack="true" OnTextChanged="tb_date_TextChanged"></asp:TextBox></td>
                </tr>
                <tr style="line-height:5px"><td colspan="3"> </td></tr>
            </table>
        </div>
    </div>

    <div class="row">
        <div class="col-xs-12 lsf-clearPadding" style="height:400px; overflow-y:scroll;padding-left:1px;padding-right:0px">
            <table class="col-xs-12 lsu-table-xs" style="font-size:13px">
                <tr class="lss-bgcolor-blue" style="color:white;">
                    <td style="width:43%" ><asp:Literal ID="lt_listdate" runat="server">Date</asp:Literal></td>
                    <td style="width:30%" ><asp:Literal ID="lt_listuser" runat="server">Staff</asp:Literal></td>
                    <td style="width:14%"><asp:Literal ID="lt_unit" runat="server">Unit</asp:Literal></td>
                    <td style="width:13%"><asp:Literal ID="lt_type2" runat="server">Type</asp:Literal></td>
                </tr>
                <asp:Repeater ID="repeater_myapplications" runat="server">
                    <ItemTemplate>
                        <tr style="height:42px;<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>" onclick="MyPostBack('detail',<%#Eval("RequestID") %>)">
                            <td style="font-weight:bold"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).leavefrom.ToString("yyyy-MM-dd")%> <label class="lss-color-blue" style="font-weight:bolder; font-size:16px">></label> <%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).leaveto.ToString("MM-dd")%></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).uname %></td>
                            <td><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).totaldays %> D</td>
                            <td><%#BLL.workflow.GetTypeDesc( ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).WorkflowTypeID,((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).minleaveCode,true) %></td>
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