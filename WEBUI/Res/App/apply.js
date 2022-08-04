function checkNewTab(msg,act,type,from)
{
    if (msg != "")
    {
        if (confirm(msg)) {
            event.preventDefault();
            window.location.href = 'approval_wait.aspx?action=' + act + '&applicationtype=' + type + '&from=' + from;
        }
        else {
            RollbackSelectCss();
            return false;
        }

    }
    else {
        window.location.href = 'approval_wait.aspx?action=' + act + '&applicationtype=' + type + '&from=' + from;
    }
}


function checkOtherPage(msg, url) {

    if (msg != "")
    {
        if (confirm(msg)) {
            event.preventDefault();
            window.location.href = url;
        }
        else {
            RollbackSelectCss();
            return false;
        }
    }
    else
    {
        window.location.href = url;
    }
}

function RollbackSelectCss() {

    var target = $("ul[fixname='mytab'] li:eq(0)");
    var target2 = $("ul[fixname='mytab'] li:eq(1)");
    var target3 = $("ul[fixname='mytab'] li:eq(2)");
    var target4 = $("ul[fixname='mytab'] li:eq(3)");

    target.removeClass();
    target2.removeClass();
    target3.removeClass();
    target4.removeClass();

    window.setTimeout(function () {
        var target = $("ul[fixname='mytab'] li:eq(0)");
        var target2 = $("ul[fixname='mytab'] li:eq(1)");
        var target3 = $("ul[fixname='mytab'] li:eq(2)");
        var target4 = $("ul[fixname='mytab'] li:eq(3)");

        target.addClass("active");
        target2.addClass("active");
        target3.addClass("active");
        target4.addClass("active");

        target2.removeClass();
        target3.removeClass();
        target4.removeClass();

    }, 1);

    window.setTimeout(function () {
        var target = $("ul[fixname='mytab'] li:eq(0)");
        var target2 = $("ul[fixname='mytab'] li:eq(1)");
        var target3 = $("ul[fixname='mytab'] li:eq(2)");
        var target4 = $("ul[fixname='mytab'] li:eq(3)");

        target.addClass("active");
        target2.addClass("active");
        target3.addClass("active");
        target4.addClass("active");

        target2.removeClass();
        target3.removeClass();
        target4.removeClass();

    }, 100);
}