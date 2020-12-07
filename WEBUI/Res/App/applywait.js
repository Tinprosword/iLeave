function onGetData(obj)
{
    var contentHtml = $(obj).find("#mycontent").html();
    $("#ajaxContainer").html(contentHtml);
    showWindow('ongetdate');
}