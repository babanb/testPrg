$(document).ready(function () {

    var culture = GetCurrentCulture();

    $('#StartDate').datepicker({
        language: culture,
        autoclose: true,
        endDate: $('#EndDate').val() != "" ? $('#EndDate').val() : '0d'
    });

    $('#EndDate').datepicker({
        language: culture,
        autoclose: true,
        startDate: $('#StartDate').val(),
        endDate: '0d'
    });


    $('#StartDate').change(function () {
        $('#EndDate').datepicker('setStartDate', $(this).val());
    });

    //$('#EndDate').change(function () {
    //    $('#StartDate').datepicker('setEndDate', $(this).val());
    //});

});