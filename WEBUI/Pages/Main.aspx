<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WEBUI.Pages.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" id="bannerdiv" runat="server" >
    </div>
    <div class="row" style="margin-top:4px;">
        <div class="col-xs-12 lsf-center" style="margin-top:0px;" id="menu1" runat="server">
            <asp:ImageButton ID="apply" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_A.png" CssClass="menucss" OnClick="Apply_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_applyleaveabc" runat="server" OnClick="Apply_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu7" runat="server">
            <asp:ImageButton ID="clot" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_B.png" CssClass="menucss" OnClick="clot_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_clot" runat="server" OnClick="clot_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu2" runat="server">
            <asp:ImageButton ID="approval" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_C.png" CssClass="menucss" OnClick="Approval_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_approal" runat="server" OnClick="Approval_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu3" runat="server">
            <asp:ImageButton ID="Canlendar" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_D.png" CssClass="menucss" OnClick="Canlendar_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_calendarabc" runat="server" OnClick="Canlendar_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu6" runat="server">
            <asp:ImageButton ID="RosterInquiry" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_E.png" CssClass="menucss" OnClick="RosterInquiry_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_RosterInquiry" runat="server" OnClick="RosterInquiry_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu4" runat="server">
            <asp:ImageButton ID="Check" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_E.png" CssClass="menucss" OnClick="Check_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_check" runat="server" OnClick="Check_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu_payslip" runat="server">
            <asp:ImageButton ID="Payslip" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_E.png" CssClass="menucss" OnClick="Payslip_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_payslip" runat="server" CssClass="fixLink" OnClick="Payslip_Click"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu_Taxation" runat="server">
            <asp:ImageButton ID="taxation" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_E.png" CssClass="menucss"  OnClick="taxation_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_taxation" runat="server"  CssClass="fixLink" OnClick="taxation_Click"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu5" runat="server">
            <asp:ImageButton ID="Setting" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_F.png" CssClass="menucss" OnClick="Setting_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_setting" runat="server" OnClick="Setting_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentjs" runat="server"></asp:Content>