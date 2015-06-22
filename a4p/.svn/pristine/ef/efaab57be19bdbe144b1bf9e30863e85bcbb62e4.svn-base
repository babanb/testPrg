
$(document).ready(function () {
    initPager(true);

    $('#VEStatusFilter').ready(function () {
        jQuery("div.col-md-3 option[value=1]").remove();
    });
});

$(function () {
    $('[data-tooltip="tooltip"]').tooltip()

    DrawTable('#table_id', [4]);
    filteronSelect('#VEStatusFilter', 5, '#table_id');
    filteronText('#SMOIdFilter', 1, '#table_id');

});

//$("#SMOIdFilter").on('keyup click', function () {
//    var SMOIDFilter = $('#SMOIdFilter').val();
//    $('#table_id').dataTable().fnFilter('^' + SMOIDFilter + '.*', 1, true);
//});

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