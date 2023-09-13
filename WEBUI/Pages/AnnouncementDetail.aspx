<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="AnnouncementDetail.aspx.cs" Inherits="WEBUI.Pages.AnnouncementDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-12 divheighter" style="text-align:center; font-size:16px; font-weight:bold; padding-top:5px;padding-bottom:5px;">
            <asp:Label ID="lb_title" runat="server" Text="这里的一个新的通知。"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12">
            <asp:Label ID="lb_content" runat="server" Text="通知的内容。。通知的内容。通知的内容。通知的内容。通知的内容。通知的内容。通知的内容。通知的内容。通知的内容。"></asp:Label>
        </div>
    </div>
    <div class="row">
        <asp:Repeater ID="rp_attachment" runat="server">
            <ItemTemplate>
                <div class="col-xs-12">
                    <asp:LinkButton ID="lb_attachment1" runat="server" CommandArgument="<%#((WebServiceLayer.WebReference_Ileave_Other.t_Attachment)Container.DataItem).ID %>" OnClick="lb_attachment1_Click"  Text="<%#RP_Name((WebServiceLayer.WebReference_Ileave_Other.t_Attachment)Container.DataItem,true,45)%>" ToolTip="<%#RP_Name((WebServiceLayer.WebReference_Ileave_Other.t_Attachment)Container.DataItem,false,45)%>"/>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <asp:Literal ID="LT_JSDOWNLOAD" runat="server"></asp:Literal>
</asp:Content>