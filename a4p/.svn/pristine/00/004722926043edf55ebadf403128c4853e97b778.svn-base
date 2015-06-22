$(function () {

    $('.editSmoRequest').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editModalSMO').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editConsultationForm");

            $('#editModalSMO').modal('show');

        });
    });


    $('.deleteSmoRequest').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteModalSMO').html(data);

            $('#deleteModalSMO').modal('show');

        });
    });


    //DrawTable('#Table_Consultation', [1]);  TODO:: NUTAN 26 Sept *@

});