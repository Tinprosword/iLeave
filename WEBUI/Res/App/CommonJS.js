function test()
{
    alert("hi");
}


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

function sleep(d)
{
    var t = Date.now();
    while (Date.now() - t <= d);
}

function go()
{
    MyPostBack('aa', '');
}


function ShowMessage(msg,eventName)
{
    $('#fixmsg').html(msg).addClass("Flostalert-success").show().delay(1500).fadeOut();
    setTimeout("go(" + eventName + ")", 2000);
    return false;
}