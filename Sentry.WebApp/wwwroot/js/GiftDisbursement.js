$(document).ready(function () {
    $(window).keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            return false;
        }
    })


    var pageStart = 1;
    var pageLength = 10;
    //calcAllAmounts();
    CheckIsPending(false);
    // Data Table Variables
    // ------------------------------------------------------------
    var paginationControlVisibility = "p";
    var lengthControlVisibility = "l";

    $("#EtGiftDisbursementTable tbody").on("click", "tr", function () { clickTable(this, GetGiftDisbursementsURL); });

    $("#StGiftDisbursementTable tbody").on("click", "tr", function () { clickTable(this, GetGiftDisbursementsURL); });

    $("#EmGiftDisbursementTable tbody").on("click", "tr", function () { clickTable(this, GetGiftDisbursementsURL); });


    $('#ProcessTable tbody tr').click(function (e) {
        if ($(e.target).closest('input[type="checkbox"]').length > 0) {
            //Checkbox clicked
            SelectDisbursement(this);
        }
        else {
            clickTable(this, processDisbursementURL);
        }
    });
    $('table[id*="transaction_details_"]').each(function (projectIndex) {
        $(`#transaction_details_${projectIndex} input[id*= "__UaAccount"]`).each(function (itemIndex) {
            handleUaAccountNumber(projectIndex, itemIndex);
        })
    });

    // DataTables initialization
    $("#EtGiftDisbursementTable")
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
                    data: 'giftDisbursementId'
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
                    data: 'rejected',
                    //searchable: true,
                    //nowrap: true,
                    shrinkToFit: true,
                    render: function (data, type, row, meta) {
                        if (row.isPending == "True") {
                            return `<i role="img" class="fa fa-circle-pause text-warning" title="${row.pendingComments}"></i>`;
                        }

                        if (data == "True") {
                            return '<i role="img" class="fa fa-exclamation-triangle text-danger" title="Rejected"></i>';
                        }

                        return '<i role="img" class="fa fa-circle-check text-info" title="Awaiting Approval"></i>';
                    }
                },
                {
                    width: "10%",
                    targets: 4,
                    data: 'preparedByName',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true
                },
                {
                    width: "5%",
                    targets: 5,
                    data: 'formNumber',
                    searchable: true,
                    //nowrap: true
                },
                {
                    width: "40%",
                    targets: 6,
                    data: 'departmentCode',
                    render: $.fn.dataTable.render.ellipsis(50, true),
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true
                },
                {
                    width: "10%",
                    targets: 7,
                    data: 'total',
                    searchable: true,
                    render: $.fn.dataTable.render.number(',', '.', 3, '$')
                },
                {
                    width: "15%",
                    targets: 8,
                    data: 'reviewerNotes',
                    visible: false
                },
                
                {
                    type: 'date',
                    width: "10%",
                    targets: 9,
                    data: 'lastApprovedOnDate',
                    searchable: true,
                    //nowrap: true,
                    shrinkToFit: true
                }

            ],
            "order": [
                [8, "desc"]
            ],
            buttons: [
                'excel', 'pdfHtml5', 'print'
            ],
        });

    $("#StGiftDisbursementTable")
        /*.on('preXhr.dt', function (e, settings, data) {
            data.searchValue = data.search.value,
                data.sortColumn = data.columns[data.order[0].column].data,
                data.sortColumnDirection = data.order[0].dir
        })*/
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
                    data: 'giftDisbursementId'
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
                    targets: 3,
                    data: 'rejected',
                    //searchable: true,
                    //nowrap: true,
                    shrinkToFit: true,
                    render: function (data, type, row, meta) {
                        if (row.isPending == "True") {
                            return `<i role="img" class="fa fa-circle-pause text-warning" title="${row.pendingComments}"></i>`;
                        }

                        if (data == "True") {
                            return '<i role="img" class="fa fa-exclamation-triangle text-danger" title="Rejected"></i>';
                        }

                        return '<i role="img" class="fa fa-circle-check text-info" title="Awaiting Approval"></i>';
                    }
                },
                {
                    width: "25%",
                    targets: 4,
                    data: 'preparedByName',
                    render: $.fn.dataTable.render.ellipsis(40, true),
                    searchable: true,
                    shrinkToFit: false,
                    nowrap: true
                },
                {
                    width: "10%",
                    targets: 5,
                    data: 'formNumber',
                    searchable: true,
                },
                {
                    width: "45%",
                    targets: 6,
                    data: 'departmentCode',
                    render: $.fn.dataTable.render.ellipsis(40, true),
                    searchable: true,
                    shrinkToFit: false,
                    nowrap: true
                },
                {
                    width: "10%",
                    targets: 7,
                    data: 'Total',
                    searchable: true,
                },
                {
                    width: "15%",
                    targets: 8,
                    data: 'reviewerNotes',
                    visible: false
                },
                //{
                //    type: 'date',
                //    width: "15%",
                //    targets: 7,
                //    data: 'dateCreated',
                //    searchable: true,
                //    //nowrap: true,
                //    shrinkToFit: true
                //},               
                {
                    type: 'date',
                    width: "10%",
                    targets: 9,
                    data: 'lastApprovedOnDate',
                    searchable: true,
                    //nowrap: true,
                    shrinkToFit: true
                }

            ],
            "order": [
                [9, "desc"]
            ],
            buttons: [
                'excel', 'pdfHtml5', 'print'
            ],
        });

    $("#EmGiftDisbursementTable")
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
                    data: 'giftDisbursementId'
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
                    data: 'rejected',
                    //searchable: true,
                    //nowrap: true,
                    shrinkToFit: true,
                    render: function (data, type, row, meta) {
                        if (row.isPending == "True") {
                            return `<i role="img" class="fa fa-circle-pause text-warning" title="${row.pendingComments}"></i>`;
                        }

                        if (data == "True") {
                            return '<i role="img" class="fa fa-exclamation-triangle text-danger" title="Rejected"></i>';
                        }

                        return '<i role="img" class="fa fa-circle-check text-info" title="Awaiting Approval"></i>';
                    }
                },
                {
                    width: "10%",
                    targets: 4,
                    data: 'preparedByName',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true
                },
                {
                    width: "10%",
                    targets: 5,
                    data: 'formNumber',
                    searchable: true,
                    //nowrap: true
                },
                {
                    width: "15%",
                    targets: 6,
                    data: 'departmentCode',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true
                },
                {
                    width: "20%",
                    targets: 7,
                    data: 'payeeName',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true
                },
                {
                    width: "10%",
                    targets: 8,
                    data: 'has1099TotalValue',
                },
                {
                    width: "5%",
                    targets: 9,
                    data: 'payeePaymentType',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true
                },
                {
                    width: "5%",
                    targets: 10,
                    data: 'fund',
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true
                },            
                {
                    width: "15%",
                    targets: 11,
                    data: 'payeeSpecialInstructions',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true,
                    visable: true
                },
                {
                    width: "15%",
                    targets: 12,
                    data: 'payeeSpecialInstructions',
                    searchable: true,
                    shrinkToFit: true,
                    nowrap: true,
                    visible: false
                },
                {
                    width: "15%",
                    targets: 13,
                    data: 'reviewerNotes',
                    visible: false
                },
                {
                    width: "10%",
                    targets: 14,
                    data: 'total',
                    searchable: true,
                    render: $.fn.dataTable.render.number(',', '.', 3, '$')
                },
                //{
                //    type: 'date',
                //    width: "10%",
                //    targets: 10,
                //    data: 'dateCreated',
                //    searchable: true,
                //    //nowrap: true,
                //    shrinkToFit: true
                //},
                {
                    type: 'date',
                    width: "10%",
                    targets: 15,
                    data: 'lastApprovedOnDate',
                    searchable: true,
                    //nowrap: true,
                    shrinkToFit: true
                },
                

                {
                    width: "10%",
                    targets: 16,
                    data: 'total1099',
                    render: $.fn.dataTable.render.number(',', '.', 3, '$')
                },

            ],
            "order": [
                [12, "desc"]
            ],
            buttons: [
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [0, 1, 3, 4, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15, 16]
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
            "autoWidth": false,
            "bAutoWidth": false,

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
                    data: 'giftDisbursementId'
                },
                {
                    width: "5%",
                    targets: 1,
                    data: 'selectDisbursements',
                    //searchable: true,
                    //nowrap: true,
                    shrinkToFit: true
                },
                {
                    width: "10%",
                    targets: 2,
                    data: 'preparedByName',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    shrinkToFit: false,
                    nowrap: true
                },
                {
                    width: "5%",
                    targets: 3,
                    data: 'formNumber',
                    searchable: true

                },
                {
                    width: "15%",
                    targets: 4,
                    data: 'payeeName',
                    searchable: true
                    //shrinkToFit: true,
                //    nowrap: true
                },
                {
                    width: "10%",
                    targets: 5,
                    data: 'has1099TotalValue',
                },
                //{
                //    width: "20%",
                //    targets: 4,
                //    data: 'departmentCode',
                //    render: $.fn.dataTable.render.ellipsis(40, true),
                //    searchable: true,
                //    shrinkToFit: false,
                //    nowrap: true
                //},
                {
                    width: "10%",
                    targets: 6,
                    data: 'payeePaymentType',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    //shrinkToFit: true,
                    nowrap: true
                },
                {
                    width: "5%",
                    className: 'hide',
                    targets: 7,
                    data: 'fund'
                    //render: $.fn.dataTable.render.ellipsis(30, true),
                    //searchable: true,
                    //shrinkToFit: true,
                    //nowrap: true
                },
                {
                    width: "5%",
                    className: 'hide',
                    targets: 8,
                    data: 'rejected',
                },
                {
                    width: "20%",
                    targets: 9,
                    data: 'payeeSpecialInstructions',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    //shrinkToFit: true,
                    nowrap: true, 
                    visible: true
                },
                {
                    width: "20%",
                    targets: 10,
                    data: 'payeeSpecialInstructions',
                    nowrap: false,
                    visible: false
                },
                {
                    width: "15%",
                    targets: 11,
                    data: 'reviewerNotes',
                    visible: false
                },

                {
                    width: "10%",
                    targets: 12,
                    data: 'Total',
                    searchable: true
                },                
                {
                    width: "15%",
                    targets: 13,
                    data: 'processingError',
                    render: $.fn.dataTable.render.ellipsis(30, true),
                    searchable: true,
                    nowrap: true,
                    visable: true
                },
                {
                    width: "10%",
                    targets: 14,
                    data: 'processingError',
                    nowrap: false,
                    visible: false
                },
                //{
                //    type: 'date',
                //    width: "5%",
                //    targets: 7,
                //    data: 'dateCreated',
                //    searchable: true,
                //    //nowrap: true,
                //    shrinkToFit: true
                //},
                {
                    type: 'date',
                    width: "10%",
                    targets: 15,
                    data: 'lastApprovedOnDate',
                    searchable: true,
                    nowrap: true,
                //    shrinkToFit: true
                },
                {
                    width: "10%",
                    targets: 16,
                    data: 'total1099',
                    render: $.fn.dataTable.render.number(',', '.', 3, '$')
                },
               
               



            ],
           
            "order": [
                [15, "desc"]
            ],
            buttons: [
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [2, 3, 4, 5, 6, 7, 10, 11, 12, 14, 15, 16]
                    }                   

                },
                'pdfHtml5',
                'print'
                
            ],
        });
   
    function clickTable(elem, url) {
        var tableData = $(elem).children('td').map(function () {
            return $(this).text();
        }).get();

        var id = tableData[0];
        var goToUrl = `${url}?giftDisbursementId=${id}`;
        window.location.href = goToUrl;
    }
    if (typeof formNumber !== 'undefined') {
        GetSupportingDocuments(formNumber);
    }
});

