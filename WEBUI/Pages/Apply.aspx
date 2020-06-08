﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Apply.aspx.cs" Inherits="WEBUI.Pages.Apply" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" id="mainpage">
        <div class="col-xs-12" style="height:16px; padding:0px"/>
        <table class="col-xs-12 lsu-tablem1">
            <tr>
                <td style="width:90px"><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                <td>
                    <div style="float:left;"><asp:Literal ID="literal_applier" runat="server"></asp:Literal></div>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_leave" runat="server">Leave</asp:Literal></td>
                <td>
                    <asp:DropDownList ID="ddl_leavetype" runat="server" Width="90%">
                        <asp:ListItem Text="Please Select" Value="-1"  Selected="true"/>
                        <asp:ListItem Text="AL" Value="0"/>
                        <asp:ListItem Text="SL" Value="1"/>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_apply" runat="server">Apply</asp:Literal></td>
                <td>
                    <asp:label ID="lt_applydays" runat="server" Width="80px">0 Days</asp:label>
                    <asp:label ID="lt_balance" runat="server" Width="80px">Banlance</asp:label>
                    <asp:label ID="lt_balancedays" runat="server" Width="80px">9.2 Days</asp:label>

                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_date" runat="server">Date</asp:Literal></td>
                <td>
                    <asp:TextBox ID="tb_from" runat="server" Width="40%" ReadOnly="true"></asp:TextBox><asp:Button ID="btn_from" runat="server" Text="from" Visible="false"  /> To <asp:TextBox ID="tb_to" runat="server" Width="40%" ReadOnly="true"></asp:TextBox><asp:Button ID="btn_to" runat="server" Text="To" Visible="false"/>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_section" runat="server">Section</asp:Literal></td>
                <td><asp:DropDownList ID="dropdl_section" runat="server" Width="90%">
                    <asp:ListItem Text="Full day" Value="0"  Selected="true"/>
                    <asp:ListItem Text="AM" Value="1"/>
                    <asp:ListItem Text="PM" Value="2"/>
                    <asp:ListItem Text="3 Sections" Value="3"/>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_remarks" runat="server">Remarks</asp:Literal></td>
                <td><asp:TextBox ID="tb_remarks" runat="server" Width="90%"></asp:TextBox> </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="col-xs-8"><asp:ImageButton ID="ImageButton1" runat="server"   ImageUrl="~/Res/images/comIcon_canlendar.png" Width="40px" Height="40px" OnClick="ImageButton1_Click"/></div>
                    <div class="col-xs-2"><asp:ImageButton ID="ImageButton2" runat="server"   ImageUrl="~/Res/images/comIcon_addattence.png" Width="40px" Height="40px" OnClick="ImageButton2_Click"/></div>
                </td>
            </tr>
        </table>
        <div class=" col-xs-12" style="height:2px"></div>
        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-3">Date</td><td class="col-xs-4">Type</td><td class="col-xs-3">Section</td><td class="col-xs-2">  </td></tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="height:200px; overflow:scroll;">
            <table class="col-xs-12 lsu-table-sm">
                <asp:Repeater ID="repeater_leave" runat="server">
                    <ItemTemplate>
                        <tr><td class="col-xs-3"><%# ((BLL.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((BLL.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((BLL.Apply.LeaveData)Container.DataItem).section %></td><td class="col-xs-1"><asp:ImageButton ID="delete" Width="28px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="20px" ImageUrl="~/Res/images/close1.png" runat="server" OnClick="delete_Click" /><asp:HiddenField ID="testhidden" runat="server" Value="<%#((BLL.Apply.LeaveData)Container.DataItem).type %>" /></td></tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:aliceblue"><td class="col-xs-3"><%# ((BLL.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((BLL.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((BLL.Apply.LeaveData)Container.DataItem).section %></td><td class="col-xs-1"><asp:ImageButton ID="delete" Width="28px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="20px" ImageUrl="~/Res/images/close1.png" runat="server" OnClick="delete_Click" /><asp:HiddenField ID="testhidden" runat="server" Value="<%#((BLL.Apply.LeaveData)Container.DataItem).type %>" /></td></tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
      
        <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
            <asp:Button ID="button_apply" runat="server" Text="Apply"  CssClass="btn lss-btncolor-blue" Width="160px" OnClick="button_apply_Click"/>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentjs" runat="server">
    <script src="../Res/App/apply.js"></script>
    <asp:Literal ID="lt_AlertJS" runat="server" EnableViewState="false"/>
    <asp:Literal ID="lt_model_datafrom" runat="server"/>
    <asp:Literal ID="lt_model_datato" runat="server"/>
</asp:Content>