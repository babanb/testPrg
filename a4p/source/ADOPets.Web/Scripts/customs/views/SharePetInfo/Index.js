
var totalCount = $("#sharepetinfo_table > tbody > tr").find('input[type=checkbox]').length;

$("#sharepetinfo_table > tbody > tr").find("input[type=checkbox]").click(function (e) {
    var count = $("#sharepetinfo_table > tbody > tr").find('input[type=checkbox]:checked').length;
    count = count == undefined ? 0 : count;
    if (count <= totalCount) {
        $("#chkAllShare").prop("checked", false);
    }
});


$("#chkAllShare").click(function (e) {
    var isChecked = $("#chkAllShare").is(":checked");
    $("#sharepetinfo_table > tbody > tr").each(function () {
        $(this).find("input[type=checkbox]").prop("checked", isChecked);
    });
});

function selectContactToShare(thisObj) {
        var hrefData = $(thisObj).attr('data-href');
        $.get(hrefData, function (data) {
            $('#selectContactsModel').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#addContactForm");
            $('#selectContactsModel').modal('show');
        });
}


function selectCommunityContactToShare(thisObj) {
    var hrefData = $(thisObj).attr('data-href');
    $.get(hrefData, function (data) {
        $('#selectContactsModel').html(data);
        Initialize();
        $.validator.unobtrusive.parse("#addContactForm");
        $('#selectContactsModel').modal('show');
    });
}

function selectedContacts(thisObj) {
 
        var hrefData = $(thisObj).attr('data-href');
        $.get(hrefData, function (data) {
            $('#selectedcontactsModel').html(data);
            Initialize();
            $.validator.unobtrusive.parse("#frmselectedContacts");
            $('#selectedcontactsModel').modal('show');
        });
    
}

$("#btnReset").click(function (e) {
    $("#sharepetinfo_table > tbody > tr").each(function () {
        $(this).find("input[type=checkbox]").prop("checked", false);
    });
    $("#chkAllShare").prop("checked", false);
});


$("#drpPetNameFilter").change(function() {
        $.get('/SharePetInfo/ShareIndex', { petId: $(this).val() }, function (data) {
            $("#divIndexPartial").html(data);
        });
});


