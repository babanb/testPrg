function autoRefresh_div() {
    var activetab = $("#galleryCate").find(".active").attr("data-href");
    if (activetab.indexOf("VideoGallery") > 0) {
        var allQueryString = getUrlVars();
        var petId = allQueryString[4];
        var dataURL = "/VideoGallery/ListVideo?petId=" + petId;
        $("#gallerylist").load(dataURL);// a function which will load data from other file after x seconds
    }
}
if (MyText != 0) {
    if (myVarReloadVideo != undefined)
    { clearTimeout(myVarReloadVideo); }
    myVarReloadVideo = setTimeout('autoRefresh_div()', 40000); // refresh div after 40 secs
}

function setGalleryImage(input) {
    var result = GetTranstaltion();
    if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
            $('#imgGalleryPhoto')
                .attr('src', e.target.result);
            $("#ImagePath").val(e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$(function () {
    window.setTimeout(function () {
        $(".alert-dismissable").fadeTo(500, 0).slideUp(500, function () {
            $(this).remove();
        });
    }, 500);
    $('.editVideoGallery').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#editModalVideoGallery').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#editVideoGalleryForm");
            $('#editModalVideoGallery').modal('show');
        });
    });

    $('.deleteVideoGallery').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#deleteModalVideoGallery').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#deleteModalVideoGallery");
            $('#deleteModalVideoGallery').modal('show');
        });
    });
});


