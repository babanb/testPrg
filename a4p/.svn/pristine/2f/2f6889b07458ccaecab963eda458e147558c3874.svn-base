$(document).ready(function () {
    
    var culture = GetCurrentCulture();

    $('#AdmissionDate').datepicker({
        language: culture,
        autoclose: true,
        endDate: $('#EndDate').val()
    });

    $('#EndDate').datepicker({
        language: culture,
        autoclose: true,
        startDate: $('#AdmissionDate').val(),
    });

    $('#AdmissionDate').change(function () {
        $('#EndDate').datepicker('setStartDate', $(this).val());
    });

    $('#EndDate').change(function () {
        $('#AdmissionDate').datepicker('setEndDate', $(this).val());
    });

});