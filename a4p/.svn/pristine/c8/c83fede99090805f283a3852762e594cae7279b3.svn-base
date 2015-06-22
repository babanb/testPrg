$(document).ready(function () {
    var checkDiagnosis = $("#Diagnosis").val();
    var checkTreatment = $("#Treatment").val();
    if (checkDiagnosis.length > 0 && checkTreatment.length > 0) {
        document.getElementById("previewEC").style.display = 'block';
    }
});

$('#previewEC').click(function (e) {
    e.preventDefault();
    var ecid = $("#EcId").val();
    var SubmitReportModel = new Object();
    SubmitReportModel.EcId = ecid;
    SubmitReportModel.Diagnosis = $("#Diagnosis").val();
    SubmitReportModel.Treatment = $("#Treatment").val();
    var data = { 'Diagnoses': SubmitReportModel.Diagnosis, 'Treatment': SubmitReportModel.Treatment };
    var url = $(this).attr('data-href');
    $.ajax({
        url: $('#PreviewURL').val(),
        data: data,
        cache: false,
        type: 'GET',
        success: function (success) {
            window.location.href = url;
        },
        error: function (req, status, error) {

        }
    });
});

function SetSaveClick() {
    var FlagSave = 1;
    $('#SaveClick').val(1);
}

$('#btnSaveEC').click(function (e) {
    e.preventDefault();
    $('#btnAddIssue').click();
});

function removeFile(fileId, ECId) {
    var url = "/Econsultation/DeleteDocument";
    $.ajax({
        type: "Get",
        url: url,
        data: { "fileId": fileId, "ECId": ECId },
        success: function (data) {
            $("div[data-fileid='" + fileId + "']").remove();
        },
        error: function () {
        }
    });
}


