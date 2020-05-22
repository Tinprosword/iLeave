var designPaperWidth = 360;//width，定义了画布的宽带，1。定义设计稿的宽带，元素宽度可以按设计稿定义宽度直接写，不用换算。2可以让css一直认为你是某种设备xs,sm,mi,lg .方便编写css 样式。
var _width = parseInt(window.screen.width);
var scale = _width / designPaperWidth;
var ua = navigator.userAgent.toLowerCase();
var result = /android (\d+\.\d+)/.exec(ua);
var metastr = "<meta name=\"viewport\" content=\"width=" + designPaperWidth + ",initial-scale=" + scale + ",user-scalable=no\"/>";
if (result) {
    var version = parseFloat(result[1]);
    document.write(metastr);
}
else {
    document.write(metastr);
}