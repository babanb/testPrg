
$("#BillingCountry").change(function () {
    if ($("meta[name='accept-domain']").attr("content") != "French") {
        UpdateBillingStates();
    }
});



$(document).ready(function () {
    Initialize();
    $.validator.unobtrusive.parse("#frmRenewPlanBilling");
    $("#btn-next").unbind('click');
    $("#btn-next").click(function () {
        if ($('#frmRenewPlanBilling').valid()) {
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
            url: "/Profile/RenewPlanBilling",
            data: $("#frmRenewPlanBilling").serialize(),
            success: function (result) {
                HideLoading();
                window.location.href = "/Profile/OwnerDetail";
            },
            failure: function (result) {
                //$('#dvREnewalInfo').html(result);
                HideLoading();
            }
        });
    });

});


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