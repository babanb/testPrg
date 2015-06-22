$(function () {

    $('#addHemogram').click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addHemogramModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addHemogramForm");

            $('#addHemogramModal').modal('show');
            $('.form-control#LeftValue').val('');
        });

    });


    $('#ShowHemogramTable').click(function (e) {
        $('#HemogramTable').show();
        $('#HemogramCurve').hide();
        $('#ShowHemogramTable').hide();
        $('#showHideHemogramButtons').show();
    });

    $('#HemogramDataCurve').click(function (e) {
        $('#HemogramTable').hide();
        $('#HemogramCurve').show();
        $('#ShowHemogramTable').show();
        $('#showHideHemogramButtons').hide();
        createHemogramChart();
    });

});

function CloseDialogHemogram() {
    $('.modal').modal('hide');
    ShowAjaxSuccessMessage($("#hemogramSuccessMessage").val());
}