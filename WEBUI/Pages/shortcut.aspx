<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shortcut.aspx.cs" Inherits="WEBUI.Pages.shortcut" %>

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
    </form>
    <script>
        function confirmloginout(msg,url) {
            if (confirm(msg)) {
                window.location.href="?action=logout&url=" + url;
            }
            else {
                //window.location('main.aspx');
            }
        }
        </script>
    <asp:Literal ID="lt_js" runat="server"></asp:Literal>
</body>
</html>