/* custom admin javascript file */

function initHoverStateOnStandardTable() {

    $('.standardTable').find('tbody tr').hover(function () {
        $(this).addClass('hover');
    }, function () {
        $(this).removeClass('hover');
    });

}

function initHtmlContentFieldsWithEditor() {

    $('textarea.htmlContent').each(function () {
        var e = $(this);
        if (e.val().length < 1) {
            e.val('Content goes here.');
        }
        CKEDITOR.replace(e.attr('name'));
    });

}

$(function () {

    initHoverStateOnStandardTable();
    initHtmlContentFieldsWithEditor();

});