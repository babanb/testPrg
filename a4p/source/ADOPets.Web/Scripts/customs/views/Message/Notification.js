$(document).ready(function () {
    $.get("/Message/NotificationHistory", function (data) {
        $('#dvHistoryList').html(data);
    });
});