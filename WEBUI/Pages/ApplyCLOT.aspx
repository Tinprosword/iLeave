<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="ApplyCLOT.aspx.cs" Inherits="WEBUI.Pages.ApplyCLOT" %>
<%@ Register Src="~/Controls/CLOTTab.ascx" TagPrefix="uc1" TagName="CLOTTab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:CLOTTab runat="server" ID="CLOTTab" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>