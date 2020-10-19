<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="myDetail.aspx.cs" Inherits="WEBUI.Pages.myDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="col-xs-12" style="height:2px; padding:0px">&nbsp</div>
    <div class="row">
        <table class="col-xs-12 lsu-table">
            <tr>
                <td style="width:100px"><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                <td><asp:Label ID="lb_name" runat="server" Text="name"/></td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_status" runat="server">Status</asp:Literal></td>
                <td><asp:Label ID="lb_status" runat="server" Text="Waiting for approval"/></td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_leave" runat="server">Leave</asp:Literal></td>
                <td><asp:Label ID="lb_leave" runat="server" Text=""/></td>
            </tr>
            <tr>
                <td><asp:label ID="lt_balance" runat="server" Width="80px">Banlance</asp:label></td>
                <td>
                    <asp:label ID="lb_balancedays" runat="server" Widths="80px">9.2 D</asp:label>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_date" runat="server">Date</asp:Literal></td>
                <td><asp:Label ID="lb_from" runat="server" Text="2015-01-05"/>&nbsp;&gt;&nbsp;<asp:Label ID="lb_to" runat="server" Text="2015-01-05"/></td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_remarks" runat="server">Remarks</asp:Literal></td>
                <td><asp:Label ID="lb_remark" runat="server" Text="remarks"/></td>
            </tr>
        </table>

        <div class="col-xs-12 lsf-clearPadding" style="height:127px; overflow-y:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                    <tr class="lss-bgcolor-blue" style="color:white;">
                        <td class="col-xs-2"><asp:Literal ID="lt_listdate" runat="server">Date</asp:Literal></td>
                        <td class="col-xs-6"><asp:Literal ID="lt_listtype" runat="server">Type</asp:Literal></td>
                        <td class="col-xs-3"><asp:Literal ID="lt_listsection" runat="server">Section</asp:Literal></td>
                    </tr>
                <asp:Repeater ID="repeater_leave" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>">
                            <td class="col-xs-2"><%#( (DateTime)((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).LeaveFrom).ToString("MM-dd")%></td>
                            <td class="col-xs-6"><%#((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).Description %></td>
                            <td class="col-xs-3"><%#BLL.GlobalVariate.sections[((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).Section] %></td></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-12"><asp:Literal ID="lt_attendance" runat="server">Attachment</asp:Literal></td></tr>
        </table>
        <div class="col-xs-12" style="height:120px;">
            <div class="col-xs-12 lsu-table-xs" style="height:100%;overflow-y:hidden; overflow-x:scroll; padding-left:5px">
                <table>
                    <tr>
                        <asp:Repeater ID="repeater_pic" runat="server">
                            <ItemTemplate>
                                <td style="padding-right:10px;padding-top:3px">
                                    <asp:Image ID="Image" runat="server" ImageUrl="<%# ((MODEL.Apply.app_uploadpic)Container.DataItem).reduceImageRelatePath %>" Width="120px" Height="70px"/>
                                    <br />
                                    <a runat="server" id="aa" href="<%# ((MODEL.Apply.app_uploadpic)Container.DataItem).bigImageRelatepath %>" target="_blank"><%# ((MODEL.Apply.app_uploadpic)Container.DataItem).GetFileName(15) %></a>
                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </div>
        </div>
        <asp:Panel ID="waitingApproval_admin" runat="server">
            <div class="col-xs-12" style="padding-top:12px; color:white; font-weight:bold">
                <asp:Button ID="button_wait_admin_approval" runat="server" Text="Approve"  CssClass="lsu-imagebtn"  OnClick="button_apply_Click" style=" float:left ;margin-left:16px; background-image:url(../res/images/btnok.png); background-size:124px 44px" Width="124px" Height="44px"/>
                <asp:Button ID="button_wait_admin_reject" runat="server" Text="Reject"  CssClass="lsu-imagebtn" OnClick="button_apply_Click" style="float:right;margin-right:16px; background-image:url(../res/images/btncancel.png); background-size:124px 44px" Width="124px" Height="44px" />
            </div>
        </asp:Panel>
        <asp:Panel ID="waitingCanceling_admin" runat="server">
            <div class="col-xs-12 " style="padding-top:12px; color:white; font-weight:bold">
                <asp:Button ID="button_Cancel_admin_approval" runat ="server" Text="Approve"  OnClick="button_apply_Click" CssClass="lsu-imagebtn"  style=" float:left ;margin-left:16px; background-image:url(../res/images/btnok.png); background-size:124px 44px" Width="124px" Height="44px"/>
                <asp:Button ID="button_Cancel_admin_Reject"   runat ="server" Text="Reject"  OnClick="button_apply_Click" CssClass="lsu-imagebtn"  style="float:right;margin-right:16px; background-image:url(../res/images/btncancel.png); background-size:124px 44px" Width="124px" Height="44px" />
            </div>
        </asp:Panel>
        <asp:Panel ID="waitingApproval_user" runat="server" Visible="false">
            <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
                <asp:Button ID="button_wait_user_Withdraw" runat="server" Text="Withdraw"  OnClick="button_apply_Click" CssClass="lsu-imagebtn"  style="float:unset;margin-right:16px; background-image:url(../res/images/btnok.png); background-size:124px 44px" Width="124px" Height="44px" />
            </div>
        </asp:Panel>
        <asp:Panel ID="approved_user" runat="server" Visible="false">
            <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
                <asp:Button ID="button_approval_user_Cancel" runat="server" Text="Cancel"  OnClick="button_apply_Click" CssClass="lsu-imagebtn"  style="float:unset;margin-right:16px; background-image:url(../res/images/btnok.png); background-size:124px 44px" Width="124px" Height="44px" />
            </div>
        </asp:Panel>

    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>