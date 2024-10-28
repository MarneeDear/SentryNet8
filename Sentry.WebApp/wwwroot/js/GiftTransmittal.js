var emptyGuid = "00000000-0000-0000-0000-000000000000";

$(document).ready(function () {

    $(window).keydown(function (e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            return false;
        }
    })

    $('input[id$="FundAccount"]').each(function (index) {
        var fundAccount = $(this).val();

        //When editing, and the fund codes are pasted in, add the descriptions.  See if the field needs to be disabled for "IsNewFund".
        if (fundAccount !== "") {


            getFundDesc(fundAccount, index, Organization);
        }
        if ($(this).closest('input[id*="isNewFund"]').length > 0 && $(this).closest('input[id*="isNewFund"]').val().toUpperCase() === "TRUE") {
            $(this).parent().attr("disabled", "disabled");
            $(this).parent().find('input').attr("disabled", "disabled");
            $('#newFundMessageDiv_' + index).css('display', '');
        }
    });


    if (preparedByEmployeeId != "" && preparedByEmployeeId != undefined) {
        getPreparerDetails();
    }
    
    var pageStart = 1;
    var pageLength = 10;
    $('input[id*="IsUdfExempt"]').each(function (index) {
        handleUdf($('#Distributions_' + index + '__IsUdfExempt')[0].checked, index, false);
    });

    $('select[id*="UdfFeeExemptionId"]').each(function (index) {
        var UdfFeeExemptionText = $(`#Distributions_${index}__UdfFeeExemptionId option:selected`).text();
        handleUdfOther(UdfFeeExemptionText, index);
    });
    // Data Table Variables
    // ------------------------------------------------------------
    var giftTransmittalTable = $('#giftTransmittalTable');
    var paginationControlVisibility = "p";
    var lengthControlVisibility = "l";
    // DataTables initialization
    $(giftTransmittalTable)
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
                    data: 'itemId'
                },
                {
                    targets: 2,
                    className: 'hide',
                    data: 'waitingOnResponseFromBursar'
                },
                {
                    targets: 3,
                    className: 'hide',
                    data: 'waitingOnResponseFromPreparer'
                },
                {
                    targets: 4,
                    className: 'hide',
                    data: 'secondaryApproverStatus'
                },
                {
                    targets: 5,
                    className: 'hide',
                    data: 'status'
                },
                
                {
                    width: "5%",
                    targets: 6,
                    data: 'waitingOnResponseFromBursar',
                    shrinkToFit: true,
                    render: function (data, type, row, meta) {
                        if (row.secondaryApproverStatus == "Rejected") {
                            return '<i role="img" class="fa fa-exclamation-triangle text-danger" title="Rejected"></i>';
                        }
                        else if (row.waitingOnResponseFromBursar == "True") {
                            return '<i role="img" class="fa fa-circle-pause text-warning" title="Waiting on Response from Bursar"></i>';
                        }
                        else if (row.waitingOnResponseFromPreparer == "True") {
                            return '<i role="img" class="fa fa-circle-pause text-warning" title="Waiting on Response from Preparer"></i>';
                        }
                        else if (row.hasProcessingError == "Yes") {
                            return '<i role="img" class="fa fa-exclamation-triangle text-danger" title="Error"></i>';

                        }                        
                        else {
                            if (row.status == "Printed") {
                                return `<i role="img" class="fa fa-circle-check text-success" title="${row.status}"></i>`;
                            }
                            else if (row.status == "Rejected") {
                                return `<i role="img" class="fa fa-circle-minus text-warning" title="Rejected"></i>`;
                            }
                            else {
                                return `<i role="img" class="fa fa-circle-check text-info" title="Open"></i>`;
                            }
                        }
                    }
                },
                
                {
                    width: "10%",
                    targets: 7,
                    data: 'formNumber'
                },                
                {
                    width: "10%",
                    targets: 8,
                    data: 'batchType',
                    searchable: true
                },
                {
                    width: "27.5%",
                    targets: 9,
                    data: 'preparedBy',
                    render: $.fn.dataTable.render.ellipsis(40, true),
                    searchable: true
                },
                {
                    width: "27.5%",
                    targets: 10,
                    data: 'constituent',
                    render: $.fn.dataTable.render.ellipsis(40, true),
                    searchable: true
                },
                {
                    width: "10%",
                    targets: 11,
                    data: 'total'
                },
                {
                    width: "10%",
                    targets: 12,                    
                    data: 'hasProcessingError',
                    searchable: true
                },
                {
                    type: 'date',
                    width: "10%",
                    targets: 13,
                    data: 'dateCreated',
                    searchable: true,
                    nowrap: true
                }

            ],
            createdRow: function (row, data, dataIndex) {
                if (data.waitingOnResponseFromBursar == 'True'
                    || data.waitingOnResponseFromPreparer == 'True') {
                    $(row).css("background-color", "#fcf8e3");
                }
            },
            "order": [
                [13, "desc"]
            ],
            buttons: [
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [6, 7, 8, 11, 12, 13]
                    }

                }, 'pdfHtml5', 'print'
            ],
        });
    
    $("#giftTransmittalTable tbody, #giftTransmittalStatusTable tbody").on("click", "tr", function () { clickTable(this); });


    setupRecognitionCreditFields();
    // Data Table Variables
    // ------------------------------------------------------------
    var initializedGiftTransmittalTable = $('#initializedGiftTransmittalTable');
    var paginationControlVisibility = "p";
    var lengthControlVisibility = "l";
    // DataTables initialization
    $(initializedGiftTransmittalTable)
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
                    data: 'itemId'
                },
                //{
                //    targets: 2,
                //    className: 'hide',
                //    data: 'organization'
                //},


                {
                    width: "10%",
                    targets: 2,
                    data: 'formNumber'
                },
                //{
                //    width: "10%",
                //    targets: 3,
                //    data: 'formState',
                //    searchable: true
                //},
                {
                    width: "15%",
                    targets: 3,
                    data: 'batchType',
                    searchable: true
                },
                {
                    width: "27.5%",
                    targets: 4,
                    data: 'preparedBy',
                    render: $.fn.dataTable.render.ellipsis(40, true),
                    searchable: true
                },
                {
                    width: "27.5%",
                    targets: 5,
                    data: 'constituent',
                    render: $.fn.dataTable.render.ellipsis(40, true),
                    searchable: true
                },
                {
                    width: "10%",
                    targets: 6,
                    data: 'total'
                },
                {
                    type: 'date',
                    width: "10%",
                    targets: 7,
                    data: 'dateCreated',
                    searchable: true,
                    nowrap: true
                }

            ],
            "order": [
                [7, "desc"]
            ],
            buttons: [
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [2, 3, 4, 5, 6, 7]
                    }

                }, 'pdfHtml5', 'print'
            ],
        });

    $("#initializedGiftTransmittalTable tbody").on("click", "tr", function () { clickInitializedTable(this); });

    // Data Table Variables
    // ------------------------------------------------------------
    var giftTransmittalListTable = $('#giftTransmittalStatusTable');
    var paginationControlVisibility = "p";
    var lengthControlVisibility = "l";
    // DataTables initialization
    $(giftTransmittalListTable)
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
                    data: 'itemId'
                },
                {
                    width: "15%",
                    targets: 2,
                    data: 'formNumber'
                },
                {
                    width: "10%",
                    targets: 3,
                    data: 'batchType',
                    searchable: true
                },
                {
                    width: "27.5%",
                    targets: 4,
                    data: 'preparedBy',
                    render: $.fn.dataTable.render.ellipsis(40, true),
                    searchable: true
                },
                {
                    width: "27.5%",
                    targets: 5,
                    data: 'constituent',
                    render: $.fn.dataTable.render.ellipsis(40, true),
                    searchable: true
                },
                {
                    width: "10%",
                    targets: 6,
                    data: 'total'
                },
                {
                    type: 'date',
                    width: "10%",
                    targets: 7,
                    data: 'dateCreated',
                    searchable: true,
                    nowrap: true
                }

            ],
            "order": [
                [7, "desc"]
            ],
            buttons: [
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: [2, 3, 4, 5, 6, 7]
                    }

                }, 'pdfHtml5', 'print'
            ],
        });

    if (typeof formNumber !== 'undefined') {
        GetSupportingDocuments(formNumber);
    }

    EnableDisableApproveButton();
});

