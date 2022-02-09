<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template.aspx.cs" Inherits="WEBUI.template" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="../Res/datepicker/bootstrap-datepicker-1.9.0/css/bootstrap-datepicker3.css" rel="stylesheet"/>
    <link href="../Res/App/appcss.css" rel="stylesheet" id="appcss" runat="server"/>
    <%--<script src="Res/App/autoScale.js?lasttime=<%=BLL.GlobalVariate.autoscalejsLastmodify %>"></script>--%>
    <title></title>

    <style type="text/css">

btn_scan{width:68px;height:23px;line-height:23px;display:inline-block;*display:inline;zoom:1;margin:0;*margin-right:6px;background:url(../images/btn2.png) no-repeat;background-position:0% 0%;border:0;}
btn_scan_h{background:url(../images/btn2.png) no-repeat;background-position:100% 100%;}
title .btn_file,.title8 .btn_file{width:68px;height:23px;border:0;opacity:0;filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);}
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

        <div class="container-fluid">
             <div class="row">
        <table class="col-xs-12 lsu-bigtable" style= "background-repeat:no-repeat;background-size:360px 660px; height:660px;">
            <tr>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="90%" placeholder="User Name" Style="border:none; border-bottom:2px solid #eee;outline: none;"/>

                </td>

            </tr>
            </table>

                 </div>
            </div>
     <asp:TextBox ID="tb_user" runat="server" Width="90%" placeholder="User Name" Style="border:none; border-bottom:2px solid #eee;outline: none;"/>

    </form>
    <script src="../Res/jquery/jquery.min.js"></script>
    <script src="../Res/App/CommonJS.js"></script>
    <script src="../Res/Bootstrap/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <script src="../Res/datepicker/bootstrap-datepicker-1.9.0/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
    </script>

</body>
</html>