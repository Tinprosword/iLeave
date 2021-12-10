<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="calendar.aspx.cs" Inherits="WEBUI.Pages.calendar" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>
    <div class="row">
        <div class="col-xs-3" style="width:29% ;padding-right:0px;"><asp:Button ID="btn_myself" runat="server" Text="Me"  Width="100%" Height="30px" CssClass="btnBox btnBlueBoxSelect" OnClick="btn_myself_Click"/></div>
        <div class="col-xs-3" style="width:29% ;padding-left:0px;padding-right:0px;"><asp:Button ID="btn_team" runat="server" Text="Team"  Width="100%" Height="30px" CssClass="btnBox btnBlueBoxUnSelect" OnClick="btn_team_Click"  /></div>
        <div class="col-xs-3" style="width:42% ;padding-left:15px;height:24px;line-height:24px;padding-right:5px">
            <asp:TextBox Width="100%" ID="tb_name" runat="server" OnTextChanged="tb_name_TextChanged" AutoPostBack="true" style="border:1px solid #808080"></asp:TextBox>
        </div>
        <div class="col-xs-12 " style="margin-top:8px; margin-bottom:8px;">
            <asp:DropDownList ID="ddlzone" Height="24px" runat="server"  CssClass="col-xs-7 lsf-clearPadding" OnSelectedIndexChanged="unit_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <div style="float:left;padding-top:1px;" class="overlable">
                &nbsp;&nbsp;&nbsp;<asp:RadioButton  ID="cb_leave" runat="server"  OnCheckedChanged="cb_leave_CheckedChanged" GroupName="leaveroster" Text="假期"  Checked="true"  AutoPostBack="true"  Font-Bold="false"></asp:RadioButton>
                &nbsp<asp:RadioButton ID="cb_holiday" GroupName="leaveroster" runat="server" Text="更期" OnCheckedChanged="cb_holiday_CheckedChanged" AutoPostBack="true"  Font-Bold="false"/>
            </div>
        </div>
        <div class="col-xs-12 lsf-center">
            <asp:Calendar NextPrevStyle-HorizontalAlign="Justify"  ID="Calendar1"  runat="server" Width="340px" Height="250px" Font-Size="Larger" OnSelectionChanged="Calendar1_SelectionChanged" DayHeaderStyle-Font-Size="14px" DayHeaderStyle-CssClass="lsf-center" ToolTip=" "></asp:Calendar>
        </div>
        <div class="row col-xs-12" style="padding-right:0px;" id="divTip" runat="server" visible="true">
            <div class="col-xs-5" style="width:36%;"><asp:Image ID="Image1" runat="server" ImageUrl="~/Res/images/square.png" BackColor="Black" />&nbsp;<asp:Literal ID="lt_approval" runat="server">approval</asp:Literal></div>
            <div class="col-xs-7" style="width:64%;"><asp:Image ID="Image2" runat="server" ImageUrl="~/Res/images/square.png" BackColor="#f3e926" />&nbsp;<asp:Literal ID="lt_wait" runat="server">wait for approval</asp:Literal></div>
        </div>
        <div class="col-xs-9 lss-color-blue" style=" font-weight:bold; font-size:18px;margin-top:15px"><asp:Label ID="Label1" Height="21px" runat="server" Text="lb_date">2020-04-07</asp:Label></div>
        <div class="col-xs-3 lss-color-blue" style="margin-top:10px; text-align:right; padding-right:32px;" id="app_ok" runat="server" visible="false">
            <asp:Button ID="btn_ok" runat="server" Text="Add" BackColor="#588da7" ForeColor="White" BorderWidth="0" Height="27px" Font-Size="16px" style="border-radius:5px 5px 5px 5px"/>
        </div>
        <div class="col-xs-12" id="leaveDiv" runat="server" visible="true">
            <div class="col-xs-12 lsf-clearPadding" style="height:150px; overflow-y:scroll;">
                <table class="col-xs-12 lsu-table-xs lsf-clearPadding">
                    <tr class="lss-bgcolor-blue" style="color:white;padding-right:0px">
                        <td class="col-xs-4"><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                        <td class="col-xs-3" style='min-width:88px; display:<%=LSLibrary.WebAPP.HtmlCssHelper.CSS_DisplayValue(!bHiddenLeaveCode)%>'><asp:Literal ID="lt_section" runat="server">Type</asp:Literal></td>
                        <td class="col-xs-3" style='display:<%=LSLibrary.WebAPP.HtmlCssHelper.CSS_DisplayValue(bHiddenLeaveCode)%>'><asp:Literal ID="lt_leavecode" runat="server">LeaveCode</asp:Literal></td>
                        <td class="col-xs-5"><asp:Literal ID="lt_status" runat="server">Status</asp:Literal></td>
                    </tr>
                    <asp:Repeater ID="repeater_leave" runat="server" EnableViewState="true">
                        <ItemTemplate>
                            <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>;">
                                <td style="vertical-align:top; padding-top:3px;"><%# ((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).displayname %></td>
                                <td style='vertical-align:top; padding-top:3px;display:<%=LSLibrary.WebAPP.HtmlCssHelper.CSS_DisplayValue(!bHiddenLeaveCode)%>'><%# BLL.GlobalVariate.GetSectionMultLanguage(((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem)) %></td>
                                <td style='vertical-align:top; padding-top:3px;display:<%=LSLibrary.WebAPP.HtmlCssHelper.CSS_DisplayValue(bHiddenLeaveCode)%>'><%# ShowRP_LeaveCode( ((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).LeaveID,((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem))%></td>
                                <td style='vertical-align:top; padding-top:3px;'><%# BLL.Leave.GetLeaveStatusDesc(((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).WorkflowTypeID,((WebServiceLayer.WebReference_leave.LeaveRequestDetail)Container.DataItem).masterStatus) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="col-xs-12" id="rosterDiv" runat="server" visible="false">
            <div class="col-xs-12 lsf-clearPadding" style="height:150px; overflow:scroll;">
                <table class="col-xs-12 lsu-table-xs">
                    <tr class="lss-bgcolor-blue" style="color:white;">
                        <td class="col-xs-3"><asp:Literal ID="lt_displayname" runat="server">Name</asp:Literal></td>
                        <td class="col-xs-6"><asp:Literal ID="lt_shiftCode" runat="server">Shift</asp:Literal></td>
                        <td class="col-xs-3"><asp:Literal ID="lt_remark" runat="server">Remark</asp:Literal></td>
                    </tr>
                    <asp:Repeater ID="rp_roster" runat="server" EnableViewState="true">
                        <ItemTemplate>
                            <tr style="<%#BLL.Leave.SetBackgroundColor(Container.ItemIndex)%>;">
                                <td class="col-xs-3" style="vertical-align:top; padding-top:3px;"><%# ((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).DispayName %></td>
                                <td class="col-xs-6" style="vertical-align:top; padding-top:3px;"><label style="width:65px; font-weight:normal; font-size:15px" id="code"><%# ((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).ShiftCode %></label><label id="time" style="font-weight:normal; font-size:15px"><%# ((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).Time %></label></td>
                                <td class="col-xs-3" style="vertical-align:top; padding-top:3px;"><%# ((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).Remark %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server"></asp:Content>