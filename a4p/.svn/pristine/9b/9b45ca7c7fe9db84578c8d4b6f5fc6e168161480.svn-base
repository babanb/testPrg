var result1 = GetAlbumGalleryTranslation();
var deletedPhoto = new Array();

function saveAlbum(e) {
    e.preventDefault();
    var addViewModel = getAlbumData();
    if (addViewModel.Title != "") {
        var url = "/AlbumGallery/CreateAlbumGallery";
        var data = '{addViewModel:' + JSON.stringify(addViewModel) + ',deletedPhoto:' + JSON.stringify(deletedPhoto) + '}';
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
                //ShowAjaxSuccessMessage($("#videoGallerySuccessMessage").val());
            },
            error: function () {
                //  alert("error");
            }
        });
    } else {
        //alert(result1.TitleRequired);
        //console.log(result1);
        $("#Title").addClass("input-validation-error");
        $("#Title").parent().find('span').removeClass('field-validation-valid').addClass("field-validation-error").html("<span>" + result1.TitleRequired + "</span>");

    }
}

function removeRow(chkid) {
    if ($('#tr-0-' + chkid).find("input[type='radio']").is(':checked')) {
        var trIDNxt = $('#tr-0-' + chkid)[0].previousElementSibling;
        if (trIDNxt == undefined) { trIDNxt = $('#tr-0-' + chkid)[0].nextElementSibling; }
        $(trIDNxt).find("input[type='radio']").attr('checked', 'checked');
    }
    deletedPhoto.push($("#tr-0-" + chkid).find("#hdnDeleteFileName").val());
    $('#tr-0-' + chkid).remove();
    var trCount = $("#tblNewPhoto tr").length;
  
    if (trCount == 1) { $("#btnSaveAlbum").bind("click", saveAlbum); $("#btnSaveAlbum").removeAttr("disabled"); }
    else if (trCount == 0) { $("#btnSaveAlbum").unbind("click", saveAlbum); $("#btnSaveAlbum").attr("disabled", "disabled"); }
}

function getAlbumData() {
    var addViewModel = new Object();
    addViewModel.Title = $("#Title").val();
    addViewModel.PetId = parseInt($("#hdnPetId").val());
    if ($('input[name="rdbDefaultPhoto"]:checked').length > 0) {
        addViewModel.IsDefault = $('input[name="rdbDefaultPhoto"]:checked').val();
    } else {
        addViewModel.IsDefault = false;
    }
    addViewModel.lstGallery = getGalleryData();
    return addViewModel;
}

function getGalleryData() {
    var galleryList = new Array();
    $('#tblNewPhoto').find("tr").each(function () {
        var gallery = new Object();
        var id = $(this).attr('id');
        var dummyId = id.split("-");

        gallery.Id = (dummyId.length > 2) ? 0 : dummyId[dummyId.length - 1];
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