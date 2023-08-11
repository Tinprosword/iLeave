<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Announcement.aspx.cs" Inherits="WEBUI.Pages.Announcement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <ul id="myTab" class="nav nav-tabs lsf-clearPadding modifyTab" runat="server">
	        <li id="myTab_Notice" runat="server">
                <asp:LinkButton ID="lb_notice" runat="server" OnClick="lb_notice_Click" style="padding-top:5px; padding-bottom:3px;padding-left:10px; padding-right:10px;"  Text="Notice Board"/>
	        </li>
	        <li id="myTab_policy" runat="server">
                <asp:LinkButton id="lb_policy" OnClick="lb_policy_Click" style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px;" runat="server"  Text="Policy" />
	        </li>
            <li id="myTab_Procedure" runat="server">
                <asp:LinkButton id="lb_procedure" OnClick="lb_procedure_Click" style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px;" runat="server" Text="Procedure"/>
            </li>
        </ul>
    </div>
    <div id="ajaxContainer" class="col-xs-12 lsf-clearPadding"></div>
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>
    <div class="row" style="margin-top:10px;">
        <div class="col-xs-4" style="padding-left:14px; width:74px">
            <asp:DropDownList ID="ddl_year" runat="server"  Height="26px" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </div>
    <asp:Label ID="lb_msg" runat="server" Text="testmsg"></asp:Label>
</asp:Content><asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    
</asp:Content>