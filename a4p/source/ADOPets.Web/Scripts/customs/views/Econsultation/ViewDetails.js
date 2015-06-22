$(function () {

    $("#editEconsultation").click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editModalEconsultation').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editEconsultationForm");

            $('#editModalEconsultation').modal('show');

        });
    });
});

function CloseDialogEconsultation() {
    location.reload();
}