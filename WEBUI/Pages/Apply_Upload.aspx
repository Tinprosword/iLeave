<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="Apply_Upload.aspx.cs" Inherits="WEBUI.Pages.Apply_Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-12" style="padding-top:10px;padding-bottom:5px;">
            <div class="col-xs-10" style="padding-top:2px"><asp:FileUpload ID="FileUpload1" runat="server"  Width="99%" Style="padding-bottom:5px;height:50px; font-size:16px"></asp:FileUpload></div>
            <div class="col-xs-1"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Res/images/upload2.png" Width="42px" Height="42px" OnClick="Upload_Click" /></div>
        </div>
        <div class="col-xs-12" style="padding:0px; overflow-y:scroll; height:422px;">
            <asp:Repeater ID="repeater_attandance" runat="server" EnableViewState="true">
                <ItemTemplate>
                    <div class="col-xs-6" style="width:48%; height:130px;float:left;border:1px solid #f4f4f4">
                        <table>
                            <tr><td>
                                <div style="height:100px; width:100px; float:left; padding-top:10px; padding-left:2px;">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="<%# ((MODEL.Apply.app_uploadpic)Container.DataItem).reduceImageRelatePath %>"  Height="100%" Width="100%" />
                                </div>
                                <div style="height:30px; width:30px; float:left; position:relative; left:-15px; top:0px;">
                                    <asp:ImageButton ID="imagebutton_close" runat="server" ImageUrl="~/Res/images/close.png" Width="100%" Height="100%" OnClick="imagebutton_close_Click" CommandArgument="<%# ((MODEL.Apply.app_uploadpic)Container.DataItem).tempID %>"/>
                                </div>
                            </td></tr>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="linkbtn_file" runat="server" OnClick="linkbtn_file_Click" CommandArgument="<%#((MODEL.Apply.app_uploadpic)Container.DataItem).bigImageRelatepath %>"><%# ((MODEL.Apply.app_uploadpic)Container.DataItem).GetFileName(15) %></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                            
                    
            
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="col-xs-12 lsf-center" style="padding-top:12px; color:white; font-weight:bold">
            <asp:Button ID="button_apply" runat="server" Text="Back" OnClick="button_apply_Click1" CssClass="lsu-imagebtn"  style="background-image:url(../res/images/btnok.png);background-size:124px 44px" Height="44px" Width="124px"/>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
</asp:Content>