<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Check.aspx.cs" Inherits="WEBUI.Pages.Check"  EnableEventValidation="false" ViewStateMode="Enabled"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Res/App/check.css" rel="stylesheet" />
    <div class="row" id="div_buttons">
        <div class ="col-xs-12" style="padding:0px; padding-top:5px;">
            <div class="col-xs-12" style="height:45px">&nbsp;</div>
            <div class="col-xs-12 MaintTextColor_new" style="font-size:34px; text-align:center;">
                <asp:Label ID="lb_day" runat="server" Text="2021-01-02"></asp:Label>
            </div>
            <div class="col-xs-12" style="height:15px">&nbsp;</div>
            <div class="col-xs-12 MaintTextColor_new" style="font-size:24px; text-align:center;">
                <asp:Label ID="lb_time" runat="server" Text="09:01 PM"></asp:Label>
            </div>
            <div class="col-xs-12" style="height:50px">&nbsp;</div>
            <div class="col-xs-12" style="text-align:center"><%-->OnClick_In--%>
                <asp:Button ID="bt_checkin" OnClick="OnClick_In" runat="server" Text="Button" style="width:260px; height:260px; border:0px red solid; border-radius:130px; background-color:#06468c; color:#ffffff;font-size:46px" />
            </div>
            <div class="col-xs-12" style="height:5px">&nbsp;</div>
            <div class="col-xs-12 MaintTextColor_new" style="text-align:center;">
                <asp:Label ID="lb_msg" runat="server" Text="打卡：09:01 PM" Visible="false"></asp:Label>
            </div>
        </div>
    </div>
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true">
        <asp:ListItem Text="aa" Value="1"></asp:ListItem>
        <asp:ListItem Text="bb" Value="2"></asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="Button1" runat="server" Text="Button" />
    <!-- 模态框（Modal） -->
        <div class="modal fade col-xs-12" style="position:absolute; margin-top:20px;" id="modal_shifts" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
	        <div class="modal-dialog" style="width:200px;display:inline">
		        <div class="modal-content">
<%--			        <div class="modal-header">
				        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
					        &times;
				        </button>
				        <h4 class="modal-title" id="myModalLabel">
					        模态框（Modal）标题
				        </h4>
			        </div>--%>
			        <div class="modal-body">
                        <asp:Repeater ID="rp_shifts" runat="server">
                            <HeaderTemplate>
                                <table class=" table"><tr><td>Zone Code</td><td>Shift Code</td><td>Time</td></tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr><td><input id="Radio1" type="radio"  name="rd_shifts" value="<%#((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).ShiftCode %>"  <%#Container.ItemIndex==0?"checked='Checked'":"" %> /><%#((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).ZoneCode %></td><td><%#((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).ShiftCode %></td><td><%#((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).Time %></td>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
			        </div>
			        <div class="modal-footer">
                        <asp:Button ID="btn_model2_ok" runat="server"  Text="OK" OnClick="btn_model2_ok_Click"/>
                        <asp:Button ID="btn_model2_cancel" runat="server"  Text="cancel"  data-dismiss="modal" />
			        </div>
		        </div>
	        </div>
        </div>
    <!-- /.modal -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <asp:Literal ID="lt_jsModelWindow" runat="server"></asp:Literal>
    <script>
       setInterval("document.getElementById('ContentPlaceHolder1_lb_time').innerHTML=formatDate(new Date());", 10000);

       function formatDate(date) {
          var hours = date.getHours();
          var minutes = date.getMinutes();
          var ampm = hours >= 12 ? 'PM' : 'AM';
          hours = hours % 12;
          hours = hours ? hours : 12; // the hour '0' should be '12'
          minutes = minutes < 10 ? '0'+minutes : minutes;
          var strTime = hours + ':' + minutes + ' ' + ampm;
        return  strTime;
        }
    </script>
</asp:Content>