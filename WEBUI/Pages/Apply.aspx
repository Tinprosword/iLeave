﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Apply.aspx.cs" Inherits="WEBUI.Pages.Apply" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="">
    <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="">
    <div class="row" id="mainpage">
        <div class ="col-xs-12" style="height:2px; padding:0px">&nbsp</div>
        <table class="col-xs-12 lsu-tablem1">
            <tr>
                <td style="width:80px"><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                <td>
                    <div style="float:left;"><asp:Literal ID="literal_applier" runat="server"></asp:Literal></div>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_leave" runat="server">Leave</asp:Literal></td>
                <td>
                    <asp:DropDownList ID="ddl_leavetype" runat="server" Width="90%" AutoPostBack="true" jqname="ddl_leavetype" OnSelectedIndexChanged="ddl_leavetype_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_apply" runat="server">Apply</asp:Literal></td>
                <td>
                    <asp:label ID="lt_applydays" runat="server" Width="50px"> </asp:label>
                    <asp:label ID="lt_balance" runat="server" Width="68px">Banlance</asp:label>
                    <asp:label ID="lt_balancedays" runat="server"> </asp:label>&nbsp;<asp:label Font-Size="14px" ID="lt_balancedetail" runat="server"></asp:label>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_section" runat="server">Section</asp:Literal></td>
                <td>
                    <asp:DropDownList ID="dropdl_section" runat="server" Width="90%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div style="float:left;padding-left:10px" id="aa">
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Res/images/adddate2.png" Width="40px" Height="40px"  OnClick="Canlendar_Click"/>
                    </div>
                    <div style="float:left;margin-left:100px;margin-right:0px; padding-right:0px;width:41px">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Res/images/comIcon_addattence.png" Width="40px" Height="40px" OnClick="Upload_Click"/>
                    </div>
                    <div style="float:left;color:red;position:relative;top:1px;left:-12px">
                        <asp:image ID="ib_counta" runat="server"  ImageUrl="~/Res/images/redcicle.png" Width="20px" Height="20px" Visible="false"/>
                    </div>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_remarks" runat="server">Remarks</asp:Literal></td>
                <td><asp:TextBox ID="tb_remarks" runat="server" Width="90%"></asp:TextBox> </td>
            </tr>
            <tr>
                <td colspan="2" style="color:red"><asp:Literal ID="literal_errormsga" runat="server" Visible="false"></asp:Literal></td>
            </tr>
        </table>
        <div class=" col-xs-12" style="height:2px"></div>
        <%--<table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white">
            <tr>
                <td class="col-xs-3"><asp:Literal ID="ltlistdate" runat="server"></asp:Literal></td>
                <td class="col-xs-4"><asp:Literal ID="ltlisttype" runat="server"></asp:Literal></td>
                <td class="col-xs-4"><asp:Literal ID="lt_listsection" runat="server"></asp:Literal></td>
                <td class="col-xs-1">  </td>
            </tr>
        </table>--%>
        <div class="col-xs-12 lsf-clearPadding" style="height:200px; overflow-y:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                <tr class="lss-bgcolor-blue" style="color:white">
                    <td class="col-xs-3" style="width:18%"><asp:Literal ID="ltlistdate" runat="server"></asp:Literal></td>
                    <td class="col-xs-4" style="width:44%"><asp:Literal ID="ltlisttype" runat="server"></asp:Literal></td>
                    <td class="col-xs-4" style="width:24%"><asp:Literal ID="lt_listsection" runat="server"></asp:Literal></td>
                    <td class="col-xs-1" style="width:14%">  </td>
                </tr>
                <asp:Repeater ID="repeater_leave" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>">
                            <td><%# ((MODEL.Apply.apply_LeaveData)Container.DataItem).LeaveDate.ToString("MM-dd") %></td>
                            <td><%#((MODEL.Apply.apply_LeaveData)Container.DataItem).leavetypeCode %></td>
                            <td>
                                <asp:DropDownList ID="rp_dropdl_section" runat="server" Width="90%"  OnSelectedIndexChanged="rp_dropdl_section_SelectedIndexChanged" AutoPostBack="true" fix="<%#Container.ItemIndex%>">
                                    <asp:ListItem Text="Full day" Value="0"  Selected="true"/>
                                    <asp:ListItem Text="AM" Value="1"/>
                                    <asp:ListItem Text="PM" Value="2"/>
                                    <asp:ListItem Text="3 Sections" Value="3"/>
                                </asp:DropDownList>
                            </td>
                            <td><asp:ImageButton ID="delete" Width="28px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="28px" ImageUrl="~/Res/images/close.png" runat="server" OnClick="delete_Click" /><asp:HiddenField ID="testhidden" runat="server" Value="<%#((MODEL.Apply.apply_LeaveData)Container.DataItem).leavetypeid %>" /></td>
                        </tr>
                    </ItemTemplate>

                </asp:Repeater>
            </table>
        </div>
      
        <div class="col-xs-12 lsf-center" style="padding-top:<%=BLL.GlobalVariate.setHeight()%>px; color:white;font-weight:bold">
            <%--<asp:Button ID="button_wait_user_Withdraw" runat="server" Text="Withdraw"  OnClick="button_apply_Click" CssClass="lsu-imagebtn"  style="float:unset;margin-right:16px; background-image:url(../res/images/btnok.png); background-size:124px 44px" Width="124px" Height="44px" />--%>
            <asp:Button ID="button_apply" runat="server" Text="Apply"  CssClass="lsu-imagebtn"  style="background-image:url(../res/images/btnok.png);background-size:124px 44px" Height="44px" Width="124px" OnClick="button_apply_Click" />
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentjs" runat="server">
    <script src="../Res/App/apply.js?lastmodify=<%=BLL.GlobalVariate.applyjsLastmodify %>"></script>
    <asp:Literal ID="lt_AlertJS" runat="server" EnableViewState="false"/>
    <asp:Literal ID="lt_model_datafrom" runat="server"/>
    <asp:Literal ID="lt_model_datato" runat="server"/>
    <asp:Literal ID="lt_js_prg" runat="server"/>
</asp:Content>