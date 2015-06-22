$(function () {

    $('#GetPlanRenewalDetails').click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#PlanRenewalDetails').html(data);

            Initialize();

            $('#PlanRenewalDetails').modal('show');

        });

    });

});

function CloseDialogPulse() {
    $('.modal').modal('hide');
}