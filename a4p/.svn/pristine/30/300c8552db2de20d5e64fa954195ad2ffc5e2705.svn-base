
$(function () {
    $('#addPhotoGallery').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#addModalPhotoGallery').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#addPhotoGalleryForm");
            $('#addModalPhotoGallery').modal('show');
        });
    });
});

function CloseDialogGallery() {
    $('.modal').modal('hide');
    $(".modal-backdrop").hide();
    $(".modal-body").empty();
    $('body').removeClass("modal-open");
    ShowAjaxSuccessMessage($("#photoGallerySuccessMessage").val());
}

// for gallery tabs

$(".petGalleryMenu").click(function () {
    $("#dvMiddleContainer").load($(this).attr('data-href'));
});

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('/') + 1).split('/');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}
