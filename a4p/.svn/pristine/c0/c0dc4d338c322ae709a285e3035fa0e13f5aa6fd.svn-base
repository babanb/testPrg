$(function () {

    $('#addImmunization').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#addModalImmunization').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addImmunizationForm");

            $('#addModalImmunization').modal('show');

        });

    });

});

function CloseDialogimmunization() {
    $('.modal').modal('hide');
    ShowAjaxSuccessMessage($("#immunizationSuccessMessage").val());
}