<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WEBUI.Pages.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Repeater ID="rpt_data" runat="server">
            <ItemTemplate>
                <div><a href="detail.aspx?id=<%# Eval("Autoid")%>">Detail:<%# Eval("Autoid")%></a><%# Eval("employmentCode")%><%# Eval("workspotCode")%><%# Eval("workspotName")%></div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:TextBox ID="tb_employmentCode" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Logout" OnClick="Button2_Click" />
    </form>
    <script src="../Res/jquery/jquery.min.js"></script>
    <script src="../Res/Bootstrap/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
</body>
</html>