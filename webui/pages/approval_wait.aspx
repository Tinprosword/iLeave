<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="approval_wait.aspx.cs" Inherits="WEBUI.Pages.approval_wait" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <%--approver's menu.--%>
        <ul id="myTabApproval" class="nav nav-tabs lsf-clearPadding modifyTab" runat="server">
	        <li id="myTabApproval_pending" runat="server"><a style="padding-top:5px; padding-bottom:3px;padding-left:10px; padding-right:10px; " data-toggle="tab" runat="server" id="Approvalpending" onclick="window.location.href='approval_wait.aspx?action=0&applicationtype=0&from=0'"><asp:Literal ID="lt_pending" runat="server" Text="Pending"/></a></li>
	        <li id="myTabApproval_history" runat="server"><a style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px; " data-toggle="tab" runat="server" id="ApprovalHistory" onclick="window.location.href='approval_wait.aspx?action=0&applicationtype=3&from=0'"><asp:Literal ID="lt_processed" runat="server" Text="History"/></a></li>
        </ul>
        <%--applyer's menu--%>
        <ul id="myTabApply" class="nav nav-tabs lsf-clearPadding modifyTab" runat="server">
	        <li id="myTabapply_new" runat="server"><a style="padding-top:5px; padding-bottom:3px;padding-left:10px; padding-right:10px; " data-toggle="tab" onclick="<%=showNewLink() %>"><asp:Literal ID="lt_new" runat="server" Text="New"></asp:Literal></a></li>
	        <li id="myTabapply_pending" runat="server"><a style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px; " data-toggle="tab" onclick="window.location.href='approval_wait.aspx?action=1&applicationtype=0&from=<%=from%>'"><asp:Literal ID="lt_mypending" runat="server" Text="Pending"/></a></li>
            <li id="myTabapply_history" runat="server"><a style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px; " data-toggle="tab" onclick="window.location.href='approval_wait.aspx?action=1&applicationtype=3&from=<%=from%>'"><asp:Literal ID="lt_myhistory" runat="server" Text="History"/></a></li>
            <li id="myTabapply_es" runat="server" style="padding-left:0px;margin-left:0px;"><a style="padding-top:4px; padding-left:10px; padding-right:10px; padding-bottom:4px;" data-toggle="tab" onclick="window.location.href='estimation.aspx'"><asp:Literal ID="lt_estimation" runat="server" Text="Estimation"/></a></li>
        </ul>
    </div>
    <div id="ajaxContainer" class="col-xs-12 lsf-clearPadding"></div>
    <div class ="col-xs-12" style="height:10px; padding:0px">&nbsp</div>
    <div class="row" style="margin-top:10px;">
        <div class="col-xs-4" style="padding-left:14px; width:74px">
            <asp:DropDownList ID="ddl_year" runat="server"  Height="26px" OnSelectedIndexChanged="ddl_year_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-xs-4" style="width:136px; padding-right:1px; padding-left:2px; font-size:unset; font-weight:normal">
            <asp:RadioButtonList ID="rbl_sourceType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="99%" Font-Bold="false" AutoPostBack="true" OnSelectedIndexChanged="rbl_sourceType_SelectedIndexChanged" CssClass="overlableSize14">
                <asp:ListItem Text="Leave" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="CL/OT" Value="1"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="col-xs-3" style=" padding-right:1px;padding-left:2px;">
            <asp:TextBox ID="tb_staff" Width="100%"  runat="server"></asp:TextBox>
        </div>
        <div class="col-xs-1 lsf-clearPadding" style="width:30px;"><asp:ImageButton ID="ib_search" OnClick="ib_search_Click"  ImageUrl="~/Res/images/search.png" runat="server" Width="28px" Height="26px" /></div>
        <div class="col-xs-12" runat="server" visible="false" id="div_batchApprove">
            <div class="col-xs-4 lsf-clearPadding">
                <asp:CheckBox ID="cb_batch" runat="server" Text="Select All" OnCheckedChanged="cb_batch_CheckedChanged" AutoPostBack="true" class="radioFontNormal"/>
            </div>
            <div class="col-xs-8 lsf-clearPadding">
                <asp:Label ID="lb_batchSelected" runat="server" Text="Selected"></asp:Label>:<asp:Label ID="lb_checkedNumber" runat="server" Text="0" style="font-weight:600"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btn_batchApprover" runat="server" Text="Batch Approve" OnClick="btn_batchApprover_Click" style="border:2px solid #8da9cd;background-color:white; width:110px;"/>
            </div>
        </div>
        <div class="col-xs-12" style="" id="div_error" runat="server" visible="false">
            <asp:Label ID="lb_errormsg" runat="server" class="col-xs-12" style="color:red;">hiabc</asp:Label>
        </div>
    </div>
    <div class="row" style="padding-bottom:0px;margin-top:1px; height:500px;overflow-y:scroll" id="maindata" onscroll="setScrollTop()">
        <asp:Repeater ID="rp_list" runat="server" OnItemDataBound="rp_list_ItemDataBound">
            <ItemTemplate>
                <asp:HiddenField ID="hf_leave_requestid" runat="server"  Value=<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                <asp:HiddenField ID="hf_leave_requeststatus" runat="server"  Value=<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status.ToString()%>/>
                <div class="col-xs-12" style=" line-height:8px;text-align:center;padding:0px;  margin:0px; padding-top:1px; padding-bottom:4px" onclick="MyPostBack('detail',<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID %>)">
                    <label class="lsf-clearPadding" style="padding:0px;  margin:0px;height:1px;background-color:dimgray; width:90%; padding-left:3px; padding-right:3px;"></label>
                </div>
                <div class="col-xs-12 divheighter">
                    <asp:CheckBox ID="cb_leave" runat="server" Text="" Visible="<%#BShow_BatchApprove(GetBigRange(),dataType_myselfOrMyManage) %>" OnCheckedChanged="cb_leave_CheckedChanged" AutoPostBack="true"/>
                    <asp:Label ID="lb_name" runat="server"><%#GetStaffName((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem)%></asp:Label>
                    <div style="float:right">
                        <asp:Label ID="lb_status" runat="server"><%#GetLeaveStatus((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem)%></asp:Label> &nbsp; <a title="<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>" style="cursor:pointer;color:White;background-color:#2573A4;font-size:16px;height:34px; padding:4px;" onclick="SingleResult('../webservices/leave.asmx/GetLeaveDetail_html',{requestID:<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>,leaveid:<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).MinLeaveID%>,staff:<%# GetStaffid((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem)%>,employmentNo:<%#((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).employmentID%>,lan:<%#(int)BLL.MultiLanguageHelper.GetChoose()%>},'string',onGetData)"><%=BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_link_detail %></a>
                    </div>
                </div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_date" runat="server"><%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetFromto(BLL.MultiLanguageHelper.GetLanguagePacket().approval_To,BLL.MultiLanguageHelper.GetLanguagePacket().approval_Day)%></asp:Label></div>
                <div class="col-xs-12 divheighter"><asp:Label ID="lb_leave" runat="server"><%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetLeaveDesc(BLL.MultiLanguageHelper.GetLanguagePacket().Common_lable_cancel)%></asp:Label></div>
                <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_applydate %>:<%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetApplydate()%></div>
                <div class="col-xs-12 " style="margin-bottom:4px;">
                    <%--<div class="col-xs-4 lsf-clearPadding" style="width:40px;">--%><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_applyRemark %>:<%--</div>--%><%--<div class="col-xs-8 lsf-clearPadding">--%><%# new WebServiceLayer.MyModel.LeaveMaster((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Info_GetApprovalRemark()%><%--</div>--%></div>
                <div class="col-xs-12 divheighter" style="margin-bottom:2px;">
                    <table style="width:90%;">
                         <tr>
                             <td style="white-space: nowrap"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_attachment %>:</td>
