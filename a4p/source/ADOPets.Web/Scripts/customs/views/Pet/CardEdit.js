$(document).ready(function () {
    Initialize();
    $.validator.unobtrusive.parse("#PetForm");

    //$('#PetForm').ajaxForm({
    //    beforeSubmit: BeforeSubtmit,
    //    success: SubmitSuccesful
    //});
    $("#cancelPetButton").click(function () {
        $("#petdetails").load(this.href);
        $(".messageStyle").html('');
        $("#profileeditbtn").show();
        $("#breederEdit").hide();
        $("#cardEdit").show();
        return false;
    });
});

function SubmitSuccesful(result) {
    $('#petdetails').html(result);
    $("#profilepicedit").show();
    $("#breederEdit").hide();
    $("#cardEdit").show();
    var petName = $("#PetName").val();
    var petType = $("#PetType").val();
    $("#parPetName,#profilename h3").text(petName);
    $("#profilename span").text(petType);
    HideSuccessMsg();
}

function BeforeSubtmit() {
    var frmValid = $("#PetForm").valid();
    return frmValid;
}



