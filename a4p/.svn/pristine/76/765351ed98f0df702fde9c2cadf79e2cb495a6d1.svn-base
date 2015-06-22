$(window).on('load resize', function () {
    var winheight = $(window).height();
    var scrollheight = winheight - 234;
    //alert(scrollheight);
    $('.customscroll').css("height", scrollheight);
});

$(function () {
    var winheight = $(window).height();
    var scrollheight = winheight - 234;
    //alert(scrollheight);
    $('.customscroll').css("height", scrollheight);
});

//for custom scroller
$('.customscroll').enscroll({
    showOnHover: true,
    verticalTrackClass: 'track3',
    verticalHandleClass: 'handle3'
});

//for tooltip
$(function () { $("[data-toggle='tooltip']").tooltip(); });

$(function () {
    $(".signUpControlbx").find("div.active").removeClass("active");
    $("#dvUserInformation").addClass("active");
});

retriveTempUserInfoValues();


$(document).ready(function () {
    Initialize();
    UpdatePhoneNumberValidation('PrimaryPhone');

    $.validator.unobtrusive.parse("#frmUserInfo");
    $("#btn-next").unbind('click');
    $("#btn-next").click(function () {
        if ($('#frmUserInfo').valid()) {
            saveTempUserInfoValues();
            ShowLoading();
            $.ajax({
                type: "POST",
                url: "/Account/UserInformation",
                data: $("#frmUserInfo").serialize(),
                success: function (result) {
                    $('#mainContainerBox').html(result);
                    HideLoading();
                },
                failure: function (result) {
                    $('#mainContainerBox').html(result);
                    HideLoading();
                }
            });
        }
        return false;
    });

    $("#btnBackStep1").click(function () {
        saveTempUserInfoValues();
        var planId = $("#PlanId").val();
        var promocode = $('#Promocode').val();
        ShowLoading();
        $.ajax({
            type: 'GET',
            url: '/Account/Signup',
            data: { planId: planId, promocode: promocode },
            success: function (data) {
                HideLoading();
                $("#mainContainerBox").html(data);
                $("#txtPromoCode").val(promocode);
                $("#hdnPlanId").val(planId);
                $("#pricingPlans").find(".lnkChoosePlan").removeClass("btn-success").addClass("btn-default").find("i").addClass("fa-circle-o").removeClass("fa-check-circle");
                $("#pricingPlans").find(".lnkChoosePlan[data-PlanId=" + planId + "]").addClass("btn-success").find("i").removeClass("fa-circle-o").addClass("fa-check-circle");
            },
            failure: function (data) {
                HideLoading();
            }
        });
        return false;
    });


    var CurrentTimezone = $("#TimeZone").val();

    $("#TimeZone").html($('#TimeZone option').sort(function (x, y) {
        return $(x).text() < $(y).text() ? -1 : 1;
    }));

    $("#TimeZone").val(CurrentTimezone);

    $('#Password').blur(function () {
        var pass = $(this).val();
        if (pass.match(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,})$/)) {
            $('div#passStrength').html('<span class="glyphicon glyphicon-ok form-control-feedback" style="color:#3c763d; top:0 !important; right:20px !important;"></span>');
        }
        else {
            $('div#passStrength').html('<span class="glyphicon glyphicon-remove form-control-feedback" style="color:#a94442; top:3px !important; right:20px !important;"></span>');
        }
    });

    $('#confPassword').blur(function () {
        var pass = $(this).val();
        if (pass.match(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^\da-zA-Z])(.{8,})$/)) {
            $('div#confpassStrength').html('<span class="glyphicon glyphicon-ok form-control-feedback" style="color:#3c763d; top:0 !important; right:20px !important;"></span>');
        }
        else {
            $('div#confpassStrength').html('<span class="glyphicon glyphicon-remove form-control-feedback" style="color:#a94442; top:3px !important; right:20px !important;"></span>');
        }
    });
});




function UpdateStates() {
    var Country = $('#Country').val();
    var State = $('#State').val();
    var subItems = "<option value>" + $("#State").children()[0].text + "</option>";
    if (Country == "France") {
        $("#State").html(subItems);
        $("#State").prop("disabled", true);
    }
    else {
        $("#State").prop("disabled", false);

        if ($("#Country option:selected").index() == 0) {
            $("#State").html(subItems);
        } else {
            $.getJSON(GetApplicationPath() + "Account/GetStates", { country: Country },
                function (data) {
                    $.each(data, function (index, item) {
                        if (item.Value == State) {
                            subItems += "<option value='" + item.Value + "' selected>" + item.Text + "</option>";
                        } else {
                            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                        }
                    });
                    $("#State").html(subItems);
                });
        }
    }
}

function HideMsg(id) {
    window.setTimeout(function () {
        $(id).fadeTo(1000, 0).slideUp(1000, function () {
            $(this).css('display', 'none');
        });
    }, 2000);
}

function saveTempUserInfoValues() {
    localStorage['firstname'] = $('#FirstName').val();
    localStorage['lastname'] = $('#LastName').val();
    localStorage['email'] = $('#Email').val();
    localStorage['gender'] = $('#Gender').val();
    localStorage['phone'] = $('#PrimaryPhone').val();
    localStorage['drpreferalsource'] = $('#ReferralSource').val();
    localStorage['referencename'] = $('#Reference').val();
    localStorage['dob'] = $('#BirthDate').val();
    localStorage['timezone'] = $('#TimeZone').val();
    localStorage['username'] = $('#Username').val();
}

function retriveTempUserInfoValues() {
    $('#FirstName').val(localStorage['firstname']);
    $('#LastName').val(localStorage['lastname']);
    $('#Email').val(localStorage['email']);
    $('#Gender').val(localStorage['gender']);
    $('#PrimaryPhone').val(localStorage['phone']);
    $('#ReferralSource').val(localStorage['drpreferalsource']);
    $('#Reference').val(localStorage['referencename']);
    $('#BirthDate').val(localStorage['dob']);
    if (localStorage['timezone']) { $('#TimeZone').val(localStorage['timezone']); }
    $('#Username').val(localStorage['username']);
}