<%--                             <td style="width:30px;min-width:30px; height:30px; min-height:30px" id="tr_left" runat="server" visible="false">
                                 <img id="btnleft" src="../Res/images/add.png" style="width:100%; height:100%"  runat="server"/>
                             </td>--%>
                             <td style="width:240px;padding-left:1px;padding-right:1px;">
                                 <div id="fullf" class="hiddenScrollbar_NoWrap" style="width:238px;" runat="server">
                                     <asp:Literal ID="lt_attachlist" runat="server"></asp:Literal>
                                </div>
                             </td>
<%--                             <td style="width:30px;min-width:30px; height:30px; min-height:30px" id="tr_right" runat="server" visible="false">
                                 <img id="btnright" src="../Res/images/add.png" style="width:100%; height:100%"  runat="server"/>
                             </td>--%>
                         </tr>
                    </table>


                </div>
                <asp:Panel ID="panel_admin_waitingApprove" runat="server" Visible="<%#BShow_WaitApplyPanel(GetBigRange(),((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_approverRemark %>:</div>
                    <div class="col-xs-12">
                        <asp:TextBox ID="tb_waitapproveRemark" runat="server" TextMode="MultiLine" Width="100%" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5 lsf-clearPadding" style="float:left;">
                            <asp:Button ID="btn_approve_approve" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_approve %>" style="border:2px solid #8da9cd;background-color:white; width:100px;" OnClick="btn_Click" CommandArgument=<%#"1|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                        <div class="col-xs-5 lsf-clearPadding" style="float:right; text-align:right;">
                            <asp:Button ID="btn_approve_reject" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_reject %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"2|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_admin_waitingCancel" runat="server" Visible="<%#BShow_WaitCancelPanel(GetBigRange(),((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_approverRemark %>:</div>
                    <div class="col-xs-12">
                        <asp:TextBox ID="tb_waitcancelRemark" runat="server" TextMode="MultiLine" Width="100%" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5 lsf-clearPadding" style="float:left; ">
                            <asp:Button ID="btn_cancel_approve" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_approve %>" style="border:2px solid #8da9cd;background-color:white; width:100px;" OnClick="btn_Click"  CommandArgument=<%#"3|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                        <div class="col-xs-5 lsf-clearPadding" style="float:right; text-align:right;">
                            <asp:Button ID="btn_cancel_reject" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_reject %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"4|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_user_waiting" runat="server" Visible="<%#BShow_UserWaitingPanel(GetBigRange(),((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-12">
                        <div class="col-xs-5 lsf-clearPadding" style="float:left;">
                            <asp:Button ID="Button2" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_withdraw %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"5|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_user_approved" runat="server" Visible="<%#BShow_UserApprovedPanel(GetBigRange(),((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-12">
                        <div class="col-xs-5 lsf-clearPadding" style="float:left;">
                            <asp:Button ID="Button1" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_cancel %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click" CommandArgument=<%#"6|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.LeaveRequestMaster)Container.DataItem).RequestID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <div class="col-xs-12" style="height:5px;">&nbsp;</div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Repeater ID="rp_clot" runat="server" OnItemDataBound="rp_clot_list_ItemDataBound">
            <ItemTemplate>
                <asp:HiddenField ID="hf_clot_requestid" runat="server"  Value=<%#((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).ID.ToString()%>/>
                <asp:HiddenField ID="hf_clot_requeststatus" runat="server"  Value=<%#((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).Status.ToString()%>/>
                <div class="col-xs-12" style=" line-height:8px;text-align:center;padding:0px;  margin:0px; padding-top:1px; padding-bottom:4px">
                    <label class="lsf-clearPadding" style="padding:0px;  margin:0px;height:1px;background-color:dimgray; width:90%; padding-left:3px; padding-right:3px;"></label>
                </div>
                <div class="col-xs-12 divheighter">
                    <asp:CheckBox ID="cb_clot" runat="server" Text="" Visible="<%#BShow_BatchApprove(GetBigRange(),dataType_myselfOrMyManage) %>" OnCheckedChanged="cb_clot_CheckedChanged" AutoPostBack="true"/>
                    <asp:Label ID="lb_name" runat="server"><%# GetStaffName((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem) %></asp:Label>
                    <div style="float:right">
                        <%#ShowClotStatus((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem)%>
                         &nbsp; <a style="cursor:pointer;color:White;background-color:#2573A4;font-size:16px;height:34px; padding:4px;" onclick="SingleResult('../webservices/leave.asmx/GetCLOTDetail_html',{requestID:<%#((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).ID%>,leaveid:<%#((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).ActualID%>,staff:<%#((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).StaffID%>,employmentNo:<%#((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).EmploymentID%>,lan:<%#(int)BLL.MultiLanguageHelper.GetChoose()%>},'string',onGetData)"><%=BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_link_detail %></a>
                    </div>
                </div>
                <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().clot_day %>:<%#BLL.CLOT.showCLOTTime((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem) %></div>
                <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().clot_Type%>:<%#ShowClotName(((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).Type) %></div>
                <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_applydate %>:<%#((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).CreateDate.ToString("yyyy-MM-dd") %></div>
                <div class="col-xs-12 " style="margin-bottom:4px;"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_applyRemark %>:<%#((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).Remarks %></div>
