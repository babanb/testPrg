
var translations = GetTranstaltionForTour();

// Instance the tour
var tour = new Tour({
    steps: [{
        element: "#notibell",
        title: translations.NotificationTitle,
        content: translations.NotificationMessage
    }, {
        element: "#homesite",
        title: translations.HomeTitle,
        content: translations.HomeMessage
    }, {
        element: "#faqTour",
        title: translations.StartTourTitle,
        content: translations.StartTourMessage
    }, {
        element: "#howTo",
        title: translations.HowToTitle,
        content: translations.HowToMessage
    }, {
        element: "#downloadPhr",
        title: translations.DownloadTitle,
        content: translations.DownloadMessage
    }, {
        element: "#demo2",
        title: translations.LeftMenuTitle,
        content: translations.LeftMenuMessage
    }, {
        element: "#newsfeed",
        title: translations.NewsFeedTitle,
        content: translations.NewsFeedMessage
    }, {
        element: "#addPet",
        title: translations.AddNewPetTitle,
        content: translations.AddNewPetMessage,
        placement: "left"
    }, {
        element: "#showrightsidebar",
        title: translations.ToggleRightPanelTitle,
        content: translations.ToggleRightPanelMessage,
        placement: "left"
    }, {
        element: "#lnkMyAccountOwner",
        title: translations.MyAccountTitle,
        content: translations.MyAccountMessage,
        placement: "left"
    }, {
        element: "#hiderightsidebar",  // same as above
        title: translations.ToggleRightPanelTitle,
        content: translations.ToggleRightPanelMessage,
        placement: "left"
    }, {
        element: "#quickLinks",
        title: translations.RightMenuTitle,
        content: translations.RightMenuMessage,
        placement: "left"
    }, {
        element: "#reminderbx",
        title: translations.YourReminderTitle,
        content: translations.YourReminderMessage,
        placement: "left"
    }, {
        element: "#msgbx",
        title: translations.YourMessagesTitle,
        content: translations.YourMessagesMessage,
        placement: "left",
        onNext: function (tour) {
            if ($('.listing:first-child() .col-md-2 a:first-child').length == 0) {
                $('[data-role="end"]').click();
            }
        }
    }, {
        element: ".listing:first-child() .col-md-2 a:first-child",
        title: "Details",
        content: translations.ViewDetailsMessage,
        placement: "left"
    }]
}).init();

$("#startTour, #firstStartTour").click(function () {
    tour.restart();
})

$(document).ready(function () {
    if ($.cookie('pop') == null) {
        $('#welcomeModal').modal('show');
        $.cookie('pop', '7', { expires: 365 });
    }
});