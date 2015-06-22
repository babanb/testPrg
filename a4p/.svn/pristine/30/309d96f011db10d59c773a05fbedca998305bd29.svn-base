$(document).ready(function () {
    localStorage.clear();
    $("#next-bt").attr('disabled', 'disabled');
    $("#submit-bt").attr('disabled', 'disabled');
    $("#submitVD-bt").attr('disabled', 'disabled');
    $(document).off("click", "#selectPet").on("click", "#selectPet", function (e) {
        e.preventDefault();
        var url = $('#SelectPetURL').val();
        $.get(url, function (data) {
            if (data.success == false) {
                var petRequiredMessage = $("#hdnPetRequired").val();
                ShowAjaxWarningMessage(petRequiredMessage);
            }
            else {
                $('#selectModalPet').html(data);
                Initialize();
                $('#selectModalPet').modal('show');
            }
        });
    });

    var url1 = $('#InvestigationListURL').val();
    $.get(url1, function (data) {
        $('#divInvestigation').html(data);
        Initialize();
    });

    $(document).off("click", "#selectOwner").on("click", "#selectOwner", function (e) {
        e.preventDefault();
        var url = $('#SelectOwnerURL').val();
        $.get(url, function (data) {
            $('#selectModalOwner').html(data);
            Initialize();
            $('#selectModalOwner').modal('show');
        });
    });

    $(document).off("click", "#AddTest").on("click", "#AddTest", function (e) {

        e.preventDefault();
        var url = $(this).attr('data-url');
        $.get(url, function (data) {
            $('#addModalTest').html(data);
            Initialize();
            $('#addModalTest').modal('show');
        });
    });
    $("#callstep1").click(function () {
        var url = $('#SetupURL').val();
        $('#Step1').addClass('active');
        $('#Step2').removeClass("active");
        $('#Step3').removeClass("active");
        $('#divStep').load(url);

        var url1 = $('#InvestigationListURL').val();
        $.get(url1, function (data) {
            $('#divInvestigation').html(data);
            Initialize();
        });
    });
});

function checkCheckbox() {
    if ($('#authorizeveterinarians').is(":checked") && $('#certify').is(":checked")) {
        $("#next-bt").removeAttr('disabled');
        $("#submit-bt").removeAttr('disabled');
        $("#submitVD-bt").removeAttr('disabled');
    } else {
        $("#next-bt").attr('disabled', 'disabled');
        $("#submit-bt").attr('disabled', 'disabled');
        $("#submitVD-bt").attr('disabled', 'disabled');
    }
}
$(document).off("click", "#authorizeveterinarians").on("click", "#authorizeveterinarians", function (e) {
    checkCheckbox();
});
$(document).off("click", "#certify").on("click", "#certify", function (e) {
    checkCheckbox();
});