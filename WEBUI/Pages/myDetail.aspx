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
                <td><asp:Literal ID="lt_apply" runat="server">Apply</asp:Literal></td>
                <td>
                    <asp:label ID="lb_applydays" runat="server" Width="80px">0 Days</asp:label>
                    <asp:label ID="lt_balance" runat="server" Width="80px">Banlance</asp:label>
                    <asp:label ID="lb_balancedays" runat="server" Width="80px">9.2 Days</asp:label>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_date" runat="server">Date</asp:Literal></td>
                <td><asp:Label ID="lb_from" runat="server" Text="2015-01-05"/> -&gt;<asp:Label ID="lb_to" runat="server" Text="2015-01-05"/></td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_remarks" runat="server">Remarks</asp:Literal></td>
                <td><asp:Label ID="lb_remark" runat="server" Text="remarks"/></td>
            </tr>
        </table>

        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-3"><asp:Literal ID="lt_listdate" runat="server">Date</asp:Literal></td><td class="col-xs-5"><asp:Literal ID="lt_listtype" runat="server">Type</asp:Literal></td><td class="col-xs-3"><asp:Literal ID="lt_listsection" runat="server">Section</asp:Literal></td></tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="height:145px; overflow:scroll;">
            <table class="col-xs-12 lsu-table-sm">
                <asp:Repeater ID="repeater_leave" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <tr><td class="col-xs-3"><%# ((MODEL.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((MODEL.Apply.LeaveData)Container.DataItem).leavetypeDescription %></td><td class="col-xs-3"><%#((MODEL.Apply.LeaveData)Container.DataItem).sectionid %></td><asp:HiddenField ID="testhidden" runat="server" Value="<%#((MODEL.Apply.LeaveData)Container.DataItem).leavetypeid %>" /></td></tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:aliceblue"><td class="col-xs-3"><%# ((MODEL.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((MODEL.Apply.LeaveData)Container.DataItem).leavetypeid %></td><td class="col-xs-3"><%#((MODEL.Apply.LeaveData)Container.DataItem).leavetypeid %></td><asp:HiddenField ID="testhidden" runat="server" Value="<%#((MODEL.Apply.LeaveData)Container.DataItem).leavetypeid %>" /></td></tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-12"><asp:Literal ID="lt_attendance" runat="server">Attendance</asp:Literal></td></tr>
        </table>
        <div class="col-xs-12" style="height:80px;">
            <div class="col-xs-12 lsu-table-xs" style="height:78px;overflow-y:hidden; overflow-x:scroll; padding-left:5px">
                <table >
                    <tr>
                        <asp:Repeater ID="repeater_pic" runat="server">
                            <ItemTemplate>
                                <td style="padding-right:10px; width:90px"><asp:Image ID="Image" runat="server" ImageUrl="<%# ((MODEL.Apply.UploadPic)Container.DataItem).path %>"  Width="50px" Height="50px"/></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </div>
        </div>
        <asp:Panel ID="wait_useradmin" runat="server">
            <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
                <asp:Button ID="button_wait_useradmin_cancel" runat="server" Text="Cancel"  CssClass="btn lss-btncolor-blue" Width="90px" OnClick="button_apply_Click" />
                <asp:Button ID="button_wait_useradmin_approval" runat="server" Text="Approval"  CssClass="btn lss-btncolor-blue" Width="90px" OnClick="button_apply_Click"/>
                <asp:Button ID="button_wait_useradmin_reject" runat="server" Text="Reject"  CssClass="btn lss-btncolor-blue" Width="90px" OnClick="button_apply_Click"/>
            </div>
        </asp:Panel>
        <asp:Panel ID="wait_user" runat="server" Visible="false">
            <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
                <asp:Button ID="button_wait_user_cancel" runat="server" Text="Cancel"  CssClass="btn lss-btncolor-blue" Width="90px" OnClick="button_apply_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="wait_admin" runat="server" Visible="false">
            <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
                <asp:Button ID="button_wait_admin_approval" runat="server" Text="Approval"  CssClass="btn lss-btncolor-blue" Width="90px" OnClick="button_apply_Click" />
                <asp:Button ID="button_wait_admin_reject" runat="server" Text="Reject"  CssClass="btn lss-btncolor-blue" Width="90px" OnClick="button_apply_Click"/>
            </div>
        </asp:Panel>
        <asp:Panel ID="approval_user" runat="server" Visible="false">
            <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
                <asp:Button ID="button_approval_user_withdraw" runat="server" Text="Withdraw"  CssClass="btn lss-btncolor-blue" Width="90px" OnClick="button_apply_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="withdrawing_admin" runat="server">
            <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
                <asp:Button ID="button_withdrawing_admin_ok" runat="server" Text="Cancel"  CssClass="btn lss-btncolor-blue" Width="90px" OnClick="button_apply_Click"/>
                <asp:Button ID="button_withdrawing_admin_no" runat="server" Text="Cancel"  CssClass="btn lss-btncolor-blue" Width="90px" OnClick="button_apply_Click"/>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>