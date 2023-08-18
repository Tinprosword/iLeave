<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="AnnouncementDetail.aspx.cs" Inherits="WEBUI.Pages.AnnouncementDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-12 divheighter" style="text-align:center; font-size:16px; font-weight:bold; padding-top:5px;padding-bottom:5px;">
            <asp:Label ID="Label1" runat="server" Text="这里的一个新的通知。"></asp:Label>
        </div>
    </div>
    <div class="row">
        <asp:Label ID="Label2" runat="server" Text="通知的内容。。通知的内容。通知的内容。通知的内容。通知的内容。通知的内容。通知的内容。通知的内容。通知的内容。"></asp:Label>
    </div>
    <asp:Repeater ID="rp_attachment" runat="server">
        <ItemTemplate>
            <div class="col-xs-12" style="">
                <asp:LinkButton ID="lb_attachment1" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#((MODEL.Announcement.Attachement)Container.DataItem).idInTable %>" OnClick="lb_attachment1_Click"  Text="<%#RP_NAMA((MODEL.Announcement.Attachement)Container.DataItem)%>"/>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>