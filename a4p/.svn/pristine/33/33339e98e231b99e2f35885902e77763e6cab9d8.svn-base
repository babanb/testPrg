var culture = GetCurrentCulture();

function GetCurrentCulture() {
    return $("meta[name='accept-language']").attr("content");
}

$(document).ready(function () {

    var ECID = $("#Id").val();
    if (ECID > 0) {
        $('#Country').val($("#CountryId").val());
        $('#TimeZone').val($("#TimeZoneId").val());
    }

    if (culture == "en-IN") {
        $('.timepicker1').datetimepicker({
            language: 'en-gb',
            autoclose: true,
            pickDate: false
        });
    }
    else {
        $('.timepicker1').datetimepicker({
            language: culture,
            autoclose: true,
            pickDate: false
        });
    }

    UpdatePhoneNumberValidation('Phone');

    $("#newenconsultation .panel-heading a").click(function () {
        $("#newenconsultation .panel-heading a").removeClass("first-time");
    });

    $('#bestcontactoption').on('change', function () {

        if ($(this).val() == 'Email') {
            $('#enteremail').show();
            $('#enterphone').hide();
        } else {
            $('#enteremail').hide();
            $('#enterphone').show();
        }
    });

    $("#Time").on("keypress", function (event) {
        if (event.keyCode == 37 || event.keyCode == 39) {
            return true;
        }
        return false;
    });

    $('#Time').on("paste", function (e) {
        e.preventDefault();
    });

    //$('#Phone').keyup(function () {
    //    if (this.value.match(/[^0-9]/g) && this.value.length < 2) {
    //        this.value = this.value.replace(/[^0-9]/g, '');
    //    }
    //});

    jQuery(function () {
        return $("body").off("click", "#submit-bt", function (e) {
        });
    });

    jQuery(function () {
        return $("body").on("click", "#submit-bt", function (e) {
            e.preventDefault();
            $.validator.unobtrusive.parse("#newEconsultation");
            var validator = $("#newEconsultation").valid();
            if (validator) {
                ShowLoading();
                $.ajax({
                    url: '/Econsultation/Setup',
                    data: $("#newEconsultation").serialize(),
                    type: 'POST',
                    dataType: 'json',
                    success: function (success) {

                        if (success) {
                            var url = "/Econsultation/Payment";
                            $('#divStep').load(url);
                            HideLoading();
                        }
                    },
                    error: function (req, status, error) {

                    }
                });
            }
            else {
            }
        });
    });

    jQuery(function () {
        return $("body").off("click", "#next-bt", function (e) {
        });
    });

    jQuery(function () {
        return $("body").on("click", "#next-bt", function (e) {
            var petId = $("#PetId").val();
            var flag = true;
            if (petId <= 0) {
                $("#selectPet").removeAttr('style');
                $("#spnPetRequired").remove();
                flag = false;
                $("#selectPet").attr('style', 'border: 1px solid #a94442;!important');
                $("#selectPetlink").append('<span id="spnPetRequired">' + $("#PetId").attr("data-val-required") + '</span>');
                $("#spnPetRequired").addClass("field-validation-error");
            }
            if (flag) {
                e.preventDefault();

                $.validator.unobtrusive.parse("#newEconsultation");

                var validator = $("#newEconsultation").valid();
                if (validator) {
                    ShowLoading();
                    var vetId = $("#VetID").val();
                    flag = true;
                    if (vetId <= 0) {
                        $("#assignvetModal").removeAttr('style');
                        $("#spnVetRequired").remove();
                        flag = false;
                        $("#assignvetModal").attr('style', 'border: 1px solid #a94442;!important');
                        $("#assignvetLink").append('<span id="spnVetRequired">' + $("#VetID").attr("data-val-required") + '</span>');
                        $("#spnVetRequired").addClass("field-validation-error");
                    }
                    if (flag) {
                        $.ajax({
                            url: '/Econsultation/Setup',
                            data: $("#newEconsultation").serialize(),
                            type: 'POST',
                            dataType: 'json',
                            success: function (success) {
                                if (success) {


                                    var url = "/Econsultation/Billing";
                                    $('#Step1').removeClass("active");
                                    $('#Step2').addClass("active");
                                    $('#Billing').load(url);
                                    toggle_visibility2();
                                    HideLoading();
                                }
                            },
                            error: function (req, status, error) {
                            }
                        });
                    }
                }
                else {
                }
            }
        });
    });
    $('a[title]').tooltip();
});


