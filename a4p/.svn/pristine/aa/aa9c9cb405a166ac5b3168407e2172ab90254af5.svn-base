List = {

    initPhotoGalleryUpload: function () {
        var innerData = '';
        uploadType = 'album';
        var ecid = $("#EcId").val();
        var petid = $("#PetId").val();
        var ownerid = $("#PetOnwerId").val();
        var vetId = $("#VetId").val();
        var buttonFileUpload = $('#btnFileUpload');
        var btnCaption = buttonFileUpload.text();
        //debugger;
        new AjaxUpload(buttonFileUpload, {
            action: '/Handler/EcFileUploadHandler.ashx?uploadType =' + uploadType + '&PetId=' + petid + '&ecId=' + ecid,
            name: 'myfile',
            size: 1,
            data: {
                'uploadType': 'album',
                'PetId': petid,
                'EcId': ecid
            },
            onSubmit: function (file, ext) {
                if (!(ext && /^(jpg|png|jpeg|gif|bmp|pdf|doc|docx|txt)$/i.test(ext))) {
                    //     HideLoading();
                    $("#dvGalleryUploadError").html('Error: invalid file extension');
                    return false;
                }
                // change button text, when user selects file
                this.disable();
                try {
                    $("input[name='rdbDefaultPhoto']:checked").each(function () {
                        $(this).removeAttr("checked");
                    });
                }
                catch (err) { }
                var imgfilename = file.split(".")[0];
                var extString = "word";
                if (ext == "doc" || ext == "docx") {
                    extString = "word";
                }
                if (ext == "pdf") {
                    extString = "pdf";
                }
                if (ext == "txt") {
                    extString = "text";
                }
                if (ext == "jpg" || ext == "png" || ext == "jpeg" || ext == "gif" || ext == "bmp") {
                    extString = "photo";
                }
                trId = "tr-0-" + imgfilename.replace(/ /g, '');
                var innerData = "<div class='btn-group' id='tr-0-" + imgfilename.replace(/ /g, '') + "' datachking='unprocessed'>";
                innerData += "<a href='temp/dummyDoc.docx' class='btn btn-default' target='_blank' style='float:left'><i class='fa  fa-file-" + extString +"-o'></i> " + imgfilename + "</a><button type='button' onclick='removeRow(\"" + imgfilename.replace(/ /g, '') + "\")' class='btn btn-default'><span class='fa fa-times'></span></button>";
                innerData += "<input type='hidden' id='hdnURL' value='response'/>";
                innerData += "</div>";
                $("#dvAlbumPhotoList").append(innerData);
            },
            onComplete: function (file, response) {

                this.enable();
                if (response.toString().match("ERROR:")) {
                    $("#dvGalleryUploadError").html(response);
                    return false;
                }
                imgURLName = "/Econsultation/GetImage?VetId="+ vetId +"&EcId=" + ecid + "&fileName=" + response;
                $("#dvGalleryUploadError").html("");
                var newData = $("#" + trId + "[datachking='unprocessed']").html();
                newData = newData.replace("response", response);
                newData = newData.replace("temp/dummyDoc.docx", imgURLName);
                $("#" + trId + "[datachking='unprocessed']").html(newData);
                $("#" + trId + "[datachking='unprocessed']").removeAttr('datachking');
                List.initPhotoGalleryUpload();
            }
        });
    }
}
$(document).ready(function () {
    var ecid = $("#EcId").val();
    var petid = $("#PetId").val();
    List.initPhotoGalleryUpload();
    $.get("/Econsultation/GetPetInfoStatus", { ecId: ecid }, function (data) {
        $("#hdnPetInfoStatus").attr('value', data.success);
    });

    $(document).off("click", "#btnFileUpload").on("click", "#btnFileUpload", function (e) {
        List.initPhotoGalleryUpload();
    });

    function getFileData() {
        var fileList = new Array();
        $('#dvAlbumPhotoList').find("div").each(function () {
            var objfile = new Object();
            var id = $(this).attr('id');
            var dummyId = id.split("-");
            var ecid = $("#EcId").val();
            objfile.Id = (dummyId.length > 2) ? 0 : dummyId[dummyId.length - 1];
            objfile.ECId = ecid;
            objfile.DocumentName = $(this).find("#hdnURL").val();
            if (objfile.DocumentName != '') {
                fileList.push(objfile);
            }
        });
        return fileList;
    }

    $('#btnSubmitEC').click(function (e) {
        e.preventDefault();
        var requestid = $("#EcId").val();
        var ecid = $("#EcId").val();
        var SubmitReportModel = new Object();
        SubmitReportModel.lstAttachment = getFileData();
        SubmitReportModel.EcId = ecid;
        SubmitReportModel.Diagnosis = $("#Diagnosis").val();
        SubmitReportModel.Treatment = $("#Treatment").val();
        
        var data = '{model:' + JSON.stringify(SubmitReportModel) + '}';
        $.validator.unobtrusive.parse("#SubmitEconsultation");
        var validator = $("#SubmitEconsultation").valid();
        if (validator) {
            $.ajax({
                url: $('#SubmitECURL').val(),
                data: data,
                processData: false,
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                success: function (response) {
                    if (response.success) {
                        ShowAjaxSuccessMessage(response.successMessage);
                        setTimeout('Redirect()', 100);
                    }
                },
                error: function (req, status, error) {
                    // alert(error);
                }
            });
        }
    });

    $('#btnSaveEC').click(function (e) {
        e.preventDefault();
        var requestid = $("#EcId").val();
        var ecid = $("#EcId").val();
        var SubmitReportModel = new Object();
        SubmitReportModel.lstAttachment = getFileData();
        SubmitReportModel.EcId = ecid;
        SubmitReportModel.Diagnosis = $("#Diagnosis").val();
        SubmitReportModel.Treatment = $("#Treatment").val();

        var data = '{model:' + JSON.stringify(SubmitReportModel) + '}';
        $.validator.unobtrusive.parse("#SubmitEconsultation");
        var validator = $("#SubmitEconsultation").valid();
        if (validator) {
            $.ajax({
                url: $('#SaveECURL').val(),
                data: data,
                processData: false,
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                success: function (response) {
                    if (response.success) {
                        ShowAjaxSuccessMessage(response.successMessage);
                        setTimeout('Redirect()', 100);
                    }
                },
                error: function (req, status, error) {
                    // alert(error);
                }
            });
        }
        });
});

function Redirect() {
    window.location = '../Econsultation/Index';
}

$('#previewEC').click(function (e) {
    e.preventDefault();

    if ($("#Diagnosis").val().length > 0 && $("#Treatment").val().length > 0) {
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
    }
});

function removeRow(chkid) {
    var url = "/Econsultation/RemoveSeletedFile";
    var ecid = $("#EcId").val();
    var petid = $("#PetId").val();
    var Filename = $("#hdnURL").val();
    $('#tr-0-' + chkid).remove();
    $.ajax({
        type: "Get",
        url: url,
        data: { "Filename": Filename, "PetId": petid, "EcId": ecid },
        success: function (data) {
        },
        error: function () {
        }
    });
}

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

