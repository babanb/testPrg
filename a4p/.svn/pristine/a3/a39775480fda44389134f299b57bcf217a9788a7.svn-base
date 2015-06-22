var resultPlan = GetTranstaltionPlanPromo();
var totalCount = $("#tblAvailablePlans > tbody > tr").find('input[type=checkbox]').length;

$(function () {
    var culture = GetCurrentCulture();

    $('#StartDate').datepicker({
        language: culture,
        autoclose: true,
        endDate: $('#EndDate').val()
    });

    $('#EndDate').datepicker({
        language: culture,
        autoclose: true,
        startDate: $('#StartDate').val()
    });

    $('#StartDate').change(function () {
        $('#EndDate').datepicker('setStartDate', $(this).val());
    });

    $('#EndDate').change(function () {
        $('#StartDate').datepicker('setEndDate', $(this).val());
    });

    $('#PlansByPromoCode').click(function () {
        ChangePlanListByPromoCode();
    });

    $("#lnkSelectAllPlan").click(function () {
        var className = $("#lnkSelectAllPlan").find("i").attr("class");
        if (className.toLowerCase().trim().indexOf("fa-square-o") > 0) {
            $("#lnkSelectAllPlan").find("i").removeClass("fa fa-square-o").addClass("fa fa-check-square-o");
            $("#tblAvailablePlans  > tbody > tr").find("input[type=checkbox]").prop("checked", "checked");
        }
        else {
            $("#lnkSelectAllPlan").find("i").removeClass("fa fa-check-square-o").addClass("fa fa-square-o");
            $("#tblAvailablePlans  > tbody > tr").find("input[type=checkbox]").prop("checked", "");
        }
    });
});

$("#tblAvailablePlans > tbody > tr").find("input[type=checkbox]").click(function (e) {
    var count = $("#tblAvailablePlans > tbody > tr").find('input[type=checkbox]:checked').length;
    count = count == undefined ? 0 : count;
    if (count == totalCount) {
        $("#lnkSelectAllPlan").find("i").removeClass("fa fa-square-o").addClass("fa fa-check-square-o");
    } else
        if (count <= totalCount) {
            // $("#chkAllShare").prop("checked", false);
            $("#lnkSelectAllPlan").find("i").removeClass("fa fa-check-square-o").addClass("fa fa-square-o");
        }
});

function submitWith() {
    var checkedCount = $("table input[type=checkbox]:checked").length;
    if (parseInt(checkedCount) <= 0) {
        $('#validationMessage').html(resultPlan.AddPromoPlanValidation);
    } else {
        $('#validationMessage').html(''); $("#btnSubmitPromocode").click();

    }
    var promoVal = $("#Promocode").val();
    if (promoVal == null || promoVal == "" || promoVal == undefined) {
        $("#btnSubmitPromocode").click();
    }
}

function ChangePlanListByPromoCode() {
    var promocode = $('#IdPromo').val();
    var CurrentPromo = $('#Promocode').val();
    var AdditionalPets = $('#AdditionalPetRenewal :selected').val();
    var subItems;
    $.getJSON(GetApplicationPath() + "PlansAndPromo/IsPromocodeExists", { promocode: promocode },
            function (data) {
                if (data.length == 0) {
                    $("#Promocode").addClass("input-validation-error");
                    $("#Promocode").show();
                } else {
                    if (!data.isvalid) {
                        $("#Promocode").addClass("input-validation-error");
                        $("#Promocode").show();
                    } else {
                        $("#Promocode").removeClass("input-validation-error");
                        $("#Promocode").hide();
                    }
                }
            });
}

