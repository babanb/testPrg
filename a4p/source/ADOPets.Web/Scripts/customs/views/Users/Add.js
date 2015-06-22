﻿var phonenumberRegex = "";

$(document).ready(function () {

    $("#final_plan").hide();
    ChangePlan();
    getDomain();
    if ($('#selectuserType').val() == "OwnerAdmin") {
        $("#promoPlan").show();
    } else {
        $("#promoPlan").hide();
    }

    if ($('#selectuserType').val() == "VeterinarianAdo" || $('#selectuserType').val() == "VeterinarianLight" || $('#selectuserType').val() == "VeterinarianExpert") {
        $("#extraFields").show();
    } else {
        $("#extraFields").hide();
    }

    $("#selectuserType").change(function () {
        if ($('#selectuserType').val() == "OwnerAdmin") {
            $("#promoPlan").show();
        } else {
            $("#promoPlan").hide();
        }

        if ($('#selectuserType').val() == "VeterinarianAdo" || $('#selectuserType').val() == "VeterinarianLight" || $('#selectuserType').val() == "VeterinarianExpert") {
            $("#extraFields").show();
        } else {
            $("#extraFields").hide();
        }
    });

    //$("#PrimaryPhone").blur(function () {
   
    //    var primaryPhoneErrorMessage = $("#PrimaryPhone").attr("data-val-regex");
    //    var PrimaryPhone = $(this).val();
    //    if (PrimaryPhone != "") {
    //        if (PrimaryPhone.match(phonenumberRegex)) {
    //            $("span[data-valmsg-for='PrimaryPhone']").addClass("field-validation-valid").removeClass("field-validation-error").attr("display", "none");
    //            $("#PrimaryPhone").removeClass("input-validation-error");
    //        }
    //        else {
    //            $("span[data-valmsg-for='PrimaryPhone']").removeClass("field-validation-valid").addClass("field-validation-error").attr("display", "block").text(primaryPhoneErrorMessage);;
    //            $("#PrimaryPhone").addClass("input-validation-error");
    //        }
    //    }
    //});

    //$("#SecondaryPhone").blur(function () {
    //    var SecondaryPhoneErrorMessage = $("#SecondaryPhone").attr("data-val-regex");
    //    var SecondaryPhone = $(this).val();
    //    if (SecondaryPhone != "") {
    //        if (SecondaryPhone.match(phonenumberRegex)) {
    //            $("span[data-valmsg-for='SecondaryPhone']").addClass("field-validation-valid").removeClass("field-validation-error").attr("display", "none");
    //            $("#SecondaryPhone").removeClass("input-validation-error");
    //        }
    //        else {
    //            $("span[data-valmsg-for='SecondaryPhone']").removeClass("field-validation-valid").addClass("field-validation-error").attr("display", "block").text(SecondaryPhoneErrorMessage);
    //            $("#SecondaryPhone").addClass("input-validation-error");
    //        }
    //    }
    //});

    $('#Password').blur(function () {
        var pass = $(this).val();
        if (pass.match(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,})$/)) {
            $('div#passStrength').html('<span class="glyphicon glyphicon-ok form-control-feedback" style="color:#3c763d; top:0 !important; right:-15px !important;"></span>');
        }
        else {
            $('div#passStrength').html('<span class="glyphicon glyphicon-remove form-control-feedback" style="color:#a94442; top:10px !important; right:-15px !important;"></span>');
        }
    });

    $("#Country").change(function () {
        UpdateStates();
    });
    $("#AdditionalPets").change(function () {
        ChangePlan();
    });
    $("#BasePlanName").change(function () {
        ChangePlan();
    });
    $("#Promocode").change(function () {
        ChangePlanListByPromoCode();
    });

    $('#imgNewUserPhoto').on('change', function () {
        readURL(this);
    });

    var CurrentTimezone=$("#TimeZone").val();

    $("#TimeZone").html($('#TimeZone option').sort(function (x, y) {
        return $(x).text() < $(y).text() ? -1 : 1;
    }));

    $("#TimeZone").val(CurrentTimezone);
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgNewUserProfilePic').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function ChangePlan() {
    var petCount = $('#AdditionalPets').val();
    var Planid = $('#BasePlanName :selected').val();
    var Planname = $('#BasePlanName :selected').text();

    if (petCount == ' ') {

        $("#final_plan").hide();

        $("#PlanId").val($("#BasePlanId").val());
        $("#PlanName").val($("#BasePlanName").val());
        $("#Price").val($("#BasePlanPrice").val());
        $("#AdditionalPetCount").val(0);
    }
    else {
        if (Planid == 12) {
            $("#final_plan").hide();
            $("#AdditionalPets").prop('disabled', true);
        } else {
            $("#AdditionalPets").prop('disabled', false);


            petCount = petCount == "" ? 0 : petCount;
            $.getJSON(GetApplicationPath() + "Profile/GetPlanPrice", { PlanID: Planid},
                function (data) {
                    $("#final_plan").html(data.finalPlan);
                    $("#final_plan").show();
                    $("#Price").val(data.price);
                    $("#description").text(data.Description);
                    $("#MaxPetCount").text(data.MaxPetCount);

                });
        }
    }
}

function ChangePlanListByPromoCode() {
    var promocode = $('#Promocode').val();
    var subItems;
    $.getJSON(GetApplicationPath() + "Users/GetAllPlansByPromocode1",
        {
            promocode: promocode, isTrial: true
        },
    function (data) {
        $.each(data.Items, function (index, item) {
            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
        });
        $("#BasePlanId").val(data.Items[0].Value);
        $("#BasePlanName").html(subItems);
        $("#AdditionalInfo").text(data.AdditionalInfo);
        $("#description").text(data.Description);
        $("#MaxPetCount").text(data.MaxPetCount);
        ChangePlan();
        if (promocode == "FREE_PROMO") {
            $("#AdditionalPets").prop('disabled', true);
        } else {
            $("#AdditionalPets").prop('disabled', false);
        }
    });
}

function UpdateStates() {

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

function getDomain() {
    var domain = $("meta[name='accept-domain']").attr("content");
    if (domain == 'French') {
        phonenumberRegex = "^\\(?([0-9]{2})\\)?[-. ]?([0-9]{2})[-. ]?([0-9]{2})[-. ]?([0-9]{2})[-. ]?([0-9]{2})$";
    } else if (domain == 'US') {
        phonenumberRegex = "^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})";
    }
}