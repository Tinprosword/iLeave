<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template.aspx.cs" Inherits="WEBUI.template" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="Res/datepicker/bootstrap-datepicker-1.9.0/css/bootstrap-datepicker3.css" rel="stylesheet"/>
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
                <div class="glyphicon bg-warning">aaa</div>

                <div class="input-group input-daterange">
       <input type="text" id="datetimepicker"/>
    </div>

            </div>
        </div>
    </form>
    <script src="Res/jquery/jquery.min.js"></script>
    
    <script src="Res/App/CommonJS.js"></script>
    <script src="Res/Bootstrap/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <script src="Res/datepicker/bootstrap-datepicker-1.9.0/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
        alert('aab');

                $('#datetimepicker').datepicker({
                    language: 'zh-CN', //语言
                    autoclose: true, //选择后自动关闭
                    clearBtn: true,//清除按钮
                    format: "yyyy-mm-dd"//日期格式
                });

    </script>

</body>
</html>