$(document).ready(function () {

    UpdatePhoneNumberValidation('Phone');

    var culture = GetCurrentCulture();

    $('#StartDate').datepicker({
        language: culture,
        autoclose: true,
        endDate: $('#EndDate').val()
    });

    $('#EndDate').datepicker({
        language: culture,
        autoclose: true,
        startDate: $('#StartDate').val()
    });


    $('#StartDate').change(function () {
        $('#EndDate').datepicker('setStartDate', $(this).val());
    });

    $('#EndDate').change(function () {
        $('#StartDate').datepicker('setEndDate', $(this).val());
    });

});