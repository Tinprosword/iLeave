<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="myDetail.aspx.cs" Inherits="WEBUI.Pages.myDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <table class="col-xs-12 lsu-table-xs">
            <tr>
                <td style="width:100px">Applier</td>
                <td></td>
            </tr>
            <tr>
                <td>Total days</td>
                <td></td>
            </tr>
            <tr>
                <td>Status</td>
                <td>
                </td>
            </tr>
            <tr>
                <td>Leave type</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Balance</td>
                <td>
                </td>
            </tr>
            <tr>
                <td>Leave date</td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td>Remarks</td>
                <td></td>
            </tr>
        </table>

        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-3">Data</td><td class="col-xs-5">Type</td><td class="col-xs-3">Section</td><td class="col-xs-1">  </td></tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="height:122px; overflow:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                <asp:Repeater ID="repeater_leave" runat="server">
                    <ItemTemplate>
                        <tr><td class="col-xs-3"><%# ((MODEL.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((MODEL.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((MODEL.Apply.LeaveData)Container.DataItem).section %></td><td class="col-xs-1"><asp:ImageButton ID="delete" Width="18px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="18px" ImageUrl="~/Res/images/close1.png" runat="server" OnClick="delete_Click" /><asp:HiddenField ID="testhidden" runat="server" Value="<%#((MODEL.Apply.LeaveData)Container.DataItem).type %>" /></td></tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:aliceblue"><td class="col-xs-3"><%# ((MODEL.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((MODEL.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((MODEL.Apply.LeaveData)Container.DataItem).section %></td><td class="col-xs-1"><asp:ImageButton ID="delete" Width="18px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="18px" ImageUrl="~/Res/images/close1.png" runat="server" OnClick="delete_Click" /><asp:HiddenField ID="testhidden" runat="server" Value="<%#((MODEL.Apply.LeaveData)Container.DataItem).type %>" /></td></tr>
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
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>