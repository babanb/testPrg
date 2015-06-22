$(function () {

    $('.editHemoglobin').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editHemoglobinModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editHemoglobinForm");

            $('#editHemoglobinModal').modal('show');
            $('#LeftValue').val('');
        });
    });


    $('.deleteHemoglobin').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteHemoglobinModal').html(data);

            $('#deleteHemoglobinModal').modal('show');

        });
    });


    DrawTable('#Table_Hemoglobin', [0]);

});
