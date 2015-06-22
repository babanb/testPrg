$(function () {

    $('#addFoodPlan').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#addModalFoodPlan').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addFoodPlanForm");

            $('#addModalFoodPlan').modal('show');

        });

    });

});

function CloseDialogfoodplan() {
    $('.modal').modal('hide');
    ShowAjaxSuccessMessage($("#foodplanSuccessMessage").val());
}