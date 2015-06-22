$(function () {

    $('#addPulse').click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addPulseModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addPulseForm");

            $('#addPulseModal').modal('show');

        });

    });

    $('#ShowPulseTable').click(function (e) {
        $('#PulseTable').show();
        $('#PulseCurve').hide();
        $('#showHidePulseButtons').show();
        $('#ShowPulseTable').hide();
    });

    $('#PulseDataCurve').click(function (e) {
        $('#PulseTable').hide();
        $('#PulseCurve').show();
        $('#showHidePulseButtons').hide();
        $('#ShowPulseTable').show();
        createPulseChart();
    });

});

function CloseDialogPulse() {
    $('.modal').modal('hide');
    ShowAjaxSuccessMessage($("#pulseSuccessMessage").val());
}