<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEBUI.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="../Res/App/appcss.css" rel="stylesheet"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script src="Res/App/autoScale.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div id="banner" class="lss-bgcolor-blue lsf-center lsf-fontsizem2" style="line-height:53px;height:53px;">
                    ILeave
                </div>
            </div>
            <div class="row">
                <div id="setting" class=" col-xs-6 col-xs-push-6" style="text-align:right;padding-right:1px;">
                    <img src="a" style="width:40px;height:40px;"/>
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
                        <td><asp:Button ID="Button1" runat="server" Text="Login"  Width="120px"/></td>
                    </tr>
                    <tr style="height:20px;">
                        <td></td>
                        <td class="lsf-fontsizes1"><asp:CheckBox ID="cb_remember" runat="server" />Remember me</td>
                    </tr>
                </table>
                <dialog></dialog>
            </div>
        </div>
        <%--<div id="banner" class="bgblue textCenter" style="line-height:80px;height:80px;vertical-align:middle;">
            <span style="font-size:30px; color:white">Leave Manager</span>
        </div>
        <div id="setting2" style="text-align:right">
            <img src="a" style="width:40px;height:40px;"/>
        </div>

        <table>
            <tr style="height:60px;font-size:23px; ">
                <td style="padding-left:46px; width:120px">User</td>
                <td style="padding-left:8px; width:360px"><asp:TextBox ID="tb_user" runat="server" Width="100%"></asp:TextBox></td>
            </tr>
            <tr style="height:60px;font-size:23px">
                <td style="padding-left:46px; width:120px">Password</td>
                <td style="padding-left:8px; width:360px"><asp:TextBox ID="tb_password" runat="server" Width="100%"></asp:TextBox></td>
            </tr>
        </table>

        <div id="bottom">
            <div id=""></div>
        </div>--%>
        </form>
    <script src="../Res/jquery/jquery.min.js"></script>
    <script src="../Res/App/onLoad.js"></script>
    <script src="../Res/App/CommonJS.js"></script>
    <script src="../Res/Bootstrap/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
</body>
</html>