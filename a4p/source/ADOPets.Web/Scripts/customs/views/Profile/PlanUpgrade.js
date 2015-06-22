﻿$(document).ready(function () {
    // var selectedItem = parseInt($('#NumberOfPets').text());
    $('#AdditionalPets').val("0");

    var remainingDays = parseInt($('#RemainingDays').val(), 10);

    GetPlanDetails();

    $('#CancelPlan').click(function () {
        var testURL = window.location.href;
        if (testURL.indexOf("Pet") > 1) {
            window.location.reload();
        }
        $('#UpgradeMyPlan').removeClass('tab-pane fade in active').addClass('tab-pane fade');
        $('#MyPlan').removeClass('tab-pane fade').addClass('tab-pane fade in active');
        localStorage.clear();

        $.getJSON(GetApplicationPath() + "Profile/ClearSessionData",
            function (data) {
            console.log(data);
        });

    });

    $("#PlanName").change(function () {
        GetPlanDetails();
    });

    $("#AdditionalPets").change(function () {
        var AdditionalPets = $('#AdditionalPets :selected').val();
        AdditionalPets = AdditionalPets || 0;
        var selectedItem = parseInt($('#NumberOfPets').text());
        selectedItem = selectedItem;
        if (AdditionalPets < selectedItem) {
            $("#AdditionalPets").addClass("input-validation-error");
            $("#AddPetsErrMsg").show();
            GetPrice(selectedItem);
        } else {
            $("#AddPetsErrMsg").hide();
            $("#AdditionalPets").removeClass("input-validation-error");
            GetPrice(AdditionalPets);
        }

    });

    $('#PlansByPromoCode1').click(function () {
        ChangePlanListByPromoCode();
    });


});

function GetPrice() {

    var Planid = $('#PlanName :selected').val();
    $.getJSON(GetApplicationPath() + "Profile/GetPlanPrice", { PlanID: Planid},
    function (data) {
        console.log(data);
        $('#UpgradePlan').text(data.finalPlan);
        $('#UpgradePrice').text($("meta[name='accept-currency']").attr("content") + data.price);
        $('#MaxPetCount').text(data.MaxPetCount);
        alert(data.MaxPetCount);
    });

}



function GetPlanDetails() {
    var renewalDate;
    var culture = GetCurrentCulture();
    var Planid = $('#PlanName :selected').val();
    var remainingDays = parseInt($('#RemainingDays').val(), 10);
    if (remainingDays > 0) {
        var d = new Date($('#lblExpirationDate').text());
        if (culture == "en-US") {
            renewalDate = (d.getMonth() + 1) + "/" + (d.getDate() + 1) + "/" + d.getFullYear();
        } else {
            renewalDate = (d.getDate() + 1) + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();
        }
    } else {
        var d = new Date()
        if (culture == "en-US") {
            renewalDate = (d.getMonth() + 1) + "/" + (d.getDate()) + "/" + d.getFullYear();
        } else {
            renewalDate = d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();
        }
    }

    $.getJSON(GetApplicationPath() + "Profile/GetStartDate", { PlanID: Planid, RenewalDate: renewalDate },
    function (data) {
        var expDate;
        var startdate;

        // get expdate
        var dateString = data.ExpirationDate.substr(6);
        var d = new Date(parseInt(dateString));
        if (culture == "en-US") {
            expDate = (d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear();
        } else {
            expDate = d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();
        }

        //get startDate
        var dateString2 = data.StartDate.substr(6);
        var sd = new Date(parseInt(dateString2));
        if (culture == "en-US") {
            startdate = (sd.getMonth() + 1) + "/" + sd.getDate() + "/" + sd.getFullYear();
        } else {
            startdate = sd.getDate() + "/" + (sd.getMonth() + 1) + "/" + sd.getFullYear();
        }

        $('#lblStartDate').text(startdate);
        $('#StartDate').val(startdate);
        $('#lblExpirationDate').text(expDate);
        $('#EndDate').val(expDate);
        $('#Duration').text(data.Dueration);
        $('#TotalRemainigDays').text(data.RemainingDyas);
        $('#UpgradePrice').text(data.price);
    });

}

function ChangePlanListByPromoCode() {
    var promocode = $('#IdPromo').val();
    var CurrentPromo = $('#Promocode').val();
    var AdditionalPets = $('#AdditionalPets :selected').val();
    var subItems;
    $.getJSON(GetApplicationPath() + "Profile/GetPlansByPromocode", { promocode: promocode, AdditionalPets: AdditionalPets, CurrentPromo: CurrentPromo },
            function (data) {
                if (data.length == 0) {                  
                } else {
                    // $("#IdPromo").removeClass("input-validation-error");
                    //$("#PromocodeErrMsg").show();
                    if (!data.isvalid) {
                        $("#PromocodeErrMsg").show();
                        HideMsg("#PromocodeErrMsg");
                    }

                    $.each(data.Items, function (index, item) {
                        subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    });

                    $("#PlanName").html(subItems);
                    $('#FinalplanName').val(data.BasePlanDetails.FinalPlanName);
                    $("#lblFinalplanName").text(data.BasePlanDetails.FinalPlanName);
                    $("#AdditionalInfo").text(data.BasePlanDetails.AdditionInfo);

                }
            });
}