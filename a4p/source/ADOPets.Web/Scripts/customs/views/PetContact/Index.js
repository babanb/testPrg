$(function () {

    $('#addPetContact').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {

            $('#addModalPetContact').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addPetContactForm");

            $('#addModalPetContact').modal('show');

            $('#addModalPetContact').on('hidden.bs.modal', function () {
                $(".modal-body").empty();
            });

        });

    });
});

function CloseDialogPetContact() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
    ShowAjaxSuccessMessage($("#petcontactSuccessMessage").val());
}