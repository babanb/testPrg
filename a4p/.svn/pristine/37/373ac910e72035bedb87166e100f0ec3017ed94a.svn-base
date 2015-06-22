$(function () {

    $('#CallUpgradePlanModel').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#UpgradeMyPlan').html(data);

            Initialize();
            $('#UserPlanInfo').removeClass('tab-pane fade in active').addClass('tab-pane fade');
            $('#UpgradeMyPlan').removeClass('tab-pane fade').addClass('tab-pane fade in active');

        });

    });

    $('#CallAddPetsModel').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#MyPlanAddPets').html(data);

            Initialize();

            $('#UserPlanInfo').removeClass('tab-pane fade in active').addClass('tab-pane fade');
            $('#MyPlanAddPets').removeClass('tab-pane fade').addClass('tab-pane fade in active');

        });

    });

    $('#CallRenewPlanModel').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#RenewMyPlan').html(data);

            Initialize();

            $('#UserPlanInfo').removeClass('tab-pane fade in active').addClass('tab-pane fade');
            $('#RenewMyPlan').removeClass('tab-pane fade').addClass('tab-pane fade in active');

        });

    });

    $('#GetPlanRenewalDetails').click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#PlanRenewalDetails').html(data);

            Initialize();

            $('#PlanRenewalDetails').modal('show');

        });

    });

    $('#CancelRenewalPlan').click(function () {
        $('#RenewMyPlan').removeClass('tab-pane fade in active').addClass('tab-pane fade');
        $('#UserPlanInfo').removeClass('tab-pane fade').addClass('tab-pane fade in active');

    });

    $('#lnkEditPlan').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#dvPlanEdit').html(data);
            Initialize();
            $('#dvPlanEdit').modal('show');
        });
    });

    $("#chkActive").click(function () {
        var isChecked = $("#chkActive").is(":checked");
        if (isChecked) {
            $("#frmMarkUserActive").submit();
        }
    });
});

$('#myAccount a[data-toggle="tab"]').click(function () {
    $('#myAccount a[data-toggle="tab"]').removeClass("active");
    $($(this).attr('href')).addClass("active");
    $(this).addClass("active");
});

function CloseDialog() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
}
