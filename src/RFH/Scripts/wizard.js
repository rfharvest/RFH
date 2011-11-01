function loadMatchingHostSites() {

    clearAllExistingMatches();
    var hasAtLeastOneMatch = false;
    var selectedValues = getSelectedValues();

    for (var x = 0; x < hostSites.length; x++) {
        if (isMatchingHostSite(hostSites[x], selectedValues)) {
            hasAtLeastOneMatch = true;
            showHostSite(hostSites[x]);
        }
    }

    if (!hasAtLeastOneMatch) {
        showNoMatchMessage();
    }
}

function clearAllExistingMatches() {
    $('#hostSiteContainer').empty();
}

function getSelectedValues() {

    var selectedValues = new Array();

    $('.searchWizard .question select').each(function () {

        var selectedValue = $(this).val();

        if (selectedValue.length > 0) {
            selectedValues.push(selectedValue);
        }
    });

    return selectedValues;
}

function isMatchingHostSite(hostSite, selectedValues) {

    for (var x = 0; x < selectedValues.length; x++) {
        if ($.inArray(parseInt(selectedValues[x]), hostSite.tagValues) < 0) {
            return false;
        }
    }

    return true;
}

function showHostSite(hostSite) {

    if ($('#hostSiteContainer ul').size() < 1) {
        $('#hostSiteContainer').append('<ul>');
    }

    $('#hostSiteContainer').append('<li><a href="' + hostSite.url + '">' + hostSite.name + '</a></li>');
}

function showNoMatchMessage() {
    $('#hostSiteContainer').append('<p><div class="sorry">Sorry, but none of our sites match your search parameters. Please remove or change some filters, and try again.</p>');
}

$(function () {

    $('.searchWizard .question select').change(function () {
        loadMatchingHostSites(); // mouse people
    }).keyup(function () {
        loadMatchingHostSites(); // keyboard people
    });

    loadMatchingHostSites();
});