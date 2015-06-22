$("#btnCancelImageUpload,#lnkOk").click(function () {
    $('#imgPetCover').removeAttr("src");
    $("#imgPetCover").css('top', '0');
    $("#imgPetCover").css('left', '0');
    $("#imgPetCover").attr('src', $("#hdnPetCImg").val());
    $("#temp_image").html("");
    $("input#imageFileCover").clearInputs(true);
    $("#imageFileCover,#temp_image").clearInputs(true);
});



$("#lnkDeleteCoverPic").click(function () {
    $("#imgPetCover").attr("src", "/Content/Images/cover_Dbg.gif");
    $("#imgPetCover").removeClass("headerimage");
    $("#hdnDeleteCoverPic").val("true");
    $('#lnkDeleteCoverPic1').click();
});

$("#lnkDeleteProfPic").click(function () {
    $("#imgPetProfile").attr("src", "/Content/Images/animal-dimg.jpg");
    $("#hdnDeleteProfPic").val("true");
    $('#lnkDeleteProfPic1').click();
});

function DeleteSuccesful(result) {
    $('#dvPetImage').html(result);
    $("#btnCancelImageUpload").click();
    HideSuccessMsg();
}
