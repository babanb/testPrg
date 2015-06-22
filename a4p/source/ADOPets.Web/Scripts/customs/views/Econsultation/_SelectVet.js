﻿function callBtnClick1(id) {
    //debugger;
    var oItemId = id;
    var doctitle = $($(this).children("i[id=selectVetAnchor]"));
    var doc = $('.fa-check-square');
    doc.removeClass("fa-check-square").addClass("fa-square-o");
    doctitle.removeClass("fa-square-o").addClass("fa-check-square");
    $.ajax({
        url: 'Econsultation/SelectVet',
        data: { Id: oItemId },
        type: 'POST',
        dataType: 'json',
        success: function (Data) {
           
            if (Data) {
                $('#vetclose-bt').click();
                $("#assignvetModal").text(Data.FirstName.Value + ' ' + Data.LastName.Value);
                localStorage['selectedVet'] = $('#assignvetModal').text();

                $("#assignvetModal").removeAttr('style');
                $("#spnVetRequired").remove();
            }
        },
        error: function (req, status, error) {

        }
    });
}

$("#VetNameFilter").on('keyup click', function () {
    $("#dvNoRecords").remove();
    showAll();
    var sel = $(this).val();
    if (sel != "") {
        var filtertype = $("#VetNameFilter").val();
        displayRecordsSelectPet(filtertype, 'VetNameFilter', 'listing3');
    }
    else {
        $('div[class="listing3"]').removeAttr('style');
        filtertype = $("#VetLastNameFilter").val();
        if (filtertype == "" || filtertype == undefined) {
            showAll();
        } else {
            filtertype = filtertype.toLowerCase();
            displayRecordsSelectPetStartsWith(filtertype, 'VetLastNameFilter', 'listing3');
            showPageFilter();
        }
    }
});


$("#VetLastNameFilter").on('keyup click', function () {
    filtertype = $("#VetLastNameFilter").val();
    showAll();
    if (filtertype != "") {
        filtertype = filtertype.toLowerCase();
        displayRecordsSelectPetStartsWith(filtertype, 'VetLastNameFilter', 'listing3');
    }
    else {
        $("[data-sub-container='sub_container']").removeAttr('style');
        var filtertype = $('#VetLastNameFilter').val();
        if (filtertype == "") { showAll(); } else {
            var filterPetType = $('#VetNameFilter').val();
            displayRecordsSelectPet(filterPetType, 'VetLastNameFilter', 'listing3');
            showPageFilter();
        }
    }
});

function displayRecordsSelectPetStartsWith(filtertype, petTypeFilter, subClass) {
    var petType = $('#' + petTypeFilter).val();
    var filterPetType = "";

    if (petType != undefined && petType != "") {
        filterPetType = $('#' + petTypeFilter)[0][petType].label;
    }

    filterPetType = filterPetType.toLowerCase();

    if (filterPetType == undefined || filterPetType == "") {
        if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='spnVetName']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-name='spnVetName']"), function (key, val) {

                if ($('#' + val.id).text().toLowerCase().match("^" + filtertype)) {
                    $(this).parent().parent().parent().parent().parent().show();
                }
                else {
                    $(this).parent().parent().parent().parent().parent().hide();
                }
            });
        }
    } else {

        if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='spnVetName']")) {
            var param = $('.' + subClass + ' :visible');
            $.each(param.find("[data-item-name='spnVetName']"), function (key, val) {
                if ($('#' + val.id).text().toLowerCase().match("^" + filtertype)) {
                    $.each($(this).parent().find("[data-item-type='spnVetLName']"), function (key, val) {
                        if (filterPetType == $('#' + val.id).text().toLowerCase()) {
                            $(this).parent().parent().parent().parent().parent().show();
                        } else {
                            $(this).parent().parent().parent().parent().parent().hide();
                        }
                    });
                }
                else {
                    $(this).parent().parent().parent().parent().parent().hide();
                }
            });
        }
    }
    displayResultSelectPet();
}

function displayResultSelectPet() {
    //debugger;
    $("[data-paging-control='paging_control']").show();
    var itemCount = $('div.listing3').find("#spnVetLName").length;
    var chkcount = 0;
    $.each($('div.listing3'), function (key, val) {
        if ($(this).css('display') == 'none') {
            chkcount = chkcount + 1;
        }
    });
    if (chkcount == itemCount) {
        showPageFilter();
        $('div[data-container="paging_container"]').append("<div class='vetlist'><div class='row'><div id='dvNoRecords'>" + '@Html.Raw(Resources.Pet_Index_FilterNoResult)' + "</div</div</div>");
        $(".vetlist").slideDown(500);
    }
    else {
        $("#dvNoRecords").remove();
        $("[data-paging-control='paging_control']").show('slow');
        showPageFilter();
    }
}

function displayResult() {
    $("[data-paging-control='paging_control']").show();
    var itemCount = $('div.listing3').find("#spnVetName").length;
    var chkcount = 0;
    $.each($('div.listing3'), function (key, val) {
        if ($(this).css('display') == 'none') {
            chkcount = chkcount + 1;
        }
    });
    if (chkcount == itemCount) {
        showPageFilter();
        $('div[data-container="paging_container"]').append("<div class='vetlist'><div class='row'><div id='dvNoRecords'>" + 'No Result' + "</div</div</div>");
        $(".vetlist").slideDown(500);
    }
    else {
        $("#dvNoRecords").remove();
        $("[data-paging-control='paging_control']").show('slow');
        showPageFilter();
    }
}
