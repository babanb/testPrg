var resourceMessage = '@Html.Raw(Resources.PagedListPagerPageItemSliceAndTotalFormat)';
var pager = new Pager();

$(document).ready(function () {
    initPager(true);
    deleteConfirmation();

    var responsiveHelper_dt_basic = undefined;
    var responsiveHelper_datatable_fixed_column = undefined;
    var responsiveHelper_datatable_col_reorder = undefined;
    var responsiveHelper_datatable_tabletools = undefined;

    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };

    $(document).off("click", "#showSmoVetReport").on("click", "#showSmoVetReport", function (e) {
        e.preventDefault();
        var url = $(this).attr('data-href');
        $.get(url, function (data) {
            $('#showModalReport').html(data);
            Initialize();
            $('#showModalReport').modal('show');
        });
    });
});
$(".myreqstatusspan").show(function () {
    var smoStatuslist = GetTranstaltionSMOStatus();
    var oItemId = $(this).attr('data-myitemid');
    if (oItemId == smoStatuslist.Open) {
        $(this).addClass('label-primary');
    }
    else if (oItemId == smoStatuslist.InProgress) {
        $(this).removeClass('label-primary');
        $(this).addClass('label-info');
    }
    else if (oItemId == smoStatuslist.Assigned) {
        $(this).removeClass('label-primary');
        $(this).addClass('label-default');
    }
    else if (oItemId == smoStatuslist.Validated) {
        $(this).removeClass('label-primary');
        $(this).addClass('label-warning');
    }
    else if (oItemId == smoStatuslist.Complete) {
        $(this).removeClass('label-primary');
        $(this).addClass('label-success');
    }
    else if (oItemId == smoStatuslist.PaymentPending) {
        $(this).removeClass('label-primary');
        $(this).addClass('label-danger');
    }
});
function deleteConfirmation() {
    $('[data-toggle="confirmation"]').confirmation({
        title: '@Html.Raw(Resources.StatusConfirmationTitle)',
        btnOkLabel: '@Html.Raw(Resources.StatusConfirmationOkLabel)',
        btnCancelLabel: '@Html.Raw(Resources.StatusConfirmationCancelLabel)'
    });
}

function displayResult() {
    $("[data-paging-control='paging_control']").show();
    var itemCount = $('div.listing2_k').find("#spnPetType").length;
    var chkcount = 0;
    $.each($('div.listing2_k'), function (key, val) {
        if ($(this).css('display') == 'none') {
            chkcount = chkcount + 1;
        }
    });
    if (chkcount == itemCount) {
        showPageFilter();
        $('div[data-container="paging_container"]').append("<div class='petlist'><div class='row'><div id='dvNoRecords'>" + '@Html.Raw(Resources.FilterNoResult)' + "</div</div</div>");
        $(".petlist").slideDown(500);
    }
    else {
        $("#dvNoRecords").remove();
        $("[data-paging-control='paging_control']").show('slow');
        showPageFilter();
    }
}

$(function () {
    $('[data-tooltip="tooltip"]').tooltip()

    DrawTable('#table_id', [5]);
    filteronSelect('#SmoStatusFilter', 5, '#table_id');
    filteronText('#SMOIDFilter', 1, '#table_id');
});


$("#OwnerNameFilter").on('keyup click', function () {
    var OwnerNameFilter = $('#OwnerNameFilter').val();
    $('#table_id').dataTable().fnFilter('^' + OwnerNameFilter + '.*', 2, true);
});

//$("#SMOIDFilter").on('keyup click', function () {
//    var SMOIDFilter = $('#SMOIDFilter').val();
//    $('#table_id').dataTable().fnFilter('^' + SMOIDFilter + '.*', 1, true);
//});

$("#SMOTitleFilter").on('keyup click', function () {
    var SMOTitleFilter = $('#SMOTitleFilter').val();
    $('#table_id').dataTable().fnFilter('^' + SMOTitleFilter + '.*', 3, true);
});

$("#PetNameVDFilter").on('keyup click', function () {
    var PetNameVDFilter = $('#PetNameVDFilter').val();
    $('#table_id').dataTable().fnFilter('^' + PetNameVDFilter + '.*', 0, true);
});

//$("#SmoStatusFilter").on('keyup click', function () {
//    var smoStatusFilter = $('#SmoStatusFilter').val();
//    $('#table_id').dataTable().fnFilter('^' + smoStatusFilter + '.*', 5, true);
//});

$(".dvPetInfo").click(function (e) {
    var smoId = $(this).attr("data-smoId");
    var isInComplete;

    var chkValue = $(this).children("#chk").attr("value");
    if (chkValue.toLowerCase() == "true") {
        var tootipVal = $("#hdnComplete").val();
        $(this).children('#chk').attr('value', 'False');
        //  $(this).children('#chk').removeAttr("checked");
        $(this).removeClass("btn-warning").addClass("btn-default");
        $(this).children('#selectPetAnchor').removeClass("fa-check-square").addClass("fa-square-o");
        $(this).removeAttr('data-original-title').attr('data-original-title', tootipVal);
        isInComplete = false;
    } else {
        var tootipVal = $("#hdnInComplete").val();
        $(this).children('#chk').attr('value', 'True');
        // $(this).children('#chk').click();
        //  $(this).find('#chk').attr('checked', true);
        $(this).removeClass("btn-default").addClass("btn-warning");
        $(this).children('#selectPetAnchor').removeClass("fa-square-o").addClass("fa-check-square");
        $(this).removeAttr('data-original-title').attr('data-original-title', tootipVal);
        isInComplete = true;
    }

    $.ajax({
        url: '/SMO/petInfoStatus',
        data: { smoId: smoId, isInComplete: isInComplete },
        type: 'Get',
        success: function (data) {
            if (data.success) {
                console.log(data.success);
            }
        },
        error: function (req, status, error) {

        }
    });
});
