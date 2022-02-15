<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Check.aspx.cs" Inherits="WEBUI.Pages.Check" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Res/App/check.css" rel="stylesheet" />
    <div class="row" id="div_buttons">
        <div class ="col-xs-12" style="padding:0px; padding-top:5px;">
            <div class="col-xs-12" style="height:45px">&nbsp;</div>
            <div class="col-xs-12 MaintTextColor_new" style="font-size:34px; text-align:center;">
                <asp:Label ID="lb_day" runat="server" Text="2021-01-02"></asp:Label>
            </div>
            <div class="col-xs-12" style="height:15px">&nbsp;</div>
            <div class="col-xs-12 MaintTextColor_new" style="font-size:24px; text-align:center;">
                <asp:Label ID="lb_time" runat="server" Text="09:01 PM"></asp:Label>
            </div>
            <div class="col-xs-12" style="height:50px">&nbsp;</div>
            <div class="col-xs-12" style="text-align:center"><%-->OnClick_In--%>
                <asp:Button ID="bt_checkin" OnClick="OnClick_In" runat="server" Text="Button" style="width:260px; height:260px; border:0px red solid; border-radius:130px; background-color:#06468c; color:#ffffff;font-size:46px" />
            </div>
            <div class="col-xs-12" style="height:5px">&nbsp;</div>
            <div class="col-xs-12 MaintTextColor_new" style="text-align:center;">
                <asp:Label ID="lb_msg" runat="server" Text="打卡：09:01 PM" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
   <script>
       setInterval("document.getElementById('ContentPlaceHolder1_lb_time').innerHTML=formatDate(new Date());", 10000);

       function formatDate(date) {
  var hours = date.getHours();
  var minutes = date.getMinutes();
  var ampm = hours >= 12 ? 'PM' : 'AM';
  hours = hours % 12;
  hours = hours ? hours : 12; // the hour '0' should be '12'
  minutes = minutes < 10 ? '0'+minutes : minutes;
  var strTime = hours + ':' + minutes + ' ' + ampm;
  return  strTime;
}
    </script>
</asp:Content>