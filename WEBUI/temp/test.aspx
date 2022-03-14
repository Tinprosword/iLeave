<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WEBUI.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <input type="button" value="向左" id="left">
   <input type="button" value="向右" id="right">
   <input type="button" value="向上" id="top">
   <input type="button" value="向下" id="bottom">
   <div>aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa<>bbbbbbbbbbbbbbbbbbbbbbbbbbbccccccccccccccccccccccc</div><br>

        <script type="text/javascript">
      $(document).ready(function(){
    	 $("#left").click(function(){
    		 var b=$("div").offset().left;
    		 b=b-100;
    		 $("div").animate({left:b+'px'});
    	 });
    	 $("#right").click(function(){
    		 var b=$("div").offset().left;
    		 b=b+100;
    		 $("div").animate({left:b+'px'});
    	 });
    	 $("#top").click(function(){
    		 var a=$("div").offset().top;
    		 a=a-100;
    		 $("div").animate({top:a+'px'});
    	 });
    	 $("#bottom").click(function(){
    		 var a=$("div").offset().top;
    		 a=a+100;
    		 $("div").animate({top:a+'px'});
    	 });
      });
   </script>
    </form>
</body>
</html>