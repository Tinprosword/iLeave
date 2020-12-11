function checkNewTab(msg,act,type)
{
    if (msg != "")
    {
        if (confirm(msg)) {
            event.preventDefault();
            window.location.href = 'approval_wait.aspx?action=' + act + '&applicationtype=' + type;
        }
        else {
            var target = $("ul[fixname='mytab'] li:eq(0)");
            var target2 = $("ul[fixname='mytab'] li:eq(1)");
            var target3 = $("ul[fixname='mytab'] li:eq(2)");

            target.removeClass();
            target2.removeClass();
            target3.removeClass();

            window.setTimeout(function () {
                var target = $("ul[fixname='mytab'] li:eq(0)");
                var target2 = $("ul[fixname='mytab'] li:eq(1)");
                var target3 = $("ul[fixname='mytab'] li:eq(2)");

                target.addClass("active");
                target2.addClass("active");
                target3.addClass("active");

                target2.removeClass();
                target3.removeClass();

            }, 1);

            window.setTimeout(function () {
                var target = $("ul[fixname='mytab'] li:eq(0)");
                var target2 = $("ul[fixname='mytab'] li:eq(1)");
                var target3 = $("ul[fixname='mytab'] li:eq(2)");

                target.addClass("active");
                target2.addClass("active");
                target3.addClass("active");

                target2.removeClass();
                target3.removeClass();

            }, 100);

            return false;
        }

    }
    else {
        window.location.href = 'approval_wait.aspx?action=' + act + '&applicationtype=' + type;
    }
}