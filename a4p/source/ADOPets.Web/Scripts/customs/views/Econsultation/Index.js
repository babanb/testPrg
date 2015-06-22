﻿jQuery(function () {
    $(".myreqstatusspan").show(function () {
        var oItemId = $(this).attr('data-myitemid');
        if (oItemId == "Open") {
            $(this).addClass('label-primary');
        }
        else if (oItemId == "Scheduled") {
            $(this).removeClass('label-primary');
            $(this).addClass('label-default');
        }
        else if (oItemId == "Complete") {
            $(this).removeClass('label-primary');
            $(this).addClass('label-success');
        }
        else if (oItemId == "InProgress") {
            $(this).removeClass('label-primary');
            $(this).addClass('label-info');
        }
        else if (oItemId == "Withdraw") {
            $(this).removeClass('label-primary');
            $(this).addClass('label-warning');
        }
        else if (oItemId == "Closed") {
            $(this).removeClass('label-primary');
            $(this).addClass('label-success');
        }
        else {//if (oItemId == smoStatuslist.PaymentPending) {
            $(this).removeClass('label-primary');
            $(this).addClass('label-danger');
        }
    });

    $('#SelectECStatus').change(function () {
        $("#dvNoRecords").remove();
        showAll();
        var sel = $(this).val();
        if (sel != "") {
            var filtertype = $('#SelectECStatus  :selected').text();
            displayRecordsSelectPet(filtertype, 'SelectPetNameFilter', 'listing2');
        }
        else {
            $('div[class="listing2"]').removeAttr('style');
            filtertype = $("#SelectPetNameFilter").val();
            if (filtertype == "" || filtertype == undefined) {
                showAll();
            } else {
                filtertype = filtertype.toLowerCase();
                displayRecordsSelectPetStartsWith(filtertype, 'SelectECStatus', 'listing2');
                showPageFilter();
            }
        }
    });


    $("#SelectPetNameFilter").on('keyup click', function () {
        filtertype = $("#SelectPetNameFilter").val();
        showAll();
        if (filtertype != "") {
            filtertype = filtertype.toLowerCase();
            displayRecordsSelectPetStartsWith(filtertype, 'SelectECStatus', 'listing2');
        }
        else {
            $("[data-sub-container='sub_container']").removeAttr('style');
            var filtertype = $('#SelectECStatus').val();//$('#SelectECStatus :selected').text();
            //alert(filtertype);
            if (filtertype == "") { showAll(); } else {
                filtertype = $('#SelectECStatus :selected').text();
                displayRecordsSelectPet($('#SelectECStatus :selected').text(), 'SelectPetNameFilter', 'listing2');
                showPageFilter();
            }
        }
    });

    function displayRecordsSelectPetStartsWith(filtertype, petTypeFilter, subClass) {
        var petType = $('#' + petTypeFilter).val();
        var filterPetType = "";

        if (petType != undefined && petType != "") {
            filterPetType = $('#' + petTypeFilter + ' :selected').text();
        }
        filterPetType = filterPetType.toLowerCase();
        filtertype = filtertype.toLowerCase();

        if (filterPetType == undefined || filterPetType == "") {
            if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='pet_name']")) {
                var param = $('.' + subClass + ' :visible');
                $.each(param.find("[data-item-name='pet_name']"), function (key, val) {
                    if (val.innerHTML.toLowerCase().match("^" + filtertype)) {
                        $(this).parent().parent().parent().parent().parent().show();
                    }
                    else {
                        $(this).parent().parent().parent().parent().parent().hide();
                    }
                });
            }
        } else {
            if ($('div[class="' + subClass + '"] :visible').find("[data-item-name='pet_name']")) {
                var param = $('.' + subClass + ' :visible');
                $.each(param.find("[data-item-name='pet_name']"), function (key, val) {
                    if (val.innerHTML.toLowerCase().match("^" + filtertype)) {
                        $.each($(this).parent().find("[data-item-type='request_status']"), function (key, val) {
                            if (filterPetType == val.innerHTML.toLowerCase()) {
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

    function displayRecordsSelectPet(filtertype, petNameFilter, subClass) {
        filterText = $("#" + petNameFilter).val();
        filterText = filterText.toLowerCase();
        filtertype = filtertype.toLowerCase();

        if (filterText == undefined || filterText == "") {
            if ($('div[class="' + subClass + '"] :visible').find("[data-item-type='request_status']")) {
                var param = $('.' + subClass + ' :visible');
                $.each(param.find("[data-item-type='request_status']"), function (key, val) {
                    if (filtertype == val.innerHTML.toLowerCase()) {
                        $(this).parent().parent().parent().parent().parent().show();
                    }
                    else {
                        $(this).parent().parent().parent().parent().parent().hide();
                    }
                });
            }
        }
        else {
            if ($('div[class="' + subClass + '"] :visible').find("[data-item-type='request_status']")) {
                var param = $('.' + subClass + ' :visible');
                $.each(param.find("[data-item-type='request_status']"), function (key, val) {
                    if (filtertype == val.innerHTML.toLowerCase()) {
                        $.each($(this).parent().find("[data-item-name='pet_name']"), function (key, val) {
                            if (val.innerHTML.toLowerCase().match("^" + filterText)) {
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
        var itemCount = $('div.listing2').find("#spnRequestStatus").length;
        var chkcount = 0;
        $.each($('div.listing2'), function (key, val) {
            if ($(this).css('display') == 'none') {
                chkcount = chkcount + 1;
            }
        });
        if (chkcount == itemCount) {
            showPageFilter();
            $('div[data-container="paging_container"]').append("<div class='petlist'><div class='row'><div id='dvNoRecords'>" + 'No Matching Records Found' + "</div</div</div>");
            $(".petlist").slideDown(500);
        }
        else {
            $("#dvNoRecords").remove();
            $("[data-paging-control='paging_control']").show('slow');
            showPageFilter();
        }
    }

    function displayResult() {
        $("[data-paging-control='paging_control']").show();
        var itemCount = $('div.listing2').find("#spnRequestStatus").length;
        var chkcount = 0;
        $.each($('div.listing2'), function (key, val) {
            if ($(this).css('display') == 'none') {
                chkcount = chkcount + 1;
            }
        });
        if (chkcount == itemCount) {
            showPageFilter();
            $('div[data-container="paging_container"]').append("<div class='petlist'><div class='row'><div id='dvNoRecords'>" + 'No Matching Records Found' + "</div</div</div>");
            $(".petlist").slideDown(500);
        }
        else {
            $("#dvNoRecords").remove();
            $("[data-paging-control='paging_control']").show('slow');
            showPageFilter();
        }
    }
});
