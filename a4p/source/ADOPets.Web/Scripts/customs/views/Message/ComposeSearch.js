

$(document).ready(function () {
    $("#FirstNameFilter").keyup(function () {
        var myLength = $("#FirstNameFilter").val().length;
        if (myLength != 0) {
            $("#displaylist").show();
        }
        else {
            $("#displaylist").hide();
        }
    });

    $("#LastNameFilter").keyup(function () {
        var myLength1 = $("#LastNameFilter").val().length;
        if (myLength1 != 0) {
            $("#displaylist").show();
        }
        else {
            $("#displaylist").hide();
        }
    });
});