$(window.document).on('blur', 'input[id*="Amount"]', function (e) {
    var amount = commaStringToAmount($(this).val());
    $(this).val(amountToCommaString(amount));
});
function handleUaAccountNumber(projectIndex, itemIndex) {
    var uaAccount = $(`#GiftDisbursementProjects_${projectIndex}__ProjectItems_${itemIndex}__UaAccount`).val();

    if (uaAccount.startsWith("2") || uaAccount.startsWith("7")) {
        
        $(`#GiftDisbursementProjects_${projectIndex}__ProjectItems_${itemIndex}__UaAccount`).css("color", "#000000");
        $(`#GiftDisbursementProjects_${projectIndex}__ProjectItems_${itemIndex}__UaAccount`).css("background-color", "#FCF3CF");
        
    }
    else {
        $(`#GiftDisbursementProjects_${projectIndex}__ProjectItems_${itemIndex}__UaAccount`).css("background-color", "#FFFFFF");
    }

}


function getColleges() {
    //Called when a fund account search is started.

    $.ajax({
        type: "GET",
        url: getCollegesURL,
        data: "",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {

            //Clear department and project dropdowns.
            var deptSelect = $('#DisbursementFrom_DepartmentCode');
            deptSelect.empty().append($('<option></option>').val('').text('Please select'));

            var projSelect = $('#DisbursementFrom_ProjectId');
            projSelect.empty().append($('<option></option>').val('').text('Please select'));

            var collSelect = $('#DisbursementFrom_CollegeCode');

            //Only  load the College list once per control.
            if (collSelect[0].options.length === 0) {
                collSelect.empty().append($('<option></option>').val('').text('Please select'));

                $.each(data, function (index, item) {
                    collSelect.append($('<option></option>').val(item.value).text(item.text));
                });
            }
            //Select the item with no value -- ("Please select");
            collSelect.val("");
        },
        error: function (request, error) {
            console.log(error);
            //    alert(error);
        }
    });
}

