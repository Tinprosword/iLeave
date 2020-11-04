$("input[fixname='datefrom']").datepicker({
    language: 'zh-CN', //语言
    autoclose: true, //选择后自动关闭
    clearBtn: true,//清除按钮
    format: "yyyy-mm-dd",//日期格式
    todayBtn: true,
});

$("input[fixname='datefrom']").attr("readonly", true);