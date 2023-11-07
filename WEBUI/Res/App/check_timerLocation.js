//document.location.href = "js://webview?msgtype=GPS&value1=0";
//
//不知道为什么语句必须放到JS文件中。否则当 Mobile app, 调用一个 JS 方法  如， Mobile_UpdateLocation 之后，
//上面的语句，如果不放到文件中。就再调用Mobile_UpdateLocation，会调用不到。
//可能是 app 的JS 调用会破坏 页面的JS，所以必须 放入到单独的文件中。

function startTimerLocation() {
    document.location.href = "js://webview?msgtype=GPS&value1=0";
    setInterval("document.location = 'js://webview?msgtype=GPS&value1=0';", 7000);
}
startTimerLocation();