function getDepartments(collegeCode) {
    //Get the department data, based on the college.

    //var myData = { collegeCode: collegeId };
    //var jsonData = JSON.stringify(myData);

    $.ajax({
        type: "GET",
        url: getDepartmentsURL,
        data: {
            collegeCode : collegeCode
        },
        contentType: "application/json; charset=utf-8",
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {

            //Clear Departments and Projects.
            var deptSelect = $('#DisbursementFrom_DepartmentCode');
            deptSelect.empty().append($('<option></option>').val('').text('Please select'));

            //Load departments.
            $.each(data, function (index, item) {
                deptSelect.append($('<option></option>').val(item.value).text(item.text));
            });

            var projSelect = $('select[id$="ProjectId"]');
            projSelect.empty();
        },
        error: function (request, error) {
            console.log(error);
            //    alert(error);
        }
    });
}

function getProjects(departmentCode) {
    $.ajax({
        type: "GET",
        url: getProjectsURL,
        data: {
            departmentCode: departmentCode
        },
        contentType: "application/json; charset=utf-8",
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            //Clear project dropdown.
            var projSelect = $('select[id$="ProjectId"]');
            projSelect.empty().append($('<option></option>').val('').text('Please select'));

            var existingProjects = $('input[id$="__ProjectId"]');
            var existingProjectsValues = [];

            for (var i = 0; i < existingProjects.length; i++)
                existingProjectsValues.push(existingProjects[i].value);

            var filteredList = data.filter(dataItem => !existingProjectsValues.includes(dataItem.value));

            $.each(filteredList, function (i, item) {
                projSelect.append($('<option></option>').val(item.value).text(item.text));
            })
        },
        error: function (request, error) {
            console.log(error);
            //    alert(error);
        }
    });
}

