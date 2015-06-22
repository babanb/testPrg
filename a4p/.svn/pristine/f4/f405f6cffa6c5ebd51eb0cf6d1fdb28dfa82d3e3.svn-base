$(document).ready(function () {
    retriveTempBillingValues();
    UpdateBillingStates();
    $("#Country").change(function () {
        UpdateBillingStates();
    });

    function UpdateBillingStates() {
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
                $.getJSON(GetApplicationPath() + "Account/GetStates", { country: Country },
                    function (data) {
                        $.each(data, function (index, item) {
                            if (item.Value == State || item.Value == localStorage['state']) {
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
    $(document).off("click", "#next-billing-bt").on("click", "#next-billing-bt", function (e) {
        e.preventDefault();
        $.validator.unobtrusive.parse("#SMOBillingForm");
        var validator = $("#SMOBillingForm").valid();
        if (validator) {
            saveTempBillingValues();
            $.ajax({
                url: $('#BillingURL').val(),
                data: $("#SMOBillingForm").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (success) {
                    if (success) {
                        var url = $('#ConfirmationURL').val();
                        $('#divStep').load(url);
                        $('#Step2').removeClass('active');
                        $('#Step3').addClass("active");
                    }
                },
                error: function (req, status, error) {

                }
            });
        }
        else {
        }
    });
    $(document).off("click", "#back-billing-bt").on("click", "#back-billing-bt", function (e) {
        saveTempBillingValues();
        var url = $('#SetupURL').val();
        $.get(url, function (data) {
            $('#divStep').html(data);
            Initialize();
            var urlList = $('#InvestigationListURL').val();
            $.get(urlList, function (data) {
                $('#divInvestigation').html(data);
            });
        });
        
    });
});
function saveTempBillingValues() {
    localStorage['cardType'] = $('#CreditCardType').val();
    localStorage['cardNumber'] = $('#CreditCardNumber').val();
    localStorage['expdate'] = $('#ExpirationDate').val();
    localStorage['cvv'] = $('#CCV').val();
    localStorage['address1'] = $('#Address1').val();
    localStorage['address2'] = $('#Address2').val();
    localStorage['city'] = $('#City').val();
    localStorage['country'] = $('#Country').val();
    localStorage['state'] = $('#State').val();
    localStorage['zip'] = $('#Zip').val();
}
function retriveTempBillingValues() {
    $('#CreditCardType').val(localStorage['cardType']);
    $('#CreditCardNumber').val(localStorage['cardNumber']);
    $('#ExpirationDate').val(localStorage['expdate']);
    $('#CCV').val(localStorage['cvv']);
    $('#Address1').val(localStorage['address1']);
    $('#Address2').val(localStorage['address2']);
    $('#City').val(localStorage['city']);
    // $('#Country').val(localStorage['country']);
    if (localStorage['country']) { $('#Country').val(localStorage['country']); }
    $('#State').val(localStorage['state']);
    $('#Zip').val(localStorage['zip']);
}

