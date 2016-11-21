function UmbracoGAEventTracking(nodeId, regex) {
    // Properties
    var self = this;
    self.eventEndpoint = window.location.origin + "/Umbraco/GoogleAnalytics/GAEventTracking/GetEvents?eventRootId=" + nodeId;
    self.events = [];
    self.regex = regex;

    // Methods
    function sendGaEvent(eventCategory, action, eventLabel) {
        ga('send', 'event', eventCategory, action, eventLabel);
        //console.log(
        //    "Category: " + eventCategory
        //    + ", Action : " + action
        //    + ", Label : " + eventLabel);
    }
    function httpGetAsync(theUrl, successCallback, failCallback) {
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.overrideMimeType("application/json");
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState === 4 && xmlHttp.status === 200) {
                var json = JSON.parse(xmlHttp.responseText);
                successCallback(json);
            }
        };
        xmlHttp.open("GET", theUrl, true); // true for asynchronous
        xmlHttp.send(null);
    }
    function getValueForPlaceholder(element, placeholder) {
        switch (placeholder) {
            case "{PAGE_URL}":
                return window.location.href;
            case "{PAGE_RELATIVE_URL}":
                return window.location.href.replace(window.location.origin, "");
            case "{TAG_NAME}":
                return "<" + element.tagName.toLowerCase() + ">";
            case "{ID}":
                return element.id;
            case "{CLASS}":
                return element.className;
            case "{VALUE}":
                return !isNull(element.value)
                   ? element.value
                   : "";
            case "{SRC}":
                return !isNull(element.src)
                   ? element.src
                   : "";
            case "{LINK_URL}":
                return !isNull(element.href)
                   ? element.href
                   : "";
            case "{LINK_RELATIVE_URL}":
                return isNull(element.href)
                    ? ""
                    : element.href.replace(window.location.origin, "");
            case "{ALT}":
                return !isNull(element.alt)
                    ? element.alt
                    : "";
            case "{TITLE}":
                return !isNull(element.title)
                   ? element.title
                   : "";
            case "{INNER_HTML}":
                return !isNull(element.innerHTML)
                   ? element.innerHTML
                   : "";
            default:
                return "";
        }
    };
    function isNull(obj) {
        return (obj === null || typeof obj === 'undefined');
    }
    function registerElementForEvent(element, event) {
        element.addEventListener(
            event.Action,
            function () {
                var label = getGaLabelForElement(element, event.Label);
                sendGaEvent(event.Category, event.Action, label);
            });
    }
    function getGaLabelForElement(element, label) {
        var newLabel = label;
        var placeholders = getRegexMatches(self.regex, newLabel);
        if (placeholders !== null && typeof placeholders !== 'undefined') {
            for (var i = 0; i < placeholders.length; i++) {
                newLabel = newLabel.replace(
                    placeholders[i],
                    getValueForPlaceholder(element, placeholders[i]));
            }
        }
        return newLabel;
    };
    function getRegexMatches(reg, str) {
        var matches = [];
        var match;
        do {
            match = regex.exec(str);
            if (match !== null)
                matches.push(match[0]);
        } while (match)
        return matches;
    }

    // Instance methods
    self.registerEventElements = function () {
        for (var i = 0; i < self.events.length; i++) {
            var elements = document.querySelectorAll(self.events[i].CssSelector);
            for (var j = 0; j < elements.length; j++) {
                registerElementForEvent(elements[j], self.events[i]);
            }
        }
    }
    self.init = function () {
        httpGetAsync(
            self.eventEndpoint,
            function (json) {
                if (json !== null && typeof json !== 'undefined') {
                    self.events = json;
                    self.registerEventElements();
                } else {
                    console.log("Unable to retieve Google GA Events from Umbraco backend");
                }
            });
    };
}