$(".collapse.in").each(function () {
    $(this).prev(".card-header").find(".fa").addClass("fa-angle-down").removeClass("fa-angle-right");
});

// Toggle right and down arrow icon on show hide of collapse element
$(".collapse").on('show.bs.collapse', function () {
    $(this).prev(".card-header").find(".fa").removeClass("fa-angle-right").addClass("fa-angle-down");
}).on('hide.bs.collapse', function () {
    $(this).prev(".card-header").find(".fa").removeClass("fa-angle-down").addClass("fa-angle-right");
});

//function getProjectPurpose(index) {
//    ////Get the project purpose data, based on the department and projectId.
//    var departmentCode = $('#DisbursementFrom_DepartmentCode').val();
//    var projectId = $(`#GiftDisbursementItems_${index}__ProjectId`).val();

//    $.ajax({
//        type: "GET",
//        url: getProjectPurposeURL,
//        data: {
//            departmentCode: departmentCode,
//            projectId: projectId
//        },
//        contentType: "application/json; charset=utf-8",
//        headers:
//        {
//            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
//        },
//        success: function (data) {

            
//            $('#ProjectPurpose').text(data);
//        },
//        error: function (request, error) {
//            console.log(error);
//            //    alert(error);
//        }
//    });
//}
function checkUaAccountNumber(projectIndex, itemIndex) {
    var uaAccount = $(`#GiftDisbursementProjects_${projectIndex}__ProjectItems_${itemIndex}__UaAccount`).val();
    var uaAccountNum = uaAccount.toString().charAt(0);
    $('#KfsNumber').text(uaAccountNum);
    $('#kfsNumberModal').modal('show');    
}
function getProjectPurpose(projectId, departmentCode) {

    //var departmentCode = $('#DisbursementFrom_DepartmentCode').val();

    $.ajax({
        type: "GET",
        url: getProjectPurposeURL,
        data: {
            departmentCode: departmentCode,
            projectId: projectId
        },
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            $('#ProjectPurpose').text(data);
            $("#projectPurposeModal").modal('show');
        },
        error: function (request, error) {
            console.log(error);
            //    alert(error);
        }
    });
}

