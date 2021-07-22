<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Taxation.aspx.cs" Inherits="WEBUI.Pages.Taxation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" id="divSearch" runat="server">
        <div class ="col-xs-12" style="height:2px; padding:0px">&nbsp</div>
        <table class="col-xs-12 lsu-tablem1">
            <tr>
                <td style="width:100px"><asp:Literal ID="lt_company" runat="server"/></td>
                <td><asp:Label ID="lb_companyName" runat="server" Text="DWSolutions"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_date" runat="server"/></td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="130px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" ></asp:DropDownList>
                </td>
            </tr>
            <tr style=" display:none">
                <td><asp:Literal ID="lt_status" runat="server"/></td>
                <td><asp:Label ID="lb_status" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top:10px; padding-left:10px"><asp:Button ID="btn_search" runat="server" Text="Download"  CssClass="CommonBlueButton" OnClick="btn_search_Click" />
                    <asp:Label ID="lb_msg" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>