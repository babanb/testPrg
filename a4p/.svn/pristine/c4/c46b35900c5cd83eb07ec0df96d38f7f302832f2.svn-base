    $("#BillingCountry").change(function () {
        if ($("meta[name='accept-domain']").attr("content") != "French") {
            UpdateBillingStates();
        }
    });

    $(document).ready(function () {
        Initialize();
        $.validator.unobtrusive.parse("#PlanBillingForm");
        $("#btn-next").unbind('click');
        $("#btn-next").click(function () {
            if ($('#PlanBillingForm').valid()) {
                $("#dvPaymentConfirmation").modal('show');
            }
            return false;
        });

        $("#btnConfirm").click(function () {
            ShowLoading();
            $('#dvPaymentConfirmation').modal('hide');
            $(".modal-backdrop").hide();
            $.ajax({
                type: "POST",
                url: "/Profile/PlanBilling",
                data: $("#PlanBillingForm").serialize(),
                success: function (result) {
                    HideLoading();
                    window.location.href = "/Profile/OwnerDetail";
                },
                failure: function (result) {
                    $('#mainContainerBox').html(result);
                    HideLoading();
                }
            });
        });

    });


function saveTempBillingValues() {
    localStorage['cardType1'] = $('#CreditCardType').val();
    localStorage['cardNumber1'] = $('#CreditCardNumber').val();
    localStorage['expdate1'] = $('#ExpirationDate').val();
    localStorage['cvv1'] = $('#CVV').val();
    localStorage['address_1'] = $('#BillingAddress1').val();
    localStorage['address_2'] = $('#BillingAddress2').val();
    localStorage['city1'] = $('#BillingCity').val();
    localStorage['country1'] = $('#BillingCountry').val();
    localStorage['state1'] = $('#BillingState').val();
    localStorage['zip1'] = $('#BillingZip').val();
}

function retriveTempBillingValues() {
    $('#CreditCardType').val(localStorage['cardType1']);
    $('#CreditCardNumber').val(localStorage['cardNumber1']);
    $('#ExpirationDate').val(localStorage['expdate1']);
    $('#CVV').val(localStorage['cvv1']);
    $('#BillingAddress1').val(localStorage['address_1']);
    $('#BillingAddress2').val(localStorage['address_2']);
    $('#BillingCity').val(localStorage['city1']);
    // $('#BillingCountry').val(localStorage['country1']);
    if (localStorage['country1']) { $('#BillingCountry').val(localStorage['country1']); }
    UpdateBillingStates();
    $('#BillingState').val(localStorage['state1']);
    $('#BillingZip').val(localStorage['zip1']);
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
            $.getJSON(GetApplicationPath() + "Profile/GetStates", { country: Country },
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