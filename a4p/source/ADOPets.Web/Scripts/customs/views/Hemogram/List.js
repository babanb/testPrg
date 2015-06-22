$(function () {

    $('.editHemogram').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editHemogramModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editHemogramForm");

            $('#editHemogramModal').modal('show');

        });
    });


    $('.deleteHemogram').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteHemogramModal').html(data);

            $('#deleteHemogramModal').modal('show');

        });
    });


    DrawTable('#Table_Hemogram', [0]);

});
