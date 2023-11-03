setInterval("document.getElementById('ContentPlaceHolder1_lb_time').innerHTML=formatDate(new Date());", 10000);

function formatDate(date)
{
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}

//为什么mobile不直接调用MyPostBack,因为外部非串行触发的脚本，最好不要直接调用。有可能页面已经切换。那么会直接调用切换后页面的MyPostBack
//所以加个壳，检测来源。
function MyPostBack_mobile(towhichpage,actionname, data)
{
    theurl = window.location.href;
    if (theurl.includes(towhichpage))
    {
        //alert("it is right aspx:" + theurl + ".action:" + actionname + "data:" + data);
        MyPostBack(actionname, data);
    }
    
}