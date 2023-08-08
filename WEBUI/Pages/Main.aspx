<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="WEBUI.Pages.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row" style="background-color:#f3f7f9; min-height:585px; position:relative;" id="maindiv" runat="server">
        <div class="col-xs-12 lsf-center" style=" color:#8a8b8b; height:38px; line-height:38px; font-weight:bold; font-size:15px;"><asp:Label ID="lb_name" runat="server" Text="Chan Tai Man"></asp:Label></div>
        
  
            <div class="col-xs-6 lsf-clearPadding menuBox" id="menu1" runat="server">
                <div class="menuBoxInside">
                    <div class="menuBox_imgbox"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Res/images/Menu3_applyleave.png" OnClick="Apply_Click" Width="80px" Height="80px" CssClass="menuBox_img"/></div>
                    <div class="menuBox_linkbox"><asp:LinkButton ID="lt_applyleaveabc" runat="server" OnClick="Apply_Click"  Text="aaa"></asp:LinkButton></div>
                </div>
            </div>

        <div class="col-xs-6 lsf-clearPadding menuBox" id="menu7" runat="server" >
                <div class="menuBoxInside">
                    <div class="menuBox_imgbox"><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Res/images/Menu3_applyclot.png" OnClick="clot_Click"  CssClass="menuBox_img" /></div>
                    <div class="menuBox_linkbox"><asp:LinkButton ID="lt_clot" runat="server" OnClick="clot_Click"  Text="aaa"></asp:LinkButton></div>
                </div>
                
            </div>

        <div class="col-xs-6 lsf-clearPadding menuBox" id="menu2" runat="server" >
                <div class="menuBoxInside">
                    <div class="menuBox_imgbox"><asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Res/images/Menu3_apporve.png" OnClick="Approval_Click"   CssClass="menuBox_img"/></div>
                    <div class="menuBox_linkbox"><asp:LinkButton ID="lt_approal" runat="server" OnClick="Approval_Click"  Text="aaa"></asp:LinkButton></div>
                </div>
                
            </div>

        <div class="col-xs-6 lsf-clearPadding menuBox" id="menu3" runat="server" >
                <div class="menuBoxInside">
                    <div class="menuBox_imgbox"><asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Res/images/Menu3_calender.png" OnClick="Canlendar_Click"  CssClass="menuBox_img" /></div>
                    <div class="menuBox_linkbox"><asp:LinkButton ID="lt_calendarabc" runat="server" OnClick="Canlendar_Click"  Text="aaa"></asp:LinkButton></div>
                </div>
                
            </div>

        <div class="col-xs-6 lsf-clearPadding menuBox" id="menu6" runat="server" >
                <div  class="menuBoxInside">
                    <div class="menuBox_imgbox"><asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Res/images/Menu3_roster.png" OnClick="RosterInquiry_Click"  CssClass="menuBox_img"/></div>
                    <div class="menuBox_linkbox"><asp:LinkButton ID="lt_RosterInquiry" runat="server" OnClick="RosterInquiry_Click"  Text="aaa"></asp:LinkButton></div>
                </div>
                
            </div>

         <div class="col-xs-6 lsf-clearPadding menuBox" id="menu4" runat="server" >
                <div class="menuBoxInside">
                    <div class="menuBox_imgbox"><asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Res/images/Menu3_inout.png" OnClick="Check_Click"  CssClass="menuBox_img" /></div>
                    <div class="menuBox_linkbox"><asp:LinkButton ID="lt_check" runat="server" OnClick="Check_Click"  Text="aaa"></asp:LinkButton></div>
                </div>
                
            </div>
         <div class="col-xs-6 lsf-clearPadding menuBox" id="menu_payslip" runat="server" >
                <div  class="menuBoxInside">
                    <div class="menuBox_imgbox"><asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Res/images/Menu3_payslip.png" OnClick="Payslip_Click"  CssClass="menuBox_img" /></div>
                    <div class="menuBox_linkbox"><asp:LinkButton ID="lt_payslip" runat="server" OnClick="Payslip_Click"  Text="aaa"></asp:LinkButton></div>
                </div>
                
            </div>
         <div class="col-xs-6 lsf-clearPadding menuBox" id="menu_Taxation" runat="server" >
                <div class="menuBoxInside">
                    <div class="menuBox_imgbox"><asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Res/images/Menu3_tax.png" OnClick="taxation_Click"  CssClass="menuBox_img" /></div>
                    <div class="menuBox_linkbox"><asp:LinkButton ID="lt_taxation" runat="server" OnClick="taxation_Click"  Text="aaa"></asp:LinkButton></div>
                </div>
                
            </div>
        

       <%-- <div class="col-xs-12 lsf-center" style="margin-top:0px;" id="menu1" runat="server">
            <asp:ImageButton ID="apply" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_A.png" CssClass="menucss" OnClick="Apply_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_applyleaveabc" runat="server" OnClick="Apply_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu7" runat="server">
            <asp:ImageButton ID="clot" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_B.png" CssClass="menucss" OnClick="clot_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_clot" runat="server" OnClick="clot_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu2" runat="server">
            <asp:ImageButton ID="approval" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_C.png" CssClass="menucss" OnClick="Approval_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_approal" runat="server" OnClick="Approval_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu3" runat="server">
            <asp:ImageButton ID="Canlendar" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_D.png" CssClass="menucss" OnClick="Canlendar_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_calendarabc" runat="server" OnClick="Canlendar_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu6" runat="server">
            <asp:ImageButton ID="RosterInquiry" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_E.png" CssClass="menucss" OnClick="RosterInquiry_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_RosterInquiry" runat="server" OnClick="RosterInquiry_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu4" runat="server">
            <asp:ImageButton ID="Check" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_E.png" CssClass="menucss" OnClick="Check_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_check" runat="server" OnClick="Check_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu_payslip" runat="server">
            <asp:ImageButton ID="Payslip" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_E.png" CssClass="menucss" OnClick="Payslip_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_payslip" runat="server" CssClass="fixLink" OnClick="Payslip_Click"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu_Taxation" runat="server">
            <asp:ImageButton ID="taxation" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_E.png" CssClass="menucss"  OnClick="taxation_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_taxation" runat="server"  CssClass="fixLink" OnClick="taxation_Click"></asp:LinkButton></div>
        </div>
        <div class="col-xs-12 lsf-center" style="margin-top:6px;" id="menu5" runat="server">
            <asp:ImageButton ID="Setting" runat="server" ImageUrl="~/Res/images/Content_Page_Color_Botton_F.png" CssClass="menucss" OnClick="Setting_Click"/>
            <div style="position:relative;top:-66px; font-size:26px; height:0px;color:white"><asp:LinkButton ID="lt_setting" runat="server" OnClick="Setting_Click" CssClass="fixLink"></asp:LinkButton></div>
        </div>--%>
        <div style=" position:absolute; z-index:1; right:5px; top:3px; background-color:white; width:100px; height:60px; border:solid 1px black;">
            <asp:LinkButton ID="LB_MSG" runat="server" Text="Message"/><br />
            <asp:LinkButton ID="LB_Setup" runat="server" Text="Setup"/>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="contentjs" runat="server">
    <script type="text/javascript">
        alert("aaa");
    </script>
</asp:Content>