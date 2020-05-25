<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WEBUI.Pages.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="apply" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_B.png" Width="318px" Height="82px" OnClick="apply_Click"/>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="application" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_B.png" Width="318px" Height="82px" OnClick="application_Click"/>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="approval" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_C.png" Width="318px" Height="82px" OnClick="approval_Click"/>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="roster" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_D.png" Width="318px" Height="82px" OnClick="roster_Click"/>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="money" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_E.png" Width="318px" Height="82px" OnClick="money_Click"/>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;">
            <asp:ImageButton ID="tax" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_F.png" Width="318px" Height="82px" OnClick="tax_Click"/>
        </div>
    </div>
</asp:Content>