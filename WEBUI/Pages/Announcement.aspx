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
        <div class="col-xs-8" style="float:right; text-align:right;">
            <asp:Button ID="btn_readAll" runat="server" Text="Set All Read" style="color:White;background-color:#2573A4;border-width:0px;font-size:16px;height:28px;border-radius:4px 4px 4px 4px" OnClick="btn_readAll_Click" />
<%--            border:2px solid #8da9cd;background-color:white; width:100px;--%>
<%--            color:White;background-color:#2573A4;border-width:0px;font-size:16px;height:28px;border-radius:4px 4px 4px 4px--%>
        </div>
    </div>
    <div class="row">
        <asp:Repeater ID="rp_announctment" runat="server">
            <ItemTemplate>
                <div class="col-xs-12" style=" line-height:8px;text-align:center;padding:0px;  margin:0px; padding-top:1px;" >
                    <label class="lsf-clearPadding" style="padding:0px;  margin:0px;height:1px;background-color:dimgray; width:90%; padding-left:3px; padding-right:3px;"></label>
                </div>
                <div class="col-xs-12 divheighter" style="text-align:left; font-size:14px; font-weight:bold; padding-bottom:2px; padding-top:12px; padding-left:20px;">
                    <asp:linkbutton ID="lb_title" runat="server" OnClick="lb_title_Click"  CommandArgument="<%#((WebServiceLayer.WebReference_Ileave_Other.t_Announcement)Container.DataItem).ID %>" Text="<%#((WebServiceLayer.WebReference_Ileave_Other.t_Announcement)Container.DataItem).Subject.Trim() %>"/>
                    <asp:Label ID="lb_title_new" runat="server" Text=<%#rp_announctment_displayTitle((WebServiceLayer.WebReference_Ileave_Other.t_Announcement)Container.DataItem)%>  style="font-style:italic; color:red"/>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content><asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    
</asp:Content>