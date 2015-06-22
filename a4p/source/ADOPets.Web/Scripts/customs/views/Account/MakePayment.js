$(window).on('load resize', function () {
    var winheight = $(window).height();
    var scrollheight = winheight - 234;
    $('.customscroll').css("height", scrollheight);
});

$(function () {
    var winheight = $(window).height();
    var scrollheight = winheight - 234;
    $('.customscroll').css("height", scrollheight);
});

$('.customscroll').enscroll({
    showOnHover: true,
    verticalTrackClass: 'track3',
    verticalHandleClass: 'handle3'
});

//for tooltip
$(function () { $("[data-toggle='tooltip']").tooltip(); });

$(function () {
    $(".signUpControlbx").find("div.active").removeClass("active");
    $("#dvMakePayment").addClass("active");
});

retriveTempBillingValues();

if ($("meta[name='accept-domain']").attr("content") != "French") {
    UpdateBillingStates();
}

$("#BillingCountry").change(function () {
    if ($("meta[name='accept-domain']").attr("content") != "French") {
        UpdateBillingStates();
    }
});

$(document).ready(function () {
    Initialize();
    $.validator.unobtrusive.parse("#frmMakePayment");
    $("#btn-next").unbind('click');
    $("#btn-next").click(function () {
        if ($('#frmMakePayment').valid()) {

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
            url: "/Account/MakePayment",
            data: $("#frmMakePayment").serialize(),
            success: function (result) {
                HideLoading();
                $('#mainContainerBox').html(result);
                $('#dvPaymentConfirmation').modal('hide');
                $(".modal-backdrop").hide();
            },
            failure: function (result) {
                $('#mainContainerBox').html(result);
                HideLoading();
            }
        });
    });

    $("#btnBackStep2").click(function () {
        saveTempBillingValues();
        ShowLoading();
        $.ajax({
            type: "GET",
            url: "/Account/UserInformation",
            success: function (result) {
                HideLoading();
                $('#mainContainerBox').html(result);
            },
            failure: function (result) {
                HideLoading();
            }
        });
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

