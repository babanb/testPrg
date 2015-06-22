List = {
    initFancyForAlbumGallery: function (xpath) {
        $(xpath).fancybox({
            'onComplete': function () {
                var settings = { autoReinitialise: true };
            },
            'hideOnOverlayClick': false,
            'showCloseButton': true,
            'titlePosition': 'inside',
            'type': 'image',
            'overlayOpacity': '.3',
            'transitionIn': 'elastic',
            'transitionOut': 'elastic',
            'easingIn': 'easeInSine',
            'easingOut': 'easeOutSine',
            'cyclic': true
        });
    },
    initPhotoGalleryUpload: function () {
        var result = GetAlbumGalleryTranslation();
        var innerData = '';
        uploadType = 'album';
        var buttonFileUpload = $('#btnFileUpload');
        var btnCaption = buttonFileUpload.text();
        new AjaxUpload(buttonFileUpload, {
            action: '/Handler/FileUploadHandler.ashx?uploadType =' + uploadType,
            name: 'myfile',
            size: 1,
            data: {
                'uploadType': 'album'
            },
            onSubmit: function (file, ext) {
                if (!(ext && /^(jpg|png|jpeg|gif|bmp)$/i.test(ext))) {
                    var invalidFiletypeMessage = $("#hdnInvalidImagetype").val();
                    $("#dvGalleryUploadError").html(invalidFiletypeMessage);
                    return false;
                }
                this.disable();
                try {
                    $("input[name='rdbDefaultPhoto']:checked").each(function () {
                        $(this).removeAttr("checked");
                    });
                }
                catch (err) { }
                var imgfilename = file.split(".")[0].split(' ').join('_');
                trId = "tr-0-" + imgfilename;
                var innerData = "<tr id='tr-0-" + imgfilename + "' datachking='unprocessed'>";
                innerData += "<td width='20%'><img src='imgURLName' class='img-thumbnail' style='height: 50px;width: 50px;'/>";
                innerData += "<input type='hidden' id='hdnImageURL' value='response'/></td>";
                innerData += "<td width='25%'><input type='text' class='form-control' value='' placeholder='" + result.PhotoTitle + "'/>";
                innerData += "<input type='hidden' id='hdnDeleteFileName' value='deleterowFile'></td>";
                innerData += "<td width='20%' id='td-" + trId + "'> <img class='chkprogress' src='/Content/images/processing.gif' /></td>";
                innerData += "<td width='5%'><a href='javascript:void(0)' onclick='removeRow(\"" + imgfilename + "\")' class='text-warning'><i class='fa fa-trash-o'></i></a></td>";
                innerData += "<td width='10%'><input type='radio' name='rdbDefaultPhoto' id='rdb-" + imgfilename + "' value='" + imgfilename + "' checked/> " + result.ISDefault + " </td>";
                innerData += "</tr>";
                $("#tblNewPhoto").append(innerData);
            },
            onComplete: function (file, response) {
                console.log(response);
                var petid = $("#hdnPetId").val();
                $("#" + trId + "[datachking='unprocessed']").find("#td-" + trId).empty();
                this.enable();
                if (response.toString().match("ERROR:")) {
                    $("#dvGalleryUploadError").html(response);
                    return false;
                }
                imgURLName = "/PhotoGallery/GetImage?fileName=" + response + "&imgType=thumb&petId=" + petid;
                $("#dvGalleryUploadError").html("");
                var newData = $("#" + trId + "[datachking='unprocessed']").html();
                newData = newData.replace("response", response);
                newData = newData.replace("imgURLName", imgURLName);
                newData = newData.replace("deleterowFile", response);
                $("#" + trId + "[datachking='unprocessed']").html(newData);
                $("#" + trId + "[datachking='unprocessed']").find("input[name='rdbDefaultPhoto']").attr("checked", "checked");
                $("#" + trId + "[datachking='unprocessed']").removeAttr('datachking');
                //deleterowFile
                var trCount = $("#tblNewPhoto tr").length;

                if (trCount == 1) {
                    $("#btnSaveAlbum").unbind("click", saveAlbum);
                    $("#btnSaveAlbum").bind("click", saveAlbum);
                    $("#btnSaveAlbum").removeAttr("disabled");
                }
                List.initPhotoGalleryUpload();
            }
        });
    }
}
$('#addAlbumGallery').click(function (e) {
    e.preventDefault();
    $.get(this.href, function (data) {
        $('#addModalAlbumGallery').html(data);
        Initialize();
        $.validator.unobtrusive.parse("#addAlbumGalleryForm");
        $('#addModalAlbumGallery').modal('show');
        loadPhotoGallery();
        $("#dvGalleryPhoto").hide();
        List.initPhotoGalleryUpload();
    });
});

window.setTimeout(function () {
    $(".alert-dismissable").fadeTo(500, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 500);

$('.lnkViewAlbumDetails').click(function (e) {
    e.preventDefault();
    $.get(this.href, function (data) {
        $("#dvAlbumList").html(data);
    });
});

$('.deleteAlbumGallery').click(function (e) {
    e.preventDefault();
    $.get(this.href, function (data) {
        $('#deleteModalAlbumGallery').html(data);
        Initialize();
        $.validator.unobtrusive.parse("#deleteAlbumGalleryForm");
        $('#deleteModalAlbumGallery').modal('show');
    });
});

$('.editAlbumGallery').click(function (e) {
    e.preventDefault();
    $.get(this.href, function (data) {
        $('#editModalAlbumGallery').html(data);
        Initialize();
        $.validator.unobtrusive.parse("#editAlbumGalleryForm");
        $('#editModalAlbumGallery').modal('show');
        loadPhotoGallery();
        $("#dvGalleryPhoto").hide();
        List.initPhotoGalleryUpload();
    });
});

$('.deletePhotoGallery').click(function (e) {
    e.preventDefault();
    $.get(this.href, function (data) {
        $('#deleteModalPhotoGallery').html(data);
        Initialize();
        $.validator.unobtrusive.parse("#deletePhotoGalleryForm");
        $("#deletePhotoGalleryForm").find("#PetId").append("<input id='reqFrom' name='reqFrom' type='hidden value='reqFrom'>");
        $('#deleteModalPhotoGallery').modal('show');
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