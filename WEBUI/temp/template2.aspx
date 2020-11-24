<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="template2.aspx.cs" Inherits="WEBUI.template2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <ul id="myTab" class="nav nav-tabs">
	        <li ><a data-toggle="tab" onclick="window.location.href='template.aspx'">Pending</a></li>
	        <li class="active"><a data-toggle="tab" onclick="window.location.href='template2.aspx'">History</a></li>
        </ul>
    temp2
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>