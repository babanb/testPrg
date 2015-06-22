$(function () {
    var current = $("#currentHealthHistory").val();
    $('.nav-pills a[href=#' + current + ']').tab('show');
    $('.healthHistory a[data-toggle="pill"]').find('div').removeClass("talk-up");
    $('.healthHistory a[href=#' + current + ']').append("<div class='talk-up'></div>");
});

$(document).ready(function () {

    $('.healthHistory a[data-toggle="pill"]').click(function () {
        $('.healthHistory a[data-toggle="pill"]').find('div').removeClass("talk-up");
        $(this).append("<div class='talk-up'></div>");
    });

});