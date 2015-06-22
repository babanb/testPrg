$(document).ready(function () {

    Initialize();

    $('#BasicInfo').removeClass('tab-pane fade in active').addClass('tab-pane fade');
    $('#Billing').removeClass('tab-pane fade').addClass('tab-pane fade in active');
    $('#billingInfoLi').addClass('active');
    $('#basicInfoLi').removeClass('active');
    $('#confirmationLi').removeClass('active');


    retriveTempBillingValues();
    UpdateBillingStates();
    $.validator.unobtrusive.parse("#PaymentForm");


    $('#ExpirationDate').removeAttr("data-val-date");

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

function saveTempBillingValues() {
    localStorage['cardType'] = $('#CreditCardType').val();
    localStorage['cardNumber'] = $('#CreditCardNumber').val();
    localStorage['expdate'] = $('#ExpirationDate').val();
    localStorage['cvv'] = $('#CVV').val();
    localStorage['address1'] = $('#BillingAddress1').val();
    localStorage['address2'] = $('#BillingAddress2').val();
    localStorage['city'] = $('#BillingCity').val();
    localStorage['country'] = $('#BillingCountry').val();
    localStorage['state'] = $('#BillingState').val();
    localStorage['zip'] = $('#BillingZip').val();
}

function retriveTempBillingValues() {
    $('#CreditCardType').val(localStorage['cardType']);
    $('#CreditCardNumber').val(localStorage['cardNumber']);
    $('#ExpirationDate').val(localStorage['expdate']);
    $('#CVV').val(localStorage['cvv']);
    $('#BillingAddress1').val(localStorage['address1']);
    $('#BillingAddress2').val(localStorage['address2']);
    $('#BillingCity').val(localStorage['city']);
    if (localStorage['country']) { $('#BillingCountry').val(localStorage['country']); }
    $('#BillingState').val(localStorage['state']);
    $('#BillingZip').val(localStorage['zip']);
}

function UpdateBillingStates() {

    var Country = $('#BillingCountry').val();
    var State = $('#BillingState').val();

    var subItems = "<option value>" + $("#BillingState").children()[0].text + "</option>";
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

                        if (item.Value == State || item.Text == localStorage['BillingState']) {
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
var culture = GetCurrentCulture();
$('.datepickerMonthYear').datepicker({
    language: culture,
    format: "mm/yyyy",
    minViewMode: 1,
    startDate: '1d',
    startView: 'decade',
    autoclose: true
});

jQuery(function () {
    return $("body").off("click", "#next-billing-bt", function (e) {
    });
});

jQuery(function () {
    return $("body").on("click", "#next-billing-bt", function (e) {
        e.preventDefault();
        $.validator.unobtrusive.parse("#EconsultationBillingForm");
        var validator = $("#EconsultationBillingForm").valid();

        if (validator) {
            ShowLoading();
            $.ajax({
                url: '/Econsultation/Billing',
                data: $("#EconsultationBillingForm").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (success) {
                    if (success) {
                        var url = "/Econsultation/Confirmation";
                        $('#Step2').removeClass('active');
                        $('#Step3').addClass("active");
                        $('#Confirmation').load(url);
                        toggle_visibility3();
                        HideLoading();
                    }
                },
                error: function (req, status, error) {

                }
            });
        }
        else {
        }
    });
});
jQuery(function () {
    return $("body").off("click", "#back-billing-bt", function (e) {
    });
});
jQuery(function () {
    return $("body").on("click", "#back-billing-bt", function (e) {
        var url = "/Econsultation/Setup";
        ShowLoading();
        $('#Setup').load(url);
        toggle_visibility1();
        HideLoading();
    });
});