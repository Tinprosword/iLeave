<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="WEBUI.Pages.Setting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
                <table class="col-xs-12 lsf-maringTop3px lsu-bigtable">
                    <tr>
                        <td style="width:130px;"><asp:Literal ID="lt_address" runat="server">Server address</asp:Literal></td>
                        <td><asp:Label ID="lb_serveraddress" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Https</td>
                        <td><asp:CheckBox ID="cb" runat="server" Enabled="false" /></td>
                    </tr>
                    <tr>
                        <td><asp:Literal ID="lt_language" runat="server">Language</asp:Literal></td>
                        <td>
                            <asp:RadioButtonList ID="cb_languagea" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="Engligh　" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="简体　" Value="1"></asp:ListItem>
                                <asp:ListItem Text="繁體　" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr style="height:15px;">
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="Button1" runat="server" Text="Save"  Width="120px" OnClick="Button1_Click"/></td>
                    </tr>
                </table>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>