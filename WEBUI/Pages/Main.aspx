﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WEBUI.Pages.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" id="bannerdiv" runat="server" >
    </div>
    <div class="row" style="margin-top:6px;">
        <div class="col-xs-12 lsf-center" style="margin-top:0px;">
            <asp:ImageButton ID="apply" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_A.png" Width="318px" Height="80px" OnClick="Apply_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_applyleaveabc" runat="server" OnClick="Apply_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
<%--        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="application" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_B.png" Width="318px" Height="80px" OnClick="Application_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_applicationsabc" runat="server" OnClick="Application_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>--%>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="approval" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_C.png" Width="318px" Height="80px" OnClick="Approval_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_approal" runat="server" OnClick="Approval_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="Canlendar" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_D.png" Width="318px" Height="80px" OnClick="Canlendar_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_calendarabc" runat="server" OnClick="Canlendar_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="Check" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_E.png" Width="318px" Height="80px" OnClick="Check_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_check" runat="server" OnClick="Check_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="Setting" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_F.png" Width="318px" Height="80px" OnClick="Setting_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_setting" runat="server" OnClick="Setting_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>