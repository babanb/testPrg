$(function () {

    $('.editVeterinarian').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editeModalVeterinarian').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editVeterinarianForm");

            $('#editeModalVeterinarian').modal('show');

        });
    });


    $('.deleteVeterinarian').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteModalVeterinarian').html(data);

            $('#deleteModalVeterinarian').modal('show');

        });
    });

    DrawTable('#Table_veterinarian'); 
    filteronText('#FirstNameFilter', 2, '#Table_veterinarian'); 
    filteronText('#LastNameFilter', 3, '#Table_veterinarian');  
});


//DrawTable('#Table_veterinarian'); 
//TODO:: NUTAN 26 Sept 

