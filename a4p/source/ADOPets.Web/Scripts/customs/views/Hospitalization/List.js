$(function () {

    $('.editHospitalization').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editModalHospitalization').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editHospitalizationForm");

            $('#editModalHospitalization').modal('show');

        });
    });


    $('.deleteHospitalization').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteModalHospitalization').html(data);

            $('#deleteModalHospitalization').modal('show');

        });
    });

    DrawTable('#Table_hospitalization', [2]);

});