function showPage(page) {
    var idname = "#" + page;
    $('#mainpage').css('visibility', 'hidden');
    $('#uploadpage').css('visibility', 'hidden');
    $(idname).css('visibility', 'visible');
}