//function getProjectPurposeByIndex(projectId) {
//    var ProjectId = $(`#GiftDisbursementItems_${index}__ProjectId option:selected`).val();

//    if (ProjectId === undefined)
//        ProjectId = $(`#GiftDisbursementItems_${index}__ProjectId`).val();

//    if (ProjectId != undefined && ProjectId.length > 0) {
//        getProjectPurpose(ProjectId);
//    }
//}

$('#DisbursementFrom___ProjectId').change(function () {
    var ProjectId = $(this).val();

    if (ProjectId.length > 0) {
        $('#projectPurposeButton').attr('disabled', false);
    }
    else {
        $('#projectPurposeButton').attr('disabled', true);
    }
});

function getScholarshipProjectPurpose() {

    var ProjectId = $("#DisbursementFrom_ProjectId").val();

    if (ProjectId != undefined && ProjectId.length > 0) {
        getProjectPurpose(ProjectId);
    }
}

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

//$('#UploadFileBtn').click(function (e) {
//    e.preventDefault();

//    var file = $('#supportingDocument')[0].files[0];

//    var formData = new FormData();
//    formData.append('SupportingDocument', file);
//    formData.append('FormNumber', formNumber);

//    $.ajax({
//        type: "POST",
//        url: uploadSupportingDocumentURL,
//        data: formData,
//        contentType: false,
//        processData: false,
//        headers:
//        {
//            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
//        },
//        success: function (data) {
//            if (data.status == "Success") {
//                $('#supportingDocument').val(null);
//                $('#SupportingDocumentList').empty();
//                $.each(data.documents, function (i) {
//                    var listItem =
//                        `<li>
//                        <a
//                           href="${viewSupportingDocumentURL}?id=${data.documents[i].id}"
//                           target="_blank">
//                            Click to view ${data.documents[i].fileName}
//                        </a>
//                    </li>`;

//                    $('#SupportingDocumentList').append(listItem);
//                });
//            }
//            else {
//                $('#UploadStatusMessage').text(data.message);

//            }
           
//        },
//        error: function (request, error) {
//            console.log(error);
//            //    alert(error);
//        }
//    });
//})

function calcAllAmounts(table) {
    var total = 0;
    $(table).find('input[id*="__Amount"]').each(function () {
        total += commaStringToAmount($(this).val());
    });

    return total;
}

