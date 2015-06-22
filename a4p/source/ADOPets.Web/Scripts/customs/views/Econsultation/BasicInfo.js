$(document).ready(function () {

    $('#bestcontactoption').on('change', function () {
        
        if ($(this).val() == 'Email') {
            $('#enteremail').show();
            $('#enterphone').hide();
        } else {
            $('#enteremail').hide();
            $('#enterphone').show();
        }
    });

    retriveTempBillingValues();
    UpdateBillingStates();
    $.validator.unobtrusive.parse("#PaymentForm");


    $("#BillingCountry").change(function () {
        UpdateBillingStates();

    });

    $("#btnback").click(function () {
        saveTempBillingValues();
        $('#billingInfoLi').removeClass('active');
        $('#basicInfoLi').addClass('active');
    });

    $("#next-bt").click(function () {
        saveTempBillingValues();
    });
  
});

function UpdateBillingStates() {

    var Country = $('#BillingCountry').val();
    var State = $('#BillingState').val();

    var subItems = "<option value>" + $("#State").children()[0].text + "</option>";
    if (Country == "France") {
        $("#BillingState").html(subItems);
        $("#BillingState").prop("disabled", true);
    }
    else {
        $("#BillingState").prop("disabled", false);

        if ($("#BillingCountry option:selected").index() == 0) {
            $("#BillingState").html(subItems);
        } else {
            $.getJSON(GetApplicationPath() + "Econsultation/GetStates", { country: Country },
                function (data) {
                    $.each(data, function (index, item) {

                        if (item.Value == State || item.Text == localStorage['state']) {
                            subItems += "<option value='" + item.Value + "' selected>" + item.Text + "</option>";
                        } else {
                            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                        }
                    });

                    $("#BillingState").html(subItems);
                });
        }
    }
}



