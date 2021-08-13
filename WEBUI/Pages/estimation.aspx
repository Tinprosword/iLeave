<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="estimation.aspx.cs" Inherits="WEBUI.Pages.estimation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <ul id="myTabApply" fixname="mytab"   class="nav nav-tabs lsf-clearPadding" runat="server">
	            <li id="myTabapply_new" runat="server" ><a style="padding-top:5px; padding-bottom:3px;padding-left:10px; padding-right:10px; " data-toggle="tab" onclick="window.location.href='apply.aspx'"><asp:Literal ID="lt_new" runat="server" Text="New"></asp:Literal></a></li>
	            <li id="myTabapply_pending" runat="server"><a style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px; " data-toggle="tab" onclick="window.location.href='approval_wait.aspx?action=1&applicationtype=0&from=0'"><asp:Literal ID="lt_mypending" runat="server" Text="Pending"/></a></li>
                <li id="myTabapply_history" runat="server"><a style="padding-top:4px; padding-bottom:4px;padding-left:10px; padding-right:10px; " data-toggle="tab" onclick="window.location.href='approval_wait.aspx?action=1&applicationtype=3&from=0'"><asp:Literal ID="lt_myhistory" runat="server" Text="History"/></a></li>
                <li id="myTabapply_es" runat="server" class="active" style="padding-left:0px;margin-left:0px;"><a style="padding-top:4px; padding-left:10px; padding-right:10px; padding-bottom:4px;" data-toggle="tab"><asp:Literal ID="lt_estimation" runat="server" Text="Estimation"/></a></li>
        </ul>
    </div>

    <div class="row" id="mainpage">
        <div style="height:15px;"></div>
        <div class ="col-xs-12" style="height:2px; padding:0px">&nbsp</div>
            <table class="col-xs-12 lsu-tablem1">
                <tr>
                    <td style="width:120px"><asp:Literal ID="lt_type" runat="server">Leave Type</asp:Literal></td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="150px">
                            <asp:ListItem Text="Annual Leave" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Sick Leave" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width:120px"><asp:Literal ID="lt_date" runat="server">Estimation Date</asp:Literal></td>
                    <td><asp:TextBox ID="tb_date" runat="server" Width="150px" CssClass="inputDate"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="1">
                        <asp:Button ID="bt_estimation" runat="server" Text="Show Balance" BackColor="#2573a4" ForeColor="White" BorderWidth="0" Height="34px" Font-Size="16px" style="border-radius:5px 5px 5px 5px" OnClick="bt_estimation_Click"/>
                    </td>
                        <td><asp:Label  ID="lb_msg" runat="server" Text="--"></asp:Label></td>
                </tr>
            </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <script type="text/javascript">
        
        $(".inputDate").datepicker({
            language: 'zh-CN', //语言
            autoclose: true, //选择后自动关闭
            clearBtn: true,//清除按钮
            format: "yyyy-mm-dd"//日期格式
        });
                </script>
</asp:Content>