<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Check.aspx.cs" Inherits="WEBUI.Pages.Check"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Res/App/check.css" rel="stylesheet" />
    <asp:HiddenField ID="hf_nowifi" Value="no wifi" runat="server" />
    <asp:HiddenField ID="hf_nogps" Value="no gps" runat="server" />
    <asp:HiddenField ID="hf_cantconvertGps" Value="can not convert gps" runat="server" />

    <asp:HiddenField ID="hf_back_isapp" Value="" runat="server" />
    <asp:HiddenField ID="hf_back_iszone" Value="" runat="server" />
    <asp:HiddenField ID="hf_back_invaliddata" Value="" runat="server" />
    <asp:HiddenField ID="hf_back_wifiname" Value="" runat="server" />
    <asp:HiddenField ID="hf_back_gpslatlon" Value="" runat="server" />
    <asp:HiddenField ID="hf_back_gpsDecode" Value="" runat="server" />
    <asp:HiddenField ID="hf_back_time1" Value="" runat="server" />
    <asp:HiddenField ID="hf_back_time2" Value="" runat="server" />

    <div class="row" id="div_buttons">
        <div class ="col-xs-12" style="padding:0px; padding-top:5px;">
            <div class="col-xs-12" style="height:45px">&nbsp;</div>
            <div class="col-xs-12 MaintTextColor_new" style="font-size:18px; text-align:center;">
                <asp:Label ID="lb_day" runat="server" Text="2021-01-02"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lb_time" runat="server" Text="09:01 PM"></asp:Label>
            </div>
            <div class="col-xs-12" style="height:15px">&nbsp;</div>
            <div class="col-xs-12 MaintTextColor_new" style="font-size:18px; text-align:center;">
                <asp:Panel id="panel_appmsg" runat="server" Visible="false">
                    <asp:Label ID="lb_LOCATIONPRE" runat="server" Text="GPS:"/><asp:Label ID="lb_locationname" runat="server" Text="--Waiting--"></asp:Label>
                    <br/><asp:Label ID="LB_WIFIPRE" runat="server" Text="WIFI:"/><asp:Label ID="lb_wifi" runat="server"    Text="--Waiting--"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="panel_LocalZone" runat="server" Visible="false" CssClass="lsu-litterBlue">
                    <asp:Label ID="lb_localzonePrefix" runat="server" Text="Zone:"/><asp:Label ID="lb_localzoneDesc" runat="server" Text="Team A"></asp:Label>
                </asp:Panel>
            </div>
            <div class="col-xs-12" style="height:20px">&nbsp;</div>
            <div class="col-xs-12" style="text-align:center"><%-->OnClick_In--%>
                <asp:Button ID="bt_checkin" OnClientClick="ClickCheckin();return false;"  runat="server" Text="Button" style="width:280px; height:280px; border:0px red solid; border-radius:140px; background-color:#06468c; color:#ffffff;font-size:37px" /><%--OnClientClick="if(confirm(onclickCheck())){return true;}else{return false;}"--%>
            </div>
            <div class="col-xs-12" style="height:5px">&nbsp;</div>
            <div class="col-xs-12 MaintTextColor_new" style="text-align:center;">
                <asp:Label ID="lb_commonmsg" runat="server" Text="數據錯誤。" Visible="false"></asp:Label>
                <asp:Label ID="lb_msg_current" runat="server" Text="本次打卡：09:01 PM" Visible="false"></asp:Label><br />
                <asp:Label ID="lb_msg_current_gpswifi" runat="server" Text="中國，wifi:abc" Visible="false"></asp:Label><br />
                <asp:Label ID="lb_msg2_pre" runat="server" Text="上次打卡：09:01 PM" Visible="false"></asp:Label><br />
                <asp:Label ID="lb_msg2_pregpswifi" runat="server" Text="中國，wifi:abc" Visible="false"></asp:Label><br />
                <asp:Label ID="lb_msg2_pre2" runat="server" Text="上次打卡：09:01 PM" Visible="false"></asp:Label><br />
                <asp:Label ID="lb_msg2_pregpswifi2" runat="server" Text="中國，wifi:abc" Visible="false"></asp:Label><br />
            </div>
        </div>
    </div>
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
                        <asp:Repeater ID="rp_shifts" runat="server" OnLoad="rp_shifts_Load">
                            <HeaderTemplate>
                                <table class=" table"><tr><td>Zone Code</td><td>Shift Code</td><td>Time</td></tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><input type="radio" id="rd_shift" name="rd_shiftname" runat="server" value="<%#((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).ShiftCode %>" onclick="return selectSingleRadio(this,'rd_shiftname');" checked="<%#Container.ItemIndex==0?true:false %>"/>&nbsp;&nbsp;&nbsp;&nbsp;<%#((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).ZoneCode %></td>
                                    <td><%#((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).ShiftCode %></td>
                                    <td><%#((WebServiceLayer.WebReference_leave.v_System_Calendar)Container.DataItem).Time %></td>
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
<div class="modal fade col-xs-12" style="position:absolute; margin-top:20px;" id="modal_location" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog" style="width:200px;display:inline">
        <div class="modal-content">
	        <div class="modal-body">
                <div id="abc">
                    <div class="col-xs-12">
                        <asp:Label ID="lb_model_location_gps" runat="server" Text="中国 深圳"></asp:Label>
                    </div>
                    <div class="col-xs-12">
                        <asp:Label ID="lb_model_location_wifi" runat="server" Text="WIFI:中国"></asp:Label>
                    </div>
                </div>
	        </div>
	        <div class="modal-footer">
                <asp:Button ID="btn_model_location_ok" runat="server"  Text="OK"/>
                <asp:Button ID="btn_model_location_cancel" runat="server"  Text="cancel"  data-dismiss="modal" />
	        </div>
        </div>
    </div>
</div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <script>
        function selectSingleRadio(rbtn1, GroupName) {  
            $("input[type=radio]").each(function (i) {  
                if (this.name.substring(this.name.length - GroupName.length) == GroupName) {  
                    this.checked = false;  
                }
            })
            rbtn1.checked = true;  
        }

        function onclickCheck() {
            var msg = '<%=clickConfirmMsg()%>';
            return msg;
        }
    </script>

    <asp:Literal ID="lt_jsModelWindow" runat="server"></asp:Literal>
    <asp:Literal ID="lt_jsmsg" runat="server"></asp:Literal>
    <script src="../Res/App/check.js?lastmodify=<%=BLL.GlobalVariate.checkjsLastmodify %>"></script>
    <asp:Literal ID="lt_jsTimerRequestMobileLocation" runat="server"></asp:Literal>
    <asp:Literal ID="lt_jsLoginout" runat="server"></asp:Literal>
</asp:Content>