$(document).ready(function () {
    $('#editVideoGalleryForm').ajaxForm({
        beforeSubmit: BeforeSubtmit,
        success: SubmitVideoSuccesful
    });
});

function BeforeSubtmit() {
    var frmValid = $("#editVideoGalleryForm").valid();
    if (frmValid) { ShowLoading(); }
    return frmValid;
}

function SubmitVideoSuccesful(result) {
    $("#btnCloseModel").click();
    $('body').removeClass("modal-open");
    $(".modal-backdrop").hide();
    HideLoading();
    var allQueryString = getUrlVars();
    var petId = allQueryString[4];
    var dataURL = "/VideoGallery/Index?petId=" + petId;
    $("#dvMiddleContainer").load(dataURL);
}