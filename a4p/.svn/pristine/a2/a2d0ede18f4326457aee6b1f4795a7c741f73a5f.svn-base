$(function () {

    $('.editAllergy').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editModalAllergy').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editAllergyForm");

            $('#editModalAllergy').modal('show');

        });
    });


    $('.deleteAllergy').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteModalAllergy').html(data);

            $('#deleteModalAllergy').modal('show');

        });
    });


    DrawTable('#Table_Allergy', [1]);  

});