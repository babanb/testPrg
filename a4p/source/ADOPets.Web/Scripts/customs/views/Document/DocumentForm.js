$(function () {

    var documentType = $('#DocumentType').val();
    var documentSubType = $('#DocumentSubType').val();

    UpdateDocumentSubTypes(documentType, documentSubType);
    
    $("#DocumentType").change(function () {

        var val = $(this).val();
        UpdateDocumentSubTypes(val, null);

    });

});

function UpdateDocumentSubTypes(documentType, documentSubType) {

    var subItems = "";

    $.getJSON(GetApplicationPath() + "Document/GetDocumentSubtypes", { documentType: documentType },
           function (data) {
               $.each(data, function (index, item) {
                   if (item.Value == documentSubType) {
                       subItems += "<option value='" + item.Value + "' selected>" + item.Text + "</option>";
                   } else {
                       subItems += "<option value='" + item.Value + "'>" + item.Text + "</option>";
                   }
               });
               $("#DocumentSubType").html(subItems);
           });

}