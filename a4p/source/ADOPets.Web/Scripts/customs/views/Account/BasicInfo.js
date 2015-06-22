
﻿$(document).ready(function () {
    window.parent.$("body").animate({ scrollTop: 0 }, 0);
    UpdatePhoneNumberValidation('PrimaryPhone');
    UpdatePhoneNumberValidation('SecondaryPhone');
    UpdateStates();

    $("#BasePlanName").append('<span id="spnMRADetails" style="cursor: pointer;font-size:15px;color:#85bb41;padding-left:10px;" class="fa fa-info-circle"></span>');
    $('#spnMRADetails').bind('click', showMRADetail);

    $("#signupStep1,#signupStep2,#signupStep3").removeClass("active");
    $("#signupStep1").addClass("active");
    $("#signup-form .panel-heading a").click(function () {
        $("#signup-form .panel-heading a").removeClass("first-time");
    });

    $("#Country").change(function () {
        UpdateStates();
    });

    $('#Password').blur(function () {
        var pass = $(this).val();
        if (pass.match(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,})$/)) {
            $('div#passStrength').html('<span class="glyphicon glyphicon-ok form-control-feedback" style="color:#3c763d; top:0 !important; right:-15px !important;"></span>');
        }
        else {
            $('div#passStrength').html('<span class="glyphicon glyphicon-remove form-control-feedback" style="color:#a94442; top:10px !important; right:-15px !important;"></span>');
        }
    });

    $('#confPassword').blur(function () {
        var pass = $(this).val();
        if (pass.match(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,})$/)) {
            $('div#confpassStrength').html('<span class="glyphicon glyphicon-ok form-control-feedback" style="color:#3c763d; top:0 !important; right:-15px !important;"></span>');
        }
        else {
            $('div#confpassStrength').html('<span class="glyphicon glyphicon-remove form-control-feedback" style="color:#a94442; top:10px !important; right:-15px !important;"></span>');
        }
    });
    $("#AdditionalPetDDL").change(function () {
        ChangePlan();
    });

    $('#PlansByPromoCode').click(function () {
        ChangePlanListByPromoCode();
    });

    var CurrentTimezone = $("#TimeZone").val();

    $("#TimeZone").html($('#TimeZone option').sort(function (x, y) {
        return $(x).text() < $(y).text() ? -1 : 1;
    }));

    $("#TimeZone").val(CurrentTimezone);


    $("#spnMRADetails").click(function () {
        var hdnData = $("#hdnMRADetail").val();
    });
});

function ChangePlan() {
    var petCount = $('#AdditionalPetDDL').val();

    if (petCount == '') {
        $("#final_plan").hide();

        $("#PlanId").val($("#BasePlanId").val());
        $("#PlanName").val($("#BasePlanName").val());
        $("#Price").val($("#BasePlanPrice").val());
        $("#AdditionalPetCount").val(0);

    } else {
        var planId = $("#PlanId").val(); var promocode = $('#IdPromo').val();
        $.getJSON(GetApplicationPath() + "Account/GetPlanDetailsByPetCount", { petToAdd: petCount, planId: planId, promocode: promocode },
            function (data) {
                if (data.Price > 0) {
                    $("#final_plan").html(data.PlanName);
                    $("#final_plan").show();
                }

                $("#PlanId").val(data.PlanId);
                $("#PlanName").val(data.PlanName);
                $("#Price").val(data.Price);
                $("#AdditionalPetCount").val(petCount);
            });
    }
}

function ChangePlanListByPromoCode() {
    var promocode = $('#IdPromo').val();
    var subItems;
    $.getJSON(GetApplicationPath() + "Account/GetAdditionalPetsByPromocode", { promocode: promocode },
            function (data) {
                if (data.length == 0) {
                    $("#Promocode").val('');
                    $("#IdPromo").addClass("input-validation-error");
                    $("#PromocodeErrMsg").css('opacity', '');
                    $("#PromocodeErrMsg").css('display', 'block');
                    HideMsg("#PromocodeErrMsg");
                } else {
                    $("#IdPromo").removeClass("input-validation-error");
                    $("#PromocodeSuccessmsg").css('opacity', '');
                    $("#PromocodeSuccessmsg").css('display', 'block');
                    HideMsg("#PromocodeSuccessmsg");
                    $.each(data.Items, function (index, item) {
                        if (item.Text == 0) {
                            subItems = "<option value>" + $("#AdditionalPetDDL").children()[0].text + "</option>";
                        }
                        else {
                            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                        }
                    });

                    $("#Promocode").val(promocode);
                    $("#AdditionalPets").html(subItems);
                    $("#final_plan").hide();
                    $("#PromocodeErrMsg").css('display', 'none');
                    $("#PlanId").val(data.BasePlanDetails.PlanId);
                    $("#PlanName").val(data.BasePlanDetails.PlanName);
                    $("#Price").val(data.BasePlanDetails.Price);

                    $("#BasePlanId").val(data.BasePlanDetails.PlanId);
                    $("#BasePlanName").text(data.BasePlanDetails.PlanName);
                    $("#BasePlanPrice").val(data.BasePlanDetails.Price);
                    $("#BasePlanDescription").text(data.BasePlanDetails.PlanDesciption);

                    $("#AdditionalPetDDL").val($("#AdditionalPetDDL option:first").val());
                    $("#BasePlanName").append('<span id="spnMRADetails" style="cursor: pointer;font-size:15px;color:#85bb41;padding-left:10px;" class="fa fa-info-circle"></span>');
                    $('#spnMRADetails').bind('click', showMRADetail);
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

function HideMsg(id) {
    window.setTimeout(function () {
        $(id).fadeTo(1000, 0).slideUp(1000, function () {
            $(this).css('display', 'none');
        });
    }, 2000);
}


function showMRADetail() {
    $('#dvMRADetails').modal('toggle');
}