$(document).off("click", "#submitVD-bt").on("click", "#submitVD-bt", function (e) {
    e.preventDefault();
    $.validator.unobtrusive.parse("#newEC");
    var validator = $("#newEC").valid();
    //var Quest = $("#Question").val();
    //var Ans = $("#FirstOpinion").val();
    //var diag = $("#Diagnosis").val();
    var selectPet = $("#selectPet").html();
    var selectOwner = $("#selectOwner").html();
    //if (Quest == "" || Ans == "" || diag == "") {
    //    validator = false;
    //}
    var vetId = $("#VetID").val();

    if (selectPet.trim().match("Click here")) {
        validator = false;
        $("#selectPet").removeAttr('style');
        $("#spnPetRequired").remove();
        $("#selectPet").attr('style', 'border: 1px solid #a94442;!important');
        $("#selectPetlink").append('<span id="spnPetRequired">' + $("#PetId").attr("data-val-required") + '</span>');
        $("#spnPetRequired").addClass("field-validation-error");//.html("<span>Pet is required</span>");
    }
    if (selectOwner.trim().match("Click here")) {
        validator = false;
        $("#selectOwner").removeAttr('style');
        $("#spnOwnerRequired").remove();
        $("#selectOwner").attr('style', 'border: 1px solid #a94442;!important');
        $("#selectOwnerlink").append('<span id="spnOwnerRequired">' + $("#OwnerId").attr("data-val-required") + '</span>');
        $("#spnOwnerRequired").addClass("field-validation-error");//.html("<span>Owner is required</span>");
    }
    if (vetId <= 0) {
        $("#assignvetModal").removeAttr('style');
        $("#spnVetRequired").remove();
        validator = false;
        $("#assignvetModal").attr('style', 'border: 1px solid #a94442;!important');
        $("#assignvetLink").append('<span id="spnVetRequired">' + $("#VetID").attr("data-val-required") + '</span>');
        $("#spnVetRequired").addClass("field-validation-error");
    }


    if (validator) {
        ShowLoading();
        $.ajax({
            url: $(this).attr('data-url'),
            data: $("#newEC").serialize(),
            type: 'POST',
            dataType: 'json',
            success: function (success) {
                if (success) {
                    var url = $('#UrlIndexVD').val();
                    window.location = url;
                    HideLoading();
                }
            },
            error: function (req, status, error) {

            }
        });
    }
    else {
        //alert("Please fill all mandatory fields");
        if ($("#newSmo .panel-heading a").hasClass("mandatory")) {
            $("#pnlCondition .panel-collapse").addClass('hideShowCollapsPanel');
            $("#pnlCondition .panel-collapse").addClass('in');
            $("#pnlCondition .panel-heading a").removeClass("first-time");
            $("#pnlFirstOpinion .panel-collapse").addClass('hideShowCollapsPanel');
            $("#pnlFirstOpinion .panel-collapse").addClass('in');
            $("#pnlFirstOpinion .panel-heading a").removeClass("first-time");
        }
        //if (Quest == "") {
        //    $("#Question").valid();
        //}
        //if (Ans == "") {
        //    $("#FirstOpinion").valid();
        //}
        //if (diag == "") {
        //    $("#Diagnosis").valid();
        //}
    }
});

$(document).off("click", "#cancel-bt").on("click", "#cancel-bt", function (e) {
    var url = $('#UrlIndexVD').val();
    window.location = url;
});