$(document).ready(function () {
    UpdatePhoneNumberValidation('Home');
    if ($("meta[name='accept-domain']").attr("content") != "French") {
        UpdatePhoneNumberValidation('Office');
    }
    UpdatePhoneNumberValidation('Cell');
    UpdatePhoneNumberValidation('Fax');
    if ($("meta[name='accept-domain']").attr("content") != "French") {
        UpdateOwnerStates();
    }
    $("#Country").change(function () {
        if ($("meta[name='accept-domain']").attr("content") != "French") {
            UpdateOwnerStates();
        }
    });


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

    $('#EndDate').change(function () {
        $('#StartDate').datepicker('setEndDate', $(this).val());
    });
});



function UpdateOwnerStates() {

    var Country = $('#Country').val();
    var State = $('#State').val();

    var subItems = "<option value>" + $("#State").children()[0].text + "</option>";
    if (Country == "France") {
        $("#State").html(subItems);
        $("#State").prop("disabled", true);
    }
    else {
        $("#State").prop("disabled", false);
        if ($("#Country option:selected").index() == 0) {
            $("#State").html(subItems);
        } else {
            $.getJSON(GetApplicationPath() + "Pet/GetStates", { country: Country },
                function (data) {
                    $.each(data, function (index, item) {
                        if (item.Value == State) {
                            subItems += "<option value='" + item.Value + "' selected>" + item.Text + "</option>";
                        } else {
                            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                        }
                    });
                    $("#State").html(subItems);
                });
        }
    }
}