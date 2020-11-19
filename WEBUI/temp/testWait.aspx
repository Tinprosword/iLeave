<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testWait.aspx.cs" Inherits="WEBUI.temp.testWait" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

        <asp:Literal ID="aa" runat="server"></asp:Literal>
    </form>
</body>
</html>