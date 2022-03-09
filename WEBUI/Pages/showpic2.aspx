<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="showpic2.aspx.cs" Inherits="WEBUI.Pages.showpic2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" id="mainpage">
        <div class ="col-xs-12" style="overflow:scroll;height:560px; width:330px;float:left;margin-left:10px;">
            <asp:Image ID="Image1" fixname="image1" isbig="0"  runat="server" style="width:99%" />
        </div>
        <div style="float:left;position:relative;top:10px;left:-15px; z-index:99;">
            <img ID="img_big"  src="../Res/images/biger.png" Width="20" Height="20" onclick="ChangeImageSize('image1','isbig')"/>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <script>
        function ChangeImageSize(imageFixName,bigAttrName)
        {
            var finname = "img[fixname='" + imageFixName + "']"
            var theImg = $(finname);
            var isbig = theImg.attr(bigAttrName);
            if (isbig=="0")
            {
                theImg.css("width", "");
                theImg.attr(bigAttrName, "1");
            }
            else
            {
                theImg.css("width", "99%");
                theImg.attr(bigAttrName, "0");
            }
        }
    </script>
</asp:Content>