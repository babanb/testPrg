

$("#btnAddGP").click(function () {

    var result = GetAlbumGalleryTranslation();
    $("input#chkPhoto").each(function () {
        if ($(this).is(":checked")) {
            var ele = ''; 
            var chkId = $(this).attr('data-chk'); 
            var isDisabled = $(this).attr('disabled');
          
            if (chkId != null && isDisabled != "disabled") {

                try {
                    $("input[name='rdbDefaultPhoto']:checked").each(function () {
                        $(this).removeAttr("checked");
                    });
                }
                catch (err) { }

                var datachkvalue = $(this).attr('data-val');
                $(this).removeAttr('data-chk').attr("disabled", true);
                ele = $("#dv-" + chkId + "> figure > img ");
                var phototitle = $("#dv-" + chkId).find("#hdnPhotoTitle").val();
                var imgpath = $("#dv-" + chkId).find("#hdnPhotoURL").val();
                var petid = $("#hdnPetId").val();
                var imgURLName = "/PhotoGallery/GetImage?fileName=" + imgpath+"&imgType=thumb&petId=" + petid;;
                var imgName = imgpath.split('.')[0];
                var innerData = "<tr id='tr-" + chkId + "'>";
                innerData += "<td width='20%'><img src='" + imgURLName + "' class='img-thumbnail' style='height: 50px;width: 50px;'/>";
                innerData += "<input type='hidden' id='hdnImageURL' value='" + imgpath + "'/></td></td>";
                innerData += "<td width='25%'><input type='text' class='form-control' readonly value='" + phototitle + "' /></td>";
                innerData += "<td width='10%'>&nbsp;</td>";
                innerData += "<td width='5%'><a href='javascript:void(0)' onclick='removeGalleryRow(" + chkId + ",\"" + datachkvalue + "\");' class='text-warning'><i class='fa fa-trash-o'></i></a></td>";
                innerData += "<td width='10%'><input type='radio' name='rdbDefaultPhoto' id='rdb-" + chkId + "' value='" + imgName + "' checked />" + result.ISDefault + " </td>";
                innerData += "</tr>";

                $("#tblNewPhoto").append(innerData);
                var trCount = $("#tblNewPhoto tr").length;

                if (trCount == 1) {
                    $("#btnSaveAlbum").unbind("click", saveAlbum);
                    $("#btnSaveAlbum").bind("click", saveAlbum);
                    $("#btnSaveAlbum").removeAttr("disabled");
                }
            }
        }
    });
    //$("#lnkChooseFromGallery").find("input[type='radio']").removeAttr("checked");
    $("#dvGalleryPhoto").hide();
});

function removeGalleryRow(chkid, datachkvalue) {
    if ($('#tr-' + chkid).find("input[type='radio']").is(':checked')) {
        var trIDNxt = $('#tr-' + chkid)[0].previousElementSibling;
        if (trIDNxt == undefined) { trIDNxt = $('#tr-' + chkid)[0].nextElementSibling; }
        $(trIDNxt).find("input[type='radio']").attr('checked', 'checked');
    }
    $('#tr-' + chkid).remove();
    $("#dv-" + chkid).find("input#chkPhoto").attr('data-chk', chkid);
    $("#dv-" + chkid).find("input#chkPhoto").removeAttr('disabled').removeAttr('checked');

    var trCount = $("#tblNewPhoto tr").length;

    if (trCount == 1) { $("#btnSaveAlbum").bind("click", saveAlbum); $("#btnSaveAlbum").removeAttr("disabled"); }
    else if (trCount == 0) { $("#btnSaveAlbum").unbind("click", saveAlbum); $("#btnSaveAlbum").attr("disabled", "disabled"); }
}