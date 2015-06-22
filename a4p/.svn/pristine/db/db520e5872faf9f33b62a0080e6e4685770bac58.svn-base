$(function () {

    $('.editWeight').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editWeightModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editWeightForm");
            
            ValidateWeightLeftValueRange('LeftValue', 'MeasureUnit', 'LeftValueRange');
            ValidateWeightRightValueRange('RightValue', 'MeasureUnit', 'RightValueRange');

            $('#editWeightModal').modal('show');

        });
    });


    $('.deleteWeight').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteWeightModal').html(data);

            $('#deleteWeightModal').modal('show');

        });
    });

    DrawTable('#Table_Weight', [0]);
});
