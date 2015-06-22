var result = GetAlbumGalleryTranslation();
var deletedPhoto = new Array();
function removeRow(chkid) {
    console.log(chkid);
    if ($('#tr-0-' + chkid).find("input[type='radio']").is(':checked')) {
        var trIDNxt = $('#tr-0-' + chkid)[0].previousElementSibling;
        if (trIDNxt == undefined) { trIDNxt = $('#tr-0-' + chkid)[0].nextElementSibling; }
        $(trIDNxt).find("input[type='radio']").attr('checked', 'checked');
    }
    deletedPhoto.push($("#tr-0-" + chkid).find("#hdnDeleteFileName").val());
    $('#tr-0-' + chkid).remove();

    $(".sectectPhotocheck").find("input#chkPhoto").attr('data-val', chkid);
    $(".sectectPhotocheck").find("input#chkPhoto").removeAttr('disabled').removeAttr('checked');

    //var trCount = $("#tblNewPhoto tr").length;
    //if (trCount == 1) { $("#btnSaveAlbum").bind("click", saveAlbum); $("#btnSaveAlbum").removeAttr("disabled"); }
    //else if (trCount == 0) { $("#btnSaveAlbum").unbind("click", saveAlbum); $("#btnSaveAlbum").attr("disabled", "disabled"); }
}

$("#btnUpdateAlbum").click(function () {
    var trCount = $("#tblNewPhoto tr").length;
    console.log(trCount);
    if (trCount > 0) {
        var editViewModel = getAlbumData();
        if (editViewModel.Title != "") {
            var url = "/AlbumGallery/EditAlbumGallery";
            var data = '{editViewModel:' + JSON.stringify(editViewModel) + ',deletedPhoto:' + JSON.stringify(deletedPhoto) + '}';
            $.ajax({
                type: "POST",
                url: url,
                data: data,
                processData: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#btnCloseModel").click(); $(".modal-backdrop").hide();
                    $("#dvMiddleContainer").load($(".petGalleryMenu.active").attr('data-href'));
                    HideSuccessMsg();
                },
                error: function () {
                    //  alert("error");
                }
            });
        } else {
            $("#Title").addClass("input-validation-error");
            $("#Title").parent().find('span').removeClass('field-validation-valid').addClass("field-validation-error").html("<span>" + result.TitleRequired + "</span>");
        }
    }
});

function getAlbumData() {
    var editViewModel = new Object();
    
    editViewModel.Title = $("#Title").val();
    editViewModel.PetId = parseInt($("#hdnPetId").val());
    editViewModel.Id = parseInt($("#hdnAlbumId").val());

    if ($('input[name="rdbDefaultPhoto"]:checked').length > 0) {
        editViewModel.IsDefaultCover = $('input[name="rdbDefaultPhoto"]:checked').val();
    } else { editViewModel.IsDefaultCover = false; }
    editViewModel.lstGallery = getGalleryData();
    return editViewModel;
}

function getGalleryData() {
    var galleryList = new Array();
    $('#tblNewPhoto').find("tr").each(function () {
        var gallery = new Object();
        var id = $(this).attr('id');
        var dummyId = id.split("-");
        var galId = $(this).find("#hdnGalleryId").val();
        var dummygalId = (dummyId.length > 2) ? 0 : dummyId[dummyId.length - 1];
        gallery.Id = (galId > 0) ? galId : dummygalId;
   
        gallery.Title = $(this).find("input[type='text']").val();
        gallery.PetId = parseInt($("#hdnPetId").val());
        gallery.CreatedDate = Date.now();
        gallery.ImageURL = $(this).find("#hdnImageURL").val();
        gallery.IsGalleryPhoto = true;
        galleryList.push(gallery);
    });
    return galleryList;
}


$(document).ready(function () {
    $("#lnkChooseFromGallery").click(function () {
        loadPhotoGallery();
    });
    $("#btnCloseModel").click(function () {
        $(".modal-backdrop").hide();
        $("#dvMiddleContainer").load($(".petGalleryMenu.active").attr('data-href'));
        HideSuccessMsg();
    });
});


function loadPhotoGallery() {
    if ($("#dvGalleryPhoto").find("input#chkPhoto").length > 0) {
        $("#dvGalleryPhoto").show();
    } else {
        var petid = $("#hdnPetId").val();
        var albumId = $("#hdnAlbumId").val();

        $(this).find("input").attr("checked", "checked");
        $.get('/AlbumGallery/ListPhotoGallery/', { petId: petid, albumId: albumId }, function (data) {
            $("#dvGalleryPhoto").html(data);
        });
    }
}


