$(document).ready(function () {

    UpdatePhoneNumberValidation('PrimaryPhone');
    UpdatePhoneNumberValidation('SecondaryPhone');
    UpdateStates();
    $("#final_plan").hide();

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

    $("#Promocode").change(function () {
        ChangePlanListByPromoCode();
    });

    $('#imgNewUserPhoto').on('change', function () {
        readURL(this);
    });

    var CurrentTimezone = $("#TimeZone").val();

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
    if (petCount == '') {
        $("#final_plan").hide();

        $("#PlanId").val($("#BasePlanId").val());
        $("#PlanName").val($("#BasePlanName").val());
        $("#Price").val($("#BasePlanPrice").val());
        $("#AdditionalPetCount").val(0);

    } else {
        $.getJSON(GetApplicationPath() + "Profile/GetPlanPrice", { PlanID: Planid, AdditionalPets: petCount },
            function (data) {
                $("#final_plan").html(data.finalPlan);
                $("#final_plan").show();
                $("#Price").val(data.price);
            });
    }
}

function ChangePlanListByPromoCode() {
    var promocode = $('#Promocode').val();
    var subItems;
    $.getJSON(GetApplicationPath() + "Users/GetPlansByPromocode", { promocode: promocode },
            function (data) {             
                    $.each(data.Items, function (index, item) {
                            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    });
                    $("#BasePlanName").html(subItems);
                    $("#AdditionalInfo").text(data.AdditionalInfo);
                    $("#description").text(data.Description);
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