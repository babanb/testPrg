List = {

    initPhotoGalleryUpload: function () {
        var innerData = '';
        uploadType = 'album';
        var smoid = $("#SMOREquest_ID").val();
        var petid = $("#SMOREquest_PetId").val();
        var buttonFileUpload = $('#btnFileUpload');
        var btnCaption = buttonFileUpload.text();

        new AjaxUpload(buttonFileUpload, {
            action: '/Handler/SmoFileUploadHandler.ashx?uploadType =' + uploadType + '&PetId=' + petid + '&SmoId=' + smoid,
            name: 'myfile',
            size: 1,
            data: {
                'uploadType': 'album',
                'PetId': petid,
                'SmoId': smoid
            },
            onSubmit: function (file, ext) {
                if (!(ext && /^(jpg|png|jpeg|gif|bmp|pdf|doc|docx|txt)$/i.test(ext))) {
                    var invalidFiletypeMessage = $("#hdnInvalidImagetype").val();
                    $("#dvGalleryUploadError").html(invalidFiletypeMessage);
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
                var divIcon = "<i class='fa  fa-file-word-o'></i>";
                $.get("/SMO/GenerateFileExtDiv", { filename: file }, function (data) {
                    divIcon = data;
                    var imgfilename = file.split(".")[0];
                    trId = "tr-0-" + imgfilename.replace(/ /g, '');
                    var innerData = "<div class='btn-group' id='tr-0-" + imgfilename.replace(/ /g, '') + "' datachking='unprocessed'>";
                    innerData += "<a href='temp/dummyDoc.docx' class='btn btn-default' target='_blank'>" + divIcon + " " + imgfilename + "</a><button type='button' onclick='removeRow(\"" + imgfilename.replace(/ /g, '') + "\")' class='btn btn-default'><span class='fa fa-times'></span></button>";
                    innerData += "<input type='hidden' id='hdnURL' value='response'/>";
                    innerData += "</div>";
                    $("#dvAlbumPhotoList").append(innerData);
                });
            },
            onComplete: function (file, response) {

                this.enable();
                if (response.toString().match("ERROR:")) {
                    $("#dvGalleryUploadError").html(response);
                    return false;
                }
                imgURLName = "/SMO/GetImage?fileName=" + response;
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
    var smoid = $("#SMOREquest_ID").val();
    var petid = $("#PetId").val();
    List.initPhotoGalleryUpload();
    $.get("/SMO/GetPetInfoStatus", { smoId: smoid }, function (data) {
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
            var smoid = $("#SMOREquest_ID").val();
            objfile.Id = (dummyId.length > 2) ? 0 : dummyId[dummyId.length - 1];
            objfile.SMOId = smoid;
            objfile.DocumentName = $(this).find("#hdnURL").val();
            if (objfile.DocumentName != '') {
                fileList.push(objfile);
            }
        });
        return fileList;
    }

    $('#btnSubmitSecondOpinion').click(function (e) {
        e.preventDefault();
        var isPetInfoInComplete = $("#hdnPetInfoStatus").val();
        if (isPetInfoInComplete != "false") {
            $('#alertModalPetInfoStatus').modal('show');
        }
        else {
            var url = $('#ConfirmSendReportURL').val();
            $.get(url, function (data) {
                $('#ConfrimModel').html(data);
                Initialize();
                $('#ConfrimModel').modal('show');
            });
        }
    });

    $('#btnSendReport').click(function (e) {
        ShowLoading();
        var conclusiontext = $("#SMOREquest_VetComment_Value").val();
        var requestid = $("#SMOREquest_ID").val();
        var smoid = $("#SMOREquest_ID").val();
        var addViewModel = new Object();
        addViewModel.lstAttachment = getFileData();
        addViewModel.SMOId = smoid;
        addViewModel.SMOVetConclusion = $("#SMOREquest_VetComment_Value").val();
        addViewModel.RequestReason = $("#SMOREquest_RequestReason").val();
        addViewModel.MedicalHistoryComment = $("#SMOREquest_MedicalHistoryComment").val();
        addViewModel.AdditionalInformation = $("#SMOREquest_AdditionalInformation").val();

        var data = '{model:' + JSON.stringify(addViewModel) + '}';
        $.ajax({
            url: $('#EditVDURL').val(),
            data: data,
            processData: false,
            contentType: "application/json; charset=utf-8",
            type: 'POST',
            success: function (success) {
                HideLoading();
                if (success) {
                    $('#btnCloseSendReport').click();
                    var url = $('#IndexURL').val();
                }
                ShowAjaxSuccessMessageLonger(success.successMessage);
                setTimeout(function () { window.location = url; }, 3000);
            },
            error: function (req, status, error) {
                HideLoading();
            }
        });
    });

    $('#btnSaveSecondOpinion').click(function (e) {
        e.preventDefault();
        var conclusiontext = $("#SMOREquest_VetComment_Value").val();
        var requestid = $("#SMOREquest_ID").val();
        var smoid = $("#SMOREquest_ID").val();
        var addViewModel = new Object();
        addViewModel.lstAttachment = getFileData();
        addViewModel.SMOId = smoid;
        addViewModel.SMOVetConclusion = $("#SMOREquest_VetComment_Value").val();
        addViewModel.RequestReason = $("#SMOREquest_RequestReason").val();
        addViewModel.MedicalHistoryComment = $("#SMOREquest_MedicalHistoryComment").val();
        addViewModel.AdditionalInformation = $("#SMOREquest_AdditionalInformation").val();

        var data = '{model:' + JSON.stringify(addViewModel) + '}';

        $.ajax({
            url: $('#SaveSMOURL').val(),
            data: data,
            processData: false,
            contentType: "application/json; charset=utf-8",
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    ShowAjaxSuccessMessageLonger(response.successMessage);
                    setTimeout(function () { location.reload(); }, 3000);
                }
            },
            error: function (req, status, error) {
            }
        });
    });
});

$('#previewSmo').click(function (e) {
    e.preventDefault();

    var addViewModel = new Object();
    addViewModel.SMOVetConclusion = $("#SMOREquest_VetComment_Value").val();
    addViewModel.RequestReason = $("#SMOREquest_RequestReason").val();
    addViewModel.MedicalHistoryComment = $("#SMOREquest_MedicalHistoryComment").val();
    addViewModel.AdditionalInformation = $("#SMOREquest_AdditionalInformation").val();
    var data = { 'RequestReason': addViewModel.RequestReason, 'MedicalHistoryComment': addViewModel.MedicalHistoryComment, 'VetComment': addViewModel.SMOVetConclusion, 'AdditionalInformation': addViewModel.AdditionalInformation };
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

function removeRow(chkid) {
    var url = "/SMO/RemoveSeletedFile";
    var smoid = $("#SMOREquest_ID").val();
    var petid = $("#SMOREquest_PetId").val();
    var Filename = $("#hdnURL").val();
    $('#tr-0-' + chkid).remove();
    $.ajax({
        type: "Get",
        url: url,
        data: { "Filename": Filename, "PetId": petid, "SmoId": smoid },
        success: function (data) {
        },
        error: function () {
        }
    });
}

function removeFile(fileId, SMOId) {
    var url = "/SMO/DeleteDocument";
    $.ajax({
        type: "Get",
        url: url,
        data: { "fileId": fileId, "SMOId": SMOId },
        success: function (data) {
            $("div[data-fileid='" + fileId + "']").remove();
        },
        error: function () {
        }
    });
}

