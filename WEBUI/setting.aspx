<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setting.aspx.cs" Inherits="WEBUI.setting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="Res/App/appcss.css" rel="stylesheet"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="Res/App/autoScale.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div id="banner" class="lss-bgcolor-blue lsf-center lsf-fontsizem2" style="line-height:53px;height:53px; color:white">
                    ILeave
                </div>
            </div>
            <div class="row">
                <table class="col-xs-12 lsf-maringTop3px lsu-bigtable">
                    <tr>
                        <td style="width:130px;"><asp:Literal ID="lt_address" runat="server">Server address</asp:Literal></td>
                        <td><asp:TextBox ID="tb_address" runat="server" Width="90%"/></td>
                    </tr>
                    <tr>
                        <td>Https</td>
                        <td><asp:CheckBox ID="cb" runat="server" /></td>
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
        </div>
        </form>
        <script src="Res/jquery/jquery.min.js"></script>
        <asp:Literal ID="lt_js" runat="server"/>
        <script src="Res/App/CommonJS.js"></script>
        <script src="Res/Bootstrap/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
</body>
</html>