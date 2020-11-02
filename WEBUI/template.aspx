<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template.aspx.cs" Inherits="WEBUI.Sign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="Res/App/appcss.css" rel="stylesheet" id="appcss" runat="server"/>
    <%--<script src="Res/App/autoScale.js?lasttime=<%=BLL.GlobalVariate.autoscalejsLastmodify %>"></script>--%>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div id="banner" class="lss-bgcolor-blue lsf-center lsf-fontsizem2" style="line-height:53px;height:53px; color:white">
                    ILeave
                </div>
                <div class="alert"></div>
            </div>
        </div>
    </form>
    <script src="Res/jquery/jquery.min.js"></script>
    <script src="Res/App/CommonJS.js"></script>
    <script src="Res/Bootstrap/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>

    <script>
     $('.alert').html('操作成功').addClass('alert-success').show().delay(1500).fadeOut();
    </script>

</body>
</html>