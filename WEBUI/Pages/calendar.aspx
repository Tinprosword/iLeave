<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="calendar.aspx.cs" Inherits="WEBUI.Pages.calendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-2">Myself</div><div class="col-xs-2">Team</div><div class="col-xs-7"><asp:CheckBox ID="cb_leave" runat="server" Text="假期" />&nbsp&nbsp&nbsp<asp:CheckBox ID="cb_holiday" runat="server" Text="更期" /></div>
        <div class="col-xs-12">Unit A</div>
        <div class="col-xs-12 lsf-center">
            <asp:Calendar ID="Calendar1" runat="server" Width="330px" Height="360px" Font-Size="Larger"></asp:Calendar>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>