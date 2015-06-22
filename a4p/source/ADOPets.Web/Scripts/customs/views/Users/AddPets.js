$(document).ready(function () {
    $("#AdditionalPets option").filter(function (index) { return $(this).val() === '0'; }).html($('#changeselectText').val()).val(0);
    var selectedItem = parseInt($('#NumberOfPets').val());
    if (selectedItem != 0)
        $('#AdditionalPets').val(selectedItem);

    $("#AdditionalPets").change(function () {
        var container = $('#AddPetsForm');

        var petCount = parseInt($(this).val());
        petCount = petCount || 0;
        var subscriptionId = container.find("#PlanId").val();
        var additionalPet = parseInt(container.find('#NumberOfPets').val());
        var NumberofPets = parseInt(container.find('#CurrentPlan_NumberofPets').val());
        if (petCount >= additionalPet) {
            $('input[type="submit"]').removeAttr('disabled');
            $.getJSON(GetApplicationPath() + "Profile/GetPetsPrice", { subscriptionId: subscriptionId, petCount: petCount, NumberofPets: NumberofPets },
                function (data) {
                    $('#SelectedPlan').text(data.finalPlan);
                    $('#PriceToPay').text($("meta[name='accept-currency']").attr("content") + data.PriceToPay);
                    container.find('#AddPetsErrMsg').hide();
                });
        } else {
            container.find('#AddPetsErrMsg').show();
            $('input[type="submit"]').attr("disabled", true);
            $('#PriceToPay').text($("meta[name='accept-currency']").attr("content")+'0');
        }
    });

    $('#CancelOwnerAddPets').click(function () {
        $('#MyPlanAddPets').removeClass('tab-pane fade in active').addClass('tab-pane fade');
        $('#UserPlanInfo').removeClass('tab-pane fade').addClass('tab-pane fade in active');

    });
});

$(document).ready(function () {
    $('#CancelOwnerAddPets').click(function () {
        location.reload();
    });
});