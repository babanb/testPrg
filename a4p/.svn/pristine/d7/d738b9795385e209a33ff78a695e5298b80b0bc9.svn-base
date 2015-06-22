$(function () {

    $('.editSurgery').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editModalSurgery').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editSurgeryForm");

            $('#editModalSurgery').modal('show');

        });
    });


    $('.deleteSurgery').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteModalSurgery').html(data);

            $('#deleteModalSurgery').modal('show');

        });
    });


   DrawTable('#Table_surgery', [1]);

});