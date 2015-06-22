$(function () {

    $('.editConsultation').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editModalConsultation').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editConsultationForm");

            $('#editModalConsultation').modal('show');

        });
    });


    $('.deleteConsultation').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteModalConsultation').html(data);

            $('#deleteModalConsultation').modal('show');

        });
    });

    DrawTable('#Table_consultation', [1]);

});