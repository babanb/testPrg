

$(document).ready(function () {
    $.get("/NewsFeed/NewsFeedPartial", function (data) {
        $('#dvHistoryList').html(data);
    });
});