<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="ApplyCLOT.aspx.cs" Inherits="WEBUI.Pages.ApplyCLOT" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="row">
        <ul id="myTabApply" fixname="mytab"   class="nav nav-tabs lsf-clearPadding" runat="server">
	            <li id="myTabapply_new" runat="server" class="active"><a style="padding-top:5px; padding-bottom:3px;padding-left:10px; padding-right:10px; " data-toggle="tab"><asp:Literal ID="lt_new" runat="server" Text="New"></asp:Literal></a></li>
	            <li id="myTabapply_pending" runat="server"><a style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px; " data-toggle="tab" onclick="<%=showPendEvent()%>"><asp:Literal ID="lt_mypending" runat="server" Text="Pending"/></a></li>
                <li id="myTabapply_history" runat="server"><a style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px; " data-toggle="tab" onclick="<%=showhisEvent()%>"><asp:Literal ID="lt_myhistory" runat="server" Text="History"/></a></li>
        </ul>
    </div>

    <div class="row" id="mainpage">
        <div class ="col-xs-12" style="height:2px; padding:0px">&nbsp</div>
        <table class="col-xs-12 lsu-tablem1">
            <tr>
                <td style="width:74px"><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                <td colspan="2">
                    <div style="float:left;"><asp:Literal ID="literal_applier" runat="server"></asp:Literal></div>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_leave" runat="server">Type</asp:Literal></td>
                <td colspan="2">
                    <asp:DropDownList style="height:24px" ID="ddl_leavetype" runat="server" Width="90%" AutoPostBack="true" OnSelectedIndexChanged="ddl_leavetype_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            
            <tr>
                <td><asp:Literal ID="lt_apply" runat="server">Planned</asp:Literal></td>
                <td colspan="2">
                    <asp:label ID="lt_applydays" runat="server" style="white-space: pre;"></asp:label>
                </td>
            </tr>
            <tr>
                <td><asp:label ID="lt_balance" runat="server" Width="60px">Banlance</asp:label></td>
                <td colspan="2">
                    <asp:label ID="lt_balancedays" runat="server" style="white-space: pre;"></asp:label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="lt_date" runat="server">Date</asp:Literal>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tb_date" runat="server" Width="90%" CssClass="datepicker1"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr_secion" runat="server" visible="false">
                <td><asp:Literal ID="lt_section" runat="server">Section</asp:Literal></td>
                <td>
                    <asp:DropDownList ID="ddl_section" runat="server" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddl_section_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_time" runat="server">Time</asp:Literal></td>
                <td>
                    <asp:DropDownList Width="80px" ID="DropDownList1" runat="server" OnTextChanged="OnFromTO_TextChanged" AutoPostBack="true">
                    </asp:DropDownList> &nbsp;&nbsp;: &nbsp;&nbsp;
                    <asp:DropDownList Width="80px" ID="DropDownList2" runat="server" OnTextChanged="OnFromTO_TextChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <div style="height:10px;"></div>
                    <asp:DropDownList Width="80px" ID="DropDownList3" runat="server" OnTextChanged="OnFromTO_TextChanged" AutoPostBack="true">
                    </asp:DropDownList> &nbsp;&nbsp;: &nbsp;&nbsp;
                    <asp:DropDownList Width="80px" ID="DropDownList4" runat="server" OnTextChanged="OnFromTO_TextChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    <div style="height:5px;"></div>
                    </td>
                    <td style="width:80px; vertical-align:bottom;padding-bottom:12px;"><asp:Button ID="btn_add" runat="server" Text="Add"  BackColor="#2573a4" ForeColor="White" BorderWidth="0" Height="34px" Font-Size="16px" Width="68px" style="border-radius:5px 5px 5px 5px" OnClick="btn_add_Click"/></td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_hours" runat="server" Text="Number of Hours"></asp:Literal></td>
                <td>
                    <div style="float:left;">
                        <asp:TextBox ID="tb_hours" runat="server" Width="100%" Text="0" ></asp:TextBox>
                    </div>
                </td>
                <td>
                    <div style="float:left; padding-right:0px;width:41px">
                        <asp:ImageButton ID="image_btn" runat="server" ImageUrl="~/Res/images/comIcon_addattence.png" Width="40px" Height="40px" OnClick="image_btn_Click"/>
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
                <td style="width:80px; vertical-align:bottom;padding-bottom:12px;"><asp:Button ID="btn_apply" runat="server" Text="Submit" BackColor="#2573a4" ForeColor="White" BorderWidth="0" Height="34px" Font-Size="16px" Width="68px" style="border-radius:5px 5px 5px 5px; padding:0px;" OnClick="btn_apply_Click"/></td>
            </tr>
        </table>
        <div class=" col-xs-12" style="height:16px; color:red;padding-left:15px;"><asp:Literal ID="literal_errormsga" runat="server" Visible="false"></asp:Literal></div>
        <div class=" col-xs-12" style="height:2px"></div>
        <div class="col-xs-12 lsf-clearPadding" style="height:260px; overflow-y:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                <tr class="lss-bgcolor-blue" style="color:white">
                    <td class="col-xs-3" ><asp:Literal ID="ltlistdate" runat="server">Date</asp:Literal></td>
                    <td class="col-xs-2" ><asp:Literal ID="ltlisttype" runat="server">Type</asp:Literal></td>
                    <td class="col-xs-4" ><asp:Literal ID="ltlistfromto" runat="server">Time</asp:Literal></td>
                    <td class="col-xs-2" ><asp:Literal ID="ltlisthours" runat="server">Hour(s)</asp:Literal></td>
                    <td class="col-xs-1" ></td>
                </tr>
                <asp:Repeater ID="repeater_clot" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <tr>
                        <td class="col-xs-3" ><asp:Literal ID="ltlistdate" runat="server" Text='<%#((MODEL.CLOT.CLOTItem)Container.DataItem).date.ToString("yyyy-MM-dd") %>'></asp:Literal></td>
                        <td class="col-xs-2" ><asp:Literal ID="ltlisttype" runat="server" Text='<%#((MODEL.CLOT.CLOTItem)Container.DataItem).type.ToString() %>'></asp:Literal></td>
                        <td class="col-xs-4" ><asp:Literal ID="ltlistfromto" runat="server" Text='<%#((MODEL.CLOT.CLOTItem)Container.DataItem).GetTimeRangeDesc() %>'></asp:Literal></td>
                        <td class="col-xs-2" ><asp:Literal ID="ltlisthours" runat="server" Text='<%#((MODEL.CLOT.CLOTItem)Container.DataItem).GetHoursFromStringMember()%>'></asp:Literal></td>
                        <td style="text-align:right"><asp:ImageButton ID="delete" Width="30px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="30px" ImageUrl="~/Res/images/close.png" runat="server" OnClick="delete_Click" /></td>
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <script src="../Res/App/apply.js?lastmodify=<%=BLL.GlobalVariate.applyjsLastmodify %>"></script>
    <asp:Literal ID="js_waitdiv" runat="server"/>
    <asp:Literal ID="lt_js_prg" runat="server"/>
    <script type="text/javascript">
        
        $(".datepicker1").datepicker({
            language: 'zh-CN', //语言
            autoclose: true, //选择后自动关闭
            clearBtn: true,//清除按钮
            format: "yyyy-mm-dd"//日期格式
        });
                </script>

</asp:Content>