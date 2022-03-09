<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="template2.aspx.cs" Inherits="WEBUI.template2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <img alt=""  src="../Res/images/btnok.png" height="20px" width="20px" /><ul id="myTab" class="nav nav-tabs">
	        <li ><a data-toggle="tab" >Pending</a></li>
	        <li class="active"><a data-toggle="tab" >History</a></li>
        </ul>
    temp2
    <asp:TextBox ID="tb_user" runat="server" Width="90%" placeholder="User Name" Style="border:none; border-bottom:2px solid #eee;outline: none;"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>