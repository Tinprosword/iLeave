<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Apply_Upload.aspx.cs" Inherits="WEBUI.Pages.Apply_Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-12">
            <div class="col-xs-3" style="width:150px"><asp:FileUpload ID="FileUpload1" runat="server"  AllowMultiple="true" Width="150px"/></div>
            <div class="col-xs-2"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Res/images/add.png" Width="30px" OnClick="ImageButton1_Click" /></div>
        </div>
        <div class="col-xs-12">
            <asp:Repeater ID="repeater_attandance" runat="server">
                <ItemTemplate>
                    <div class="col-xs-4" style="height:80px"><asp:Image ID="Image1" runat="server" ImageUrl="~/Res/images/setting.gif"  Height="80px" /></div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>