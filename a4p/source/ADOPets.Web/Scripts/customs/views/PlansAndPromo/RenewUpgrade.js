$(document).ready(function () {
    $('#PromoCodeId').change(function () {
        ChangePlanListByPromoCode();
    });
});

function ChangePlanListByPromoCode() {
    var promocode = $('#PromoCodeId').val();

    if (promocode != "") {
        var subItems;
        $.getJSON(GetApplicationPath() + "PlansAndPromo/GetPlansByPromocode", { promocode: promocode, isRenewPlan: false },
                function (data) {
                    if (data.length == 0) {
                    } else {
                        if (!data.isvalid) {
                            $("#PromocodeErrMsg").show();
                            HideMsg("#PromocodeErrMsg");
                        }

                        $.each(data.Items, function (index, item) {
                            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                        });

                        $("#PlanId").html(subItems);
                    }
                });
    } else {
        $('#PlanatRenewalId option:first-child').attr("selected", "selected");
        $('#PlanId').html($('#PlanatRenewalId').html());
    }
}