function NASort(a, b) {
    return (a.innerHTML > b.innerHTML) ? 1 : -1;
};
var val = $('#PetTypeVDFilter').val();
$('#PetTypeVDFilter option').sort(NASort).appendTo('#PetTypeVDFilter');
$('#PetTypeVDFilter').val(val);

function callDeleteConfirm(ID) {
    $.get($('#DeletePetURL').val() + '/' + ID, function (data) {
        $('#confirmbx').html(data);
        Initialize();
        $('#confirmbx').modal('show');
    });
}

$(function () {
    $('[data-tooltip="tooltip"]').tooltip();

    DrawTable('#table_PetVD', [0]);
    filteronSelectAll('#PetTypeVDFilter', 2, '#table_PetVD');
    var valText = $('#PetNameVDFilter').val();
    $('#table_PetVD').dataTable().fnFilter('^' + valText + '.*', 1, true);
    var valText1 = $('#PetIdVDFilter').val();
    $('#table_PetVD').dataTable().fnFilter('^' + valText1 + '.*', 0, true);
});

$("#PetNameVDFilter").on('keyup click', function () {
    var valText = $('#PetNameVDFilter').val();
    $('#table_PetVD').dataTable().fnFilter('^' + valText + '.*', 1, true);
});

$("#PetIdVDFilter").on('keyup click', function () {
    var valText = $('#PetIdVDFilter').val();
    $('#table_PetVD').dataTable().fnFilter('^' + valText + '.*', 0, true);
});