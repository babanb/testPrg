var translations = GetTranstaltionForTour();

// Instance the tour
var tour = new Tour({
    steps: [{
        element: "#lnkMRTab",
        title: translations.MRTabTitle,
        content: translations.MRTabMessage,
        placement: "bottom"
    }, {
        element: "#HealthHistory",
        title: translations.HealthHistoryTitle,
        content: translations.HealthHistoryMessage,
        placement: "bottom"
    }, {
        element: "#HealthMeasureTracker",
        title: translations.TrackerTitle,
        content: translations.TrackerMessage,
        placement: "bottom"
    }, {
        element: "#Document",
        title: translations.DocumentsTitle,
        content: translations.DocumentsMessage,
        placement: "bottom"
    }]
}).init();
//tour.restart();
$("#startTour").click(function () {
    tour.restart();
    //$.removeCookie('petid'); // => true
})

$(document).ready(function () {
    if ($.cookie('mr') == null) {
        tour.restart();
        $.cookie('mr', '7', { expires: 365 });
    }
});