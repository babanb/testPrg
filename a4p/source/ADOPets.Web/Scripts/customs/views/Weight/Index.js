$(function () {

    $('#addWeight').click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addWeightModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addWeightForm");

            ValidateWeightLeftValueRange('LeftValue', 'MeasureUnit', 'LeftValueRange');
            ValidateWeightRightValueRange('RightValue', 'MeasureUnit', 'RightValueRange');

            $('#addWeightModal').modal('show');

        });

    });

    $('#ShowWeightTable').click(function (e) {
        $('#WeightTable').show();
        $('#WeightCurve').hide();
        $('#showHideWeightButtons').show();
        $('#showHideWeightButtons-back').hide();
    });

    $('#WeightDataCurve').click(function (e) {
        $('#WeightTable').hide();
        $('#WeightCurve').show();
        $('#showHideWeightButtons').hide();
        $('#showHideWeightButtons-back').show();
        createWeightChart();
    });

});

function ValidateWeightLeftValueRange(targetElementId, measureUnitId, messageId) {

    var measureUnit = $('#' + measureUnitId).val();
    var message = $('#' + messageId).val();

    $('#' + targetElementId).rules('add',
        {
            range: measureUnit == 'Pounds' ? [0, 11023] : [0, 5000],
            messages: {
                range: message
            }
        });
}

function ValidateWeightRightValueRange(targetElementId, measureUnitId, messageId) {

    var measureUnit = $('#' + measureUnitId).val();
    var message = $('#' + messageId).val();

    $('#' + targetElementId).rules('add',
        {
            range: measureUnit == 'Pounds' ? [0, 15] : [0, 999],
            messages: {
                range: message
            }
        });
}

function CloseDialogWeight() {
    $('.modal').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
    ShowAjaxSuccessMessage($("#weightSuccessMessage").val());
}