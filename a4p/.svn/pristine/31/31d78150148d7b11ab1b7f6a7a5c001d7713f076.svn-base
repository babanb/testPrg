$(document).ready(function () {
    //   localStorage.clear();

    var promo = $("#Promocode").val();
    if (promo != undefined) {
        $("#txtPromoCode").val(promo);
    }

    $('#CancelRenewalPlan').click(function () {
        $('#RenewaltMyPlan').removeClass('tab-pane fade in active').addClass('tab-pane fade');
        $('#MyPlan').removeClass('tab-pane fade').addClass('tab-pane fade in active');
        localStorage.clear();

        $.getJSON(GetApplicationPath() + "Profile/ClearSessionData",
            function (data) {
            });
    });

    $('#PlansByPromoCode').click(function () {
        ChangePlanListByPromoCode();
    });

    $("#btn-next").click(function () {
        //lnkChoosePlan pricingPlans lnkChoosePlan
        var planId = $("#hdnPlanId").val();
        var promocode = $('#txtPromoCode').val();
        if (planId > 0) {
            ShowLoading();
            $.ajax({
                type: 'GET',
                url: '/Profile/RenewPlanBilling',
                data: { planId: planId, promocode: promocode },
                success: function (data) {
                    $("#dvREnewalInfo").html(data);
                    HideLoading();
                },
                failure: function () { HideLoading(); }
            });
        }
        else {
            $("#EnableTrue").show();
            window.parent.$("#mainBox").animate({ scrollTop: 0 }, 0);
        }
    });

});

function ChangePlanListByPromoCode() {
    var promocode = $('#txtPromoCode').val();
    if (promocode != "") {
        ShowLoading();
        $.ajax({
            type: 'GET',
            url: '/Profile/GetRenewPlansByPromocode',
            data: { promocode: promocode },
            success: function (data) {
                HideLoading();
                $("#txtPromoCode").removeClass("input-validation-error");
                $("#PromocodeSuccessmsg").css('opacity', '');
                $("#PromocodeSuccessmsg").css('display', 'block');
                HideMsg("#PromocodeSuccessmsg");
                $("#PromocodeErrMsg").css('display', 'none');
                $("#dvListNewPlan").html(data);
                $("#hdnPlanId").val();
            },
            failure: function (data) {
                HideLoading();
                // $("#Promocode").val('');
                $("#txtPromoCode").addClass("input-validation-error");
                $("#PromocodeErrMsg").css('opacity', '');
                $("#PromocodeErrMsg").css('display', 'block');
                HideMsg("#PromocodeErrMsg");
            }
        });
    } else {
        $("#Promocode").val('');
        $("#txtPromoCode").addClass("input-validation-error");
        $("#PromocodeErrMsg").css('opacity', '');
        $("#PromocodeErrMsg").css('display', 'block');
        HideMsg("#PromocodeErrMsg");
    }
    return false;
}

function getRenwalPetsPrice() {
    var AdditionalPets = $('#AdditionalPetRenewal :selected').val();
    AdditionalPets = AdditionalPets || 0;
    var selectedItem = parseInt($('#CurrentPlan_NumberofPets').val());
    selectedItem = selectedItem || 0;
    if (AdditionalPets < selectedItem) {
        $("#AdditionalPetRenewal").addClass("input-validation-error");
        $("#AddPetsRenewalErrMsg").show();
        GetPrice(selectedItem);
    } else {
        $("#AddPetsRenewalErrMsg").hide();
        $("#AdditionalPetRenewal").removeClass("input-validation-error");
        GetPrice(AdditionalPets);
    }
}
function GetPrice() {
    // var Planid = $('#PlanName :selected').val();
    var planId = $("#hdnPlanId").val();
    $.getJSON(GetApplicationPath() + "Profile/GetPlanPrice", { PlanID: Planid },
    function (data) {
        $('#RenuewalPlan').text(data.finalPlan);
        $('#RenewalPrice').text($("meta[name='accept-currency']").attr("content") + data.price);
        $("#AdditionalInfo").text(data.AdditionalInfo);
        $("#MaxPetCount").text(data.MaxPetCount);
    });

}
function GetPlanDetails() {
    var renewalDate;
    var culture = GetCurrentCulture();
    var Planid = $('#hdnPlanId').val();
    //var remainingDays = parseInt($('#RemainingDays').val(), 10);
    //if (remainingDays > 0) {
    //    var d = new Date($('#lblExpirationDate').text());
    //    if (culture == "en-US") {
    //        renewalDate = (d.getMonth() + 1) + "/" + (d.getDate() + 1) + "/" + d.getFullYear();
    //    } else {
    //        renewalDate = (d.getDate() + 1) + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();
    //    }
    //} else {
    //    var d = new Date()
    //    if (culture == "en-US") {
    //        renewalDate = (d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear();
    //    } else {
    //        renewalDate = d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();
    //    }
    //}
    if (Planid != undefined) {
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
            $('#lblFinalplanName').text(data.price);
        });
    }
}

function HideMsg(id) {
    window.setTimeout(function () {
        $(id).fadeTo(1000, 0).slideUp(1000, function () {
            $(this).hide();
        });
    }, 2000);
}

function CloseDialogRemovePets() {
    $('.modal').modal('hide');
    getRenwalPetsPrice();
}