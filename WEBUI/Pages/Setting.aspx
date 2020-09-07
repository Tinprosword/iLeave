<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="WEBUI.Pages.Setting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
                <table class="col-xs-12 lsf-maringTop3px lsu-bigtable">
                    <tr>
                        <td style="width:130px;"><asp:Literal ID="lt_address" runat="server">Server address</asp:Literal></td>
                        <td><asp:Label ID="lb_serveraddress" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><asp:Literal ID="lt_language" runat="server">Language</asp:Literal></td>
                        <td>
                            <asp:RadioButtonList ID="cb_languagea" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="cb_languagea_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Engligh　" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="简体　" Value="1"></asp:ListItem>
                                <asp:ListItem Text="繁體　" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="lt_language0" runat="server">退出帐户</asp:Literal>
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Res/images/exit.png"  Width="30px" OnClick="ImageButton1_Click"/>
                        </td>
                    </tr>
                </table>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <asp:Literal ID="js_webview" runat="server"></asp:Literal>
</asp:Content>