function setTotalProjectAmount(elem) {
    var table = $(elem).closest('table')[0];

    var projectBalance = $(table).find('[id*="projectBalance_"]').text();

    var total = calcAllAmounts(table);

    var formattedTotal = amountToCommaString(total);

    var totalProjectAmount = $(table).find('[id*="totalProjectAmount_"]');
    totalProjectAmount.text(`$${formattedTotal}`);

    var balance = parseFloat(projectBalance.replace(/[^0-9\.]+/g, "")) - total;
    if (balance < 0) {
        totalProjectAmount.addClass("text-danger");
    }
    else {
        totalProjectAmount.removeClass("text-danger");
    }

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

function AddDisbursement(elem) {
    var index = $(elem).attr("data-index");
    var department = $("#DisbursementFrom_DepartmentCode option:selected").val();
    var disbursementType = $("#Type").val();

    $.ajax({
        type: "GET",
        url: addItemURL,
        data: {
            index: index,
            department: department,
            disbursementType: disbursementType
        },
        success: function (data) {

            var currRow = $(elem).closest('tr');

            currRow.before(data);
        },
        error: function (request, error) {
            console.log(error);
        }
    });

    index = parseInt(index) + 1;

    $(elem).attr("data-index", index);
}

function RemoveDisbursement(elem) {
    var index = $(elem).attr("data-index");

    $(`#GiftDisbursementItems_${index}__Deleted`).prop('checked', true);

    $(`#gift_disbursement_transaction_detail_${index}`).hide();
    $(`#gift_disbursement_transaction_description_${index}`).hide();

    $(`#gift_disbursement_debit_account_number_${index}`).hide();
    $(`#gift_disbursement_debit_account_number_${index}`).hide();

}

function SelectDisbursement(elem) {

    //var table = $('#ProcessTable').DataTable();
    var selector = $(elem).children(".disbursementSelect").find("input");

    if ($(selector).prop('checked')) {
        $(elem).addClass('selected');
    }
    else {
        $(elem).removeClass('selected');
    }
    
}

function SelectAllDisbursements(elem) {
    var table = $('#ProcessTable').DataTable();
    var rows = table.rows({ search: 'applied', page: 'all' }).nodes();

    if ($(elem).prop('checked')) {
        $(rows.each(function (index, item) {

            $(index).addClass('selected');
            $(index).children(".disbursementSelect").find("input").prop('checked', true);
        }));
    }
    else {
        $(rows.each(function (index, item) {

            $(index).removeClass('selected');
            $(index).children(".disbursementSelect").find("input").prop('checked', false);
        }));
    }
}

function ShowBulkProcessingModal() {
    $('#processDisbursements tbody').empty();
    $("#bulkProcessingModal").modal('show');
    var selectedRows = $("#ProcessTable").DataTable().rows('.selected').data();
    $(selectedRows).each(function (index, item) {
        var newRow = `<tr><td class="id" style="display:none">${item.giftDisbursementId}</td><td>${item.formNumber}</td></tr>`;
        $("#processDisbursements").append(newRow);
    });
}

function BulkProcess() {
    var idList = [];
    var postDate = new Date($("#PostDate").val());
    console.log(postDate);
    $("#processDisbursements tbody tr").each(function () {
        var id = $(this).find("td.id").html();
        idList.push(id);
    });
    //alert(`${idList} | ${postDate}`);
    $("#pleaseWaitMessage").text("Please wait while the forms are processed.");
    $('#pleaseWaitModal').modal('show');
    $.ajax({
        type: "POST",
        url: bulkProcessUrl,
        data: JSON.stringify({
            DisbursementIds: idList,
            PostDate: postDate.toISOString().substring(0, 10)
        }),
        contentType: "application/json",
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            console.log("SUCCESS PROCESS");
            $("#pleaseWaitModal").modal('hide');
            $("#bulkProcessingResultsModal").modal("show");
            $("#resultsMessage").text("All forms processed successfully.");
        },
        error: function (request, error) {
            console.log(error);
            $("#pleaseWaitModal").modal('hide');
            $("#bulkProcessingResultsModal").modal("show");
            $("#resultsMessage").text("There were one or more errors processing the forms.");
            //alert("There were errors processing one or more disbursements");
            //location.reload();
        }
    });

}

$("#bulkProcessingResultsModal").on('hidden.bs.modal', function (e) {
    location.reload();
})

