<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEBUI.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="Res/App/appcss.css" rel="stylesheet"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <script>
        var designPaperWidth = 540;
        var _width = parseInt(window.screen.width);
        var scale = _width/designPaperWidth;
        var ua = navigator.userAgent.toLowerCase();
        var result = /android (\d+\.\d+)/.exec(ua);
        var metastr = "<meta name=\"viewport\" content=\"width=" + designPaperWidth + ",initial-scale=" + scale + ",user-scalable=no\"/>";
        if (result)
        {
            var version = parseFloat(result[1]);
            document.write(metastr);
        }
        else
        {
            document.write(metastr);
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page" class="container-fluid" style="margin-right:0px; margin-left:0px;">
            <div id="bannerrow" class="row">
                <div id="banner" class="col-sm-12 bgblue textCenter" style="line-height:80px;height:80px;vertical-align:middle;">
                    <span style="font-size:30px; color:white">Leave Manager</span>
                </div>
            </div>
            <div id="settingrow" class="row">
                <div id="setting" class="col-sm-1" style="border:1px solid blue">
                    <img src="a" style="width:40px;height:40px"/>
                </div>
                <div id="setting2" class="col-sm-1" style="border:1px solid blue">
                    <img src="a" style="width:40px;height:40px"/>
                </div>
            </div>
            <div id="inputrow" class="row">
                <div id=""></div>
            </div>
            <div id="bottomrow" class="row">
                <div id=""></div>
            </div>
        </div>
    </form>
    <script src="../Res/jquery/jquery.min.js"></script>
    <script src="../Res/Bootstrap/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
</body>
</html>