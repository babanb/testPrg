$(document).ready(function () {
    $("#signupStep1,#signupStep2,#signupStep3").removeClass("active");
    $("#signupStep3").addClass("active");

    window.parent.$("body").animate({ scrollTop: 0 }, 0);
    $('#BasicInfo').removeClass('tab-pane fade in active').addClass('tab-pane fade');
    $('#Billing').removeClass('tab-pane fade in active').addClass('tab-pane fade');
    $('#Confirmation').removeClass('tab-pane fade').addClass('tab-pane fade in active');
    $('#billingInfoLi').removeClass('active');
    $('#basicInfoLi').removeClass('active');
    $('#confirmationLi').addClass('active');
    
});

$('.pull-right a').click(function () {
    if ($(this).attr('href') == '#BasicInfo') {
        $("#signupStep1,#signupStep2,#signupStep3").removeClass("active");
        $("#signupStep1").addClass("active");
        $('#basicInfoLi').addClass('active');
        $('#billingInfoLi').removeClass('active');
        $('#confirmationLi').removeClass('active');
    } else if ($(this).attr('href') == '#Billing') {
        $("#signupStep1,#signupStep2,#signupStep3").removeClass("active");
        $("#signupStep2").addClass("active");
        $('#billingInfoLi').addClass('active');
        $('#basicInfoLi').removeClass('active');
        $('#confirmationLi').removeClass('active');
    }

  
   
});

function onBegin() {
    ShowLoading();
    localStorage.clear();
}
function onComplete() {
    HideLoading();
    localStorage.clear();
}