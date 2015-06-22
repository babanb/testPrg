$(document).ready(function () {
    //UpdatePhoneNumberValidation('FarmerHomePhone');
    //UpdatePhoneNumberValidation('FarmerOfficePhone');
    //UpdatePhoneNumberValidation('FarmerCellPhone');
    //UpdatePhoneNumberValidation('FarmerFax');
    SetUpPetType();
    if ($("meta[name='accept-domain']").attr("content") != "French") {
        UpdatePetStates();
    }
    //UpdateFarmerStates();
    UpdateBloodGroupType();
    SetUpColor();
    SetUpHairType();

    $('#imageFile').on('change', function () {
        readURL(this);
    });

    $("#CountryOfBirth").change(function () {
        if ($("meta[name='accept-domain']").attr("content") != "French") {
            UpdatePetStates();
        }
    });



    $("#PetType").change(function () {
        SetUpPetType();
        UpdateBloodGroupType();

    });

    $("#HairType").change(function () {
        SetUpHairType();
    });

    $("#Color").change(function () {
        SetUpColor();
    });
});

function UpdateBloodGroupType() {

    var petType = $('#PetType').val();
    var currentBloodType = $('#BloodGroupType').val();

    var subItems = "<option value>" + $("#BloodGroupType").children()[0].text + "</option>";

    if ($("#PetType option:selected").index() == 0) {
        $("#BloodGroupType").html(subItems);
    } else {
        $.getJSON(GetApplicationPath() + "Pet/GetBloodGroupTypes", { petType: petType },
            function (data) {
                $.each(data, function (index, item) {
                    if (item.Value == currentBloodType) {
                        subItems += "<option value='" + item.Value + "' selected>" + item.Text + "</option>";
                    } else {
                        subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                    }
                });
                $("#BloodGroupType").html(subItems);
            });
    }
}

function UpdatePetStates() {

    var petCountry = $('#CountryOfBirth').val();
    var petState = $('#StateOfBirth').val();

    var subItems = "<option value>" + $("#StateOfBirth").children()[0].text + "</option>";

    if (petCountry == "France") {
        $("#StateOfBirth").html(subItems);
        $("#StateOfBirth").prop("disabled", true);
    }
    else {
        $("#StateOfBirth").prop("disabled", false);

        if ($("#CountryOfBirth option:selected").index() == 0) {
            $("#StateOfBirth").html(subItems);
        } else {
            $.getJSON(GetApplicationPath() + "Pet/GetStates", { country: petCountry },
                function (data) {
                    $.each(data, function (index, item) {
                        if (item.Value == petState) {
                            subItems += "<option value='" + item.Value + "' selected>" + item.Text + "</option>";
                        } else {
                            subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                        }
                    });
                    $("#StateOfBirth").html(subItems);
                });
        }
    }
}

function SetUpPetType() {
    var petType = $("#PetType").val();
    $("input[id='CurrentPetType']").val(petType);
    if (petType == 'Dog') {
        $('label[for=Breed]').addClass('required');
    } else {
        $('label[for=Breed]').removeClass('required');
    }
}

function SetUpColor() {

    var color = $("#Color").val();
    $("input[id='CurrentColor']").val(color);
    if (color == "Other") {
        $("#ColorOther").prop("readonly", false);
        $('label[for=ColorOther]').addClass('required');
    } else {
        $("#ColorOther").val("");
        $("#ColorOther").prop("readonly", true);
        $('label[for=ColorOther]').removeClass('required');
    }
}

function SetUpHairType() {
    var hairType = $("#HairType").val();
    $("input[id='CurrentHairType']").val(hairType);
    if (hairType == "Other") {
        $("#HairTypeOther").prop("readonly", false);
        $('label[for=HairTypeOther]').addClass('required');

    } else {
        $("#HairTypeOther").val("");
        $('label[for=HairTypeOther]').removeClass('required');
        $("#HairTypeOther").prop("readonly", true);
    }
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#petImage').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}






