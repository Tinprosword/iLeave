<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WEBUI.Pages.Test" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    
<form id="form1" runat="server">
        
<style type="text/css">
/* The Modal (background) */
.modalWindow
{
    display:none;
    position: fixed; /* Stay in place */
    z-index: 1; /* Sit on top */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgba(0,0,0,0.5); /* Black w/ opacity */
}
.modalContent
{
    width:100%;
    height:95%;
    background-color:white;
    position:absolute;
    bottom:0px;
    left:0px;
}
</style>

<h2>模态框</h2>

<!-- 显示模态框 -->
<input type="button" onclick="ShowDivByfixname('myModalee')" value="打开模态框" />
<input type="button" onclick="ShowDivByfixname('myModalee2')" value="打开模态框" />

<!-- 模态框 -->
<div  class="modalWindow" fixname="myModalee">
    <div class="modalContent">
        <div onclick="HiddenDivByfixname('myModalee')" style="color:red;font-size:20px; float:right">x</div>
        abc
    </div>
</div>

<div  class="modalWindow" fixname="myModalee2">
    <div class="modalContent" style="height:80%">
        <div onclick="HiddenDivByfixname('myModalee2')" style="color:red;font-size:20px; float:right">x</div>
        ehg
    </div>
</div>

<script src="../Res/jquery/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">



function ShowDivByfixname(windowname)
{
    var winabc = $("div[fixname='"+windowname+"']");
    winabc.show();
    return;
}

// When the user clicks on <span> (x), close the modal
function HiddenDivByfixname(windowname)
{
    var winabc = $("div[fixname='"+windowname+"']");
    winabc.hide();
    return;
}

</script>
    </form>
</body>
</html>