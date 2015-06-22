function Initialize() {
    ConfigureGlobalize();
    DatePickerReady();
    HideSuccessMsg();
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

$(document).ready(function () {
    $("#homeForm .panel-heading a").click(function () {
        $("#homeForm .panel-heading a").removeClass("first-time");
    });
    $("#next-bt").click(function () {
        $(".panel-collapse").addClass('hideShowCollapsPanel');
        $(".panel-collapse").addClass('in');
        $("#homeForm .panel-heading a").removeClass("first-time");
    });

    $(".panel-heading").click(function () {
        $(this).next('.panel-collapse').removeClass('hideShowCollapsPanel');
    });
});

$(function () {
    $('a[title]').tooltip();
});

