$(document).ready(function () {
    window.parent.$("body").animate({ scrollTop: 0 }, 0);
    Initialize();

    $('#BasicInfo').removeClass('tab-pane fade in active').addClass('tab-pane fade');
    $('#Billing').removeClass('tab-pane fade').addClass('tab-pane fade in active');
    $('#billingInfoLi').addClass('active');
    $('#basicInfoLi').removeClass('active');
    $('#confirmationLi').removeClass('active');

    /* 
    commented to hide the permenant address section as per marie's suggession 

    if (localStorage['CheckBack'] == 'Yes') {
        $("#IsBillingAddressSame").prop("checked", true);
        $('#plaintext-add').show('fast');
        $('#billing-add').hide('fast');
        
    } else {
        $("#IsBillingAddressSame").prop("checked", false);
        $('#billing-add').show('fast');
        $('#plaintext-add').hide('fast');
    }
    
    $('#IsBillingAddressSame').change(function () {
        if (this.checked) {
            localStorage['CheckBack'] = 'Yes'
            $('#plaintext-add').show('fast');
            $('#billing-add').hide('fast');
        }
        else {
            localStorage['CheckBack'] = 'No'
            $('#plaintext-add').hide('fast');
            $('#billing-add').show('fast');

        }
    });

    localStorage['CheckBack'] = 'No'
*/

    UpdateBillingStates();
    $.validator.unobtrusive.parse("#PaymentForm");

    retriveTempBillingValues();

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
            $.getJSON(GetApplicationPath() + "Account/GetStates", { country: Country },
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
