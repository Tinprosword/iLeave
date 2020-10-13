<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="chooseEmployment.aspx.cs" Inherits="WEBUI.Pages.chooseEmployment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="text-align:center; margin-top:10px;">
        <asp:Repeater ID="rp_items" runat="server">
            <ItemTemplate>
                <div id="left" class="col-xs-12">
                    <button ID="btn_choose" runat="server"  style="width:300px;height:120px;  text-align:left" eid="<%#(int)((WebServiceLayer.WebReference_user.PersonBaseinfo)Container.DataItem).e_id %>" eno="<%#((WebServiceLayer.WebReference_user.PersonBaseinfo)Container.DataItem).e_EmploymentNumber %>" onserverclick="chooseMe"><%#ShowEmploymentDesc((int)((WebServiceLayer.WebReference_user.PersonBaseinfo)Container.DataItem).e_id)%></button>
                </div>
                <br/>&nbsp;<br/>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>