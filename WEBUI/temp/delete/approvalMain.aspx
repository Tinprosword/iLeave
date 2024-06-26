﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="approvalMain.aspx.cs" Inherits="WEBUI.Pages.approvalMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="apply" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_A.png" Width="318px" Height="80px" OnClick="wait" />
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white" onclick="wait"><asp:LinkButton ID="lt_applyleaveabc" runat="server" CssClass="fixLink" OnClick="wait">Wait for approval</asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="application" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_B.png" Width="318px" Height="80px" OnClick="approved" />
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white" onclick="approved"><asp:LinkButton ID="lt_applicationsabc" runat="server"  CssClass="fixLink" OnClick="approved">Approved</asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="approval" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_C.png" Width="318px" Height="80px" OnClick="reject" />
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white" onclick="reject"><asp:LinkButton ID="lt_approal" runat="server" CssClass="fixLink" OnClick="reject">Rejected</asp:LinkButton></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server"></asp:Content>