$(document).ready(function () { 
    $('#MyPlanAddPets').removeClass('tab-pane fade in active').addClass('tab-pane fade');
    $('#PlanBilling').removeClass('tab-pane fade in active').addClass('tab-pane fade');
    $('#PlanConfirmation').removeClass('tab-pane fade').addClass('tab-pane fade in active');


    $("#backToBilling").click(function () {
        $('#MyPlanAddPets').removeClass('tab-pane fade in active').addClass('tab-pane fade');
        $('#PlanConfirmation').removeClass('in tab-pane fade active').addClass('tab-pane fade');
        $('#PlanBilling').removeClass('tab-pane fade').addClass('tab-pane fade in active');
    });

});

function onBegin() {
    ShowLoading();
    localStorage.clear();
}
function onComplete() {
    HideLoading();
    localStorage.clear();
}