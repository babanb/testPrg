$(function () {



    $('#addCBG').click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addCBGModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addCBGForm");

            $('#addCBGModal').modal('show');
            $('.form-control#LeftValue').val('');
        });

    });

    $('#ShowCBGTable').click(function (e) {
        $('#CBGTable').show();
        $('#ShowCBGTable').hide();
        $('#showHideCBGButtons').show();
        $('#CBGCurve').hide();        
    });

    $('#CBGDataCurve').click(function (e) {
        $('#CBGTable').hide();
        $('#showHideCBGButtons').hide();
        $('#ShowCBGTable').show();
        $('#CBGCurve').show();
        createCBGChart();
    });

});

function CloseDialogCBG() {
    $('.modal').modal('hide');
    ShowAjaxSuccessMessage($("#CBGSuccessMessage").val());
}
