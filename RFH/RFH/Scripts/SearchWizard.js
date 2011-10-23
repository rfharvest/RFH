?function SearchWizard(queryForm, searchResultsWidget) {
    this.queryForm = queryForm;
    this.searchResultsWidget = searchResultsWidget;

    this._getFilters = function () {
        var filters = {};
        var formSelects = this.queryForm.find('select');   // Find all dropdowns in the form
        formSelects.each(function () {
            filters[this.name] = this.value;            // Add dropdown's value to filter as key/value pair
        });
        return filters;
    };

    this._onChange = function () {
        console.log("onChange fired");
        var filters = this._getFilters();
        this.searchResultsWidget.applyFilters(filters);
    };

    var me = this;
    var changeElems = this.queryForm.find('select');
    changeElems.change(function () { me._onChange() });
    changeElems.keyup(function () { me._onChange() });
};

function SearchResults(hostSites, jqResultsNode) {
    this.hostSites = hostSites;
    this.hostSiteLinkNodes = {}; // map hostSiteId -> hostSiteLinkNode
    this.jqResultsNode = jqResultsNode;
    this.jqSorryNode = null;

    for (var i in this.hostSites) {
        var hostSite = this.hostSites[i];

        // Create a new host-site hyperlink
        var newHtml = "<div><a href=\"" + hostSite.url + "\">" + hostSite.name + "</a></div>";
        var newHtmlNode = $(newHtml);

        // Map the host-site's data object to its hyperlink, for easy retrieval 
        this.hostSiteLinkNodes[hostSite.id] = newHtmlNode;

        // Add the hyperlink to the document
        this.jqResultsNode.append(newHtmlNode);
    }

    // If the user filters away all possible host-sites, then display an apology, even though it's the user's fault.
    this.jqSorryNode = $("<div class=\"sorry hidden\">Sorry, but none of our sites match your search parameters. Please remove or change some filters, and try again.</div>");
    this.jqResultsNode.append(this.jqSorryNode);

    this.applyFilters = function (filters) {
        filters = filters || {};
        var isAnySearchResultShown = false;

        for (var i in this.hostSites) {
            var hostSite = this.hostSites[i];
            var isFilteredOut = false;

            // Test every host-site to see if it passes all the filters
            for (var fKey in filters) {
                var fVal = filters[fKey] || "";
                var hsVal = (hostSite.criteria && hostSite.criteria[fKey]) || "";
                if (fVal != "" && hsVal != fVal) {
                    isFilteredOut = true;
                    break;
                }
            }

            // Show/hide the host-site's hyperlink
            var hostSiteLinkNode = this.hostSiteLinkNodes[hostSite.id];
            if (isFilteredOut && hostSiteLinkNode) {
                hostSiteLinkNode.addClass("hidden");
            } else {
                hostSiteLinkNode.removeClass("hidden");
                isAnySearchResultShown = true;
            }
        }

        if (isAnySearchResultShown) {
            this.jqSorryNode.addClass("hidden");
        } else {
            this.jqSorryNode.removeClass("hidden");
        }
    }
};