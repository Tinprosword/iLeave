<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Check.aspx.cs" Inherits="WEBUI.Pages.Check" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Res/App/check.css" rel="stylesheet" />
    <div class="row" id="div_buttons">
        <div class ="col-xs-12" style="padding:0px; padding-top:5px;">
            <div class="col-xs-6" style="text-align:center; padding-left:3px;margin-left:0px; padding-right:4px;">
                <asp:LinkButton ID="linkbtn_in" runat="server" CssClass="inButton" OnClick="OnClick_In"><asp:Image ID="image_in" runat="server" ImageUrl="~/Res/images/checkin-icon.png" style="vertical-align: middle;  text-align:center;  width:44px;" />&nbsp;<label style="color:white" id="label_checkin" runat="server">Check In</label></asp:LinkButton>
            </div>
            <div class="col-xs-6" style="text-align:center;  padding-left:3px;margin-left:0px; padding-right:4px;" >
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="outButton" OnClick="OnClick_Out"><asp:Image ID="image_out" runat="server" ImageUrl="~/Res/images/checkin-icon.png" style="vertical-align: middle;  text-align:center;  width:44px;" />&nbsp;<label style="color:white" id="label_checkout" runat="server">Check Out</label></asp:LinkButton>
            </div>
        </div>
        <div class ="col-xs-12" style="padding:0px; margin-top:8px;">
            <table style="margin-left:10%; margin-right:10%; width:80%">
                <tr>
                    <td style="text-align:left"><asp:ImageButton  ID="imagebtn_back" runat="server" OnClick="button_left_Click" Width="40px" Height="30px" ImageUrl="~/Res/images/back4.png"/></td>
                    <td style="text-align:center"><asp:Label ID="label_SelectedDate" runat="server" Text="Label" style="font-size:22px"></asp:Label></td>
                    <td style="text-align:right"><asp:ImageButton ID="ImageButton1" runat="server" OnClick="button_right_Click" Width="40px" Height="30px" ImageUrl="~/Res/images/go.png"/></td>
                </tr>
            </table>
        </div>
        <div class ="col-xs-12" style="padding:0px; padding-left:5px; padding-top:12px;  overflow-y:scroll; height:460px">
            <table class="col-xs-12 lsu-table-xs">
                    <tr class="lss-bgcolor-blue" style="color:white;">
                        <td class="col-xs-3"><asp:Label ID="label_title_date" runat="server">Date</asp:Label></td>
                        <td class="col-xs-2"><asp:Label ID="label_title_inout" runat="server">In/Out</asp:Label></td>
                        <td class="col-xs-7"><asp:Label ID="label_title_location" runat="server">Location</asp:Label></td>
                    </tr>
            <asp:Repeater ID="repeater_list" runat="server">
               
                <ItemTemplate>
                    <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>">
                        <td class="col-xs-3"><%# ((WebServiceLayer.WebReference_leave.AttendanceRawData)Container.DataItem).LogDateTime.ToString("hh:mm:ss") %></td>
                        <td class="col-xs-2"><%# SpecialLanguage( ((WebServiceLayer.WebReference_leave.AttendanceRawData)Container.DataItem).Type) %></td>
                        <td class="col-xs-7"><%# ((WebServiceLayer.WebReference_leave.AttendanceRawData)Container.DataItem).GpsLocationName %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>