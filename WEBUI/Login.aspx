<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEBUI.Login" %>
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
    <form id="form1" runat="server" defaultbutton="Button1" defaultfocus="tb_user">
        <div class="container-fluid">
            <div class="row">
                <div id="banner" class="lss-bgcolor-blue lsf-center lsf-fontsizem2" style="line-height:53px;height:53px; color:white">
                    ILeave
                </div>
            </div>
            <div class="row">
                <div id="setting" class=" col-xs-6 col-xs-push-6" style="text-align:right;padding-right:10px; padding-top:5px">
                    <asp:ImageButton ID="btn_setting" runat="server" ImageUrl="Res/images/setting.gif" OnClick="Btn_setting_Click"  Width="30px" Height="30px" TabIndex="1"/>
                </div>
                <table class="col-xs-12 lsf-maringTop3px lsu-bigtable">
                    <tr>
                        <td style="width:90px;">User</td>
                        <td><asp:TextBox ID="tb_user" runat="server" Width="90%"/></td>
                    </tr>
                    <tr>
                        <td>Password</td>
                        <td><asp:TextBox ID="tb_password" runat="server" Width="90%" TextMode="Password"/></td>
                    </tr>
                    <tr style="height:15px;">
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><asp:Button ID="Button1" runat="server" Text="Login"  Width="120px" OnClick="Button1_Click"/></td>
                    </tr>
                    <tr style="height:20px;">
                        <td></td>
                        <td class="lsf-fontsizes1"><asp:CheckBox ID="cb_remember" runat="server" />Remember me</td>
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