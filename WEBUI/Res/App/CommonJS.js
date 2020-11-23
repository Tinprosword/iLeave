function MyPostBack(postback_target,postback_argument)
{
    var theForm = document.forms['form1'];
    theForm.mypostback_target.value = postback_target;
    theForm.mypostback_argument.value = postback_argument;
    theForm.submit();
}


//可以用作来弹出窗口
function ShowDivByfixname(windowname)
{
    var winabc = $("div[fixname='" + windowname + "']");
    winabc.show();
    return;
}

// When the user clicks on <span> (x), close the modal
function HiddenDivByfixname(windowname)
{
    var winabc = $("div[fixname='" + windowname + "']");
    winabc.hide();
    return;
}


function GOHistory()
{
    history.back();
}

//model
function showModel(modelid) {
    var myModelID = "#" + modelid + "";
    $(myModelID).modal({ backdrop: 'static' }); 
}

function hiddenModel(myModelID) {
    $(myModelID).modal('hide');
}

function showMyModel() {
    showModel('myModal');
}

function hiddenMyModel() {
    hiddenModel('myModal');
}
//model end


//ajax start
function SingleResult(url, Postdata, datatype, callfun) {
    $.ajax({
        type: "post",
        url: url,
        data: Postdata,
        async: true,
        success: function (result) {
            callfun($(result).find(datatype).text());
        },
        error: function () { }
    });
}

function ModelsResult(url, Postdata, rootname, eachFun) {
    $.ajax({
        type: "post",
        url: url,
        data: Postdata,
        async: true,
        success: function (result) {
            $(result).find(rootname).each(eachFun);//eachFun(i,obj)
        },
        error: function () { }
    });
}



function SingleResultWithWait(url, Postdata, datatype, callfun, modelid)
{
    var myModelID = "#" + modelid + "";
    $.ajax({
        type: "post",
        url: url,
        data: Postdata,
        async: true,
        beforeSend: function () { $(myModelID).modal({ backdrop: 'static' }); },//when null,do nothing.
        complete: function () { $(myModelID).modal('hide'); },//when null,do nothing.
        success: function (result) {
            callfun($(result).find(datatype).text());
        },
        error: function () { }
    });
}

function ModelsResultWithWait(url, Postdata, rootname, eachFun,modelid) {
    $.ajax({
        type: "post",
        url: url,
        data: Postdata,
        async: true,
        beforeSend: function () { $(myModelID).modal({ backdrop: 'static' }); },//when null,do nothing.
        complete: function () { $(myModelID).modal('hide'); },//when null,do nothing.
        success: function (result) {
            $(result).find(rootname).each(eachFun);//eachFun(i,obj)
        },
        error: function () { }
    });
}


function SingleResultWithWaitHardCodeMID(url, Postdata, datatype, callfun) {
    SingleResultWithWait(url, Postdata, datatype, callfun, "myModal");
}


function ModelsResultWithWaitHardCodeMID(url, Postdata, datatype, callfun) {
    ModelsResultWithWait(url, Postdata, datatype, callfun, "myModal");
}


function SingleResult_StringWithWaitHardCodeMID(url, Postdata, callfun) {
    SingleResultWithWait(url, Postdata, "string", callfun, "myModal");
}

function SingleResult_intWithWaitHardCodeMID(url, Postdata, callfun) {
    SingleResultWithWait(url, Postdata, "int", callfun, "myModal");
}


function getMember(obj, memberName) {
    return $(obj).children(memberName).text();
}
//ajax end


//div start
// 弹窗
function showWindow(showmsg) {
    $('#showdiv').show(); //显示弹窗
    $('.content').append(showmsg); //追加内容
    $('#cover').css('display', 'block'); //显示遮罩层
}

// 关闭弹窗
function closeWindow() {
    $('#showdiv').hide(); //隐藏弹窗
    $('#cover').css('display', 'none'); //显示遮罩层
    $('#showdiv .content').html(""); //清空追加的内容
}

//end div