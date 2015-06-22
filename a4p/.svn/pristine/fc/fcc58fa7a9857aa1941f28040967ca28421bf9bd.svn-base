$(window).on('load resize', function () {
    var winheight = $(window).height();
    var scrollheight = winheight - 234;
    $('.customscroll').css("height", scrollheight);
});

$(function () {
    var winheight = $(window).height();
    var scrollheight = winheight - 234;
    $('.customscroll').css("height", scrollheight);
});

$('.customscroll').enscroll({
    showOnHover: true,
    verticalTrackClass: 'track3',
    verticalHandleClass: 'handle3'
});



$(function () {
    $(".signUpControlbx").find("div.active").removeClass("active");
    $("#dvSelectPlan").addClass("active");
});


$(document).ready(function () {
    $('#PlansByPromoCode').click(function (e) {
        //    e.preventdefault();
        ChangePlanListByPromoCode();
    });

    $("#btn-next").click(function () {
        //lnkChoosePlan pricingPlans lnkChoosePlan
        var planId = $("#hdnPlanId").val();
        var promocode = $('#txtPromoCode').val();
        if (planId > 0) {
            ShowLoading();
            $.ajax({
                type: 'GET',
                url: '/Account/UserInformation',
                data: { planId: planId, promocode: promocode },
                success: function (data) {
                    $("#mainContainerBox").html(data);
                    HideLoading();
                },
                failure: function () { HideLoading(); }
            });
        }
        else {
            $("#EnableTrue").show();
            window.parent.$("#mainBox").animate({ scrollTop: 0 }, 0);
        }
    });

    $(function () {
        var winheight = $(window).height();
        var scrollheight = winheight - 234;
        //alert(scrollheight);
        $('.customscroll').css("height", scrollheight);
    });
});

function HideMsg(id) {
    window.setTimeout(function () {
        $(id).fadeTo(1000, 0).slideUp(1000, function () {
            $(this).css('display', 'none');
        });
    }, 2000);
}

function ChangePlanListByPromoCode() {
    var promocode = $('#txtPromoCode').val();
    if (promocode != "") {
        ShowLoading();
        $.ajax({
            type: 'GET',
            url: '/Account/GetPlansByPromocode',
            data: { promocode: promocode },
            success: function (data) {
                HideLoading();
                $("#txtPromoCode").removeClass("input-validation-error");
                $("#PromocodeSuccessmsg").css('opacity', '');
                $("#PromocodeSuccessmsg").css('display', 'block');
                HideMsg("#PromocodeSuccessmsg");
                $("#PromocodeErrMsg").css('display', 'none');
                $("#dvPlanList").html(data);
            },
            failure: function (data) {
                HideLoading();
            }
        });
    } else {
        $("#Promocode").val('');
        $("#txtPromoCode").addClass("input-validation-error");
        $("#PromocodeErrMsg").css('opacity', '');
        $("#PromocodeErrMsg").css('display', 'block');
        HideMsg("#PromocodeErrMsg");
    }
    return false;
}