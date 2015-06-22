var resultPlan = GetTranstaltionPlanPromo();

$(function () {
    $("[data-toggle='tooltip']").tooltip();
    DrawTable('#PlansandPromo');
    filteronSelect('#Promocode', 2, '#PlansandPromo');
    filteronSelect('#Plan', 1, '#PlansandPromo');

    $("#Promocode").change(function () {
        ChangePlanListByPromoCode();
    });

    $("input:checkbox[name='isTrial']").on('click', function () {
        if ($("#isTrialYes").is(":checked") && $("#isTrialNo").is(":checked")) {
            filterColumn("#PlansandPromo", 6, '');
        }
        else if ($("#isTrialYes").is(":checked")) {
            filterColumn("#PlansandPromo", 6, $("#isTrialYes").val());
        }
        else if ($("#isTrialNo").is(":checked")) {
            filterColumn("#PlansandPromo", 6, $("#isTrialNo").val());
        }
        else {
            filterColumn("#PlansandPromo", 6, '');
        }
    });
});

$(".opendelete").click(function (e) {
    e.preventDefault();
    var hrefValue = $(this).attr('data-href');
    $.get(hrefValue, function (data) {
        $('#deleteModal').html(data);
        $('#deleteModal').modal('show');
        $('#plansettings').modal('toggle');
    });
});

function submitDelete() {
    var checkedCount = $("#plansettings input[type=checkbox]:checked").length;
    if (parseInt(checkedCount) <= 0) {
        $('#IsDeletePlanValidation').html(resultPlan.DeletePlanValidation);
    } else {
        $('#IsDeletePlanValidation').html('');
        $("#btnDeletePlanPromo").click();
    }
}

function ChangePlanListByPromoCode() {
    var promocode = $("#Promocode option:selected").text();
    var promocodesVal = $('#Promocode option:selected').val();
    promocodesVal = promocodesVal || 0;

    var subItems = "";
    $.getJSON(GetApplicationPath() + "Users/GetPlansByPromocode", { promocode: promocode, isGetAll: true },
            function (data) {
                subItems += "<option value='0'>" + data.Description + "</option>";
                $.each(data.Items, function (index, item) {
                    subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                });

                $("#Plan").html(subItems);
            });
}