<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="myapplications.aspx.cs" Inherits="WEBUI.Pages.myapplications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>

    <div class="row">
        
        <div class ="col-xs-4" style=" padding-left:5px;padding-right:0px"><asp:Button ID="btn_wait" runat="server" Text="Wait for approval"  Width="100%" Height="42px" CssClass="btnBox btnBlueBoxSelect" OnClick="btn_wait_Click" /></div>
        <div class ="col-xs-3" style="padding:0px;"><asp:Button ID="btn_approved" runat="server" Text="Approved"  Width="100%" Height="42px" CssClass="btnBox btnBlueBoxUnSelect" OnClick="btn_approved_Click" /></div>
        <div class ="col-xs-5" style="padding:0px; padding-right:5px;"><asp:Button ID="btn_rejectWith" runat="server" Text="Rejected/Withdrawed"  Width="100%" Height="42px" CssClass="btnBox btnBlueBoxUnSelect" OnClick="btn_rejectWith_Click" /></div>
    </div>
    <div class="row" style="padding-bottom:10px;margin-top:10px;">
        <div class="col-xs-12;" style="padding-left:7px;">
            <table>
                <tr>
                    <td colspan="3" style="padding-left:0px;padding-right:5px;"><asp:RadioButton ID="RadioButton2" runat="server" Text="My Application"  GroupName="listgroup" Checked="true" Font-Bold="false"/></td>
                </tr>
                <tr style="line-height:5px"><td colspan="3"> </td></tr>
                <tr>
                    <td style="padding-left:0px;padding-right:5px;"><asp:RadioButton ID="RadioButton1" runat="server" Text="MyTeam"  GroupName="listgroup" Font-Bold="false"/></td>
                    <td style="padding-left:0px;padding-right:5px;"><asp:Literal ID="lt_name" runat="server">name</asp:Literal></td><td><asp:TextBox ID="tb_name" runat="server" fixname="searchName" Width="86px"  OnTextChanged="tb_name_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                    <td style="padding-left:15px;padding-right:5px;">
                        <asp:Literal ID="ltdatefrom" runat="server">From</asp:Literal></td><td><asp:TextBox ID="tb_date"  data-date-format="yyyy-mm-dd" fixname="datefrom" runat="server" Width="86px" OnTextChanged="tb_date_TextChanged1" AutoPostBack="true"></asp:TextBox>
                  
                    </td>
                </tr>
                <tr style="line-height:5px"><td colspan="3"> </td></tr>
            </table>
        </div>
    </div>
    <div class="row">
        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-4"><asp:Literal ID="lt_listdate" runat="server">User</asp:Literal></td><td class="col-xs-7"><asp:Literal ID="lt_listtype" runat="server">Date</asp:Literal></td><td class="col-xs-1"></td></tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="height:400px; overflow:scroll;padding-left:5px;padding-right:0px">
            <table class="col-xs-12 lsu-table-xs" style="font-size:15px">
                <asp:Repeater ID="repeater_myapplications" runat="server">
                    <ItemTemplate>
                        <tr style="height:42px"><td class="col-xs-4" style="padding:0px"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).uname %></td><td class="col-xs-7" style="padding:0px"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).leavefrom.ToString("MM-dd")%>-><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).leaveto.ToString("MM-dd")%> (<%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).totaldays %>)</td><td class="col-xs-1" style="padding:0px"><asp:LinkButton ID="lb" OnClick="lb_Click" runat="server" CommandArgument="<%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID %>" style="font-size:24px">></asp:LinkButton></td></tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="height:42px ; background-color:aliceblue"><td class="col-xs-4" style="padding:0px"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).uname %></td><td class="col-xs-7" style="padding:0px"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).leavefrom.ToString("MM-dd")%>-><%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).leaveto.ToString("MM-dd")%> (<%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).totaldays %>)</td><td class="col-xs-1" style="padding:0px"><asp:LinkButton ID="lb" OnClick="lb_Click" runat="server" CommandArgument="<%# ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID %>" style="font-size:24px">></asp:LinkButton></td></tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>

    <div class="modalWindow" fixname="myModalee">
        <div class="modalContent">
            <div onclick="HiddenDivByfixname('myModalee')" style="color:red;font-size:20px; float:right">x</div>
        </div>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <script src="../Res/App/myapplication.js?lastmodify=<%=BLL.GlobalVariate.myapplicationjsLastmodify %>"></script>
</asp:Content>