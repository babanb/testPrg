$(document).ready(function () {

    $('.medicalRecordLink').click(function () {
        ShowLoading();
    });

    $("#HealthHistory, #HealthMeasureTracker, #Document").click(function () {
        $("#medicalRecordContent").load(this.href);
        return false;
    });

});


