<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Apply_Upload.aspx.cs" Inherits="WEBUI.Pages.Apply_Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-12" style="padding-top:10px;padding-bottom:5px;">
            <div class="col-xs-9" style="padding-top:5px"><asp:FileUpload ID="FileUpload1" runat="server"  AllowMultiple="true" Width="150px" Style="padding-bottom:5px;height:50px; font-size:16px"/></div>
            <div class="col-xs-2"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Res/images/upload2.png" Width="50px" Height="50px" OnClick="ImageButton1_Click" /></div>
        </div>
        <div class="col-xs-12" style="padding:0px; overflow-y:scroll; height:410px;">
            <asp:Repeater ID="repeater_attandance" runat="server" EnableViewState="true">
                <ItemTemplate>
                    <div class="col-xs-6" style="height:150px;float:left;border:1px solid #f4f4f4">
                        <div style="height:110px; width:110px; float:left; margin-top:30px;">
                            <asp:Image ID="Image1" runat="server" ImageUrl="<%# ((MODEL.Apply.UploadPic)Container.DataItem).reduceImage %>"  Height="100%" Width="100%" />
                        </div>
                        <div style="width:30px;height:30px;float:left;position:relative;top:15px; left:-20px;">
                            <asp:ImageButton ID="imagebutton_close" runat="server" ImageUrl="~/Res/images/close.png" Width="40px" Height="40px" OnClick="imagebutton_close_Click" CommandArgument="<%# ((MODEL.Apply.UploadPic)Container.DataItem).tempID %>"/>
                        </div>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
            <asp:Button ID="button_apply" runat="server" Text="Back"  CssClass="btn lss-btncolor-blue" Width="160px" OnClick="button_apply_Click"/>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>