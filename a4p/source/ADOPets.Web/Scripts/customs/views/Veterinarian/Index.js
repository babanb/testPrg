$(function () {

    $('#addVeterinarian').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#addModalVeterinarian').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addVeterinarianForm");

            $('#addModalVeterinarian').modal('show');

            $('#addModalVeterinarian').on('hidden.bs.modal', function () {
                $(".modal-body").empty();
            });

        });

    });

});

function CloseDialogVeterinarian() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
    ShowAjaxSuccessMessage($("#veterinarianSuccessMessage").val());
}

