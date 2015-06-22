$(function () {
    var current = $("#SelectedMedicalRecord").val();
    $('.nav-tabs a[href=#' + current + ']').tab('show');
    $('#medicalRecordTab a').removeClass("active");
    $('.nav-tabs a[href=#' + current + ']').addClass('active');

    $('#medicalRecordTab a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
        $('#medicalRecordTab a').removeClass("active");
        $(this).addClass('active');
    });
    HideLoading();
});

$("#mrsubcatnavlevel2 .nav-tabs").tabdrop();