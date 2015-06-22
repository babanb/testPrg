$(document).ready(function () {

    var culture = GetCurrentCulture();

    $('#VisitDate').datepicker({
        language: culture,
        autoclose: true,
        endDate: $('#EndDate').val() != "" ? $('#EndDate').val() : '0d'
    });
    
    $('#EndDate').datepicker({
        language: culture,
        autoclose: true,
        startDate: $('#VisitDate').val(),
        endDate: '0d'
    });

    $('#VisitDate').change(function () {
        $('#EndDate').datepicker('setStartDate', $(this).val());
    });

    //$('#EndDate').change(function () {
    //    $('#VisitDate').datepicker('setEndDate', $(this).val());
    //});

});