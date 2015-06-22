(function () {
    var bar = $('.progress-bar');
    var percent = $('.progress-bar');
    var status = $('#status');

    $('form').ajaxForm({

        beforeSubmit: BeforeSubtmit,
        beforeSend: function () {
            status.empty();
            var percentVal = '0%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        uploadProgress: function (event, position, total, percentComplete) {
            $("#ProgressId").show();
            $("#ProgressBarId").show();
            $("#btnSave").attr("disabled", "disabled");
            $("#btnClose").attr("disabled", "disabled");
            $("#uploadBtn").attr("disabled", "disabled");
            $("#TitleId").attr("disabled", "disabled");
            $("#btnCloseModel").attr("disabled", "disabled");
           
            var percentVal = percentComplete + '%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        success: function () {
            var percentVal = '100%';
            bar.width(percentVal)
            percent.html(percentVal);
        },
        complete: function (xhr) {
            status.html(xhr.responseText);

            $("#btnCloseModel").click();
            $('body').removeClass("modal-open");
            $(".modal-backdrop").hide();
            HideLoading();
            var allQueryString = getUrlVars();
            var petId = allQueryString[4];
            var dataURL = "/VideoGallery/Index?petId=" + petId;
            $("#dvMiddleContainer").load(dataURL);

        }
    });

})();

function BeforeSubtmit() {
    var frmValid = $("#addVideoGalleryForm").valid(); 
    return frmValid;
}

var allowedExtensions = {
    '.mp4': 1,
    '.mov': 1,
    '.3gp': 1,
    '.wmv': 1,
    '.ogv': 1,
    '.flv': 1,
    '.webm': 1,
    '.mkv': 1,
    '.avi': 1
};
function setfilename(val) {
    var fileName = val.substr(val.lastIndexOf("\\") + 1, val.length);
    document.getElementById("filename").value = fileName;

    var match = /\..+$/;
    var ext = val.match(match);
    if (allowedExtensions[ext]) {
        return true;
    }
    else {
        var invalidFiletypeMessage = $("#hdnInvalidVideotype").val();
        alert(invalidFiletypeMessage);
        document.getElementById('filename').value = "";
        $("#uploadfilehidden").val('');
        return false;
    }
}


    
