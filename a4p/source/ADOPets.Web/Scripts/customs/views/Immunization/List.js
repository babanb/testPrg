$(function () {

    $('.editImmunization').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editModalImmunization').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editImmunizationForm");

            $('#editModalImmunization').modal('show');

        });
    });


    $('.deleteImmunization').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteModalImmunization').html(data);

            $('#deleteModalImmunization').modal('show');

        });
    });

    DrawTable('#Table_immunization', [1]); 

});