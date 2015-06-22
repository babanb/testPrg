﻿$(function () {
    $(document).ajaxError(
        function (e, xhr, settings) {
            if (xhr.status == 401) {
                location.href = GetApplicationPath() + "Account/Login";
            }
        });

    Initialize();
});

function Initialize() {
    ConfigureGlobalize();
    DatePickerReady();
    HideSuccessMsg();
}

function HideSuccessMsg() {
    window.setTimeout(function () {
        $(".alert-dismissable").fadeTo(1000, 0).slideUp(1000, function () {
            $(this).remove();
        });
    }, 2000);
}

function ShowAjaxSuccessMessage(message) {
    console.log(message);
    $("#MessageContainer").html(
    '<div class="alert alert-success alert-dismissable">' +
        '<button type="button" class="close" ' +
                'data-dismiss="alert" aria-hidden="true">' +
            '&times;' +
        '</button>' +
         message +
     '</div>');

    HideSuccessMsg();
}

function ShowAjaxSuccessMessageLonger(message) {
    console.log(message);
    $("#MessageContainer").html(
    '<div class="alert alert-success alert-dismissable">' +
        '<button type="button" class="close" ' +
                'data-dismiss="alert" aria-hidden="true">' +
            '&times;' +
        '</button>' +
         message +
     '</div>');

    window.setTimeout(function () {
        $(".alert-dismissable").fadeTo(5000, 0).slideUp(5000, function () {
            $(this).remove();
        });
    }, 5000);
}


function ShowAjaxWarningMessage(message) {
    $("#MessageContainer").html(
        '<div class="alert alert-danger" role="alert" style="width:42%;">' +
      '<button type="button" class="close" data-dismiss="alert"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>' +
      '<span>' +
      message +
      '</span></div>');
    //window.setTimeout(function () {
    //    $(".alert-danger").fadeTo("slow", 0).slideUp("slow", function () {
    //        $(this).remove();
    //    });
    //}, 2000);
}

function DatePickerReady() {

    var culture = GetCurrentCulture();

    $('.datepicker:not(.datepickerForUserBirthday, .datepickerMonthYear, .datepickerForPetBirthday, .datepickerPast, .datepickerCustomValidation)').datepicker({
        language: culture,
        autoclose: true
    });

    $('.datepickerForUserBirthday').datepicker({
        language: culture,
        autoclose: true,
        startView: 'decade',
        endDate: '-18y'
    });

    $('.datepickerForPetBirthday').datepicker({
        language: culture,
        autoclose: true,
        startView: 'decade',
        endDate: '0d'
    });

    $('.datepickerMonthYear').datepicker({
        language: culture,
        format: "mm/yyyy",
        minViewMode: 1,
        startDate: '1d',
        startView: 'decade',
        autoclose: true
    });

    $('.datepickerPast').datepicker({
        language: culture,
        autoclose: true,
        endDate: '0d'
    });

    $('.datepickerCustomValidation').datepicker({
        language: culture,
        autoclose: true,
        startDate: '0d'
    });

}

//Ask ASP.NET what culture we prefer, because we stuck it in a meta tag
function GetCurrentCulture() {
    return $("meta[name='accept-language']").attr("content");
}
function GetDomainName() {
    return $("meta[name='accept-domain']").attr("content");
}
function DrawTable(idtable, colID) {
    $('[data-tooltip="tooltip"]').tooltip()

    colID = colID || "null";
    $(idtable).dataTable(GetTranstaltionForDatatable(colID));
    $('div' + idtable + '_filter').css("display", "none");
}

jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-uk-pre": function (a) {
        var ukDatea = a.split('/');
        return (ukDatea[2] + ukDatea[1] + ukDatea[0]) * 1;
    },

    "date-uk-asc": function (a, b) {

        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "date-uk-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});



function filteronText(Idtext, columnFilter, IdTable) {

    $(Idtext).on('keyup click', function () {

        filterColumn(IdTable, columnFilter, $(this).val());

    });

}

function filteronSelect(idSelect, columnFilter, IdTable) {
    $(idSelect).on('change', function () {
        if ($(this).find("option:selected").val() == 0) {
            filterColumn(IdTable, columnFilter, '');
        }
        else {
            filterColumn(IdTable, columnFilter, $(this).find("option:selected").text());
        }
    });

}

function filterColumn(IdTable, i, filterttext) {
    $(IdTable).DataTable().column(i).search(
   filterttext).draw();
}


function filteronSelectAll(idSelect, columnFilter, IdTable) {
    $(idSelect).on('change', function () {
        if ($(this).find("option:selected").val() == 0) {
            filterColumn(IdTable, columnFilter, '');
        }
        else {
            filterColumn(IdTable, columnFilter, $(this).find("option:selected").text());
        }
    });

}

function UpdatePhoneNumberValidation(elementId) {
    var domain = $("meta[name='accept-domain']").attr("content");
    if (domain == 'French') {
        $('#' + elementId).attr('data-val-regex-pattern', "^\\(?([0-9]{2})\\)?[-. ]?([0-9]{2})[-. ]?([0-9]{2})[-. ]?([0-9]{2})[-. ]?([0-9]{2})$");
        //  $('#' + elementId).attr('placeholder', "02 22 22 22 22");

    } else if (domain == 'US') {
        $('#' + elementId).attr('data-val-regex-pattern', "^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})");
        $('#' + elementId).attr('placeholder', "222-222-2222");
    }
}


function ConfigureGlobalize() {

    var culture = GetCurrentCulture();

    //Tell jQuery to figure it out also on the client side.
    Globalize.culture(culture);

    //Tell the validator that we want numbers parsed a certain way!
    $.validator.methods.number = function (value, element) {

        if (value == "" && !element.required) {
            return true;
        }

        if (value == 0) {
            return true;
        }

        if (Globalize.parseFloat(value)) {
            return true;
        }

        return false;
    };

    //the [Range] attribute that talks to jQuery Validation doesn't support globalization and isn't calling into the localized methods 
    //so it won't work with the , and . decimal problem
    //The same with date validation, does not work for dd/mm/yyyy
    jQuery.extend(jQuery.validator.methods, {
        range: function (value, element, param) {

            //we allow the comma and dot for all decimal values, does not matter the culture
            var val = value.indexOf(',') != -1 ? parseFloat(value.replace(',', '.')) : Globalize.parseFloat(value);
            return this.optional(element) || (val >= param[0] && val <= param[1]);
        },
        date: function (value, element, param) {
            return this.optional(element) || Globalize.parseDate(value);
        }
    });
}

function GetApplicationPath() {
    return $('#ApplicationPath').val();
}

function ShowLoading() {
    $("#divLoading").css('display', 'block');
    $("#divLoading").css('height', $(window).height());
}

function HideLoading() {
    $("#divLoading").css('display', 'none');
}


/*****************TODO :: For filter and search of Pets Added on 18th Sept 2014********************/

$('#PetTypeFilter').change(function () {
    $("#dvNoRecords").remove();
    showAll();
    var sel = $(this).val();
    console.log($("#PetTypeFilter option:selected").text());
    // alert($('#PetTypeFilter')[0][sel].label);
    if (sel != "") {
        var filtertype = $("#PetTypeFilter option:selected").text();// $('#PetTypeFilter')[0][sel].label;
        displayRecords(filtertype, 'PetNameFilter', 'listing');
    }
    else {
        $('div[class="listing"]').removeAttr('style');
        filtertype = $("#PetNameFilter").val();
        if (filtertype == "" || filtertype == undefined) {
            showAll();
        } else {
            filtertype = filtertype.toLowerCase();
            displayRecordsStartsWith(filtertype, 'PetTypeFilter', 'listing');
            showPageFilter();
        }
    }
    $('[data-tooltip="tooltip"]').tooltip();
});


$("#PetNameFilter").on('keyup click', function () {
    filtertype = $("#PetNameFilter").val();
    showAll();
    if (filtertype != "") {
        filtertype = filtertype.toLowerCase();
        displayRecordsStartsWith(filtertype, 'PetTypeFilter', 'listing');
    }
    else {
        $("[data-sub-container='sub_container']").removeAttr('style');
        var filtertype = $('#PetTypeFilter').val();
        if (filtertype == "") { showAll(); } else {
            var filterPetType = $("#PetTypeFilter option:selected").text();// $('#PetTypeFilter')[0][filtertype].label;
            displayRecords(filterPetType, 'PetNameFilter', 'listing');
            showPageFilter();
        }
    }
    $('[data-tooltip="tooltip"]').tooltip();
});

function displayRecordsStartsWith(filtertype, petTypeFilter, subClass) {
    var petType = $('#' + petTypeFilter).val();
    var filterPetType = "";

    if (petType != undefined && petType != "") {
        filterPetType = $("#" + petTypeFilter + " option:selected").text();// $('#' + petTypeFilter)[0][petType].label;
    }

    filterPetType = filterPetType.toLowerCase();

    if (filterPetType == undefined || filterPetType == "") {
        if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='pet_name']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-name='pet_name']"), function (key, val) {
                if (val.innerHTML.toLowerCase().match("^" + filtertype)) {
                    $(this).parent().parent().parent().parent().show();
                }
                else {
                    $(this).parent().parent().parent().parent().hide();
                }
            });
        }
    } else {

        if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='pet_name']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-name='pet_name']"), function (key, val) {
                if (val.innerText.toLowerCase().match("^" + filtertype)) {
                    $.each($(this).parent().find("[data-item-type='pet_type']"), function (key, val) {
                        if (filterPetType == val.innerText.toLowerCase()) {
                            $(this).parent().parent().parent().parent().show();
                        } else {
                            $(this).parent().parent().parent().parent().hide();
                        }
                    });
                }
                else {
                    $(this).parent().parent().parent().parent().hide();
                }
            });
        }
    }
    displayResult();
}

