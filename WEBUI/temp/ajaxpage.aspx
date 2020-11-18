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
            <input type="button" value="SingleResult" onclick="SingleResult('testws.asmx/HelloWorld', { a: '3', b: '4' }, 'string', justShowIt, alertstart, alertcom);"/>
            <input type="button" value="ModelsResult" onclick="ModelsResult('testws.asmx/getbook', { count: 3 }, 'book', onEachBook)"/>
            <input type="button" value="showtimeByAjaxmodel" onclick="testGetModel()"/>
            <input type="button" value="showtimeByAjaxmodel" onclick="testGetModels()"/>
            <br />
            <asp:Button ID="Button1" runat="server" Text="showtimeBydotnet" />
        </div>

        <script src="../Res/jquery/jquery.min.js"></script>
        <script>
            function alertstart()
            {
                alert('start');
            }
            function alertcom()
            {
                alert('com');
            }
            function SingleResult(url, Postdata,datatype,callfun,beforeFun,completeFun)
            {
                $.ajax({
                    type: "post",
                    url: url,
                    data: Postdata,
                    async: true,
                    beforeSend: beforeFun,//when null,do nothing.
                    complete:completeFun,//when null,do nothing.
                    success: function (result)
                    {
                        callfun($(result).find(datatype).text());
                    },
                    error: function ()
                    {}
                });
            }
            function ModelsResult(url, Postdata,rootname,eachFun,beforeFun,completeFun)
            {
                $.ajax({
                    type: "post",
                    url: url,
                    data: Postdata,
                    async: true,
                    beforeSend: beforeFun,//when null,do nothing.
                    complete:completeFun,//when null,do nothing.
                    success: function (result)
                    {
                         $(result).find(rootname).each(eachFun);//eachFun(i,obj)
                    },
                    error: function ()
                    {}
                });
            }

            function getMember(obj, memberName)
            {
                return $(obj).children(memberName).text();
            }

            function onEachBook(i,obj)
            {
                alert(getMember(obj, "id") + " . " + getMember(obj, "name") + ":" + getMember(obj, "id"));
            }

            function justShowIt(successData)
            {
                alert(successData);
            }

            function testGetint()
            {
                $.ajax({
                    type: "post",
                    url: "testws.asmx/add",
                    data: { a: 1, b: 2 },
                    async: true,
                    success: function (result)
                    {
                        alert($(result).find('int').text());
                    },
                    error: function ()
                    {
                        alert("ERROR!");
                    }
                });
            }

             function testGetstring()
            {
                $.ajax({
                    type: "post",
                    url: "testws.asmx/HelloWorld",
                    data: { a: 1, b: 2 },
                    async: true,
                    success: function (result)
                    {
                        alert($(result).find('string').text());
                    },
                    error: function ()
                    {
                        alert("ERROR!");
                    }
                });
            }

            function testGetModel()
            {
                $.ajax({
                    type: "post",
                    url: "testws.asmx/getbook",
                    data: { a: 1, b: 2 },
                    async: true,
                    success: function (result)
                    {
                        alert($(result).find('book').children("id").text() + "." + $(result).find('book').children("name").text());
                    },
                    error: function ()
                    {
                        alert("ERROR!");
                    }
                });
            }

            function testGetModels()
            {
                $.ajax({
                    type: "post",
                    url: "testws.asmx/getbooks",
                    data: { a: 1, b: 2 },
                    async: true,
                    success: mySuccess,
                    error: function ()
                    {
                        alert("ERROR!");
                    }
                });
            }

            function mySuccess(data, status)
            {
                $(data).find('book').each(myEach);
            }

            function myEach(i,obj)
            {
                alert($(obj).children("id").text() + "." + $(obj).children("name").text());
            }
        </script>
    </form>
</body>
</html>
