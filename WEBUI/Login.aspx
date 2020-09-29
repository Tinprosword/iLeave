﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEBUI.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Res/App/appcss.css" rel="stylesheet" id="appcss" runat="server"/>
    <script src="Res/App/autoScale.js?lasttime=<%=BLL.GlobalVariate.autoscalejsLastmodify %>"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="Button1" defaultfocus="tb_user">
        
        <div class="container-fluid">
            <div class="row">
                <table class="row" style="width:100%;padding:0px;margin:0px;">
                    <tr class=" lsf-center lsf-fontsizem2" style="background-color:black;height:40px;">
                        <td  style="width:35%">&nbsp;</td>
                        <td  style="width:30%;color:white;">ILeave</td>
                        <td  style="width:35%; font-size:16px;color:white;">
                            <asp:RadioButtonList ID="rbl_language" Height="36px" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbl_language_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Eng.</asp:ListItem>
                                <asp:ListItem Value="1">简</asp:ListItem>
                                <asp:ListItem Value="2">繁</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                     </tr>   
                </table>
                <table class="col-xs-12 lsf-maringTop3px lsu-bigtable">
                    <tr>
                        <td style="width:90px;"><asp:Literal ID="lt_user" runat="server" Text="User" /></td>
                        <td><asp:TextBox ID="tb_user" runat="server" Width="90%"/></td>
                    </tr>
                    <tr>
                        <td><asp:Literal ID="lt_password" runat="server" Text="Password" /></td>
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
                        <td class="lsf-fontsizes1">
                            <asp:CheckBox ID="cb_remember" runat="server" OnCheckedChanged="cb_remember_CheckedChanged"  AutoPostBack="true"/>&nbsp;Remember me
                        </td>
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