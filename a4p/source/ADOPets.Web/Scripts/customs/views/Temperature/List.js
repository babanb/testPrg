$(function () {

    $('.editTemperature').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {
            $('#editTemperatureModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#editTemperatureForm");
            
            ValidateTemperatureRange('LeftValue', 'MeasureUnit', 'LeftValueRange');

            $('#editTemperatureModal').modal('show');

        });
    });


    $('.deleteTemperature').click(function (e) {

        e.preventDefault();

        $.get(this.href, function (data) {

            $('#deleteTemperatureModal').html(data);

            $('#deleteTemperatureModal').modal('show');

        });
    });

     DrawTable('#Table_Temperature', [0]);

});
