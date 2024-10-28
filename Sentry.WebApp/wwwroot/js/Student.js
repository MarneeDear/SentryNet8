function LoadStudentParentList() {

    var uri = getParentList;
    var pageStart = 1;
    var pageLength = 10;

    // Data Table Variables
    // ------------------------------------------------------------
    var listTable = $('#listTable');
    var paginationControlVisibility = "p";
    var lengthControlVisibility = "l";

    var StudentParentCount = $('#StudentCount').find('span').text();

    if (StudentParentCount <= pageLength) {
        paginationControlVisibility = ""; // hides pagination
        lengthControlVisibility = ""; // hides page length dropdown
    };

    // DataTables initialization
    $('#listTable')
        .on('preXhr.dt', function (e, settings, data) {
                data.searchValue = data.search.value,
                data.sortColumn = data.columns[data.order[0].column].data,
                data.sortColumnDirection = data.order[0].dir
        })
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
            processing: true,
            serverSide: true,
            ajax: {
                url: uri,
                method: 'POST',
            },
            language: {
                //processing: '<div id="dt-processing">Processing... <div class="spinner"></div></div>',
                processing: '<i class="fa fa-spin fa-asterisk fa-fw"></i> <span class=""> Loading...</span>',
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
                    data: 'systemId'
                },
                {
                    targets: 2,
                    data: 'recordStatus',
                    render: function (data, type, row, meta) {
                        if (data == "Possible Match") {
                            return '<i role="img" class="fas fa-code-branch fa-rotate-270 text-info" title="Matching Issue"></i>';
                        }
                        else {
                            return '<i role="img" class="fas fa-exclamation-triangle text-warning" title="Data Quality Issue"></i>';
                        }
                    }
                },
                {
                    targets: 3,
                    autoWidth: true,
                    data: 'parentName',
                    searchable: true
                },
                {
                    targets: 4,
                    autoWidth: true,
                    data: 'studentName',
                    searchable: true
                },
                {
                    targets: 5,
                    autoWidth: true,
                    data: 'relationship',
                    searchable: true
                },
                {
                    targets: 6,
                    autoWidth: true,
                    data: 'errorCategories',
                    searchable: true
                },
                {
                    targets: 7,
                    autoWidth: true,
                    data: 'systemName',
                    searchable: true
                },
                {
                    targets: 8,
                    autoWidth: true,
                    data: 'integrationDate'
                },
                {
                    targets: 9,
                    autoWidth: true,
                    data: 'errorCount'
                }
            ],
            "order": [
                [8, "desc"]
            ],
            buttons: [
                'excel', 'pdfHtml5', 'print'
            ],
        });

    $('#listTable tbody').on('click', 'tr', function () {
        var table = $('#listTable').DataTable();
        var rowData = table.row(this).data();
        var url = "";

        if (rowData.recordStatus == "Possible Match") {
            url = `${studentParentMatchURL}?id=${rowData.id}&&systemId=${rowData.systemId}`;
        }
        else {
            url = `${studentParentEditURL}?id=${rowData.id}&&systemId=${rowData.systemId}`;
        }

        window.location.href = url;
    });

    // Tooltips [Regular]
    //----------------------------------------------------

    var elButton = $('button, i, [title]');
    $(elButton).each(function (i, obj) {
        $(this).tooltip({
            title: $(this).attr("title")
            , container: 'body'
            , placement: 'top'
            , trigger: 'hover'
        });

        $('body').on('hidden.bs.tooltip', function () {
            var tooltips = $('.tooltip').not('.in');
            if (tooltips) {
                tooltips.remove();
            }
        });
    });
    $(elButton).each(function (i, obj) {
        $(this).tooltip({
            title: $(this).attr("tooltip-title")
            , container: 'body'
            , placement: 'top'
            , trigger: 'hover'
        });

        $('body').on('hidden.bs.tooltip', function () {
            var tooltips = $('.tooltip').not('.in');
            if (tooltips) {
                tooltips.remove();
            }
        });
    });

    // Tooltip Titles
    //----------------------
    $(".ellipsis[title]").each(function (i, obj) {

        $(this).tooltip({
            title: $(this).attr("tooltip-title")
            , container: 'body'
            , placement: 'top'
            , trigger: 'hover'
        });

        $('body').on('hidden.bs.tooltip', function () {
            var tooltips = $('.tooltip').not('.in');
            if (tooltips) {
                tooltips.remove();
            }
        });
    });
};
