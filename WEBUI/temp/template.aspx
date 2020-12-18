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
        <%-- <div class="btn_scan" onmouseout="this.className='btn_scan'" onmouseover="this.className='btn_scan btn_scan_h'"><input type="file" class="btn_file" value="浏览" /></div>--%>

<%--        <div class="col-xs-10" style="padding-top:2px">
            <span style="float:left; width:90px; position:relative;left:90px; z-index:-10;border:1px solid #808080;padding:5px;">Choose File</span>
            <asp:FileUpload ID="FileUpload1"   runat="server"   capture="camera" accept = "image/*" Style="height:50px; width:90px;float:left; opacity:0;"></asp:FileUpload>
        </div>--%>


        <%--<ul id="myTab" class="nav nav-tabs">
	        <li class="active"><a href="" data-toggle="tab" onclick="window.location.href='template.aspx'">Pending</a></li>
	        <li><a href="" data-toggle="tab" onclick="window.location.href='template2.aspx'">History</a></li>
        </ul>

        temp1--%>


        <%--<div id="showdiv" class="col-xs-10 lsf-clearPadding" style="left:9%;width:360px">
            <table class="col-xs-12 lsu-table-xs lsf-clearPadding">
                <tr class="lss-bgcolor-blue  lsf-clearPadding" style="color:white; height:24px;">
                    <td class="col-xs-10" style="text-align:left">Leave Detail</td><td class="col-xs-1"><img src="../Res/images/close.png"  style="width:27px; height:27px"/></td>
                </tr>
            </table>
            <div class="center-box3;" style="float:right; margin-right:5px; padding-right:0px;margin-top:5px;padding-top:0px;" onclick="closeWindow()"></div>
            <div class="col-xs-12 lsf-clearPadding">
                <div style="height:2px">&nbsp;</div>
                <table class="col-xs-12 lsf-clearPadding" style="margin-bottom:9px;">
                    <tr><td class="col-xs-3 lsf-clearPadding;" style="padding-left:1px;">Balance</td><td><div id="lbbalance">0</div></td></tr>
                    <tr><td style="padding-left:1px;">Leave Apply</td><td><div id="lbapply">1</div></td></tr>
                </table>
                <table class="col-xs-12 lsu-table-xs lsf-clearPadding" style="margin-bottom:15px;">
                    <tr class="lss-bgcolor-blue" style="color:white; height:24px;">
                        <td colspan="20" class="col-xs-12">Approval history</td>
                    </tr>
                    <tr style="height:20px; padding:0px; margin:0px;">
                        <td class="col-xs-3">Approver</td>
                        <td class="col-xs-3">Date</td>
                        <td class="col-xs-2">Status</td>
                        <td class="col-xs-4">Remark</td>
                    </tr>
                    <tr>
                        <td>TOM</td>
                        <td>2020-02-01</td>
                        <td>Approved</td>
                        <td>ok</td>
                    </tr>
                </table>
                <table class="col-xs-12 lsu-table-xs lsf-clearPadding">
                    <tr class="lss-bgcolor-blue" style="color:white; height:24px;">
                        <td class="col-xs-3">Date<asp:Literal ID="Literal1" runat="server"></asp:Literal></td>
                        <td class="col-xs-3">Section<asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
                        <td class="col-xs-2">Unit<asp:Literal ID="Literal3" runat="server"></asp:Literal></td>
                        <td class="col-xs-4"">&nbsp;</td>
                        <td class="col-xs-1" style="width:20px;">&nbsp;</td>
                    </tr>
                </table>
                <div class="col-xs-12 lsf-clearPadding" style="width:100%; height:150px; overflow-y:scroll; overflow-x:hidden;">
                    <table class="col-xs-12 lsu-table-xs lsf-clearPadding">
                        <tr>
                            <td class="col-xs-3">2020-02-01</td>
                            <td class="col-xs-3">Full Day</td>
                            <td class="col-xs-2">0.375</td>
                            <td class="col-xs-4"></td>
                        </tr>
                        <tr>
                            <td>2020-02-01</td>
                            <td>Full Day</td>
                            <td>0.375</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>--%>
    </form>
    <script src="../Res/jquery/jquery.min.js"></script>
    <script src="../Res/App/CommonJS.js"></script>
    <script src="../Res/Bootstrap/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
    <script src="../Res/datepicker/bootstrap-datepicker-1.9.0/js/bootstrap-datepicker.min.js"></script>

    <script type="text/javascript">
    </script>

</body>
</html>