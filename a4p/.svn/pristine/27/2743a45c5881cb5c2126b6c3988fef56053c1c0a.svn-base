$(function () {
    var current = $("#currentHealthMeasureTracker").val();
    $('.nav-pills a[href=#' + current + ']').tab('show');    
    $('.tracker a[data-toggle="pill"]').find('div').removeClass("talk-up");
    $('.tracker a[href=#' + current + ']').append("<div class='talk-up'></div>");
});

$(document).ready(function () {

    $('.tracker a[data-toggle="pill"]').click(function () {
        $('.tracker a[data-toggle="pill"]').find('div').removeClass("talk-up");
        $(this).append("<div class='talk-up'></div>");
    });

    $('.vitalsnav li a[data-toggle="pill"]').click(function () {
        $('.vitalsnav li a[data-toggle="pill"]').removeClass("active");
        $('.vitalsnav a[data-toggle="pill"]').removeClass("btn-success");
        $('.vitals-tabs').removeClass("active");
        
        $($(this).attr('href')).addClass("active");
        $(this).addClass("btn-success");
        
    });

});