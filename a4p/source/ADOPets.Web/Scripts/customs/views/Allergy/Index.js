$(function () {

    $('#addAllergy').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#addModalAllergy').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addAllergyForm");

            $('#addModalAllergy').modal('show');

        });

    });

});

function CloseDialogAllergy() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
    ShowAjaxSuccessMessage($("#AllergySuccessMessage").val());
}