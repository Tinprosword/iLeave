﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="approval_wait.aspx.cs" Inherits="WEBUI.Pages.approval_wait" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="showdiv" class="col-xs-10" style="left:9%;display:none;"> 
        <div class="center-box3;" style="float:right; margin-right:5px; padding-right:0px;margin-top:5px;padding-top:0px;" onclick="closeWindow()"><img src="../Res/images/close.png"  style="width:27px; height:27px"/></div>
        <div class=" col-xs-12 lsf-clearPadding">
            <div style="height:15px">&nbsp;</div>
            Balance:<label id="lbbalance"></label>
            <div style="height:100px; overflow-y:scroll;">
                <table class="col-xs-12 lsu-table-xs lsf-clearPadding">
                    <tr class="lss-bgcolor-blue" style="color:white; height:24px;">
                        <td class="col-xs-3" style="width:18%">date<asp:Literal ID="ltlistdate" runat="server"></asp:Literal></td>
                        <td class="col-xs-4" style="width:44%">type<asp:Literal ID="ltlisttype" runat="server"></asp:Literal></td>
                        <td class="col-xs-4" style="width:24%">section<asp:Literal ID="lt_listsection" runat="server"></asp:Literal></td>
                        <td class="col-xs-1" style="width:14%">  </td>
                    </tr>
                    <tr>
                        <td class="col-xs-3" style="width:18%">1</td>
                        <td class="col-xs-4" style="width:44%">2</td>
                        <td class="col-xs-4" style="width:24%">3</td>
                        <td class="col-xs-1" style="width:14%">4</td>
                    </tr>
                </table>
            </div>
            History:<label id="bookname"></label>
        </div>
    </div>
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>
    <div class="row" style="padding-bottom:10px;margin-top:10px; height:23px;">
        <div class="col-xs-4" style="padding-left:15px; width:80px">
            <asp:DropDownList ID="ddl_year" runat="server" Width="60px" Height="26px" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="2018">2018</asp:ListItem>
                <asp:ListItem Value="2019">2019</asp:ListItem>
                <asp:ListItem Value="2020" Selected="True">2020</asp:ListItem>
                <asp:ListItem Value="2021">2021</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-xs-4">
            <asp:TextBox ID="tb_staff" runat="server" AutoPostBack="true"  OnTextChanged="tb_staff_TextChanged"></asp:TextBox>
        </div>
    </div>
    <div class="row" style="padding-bottom:0px;margin-top:10px; height:510px;overflow-y:scroll">
        <asp:Repeater ID="rp_list" runat="server">
            <ItemTemplate>
                <div class="col-xs-12" style=" line-height:8px;text-align:center;padding:0px;  margin:0px; padding-top:1px; padding-bottom:4px" onclick="MyPostBack('detail',<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID %>)">
                    <label class="lsf-clearPadding" style="padding:0px;  margin:0px;height:1px;background-color:dimgray; width:90%; padding-left:3px; padding-right:3px;"></label>
                </div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_name" runat="server"><%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).uname%></asp:Label><img alt="Detail" src="../Res/images/details.png" style="width:24px; height:24px; float:right; margin-right:10px;" onclick="ModelsResult('../webservices/leave.asmx/testws',{},'Data_GetLeaveDetail',onGetData)"/></div><%//function ModelsResult(url, Postdata, rootname, eachFun)  requestID:26063,leaveCode:'aa',staff:'sid',employmentNo:'eid'%>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_date" runat="server"><%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetFromto()%></asp:Label></div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_leave" runat="server"><%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetBalance()%></asp:Label></div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_applydate" runat="server" Text="Apply Date:2019-02-05"></asp:Label></div>
                <div class="col-xs-12 divheighter">Apply Remark:<asp:Label ID="lb_applyRemark" runat="server"></asp:Label></div>
                <div class="col-xs-12 divheighter">
                    <asp:Label ID="lb_attachment" runat="server" Text="Attachment:"></asp:Label>
                    <%# GetAttachmentHtml(   ((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID  )%>
                </div>
                <asp:Panel ID="panel_admin_waitingApprove" runat="server" Visible="<%#BShow_WaitApplyPanel(theBigrange,((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,actionType) %>">
                    <div class="col-xs-12 divheighter"><asp:Label ID="Label1" runat="server" Text="Approval Remark:"></asp:Label></div>
                    <div class="col-xs-12">
                        <asp:TextBox ID="tb_waitapproveRemark" runat="server" TextMode="MultiLine" Width="98%" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5" style="float:left; padding-left:5px;">
                            <asp:Button ID="btn_approve_approve" runat="server" Text="Approval" style="border:2px solid #8da9cd;background-color:white; width:100px;" OnClick="btn_Click" CommandArgument=<%#"1|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                        <div class="col-xs-5" style="float:right; text-align:right;padding-left:5px;">
                            <asp:Button ID="btn_approve_reject" runat="server" Text="Reject" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"2|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_admin_waitingCancel" runat="server" Visible="<%#BShow_WaitCancelPanel(theBigrange,((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,actionType) %>">
                    <div class="col-xs-12 divheighter"><asp:Label ID="Label2" runat="server" Text="Remark"></asp:Label></div>
                    <div class="col-xs-12">
                        <asp:TextBox ID="tb_waitcancelRemark" runat="server" TextMode="MultiLine" Width="98%" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5" style="float:left; padding-left:5px;">
                            <asp:Button ID="btn_cancel_approve" runat="server" Text="Approval" style="border:2px solid #8da9cd;background-color:white; width:100px;" OnClick="btn_Click"  CommandArgument=<%#"3|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                        <div class="col-xs-5" style="float:right; text-align:right;padding-left:5px;">
                            <asp:Button ID="btn_cancel_reject" runat="server" Text="Reject" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"4|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_user_waiting" runat="server" Visible="<%#BShow_UserWaitingPanel(theBigrange,((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,actionType) %>">
                    <div class="col-xs-5" style="float:left; padding-left:15px;">
                        <asp:Button ID="Button2" runat="server" Text="Withdraw" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"5|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_user_approved" runat="server" Visible="<%#BShow_UserApprovedPanel(theBigrange,((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,actionType) %>">
                    <div class="col-xs-5" style="float:left; padding-left:15px;">
                        <asp:Button ID="Button1" runat="server" Text="Cancel" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"6|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                    </div>
                </asp:Panel>
                <div class="col-xs-12" style="height:5px;">&nbsp;</div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <asp:Literal ID="js_waitdiv" runat="server"></asp:Literal>
    <script>
        function onGetData(i, obj) {
            //alert("success acess");
            var name = getMember(obj, "balance");
            //alert(name);
            $("#lbbalance").text(name);
            showWindow('ongetdate');
        }
    </script>
</asp:Content>