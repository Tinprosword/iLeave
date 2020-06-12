<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="myDetail.aspx.cs" Inherits="WEBUI.Pages.myDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <table class="col-xs-12 lsu-table">
            <tr>
                <td style="width:100px">Name</td>
                <td>黄景田 json</td>
            </tr>
            <tr>
                <td>Status</td>
                <td><asp:Label ID="Label_status" runat="server" Text="Waiting for approval"/></td>
            </tr>
            <tr>
                <td>Leave</td>
                <td>SL</td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_apply" runat="server">Apply</asp:Literal></td>
                <td>
                    <asp:label ID="lt_applydays" runat="server" Width="80px">0 Days</asp:label>
                    <asp:label ID="lt_balance" runat="server" Width="80px">Banlance</asp:label>
                    <asp:label ID="lt_balancedays" runat="server" Width="80px">9.2 Days</asp:label>

                </td>
            </tr>
            <tr>
                <td>Date</td>
                <td>
                    
                    2015-01-01 -&gt;2015-01-05</td>
            </tr>
            <tr>
                <td>Remarks</td>
                <td>I need to see doctor.</td>
            </tr>
        </table>

        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-3">Date</td><td class="col-xs-5">Type</td><td class="col-xs-3">Section</td></tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="height:200px; overflow:scroll;">
            <table class="col-xs-12 lsu-table-sm">
                <asp:Repeater ID="repeater_leave" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <tr><td class="col-xs-3"><%# ((MODEL.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((MODEL.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((MODEL.Apply.LeaveData)Container.DataItem).section %></td><asp:HiddenField ID="testhidden" runat="server" Value="<%#((MODEL.Apply.LeaveData)Container.DataItem).type %>" /></td></tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:aliceblue"><td class="col-xs-3"><%# ((MODEL.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((MODEL.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((MODEL.Apply.LeaveData)Container.DataItem).section %></td><asp:HiddenField ID="testhidden" runat="server" Value="<%#((MODEL.Apply.LeaveData)Container.DataItem).type %>" /></td></tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-12">Pic Refence</td></tr>
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
        <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
            <asp:Button ID="button_apply" runat="server" Text="Cancel"  CssClass="btn lss-btncolor-blue" Width="160px" OnClick="button_apply_Click"/>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>