<%--                <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_attachment %>:</div>--%>

                <div class="col-xs-12 divheighter" style="margin-bottom:2px;">
                    <table style="width:90%;">
                         <tr>
                             <td style="white-space: nowrap"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_attachment %>:</td>
<%--                             <td style="width:30px;min-width:30px; height:30px; min-height:30px" id="tr_left" runat="server" visible="false">
                                 <img id="btnleft" src="../Res/images/add.png" style="width:100%; height:100%"  runat="server"/>
                             </td>--%>
                             <td style="width:240px;padding-left:1px;padding-right:1px;">
                                 <div id="fullf" class="hiddenScrollbar_NoWrap" style="width:238px;" runat="server">
                                     <asp:Literal ID="lt_attachlist" runat="server"></asp:Literal>
                                </div>
                             </td>
<%--                             <td style="width:30px;min-width:30px; height:30px; min-height:30px" id="tr_right" runat="server" visible="false">
                                 <img id="btnright" src="../Res/images/add.png" style="width:100%; height:100%"  runat="server"/>
                             </td>--%>
                         </tr>
                    </table>


                </div>

                <asp:Panel ID="panel_admin_waitingApprove" runat="server" Visible="<%#BShow_WaitApplyPanel_clot(GetBigRange(),((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_approverRemark %>:</div>
                    <div class="col-xs-12">
                        <asp:TextBox ID="tb_waitapproveRemark" runat="server" TextMode="MultiLine" Width="100%" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5 lsf-clearPadding" style="float:left; ">
                            <asp:Button ID="btn_approve_approve" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_approve %>" style="border:2px solid #8da9cd;background-color:white; width:100px;" OnClick="btn_Click_clot" CommandArgument=<%#"1|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).ID%>/>
                        </div>
                        <div class="col-xs-5 lsf-clearPadding" style="float:right; text-align:right;">
                            <asp:Button ID="btn_approve_reject" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_reject %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click_clot" CommandArgument=<%#"2|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).ID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_admin_waitingCancel" runat="server" Visible="<%#BShow_WaitCancelPanel_clot(GetBigRange(),((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-12 divheighter"><%=BLL.MultiLanguageHelper.GetLanguagePacket().approval_approverRemark %>:</div>
                    <div class="col-xs-12">
                        <asp:TextBox ID="tb_waitcancelRemark" runat="server" TextMode="MultiLine" Width="100%" Height="40px"></asp:TextBox>
                    </div>
                    <div class="col-xs-12">
                        <div class="col-xs-5 lsf-clearPadding" style="float:left;">
                            <asp:Button ID="btn_cancel_approve" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_approve %>" style="border:2px solid #8da9cd;background-color:white; width:100px;" OnClick="btn_Click_clot"  CommandArgument=<%#"3|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).ID%>/>
                        </div>
                        <div class="col-xs-5 lsf-clearPadding" style="float:right; text-align:right;">
                            <asp:Button ID="btn_cancel_reject" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_reject %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click_clot" CommandArgument=<%#"4|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).ID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_user_waiting" runat="server" Visible="<%#BShow_UserWaitingPanel_clot(GetBigRange(),((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-12">
                        <div class="col-xs-5 lsf-clearPadding" style="float:left;">
                            <asp:Button ID="Button2" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_withdraw %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click_clot" CommandArgument=<%#"5|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).ID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="panel_user_approved" runat="server" Visible="<%#BShow_UserApprovedPanel_clot(GetBigRange(),((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).Status,dataType_myselfOrMyManage) %>">
                    <div class="col-xs-12">
                        <div class="col-xs-5 lsf-clearPadding" style="float:left;">
                            <asp:Button ID="Button1" runat="server" Text="<%#BLL.MultiLanguageHelper.GetLanguagePacket().application_detail_btn_cancel %>" style="border:2px solid #cd7a7a;background-color:white;width:100px;" OnClick="btn_Click_clot" CommandArgument=<%#"6|"+Container.ItemIndex+"|"+((WebServiceLayer.WebReference_leave.StaffCLOTRequest)Container.DataItem).ID%>/>
                        </div>
                    </div>
                </asp:Panel>
                <div class="col-xs-12" style="height:5px;">&nbsp;</div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <asp:Literal ID="js_error" runat="server"></asp:Literal>
    <asp:Literal ID="js_waitdiv" runat="server"></asp:Literal>
    <script src="../Res/App/applywait.js"></script>
    <asp:Literal ID="lt_jsscrolltop" runat="server"></asp:Literal>
    <script><asp:Literal ID="lt_jsScroll" runat="server"></asp:Literal></script>
    <script>
        function setScrollTop()
        {
            var topvalue = $('#maindata').scrollTop();
            setCookie("st", topvalue);
        }
    </script>
</asp:Content>