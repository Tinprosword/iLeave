<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Apply.aspx.cs" Inherits="WEBUI.Pages.Apply" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <table class="col-xs-12 lsu-table-sm">
            <tr>
                <td style="width:100px">Applier</td>
                <td>
                    <asp:Literal ID="literal_applier" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>Leave type</td>
                <td>
                    <asp:DropDownList ID="ddl_leavetype" runat="server" Width="90%">
                    <asp:ListItem Text="AL" Value="0"  Selected="true"/>
                    <asp:ListItem Text="SL" Value="1"/>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Leave date</td>
                <td>
                    <asp:TextBox ID="tb_from" runat="server" Width="40%" ReadOnly="true"></asp:TextBox><asp:Button ID="btn_from" runat="server" Text="from" Visible="false" OnClick="btn_from_Click" /> To <asp:TextBox ID="tb_to" runat="server" Width="40%" ReadOnly="true"></asp:TextBox><asp:Button ID="btn_to" runat="server" Text="To" Visible="false" OnClick="btn_to_Click" />
                </td>
            </tr>
            <tr>
                <td>Balance</td>
                <td><asp:Literal ID="literal_balance" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Section</td>
                <td><asp:DropDownList ID="dropdl_section" runat="server" Width="90%">
                    <asp:ListItem Text="Full day" Value="0"  Selected="true"/>
                    <asp:ListItem Text="AM" Value="1"/>
                    <asp:ListItem Text="DM" Value="2"/>
                    <asp:ListItem Text="3Part" Value="3"/>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Exclude</td>
                <td>
                    <asp:CheckBoxList ID="checkbl" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Text="Saturday" Value="0" Selected="true"/>
                        <asp:ListItem Text="Sunday" Value="0" Selected="true"/>
                        <asp:ListItem Text="Holiday" Value="0" Selected="true"/>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>Remarks</td>
                <td><asp:TextBox ID="tb_remarks" runat="server" Width="90%" TextMode="MultiLine" Rows="2"></asp:TextBox> </td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="button_addleave" runat="server" Text="Add Date" CssClass="btn lss-bgcolor-blue" OnClick="button_addleave_Click"/>&nbsp&nbsp
                    <asp:Button ID="button_uploadpic" runat="server" Text="Upload Pic" CssClass="btn lss-bgcolor-blue" OnClick="button_uploadpic_Click"/>
                </td>
            </tr>
        </table>
        <div class=" col-xs-12" style="height:2px"> </div>
        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-3">Data</td><td class="col-xs-5">Type</td><td class="col-xs-3">Section</td><td class="col-xs-1">  </td></tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="height:92px; overflow:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                <asp:Repeater ID="repeater_leave" runat="server">
                    <ItemTemplate>
                        <tr><td class="col-xs-3"><%# ((WEBUI.Pages.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((WEBUI.Pages.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((WEBUI.Pages.Apply.LeaveData)Container.DataItem).section %></td><td class="col-xs-1"><asp:ImageButton ID="delete" Width="18px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="18px" ImageUrl="~/Res/images/close1.png" runat="server" OnClick="delete_Click" /><asp:HiddenField ID="testhidden" runat="server" Value="<%#((WEBUI.Pages.Apply.LeaveData)Container.DataItem).type %>" /></td></tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:aliceblue"><td class="col-xs-3"><%# ((WEBUI.Pages.Apply.LeaveData)Container.DataItem).date %></td><td class="col-xs-5"><%#((WEBUI.Pages.Apply.LeaveData)Container.DataItem).type %></td><td class="col-xs-3"><%#((WEBUI.Pages.Apply.LeaveData)Container.DataItem).section %></td><td class="col-xs-1"><asp:ImageButton ID="delete" Width="18px" CommandName="itemindex" CommandArgument="<%#Container.ItemIndex%>" Height="18px" ImageUrl="~/Res/images/close1.png" runat="server" OnClick="delete_Click" /><asp:HiddenField ID="testhidden" runat="server" Value="<%#((WEBUI.Pages.Apply.LeaveData)Container.DataItem).type %>" /></td></tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </table>
        </div>
       <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-12">Pic Refence</td></tr>
        </table>
        <div class="col-xs-12" style="height:80px;  overflow-y:hidden; overflow-x:scroll; padding-left:5px">
            <table class="col-xs-12 lsu-table-xs" style="height:78px;">
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
        <div class="col-xs-12 lsf-center" style="padding-top:16px;">
            <asp:Button ID="button_apply" runat="server" Text="Apply"  CssClass="btn lss-bgcolor-blue" Width="160px" OnClick="button_apply_Click"/>
        </div>
    </div>
    <!-- Modal upload pic -->
    <div class="modal fade" id="modal_upload" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true">
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
            <button type="button" class="btn btn-primary" id="btn_closemodel" runat="server" onserverclick="btn_closemodel_ServerClick">Close</button>
            <button type="button" class="btn btn-primary" id="btn_uploadpic" runat="server" onserverclick="btn_uploadpic_ServerClick">Upload</button>
          </div>
        </div>
      </div>
    </div>

    <!-- Modal upload pic -->
    <div class="modal fade" id="modal_calendarfrom" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title">Pick Date</h4>
          </div>
          <div class="modal-body">
              <asp:Calendar ID="calendarfrom" runat="server" OnSelectionChanged="calendar_SelectionChanged"></asp:Calendar>
          </div>
          <div class="modal-footer">
          </div>
        </div>
      </div>
    </div>

    <!-- Modal upload calendar2 -->
    <div class="modal fade" id="modal_calendarto" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h4 class="modal-title">Pick Date</h4>
          </div>
          <div class="modal-body">
              <asp:Calendar ID="calendarto" runat="server" OnSelectionChanged="calendar_SelectionChanged2"></asp:Calendar>
          </div>
        </div>
      </div>
    </div>

</asp:Content>
<asp:Content ContentPlaceHolderID="contentjs" runat="server">
    <asp:Literal ID="lt_AlertJS" runat="server" EnableViewState="false"/>
    <asp:Literal ID="lt_model_upload" runat="server"/>
    <asp:Literal ID="lt_model_datafrom" runat="server"/>
    <asp:Literal ID="lt_model_datato" runat="server"/>
</asp:Content>