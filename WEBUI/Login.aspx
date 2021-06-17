<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WEBUI.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Res/Bootstrap/bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Res/App/appcss.css?lastmodify=<%=BLL.GlobalVariate.appcssLastmodify%>" rel="stylesheet" id="appcss" runat="server"/>
    <script src="Res/App/autoScale.js?lasttime=<%=BLL.GlobalVariate.autoscalejsLastmodify %>"></script>
    <title></title>
</head>
<body>
    <input type="hidden" value="d9c134ce"  name="aa"/>
    <form id="form1" runat="server" defaultbutton="Button1" defaultfocus="tb_user">
        <div class="container-fluid">
            <div class="row">
                <table class="row" style="width:100%;padding:0px;margin:0px;">
                    <tr class=" lsf-center lsf-fontsizem2" style="background-color:black;height:40px;">
                        <td  style="width:30%">&nbsp;</td>
                        <td  style="width:40%;color:white;">DW-iLeave</td>
                        <td  style="width:30%; font-size:13px;color:white; vertical-align:bottom;">
                        </td>
                     </tr>   
                </table>
                <table class="col-xs-12 lsu-bigtable" style="background-image:url(res/images/login_page_bg.jpg); background-repeat:no-repeat;background-size:360px 620px; height:620px;">
                    <tr><td style="width:40px"></td><td style="height:160px"></td><td></td></tr>
                    <tr>
                        <td colspan="3" style="height:60px; text-align:center"><asp:Image ID="Image1" runat="server" ImageUrl="~/Res/images/login_page_icon.png" Width="60" Height="60"/></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2"><asp:TextBox ID="tb_user" runat="server" Width="90%"/></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2"><asp:TextBox ID="tb_password" runat="server" Width="90%" TextMode="Password"/></td>
                    </tr>

                    <tr><td colspan="1"></td>
                        <td colspan="2" style=""><asp:Button ID="Button1" runat="server" Text="Login"  Width="90%" BackColor="#588da7" Height="30px" BorderWidth="0" OnClick="Button1_Click"/></td>
                    </tr>
                    <tr style="height:20px;">
                        <td></td>
                        <td class="lsf-fontsizes1">
                            <table>
                                <tr>
                                    <td style="width:20px"><asp:CheckBox ID="cb_remember" runat="server" OnCheckedChanged="cb_remember_CheckedChanged"  AutoPostBack="true"/></td>
                                    <td style="width:100px"><asp:Literal ID="lt_remember2" runat="server">Remember Me</asp:Literal></td>
                                    <td style="width:25px"><asp:Image ID="Image2" runat="server" ImageUrl="~/Res/images/earth.png" Width="21px" Height="21px" /></td>
                                    <td style="width:135px">
                                        <asp:LinkButton CssClass="loginSelect" Text="English" ID="lb_eng" OnClick="lb_eng_Click" runat="server" CommandArgument="0"></asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton CssClass="loginUnSelect" Text="简体" ID="lb_sc" OnClick="lb_eng_Click" runat="server" CommandArgument="1"></asp:LinkButton>&nbsp;&nbsp;
                                        <asp:LinkButton CssClass="loginUnSelect" ID="lb_tc" runat="server" OnClick="lb_eng_Click" Text="繁體" CommandArgument="2"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr><td style="height:200px"></td></tr>
                </table>
            </div>
        </div>
        </form>
        <script src="Res/jquery/jquery.min.js"></script>
        <asp:Literal ID="lt_js" runat="server"/>
        <script src="Res/App/CommonJS.js"></script>
        <script src="Res/Bootstrap/bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>


        

        
</body>
</html>