$(function () {

    $('.editHeight').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editHeightModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editHeightForm");

            ValidateHeightLeftValueRange('LeftValue', 'MeasureUnit', 'LeftValueRange');
            ValidateHeightRightValueRange('RightValue', 'MeasureUnit', 'RightValueRange');

            $('#editHeightModal').modal('show');

        });
    });


    $('.deleteHeight').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteHeightModal').html(data);

            $('#deleteHeightModal').modal('show');

        });
    });

     DrawTable('#Table_Height', [0]);


});