$('#Comments').keyup(function () {
    if ($(this).val())
        $('#btnReject').removeAttr('disabled');
    else
        $('#btnReject').attr('disabled', true);
})

function ClearDebitAccountDescription(projectCounter, itemCounter) {
    $(`#GiftDisbursementProjects_${projectCounter}__ProjectItems_${itemCounter}__DebitAccountDescription`).val(null);
}

function GetDebitAccountDescription(projectCounter, itemCounter) {
    ClearDebitAccountDescription(projectCounter, itemCounter);

    var accountNumber = $(`#GiftDisbursementProjects_${projectCounter}__ProjectItems_${itemCounter}__DebitAccountNumber`).val();
    var projectId = $(`#GiftDisbursementProjects_${projectCounter}__ProjectId`).val();
    var objectCode = $(`#GiftDisbursementProjects_${projectCounter}__ProjectItems_${itemCounter}__ObjectCode`).val();

    if (accountNumber) {
        $.ajax({
            type: "GET",
            url: getProjectAccountDetailsURL,
            data: {
                accountNumber: accountNumber,
                projectId: projectId,
                objectCode: objectCode
            },
            headers:
            {
                "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (data) {
                $(`#GiftDisbursementProjects_${projectCounter}__ProjectItems_${itemCounter}__DebitAccountDescription`).val(data.accountDescription);
            },
            error: function (request, error) {
                console.log(error);
                //    alert(error);
            }
        });
    }
}


function CheckIsPending(manualEdit) {
    
    var checked = $('#IsPending').is(':checked');
    if(checked) {
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

function OpenAddNewProjectModal() {
    var departmentCode = $('#DisbursementFrom_DepartmentCode').val();
    getProjects(departmentCode);
    $('#AddProjectModal').modal('show');
};

function AddProjectItem(elem, projectIndex) {
    var itemIndex = $(elem).attr("data-index");
    var disbursementType = $("#Type").val();   

    $.ajax({
        type: "GET",
        url: addItemURL,
        data: {
            projectIndex: projectIndex,
            itemIndex: itemIndex,
            disbursementType: disbursementType
        },
        success: function (data) {
            var currRow = $(elem).closest('table').find('tbody tr:last')

            currRow.after(data);
        },
        error: function (request, error) {
            console.log(error);
        }
    });

    itemIndex = parseInt(itemIndex) + 1;

    $(elem).attr("data-index", itemIndex);
}

function AddProject(elem) {
    var projectId = $('#ProjectId').val();

    if (projectId.length > 0) {
        AddProjectAjax(elem)
    }
}

function AddProjectAjax(elem) {
    var index = $(elem).attr("data-index");;
    var disbursementType = $("#Type").val(); 
    var departmentCode = $('#DisbursementFrom_DepartmentCode').val();
    var projectId = $('#ProjectId').val();

    $.ajax({
        type: "GET",
        url: addProjectURL,
        data: {
            index: index,
            disbursementType: disbursementType,
            departmentCode: departmentCode,
            projectId: projectId
        },
        success: function (data) {
            var currRow = $(`.projectContainer:last`);

            currRow.after(data);

            index = parseInt(index) + 1;

            $(elem).attr("data-index", index);
        },
        error: function (request, error) {
            console.log(error);
        }
    });
}

function RemoveDisbursement(elem) {
    var projectIndex = $(elem).attr("data-projectIndex");
    var projectItemIndex = $(elem).attr("data-projectItemIndex");
    $(`#GiftDisbursementProjects_${projectIndex}__ProjectItems_${projectItemIndex}__Deleted`).val('True');
    $(`#GiftDisbursementProjects_${projectIndex}__ProjectItems_${projectItemIndex}__Amount`).val(0);
    $(`#project_${projectIndex}_item_${projectItemIndex}`).hide();

    var projectItemDescriptionElem = $(`#project_${projectIndex}_item_${projectItemIndex}_description`);
    $(projectItemDescriptionElem).hide();
    $(projectItemDescriptionElem).removeAttr('required');

    setTotalProjectAmount(elem);
}