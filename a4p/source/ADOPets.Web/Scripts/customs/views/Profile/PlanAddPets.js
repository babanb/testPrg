$(document).ready(function () {
    var selectedItem = parseInt($('#NumberOfPets').val());
    if(selectedItem!=0)
    $('#AdditionalPets').val(selectedItem);

    $("#AdditionalPets").change(function () {

        var container = $('#PlanAddPetsForm');

        var petCount = parseInt($(this).val());
        var subscriptionId = container.find("#PlanId").val();
        var additionalPet = parseInt(container.find('#NumberOfPets').val());
        if (petCount >= additionalPet) {
            $.getJSON(GetApplicationPath() + "Profile/GetPetsPrice", { subscriptionId: subscriptionId, petCount: petCount },
                function (data) {
                    $('#SelectedPlan').text(data.finalPlan);
                    $('#PriceToPay').text($("meta[name='accept-currency']").attr("content") + data.PriceToPay);
                    container.find('#AddPetsErrMsg').hide();
                });
        } else if (petCount > 0) {
            container.find('#AddPetsErrMsg').show();
        }
    });
    
    $('#CancelPlanAddPets').click(function () {
        $('#MyPlanAddPets').removeClass('tab-pane fade in active').addClass('tab-pane fade');
        $('#MyPlan').removeClass('tab-pane fade').addClass('tab-pane fade in active');
        localStorage.clear();
       

        $.getJSON(GetApplicationPath() + "Profile/ClearSessionData",
            function (data) {
                console.log(data);
            });
    });
});
