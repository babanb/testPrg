$("#next-bt").attr('disabled', 'disabled');
$("#submit-bt").attr('disabled', 'disabled');
$("#submitVD-bt").attr('disabled', 'disabled');

function checkCheckbox() {
    if ($('#ConfirmMedicalRecords').is(":checked") && $('#TermsAndConditions').is(":checked")) {
        $("#next-bt").removeAttr('disabled');
        $("#submit-bt").removeAttr('disabled');
        $("#submitVD-bt").removeAttr('disabled');
    } else {
        $("#next-bt").attr('disabled', 'disabled');
        $("#submit-bt").attr('disabled', 'disabled');
        $("#submitVD-bt").attr('disabled', 'disabled');
    }
}
$('#ConfirmMedicalRecords').click(function () {
    checkCheckbox();
});
$('#TermsAndConditions').click(function () {
    checkCheckbox();
});


function toggle_visibility1() {
    document.getElementById("Setup").style.display = 'block';
    document.getElementById("Billing").style.display = 'none';
    document.getElementById("Confirmation").style.display = 'none';

    document.getElementById("step1").className = 'consultSteptab radius3 active';
    document.getElementById("step2").className = 'consultSteptab radius3';
    document.getElementById("step3").className = 'consultSteptab radius3';
}

function toggle_visibility2() {
    document.getElementById("Setup").style.display = 'none';
    document.getElementById("Billing").style.display = 'block';
    document.getElementById("Confirmation").style.display = 'none';
    document.getElementById("step1").className = 'consultSteptab radius3';
    document.getElementById("step2").className = 'consultSteptab radius3 active';
    document.getElementById("step3").className = 'consultSteptab radius3';
}

function toggle_visibility3() {
    document.getElementById("Setup").style.display = 'none';
    document.getElementById("Billing").style.display = 'none';
    document.getElementById("Confirmation").style.display = 'block';
    document.getElementById("step1").className = 'consultSteptab radius3';
    document.getElementById("step2").className = 'consultSteptab radius3';
    document.getElementById("step3").className = 'consultSteptab radius3 active';
}

$(document).off("click", "#selectOwner").on("click", "#selectOwner", function (e) {
    e.preventDefault();
    var url = $('#SelectOwnerURL').val();
    $.get(url, function (data) {
        $('#selectModalOwner').html(data);
        Initialize();
        $('#selectModalOwner').modal('show');
    });
});

jQuery(function () {
    return $("body").on("click", "#selectPet", function (e) {
        e.preventDefault();
        var url = "/Econsultation/SelectPet";
        var ownerVal = $("#selectOwner").html();

        if (ownerVal == undefined || ownerVal.indexOf("OwnerId") <= 0) {
            $.get(url, function (data) {
                $('#selectModalPet').html(data);
                Initialize();
                $('#selectModalPet').modal('show');
            });
        }
    });
});

jQuery(function () {
    return $("body").on("click", "#assignvetModal", function (e) {
        e.preventDefault();
        var url = "/Econsultation/SelectVet";
        $.get(url, function (data) {
            $('#SelectassignvetModal').html(data);
            Initialize();
            $('#SelectassignvetModal').modal('show');

        });
    });
});

$("#btn-sm1").click(function () {
    var oItemId = $(this).attr('data-myitemid');
    var doctitle = $($(this).children("i[id=selectVetAnchor]"));
    var doc = $('.fa-check-square');
    doc.removeClass("fa-check-square").addClass("fa-square-o");
    doctitle.removeClass("fa-square-o").addClass("fa-check-square");
    $.ajax({
        url: 'Econsultation/SelectVet',
        data: { Id: oItemId },
        type: 'POST',
        dataType: 'json',
        success: function (success) {
            if (success) {
                $('#vetclose-bt').click();
                $("#assignvetModal").text(success.success.Value);
            }
        },
        error: function (req, status, error) {

        }
    });

});

$(".btn-sm").click(function () {
    var oItemId = $(this).attr('data-myitemid');
    var doctitle = $($(this).children("i[id=selectPetAnchor]"));
    var doc = $('.fa-check-square');
    doc.removeClass("fa-check-square").addClass("fa-square-o");
    doctitle.removeClass("fa-square-o").addClass("fa-check-square");
    $.ajax({
        url: 'Econsultation/SelectPet',
        data: { Id: oItemId },
        type: 'POST',
        dataType: 'json',
        success: function (success) {
            if (success) {
                $('#petclose-bt').click();
                $("#selectPet").text(success.success.Value);
            }
        },
        error: function (req, status, error) {

        }
    });

});