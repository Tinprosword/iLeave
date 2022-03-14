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
function AjaxGet(url) {
    $.ajax({
        type: "get",
        url: url,
    });
}

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
            //alert(result);
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
function closeWindow()
{
    $('#showdiv').hide(); //隐藏弹窗
    $('#cover').css('display', 'none'); //显示遮罩层
    $('#showdiv .content').html(""); //清空追加的内容
}
//end div



//cookie
//保存Cookie
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}
//取出Cookie
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return null;
}
//删除Cookie
function delCookie(cname) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(cname);
    document.cookie = cname + "=" + cval + "; expires=" + exp.toGMTString();
}
//end cookie

function showCalendar(inputCssName) {
    var cssnameSelecter = "." + inputCssName;
    $(cssnameSelecter).datepicker({
        language: 'zh-CN', //语言
        autoclose: true, //选择后自动关闭
        clearBtn: true,//清除按钮
        format: "yyyy-mm-dd"//日期格式
    });
}

//隐藏scroll(需要配合css.),并可以设置个按钮来左右移动。leftbtn:#controlid.
function SetSroll(leftbtn, rightbtn, content, offsetleft) {
    var leftbtn = $(leftbtn);
    var rightbtn = $(rightbtn);

    leftbtn.click(
        function () {
            var divContent = $(content);
            var sl = divContent.scrollLeft();
            sl -= offsetleft;
            divContent.scrollLeft(sl);
        }
    );

    rightbtn.click(
        function () {
            var divContent = $(content);
            var sl = divContent.scrollLeft();
            sl += offsetleft;
            divContent.scrollLeft(sl);
        }
    );
}