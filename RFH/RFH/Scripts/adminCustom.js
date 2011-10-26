/* custom admin javascript file */

$(function () {

    $('.standardTable').find('tbody tr').hover(function () {
        $(this).addClass('hover');
    }, function () {
        $(this).removeClass('hover');
    })

});