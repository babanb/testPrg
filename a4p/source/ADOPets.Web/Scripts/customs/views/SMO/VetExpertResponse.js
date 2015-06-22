List = {
    initPhotoGalleryUpload: function () {
        var innerData = '';
        uploadType = 'album';
        var smoid = $("#Id").val();
        var petid = $("#PetId").val();
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
                    ////     HideLoading();
                    //$("#dvGalleryUploadError").html('Error: invalid file extension');
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
                    innerData += "<a href='temp/dummyDoc.docx' class='btn btn-default' target='_blank' style='float:left'>" + divIcon + " " + imgfilename + "</a><button type='button' onclick='removeRow(\"" + imgfilename.replace(/ /g, '') + "\")' class='btn btn-default'><span class='fa fa-times'></span></button>";
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
$(function () {
    var expertresponsereqmessage = $("#hdnExpertResponseErrorMessage").val();
    $("#ExpertResponse").attr("data-val-required", expertresponsereqmessage);
    $("#ExpertResponse").attr("data-val", "true");

});

$("#ExpertResponse").blur(function () {
    var responsetext = $("#ExpertResponse").val();
    if (responsetext.trim() != "") {
        $("#spnExpertResponse").addClass("field-validation-valid").removeClass("field-validation-error").attr("display", "none");
        $("#ExpertResponse").removeClass("input-validation-error");
    }
});

$(document).ready(function () {
    var smoid = $("#Id").val();
    var petid = $("#PetId").val();
    List.initPhotoGalleryUpload();
    $(document).off("click", "#btnFileUpload").on("click", "#btnFileUpload", function (e) {
        List.initPhotoGalleryUpload();
    });

    function getGalleryData() {

        var galleryList = new Array();
        $('#dvAlbumPhotoList').find("div").each(function () {
            var gallery = new Object();
            var id = $(this).attr('id');
            var dummyId = id.split("-");
            var smoid = $("#Id").val();
            gallery.Id = (dummyId.length > 2) ? 0 : dummyId[dummyId.length - 1];
            gallery.SMOId = smoid;
            gallery.DocumentName = $(this).find("#hdnURL").val();
            galleryList.push(gallery);
        });
        return galleryList;
    }
    
    $('#btnSaveResponse').click(function (e) {

        e.preventDefault();
        var responsetext = $("#ExpertResponse").val();
        if (responsetext.trim() != "") {
            $("#spnExpertResponse").addClass("field-validation-valid").removeClass("field-validation-error").attr("display", "none");
            $("#ExpertResponse").removeClass("input-validation-error");
            var smoid = $("#Id").val();
            var addViewModel = new Object();
            addViewModel.lstAttachment = getGalleryData();
            addViewModel.ExpertResponse = responsetext;
            addViewModel.SMOId = smoid;
            addViewModel.ExpertRelId = $("#ExpertRelId").val();

            var data = '{addViewModel:' + JSON.stringify(addViewModel) + '}';
            ShowLoading();
            $.ajax({
                url: $('#SaveSMOResponse').val(),
                data: data,
                processData: false,
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                success: function (success) {
                    if (success) {
                        HideLoading();
                        window.location.reload();
                     //   var url = $('#IndexURL').val();
                     //   window.location = url;
                    }
                },
                error: function (req, status, error) {
                }
            });

        } else {
            $("#spnExpertResponse").removeClass("field-validation-valid").addClass("field-validation-error").attr("display", "block");
            $("#ExpertResponse").addClass("input-validation-error");
        }
    });


    $('#btnSubmitResponse').click(function (e) {

        e.preventDefault();
        var responsetext = $("#ExpertResponse").val();
        if (responsetext.trim() != "") {
            $("#spnExpertResponse").addClass("field-validation-valid").removeClass("field-validation-error").attr("display", "none");
            $("#ExpertResponse").removeClass("input-validation-error");
            var smoid = $("#Id").val();
            var addViewModel = new Object();
            addViewModel.lstAttachment = getGalleryData();
            addViewModel.ExpertResponse = responsetext;
            addViewModel.SMOId = smoid;
            addViewModel.ExpertRelId = $("#ExpertRelId").val();

            var data = '{addViewModel:' + JSON.stringify(addViewModel) + '}';
            ShowLoading();
            $.ajax({
                url: $('#EditVDURL').val(),
                data: data,
                processData: false,
                contentType: "application/json; charset=utf-8",
                type: 'POST',
                success: function (success) {
                    if (success) {
                        HideLoading();
                        var url = $('#IndexURL').val();
                        window.location = url;
                    }
                },
                error: function (req, status, error) {
                }
            });

        } else {
            $("#spnExpertResponse").removeClass("field-validation-valid").addClass("field-validation-error").attr("display", "block");
            $("#ExpertResponse").addClass("input-validation-error");
        }
    });
});


function removeRow(chkid) {
    //  event.preventDefault();
    var url = "/SMO/RemoveSeletedFile";
    var smoid = $("#Id").val();
    var petid = $("#PetId").val();
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