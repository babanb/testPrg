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


    $('.deletePetContact').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteModalPetContact').html(data);

            $('#deleteModalPetContact').modal('show');

        });
    });

    DrawTable('#Table_petcontact');
    filteronText('#FirstNameFilter', 1, '#Table_petcontact');
    filteronText('#LastNameFilter', 2, '#Table_petcontact');

});