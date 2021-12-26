

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <%
        var all= Request.Form;
            var allkeys=all.Keys;

            for (int i = 0; i < allkeys.Count; i++)
            {
                this.Label1.Text += all.Keys[i].ToString() + "  :  " + all[i].ToString();
            }
        %>

</body>
</html>