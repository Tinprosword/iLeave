<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="WEBUI.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br/>
    <br/>
    <div style="text-align:center"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/pages/main.aspx"> Home    </asp:HyperLink>Sorry.Error!</div>
    <br/>
    <br/>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>