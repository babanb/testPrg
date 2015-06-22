var translations = GetTranstaltionForTour();

// Instance the tour
var tour = new Tour({
    steps: [{
        element: "#lnkIDCard",
        title: translations.IdCardTabTitle,
        content: translations.IdCardTabMessage,
        placement: "bottom"
    }, {
        element: "#profileeditbtn",
        title: translations.EditImageTitle,
        content: translations.EditImageMessage,
        placement: "top"
    }, {
        element: "#showpetdetails",
        title: translations.ManagePetTitle,
        content: translations.ManagePetMessage,
        placement: "top"
    }, {
        element: "#showbreederdetails",
        title: translations.AdoptionTitle,
        content: translations.AdoptionMessage,
        placement: "top"
    }, {
        element: "#showinsurancedetails",
        title: translations.InsuranceTitle,
        content: translations.InsuranceMessage,
        placement: "top"
    }]
}).init();
//tour.restart();
$("#startTour").click(function () {
    tour.restart();
    //$.removeCookie('petid'); // => true
})

$(document).ready(function () {
    if ($.cookie('petid') == null) {
        tour.restart();
        $.cookie('petid', '7', { expires: 365 });
    }
});