<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="WEBUI.Pages.Setting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="background-color:#0954a7; height:585px;">
        <div style="text-align:center;  position:absolute ; bottom:100px; width:100%">
            <asp:Button ID="bt_out" runat="server" Text="Button" BackColor="#e10e51" style="vertical-align:bottom; color:white; text-align:center; border:0px solid red; font-size:20px" Width="280px" Height="40px" OnClick="bt_out_Click" />
        </div>
                <table class="col-xs-12 lsf-maringTop3px lsu-bigtable">
                    <tr style="height:1px;"><td ></td></tr>
                    <tr style="height:80px;">
                        <td style="width:25px;">&nbsp;</td>
                        <td style="width:80px;"><img src="../Res/images/settingAccount.png" class="setting_img" /></td>
                        <td class="whitea setting_link"><asp:LinkButton ID="lb_account" runat="server">Account</asp:LinkButton></td>
                    </tr>
                    <tr style="height:80px;">
                        <td>&nbsp;</td>
                        <td><img src="../Res/images/settingLanguage.png" class="setting_img" /></td>
                        <td class="whitea setting_link">
                            <asp:LinkButton ID="lb_english" runat="server" OnClick="lb_english_Click">English</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lb_cn" runat="server" OnClick="lb_english_Click">简体</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lb_trans" runat="server" OnClick="lb_english_Click">繁體</asp:LinkButton>
                        </td>
                    </tr>
                    <tr style="height:80px;">
                        <td>&nbsp;</td>
                        <td><img src="../Res/images/setting_othersetting.png" class="setting_img" /></td>
                        <td class="whitea setting_link"><asp:LinkButton ID="lb_othersetting" runat="server">Other Settings</asp:LinkButton></td>
                    </tr>
                    <tr style="height:80px;">
                        <td>&nbsp;</td>
                        <td><img src="../Res/images/setting_loginout.png" class="setting_img" /></td>
                        <td class="whitea setting_link"><asp:LinkButton ID="lb_out" runat="server" OnClick="lb_out_Click">Login Out</asp:LinkButton></td>
                    </tr>
                    <asp:Panel ID="panel_changeServer" runat="server" Visible="true">
                    <tr style="height:80px;">
                        <td>&nbsp;</td>
                        <td class="whitea setting_link"><img src="../Res/images/closewhite2.png" class="setting_img" /></td>
                        <td class="whitea setting_link"><asp:LinkButton ID="lb_changeserver" runat="server" OnClick="btn_changeserver_Click">ChangeServer</asp:LinkButton></td>
                    </tr>
                    </asp:Panel>
                </table>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <asp:Literal ID="js_webview" runat="server"></asp:Literal>
</asp:Content>