<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Apply.aspx.cs" Inherits="WEBUI.Pages.Apply" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" id="mainpage">
        <div class="col-xs-12" style="height:16px; padding:0px"/>
        <table class="col-xs-12 lsu-tablem1">
            <tr>
                <td style="width:90px"><asp:Literal ID="lt_name" runat="server">Name</asp:Literal></td>
                <td>
                    <div style="float:left;"><asp:Literal ID="literal_applier" runat="server"></asp:Literal></div>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_leave" runat="server">Leave</asp:Literal></td>
                <td>
                    <asp:DropDownList ID="ddl_leavetype" runat="server" Width="90%">
                        <asp:ListItem Text="Please Select" Value="-1"  Selected="true"/>
                        <asp:ListItem Text="AL" Value="0"/>
                        <asp:ListItem Text="SL" Value="1"/>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_apply" runat="server">Apply</asp:Literal></td>
                <td>
                    <asp:label ID="lt_applydays" runat="server" Width="80px">0 Days</asp:label>
                    <asp:label ID="lt_balance" runat="server" Width="80px">Banlance</asp:label>
                    <asp:label ID="lt_balancedays" runat="server" Width="80px">9.2 Days</asp:label>

                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_date" runat="server">Date</asp:Literal></td>
                <td>
                    <asp:TextBox ID="tb_from" runat="server" Width="40%" ReadOnly="true"></asp:TextBox><asp:Button ID="btn_from" runat="server" Text="from" Visible="false"  /> To <asp:TextBox ID="tb_to" runat="server" Width="40%" ReadOnly="true"></asp:TextBox><asp:Button ID="btn_to" runat="server" Text="To" Visible="false"/>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_section" runat="server">Section</asp:Literal></td>
                <td><asp:DropDownList ID="dropdl_section" runat="server" Width="90%">
                    <asp:ListItem Text="Full day" Value="0"  Selected="true"/>
                    <asp:ListItem Text="AM" Value="1"/>
                    <asp:ListItem Text="PM" Value="2"/>
                    <asp:ListItem Text="3 Sections" Value="3"/>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><asp:Literal ID="lt_remarks" runat="server">Remarks</asp:Literal></td>
                <td><asp:TextBox ID="tb_remarks" runat="server" Width="90%"></asp:TextBox> </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div class="col-xs-8"><asp:ImageButton ID="ImageButton1" runat="server"   ImageUrl="~/Res/images/comIcon_canlendar.png" Width="40px" Height="40px" OnClick="ImageButton1_Click"/></div>
                    <div class="col-xs-2"><asp:ImageButton ID="ImageButton2" runat="server"   ImageUrl="~/Res/images/comIcon_addattence.png" Width="40px" Height="40px" OnClick="ImageButton2_Click"/></div>
                </td>
            </tr>
        </table>
        <div class=" col-xs-12" style="height:2px"></div>
        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-3">Date</td><td class="col-xs-4">Type</td><td class="col-xs-3">Section</td><td class="col-xs-2">  </td></tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="height:200px; overflow:scroll;">
            <table class="col-xs-12 lsu-table-sm">
                <asp:Repeater ID="repeater_leave" runat="server">
                    <ItemTemplate>
                        <tr><td class="col-xs-3"><%# ((BLL.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((BLL.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((BLL.Apply.LeaveData)Container.DataItem).section %></td><td class="col-xs-1"><asp:ImageButton ID="delete" Width="28px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="20px" ImageUrl="~/Res/images/close1.png" runat="server" OnClick="delete_Click" /><asp:HiddenField ID="testhidden" runat="server" Value="<%#((BLL.Apply.LeaveData)Container.DataItem).type %>" /></td></tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:aliceblue"><td class="col-xs-3"><%# ((BLL.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((BLL.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((BLL.Apply.LeaveData)Container.DataItem).section %></td><td class="col-xs-1"><asp:ImageButton ID="delete" Width="28px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="20px" ImageUrl="~/Res/images/close1.png" runat="server" OnClick="delete_Click" /><asp:HiddenField ID="testhidden" runat="server" Value="<%#((BLL.Apply.LeaveData)Container.DataItem).type %>" /></td></tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
       <%--<table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-12">Pic Refence</td></tr>
        </table>
        <div class="col-xs-12" style="height:80px;">
            <div class="col-xs-2" style="height:80px;line-height:80px; vertical-align:middle;padding:0px; width:40px" onclick="button_uploadpic_Click">
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Res/images/add.png"  Width="35px" Height="35px" OnClick="ImageButton2_Click"/>
            </div>
            <div class="col-xs-9 lsu-table-xs" style="height:78px;overflow-y:hidden; overflow-x:scroll; padding-left:5px">
                <table >
                    <tr>
                        <asp:Repeater ID="repeater_pic" runat="server">
                            <ItemTemplate>
                                <td style="padding-right:10px; width:90px"><asp:Image ID="Image" runat="server" ImageUrl="<%# ((WEBUI.Pages.Apply.UploadPic)Container.DataItem).path %>"  Width="50px" Height="50px"/>
                                <br><asp:ImageButton ID="btn_close" runat="server" ImageUrl="~/Res/images/close.png" Width="30px" Height="30px" OnClick="btn_close_Click" CommandArgument="<%#Container.ItemIndex%>" /></td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </div>
        </div>--%>
        <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
            <asp:Button ID="button_apply" runat="server" Text="Apply"  CssClass="btn lss-btncolor-blue" Width="160px" OnClick="button_apply_Click"/>
        </div>
    </div>
   </div>
    <div class="row" id="uploadpage" style="visibility:hidden">
        <asp:Button ID="Button1" runat="server" Text="Button" />
    </div>
    <!-- Modal upload pic -->
    <div class="modal col-xs-12" id="modal_upload" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true" >
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title" id="staticBackdropLabel">Upload Picture</h4>
          </div>
          <div class="modal-body">
              <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true"/>
              <br />
              <asp:Literal ID="literal_uploadMsg" runat="server"></asp:Literal>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" id="btn_closemodel" runat="server" >Close</button>
            <button type="button" class="btn btn-primary" id="btn_uploadpic" runat="server" >Upload</button>
          </div>
        </div>
      </div>
    </div>

  <%--  <!-- Modal calandar -->
    <div class="modal fade" id="modal_calendarFrom" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title">Pick Date</h4>
          </div>
          <div class="modal-body">
              <asp:Calendar ID="calendar" runat="server" OnSelectionChanged="calendar_SelectionChanged"></asp:Calendar>
          </div>
          <div class="modal-footer">
          </div>
        </div>
      </div>
    </div>

    <!-- Modal calandar -->
    <div class="modal fade" id="modal_calendarTo" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title">Pick Date</h4>
          </div>
          <div class="modal-body">
              <asp:Calendar ID="calendar1" runat="server" OnSelectionChanged="calendar_SelectionChanged"></asp:Calendar>
          </div>
          <div class="modal-footer">
          </div>
        </div>
      </div>
    </div>--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentjs" runat="server">
    <script src="../Res/App/apply.js"></script>
    <asp:Literal ID="lt_AlertJS" runat="server" EnableViewState="false"/>
    <asp:Literal ID="lt_model_upload" runat="server"/>
    <asp:Literal ID="lt_model_datafrom" runat="server"/>
    <asp:Literal ID="lt_model_datato" runat="server"/>
    
</asp:Content>