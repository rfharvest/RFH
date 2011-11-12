/* custom javascript file */

$(function () {
    var sidebarHeight = $('#sidebar').height();
    var contentHeight = $('#content').outerHeight();
    if (sidebarHeight > contentHeight) {
        $('#content').css('height', sidebarHeight-40 + 'px');
    } else {
        $('#sidebar').css('height', contentHeight + 'px');
    }
});