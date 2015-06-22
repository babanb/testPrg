$(function () {

    $('.editPulse').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editPulseModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editPulseForm");

            $('#editPulseModal').modal('show');

        });
    });


    $('.deletePulse').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deletePulseModal').html(data);

            $('#deletePulseModal').modal('show');

        });
    });

     DrawTable('#Table_Pulse', [0]);


});
