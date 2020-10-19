<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showpic.aspx.cs" Inherits="WEBUI.Pages.showpic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Close" OnClick="Button1_Click" />
            <br/>
            <asp:Image ID="Image1" runat="server" />
            
        </div>
        <asp:Literal ID="js" runat="server"></asp:Literal>
    </form>
</body>
</html>
