<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Apply.aspx.cs" Inherits="WEBUI.Pages.Apply" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="">
    <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="">

    <div class="row">
        <ul id="myTabApply" class="nav nav-tabs lsf-clearPadding" runat="server">
	            <li id="myTabapply_new" runat="server" class="active"><a style="padding-top:5px; padding-bottom:3px;" data-toggle="tab" onclick="window.location.href='apply.aspx'"><asp:Literal ID="lt_new" runat="server" Text="New"></asp:Literal></a></li>
	            <li id="myTabapply_pending" runat="server"><a style="padding-top:4px; padding-bottom:4px;" data-toggle="tab" onclick="window.location.href='approval_wait.aspx?action=1&applicationtype=0'"><asp:Literal ID="lt_mypending" runat="server" Text="Pending"/></a></li>
                <li id="myTabapply_history" runat="server"><a style="padding-top:4px; padding-bottom:4px;" data-toggle="tab" onclick="window.location.href='approval_wait.aspx?action=1&applicationtype=3'"><asp:Literal ID="lt_myhistory" runat="server" Text="History"/></a></li>
        </ul>
    </div>

    <div class="row" id="mainpage">
        <div class ="col-xs-12" style="height:2px; padding:0px">&nbsp</div>
        <table class="col-xs-12 lsu-tablem1">
            <tr>
                <td style="width:80px"><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                <td colspan="2">
                    <div style="float:left;"><asp:Literal ID="literal_applier" runat="server"></asp:Literal></div>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_leave" runat="server">Leave</asp:Literal></td>
                <td colspan="2">
                    <asp:DropDownList style="height:24px" ID="ddl_leavetype" runat="server" Width="90%" AutoPostBack="true" jqname="ddl_leavetype" OnSelectedIndexChanged="ddl_leavetype_SelectedIndexChanged"></asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td><asp:Literal ID="lt_apply" runat="server">Apply</asp:Literal></td>
                <td colspan="2">
                    <asp:label ID="lt_applydays" runat="server" Width="50px"> </asp:label>
                    <asp:label ID="lt_balance" runat="server" Width="68px">Banlance</asp:label>
                    <asp:label ID="lt_balancedays" runat="server"> </asp:label>&nbsp;<asp:label Font-Size="14px" ID="lt_balancedetail" runat="server"></asp:label>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_section" runat="server">Section</asp:Literal></td>
                <td colspan="2">
                    <asp:DropDownList ID="dropdl_section" runat="server" Width="90%" style="height:24px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
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
                <td style="vertical-align:bottom">
                    <asp:TextBox ID="tb_remarks" runat="server" Height="50px" Width="100%" TextMode="MultiLine" style="padding-bottom:0px;"></asp:TextBox>
                </td>
                <td style="width:30px; vertical-align:bottom;padding-bottom:12px;"><asp:Button ID="btn_apply" runat="server" Text="Submit" BackColor="#2573a4" ForeColor="White" BorderWidth="0" Height="34px" Font-Size="16px" style="border-radius:5px 5px 5px 5px"  OnClick="button_apply_Click"/></td>
            </tr>
        </table>
        <div class=" col-xs-12" style="height:16px; color:red;padding-left:15px;"><asp:Literal ID="literal_errormsga" runat="server" Visible="false"></asp:Literal></div>
        <div class=" col-xs-12" style="height:2px"></div>
        <div class="col-xs-12 lsf-clearPadding" style="height:235px; overflow-y:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                <tr class="lss-bgcolor-blue" style="color:white">
                    <td class="col-xs-3" style="width:18%"><asp:Literal ID="ltlistdate" runat="server"></asp:Literal></td>
                    <td class="col-xs-4" style="width:44%"><asp:Literal ID="ltlisttype" runat="server"></asp:Literal></td>
                    <td class="col-xs-4" style="width:70px"><asp:Literal ID="lt_listsection" runat="server"></asp:Literal></td>
                    <td class="col-xs-1" style="width:30px">&nbsp;</td>
                </tr>
                <asp:Repeater ID="repeater_leave" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>">
                            <td><%# ((MODEL.Apply.apply_LeaveData)Container.DataItem).LeaveDate.ToString("MM-dd") %></td>
                            <td><%#((MODEL.Apply.apply_LeaveData)Container.DataItem).leavetypeCode %></td>
                            <td>
                                <asp:DropDownList ID="rp_dropdl_section" runat="server" Width="100%"  OnSelectedIndexChanged="rp_dropdl_section_SelectedIndexChanged" AutoPostBack="true" fix="<%#Container.ItemIndex%>">
                                    <asp:ListItem Text="Full day" Value="0"  Selected="true"/>
                                    <asp:ListItem Text="AM" Value="1"/>
                                    <asp:ListItem Text="PM" Value="2"/>
                                    <asp:ListItem Text="3 Sections" Value="3"/>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:right"><asp:ImageButton ID="delete" Width="30px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="30px" ImageUrl="~/Res/images/close.png" runat="server" OnClick="delete_Click" /><asp:HiddenField ID="testhidden" runat="server" Value="<%#((MODEL.Apply.apply_LeaveData)Container.DataItem).leavetypeid %>" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentjs" runat="server">

    <script src="../Res/App/apply.js?lastmodify=<%=BLL.GlobalVariate.applyjsLastmodify %>"></script>
    <asp:Literal ID="lt_js_prg" runat="server"/>
    <asp:Literal ID="js_waitdiv" runat="server"/>
</asp:Content>