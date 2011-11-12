/* custom javascript file */

var gAdjustLayoutHeightIsQueued = false;

function adjustLayoutHeight() {

    gAdjustLayoutHeightIsQueued = false;
    
    var $sidebar = $('#sidebar');
    var $content = $('#content');
    var sidebarHeight = $sidebar.outerHeight();
    var contentHeight = $content.outerHeight();

    if (sidebarHeight > contentHeight) {
        $content.css('height', sidebarHeight - 40 + 'px');
    } else {
        $sidebar.css('height', contentHeight + 'px');
    }
}

function initWindowResizeFeature() {
    
    $(window).resize(function () {

        if (!gAdjustLayoutHeightIsQueued) {

            gAdjustLayoutHeightIsQueued = true;
            window.setTimeout(function () {
                adjustLayoutHeight();
            }, 1000);
        }
    });
}

$(function () {

    adjustLayoutHeight();

    initWindowResizeFeature();

});