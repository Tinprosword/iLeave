﻿$("input[fixname='datefrom']").datetimepicker({
    language: 'fr',
    weekStart: 1,
    todayBtn: 1,
    clearBtn: 1,
    autoclose: 1,
    todayHighlight: 1,
    startView: 2,
    minView: 2,
    forceParse: 0
});

$("input[fixname='datefrom']").attr("readonly", true);