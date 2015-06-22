$(function () {

    $('.editPetContact').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editeModalPetContact').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editPetContactForm");

            $('#editeModalPetContact').modal('show');

        });
    });



    $('.editVeterinarian').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editeModalVeterinarian').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editVeterinarianForm");

            $('#editeModalVeterinarian').modal('show');

        });
    });

    DrawTable('#contactList'); //Table_Contact original id here
});

function CloseDialog() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
    location.reload(true);
}

function CloseDialogPetContact() {
    $('.modal').modal('hide');
    $(".modal-body").empty();

    location.reload(true);
}

function CloseDialogVeterinarian() {
    $('.modal').modal('hide');
    $(".modal-body").empty();
   
    location.reload(true);
}