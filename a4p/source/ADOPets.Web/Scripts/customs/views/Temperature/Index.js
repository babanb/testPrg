$(function () {

    $('#addTemperature').click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addTemperatureModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addTemperatureForm");

            ValidateTemperatureRange('LeftValue', 'MeasureUnit', 'LeftValueRange');

            $('#addTemperatureModal').modal('show');
        });

    });

    $('#ShowTemperatureTable').click(function (e) {
        $('#TemperatureTable').show();
        $('#TemperatureCurve').hide();
        $('#showHideTemperatureButtons').show();
        $('#showHideTemperatureButtons-back').hide();
    });

    $('#TemperatureDataCurve').click(function (e) {
        $('#TemperatureTable').hide();
        $('#TemperatureCurve').show();
        $('#showHideTemperatureButtons').hide();
        $('#showHideTemperatureButtons-back').show();
        createTemperatureChart();
    });

});

function ValidateTemperatureRange(targetElementId, measureUnitId, messageId) {

    var measureUnit = $('#' + measureUnitId).val();
    var message = $('#' + messageId).val();

    $('#' + targetElementId).rules('add',
        {
            range: measureUnit == 'Fahrenheit' ? [32, 140] : [0, 60],
            messages: {
                range: message
            }
        });
}

function CloseDialogTemperature() {
    $('.modal').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
    ShowAjaxSuccessMessage($("#temperatureSuccessMessage").val());
}