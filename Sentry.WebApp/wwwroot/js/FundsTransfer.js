$(document).ready(function () {

    var pageStart = 1;
    var pageLength = 10;
    CheckIsPending(false);
    isGift();
    // Data Table Variables
    // ------------------------------------------------------------
    var paginationControlVisibility = "p";
    var lengthControlVisibility = "l";

    $("#fundsTransferTable tbody").on("click", "tr", function () { clickTable(this, GetFundsTransferURL); });
    $("#ProcessTable tbody").on("click", "tr", function () { clickTable(this, GetFundsTransferReviewURL); });
    $('#ProcessTable tbody tr').click(function (e) {
        if ($(e.target).closest('input[type="checkbox"]').length > 0) {
            //Checkbox clicked
            SelectFundsTransfer(this);
        }
        else {
            clickTable(this, processFundsTransferURL);
        }
    });

    // DataTables initialization
    $("#fundsTransferTable")
        //.on('preXhr.dt', function (e, settings, data) {
        //    data.searchValue = data.search.value,
        //        data.sortColumn = data.columns[data.order[0].column].data,
        //        data.sortColumnDirection = data.order[0].dir
        //})
        .DataTable({
            pageLength: pageLength,
            "lengthMenu": [10, 25, 50, 75, 100],
            // dom() more info: https://datatables.net/reference/option/dom ::
            // B = Buttons
            // l = page (l)ength changing input control
            // f = (f)iltering input
            // r = p(r)ocessing display element
            // t = (t)able
            // i = table (i)nfo summary - aka: showing x of x records
            // p = (p)agination control
            dom: 'B' + lengthControlVisibility + 'frti' + paginationControlVisibility,
            cache: true,
            paging: true,
            //processing: true,
            //serverSide: true,

            language: {
                //processing: '<div id="dt-processing">Processing... <div class="spinner"></div></div>',
                //processing: '<i class="fa fa-spin fa-asterisk fa-fw"></i> <span class=""> Loading...</span>',
                search: 'Filter',
                emptyTable: "No records found."
            },
            // Ellipsis: https://datatables.net/plug-ins/dataRender/ellipsis more info: https://cdn.datatables.net/plug-ins/1.10.19/dataRender/ellipsis.js ::
            columnDefs: [

                {
                    targets: 0,
                    className: 'hide',
                    data: 'id'
                },
                {
                    targets: 1,
                    className: 'hide',
                    data: 'isPending'
                },
                {
                    targets: 2,
                    className: 'hide',
                    data: 'pendingComments'
                },                
                {
                    width: "5%",
                    targets: 3,
                    data: 'status',
                    //searchable: true,
                    //nowrap: true,
                    shrinkToFit: true,
                    render: function (data, type, row, meta) {
                        if (row.isPending == "True") {
                            return `<i role="img" class="fa fa-circle-pause text-warning" title="${row.pendingComments}"></i>`;
                        }

                        else if (row.status == "Rejected") {
                            return '<i role="img" class="fa fa-exclamation-triangle text-danger" title="Rejected"></i>';
                        }

                        return '<i role="img" class="fa fa-circle-check text-info" title="Awaiting Approval"></i>';
                    }
                },
                {
                    targets: 4,
                    data: 'formNumber',
                    searchable: true,
                    width: "10%"
                },
                {
                    width: "15%",
                    targets: 5,
                    data: 'preparedByName',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true
                },
                {
                    targets: 6,
                    data: 'transferFrom',
                    searchable: true,
                },
                {
                    targets: 7,
                    data: 'transferTo',
                    searchable: true,
                    render: $.fn.dataTable.render.ellipsis(42, true),
                },
                {
                    targets: 8,
                    className: 'hide',
                    data: 'transferTo'
                },       
                {
                    targets: 9,
                    data: 'total',
                    searchable: true,
                },
                {
                    type: 'date',
                    targets: 10,
                    data: 'approvedOn',
                    searchable: true,
                    width: "15%"
                }
            ],
            "order": [
                [10, "asc"]
            ],
            buttons: [
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [4,5,6,8,9,10]
                    }

                },
                'pdfHtml5',
                'print'
            ],
        });

    $("#ProcessTable")
        //.on('preXhr.dt', function (e, settings, data) {
        //    data.searchValue = data.search.value,
        //        data.sortColumn = data.columns[data.order[0].column].data,
        //        data.sortColumnDirection = data.order[0].dir
        //})
        .DataTable({
            pageLength: pageLength,
            "lengthMenu": [10, 25, 50, 75, 100],
            // dom() more info: https://datatables.net/reference/option/dom ::
            // B = Buttons
            // l = page (l)ength changing input control
            // f = (f)iltering input
            // r = p(r)ocessing display element
            // t = (t)able
            // i = table (i)nfo summary - aka: showing x of x records
            // p = (p)agination control
            dom: 'B' + lengthControlVisibility + 'frti' + paginationControlVisibility,
            cache: true,
            paging: true,
            //processing: true,
            //serverSide: true,

            language: {
                //processing: '<div id="dt-processing">Processing... <div class="spinner"></div></div>',
                //processing: '<i class="fa fa-spin fa-asterisk fa-fw"></i> <span class=""> Loading...</span>',
                search: 'Filter',
                emptyTable: "No records found."
            },
            // Ellipsis: https://datatables.net/plug-ins/dataRender/ellipsis more info: https://cdn.datatables.net/plug-ins/1.10.19/dataRender/ellipsis.js ::
            columnDefs: [

                {
                    targets: 0,
                    className: 'hide',
                    data: 'id'
                },
                {
                    targets: 1,
                    data: 'routingType',
                    searchable: true
                },
                {
                    targets: 2,
                    data: 'preparedByName',
                    searchable: true
                },
                {
                    targets: 3,
                    data: 'formNumber',
                    searchable: true,
                },
                {
                    targets: 4,
                    data: 'total',
                    searchable: true,
                },
                {
                    targets: 5,
                    data: 'processingError',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    nowrap: true,
                    visable: true
                },
                {
                    targets: 6,
                    data: 'processingError',
                    nowrap: false,
                    visible: false
                },
                {
                    type: 'date',
                    targets: 7,
                    data: 'lastApprovedOnDate',
                    searchable: true,
                    nowrap: true
                }
            ],
            "order": [
                [7, "asc"]
            ],
            buttons: [
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [1, 2, 3, 4, 6, 7]
                    }

                },
                'pdfHtml5',
                'print'
            ],
        });

    function clickTable(elem, url) {
        var tableData = $(elem).children("td").map(function () {
            return $(this).text();
        }).get();

        var id = tableData[0];
        //var itemId = tableData[1];

        window.location.href = url + `?fundsTransferId=${id}`;
    }
    if (typeof formNumber !== 'undefined') {
        GetSupportingDocuments(formNumber);
    }


});

