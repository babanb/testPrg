﻿$(document).ready(function () {
    $("#AdditionalPets option").filter(function (index) { return $(this).val() === '0'; }).html($('#changeselectText').val()).val(0);
    $("#Promocode").val($("#CurrentPromocode").val());
    $("#AdditionalPets").change(function () {
        getRenwalPetsPrice();
    });


    $("#PlanName").change(function () {
        ChangePlan();
    });

    $("#Promocode").change(function () {
        ChangePlanListByPromoCode();
    });

    $('#CancelOwnerUpgradeplan').click(function () {
        $('#UpgradeMyPlan').removeClass('tab-pane fade in active').addClass('tab-pane fade');
        $('#UserPlanInfo').removeClass('tab-pane fade').addClass('tab-pane fade in active');

    });

});


function ChangePlan() {
    var petCount = $('#AdditionalPets').val();

    var Planid = $('#PlanName :selected').val();
    if (petCount == '') {
        petCount = 0;
    }

    // petCount = 0;
    $.getJSON(GetApplicationPath() + "Profile/GetPlanPrice", { PlanID: Planid },
        function (data) {
            $("#UpgradePlan").html(data.finalPlan);            
            $("#UpgradePrice").html($("meta[name='accept-currency']").attr("content") + data.price);
            $("#AdditionalInfo").text(data.AdditionalInfo);
            $("#BasePlanDescription").text(data.Description);
            $('#MaxPetCount').text(data.MaxPetCount);
        });

}

function ChangePlanListByPromoCode() {
    var promocode = $('#Promocode').val();
    var subItems;
    $.getJSON(GetApplicationPath() + "Users/GetPlansByPromocode", { promocode: promocode },
            function (data) {
                $.each(data.Items, function (index, item) {
                    subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                });
                //  $("#BasePlanId").val(data.Items[0].Value);
                $("#PlanName").html(subItems);
                $("#AdditionalInfo").text(data.AdditionalInfo);
                $("#BasePlanDescription").text(data.Description);
                $("#MaxPetCount").text(data.MaxPetCount);
                ChangePlan();
            });
}


function getRenwalPetsPrice() {
    var AdditionalPets = $('#AdditionalPets :selected').val();
    AdditionalPets = AdditionalPets || 0;
    var selectedItem = parseInt($('#CurrentPlan_NumberofPets').val());
    selectedItem = selectedItem || 0;
    if (AdditionalPets < selectedItem) {
        $("#AdditionalPets").addClass("input-validation-error");
        $("#AddPetsErrMsg").css("display", "block");
        ChangePlan();
    } else {
        $("#AddPetsErrMsg").css("display", "none");
        $("#AdditionalPets").removeClass("input-validation-error");
        ChangePlan();
    }
}
 