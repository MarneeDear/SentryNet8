$(document).ready(function () {

    var pageStart = 1;
    var pageLength = 10;
    // Data Table Variables
    // ------------------------------------------------------------
    var paginationControlVisibility = "p";
    var lengthControlVisibility = "l";

    $("#newVendorRequestTable tbody").on("click", "tr", function () { clickTable(this, GetNewVendorRequetsURL); });


    // DataTables initialization
    $("#newVendorRequestTable")
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
                    width: "5%",
                    targets: 1,
                    data: 'formNumber',
                    searchable: true,
                },
                {
                    targets: 2,
                    className: 'hide',
                    data: 'preparedBy'
                },     
                {
                    width: "10%",
                    targets: 3,
                    data: 'vendorName',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true
                },
                {
                    width: "10%",
                    targets: 4,
                    data: 'vendorStreetAddress',
                    searchable: true,
                    //nowrap: true
                },
                {
                    width: "5%",
                    targets: 5,
                    data: 'vendorCity',
                },
                {
                    width: "10%",
                    targets: 6,
                    data: 'vendorState',
                    searchable: true,
                },

                {
                    width: "10%",
                    targets: 7,
                    data: 'vendorZip',
                    searchable: true,
                    //nowrap: true,
                    shrinkToFit: true
                },
                {
                    width: "10%",
                    targets: 8,
                    data: 'paymentType',
                    searchable: true,
                    //nowrap: true,
                    shrinkToFit: true
                },
                {
                    width: "10%",
                    targets: 9,
                    data: 'processingError',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    nowrap: true,
                    visable: true
                },
                {
                    type: 'date',
                    width: "10%",
                    targets: 10,
                    data: 'lastApprovedOnDate',
                    searchable: true,
                    //nowrap: true,
                    shrinkToFit: true
                }               

            ],
            "order": [
                [10, "desc"]
            ],
            buttons: [
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 10]
                    }

                },
                'pdfHtml5',
                'print'
            ],
        });

    function clickTable(elem) {
        var tableData = $(elem).children("td").map(function () {
            return $(this).text();
        }).get();

        var url = GetNewVendorRequetsURL;
        var id = tableData[0];
        //var itemId = tableData[1];

        window.location.href = url + `?id=${id}`;
    }
    if (typeof formNumber !== 'undefined') {
        GetSupportingDocuments(formNumber);
    }


});

function removeSpecialCharacters(elem) {
    $(elem).val(elem.value.replace(/[^\w\s]/gi, ''));
}

function capitalize(elem) {
    $(elem).val(elem.value.toUpperCase());
}


function UploadFileAjax(file, formNumber, supportingDocumentType) {
    var formData = new FormData();
    formData.append('SupportingDocument', file);
    formData.append('FormNumber', formNumber);
    formData.append('supportingDocumentType', supportingDocumentType);
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
        }
    });

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
    var attachmentType = $('#AttachmentType').val();
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
    else if (attachmentType == null || attachmentType == '') {
        $('#UploadStatusMessage').removeClass('d-none');
        $('#UploadStatusMessage').text('Please select a corresponding Attachment Type');
    }
    else if (!matchType) {
        $('#UploadStatusMessage').removeClass('d-none');
        $('#UploadStatusMessage').text('Please select a supported file type');
    }
    else {
        $('#UploadStatusMessage').addClass('d-none');
        UploadFileAjax(file, formNumber, attachmentType);
    }
}
function deleteFile(id, fileName) {
    var supportingDocId = $("#hidden_SupportingDoc_Id").val(id)
    var supportingDocFileName = $("#hidden_SupportingDoc_FileName").val(fileName)

    $('#DeleteFileName').text(`Delete ${fileName}`);
    $('#DeleteMessage').text(`Are you sure you want to delete ${fileName}`)
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

$('#ApproveRejectComments').keyup(function () {
    if ($(this).val())
        $('#btnReject').removeAttr('disabled');
    else
        $('#btnReject').attr('disabled', true);
})

function ToggleVisibility(show, elemType) {
    console.log("Toggling " + elemType);
    console.log(show);
    var spanId = "#" + elemType + "Span";
    var hideId = "#" + elemType + "Hide";
    var showId = "#" + elemType + "Show";
    if (show == 1) {
        $(spanId).attr("style", "visibility: visible");
        $(hideId).attr("style", "visibility: visible");
        $(showId).attr("style", "visibility: hidden");
    }
    else {
        $(spanId).attr("style", "visibility: hidden");
        $(hideId).attr("style", "visibility: hidden");
        $(showId).attr("style", "visibility: visible");
    }    
}

