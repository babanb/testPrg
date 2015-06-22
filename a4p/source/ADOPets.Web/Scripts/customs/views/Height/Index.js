$(function () {

    $('#addHeight').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addHeightModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addHeightForm");

            ValidateHeightLeftValueRange('LeftValue', 'MeasureUnit', 'LeftValueRange');
            ValidateHeightRightValueRange('RightValue', 'MeasureUnit', 'RightValueRange');

            $('#addHeightModal').modal('show');

        });

    });

    $('#ShowHeightTable').click(function (e) {
        $('#HeightTable').show();
        $('#HeightCurve').hide();
        $('#showHideHeightButtons').show();
        $('#showHideHeightButtons-back').hide();
    });

    $('#HeightDataCurve').click(function (e) {
        $('#HeightTable').hide();
        $('#HeightCurve').show();
        $('#showHideHeightButtons').hide();
        $('#showHideHeightButtons-back').show();
        createHeightChart();
    });



});

function ValidateHeightLeftValueRange(targetElementId, measureUnitId, messageId) {

    var measureUnit = $('#' + measureUnitId).val();
    var message = $('#' + messageId).val();

    $('#' + targetElementId).rules('add',
        {
            range: measureUnit == 'Feet' ? [0, 49] : [0, 15],
            messages: {
                range: message
            }
        });
}

function ValidateHeightRightValueRange(targetElementId, measureUnitId, messageId) {

    var measureUnit = $('#' + measureUnitId).val();
    var message = $('#' + messageId).val();

    $('#' + targetElementId).rules('add',
        {
            range: measureUnit == 'Feet' ? [0, 11] : [0, 99],
            messages: {
                range: message
            }
        });
}

function CloseDialogheight() {
    $('.modal').modal('hide');
    $('body').removeClass('modal-open');
    $('.modal-backdrop').remove();
    ShowAjaxSuccessMessage($("#heightSuccessMessage").val());
}