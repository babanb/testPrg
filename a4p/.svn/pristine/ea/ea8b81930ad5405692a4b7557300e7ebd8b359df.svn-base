$(function () {

    $('#addHemoglobin').click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addHemoglobinModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addHemoglobinForm");

            $('#addHemoglobinModal').modal('show');
            $('.form-control#LeftValue').val('');
        });

    });


    $('#ShowHemoglobinTable').click(function (e) {
        $('#HemoglobinTable').show();
        $('#ShowHemoglobinTable').hide();
        $('#showHideHemoglobinButtons').show();
        $('#HemoglobinCurve').hide();
    });

    $('#HemoglobinDataCurve').click(function (e) {
        $('#HemoglobinTable').hide();
        $('#showHideHemoglobinButtons').hide();
        $('#ShowHemoglobinTable').show();
        $('#HemoglobinCurve').show();
        createHemoglobinChart();
    });

});

function CloseDialogHemoglobin() {
    $('.modal').modal('hide');
    ShowAjaxSuccessMessage($("#HemoglobinSuccessMessage").val());
}