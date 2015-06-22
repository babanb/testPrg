$(document).ready(function () {

    UpdatePhoneNumberValidation('FarmerHomePhone');
    if ($("meta[name='accept-domain']").attr("content") != "French") {
        UpdatePhoneNumberValidation('FarmerOfficePhone');
    }
    UpdatePhoneNumberValidation('FarmerCellPhone');
    UpdatePhoneNumberValidation('FarmerFax');
    if ($("meta[name='accept-domain']").attr("content") != "French") {
        UpdateFarmerStates();
    }

    Initialize();
    $.validator.unobtrusive.parse("#BreederForm");

    //$('#BreederForm').ajaxForm({
    //    beforeSubmit: BreederBeforeSubtmit,
    //    success: BreederSubmitSuccesful
    //});

    $("#cancelBreederButton").click(function () {
        $("#breederdetails").load(this.href);
        $("#breederEdit").show();
        $("#cardEdit").hide();
        return false;
    });

    $("#FarmerCountry").change(function () {
        if ($("meta[name='accept-domain']").attr("content") != "French") {
            UpdateFarmerStates();
        }
    });
});

function BreederSubmitSuccesful(result) {
    $('#breederdetails').html(result);
    $("#breederEdit").show();
    $("#cardEdit").hide();
    HideSuccessMsg();
}

function BreederBeforeSubtmit() {
    var frmValid = $("#BreederForm").valid();
    return frmValid;
}

function UpdateFarmerStates() {

    var farmerCountry = $('#FarmerCountry').val();
    var farmerState = $('#FarmerState').val();

    var subItems = "<option value>" + $("#FarmerState").children()[0].text + "</option>";

    if (farmerCountry == "France") {
        $("#FarmerState").html(subItems);
        $("#FarmerState").prop("disabled", true);
    }
    else {
        $("#FarmerState").prop("disabled", false);

        if ($("#FarmerCountry option:selected").index() == 0) {
            $("#FarmerState").html(subItems);
        } else {
            $.getJSON(GetApplicationPath() + "Pet/GetStates", { country: farmerCountry },
                function (data) {
                    $.each(data, function (index, item) {
                        if (item.Value == farmerState) {
                            subItems += "<option value='" + item.Value + "' selected>" + item.Text + "</option>";
                        } else {
                            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                        }
                    });
                    $("#FarmerState").html(subItems);
                });
        }
    }
}