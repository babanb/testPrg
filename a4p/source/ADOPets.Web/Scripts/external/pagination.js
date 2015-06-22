var recordsPerPage = 0;
var totalRecordCount = 0;
var pageName = false;

var ua = window.navigator.userAgent;
var msie = ua.indexOf("MSIE ");

if (msie > 0)      // If Internet Explorer, return version number
{  //alert(parseInt(ua.substring(msie + 5, ua.indexOf(".", msie))));
    $("[data-container='paging_container']").attr("style", "display:none;");
}
else                 // If another browser, return 0
{ $("[data-container='paging_container']").removeAttr("style"); }


function initPager(pageTitle) {
    pageName = pageTitle;
    pager.init();
    pager.showPage(1, resourceMessage);
}

function showPageFilter() {
    pager.showPageFilter(1, resourceMessage);
}

function showAll() {
    pager.showPage(0, null);
}

Pager = function () {
    this.itemsPerPage;
    this.currentPage = 1;
    this.pagingControlsContainer;
    this.pagingContainerPath;
    this.paragraphs;
    this.resourceMessage = '';
    this.childClass;
    this.checkParagraphs;
    this.init = function () {
        this.itemsPerPage = recordsPerPage = $("[data-container='paging_container']").attr('data-page-size');
        this.pagingControlsContainer = $("[data-paging-control='paging_control']");
        this.paragraphs = $("[data-container='paging_container']").find("[data-sub-container='sub_container']");
        this.pagingContainerPath = (msie > 0) ? $("#subCntnt1") : $("[data-container='paging_container']");
        var subClassChk = $("[data-sub-container='sub_container']").attr('class');
        this.childClass = (subClassChk != undefined) ? subClassChk.split(' ')[0] : "";
        this.checkParagraphs = $("[data-sub-container='sub_container']");
    };

    this.numPages = function () {
        var numPages = 0;
        if (this.paragraphs != null && this.itemsPerPage != null) {
            numPages = Math.ceil(this.paragraphs.length / this.itemsPerPage);
        }
        return numPages;
    };

    this.showPage = function (page, resourceMessage) {
        this.currentPage = page;
        this.resourceMessage = resourceMessage;
        this.paragraphs = this.checkParagraphs;
        if (page == 0) {
            var html = '';
            var subclass = this.childClass;
            this.currentPage = 1;
            totalRecordCount = this.paragraphs.length;
            this.paragraphs.each(function () {
                html += '<div class="' + subclass + '">' + $(this).html() + '</div>';
            });
            $(this.pagingContainerPath).html(html);
            this.currentPage = 1;
            renderControls(this.pagingControlsContainer, this.currentPage, this.numPages(), resourceMessage, subclass);
        }
        else {
            this.paginateData(page);
        }
    }

    this.showPageFilter = function (page, resourceMessage) {
        this.currentPage = page;
        this.resourceMessage = resourceMessage;
        var subclass = this.childClass;
        if (this.childClass != "") {
            this.paragraphs = $('.' + subclass + ':visible');
            this.paginateData(page);
        }
    }

    this.showOtherPage = function (page) {
        this.currentPage = page;
        this.paginateData(page);
    }

    var renderControls = function (container, currentPage, numPages, resourceMessage, subClass) {
        var prevId = 0;
        var nxtId = 0;
        var itemCount = $('div.' + subClass + ':visible').find("[data-item='item']").length;
        if (numPages > 1) {
            var itemCount = $('div.' + subClass + '').find("[data-item='item']").length;
        }

        if (currentPage <= 1) {
            prevId = currentPage;
        }
        else {
            prevId = currentPage - 1;
        }
        if (currentPage == numPages) {
            nxtId = currentPage;
        }
        else {
            nxtId = currentPage + 1;
        }

        var pagingControls = '<ul class="pagination pull-left">';
        if (itemCount == 0) {
            pagingControls += '<li><a href="javascript:void(0);">&laquo;</a></li>';
        }
        else {
            pagingControls += '<li><a href="javascript:void(0);" onclick="pager.showOtherPage(' + prevId + '); return false;">&laquo;</a></li>';
        }

        for (var i = 1; i <= numPages; i++) {
            if (i != currentPage) {
                pagingControls += '<li><a href="javascript:void(0);" onclick="pager.showOtherPage(' + i + '); return false;">' + i + '</a></li>';

            } else {
                pagingControls += '<li class="active"><a href="javascript:void(0);">' + i + '</a></li>';
            }
        }
        if (itemCount == 0) {
            pagingControls += ' <li><a href="javascript:void(0);">&raquo;</a></li></ul>';
        }
        else {
            pagingControls += ' <li><a href="javascript:void(0);" onclick="pager.showOtherPage(' + nxtId + '); return false;">&raquo;</a></li></ul>';
        }

        if (itemCount == 0) {
            recordId = 0;
            totalRecordsOnPage = 0;
            totalRecords = 0;
        } else {
            itemCount = $('div.' + subClass + ':visible').find("[data-item='item']").length;
            recordId = (currentPage == 1) ? 1 : (((currentPage - 1) * recordsPerPage) + 1);
            totalRecordsOnPage = (currentPage == 1) ? itemCount : (recordId + itemCount - 1);
            if (itemCount == 1 && numPages == currentPage) {
                totalRecordsOnPage = totalRecordCount;
            }
            totalRecords = (totalRecordCount != undefined || totalRecordCount != "") ? parseInt(totalRecordCount) : 0;
        }

        var showPagingMessage = this.resourceMessage.replace('{0}', recordId);
        showPagingMessage = showPagingMessage.replace('{1}', totalRecordsOnPage);
        showPagingMessage = showPagingMessage.replace('{2}', totalRecords);

        if (totalRecordCount > recordsPerPage) {
            pagingControls += '<h5 class="pull-right">' + showPagingMessage + '</h5>';
            $(container).html(pagingControls);
        }
        else {
            $("[data-paging-control='paging_control']").hide();
        }

        if (pageName) {
            deleteConfirmation();
        }
        else { initialiseclick(); }
    }

    this.paginateData = function (page) {
        var html = '';
        var subclass = this.childClass;
        totalRecordCount = this.paragraphs.length;

        this.paragraphs.slice((page - 1) * this.itemsPerPage,
        (parseInt((page - 1) * this.itemsPerPage) + parseInt(this.itemsPerPage))).each(function () {
            html += '<div class="' + subclass + '">' + $(this).html() + '</div>';
        });
        if (this.childClass != "") {
            $(this.pagingContainerPath).html(html);
            renderControls(this.pagingControlsContainer, this.currentPage, this.numPages(), resourceMessage, subclass);
        }
        $('[data-tooltip="tooltip"]').tooltip()
    }
}
