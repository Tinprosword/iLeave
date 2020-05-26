<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Apply.aspx.cs" Inherits="WEBUI.Pages.Apply" %>
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
                <td>Balance</td>
                <td><asp:Literal ID="literal_balance" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>Leave date</td>
                <td>
                    <asp:TextBox ID="tb_from" runat="server" Width="40%" ReadOnly="true"></asp:TextBox> To <asp:TextBox ID="tb_to" runat="server" Width="40%" ReadOnly="true"></asp:TextBox>
                </td>
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
                <td><asp:Button ID="button_addleave" runat="server" Text="Add Date" CssClass="btn btn-primary"/>&nbsp&nbsp
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#staticBackdrop">Upload Pic</button>
                </td>
            </tr>
        </table>
        <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-3">Data</td><td class="col-xs-5">Type</td><td class="col-xs-3">Section</td><td class="col-xs-1">  </td></tr>
        </table>
        <div class="col-xs-12 lsf-clearPadding" style="height:92px; overflow:scroll;">
            <table class="col-xs-12 lsu-table-xs">
                <tr><td class="col-xs-3">05-01周五</td><td class="col-xs-5">Sick leave</td><td class="col-xs-3">Full day</td><td class="col-xs-1">*****</td></tr>
                <tr><td class="col-xs-3">05-01周五</td><td class="col-xs-5">Sick leave</td><td class="col-xs-3">Full day</td><td class="col-xs-1">*****</td></tr>
                <tr><td class="col-xs-3">05-01周五</td><td class="col-xs-5">Sick leave</td><td class="col-xs-3">Full day</td><td class="col-xs-1">*****</td></tr>
                <tr><td class="col-xs-3">05-01周五</td><td class="col-xs-5">Sick leave</td><td class="col-xs-3">Full day</td><td class="col-xs-1">*****</td></tr>
                <tr><td class="col-xs-3">05-01周五</td><td class="col-xs-5">Sick leave</td><td class="col-xs-3">Full day</td><td class="col-xs-1">*****</td></tr>
                <tr><td class="col-xs-3">05-01周五</td><td class="col-xs-5">Sick leave</td><td class="col-xs-3">Full day</td><td class="col-xs-1">*****</td></tr>
                <tr><td class="col-xs-3">05-01周五</td><td class="col-xs-5">Sick leave</td><td class="col-xs-3">Full day</td><td class="col-xs-1">*****</td></tr>
                <tr><td class="col-xs-3">05-01周五</td><td class="col-xs-5">Sick leave</td><td class="col-xs-3">Full day</td><td class="col-xs-1">*****</td></tr>
            </table>
        </div>
       <table class="col-xs-12 lsu-table-xs lss-bgcolor-blue" style="color:white;">
            <tr><td class="col-xs-12">Pic Refence</td></tr>
        </table>
        <div class="col-xs-12" style="height:80px; overflow:scroll">
            <table class="col-xs-12 lsu-table-xs" style="height:78px;">
                <tr>
                    <td style="padding-right:10px"><asp:Image ID="Image1" runat="server" ImageUrl="../res/images/setting.gif"  Width="78px" Height="78px"/></td>
                    <td style="padding-right:10px"><asp:Image ID="Image2" runat="server" ImageUrl="../res/images/setting.gif"  Width="78px" Height="78px"/></td>
                    <td style="padding-right:10px"><asp:Image ID="Image3" runat="server" ImageUrl="../res/images/setting.gif"  Width="78px" Height="78px"/></td><td style="padding-right:10px"><asp:Image ID="Image4" runat="server" ImageUrl="../res/images/setting.gif"  Width="78px" Height="78px"/></td>

                    <td style="padding-right:10px"><asp:Image ID="Image5" runat="server" ImageUrl="../res/images/setting.gif"  Width="78px" Height="78px"/></td><td style="padding-right:10px"><asp:Image ID="Image6" runat="server" ImageUrl="../res/images/setting.gif"  Width="78px" Height="78px"/></td>

                    <td style="padding-right:10px"><asp:Image ID="Image7" runat="server" ImageUrl="../res/images/setting.gif"  Width="78px" Height="78px"/></td><td style="padding-right:10px"><asp:Image ID="Image8" runat="server" ImageUrl="../res/images/setting.gif"  Width="78px" Height="78px"/></td>

                    <td style="padding-right:10px"><asp:Image ID="Image9" runat="server" ImageUrl="../res/images/setting.gif"  Width="78px" Height="78px"/></td><td style="padding-right:10px"><asp:Image ID="Image10" runat="server" ImageUrl="../res/images/setting.gif"  Width="78px" Height="78px"/></td>

                </tr>
            </table>
        </div>
        <div class="col-xs-12 lsf-center" style="padding-top:20px;padding-bottom:20px">
            <asp:Button ID="button_apply" runat="server" Text="    Apply    " CssClass="btn btn-primary"/>
        </div>
    </div>
    <!-- Modal -->
<div class="modal fade" id="staticBackdrop" data-backdrop="static" tabindex="-1" role="dialog" aria-labelledby="staticBackdropLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="staticBackdropLabel">Modal title</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        ...
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Understood</button>
      </div>
    </div>
  </div>
</div>
</asp:Content>