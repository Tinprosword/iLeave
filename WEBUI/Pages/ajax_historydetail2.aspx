﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/ajax.Master" AutoEventWireup="true" CodeBehind="ajax_historydetail2.aspx.cs" Inherits="WEBUI.Pages.ajax_historydetail2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="showdiv" class="col-xs-12 lsf-clearPadding" style="height:<%=totalHeight%>px">
        <%--title1--%>
	    <table class="col-xs-12 lsu-table-xs4padding lsf-clearPadding" style="height:30px;">
		    <tbody>
                <tr class="lss-bgcolor-blue  lsf-clearPadding" style="color:white; height:24px;"><td class="col-xs-10" style="text-align:left"><asp:Literal ID="lt_leavedetail" runat="server">lt</asp:Literal></td><td class="col-xs-1"><img src="../Res/images/close.png" style="width:27px; height:27px" onclick="closeWindow()"></td></tr>
	        </tbody>
	    </table>
        <div class="col-xs-8" style="height:6px">&nbsp;</div>
        <%--balance--%>
        
		<table class="col-xs-12 lsf-clearPadding" style="margin-bottom:9px; height:40px;">
			<tbody>
                <tr><td class="col-xs-4 lsf-clearPadding;" style="padding-left:4px;padding-right:1px"><asp:Literal ID="lt_bancetitle" runat="server">lt</asp:Literal></td><td style="text-align:right;width:40px;"><div id="lbbalance"><asp:Literal ID="lt_balance" runat="server">3.81</asp:Literal></div></td><td>&nbsp;&nbsp;&nbsp;<asp:Literal ID="lt_days" runat="server">Day(s)</asp:Literal></td></tr>
			    <tr><td style="padding-left:4px;padding-right:1px"><asp:Literal ID="lt_applycount" runat="server">lt</asp:Literal></td><td style="text-align:right"><div id="lbapply"><asp:Literal ID="lt_apply" runat="server">3.81</asp:Literal></div></td><td>&nbsp;&nbsp;&nbsp;<asp:Literal ID="lt_days2" runat="server">Day(s)</asp:Literal></td></tr>
		    </tbody>
		</table>

        <%--approve history--%>
        <asp:Panel ID="panel_history" runat="server" Visible="false">
        <table class="col-xs-12 lsu-table-xs4padding lsf-clearPadding" style="margin-bottom:2px; height:30px">
            <tr class="lss-bgcolor-blue" style="color:white; height:24px;">
                    <td colspan="2" class="col-xs-12"><asp:Literal ID="lt_historytitle" runat="server">lt</asp:Literal></td>
			</tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="width:100%; height:<%=list_dataheight%>px;  overflow-y:scroll; overflow-x:hidden;">
        <table class="col-xs-12 lsu-table-xs4padding lsf-clearPadding" style="margin-bottom:2px; ">
                <asp:Repeater ID="rp_history" runat="server">
                    <ItemTemplate>
                        <tr style="height:23px">
                            <td><%#Container.ItemIndex+1%> </td>
				            <td style="width:99%"><%#((WebServiceLayer.WebReference_leave.LeaveHistory)Container.DataItem).ApplyDate.ToString("yyyy-MM-dd")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#BLL.User_wsref.GetDisplayName(((WebServiceLayer.WebReference_leave.LeaveHistory)Container.DataItem).uid,nametype) %></td>
                        </tr>
                        <asp:Panel ID="panel_remark" runat="server" Visible="<%#!string.IsNullOrEmpty(((WebServiceLayer.WebReference_leave.LeaveHistory)Container.DataItem).Remark)%>">
                        <tr style="height:23px">
                            <td></td>
                            <td><%#BLL.MultiLanguageHelper.GetLanguagePacket((LSLibrary.WebAPP.LanguageType)lan).approval_approverRemark %>:<%#((WebServiceLayer.WebReference_leave.LeaveHistory)Container.DataItem).Remark%></td>
                        </tr>
                        <tr style="height:5px;"><td style="height:5px; line-height:5px;">&nbsp</td></tr>
                        </asp:Panel>
                    </ItemTemplate>
                </asp:Repeater>

        </table>
            </div>
        </asp:Panel>
        <%--list--%>             
		<table class="col-xs-12 lsu-table-xs lsf-clearPadding">
			<tbody>
                <tr class="lss-bgcolor-blue" style="color:white; height:24px;">
                    <td>
                        <table class="col-xs-12 lsu-table-xs4padding lsf-clearPadding">
                            <tbody><tr>
				                <td class="col-xs-4" style="width:120px"><asp:Literal ID="lt_col1" runat="server">lt</asp:Literal></td>
				                <td class="col-xs-4" style="width:120px"><asp:Literal ID="lt_col2" runat="server">lt</asp:Literal></td>
				                <td class="col-xs-2" style="width:60px;text-align:right;"><asp:Literal ID="lt_col3" runat="server">lt</asp:Literal></td>
				                <td class="col-xs-3"><asp:Literal ID="lt_colstatus" runat="server">lt</asp:Literal></td>
                            </tr></tbody>
                        </table>
                    </td>
				    <td class="col-xs-1" style="width:17px;">&nbsp;</td>
			    </tr>
		    </tbody>
		</table>
                         
		<div class="col-xs-12 lsf-clearPadding" style="width:100%;  height:<%=historytitleHeight%>px; overflow-y:scroll; overflow-x:hidden;" id="divlist" runat="server">
			<table class="col-xs-12 lsu-table-xs4padding lsf-clearPadding">
                <tbody>
                    <asp:Repeater ID="rp_list" runat="server">
                        <ItemTemplate>
                            <tr style="">
                                <td class="col-xs-4" style="width:120px"><%#((DateTime)((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).LeaveFrom).ToString("yyyy-MM-dd")%></td>
					            <td class="col-xs-4" style="width:120px"><%#BLL.GlobalVariate.GetSectionMultLanguage(((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem),lan) %></td>
					            <td class="col-xs-2" style="width:60px;text-align:right;"><%#BLL.GlobalVariate.GetUnit((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem) %>&nbsp;</td>
					            <td class="col-xs-3"><%# GetLeaveStatus((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
			    </tbody>
			</table>
		</div>
    </div>
</asp:Content>
