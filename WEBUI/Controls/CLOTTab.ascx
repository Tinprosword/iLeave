<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CLOTTab.ascx.cs" Inherits="WEBUI.Controls.CLOTTab" %>
<div class="row">
    <ul id="myTabApply" fixname="mytab"   class="nav nav-tabs lsf-clearPadding" runat="server">
	        <li id="myTabapply_new" runat="server" class="active"><a style="padding-top:5px; padding-bottom:3px;" data-toggle="tab" runat="server" id="a_new"><asp:Literal ID="lt_new" runat="server" Text="New"></asp:Literal></a></li>
	        <li id="myTabapply_pending" runat="server"><a style="padding-top:4px; padding-bottom:4px;" data-toggle="tab"  runat="server" id="a_pending"><asp:Literal ID="lt_mypending" runat="server" Text="Pending"/></a></li>
            <li id="myTabapply_history" runat="server"><a style="padding-top:4px; padding-bottom:4px;" data-toggle="tab"  runat="server" id="a_history"><asp:Literal ID="lt_myhistory" runat="server" Text="History"/></a></li>
    </ul>
</div>