function clickTable(elem) {
    var tableData = $(elem).children("td").map(function () {
        return $(this).text();
    }).get();

    var url = GetGiftTransmittalsURL;
    var id = tableData[0];
    var itemId = tableData[1];

    window.location.href = url + `?giftTransmittalId=${id}&giftTransmittalItemId=${itemId}`;
}
function clickInitializedTable(elem) {
    var tableData = $(elem).children("td").map(function () {
        return $(this).text();
    }).get();

    var url = createGiftTransmittalURL;
    var id = tableData[0];
    //var organization = Organization;

    window.location.href = url + `?giftTransmittalId=${id}`;
}
checkOrgRadio();

function handleBatchType(typeValue) {
    if (typeValue === "Other") {
        //Display Other field.
        $('.batchTypeOther').parent().removeClass('d-none');
    }
    else {
        //Clear and hide Other field.
        $('#batchTypeOtherDesc').val("");
        $('input[id="Transaction_BatchTypeOtherDesc"]').val("");
        $('.batchTypeOther').parent().addClass('d-none');
    }

    batchTypeCode = $('#Transaction_BatchTypeCode').val();

    $('#distTable tbody tr').each(function (index) {
        calcAllAmounts(index, true);
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
//function showDesignationSearch() {
//    getInitializedColleges()
//    $('#mainDataRow').find('#selectUAProject').slideDown("slow", function () { });

//}
function hideDesignationSearch() {
    $('#mainDataRow').find('#selectUAProject').slideUp("slow", function () { });
}
//function getInitializedColleges() {
//    //Called when a fund account search is started.

//    $.ajax({
//        type: "GET",
//        url: getCollegesURL,
//        //data: { collegeCode: collegeCode },
//        headers:
//        {
//            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
//        },
//        success: function (data) {

//            //Clear department and project dropdowns.
//            var deptSelect = $('#Dept_code');
//            deptSelect.empty().append($('<option></option>').val('').text('Please select'));

//            var projSelect = $('#ProjectID');
//            projSelect.empty().append($('<option></option>').val('').text('Please select'));

//            var collSelect = $('#College_code');

//            //Only  load the College list once per control.
//            if (collSelect[0].options.length === 0) {
//                collSelect.empty().append($('<option></option>').val('').text('Please select'));

//                $.each(data, function (index, item) {
//                    collSelect.append($('<option></option>').val(item.value).text(item.text));
//                });
//            }
//            //Select the item with no value -- ("Please select");
//            collSelect.val("");
//        },
//        error: function (request, error) {
//            console.log(error);
//            alert(error);
//        }
//    });
//}
//function getInitializedDepartments(collegeCode) {
//    //Get the department data, based on the college.

//    $.ajax({
//        type: "GET",
//        url: getDepartmentsURL,
//        data: { collegeCode: collegeCode },
//        headers:
//        {
//            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
//        },
//        success: function (data) {

//            //Clear Departments and Projects.
//            var deptSelect = $('#Dept_code');
//            deptSelect.empty().append($('<option></option>').val('').text('Please select'));

//            //var projSelect = $('#ProjectID_' + distCounter);
//            //projSelect.empty().append($('<option></option>').val('').text('Please select'));

//            //Load departments.
//            $.each(data, function (index, item) {
//                deptSelect.append($('<option></option>').val(item.value).text(item.text));
//            });
//        },
//        error: function (request, error) {
//            console.log(error);
//            alert(error);
//        }
//    });
//}
//function getInitializedProjects(departmentCode) {
//    //Get the project data, based on the department.
//    $.ajax({
//        type: "GET",
//        url: getProjectsURL,
//        data: { departmentCode: departmentCode, organization: Organization },
//        headers:
//        {
//            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
//        },
//        success: function (data) {

//            //Clear project dropdown.
//            var projSelect = $('#UAFundID');
//            projSelect.empty().append($('<option></option>').val('').text('Please select'));

//            //Populate projects.
//            $.each(data, function (index, item) {
//                projSelect.append($('<option></option>').val(item.value).text(item.text));
//            });

//            $('#messageText').css('visibility', 'hidden');
//        },
//        error: function (request, error) {
//            console.log(error);
//            alert(error);
//        }
//    });
//}

function acceptDesignationSearch() {
    //Populate the project text and slide Fund Search panel up to close.

    var projId = $('#UAFundID').find(':selected').val();
    $('#ProjectDistribution_ProjectId').val(projId);

    hideDesignationSearch();
}

function getColleges(distCounter) {
    //Called when a fund account search is started.

    $.ajax({
        type: "GET",
        url: getCollegesURL,
        //data: { collegeCode: collegeCode },
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {

            //Clear department and project dropdowns.
            var deptSelect = $('#Dept_code_' + distCounter);
            deptSelect.empty().append($('<option></option>').val('').text('Please select'));

            var projSelect = $('#ProjectID_' + distCounter);
            projSelect.empty().append($('<option></option>').val('').text('Please select'));

            var collSelect = $('#College_code_' + distCounter);

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
            alert(error);
        }
    });
}


function getDepartments(collegeCode, distCounter) {
    //Get the department data, based on the college.

    $.ajax({
        type: "GET",
        url: getDepartmentsURL,
        data: { collegeCode : collegeCode },
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {

            //Clear Departments and Projects.
            var deptSelect = $('#Dept_code_' + distCounter);
            deptSelect.empty().append($('<option></option>').val('').text('Please select'));

            //var projSelect = $('#ProjectID_' + distCounter);
            //projSelect.empty().append($('<option></option>').val('').text('Please select'));

            //Load departments.
            $.each(data, function (index, item) {
                deptSelect.append($('<option></option>').val(item.value).text(item.text));
            });
        },
        error: function (request, error) {
            console.log(error);
            alert(error);
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

//function getUAFundDesc(isNewFund, fundID, distCounter, organization) {
//    //Used when user manually enters a fund code and needs the match description populated.

//    if (isNewFund.toUpperCase() === "TRUE") return;

//    if (fundID.length <= 0) return false;

//    var myData = { id: fundID, index: distCounter, organization: organization };
//    var jsonData = JSON.stringify(myData);

//    $.ajax({
//        type: "POST",
//        url: SearchProjectURL,
//        data: jsonData,
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        headers:
//        {
//            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
//        },
//        success: function (data) {
//            //Put fund description in field.
//            if (data.indexOf("Not Found") !== -1) {
//                $('#Distributions_' + distCounter + '__FundAccount').val("");
//                $('#messageText').text("Designation not found in database.").removeClass('d-none').removeClass('alert-success').addClass('alert-danger');
//                $('#messageText').css('visibility', 'visible');
//                return false;
//            }
//            else {
//                $('#Distributions_' + distCounter + '__FundAccount').val(data);
//                $('#messageText').css('visibility', 'hidden');
//                return false;
//            }
//        },
//        error: function (request, error) {
//            console.log(error);
//        }
//    });
//}

//function handleDistributionDropDowns(distCounter, batchTypeCode) {

//    //Handle UDF reason.
//    if ($('#Distributions_' + distCounter + '__IsUdfExempt').length > 0) {
//        handleUdf($('#Distributions_' + distCounter + '__IsUdfExempt')[0].checked, distCounter);
//    }

//    //Handle UDF Other description.
//    if ($('select[name="Distributions[' + distCounter + '].UdfFeeExemptionId"]').length > 0) {
//        var options = $('select[name="Distributions[' + distCounter + '].UdfFeeExemptionId"]')[0].options;
//        handleUdfOther(options[options.selectedIndex].text, distCounter);
//    }
//}

function handleUdf(checked, distCounter, doCalcUdfAmount) {
    if (checked) {
        //Display exemption reason control.
        $('select[name="Distributions[' + distCounter + '].UdfFeeExemptionId"]').css("display", "");
        $(`input[id="Distributions[${distCounter}]__UdfFeeAmount"]`).val(amountToCommaString(0));
        //Change asterisk styling.
        $('#udfReasonCellHeader > label > label').addClass("required");
    }
    else {
        //Clear and hide reason control.
        $('select[name="Distributions[' + distCounter + '].UdfFeeExemptionId"]')[0].options.selectedIndex = 0;
        $('select[name="Distributions[' + distCounter + '].UdfFeeExemptionId"]').css("display", "none");

        //Change asterisk styling.
        $('#udfReasonCellHeader > label > label').removeClass("required");
        //Clear and hide "Other" field.
        handleUdfOther("", distCounter);
    }

    //Need to recalculate UDF amount, depending on UDF checkbox values.
    calcAllAmounts(distCounter, doCalcUdfAmount);
}

function handleUdfOther(udfValue, distCounter) {
    if (udfValue === "Other") {
        //Enable Other field.
        $('#Distributions_' + distCounter + '__UdfFeeExemptionOtherDesc').css("display", "");

        //Change asterisk styling.
        $('#udfOtherCellHeader > label > label').addClass("required");
    }
    else {
        //Clear and hide Other field.
        $('#Distributions_' + distCounter + '__UdfFeeExemptionOtherDesc').val("");
        $('#Distributions_' + distCounter + '__UdfFeeExemptionOtherDesc').css("display", "none");

        //Change asterisk styling.
        $('#udfOtherCellHeader > label > label').removeClass("required");
    }
}
//$(document).ready(function () {
//    var Distributions = $('#distTable tbody tr[id^="mainDataRow_"]');

//    $.each(Distributions, function (i) {
//        calcAllAmounts(i);
//    })
//});

function getProjects(departmentCode, distCounter) {
    //Get the project data, based on the department.
    $.ajax({
        type: "GET",
        url: getProjectsURL,
        data: { departmentCode: departmentCode, organization: Organization },
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {

            //Clear project dropdown.
            var projSelect = $('#UAFundID_' + distCounter);
            projSelect.empty().append($('<option></option>').val('').text('Please select'));

            //Populate projects.
            $.each(data, function (index, item) {
                projSelect.append($('<option></option>').val(item.value).text(item.text));
            });

            $('#messageText').css('visibility', 'hidden');
        },
        error: function (request, error) {
            console.log(error);
            alert(error);
        }
    });
}

$('input[name="InitializedConstituent.ConstituentFor"]').change(function () {
    checkOrgRadio();
})
function checkOrgRadio() {
    var checked = $('#individual').is(':checked');

    if (checked) {
        $('#organizationNameSection').addClass('d-none');
        $('#constituentNameSection').removeClass('d-none');
        $('#InitializedConstituent_OrganizationName').val("");
    }
    else {
        $('#constituentNameSection').addClass('d-none');
        $('#organizationNameSection').removeClass('d-none');
        $('#InitializedConstituent_FirstName').val("");
        $('#InitializedConstituent_LastName').val("");
    }
}




function getFundDesc(fundID, distCounter, organization) {
    //Used when user manually enters a fund code and needs the match description populated.
    handleFundAccount(distCounter);

    var isNewFund = $(`#Distributions_${distCounter}__IsNewFund`).is(':checked');
    if (isNewFund === true || fundID.length <= 0) return;

    var myData = { id: fundID, index: distCounter, organization: organization };
    var jsonData = JSON.stringify(myData);

    $.ajax({
        type: "POST",
        url: SearchProjectURL,
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        headers:
        {
            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            //Put fund description in field.
            if (data.projDesc == "Not Found") {
                $('#Distributions_' + distCounter + '__FundAccount').val("");
                $('#Distributions_' + distCounter + '__FundAccountName').val("");
                $('#messageText').text("Designation not found in database.").removeClass('d-none').removeClass('alert-success').addClass('alert-danger');
                $('#messageText').css('visibility', 'visible');
                return false;
            }
            else {
                $('#Distributions_' + distCounter + '__FundAccount').val(data.id);
                $('#Distributions_' + distCounter + '__FundAccountName').val(`${data.id} - ${data.projDesc}`);
                $('#messageText').css('visibility', 'hidden');
                return false;
            }
        },
        error: function (request, error) {
            console.log(error);
        }
    });
}

function showUAFundSearch(distCounter) {
    //Get colleges and slide Fund Search panel down to open.

    getColleges(distCounter);

    $('#mainDataRow_' + distCounter).find('#selectUAProject').slideDown("slow", function () { });
}

function acceptUAFundSearch(distCounter) {
    //Populate the project text and slide Fund Search panel up to close.

    var projText = $('#UAFundID_' + distCounter).find(':selected').val();
    $('#Distributions_' + distCounter + '__FundAccountName').val(projText).trigger('change');

    hideUAFundSearch(distCounter);
}

function cancelUAFundSearch(distCounter) {
    //Cancel fund search.
    hideUAFundSearch(distCounter);
}

function hideUAFundSearch(distCounter) {
    $('#mainDataRow_' + distCounter).find('#selectUAProject').slideUp("slow", function () { });
}

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

function handleFundAccount(distCounter) {
    $(`#newFundMessageDiv_${distCounter}`).css('display', 'none');
    $(`#Distributions_${distCounter}__IsNewFund`).val('False');
}

$('#splitButton').click(function (e) {
    e.preventDefault();

    var valid = true;
    var distRows = $('#distTable tbody tr[id^="mainDataRow_"]');

    $.each(distRows, function (i) {
        var FundAccount = $(`#Distributions_${i}__FundAccount`).val();
        var Amount = $(`#Distributions_${i}__Amount`).val();
        var IsUdfExemptChecked = $(`#Distributions_${i}__IsUdfExempt`).is(':checked');
        var UdfFeeExemptionId = $(`#Distributions_${i}__UdfFeeExemptionId`).val();
        var UdfFeeExemptionOtherDesc = $(`#Distributions_${i}__UdfFeeExemptionOtherDesc`).val();
        //Distributions[0].IsNewFund
        var isNewFund = $(`Distributions[${i}].IsNewFund`).val();

        if (!isNewFund == 0) {
            if ((FundAccount == '' || FundAccount == 'NaN')) {
                valid = false;
            }
        }
        if ((Amount == '' || Amount == 'NaN')) {
            valid = false;
            //return false;
        }

        if (IsUdfExemptChecked) {
            if (UdfFeeExemptionId == '' ||
                UdfFeeExemptionId == 2 && UdfFeeExemptionOtherDesc == '') {
                valid = false;
                //return false;
            }
        }
    });

    if (valid) {
        AddDistribution($(this));
    }
    else {
        $('#messageText').text("A Distribution is invalid. Please fill in the required fields.").removeClass('d-none').removeClass('alert-success').addClass('alert-danger');
        $('#messageText').css('visibility', 'visible');
    }
})

function AddDistribution(elem) {
    var index = $(elem).attr("data-index");
    var organization = $("#Organization").val();
    var formNumber = $("#FormNumber").val();

    $.ajax({
        type: "GET",
        url: addItemURL,
        data: {
            index: index,
            organization: organization,
            formNumber: formNumber
        },
        success: function (data) {
            var currRow = $(elem).closest('table').find('tbody tr:last');

            if (currRow.length > 0) {
                currRow.after(data);
            }

            handleDistributions();
        },
        error: function (request, error) {
            console.log(error);
        }
    });

    index = parseInt(index) + 1;

    $(elem).attr("data-index", index);

    $(`#Distributions_${index - 1}__ReceiptAmount`).val('0.00');
}

//function AddDistribution() {
//    var placeholder = (Organization == "uaf" ? "99-99-9999" : "Designation Name");
//    var lastIndex = $('[id^="mainDataRow_"]:last')[0];
//    var newIndex = 0;

//    if (lastIndex != undefined) {
//        lastIndex = lastIndex.id.replace('mainDataRow_', '');
//        newIndex = parseInt(lastIndex) + 1;
//    }

//    var row =
//        `<tr id="mainDataRow_${newIndex}" class="DistributionGroup_${newIndex}">
//        <td id="designationDiv_${newIndex}">
//            <div class="input-group">
//                <a class="delete Remove_${newIndex}"
//                   data-toggle="tooltip"
//                   onclick="RemoveDistribution(${newIndex})"
//                   style="cursor:pointer;"><i class="fas fa-trash-alt" style="color:firebrick;">&#xE872;</i></a>
//                <input id="Distributions_${newIndex}__FundAccount" 
//                        name="Distributions[${newIndex}].FundAccount"
//                        class="form-control inlineControl" placeholder=${placeholder} />

//                <input type="button" class="btn btn-sm btn-blue input-group-append valid" id="expandButton_${newIndex}" value="Expand"
//                        onclick="showUAFundSearch(${newIndex}, '${Organization}')" />
//            </div>
//            <div id="selectUAProject" class="dropPanel" style="display: none">
//                <div id="college" class="dropPanelDiv">
//                    <div class="row">
//                        <div class="col-md-12 form-group mt-2">
//                            <label class="control-label">College</label>
//                            <select id="College_code_${newIndex}" class="form-control dropPanelControl"
//                                    onchange="getDepartments(this.value, ${newIndex})"></select>
//                        </div>
//                    </div>
//                </div>
//                <div id="dept" class="dropPanelDiv">
//                    <div class="row">
//                        <div class="col-md-12 form-group">
//                            <label class="control-label">Department</label>
//                            <select id="Dept_code_${newIndex}" class="form-control dropPanelControl"
//                                    onchange="getProjects(this.value, ${newIndex})"></select>
//                        </div>
//                    </div>
//                </div>
//                <div id="UAproject" class="dropPanelDiv">
//                    <div class="row">
//                        <div class="col-md-12 form-group">
//                            <label class="control-label">Designation</label>
//                            <select id="UAFundID_${newIndex}" class="form-control dropPanelControl"></select>
//                        </div>
//                    </div>
//                </div>
//                <div>
//                    <button type="button" class="btn btn-light dropPanelButton"
//                            onclick="cancelUAFundSearch(${newIndex})">
//                        Cancel
//                    </button>
//                    <button type="button" class="btn btn-blue dropPanelButton"
//                            onclick="acceptUAFundSearch(${newIndex})">
//                        Select
//                    </button>
//                </div>
//            </div>

//        </td>
//        <td hidden>
//            <input id="Distributions_${newIndex}__Id" name="Distributions[${newIndex}].Id" class="form-control" value="00000000-0000-0000-0000-000000000000" />
//        </td>
//        <td hidden>
//            <input class="form-control" type="datetime-local" id="Distributions_${newIndex}__DateAdded" name="Distributions[${newIndex}].DateAdded" value="">
//        </td>
//        <td>
//            <input id="Distributions_${newIndex}__Amount" name="Distributions[${newIndex}].Amount" class="form-control amount"
//                    placeholder="0.00" onchange="calcAllAmounts(${newIndex}, true)" />
//        </td>
//        <td>
//            <input id="Distributions_${newIndex}__BenefitAmount" name="Distributions[${newIndex}].BenefitAmount"
//                    class="form-control amount" placeholder="0.00"
//                    onchange="calcAllAmounts(${newIndex}, true)" />
//        </td>
//        <td>
//            <input id="Distributions_${newIndex}__ReceiptAmount" name="Distributions[${newIndex}].ReceiptAmount"
//                    class="form-control amount" readonly tabindex="-1" />
//        </td>
//        <td class="col-md-1" id="udfAmountCell">
//            <input id="Distributions_${newIndex}__UdfFeeAmount" name="Distributions[${newIndex}].UdfFeeAmount"
//                    class="form-control amount" readonly tabindex="-1"
//                    onchange="calcAllAmounts(${newIndex}, true)" />
//        </td>
//        <td>
//            <input type="checkbox" id="Distributions_${newIndex}__IsPledge" name="Distributions[${newIndex}].IsPledge"
//                    class="checkbox" />
//        </td>
//        <td>
//            <input type="checkbox" id="Distributions_${newIndex}__IsUdfExempt" name="Distributions[${newIndex}].IsUdfExempt"
//                    class="checkbox" onchange="handleUdf(this.checked, ${newIndex})" />
//        </td>
//        <td id="udfReasonCell">
//            <select id="Distributions_${newIndex}__UdfFeeExamptionId" name="Distributions[${newIndex}].UdfFeeExemptionId"
//                    class="form-control"
//                    style="display:none; "
//                    onchange="handleUdfOther(this.options[this.options.selectedIndex].text, ${newIndex})">
//                <option value="">Select</option>
//                <option value="1">Scholarship (must be awarded within 12 mos)</option>
//                <option value="2">Other</option>
//            </select>
//        </td>
//        <td id="udfOtherCell">
//            <input id="Distributions_${newIndex}__UdfFeeExemptionOtherDesc" name="Distributions[${newIndex}].UdfFeeExemptionOtherDesc"
//                    class="form-control" style="display:none; " />
//        </td>
//    </tr>
//    <tr class="DistributionGroup_${newIndex}">
//        <td>
//            <div>
//                <div class="form-check form-check-inline">
//                    <label style="margin-left: 10px;" class="form-check-label">Or New Designation</label> &nbsp
//                    <input type="checkbox"
//                            id="Distributions_${newIndex}__IsNewFund" 
//                            name="Distributions[${newIndex}].IsNewFund"
//                            class="form-check-input IsNewFundChbx"
//                            onclick="handleNewFund(this, ${newIndex})" />
//                </div>
//            </div>
//        </td>
//    </tr>
//    <tr class="DistributionGroup_${newIndex}">
//        <td colspan="10" style="border-top: none; padding: 0;">
//            <div id="newFundMessageDiv_${newIndex}" style="text-align: center; display: none;">
//                <span id="newFundMessage">
//                    ${(Organization == "uaf" ? "Please complete a <a target=\"_blank\" rel=\"noopener noreferrer\" href=\"/doc/NewProjectRequest.pdf\">New Project Request</a> and attach to the Gift Transmittal when submitting to the UAF Financial Services office." : "Please send an action to UA Financials requesting a new account be established.")}
//                </span>
//            </div>
//        </td>
//    </tr>`;

//    $('#distTable tbody').append(row);

//    $('#hidden_Distribution__IsNewFund').append(`<input type="hidden" name="Distributions[${newIndex}].IsNewFund" id="Distributions_${newIndex}__IsNewFund" />`);
//}

//function RemoveDistribution(index) {
//    var giftTransmittalId = $('#GiftTransmittalId').val();
//    var itemId = $("#ItemId").val();

//    var distributionId = $(`#Distributions_${index}__Id`).val();

//    if (distributionId != emptyGuid) {
//        RemoveDistributionAjax(index, giftTransmittalId, itemId, distributionId);
//    }
//    else {
//        RemoveNewDistribution(index);
//    }
//}

//function RemoveDistributionAjax(index, giftTransmittalId, itemId, distributionId) {
//    var token = $('input[name=__RequestVerificationToken]').val();

//    var jsonData = JSON.stringify({
//        giftTransmittalId: giftTransmittalId,
//        itemId: itemId,
//        distributionId: distributionId
//    });

//    $.ajax({
//        type: "POST",
//        url: deleteDistributionURL,
//        data: jsonData,
//        contentType: "application/json; charset=utf-8",
//        headers:
//        {
//            "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
//        },
//        success: function (data) {
//            RemoveNewDistribution(index);
//        },
//        error: function (request, error) {
//            //If error, display the Error modal
//            console.log(error);
//            alert("There was an error removing the distribution");
//        }
//    });
//}

function RemoveNewDistribution(index) {
    var group = $(`tr.DistributionGroup_${index}`);
    group.remove();
    $(`input[name^="Distributions[${index}]"]`).remove();
}

$('#IncludeRecognitionCredit').change(function () {
    setupRecognitionCreditFields();
})

function setupRecognitionCreditFields() {
    var checked = $('#IncludeRecognitionCredit').is(':checked');

    if (checked) {
        $('[id^="RecognitionCredit"]').removeAttr('disabled');
    }
    else {
        $('[id^="RecognitionCredit"]').val('');
        $('[id^="RecognitionCredit"]').attr('disabled', 'disabled');
    }
}

function calcAllAmounts(counter, doCalcUdfAmount) {
    //Calculate all financial amounts.

    calcReceiptAmount(counter);

    calculateGiftTotal();
    calculateBenefitTotal();
    calculateReceiptTotal();

    //This depends on the Receipt total.
    if (doCalcUdfAmount == true) {
        calcUdfAmount(counter, batchTypeCode);
    }

    calculateUdfTotal();
    calculateUdfDevTotal();
    calculateUdfPresTotal();
    calculateUdfDeanTotal();
}

function calcReceiptAmount(counter) {
    //Set receipt amount = amount - benefit amount.
    var amount = commaStringToAmount($('#Distributions_' + counter + '__Amount').val());
    var benefit = commaStringToAmount($('#Distributions_' + counter + '__BenefitAmount').val());
    $('#Distributions_' + counter + '__ReceiptAmount').val(amountToCommaString(amount - benefit));
}

function calcUdfAmount(counter, batchTypeCode) {
    //If non-UDF-exempt receipt total >= $1000 and the Batch Type is NOT Gift Commitment (Id 5) or Pledge (Id 7) , apply a 6% UDF fee to the receipts that are not exempt.
    var total = 0;
    //var index = 0;

    //Get the non-UDF-exempt receipt total.
    //$("#distTable").find('input[id*="__ReceiptAmount"]').each(function () {
    //    //if (!$('#Distributions_' + index + '__IsUdfExempt')[0].checked) {
    //    total += commaStringToAmount($(this).val());
    //    //}
    //    index++;
    //});

    $("#distTable").find('input[id*="__ReceiptAmount"]').each(function () {
        //if (!$('#GiftTransmittalItemDistributionList_' + index + '__IsUdfExempt')[0].checked) {
        total += commaStringToAmount($(this).val());
        //}
    });

    index = 0;
    //Based on that total, apply the correct UDF fee to each row.
    if (total >= 1000 && (batchTypeCode != 5 && batchTypeCode != 6 && batchTypeCode != 7)) {
        $("#distTable").find(`input[id*="__UdfFeeAmount"]`).each(function (index) {
            if (!$(`#Distributions_${index}__IsUdfExempt`)[0].checked) {
                var receiptAmount = commaStringToAmount($(`#Distributions_${index}__ReceiptAmount`).val());
                var udfAmount = receiptAmount * .06;
                var udfFeeDevelopment = receiptAmount * .04;
                var udfFeePresident = receiptAmount * .01;
                var udfFeeDean = receiptAmount * .01;

                $("#distTable").find(`input[id*="${index}__UdfFeeAmount"]`).val(amountToCommaString(udfAmount));
                $("#distTable").find(`input[id*="${index}__UdfFeeDevelopment"]`).val(amountToCommaString(udfFeeDevelopment));
                $("#distTable").find(`input[id*="${index}__UdfFeePresident"]`).val(amountToCommaString(udfFeePresident));
                $("#distTable").find(`input[id*="${index}__UdfFeeDean"]`).val(amountToCommaString(udfFeeDean));
            }
            else {
                $("#distTable").find(`input[id*="${index}__UdfFeeAmount"]`).val(amountToCommaString(0));
                $("#distTable").find(`input[id*="${index}__UdfFeeDevelopment"]`).val(amountToCommaString(0));
                $("#distTable").find(`input[id*="${index}__UdfFeePresident"]`).val(amountToCommaString(0));
                $("#distTable").find(`input[id*="${index}__UdfFeeDean"]`).val(amountToCommaString(0));
            }
        })

        
    }
    else {
        $("#distTable").find(`input[id*="__UdfFeeAmount"]`).each(function (index) {
            $("#distTable").find(`input[id*="${index}__UdfFeeAmount"]`).val(amountToCommaString(0));
            $("#distTable").find(`input[id*="${index}__UdfFeeDevelopment"]`).val(amountToCommaString(0));
            $("#distTable").find(`input[id*="${index}__UdfFeePresident"]`).val(amountToCommaString(0));
            $("#distTable").find(`input[id*="${index}__UdfFeeDean"]`).val(amountToCommaString(0));

        })
    }
}

function SetManualUdfAmount(elem) {
    var udfAmount = commaStringToAmount($(elem).val())
    $(elem).val(amountToCommaString(udfAmount));

    calculateUdfTotal();
    calculateUdfDevTotal();
    calculateUdfPresTotal();
    calculateUdfDeanTotal();
}

function calculateGiftTotal() {
    var total = 0;
    $("#distTable").find('input[id*="__Amount"]').each(function () {
        total += commaStringToAmount($(this).val());
    });

    if (total >= 10000) {
        $('#RequireSecondaryApproverCheckbox')
            .prop('checked', true)
            .attr('disabled', 'disabled');

        $('#RequireSecondaryApprover').val(true)
    }
    else {
        $('#RequireSecondaryApproverCheckbox')
            .removeAttr('disabled')
    }

    $("#giftTotal").val(amountToCommaString(total));
}

function calculateBenefitTotal() {
    var total = 0;
    $("#distTable").find('input[id*="__BenefitAmount"]').each(function () {
        total += commaStringToAmount($(this).val());
    });
    $("#benefitTotal").val(amountToCommaString(total));
}

function calculateReceiptTotal() {
    var total = 0;
    $("#distTable").find('input[id*="__ReceiptAmount"]').each(function () {
        total += commaStringToAmount($(this).val());
    });
    $("#receiptTotal").val(amountToCommaString(total));
}

function calculateUdfTotal() {
    var total = 0;
    $("#distTable").find('input[id*="__UdfFeeAmount"]').each(function () {
        total += commaStringToAmount($(this).val());
    });
    $("#udfTotal").val(amountToCommaString(total));
}

function calculateUdfDevTotal() {
    var total = 0;
    $("#distTable").find('input[id*="__UdfFeeDevelopment"]').each(function () {
        total += commaStringToAmount($(this).val());
    });
    $("#udfDevTotal").val(amountToCommaString(total));
}

function calculateUdfPresTotal() {
    var total = 0;
    $("#distTable").find('input[id*="__UdfFeePresident"]').each(function () {
        total += commaStringToAmount($(this).val());
    });
    $("#udfPresTotal").val(amountToCommaString(total));
}

function calculateUdfDeanTotal() {
    var total = 0;
    $("#distTable").find('input[id*="__UdfFeeDean"]').each(function () {
        total += commaStringToAmount($(this).val());
    });
    $("#udfDeanTotal").val(amountToCommaString(total));
}

function handleNewFund(elem, distCounter) {
    //Functionality for New Fund checkbox.
    //The checkbox and the Fund Designation are mutually exclusive.
    var checked = $(elem).is(':checked');

    //var val = $(id).val();
    if (checked) {
        $('#designationDiv_' + distCounter).attr("disabled", "disabled");
        $('#designationDiv_' + distCounter + ' input[type="text"]').attr("disabled", "disabled");
        $('#designationDiv_' + distCounter + ' input[type="text"]').val("");
        $(`#Distributions_${distCounter}__IsNewFund`).val('True');

        $('#newFundMessageDiv_' + distCounter).slideDown("slow", function () { });
    }
    else {
        $('#designationDiv_' + distCounter).removeAttr("disabled");
        $('#designationDiv_' + distCounter + ' input[type="text"]').removeAttr("disabled", "disabled");
        //$('#designationDiv_' + distCounter + ' input').val("");
        $('#expandButton_' + distCounter).val("Expand");
        $(`#Distributions_${distCounter}__IsNewFund`).val('False');

        $('#newFundMessageDiv_' + distCounter).slideUp("slow", function () { });
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

function getPreparerDetails() {
    var preparedByEmployeeId = $('#PreparedByEmployeeId').val();
    var formData = {
        preparedByEmployeeId: preparedByEmployeeId,        
    };
    $.ajax({       
        type: "GET",        
        url: PreparerDetailsURL,
        data: formData,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var department = data.departmentCode + ' - ' + data.departmentName;
            var departmentVal = department;
            var jobTitleVal = data.jobTitle;
            $('#DepartmentCode').val(departmentVal);
            $('#DepartmentText').text(departmentVal);
            $('#JobTitle').val(jobTitleVal);
            $('#JobTitleText').text(jobTitleVal);
            $('#PreparedByEmployeeId').text(null);

        },
        error: function (request, error) {
            console.log(error);
            alert("There was an error getting the preparer.");
        }
    });

}

$('#Approval_ApprovalComments').keyup(function () {
    if ($(this).val()) {
        $('#rejectButton').removeAttr('disabled');
    }
    else {
        $('#rejectButton').attr('disabled', true);
    }

});

$('#rejectButton').click(function (e) {
    e.preventDefault();

    $('#rejectTransmittalModal').modal('show');
});
function ApproveReject(approve) {
    $('#Approved').val(approve);
    $('#editForm').submit();
}

function RejectTransmittal() {
    if ($('#Comments').val()) {
        $('#rejectTransmittalModal').modal('hide');

        var giftTransmittalId = $('#GiftTransmittalId').val();
        var comments = $('#Comments').val();

        var formData = {
            giftTransmittalId: giftTransmittalId,
            comments: comments
        };

        $.ajax({
            type: "POST",
            url: rejectTransmittalURL,
            data: formData,
            headers:
            {
                "RequestVerificationToken": $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (data) {
                window.location.href = transmittalIndexURL;
            },
            error: function (request, error) {
                console.log(error);
                alert("There was an error rejecting the gift transmittal.");
                //    alert(error);
            }
        });
    }
}
//Preparer Amount Converstion
$('input[name="ProjectDistribution.Amount"]').change(function () {
    var total = 0;
    total += commaStringToAmount($('#ProjectDistribution_Amount').val());
    $('#ProjectDistribution_Amount').val(amountToCommaString(total));
})
//Preparer Designation removes white space
$('input[name="ProjectDistribution.ProjectId"]').change(function () {    
    var text = $('#ProjectDistribution_ProjectId').val().trim();
    $('#ProjectDistribution_ProjectId').val(text);    
})
// PreparerId ---
$('select[id*="PreparedByEmployeeId"]').each(function (i, elem) {
    initPreparerSelect();
})
function initPreparerSelect() {
    var PreparerIdDropDown = $('select#PreparedByEmployeeId');

    var clearButtonClicked = false;
    var clearButton = $('.select2-selection__clear');
    $(clearButton).on('click', function () {
        clearButtonClicked = true;
        console.log('clearButton clicked, and its value is ' + clearButtonClicked);
        $(this).parents('.form-group').find('.form-control').val(null).trigger('change');
        $(data.text).val(null);
    });

    $(PreparerIdDropDown).on('select2:open', () => {
        document.querySelector('.select2-search__field').focus();
    });

    $(PreparerIdDropDown).select2({
        selectOnClose: false, // select currently highlighted option when menu is closed
        closeOnSelect: true, // close menu once item is selected
        allowClear: true,        
        width: '100%',
        ajax: {
            delay: 250,
            url: PreparerIdMethod,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                //var employeeId = $(this).find('select[id*="PreparedByEmployeeId"] option:selected').val();

                var queryParams = {
                    term: params.term, // search term
                    //employeeId: employeeId,
                    _type: 'query_append',
                    selected: true,
                    page: params.page || 1
                }
                return queryParams;
            },
            processResults: function (data, params) {
                return {
                    results: data
                };
            },
            cache: true
        },
        placeholder: '-- Choose Preparer --',
        minimumInputLength: 3,
        minimumResultsForSearch: 5,
        templateResult: function (data) {
            //var $result = $("<option></option>");            
            var $result = $("<option></option>").text(data.text).val(data.id);
            return $result;
        },
        //templateSelection: function (data) {
        //    var name = `${data.firstName} ${data.lastName}`;
        //    return name;
        //}
    });

}
//// Initialize select2 with Current Value
var preparedByEmployeeId = $('#PreparedByEmployeeId').val();
var preparerName = $('#PreparerName').val();

if (preparedByEmployeeId != undefined && preparedByEmployeeId.length > 0) {
    if (preparerName != null) {
        var savedOption = new Option(preparerName, preparedByEmployeeId, true, true);
        $('select#PreparedByEmployeeId').append(savedOption).trigger('change');
    };
}

var clearButton = $('select2-PreparedByEmployeeId-container .select2-selection__clear');
$(clearButton).on('click', function () {
    clearPreparerFields();
});

$('select#PreparedByEmployeeId').change(function () {
    clearPreparerFields();

    if (this.value > 0) {
        var preparer = $('select#PreparedByEmployeeId').select2('data');
        var preparerVal = preparer[0].id;
        var preparerText = preparer[0].text;
        var departmentVal = preparer[0].department;
        var jobTitleVal = preparer[0].jobTitle;
        var EmailVal = preparer[0].email;
        var PhoneNumberVal = preparer[0].phone;


        $('#PreparerName').val(preparerText);
        $('#PreparedByNameText').text(preparerText);
        $('#PreparedByEmployeeId').val(preparerVal);
        $('#DepartmentCode').val(departmentVal);
        $('#DepartmentText').text(departmentVal);
        $('#JobTitle').val(jobTitleVal);
        $('#JobTitleText').text(jobTitleVal);
        $('#Email').val(EmailVal);
        $('#EmailText').text(EmailVal);
        $('#Phone').val(PhoneNumberVal);
        $('#PhoneNumberText').text(PhoneNumberVal);



        
    }
});

function clearPreparerFields() {
    $('#PreparerName').val(null);
    $('#PreparedByNameText').text(null);
    //$('#DepartmentCode').val(null);
    $('#DepartmentText').text(null);
    //$('#JobTitle').val(null);
    $('#JobTitleText').text(null);
    //$('#Email').val(null);
    $('#EmailText').text(null);
    //$('#Phone').val(null);
    $('#PhoneNumberText').text(null);
}

function CheckFormStatus(elem) {
    $('input.transmittal-status').not(elem).prop('checked', false);

    EnableDisableApproveButton();
}

$('#ApprovalComments').keyup(function () {
    if ($(this).val())
        $('#btnReject').removeAttr('disabled');
    else
        $('#btnReject').attr('disabled', true);
})

$('#ReceivedPhysicalDocuments').change(function () {
    var isChecked = $(this).is(':checked');

    if (!isChecked) {
        $('input.transmittal-status').prop('checked', false);
        $('input.transmittal-status').attr('disabled', true);
    }
    else {
        $('input.transmittal-status').removeAttr('disabled');
    }

    EnableDisableApproveButton();
})

$('#PostDate').change(function () {
    EnableDisableApproveButton();
})

function EnableDisableApproveButton() {
    var statusLength = $('input.transmittal-status:checked').length;
    var receivedPhysicalDocuments = $('#ReceivedPhysicalDocuments').is(':checked');

    if (statusLength > 0 || receivedPhysicalDocuments == false) {
        $('.btnApprove').attr('disabled', true);
    }
    else {
        if ($('#btnFinalize').length) {
            if ($('#PostDate').val()) {
                $('.btnApprove').removeAttr('disabled');
            }
            else {
                $('.btnApprove').attr('disabled', true);
            }
        }
        else {
            $('.btnApprove').removeAttr('disabled');
        }
    }
}

$('#editForm').submit(function (e) {
    $('#btnSave').prop('disabled', true);
    $('.btnApprove').prop('disabled', true);

})

function RemoveDistribution(elem) {
    var index = $(elem).attr("data-index");

    $(`#Distributions_${index}__IsDeleted`).val('True');
    $(`#Distributions_${index}__Amount`).val(0);
    $(`#mainDataRow_${index}`).hide();
    calcAllAmounts(0);
    handleDistributions();
}

function handleDistributions() {
    if (GetDistributionCount() > 1) {
        $('.removeDistributionItemSection').removeClass('d-none');
        $('.distributionItemAmountSection').addClass('col-md-11 pull-right').removeClass('col-md-12')
    }
    else {
        $('.removeDistributionItemSection').addClass('d-none');
        $('.distributionItemAmountSection').removeClass('col-md-11 pull-right').addClass('col-md-12')
    }
}

function GetDistributionCount() {
    var count = 0;
    $(".removeDistributionItemSection").each(function (index) {
        if ($(`#Distributions_${index}__IsDeleted`).val() == 'False') {
            count++;
        }
    });

    return count;
}

$('#Transaction_BatchTypeCode').change(function () {
    if ($(this).val() == '1') {
        $('#CheckPayableToTheUniversitySection').removeClass('d-none');
    }
    else {
        $('#CheckPayableToTheUniversitySection').addClass('d-none');
        $('#CheckPayableToTheUniversity_No').prop('checked', true);

        $('#CheckNumberSection').addClass('d-none');
        $('#CheckNumber').val(null);
    }
})

$('input[name="CheckPayableToTheUniversity"]').change(function () {
    if ($(this).val() == 'true') {
        $('#CheckNumberSection').removeClass('d-none');
    }
    else {
        $('#CheckNumberSection').addClass('d-none');
        $('#CheckNumber').val(null);
    }
})