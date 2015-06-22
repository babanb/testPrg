
var totalRecords = 4;

$(document).ready(function () {
    //for tooltip
    $(function () { $("[data-toggle='tooltip']").tooltip(); });


    var trs = $('.commentsrow');
    var btnMore = $("#seeMoreRecords");
    var btnLess = $("#seeLessRecords");
    var trsLength = trs.length;
    var currentIndex = totalRecords;

    trs.hide();
    trs.slice(0, totalRecords).show();
    checkButton();

    btnMore.click(function (e) {
        e.preventDefault();
        $(".commentsrow").slice(currentIndex, currentIndex + totalRecords).show();
        currentIndex += totalRecords;
        checkButton();
    });

    btnLess.click(function (e) {
        e.preventDefault();
        $(".commentsrow").slice(currentIndex - totalRecords, currentIndex).hide();
        currentIndex -= totalRecords;
        checkButton();
    });

    function checkButton() {
        var currentLength = $(".commentsrow:visible").length;

        if (currentLength >= trsLength) {
            btnMore.hide();
        } else {
            btnMore.show();
        }

        if (trsLength > totalRecords && currentLength > totalRecords) {
            //     btnLess.show();
        } else {
            btnLess.hide();
        }

    }


    $(window).scroll(function (e) {
        var scrolltoppos = $(document).scrollTop();
        //alert(scrolltoppos);
        if (scrolltoppos > 100) {
            $("#backTotop").fadeIn(1000);
        } else {
            $("#backTotop").fadeOut(1000);
        }
    });
});

$(function () {
    $('#backTotop a[href*=#]:not([href=#])').click(function () {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {

            var target = $(this.hash);
            target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
            if (target.length) {
                $('html,body').animate({
                    scrollTop: target.offset().top
                }, 1000);
                return false;
            }
        }
    });
});



