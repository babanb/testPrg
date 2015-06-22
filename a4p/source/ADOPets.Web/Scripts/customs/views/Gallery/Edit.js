$(document).ready(function () {
    $('#editPhotoGalleryForm').ajaxForm({
        beforeSubmit: BeforeSubtmit,
        success: SubmitImageSuccesful
    });
});

function BeforeSubtmit() {
    var frmValid = $("#editPhotoGalleryForm").valid();
    if (frmValid) { ShowLoading(); }
    return frmValid;
}

function SubmitImageSuccesful(result) {
    $("#btnCloseModel").click();
    $('body').removeClass("modal-open");
    $(".modal-backdrop").hide();
    HideLoading();
    var allQueryString = getUrlVars();
    var petId = allQueryString[4];
    var dataURL = "/PhotoGallery/Index?petId=" + petId;
    $("#dvMiddleContainer").load(dataURL);
}