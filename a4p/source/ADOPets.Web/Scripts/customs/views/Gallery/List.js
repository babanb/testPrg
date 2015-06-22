List = {
    initFancyForGallery: function (xpath) {
        $(xpath).fancybox({
            'onComplete': function () {
                var settings = { autoReinitialise: true }; //$('#dvPhotoCommentContainerWrap').jScrollPane(settings);
            },
            'hideOnOverlayClick': false,
            'showCloseButton': true,
            'titlePosition': 'inside',
            'type': 'image'
            //'titleFormat': List.formatGalleryTitle
        });
    },
    formatGalleryTitle: function (title, currentArray, currentIndex, currentOpts) {
        var param = currentArray[currentIndex].href.split('?')[1];
        var imageId = param.split('=')[1];
        var titleString = '<div id="tip7-title">' + 'Picture ' + (currentIndex + 1) + ' of ' + currentArray.length + '</div>';
        titleString += '<div class="home-update-box1" style="min-width:450px;width:auto;float:none;margin-bottom:-10px;padding-right:5px;"><div class="title-second" style="float:left">' + title + '</div><br /><br /><div id="dvPhotoCommentContainerWrap" style="height:170px;"><div id="dvPhotoCommentContainer" > <img src="/Images/notification-icon.png" onload="gallary.loadPhotoComments(' + imageId + ')"/></div></div></div>';

        return titleString;
    }
}

function setGalleryImage(input) {
    var result = GetTranstaltion();
    if (input.files && input.files[0]) {
        var file = input.files[0];
        var extension = file.name.substring(file.name.lastIndexOf('.'));
        // Only process image files.
        var validFileType = ".jpeg, .jpg , .png , .bmp, .gif";
        if (validFileType.toLowerCase().indexOf(extension) < 0) {
            var invalidFiletypeMessage = $("#hdnInvalidImagetype").val();
            alert(invalidFiletypeMessage);
            $('#imgGalleryPhoto').attr('src', '/Content/Images/animal-dimg.jpg');
            $("#ImagePath").val('');
            return false;
        } else {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#imgGalleryPhoto').attr('src', e.target.result);
                $("#ImagePath").val(e.target.result);
            };
        }
        reader.readAsDataURL(input.files[0]);
    }
}


window.setTimeout(function () {
    $(".alert-dismissable").fadeTo(500, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 500);

$(function () {
    $('.editPhotoGallery').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#editModalPhotoGallery').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#editPhotoGalleryForm");
            $('#editModalPhotoGallery').modal('show');
        });
    });

    $('.deletePhotoGallery').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#deleteModalPhotoGallery').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#deletePhotoGalleryForm");
            $('#deleteModalPhotoGallery').modal('show');
        });
    });
});