function displayRecords(filtertype, petNameFilter, subClass) {
    filterText = $("#" + petNameFilter).val();
    filterText = filterText.toLowerCase();
    filtertype = filtertype.toLowerCase();

    if (filterText == undefined || filterText == "") {
        if ($('div[class="' + subClass + '"] :visible').find("[data-item-type='pet_type']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-type='pet_type']"), function (key, val) {
                if (filtertype == val.innerHTML.toLowerCase()) {
                    $(this).parent().parent().parent().parent().show();
                }
                else {
                    $(this).parent().parent().parent().parent().hide();
                }
            });
        }
    }
    else {
        if ($('div[class="' + subClass + '"] :visible').find("[data-item-type='pet_type']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-type='pet_type']"), function (key, val) {
                if (filtertype == val.innerText.toLowerCase()) {
                    $.each($(this).parent().find("[data-item-name='pet_name']"), function (key, val) {
                        if (val.innerText.toLowerCase().match("^" + filterText)) {
                            $(this).parent().parent().parent().parent().show();
                        } else {
                            $(this).parent().parent().parent().parent().hide();
                        }
                    });
                }
                else {
                    $(this).parent().parent().parent().parent().hide();
                }
            });
        }
    }
    displayResult();
}

$(document).ready(function () {
    var msgcount = $("#hdnMessageCount").val();
    $("#spnMessageCount").text(msgcount);
    if ((parseInt(msgcount)) <= 0) {
        $("#notibell i").removeClass('animated swing continuously');
        $("#notibell span").removeClass('animated continuously bounce');
        $("#notibell span").hide();
    }
    else {
        $("#notibell i").addClass('animated swing continuously');
        $("#notibell span").addClass('animated continuously bounce');
        $("#notibell span").show();
    }
});

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

jQuery(function () {
    var clickedLiName = null;
    var subAction;
   
    if (clickedLiName == null) {
        subAction = window.location.href.split('/');
        clickedLiName = "lnk" + subAction[3];
        if (subAction.length > 3) {
            if (subAction[4] == "Invite") {
                clickedLiName = "lnkInvite";
            }
        }
    }

    var chkStatus = 0;
    $(".menuActive li a").each(function () {
        if ($(this).hasClass('active')) {
         //   chkStatus = 1;
        }

        var dataValueResult = $(this).attr("data-select");
        if (dataValueResult != undefined) {
            if (clickedLiName == dataValueResult) {
                chkStatus = 1;
                $(".menuActive li a").removeClass("active");
                if (subAction[4].indexOf("History") > 1) { } else {
                    if (dataValueResult != "") {
                        $("a[data-select='" + dataValueResult + "']").addClass("active");
                    }
                }
            } else {
                // chkStatus = 2;
            }
        }
    });
    if (chkStatus == 0) {
        if (clickedLiName.indexOf("Profile") > 1) {
            $(".menuActive li a").removeClass("active");
        } else {
            $("a[data-select='lnkPet']").addClass("active");
        }
    }
});

/* 9 Oct 14 for medical record menu */

$(".petEditMenu").click(function () {
    $("#mypetmenu").find('a.active').removeClass("active");
    $(this).addClass("active");
    var testURL = $(this).attr('data-href');
    // alert(testURL.indexOf("UpgradePlan"));
    ShowLoading();
    if (testURL.indexOf("UpgradePlan") > 1) {
        $("#UpgradeMyPlan").load($(this).attr('data-href')).addClass("active");
        $('#dvAllData, #dvMiddleContainer').html('');
        HideLoading();
    }
    else {
        $("#dvMiddleContainer").load($(this).attr('data-href'));
        $('#dvAllData,#UpgradeMyPlan, #PlanBilling , #PlanConfirmation, #PaymentResult').html('').removeClass("active");
        HideLoading();
    }
});

function initFancyBox() {
    $.fancybox.init();
}