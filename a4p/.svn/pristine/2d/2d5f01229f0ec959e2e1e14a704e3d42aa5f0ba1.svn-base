$(function () {

    $('#addDocumentForm').ajaxForm({
        
        beforeSubmit: BeforeSubtmit,
        success: SubmitSuccesful
        
    });

});

function SubmitSuccesful(result) {

    CloseDialogDocument();

    $('#Document').html(result);
}

function BeforeSubtmit() {
    return $('#addDocumentForm').valid();
}