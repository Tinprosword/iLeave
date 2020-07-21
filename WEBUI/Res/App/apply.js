function checkChooseDate() {
    var selectval = $("select[jqname='ddl_leavetype']").val();
    if (selectval == -1) {
        alert("please select leavetype");
        return false;
    }
    else {
        return true;
    }
}



//$("input[fixname='tb_from']").datetimepicker({
//    language: 'fr',
//    weekStart: 1,
//    todayBtn: 1,
//    autoclose: 1,
//    todayHighlight: 1,
//    startView: 2,
//    minView: 2,
//    forceParse: 0
//});
//$("input[fixname='tb_to']").datetimepicker({
//    language: 'zh-CN',
//    weekStart: 1,
//    todayBtn: 1,
//    autoclose: 1,
//    todayHighlight: 1,
//    startView: 2,
//    minView: 2,
//    forceParse: 0
//});


//$("input[fixname='tb_from']").attr("readonly", true);
//$("input[fixname='tb_to']").attr("readonly", true);