function GetSupportingDocuments(formNumber) {
    $.ajax({
        type: "GET",
        url: getSupportingDocumentsURL,
        data: {
            formNumber: formNumber
        },
        contentType: "application/json",
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            $('#SupportingDocumentsSection').html(data);
        },
        error: function (request, error) {
            console.log(error);
        }
    });
}

function CheckIsPending(manualEdit) {

    var checked = $('#IsPending').is(':checked');
    if (checked) {
        var text = $('#PendingComments').val();
        $('#PendingComments').removeAttr('disabled');
        $('#btnReject').attr('disabled', true);
        $('#btnApprove').attr('disabled', true);
        $('#PendingCommentsText').text(text);
        if (!manualEdit) {
            $('#PendingCommentsModal').modal("show");
        }
    }
    else {
        $('#PendingComments').val("");
        $('#PendingComments').attr('disabled', true);
        $('#btnApprove').removeAttr('disabled');
        $('#PendingCommentsModal').modal("hide");
    }

}

function GetSupportingDocuments(formNumber) {
    $.ajax({
        type: "GET",
        url: getSupportingDocumentsURL,
        data: {
            formNumber: formNumber
        },
        contentType: "application/json",
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            $('#SupportingDocumentsSection').html(data);
        },
        error: function (request, error) {
            console.log(error);
        }
    });
}

