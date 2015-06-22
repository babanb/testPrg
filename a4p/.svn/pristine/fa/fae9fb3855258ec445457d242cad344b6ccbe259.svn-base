function AddLikes(galleryid, gtype, galbumId) {
    $.post('/GalleryComment/AddLikes', { id: galleryid, type: gtype, albumId: galbumId }, function (data) {
            $("#spanlikes").text(data);
        });
}

function AddComments(varImageid, gtype,galbumId) {
        var varComment = $("#varComment").val();
        $.post('/GalleryComment/AddComments', { imageid: varImageid, comment: varComment, type: gtype, albumId: galbumId }, function (data) {
            $("#divComments").html(data);
            $("#varComment").val('')

            $.get('/GalleryComment/GetCommentsCount', { galleryId: varImageid, type: gtype }, function (data) {
                $("#spanComments").text(data);
                $("#spanCommentsbadge").text(data);
            });

        });

        $.get('/GalleryComment/GetCommentsCount', { galleryId: varImageid, type: gtype }, function (data) {
        
            $("#spanComments").text(data);
            $("#spanCommentsbadge").text(data);
        });
}



function ReplyComments(varCommentId, gtype) {
    var varComment = $("#varReplyComment_" + varCommentId).val();
    $.post('/GalleryComment/ReplyComments', { commentId: varCommentId, comment: varComment, type: gtype }, function (data) {
            $("#divComments").html(data);
            $("#varReplyComment_" + varCommentId).val('');
        });
}

function EditComments(varCommentId, gtype) {
    var varComment = $("#varEditComment_" + varCommentId).val();
    $.post('/GalleryComment/EditComments', { commentId: varCommentId, comment: varComment, type: gtype }, function (data) {
            $("#divComments").html(data);
        });
}

function CancelComment() {
    $("#varComment").val('');
}

function CancelEditComment(varCommentId, varComment, gtype) {
    $("#varEditComment_" + varCommentId).val(varComment);
}

function CancelEditReply(varCommentId, varComment) {
    $("#varEditReply_" + varCommentId).val(varComment);
}

function EditReply(varCommentId, gtype, gid) {
    var varComment = $("#varEditReply_" + varCommentId).val();
    $.post('/GalleryComment/EditReply', { replyid: varCommentId, comment: varComment, type: gtype, galleryId: gid }, function (data) {
        $("#divComments").html(data);
    });
}

$(function () {
    $('.deleteComment').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#deleteCommentModal').html(data);
            $('#deleteCommentModal').modal('show');
        });
    });

    $('.deleteReply').click(function (e) {
        e.preventDefault();
        $.get(this.href, function (data) {
            $('#deleteReplyModal').html(data);
            $('#deleteReplyModal').modal('show');
        });
    });

});

function CloseDialogComments() {
    $('.modal').modal('hide');
}

function CloseDialogDelete(varImageid, gtype) {
    $('.modal').modal('hide');
    $.get('/GalleryComment/GetCommentsCount', { galleryId: varImageid, type: gtype }, function (data) {
        $("#spanComments").text(data);
        $("#spanCommentsbadge").text(data);
    });
}
