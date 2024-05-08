<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Apply.aspx.cs" Inherits="WEBUI.Pages.Apply" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="">
    <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="">

    <div class="row">
        <ul id="myTabApply" fixname="mytab"   class="nav nav-tabs lsf-clearPadding modifyTab" runat="server">
	        <li id="myTabapply_new" runat="server" class="active"><a style="padding-top:5px; padding-bottom:3px;padding-left:10px; padding-right:10px; " data-toggle="tab"><asp:Literal ID="lt_new" runat="server" Text="New"></asp:Literal></a></li>
	        <li id="myTabapply_pending" runat="server"><a style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px; " data-toggle="tab" onclick="<%=showPendEvent()%>"><asp:Literal ID="lt_mypending" runat="server" Text="Pending"/></a></li>
            <li id="myTabapply_history" runat="server"><a style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px; " data-toggle="tab" onclick="<%=showhisEvent()%>"><asp:Literal ID="lt_myhistory" runat="server" Text="History"/></a></li>
            <li id="myTabapply_es" runat="server" style="padding-left:0px;margin-left:0px;"><a style="padding-top:4px; padding-left:10px; padding-right:10px; padding-bottom:4px;" data-toggle="tab" onclick="<%=showesEvent()%>"><asp:Literal ID="lt_estimation" runat="server" Text="Estimation"/></a></li>
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
            <tr style="height:28px" id="tr_CurrentALEntilte" runat="server">
                <td></td>
                <td colspan="2">
                    <asp:Label ID="lb_CurrentALEntilteName" runat="server" Text="Current Entitlement:"></asp:Label>
                    <asp:Label ID="lb_CurrentALEntilteValue" runat="server" Text="27"></asp:Label>
                    &nbsp;&nbsp;<asp:Label ID="lb_CurrentALEntiltedays" runat="server" Text="D"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_apply" runat="server">Apply</asp:Literal></td>
                <td colspan="2">
                    <asp:label ID="lt_applydays" runat="server" Width="50px" Text="1 D"> </asp:label>
                    <asp:label ID="lt_balance" runat="server" Width="68px">Banlance</asp:label>
                    <asp:label ID="lt_balancedays" runat="server" Text="5 D"> </asp:label>
                </td>
            </tr>
            <tr id="tr_radio" runat="server">
                <td><asp:Literal ID="Literal1" runat="server"></asp:Literal></td>
                <td colspan="2">
                    <asp:RadioButtonList ID="radio_ishour" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"  Font-Bold="false" CssClass="overlableSize16" CellSpacing="15" AutoPostBack="true" OnSelectedIndexChanged="radio_ishour_SelectedIndexChanged">
                        <asp:ListItem Text="By Day　　　" Value="0" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="By Hour" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr id="tr_section" runat="server">
                <td><asp:Literal ID="lt_section" runat="server">Section</asp:Literal></td>
                <td colspan="2">
                    <asp:DropDownList ID="dropdl_section" runat="server" Width="90%" style="height:24px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr_time" runat="server">
                <td><asp:Literal ID="lt_time" runat="server">Time</asp:Literal></td>
                <td>
                    <asp:DropDownList Width="80px" ID="DropDownList1" runat="server" OnTextChanged="DropDownList1_TextChanged" AutoPostBack="true">
                    </asp:DropDownList> &nbsp;&nbsp;: &nbsp;&nbsp;
                    <asp:DropDownList Width="80px" ID="DropDownList2" runat="server" OnTextChanged="DropDownList1_TextChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <div style="height:10px;"></div>
                    <asp:DropDownList Width="80px" ID="DropDownList3" runat="server" OnTextChanged="DropDownList1_TextChanged" AutoPostBack="true">
                    </asp:DropDownList> &nbsp;&nbsp;: &nbsp;&nbsp;
                    <asp:DropDownList Width="80px" ID="DropDownList4" runat="server" OnTextChanged="DropDownList1_TextChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <div style="height:5px;"></div>
                    <asp:TextBox ID="tb_total" runat="server" Text="0"></asp:TextBox>
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
        <div class="col-xs-12 lsf-clearPadding" style="height:265px; overflow-y:scroll;">
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
                                <asp:Label ID="rp_lb_byhour" runat="server"></asp:Label>
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