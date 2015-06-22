$(function () {

    $('#addSerumElectrolyte').click(function (e) {

        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addSerumElectrolyteModal').html(data);

            Initialize();

            $.validator.unobtrusive.parse("#addSerumElectrolyteForm");

            $('#addSerumElectrolyteModal').modal('show');
            $('.form-control#LeftValue').val('');
        });

    });


    $('#ShowSerumElectrolyteTable').click(function (e) {
        $('#SerumElectrolyteTable').show();
        $('#SerumElectrolyteCurve').hide();
        $('#showHideSerumElectrolyteButtons').show();
        $('#ShowSerumElectrolyteTable').hide();
        
    });

    $('#SerumElectrolyteDataCurve').click(function (e) {
        $('#SerumElectrolyteTable').hide();
        $('#SerumElectrolyteCurve').show();
        $('#showHideSerumElectrolyteButtons').hide();
        $('#ShowSerumElectrolyteTable').show();
        createSerumElectrolyteChart();
    });


});

function CloseDialogSerumElectrolyte() {
    $('.modal').modal('hide');
    ShowAjaxSuccessMessage($("#serumelectrolyteSuccessMessage").val());
}