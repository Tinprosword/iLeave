<%@ Page Title="" Language="C#" MasterPageFile="~/Controls/leave.Master" AutoEventWireup="true" CodeBehind="template2.aspx.cs" Inherits="WEBUI.template2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <input type="text" id="datetimepicker"/>
    <asp:TextBox ID="tb_date"  data-date-format="yyyy-mm-dd" fixname="datefrom" runat="server" Width="86px"></asp:TextBox>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentjs" runat="server">
    <script type="text/javascript">
        alert('aab');

                $('#datetimepicker').datepicker({
                    language: 'zh-CN', //语言
                    autoclose: true, //选择后自动关闭
                    clearBtn: true,//清除按钮
                    format: "yyyy-mm-dd"//日期格式
        });

        $("input[fixname='datefrom']").datepicker({
                    language: 'zh-CN', //语言
                    autoclose: true, //选择后自动关闭
                    clearBtn: true,//清除按钮
                    format: "yyyy-mm-dd"//日期格式
                });

    </script>
</asp:Content>
