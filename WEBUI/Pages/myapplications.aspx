﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="myapplications.aspx.cs" Inherits="WEBUI.Pages.myapplications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="margin-top:5px;">
        <div class ="col-xs-4" style=" padding-left:5px;padding-right:0px"><asp:Button ID="btn_wait" runat="server" Text="Wait for approval"  Width="100%" Height="42px" CssClass="btnBox btnBlueBoxSelect" OnClick="btn_wait_Click" /></div>
        <div class ="col-xs-3" style="padding:0px;"><asp:Button ID="btn_approved" runat="server" Text="Approved"  Width="100%" Height="42px" CssClass="btnBox btnBlueBoxUnSelect" OnClick="btn_approved_Click" /></div>
        <div class ="col-xs-5" style="padding:0px; padding-right:5px;"><asp:Button ID="btn_rejectWith" runat="server" Text="Rejected/Withdrawed"  Width="100%" Height="42px" CssClass="btnBox btnBlueBoxUnSelect" OnClick="btn_rejectWith_Click" /></div>
    </div>
    <div class="row" style="padding-bottom:10px;margin-top:10px;">
        <div class="col-xs-12;" style="padding-left:7px;">
            <table>
                <tr>
                    <td style="padding-left:0px;padding-right:5px">name</td><td><asp:TextBox ID="tb_name" runat="server" Width="90px" ReadOnly="true"></asp:TextBox></td>
                    <td style="padding-left:15px;padding-right:5px">Date From</td><td><asp:TextBox ID="tb_date" runat="server" Width="90px"></asp:TextBox></td>
                </tr>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-xs-12 lsf-clearPadding" style="height:500px; overflow:scroll;padding-left:5px;padding-right:0px">
            <table class="col-xs-12 lsu-table-xs" style="font-size:15px">
                <asp:Repeater ID="repeater_myapplications" runat="server">
                    <ItemTemplate>
                        <tr style="height:42px"><td class="col-xs-4" style="padding:0px">Sick Leave</td><td class="col-xs-7" style="padding:0px">2020-01-05_2020-1-7 (2天)</td><td class="col-xs-1" style="padding:0px"><asp:LinkButton ID="lb" OnClick="lb_Click" runat="server" style="font-size:24px">></asp:LinkButton></td></tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:aliceblue;height:42px"><td class="col-xs-4" style="padding:0px">Sick Leave</td><td class="col-xs-7" style="padding:0px">2020-01-05_2020-1-7 (2天)</td><td class="col-xs-1" style="padding:0px"><asp:LinkButton ID="lb" OnClick="lb_Click" runat="server" style="font-size:24px">></asp:LinkButton></td></tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>