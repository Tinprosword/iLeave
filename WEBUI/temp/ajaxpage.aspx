<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ajaxpage.aspx.cs" Inherits="WEBUI.temp.ajaxpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <input type="button" value="showtimeByAjax" onclick="testGet()"/>
            <br />
            <asp:Button ID="Button1" runat="server" Text="showtimeBydotnet" />
        </div>

        <script src="../Res/jquery/jquery.min.js"></script>
        <script>
            function hi()
            {
                MyAjax("testws/add", 3, 4);
                MyAjax("testws/gettime");
            }

            function testGet()
            {
                $.ajax({
                    type: "get",
                    url: "testaa.aspx",
                    async: true,
                    success: function (result) {
                        alert(result);
                    },
                    error: function () {
                        alert("ERROR!");
                    }
                });
            }

        </script>
    </form>
</body>
</html>