function amountToCommaString(num) {
    //Convert amount like "2000" to "2,000.00" for display.

    if (num === "") num = 0;
    var str = num.toLocaleString(undefined, {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
    return str;
}

function commaStringToAmount(str) {
    //Convert amount like "2,000.00" to "2000.00" for calculations.

    if (str === "") str = "0.00";
    if (str !== undefined && str !== null)
        str = str.replace(/\,/g, '');
    var num = parseFloat(str, 10);
    return num;
}

function AddItem(elem) {
    var index = $(elem).attr("data-index");

    $.ajax({
        type: "GET",
        url: addItemURL,
        data: {
            itemIndex: index
        },
        success: function (data) {

            var currBody = $(elem).closest('table').find('tbody');

            var currRow = currBody.find('tr:last-child');

            currRow.after(data);
        },
        error: function (request, error) {
            console.log(error);
        }
    });

    index = parseInt(index) + 1;

    $(elem).attr("data-index", index);
}

function RemoveTransferTo(elem) {
    var index = $(elem).attr("data-index");

    $(`#Items_${index}__Deleted`).val('True');

    $(`#TransferToItemDetails_${index}`).hide();

    setTotalProjectToAmount(elem);
}

$('#Comments').keyup(function () {
    if ($(this).val())
        $('#btnReject').removeAttr('disabled');
    else
        $('#btnReject').attr('disabled', true);
})

function setFromProjectDescription(elem) {
    $('#ProjectDescription').val(null)
    var projectId = $(elem).val();

    $.ajax({
        type: "GET",
        url: getProjectDescriptionByProjectIdURL,
        data: {
            projectId: projectId
        },
        success: function (data) {
            $('#ProjectDescription').val(data);
        },
        error: function (request, error) {
            console.log(error);
        }
    });

}

function setToProjectDescription(elem, index) {
    $(`#Items_${index}__ProjectDescription`).val(null);
    var projectId = $(elem).val();

    $.ajax({
        type: "GET",
        url: getProjectDescriptionByProjectIdURL,
        data: {
            projectId: projectId
        },
        success: function (data) {
            $(`#Items_${index}__ProjectDescription`).val(data);
        },
        error: function (request, error) {
            console.log(error);
        }
    });
}

function projectLookup(projectId) {
    $.ajax({
        type: "GET",
        url: getProjectDescriptionByProjectIdURL,
        data: {
            projectId: projectId
        },
        success: function (data) {
            return data;
        },
        error: function (request, error) {
            console.log(error);
        }
    });
}

function SelectFundsTransfer(elem) {

    var selector = $(elem).children(".fundsTransferSelect").find("input");

    if ($(selector).prop('checked')) {
        $(elem).addClass('selected');
    }
    else {
        $(elem).removeClass('selected');
    }

}

function SelectAllFundsTransfers(elem) {
    var table = $('#ProcessTable').DataTable();
    var rows = table.rows({ search: 'applied', page: 'all' }).nodes();

    if ($(elem).prop('checked')) {
        $(rows.each(function (index, item) {

            $(index).addClass('selected');
            $(index).children(".fundsTransferSelect").find("input").prop('checked', true);
        }));
    }
    else {
        $(rows.each(function (index, item) {

            $(index).removeClass('selected');
            $(index).children(".fundsTransferSelect").find("input").prop('checked', false);
        }));
    }
}


function calcAllAmounts(table) {
    var total = 0;
    $(table).find('input[id*="__Amount"]').each(function (i) {
        if (!$(`#TransferToItemDetails_${i}`).is(':hidden')) { 
            total += commaStringToAmount($(this).val());
        }
    });

    return total;
}

function setTotalProjectToAmount(elem) {
    var table = $(elem).closest('table')[0];

    var projectBalance = $(table).find('[id*="projectBalance"]').text();

    var total = calcAllAmounts(table);

    var formattedTotal = amountToCommaString(total);

    var totalProjectAmount = $(table).find('[id="totalProjectToAmount"]');
    totalProjectAmount.text(`$${formattedTotal}`);

    var balance = parseFloat(projectBalance.replace(/[^0-9\.]+/g, "")) - total;
    if (balance < 0) {
        totalProjectAmount.addClass("text-danger");
    }
    else {
        totalProjectAmount.removeClass("text-danger");
    }

}

$(window.document).on('blur', 'input[id*="Amount"]', function (e) {
    var amount = commaStringToAmount($(this).val());
    $(this).val(amountToCommaString(amount));
});

function UploadFileAjax(file, formNumber) {
    var formData = new FormData();
    formData.append('SupportingDocument', file);
    formData.append('FormNumber', formNumber);
    $('#pleaseWaitSupportingDocumentsModal').modal('show');

    $.ajax({
        type: "POST",
        url: uploadSupportingDocumentURL,
        data: formData,
        contentType: false,
        processData: false,
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            $('#file_upload').val(null);
            $('#SupportingDocumentsSection').html(data);
            $('#pleaseWaitSupportingDocumentsModal').modal('hide');

        },
        error: function (request, error) {
            console.log(error);
            //    alert(error);
        }
    });

}

function checkValidFileTypes(fileName, validFileTypes) {
    var matchType = false;
    var ignoreCaseFileName = fileName.toLowerCase();
    for (let i = 0; i < validFileTypes.length; i++) {
        if (!ignoreCaseFileName.includes(validFileTypes[i])) {
            matchType = false;
        }
        else {
            matchType = true;
            break;
        }
    }
    return matchType;
}

function uploadFiles() {
    var files = document.getElementById('file_upload').files;

    var file = files[0];
    var validFileTypes = [];
    $('input[id*="ValidFileTypes_"]').each(function () {
        validFileTypes.push($(this).val());
    })
    if (file != null || file != undefined) {
        var matchType = checkValidFileTypes(file.name, validFileTypes);
    }


    if (file == null || file == undefined) {
        $('#UploadStatusMessage').removeClass('d-none');
        $('#UploadStatusMessage').text('Please upload a file to continue');
    }
    else if (formNumber == null || formNumber == '') {
        $('#UploadStatusMessage').removeClass('d-none');
        $('#UploadStatusMessage').text('A valid Form Number is required');
    }
    else if (!matchType) {
        $('#UploadStatusMessage').removeClass('d-none');
        $('#UploadStatusMessage').text('Please select a supported file type');
    }
    else {
        $('#UploadStatusMessage').addClass('d-none');
        UploadFileAjax(file, formNumber);
    }
}
function deleteFile(id, fileName) {
    var supportingDocId = $("#hidden_SupportingDoc_Id").val(id);
    var supportingDocFileName = $("#hidden_SupportingDoc_FileName").val(fileName);

    $('#DeleteFileName').text(`Delete ${fileName}`);
    $('#DeleteMessage').text(`Are you sure you want to delete ${fileName}`);
    $("#deleteSupportingDocumentsModal").modal('show');
}
function DeleteFileAjax() {
    let id = $("#hidden_SupportingDoc_Id").val();
    var formNumber = $('#FormNumber').val();

    var formData = new FormData();
    formData.append('id', id);
    formData.append('formNumber', formNumber);
    $("#deleteSupportingDocumentsModal").modal('hide');
    $("#pleaseWaitSupportingDocumentsDeleteModal").modal('show');

    $.ajax({
        type: "DELETE",
        url: deleteSupportingDocumentURL,
        data: formData,
        contentType: false,
        processData: false,
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {

            $('#SupportingDocumentsSection').html(data);
            $("#pleaseWaitSupportingDocumentsDeleteModal").modal('hide');

        },
        error: function (request, error) {

            $("#pleaseWaitSupportingDocumentsDeleteModal").modal('hide');
            alert("There was an error deleting your document. Please try again.");

        }
    });
}

$('#editForm').submit(function (e) {
    if ($(this).valid() || e.originalEvent.submitter.hasAttribute('formnovalidate')) { 
        DisableSubmitButtons(this);
    }
});

//Checks to see if it is a gift based on routing type removes required field for account number if a gift otherwise puts back the required
$('#TransferRoutingType').on("change", function () {
    isGift();
});
function isGift() {
    var routingType = $('#TransferRoutingType').val()
    if (routingType == 4) {
        $('#accountNumber').remove();
    }
    else {
        //this is so that it does not add another star when the page fails validation remove it and re add it
        $('#accountNumber').remove();
        $('#accountNumberHeader').append("<span>*</span>");
        $('#accountNumberHeader span').attr("id", "accountNumber");
        $('#accountNumberHeader span').addClass('required');
    }
}

function DisableSubmitButtons(form) {
    var submitButtons = $(form).find(':submit');

    submitButtons.prop('disabled', true);

    var afterSubmission = function () {
        submitButtons.prop('disabled', true);
    };
    window.setTimeout(afterSubmission, 50);
}

function getProjectPurpose() {

    var projectId = $('#ProjectId').val();

    $.ajax({
        type: "GET",
        url: getProjectPurposeURL,
        data: {
            projectId: projectId
        },
        success: function (data) {
            $('#ProjectPurpose').text(data);
            $("#projectPurposeModal").modal('show');
        },
        error: function (request, error) {
            console.log(error);
        }
    });
}
function getToProjectPurpose(index) {

    var projectId = $(`#Items_${index}__ProjectId`).val();

    $.ajax({
        type: "GET",
        url: getProjectPurposeURL,
        data: {
            projectId: projectId
        },
        success: function (data) {
            $('#ProjectPurpose').text(data);
            $("#projectPurposeModal").modal('show');
        },
        error: function (request, error) {
            console.log(error);
        }
    });
}

