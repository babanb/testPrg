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

function GetCurrentCulture() {
    return $("meta[name='accept-language']").attr("content");
}

function GetDomainName() {
    return $("meta[name='accept-domain']").attr("content");
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

function HideSuccessMsg() {
    window.setTimeout(function () {
        $(".alert-dismissable").fadeTo(1000, 0).slideUp(1000, function () {
            $(this).remove();
        });
    }, 2000);
}

function ShowLoading() {
    $("#divLoading").css('display', 'block');
    $("#divLoading").css('height', $(window).height());
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

function HideLoading() {
    $("#divLoading").css('display', 'none');
}

function Initialize() {
    ConfigureGlobalize();
    DatePickerReady();
    HideSuccessMsg();
}
localStorage.clear();
$(function () { $("[data-toggle='tooltip']").tooltip(); });