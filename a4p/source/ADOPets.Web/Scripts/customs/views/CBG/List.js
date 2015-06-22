$(function () {

    $('.editCBG').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editCBGModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editCBGForm");

            $('#editCBGModal').modal('show');

        });
    });


    $('.deleteCBG').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteCBGModal').html(data);

            $('#deleteCBGModal').modal('show');

        });
    });


    DrawTable('#Table_CBG', [0]);

});
