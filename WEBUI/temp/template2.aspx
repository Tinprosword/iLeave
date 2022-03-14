<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="template2.aspx.cs" Inherits="WEBUI.template2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <img alt=""  src="../Res/images/btnok.png" height="20px" width="20px" /><ul id="myTab" class="nav nav-tabs">
	        <li ><a data-toggle="tab" >Pending</a></li>
	        <li class="active"><a data-toggle="tab" >History</a></li>
        </ul>
    temp2
    <asp:TextBox ID="tb_user" runat="server" Width="90%" placeholder="User Name" Style="border:none; border-bottom:2px solid #eee;outline: none;"/>
    <br />
    <input type="button" value="向左" id="left">
   <input type="button" value="向右" id="right">
   <input type="button" value="向上" id="top">
   <input type="button" value="向下" id="bottom">
    <br />

<style>
.hiddenScrollbar_NoWrap::-webkit-scrollbar 
{
  display: none; /* Chrome Safari */
}

.hiddenScrollbar_NoWrap 
{
  scrollbar-width: none; /* firefox */
  -ms-overflow-style: none; /* IE 10+ */
  overflow-x: hidden;
  overflow-y: auto;
  white-space: nowrap;
}
</style>
 <table style="width:142px">
     <tr>
         <td style="width:20px">
             <img id="btnleft" src="../Res/images/add.png" style="width:100%; height:100%" />
         </td>
         <td style="width:102px;padding-left:1px;padding-right:1px;">
             <div id="fullf" class="hiddenScrollbar_NoWrap" style="width:100px;">
                <div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
                <div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
                <div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
                 <div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
                 <div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
                 <div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
                 <div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
                 <div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
                 <div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
                 <div style="display:inline-block;"><a href="../Res/images/adddate2.png"><img style="width:20px; height:20px;" src="../Res/images/back4.png" /></a></div>
            </div>
         </td>
         <td style="width:20px">
             <img id="btnright" src="../Res/images/add.png" style="width:100%; height:100%" />
         </td>
     </tr>
 </table>
            

    <br />
     <br />
     <br />
     <br />
   <br>

        

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">

    <script type="text/javascript">

        SetSroll("#btnleft", "#btnright", "#fullf", 20);

        function SetSroll(leftbtn, rightbtn, content, offsetleft)
        {
            var leftbtn = $(leftbtn);
            var rightbtn = $(rightbtn);

            leftbtn.click(
            function ()
            {
                var divContent = $(content);
                var sl = divContent.scrollLeft();
                sl -= offsetleft;
                divContent.scrollLeft(sl);
            }
            );

            rightbtn.click(
            function ()
            {
                var divContent = $(content);
                var sl = divContent.scrollLeft();
                sl += offsetleft;
                divContent.scrollLeft(sl);
            }
            );
        }

        
   </script>
</asp:Content>