
$(document).ready(function () {
    $("#newSmo .panel-heading a").click(function () {
        $("#newSmo .panel-heading a").removeClass("first-time");
    });
    $(".panel-heading").click(function () {
        $(this).next('.panel-collapse').removeClass('hideShowCollapsPanel');

    });
    $(document).off("keyup click", "#TestDescription").on("keyup click", "#TestDescription", function (e) {
        var val = $("#TestDescription").val();
        if (val != "") {
            var span = $("#divTestName").find(".field-validation-error");
            span.removeClass("field-validation-error").addClass("field-validation-valid");
            span.html("");
        }
    });
    $(document).off("change", "#TestDate").on("change", "#TestDate", function (e) {
        var val = $("#TestDate").val();
        if (val != "") {
            var span = $("#divTestDate").find(".field-validation-error");
            span.removeClass("field-validation-error").addClass("field-validation-valid");
            span.html("");
        }
    });
    function callValidation()
    {
        var smoStatuslist = GetTranstaltionSMOTest();
        var testDescValue = $("#TestDescription").val();
        var testDateValue = $("#TestDate").val();
        if (testDescValue == '' || testDateValue == '') {
            if (testDescValue == '') {
                var span = $("#divTestName").find(".field-validation-valid");
                span.removeClass("field-validation-valid").addClass("field-validation-error");
                span.html("<span id='TestDescription-error' class=''>" + smoStatuslist.TestNameRequired + "</span>");
            }
            if (testDescValue == '') {
            var span1 = $("#divTestDate").find(".field-validation-valid");
            span1.removeClass("field-validation-valid").addClass("field-validation-error");
            span1.html("<span id='TestDate-error' class=''>"+smoStatuslist.TestDateRequired+"</span>");
            }
            return false;
        }
        
        return true;
    }
    $(document).off("keyup click", "#EditTestDescription").on("keyup click", "#EditTestDescription", function (e) {
        var val = $("#EditTestDescription").val();
        if (val != "") {
            var span = $("#divEditTestName").find(".field-validation-error");
            span.removeClass("field-validation-error").addClass("field-validation-valid");
            span.html("");
        }
    });
    $(document).off("change", "#EditTestDate").on("change", "#EditTestDate", function (e) {
        var val = $("#EditTestDate").val();
        if (val != "") {
            var span = $("#divEditTestDate").find(".field-validation-error");
            span.removeClass("field-validation-error").addClass("field-validation-valid");
            span.html("");
        }
    });
    function callEditValidation() {
        var smoStatuslist = GetTranstaltionSMOTest();
        var testDescValue = $("#EditTestDescription").val();
        var testDateValue = $("#EditTestDate").val();
        if (testDescValue == '' || testDateValue == '') {
            if (testDescValue == '') {
                var span = $("#divEditTestName").find(".field-validation-valid");
                span.removeClass("field-validation-valid").addClass("field-validation-error");
                span.html("<span id='EditTestDescription-error' class=''>" + smoStatuslist.TestNameRequired + "</span>");
            }
            if (testDateValue == '') {
                var span = $("#divEditTestDate").find(".field-validation-valid");
                span.removeClass("field-validation-valid").addClass("field-validation-error");
                span.html("<span id='EditTestDate-error' class=''>" + smoStatuslist.TestDateRequired + "</span>");

                
            }
            return false;
        }
        return true;
    }

    $('a[title]').tooltip();
    $(document).off("click", "#testsubmit-bt").on("click", "#testsubmit-bt", function (e) {
        e.preventDefault(); //prevent default form submit
        
        if (callValidation()) {
            var urlTest = $(this).attr('data-url');
            $.ajax({

                url: urlTest,
                data: { TestName: $("#TestDescription").val(), TestDate: $("#TestDate").val() },
                type: 'POST',
                dataType: 'json',
                cache: false,
                success: function (success) {
                    if (success.success == true) {
                        $('#testclose-bt').click();
                        var urllist = $('#InvestigationListURL').val();
                        $('#divInvestigation').load(urllist);
                    }
                    else {
                        alert("The Test already exists.");
                        $("#TestDescription").val("");
                        $("#TestDate").val("");
                        $("#TestDescription").focus();
                    }
                },
                error: function (req, status, error) {
                }
            });
        }
        else {
            return false;
        }
    });
    $(document).off("click", "#next-bt").on("click", "#next-bt", function (e) {
        e.preventDefault();
        $.validator.unobtrusive.parse("#newSmo");
        var validator = $("#newSmo").valid();
        var Quest = $("#Question").val();
        var Ans = $("#FirstOpinion").val();
        var diag = $("#Diagnosis").val();
        var selectPet = $("#selectPet").html();
        if (Quest == "" || Ans == "" || diag=="") {
            validator = false;
        }
        if (selectPet.trim().match("Click here"))
        {
            validator = false;
            $("#selectPet").removeAttr('style');
            $("#spnPetRequired").remove();
            //flag = false;
            $("#selectPet").attr('style', 'border: 1px solid #a94442;!important');
            $("#selectPetlink").append('<span id="spnPetRequired">' + $("#PetId").attr("data-val-required") + '</span>');
            $("#spnPetRequired").addClass("field-validation-error");//.html("<span>Pet is required</span>");
        }
        if (validator) {
            $.ajax({
                url: $(this).attr('data-url'),
                data: $("#newSmo").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (success) {
                    if (success) {
                        var url = $("#BillingURL").val();;
                        $('#divStep').load(url);
                        $('#Step1').removeClass('active');
                        $('#Step2').addClass("active");
                    }
                },
                error: function (req, status, error) {
                }
            });
        }
        else {
            //alert("Please fill all mandatory fields");
            if ($("#newSmo .panel-heading a").hasClass("mandatory"))
            {
                $("#pnlCondition .panel-collapse").addClass('hideShowCollapsPanel');
                $("#pnlCondition .panel-collapse").addClass('in');
                $("#pnlCondition .panel-heading a").removeClass("first-time");
                $("#pnlFirstOpinion .panel-collapse").addClass('hideShowCollapsPanel');
                $("#pnlFirstOpinion .panel-collapse").addClass('in');
                $("#pnlFirstOpinion .panel-heading a").removeClass("first-time");
            }
            if (Quest == "")
            {
                $("#Question").valid();
            }
            if (Ans == "") {
                $("#FirstOpinion").valid();
            }
            if (diag == "") {
                $("#Diagnosis").valid();
            }
        }
    });
    $(document).off("click", "#submit-bt").on("click", "#submit-bt", function (e) {
        e.preventDefault();
        $.validator.unobtrusive.parse("#newSmo");
        var validator = $("#newSmo").valid();
        if (validator) {
            $.ajax({
                url: $(this).attr('data-url'),
                data: $("#newSmo").serialize(),
                type: 'POST',
                dataType: 'json',
                success: function (success) {
                    if (success) {
                        var url = $('#PaymentURL').val();
                        $('#divStep').load(url);
                    }
                },
                error: function (req, status, error) {

                }
            });
        }
        else {
            //alert("Please fill all mandatory fields");
        }
    });
    $(document).off("click", "#cancel-bt").on("click", "#cancel-bt", function (e) {
        var url = $('#UrlIndexVD').val();
        window.location = url;
    });

    $(document).off("click", "#submitVD-bt").on("click", "#submitVD-bt", function (e) {
        e.preventDefault();
        $.validator.unobtrusive.parse("#newSmo");
        var validator = $("#newSmo").valid();
        var Quest = $("#Question").val();
        var Ans = $("#FirstOpinion").val();
        var diag = $("#Diagnosis").val();
        var selectPet = $("#selectPet").html();
        var selectOwner = $("#selectOwner").html();
        if (Quest == "" || Ans == "" || diag == "") {
            validator = false;
        }
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
        if (validator) {
            ShowLoading();
            $.ajax({
                url: $(this).attr('data-url'),
                data: $("#newSmo").serialize(),
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
            if (Quest == "") {
                $("#Question").valid();
            }
            if (Ans == "") {
                $("#FirstOpinion").valid();
            }
            if (diag == "") {
                $("#Diagnosis").valid();
            }
        }
    });
    $(document).off("click", "#testedsubmit-bt").on("click", "#testedsubmit-bt", function (e) {
        e.preventDefault(); //prevent default form submit
        if (callEditValidation()) {
            $.ajax({
                url: $('#EditInvestigationURL').val(),
                data: { TestName: $("#EditTestDescription").val(), TestDate: $("#EditTestDate").val() },
                type: 'POST',
                dataType: 'json',
                success: function (success) {
                    if (success.success) {
                        $('#testedclose-bt').click();
                        var urllist = $('#InvestigationListURL').val();
                        $('#divInvestigation').load(urllist);
                    }
                    else {
                        alert("The Test already exists.");
                        $("#EditTestDescription").val("");
                        $("#EditTestDate").val("");
                        $("#EditTestDescription").focus();
                    }
                },
                error: function (req, status, error) {
                }
            });

        }
        else {
            return false;
        }
    });

    
});
function callDeleteTest(ID) {
    $.ajax({
        url: $('#DeleteTestURL').val(),
        data: { 'ID': ID },
        type: 'POST',
        dataType: 'json',
        success: function (success) {
            if (success) {
                $('#testdelclose-bt').click();
                var urllist = $('#InvestigationListURL').val();
                $('#divInvestigation').load(urllist);
            }
        },
        error: function (req, status, error) {
        }
    });
}
function callDeleteConfirmTest(ID) {
    $.get($('#DeleteInvestigationURL').val() + '/' + ID, function (data) {
        $('#deleteModalTest').html(data);
        Initialize();
        $('#deleteModalTest').modal('show');
    });
}
function showEditTestDiv(ID) {
    $.get($('#EditInvestigationURL').val() + '/' + ID, function (data) {
        $('#editModalTest').html(data);
        Initialize();
        $('#editModalTest').modal('show');
    });
}

