<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sign.aspx.cs" Inherits="WEBUI.Sign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="Res/App/appcss.css" rel="stylesheet" id="appcss" runat="server"/>
    <script src="Res/App/autoScale.js?lasttime=<%=BLL.GlobalVariate.autoscalejsLastmodify %>"></script>
    <title></title>
</head>
<body >
    <form id="form1" runat="server" style="background-color:#588da7;">
        <div style="height:300px"> </div>
        <div class="col-xs-12" style="text-align:center; font-size:30px;font-weight:300; background-color:#588da7; color:white">Sign in</div>
        <div class="col-xs-12" style="text-align:center;background-color:#588da7;">
            <asp:TextBox ID="tb_Address" runat="server" Width="80%" Height="30px" Font-Size="15px" style="padding-top:4px">192.168.19.210:8091</asp:TextBox>
        </div>
        <div class="col-xs-12" style="text-align:center; padding-top:10px;background-color:#588da7;">
            <asp:Button ID="Button1" runat="server" Text="Connect" Height="45px" Width="60%" Font-Size="Larger" BackColor="#588da7" ForeColor="white" BorderColor="White" OnClick="Button1_Click" />
        </div>
        <div class="col-xs-12" style="background-color:#588da7; height:200px"></div>
    </form>
    <script src="Res/jquery/jquery.min.js"></script>
    <script src="Res/App/CommonJS.js"></script>
    <script src="Res/Bootstrap/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
</body>
</html>