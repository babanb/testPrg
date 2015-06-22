var culture = GetCurrentCulture();
var domain = GetDomainName();

$(document).ready(function () {

    if (domain == "India") {
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
    

    $('#mailNotificationLabel').hide();
    $('#mailNotificationCheckBox').hide();

    if (culture == "fr-FR" || culture == "pt-BR") {
        var TabDate = $("#addReminderDate").val().split("/");
        var tempDate = TabDate[1] + "/" + TabDate[0] + "/" + TabDate[2];

        var dtPrev = new Date();
        var mnth = (TabDate[1] == 0) ? TabDate[1] : TabDate[1] - 1;
        dtPrev.setFullYear(TabDate[2], mnth, TabDate[0]);
        var dtToday = new Date();

        //alert("dtPrev:" + dtPrev);
        //alert("dtToday:" + dtToday);

        if (dtPrev > dtToday) {
            $('#mailNotificationLabel').show();
            $('#mailNotificationCheckBox').show();
        } else {
            $('#mailNotificationLabel').hide();
            $('#mailNotificationCheckBox').hide();
        }
    } else {
        var tempDate = new Date($("#addReminderDate").val());
        var dtPrev = new Date();
        dtPrev.setFullYear(tempDate.getFullYear(), tempDate.getMonth(), tempDate.getDate());
        var dtToday = new Date();

        if (dtPrev > dtToday) {
            $('#mailNotificationLabel').show();
            $('#mailNotificationCheckBox').show();
        } else {
            $('#mailNotificationLabel').hide();
            $('#mailNotificationCheckBox').hide();
        }
    }
});


$(function () {
    $('#addReminderDate').change(function () {
        // if (domain == "French" || domain == "Portuguese") {
        if (culture == "fr-FR" || culture == "pt-BR") {
            var TabDate = $("#addReminderDate").val().split("/");
            var tempDate = TabDate[1] + "/" + TabDate[0] + "/" + TabDate[2];

            var dtPrev = new Date();
            var mnth = (TabDate[1] == 0) ? TabDate[1] : TabDate[1] - 1;
            dtPrev.setFullYear(TabDate[2], mnth, TabDate[0]);
            var dtToday = new Date();

            if (dtPrev > dtToday) {
                $('#mailNotificationLabel').show();
                $('#mailNotificationCheckBox').show();
            } else {
                $('#mailNotificationLabel').hide();
                $('#mailNotificationCheckBox').hide();
            }
        }
        else {
            var tempDate = new Date($("#addReminderDate").val());
            var dtPrev = new Date();
            dtPrev.setFullYear(tempDate.getFullYear(), tempDate.getMonth(), tempDate.getDate());
            var dtToday = new Date();

            if (dtPrev > dtToday) {
                $('#mailNotificationLabel').show();
                $('#mailNotificationCheckBox').show();
            } else {
                $('#mailNotificationLabel').hide();
                $('#mailNotificationCheckBox').hide();
            }
        }
    });

});