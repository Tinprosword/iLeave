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
    <div class="row">
        <asp:Repeater ID="rp_announctment" runat="server" OnItemCommand="rp_announctment_ItemCommand">
            <ItemTemplate>
                <div class="col-xs-12" style=" line-height:8px;text-align:center;padding:0px;  margin:0px; padding-top:1px; padding-bottom:4px" >
                    <label class="lsf-clearPadding" style="padding:0px;  margin:0px;height:1px;background-color:dimgray; width:90%; padding-left:3px; padding-right:3px;"></label>
                </div>
                <div class="col-xs-12 divheighter" style="text-align:center; font-size:14px; font-weight:bold; padding-bottom:5px;">
                    <asp:Label ID="lb_title" runat="server" Text="<%#RP_DisplayTitle((MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                </div>
                <div class="col-xs-12 divheighter" style="padding-bottom:8px">
                    <asp:Label ID="lb_content" runat="server" Text="<%#RP_DisplayContent((MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                </div>
                <%--为什么要这样做，因为只是想走页面流程走的更简单一点，因为动态生成的话，事件是调动不起来的，必须在页面初始化的时候，就动态生成。或者采用其他方式来调用。但是我只想得到一个简单的页面事件流程。静态控件是更简单的。--%>
                <div class="col-xs-12 divheighter" style="" id="div_links">
                    <asp:LinkButton ID="lb_attachment1" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(0,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(0,(MODEL.Announcement.GirdViewData)Container.DataItem)%>" Text="<%#RP_DisplayAttach(0,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                    <asp:LinkButton ID="lb_attachment2" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(1,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(1,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"  Text="<%#RP_DisplayAttach(1,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                    <asp:LinkButton ID="lb_attachment3" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(2,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(2,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"  Text="<%#RP_DisplayAttach(2,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                    <asp:LinkButton ID="lb_attachment4" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(3,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(3,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"  Text="<%#RP_DisplayAttach(3,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                    <asp:LinkButton ID="lb_attachment5" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(4,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(4,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"  Text="<%#RP_DisplayAttach(4,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                    <asp:LinkButton ID="lb_attachment6" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(5,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(5,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"  Text="<%#RP_DisplayAttach(5,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                    <asp:LinkButton ID="lb_attachment7" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(6,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(6,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"  Text="<%#RP_DisplayAttach(6,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                    <asp:LinkButton ID="lb_attachment8" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(7,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(7,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"  Text="<%#RP_DisplayAttach(7,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                    <asp:LinkButton ID="lb_attachment9" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(8,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(8,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"  Text="<%#RP_DisplayAttach(8,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                    <asp:LinkButton ID="lb_attachment10" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(9,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(9,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"  Text="<%#RP_DisplayAttach(9,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                    <asp:LinkButton ID="lb_attachment11" runat="server"  CommandName="mStatic_rp_link_commandname" CommandArgument="<%#RP_AttachID(10,(MODEL.Announcement.GirdViewData)Container.DataItem) %>" Visible="<%#RP_AttachVisialbe(10,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"  Text="<%#RP_DisplayAttach(10,(MODEL.Announcement.GirdViewData)Container.DataItem)%>"/>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content><asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    
</asp:Content>