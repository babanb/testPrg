﻿$(document).ready(function () {
    $("#NumberOfPets option").filter(function (index) { return $(this).val() === '0'; }).html($('#changeselectText').val()).val(0);

    $("#Promocode").val($("#CurrentPromocode").val());
    $("#PlanName").val($("#PlanID").val());
    $("#NumberOfPets").change(function () {
        getRenwalPetsPrice();
    });

    $("#Promocode").change(function () {
        ChangePlanListByPromoCode();
    });

    $("#PlanName").change(function () {
        ChangePlan();
    });

    $('#RemovePets').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#DeletepetsModel').html(data);
            Initialize();
            $('#DeletepetsModel').modal('show');

        });

    });

    $('#CancelPlan').click(function () {
        $('#RenewMyPlan').removeClass('tab-pane fade in active').addClass('tab-pane fade');
        $('#UserPlanInfo').removeClass('tab-pane fade').addClass('tab-pane fade in active');

    });

});


function ChangePlan() {
    var petCount = $('#NumberOfPets').val();
    var Planid = $('#PlanName :selected').val();
    if (petCount == '') {
        petCount = 0;
    }
    $.getJSON(GetApplicationPath() + "Profile/GetPlanPrice", { PlanID: Planid, AdditionalPets: petCount },
        function (data) {
            $("#RenuewalPlan").html(data.finalPlan);
            $("#RenewalPrice").html($("meta[name='accept-currency']").attr("content") + data.price);
            $("#AdditionalInfo").text(data.AdditionalInfo);
            $("#BasePlanDescription").text(data.Description);
            $("#MaxPetCount").text(data.MaxPetCount);
        });

}

function ChangePlanListByPromoCode() {
    var promocode = $('#Promocode').val();
    var subItems;
    if (promocode != "") {
        $.getJSON(GetApplicationPath() + "Users/GetPlansByPromocode", { promocode: promocode},
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
    } else {
        $.getJSON(GetApplicationPath() + "Users/GetAllPlansByPromocode1", { promocode: promocode },
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
}

function getRenwalPetsPrice() {
    var AdditionalPets = $('#NumberOfPets :selected').val();
    AdditionalPets = AdditionalPets || 0;
    var selectedItem = parseInt($('#CurrentPlan_NumberofPets').val());
    selectedItem = selectedItem || 0;
    if (AdditionalPets < selectedItem) {
        $("#NumberOfPets").addClass("input-validation-error");
        $("#AddPetsRenewalErrMsg").css("display", "block");
        ChangePlan();
    } else {
        $("#AddPetsRenewalErrMsg").css("display", "none");
        $("#NumberOfPets").removeClass("input-validation-error");
        ChangePlan();
    }
}

