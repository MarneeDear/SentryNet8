// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// IIFE - Immediately Invoked Function Expression
(function (dashboard) {

    // The global jQuery object is passed as a parameter
    dashboard(window.jQuery, window, document);

}
    (function ($, window, document) {

        // The $ is now locally scoped 

        // Listen for the jQuery ready event on the document
        $(function () {

            // The DOM is ready!

        });


        // The rest of the code goes here!


        // Color Variables
        const colors = {

            mainBG: '#ffffff',

            primaryBlue: '#0C234B',
            primarySkyBlue: '#81D3EB',
            primaryOasisBlue: '#378DBD',
            primaryAzuriteBlue: '#1E5288',
            primaryMidnightBlue: '#001C48',

            primaryRed: '#AB0520',
            primaryRedBloom: '#EF4056',
            primaryRedChili: '#8B0015',

            neutralCoolGray: '#E2E9EB',
            neutralWarmGray: '#F4EDE5',

            secondaryLeaf: '#70B865',
            secondaryRiver: '#007D84',
            secondarySilver: '#9EABAE',
            secondaryMesa: '#a95c42'

        };




        // Add/Remove Parent-level Nav Selected Indicator.
        // ---------------------------------------------------------------------
        $('.sidebar-submenu ul li.active').parents('.active').addClass('removeIndicator');




        //$('input').on(':focus', function () {
        //    $(this).parent('fieldset').toggleClass('fieldsetbghighlight');
        //});






        // Add/Remove Disabled/Readonly attributes dynamically based on '_IsReadOnly' suffix from the view model.
        // ---------------------------------------------------------------------

        // Input Elements:
        var allDisabledInputs = $('input[data-disabled="True"]');
        $(allDisabledInputs).prop("disabled", true).prop("readonly", true);

        // Select Elements:
        var allReadOnlySelects = $('select[data-disabled="True"]');
        $(allReadOnlySelects).prop('disabled', true);
        $(allReadOnlySelects).attr('disabled', true);
        $(allReadOnlySelects).prop('readonly', true);
        $(allReadOnlySelects).attr('readonly', true);









        // Accordion: Expand/Collapse Child Rows
        // ---------------------------------------------------------------------

        /* Toggle Accordion */
        $('.accordian-body').on('show.bs.collapse', function () {
            $(this).closest("table")
                .find(".collapse.in")
                .not(this)
                .collapse('toggle')
        });

        /* Always keep one open */
        $("[data-toggle=collapse][data-parent]").click(function (e) {
            var button = $(this);
            var parent = $(button.attr("data-parent"));
            var panel = parent.find(button.attr("href") || button.attr("data-target"));
            if (panel.hasClass("in")) {
                e.preventDefault();
                e.stopPropagation()
            }
        });

        /* Toggle Expand/Collapse All */
        $('.if-not-collapsed').on('click', function () {
            $(this).parents('.modal-content').find('.accordian-body').collapse('hide');
        });
        $('.if-collapsed').on('click', function () {
            $(this).parents('.modal-content').find('.accordian-body').collapse('show');
        });

        /* Expand only the first row by default */
        var firstTableRowToExpand = $('.childRowTableParent tbody > tr:nth-child(2) .collapse');
        $(firstTableRowToExpand).addClass('in');
        var firstTableRowIconToExpand = $('.childRowTableParent tbody > tr.accordion-toggle[aria-expanded="false"]:first-child');
        $(firstTableRowIconToExpand).attr('aria-expanded', true);

        /* Expand/Collapse Table Row Icon */
        $('.accordion-toggle').on('click', function () {
            $(this).find('.fas').toggleClass('fa-caret-circle-down fa-caret-circle-right');
        });

        /* Toggle collapsed class on toggle button */
        var collapsedButtonToggle = $('a.collapsed');
        $(collapsedButtonToggle).on('click', function (e) {
            $(this).toggleClass('collapsed');
        });






        // Print
        // ---------------------------------------------------------------------

        $('.btn-print').on('click', function () {
            window.print();
            return false;
        });






        // Email Split (Username)
        // ---------------------------------------------------------------------
        var username = $('.user-name');
        var arr = username.html().split('.');
        //console.log('site.js -- username: ' + username);
        $(username).html(arr[0]);








        // Popover (Info)
        // ---------------------------------------------------------------------

        var $elementsInfo = $('.infoPerField');
        $elementsInfo.each(function () {
            var $element = $(this);

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-info-title').html();
                }
                , content: function () {
                    return $('#popover-info-content').html();
                    //return $($(this).data('contentwrapper')).html();
                }
                , trigger: 'hover'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-info');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var systemVariableName = window.systemVariableName;

                var name = button.data('info-content-name');
                var description = button.data('info-content-description');
                var source = button.data('info-content-source');
                var sourceFieldId = button.data('info-content-sourcefieldid');

                popoverButton.parents('body').find('#popover-info-content-name').text(name);
                popoverButton.parents('body').find('#popover-info-content-description').text(description);
                popoverButton.parents('body').find('#popover-info-content-source').text(source);
                popoverButton.parents('body').find('#popover-info-content-sourcefieldid').text(sourceFieldId);

                if (sourceFieldId == null || sourceFieldId == "") {
                    popoverButton.parents('body').find('#popover-info-content-sourcefieldid').html('<span id="popover-info-content-sourcefieldid"><span class="notProvidedDataHistory"><i role="img" class="fas fa-exclamation-triangle"></i> Not Provided in ' + systemVariableName + ' </span></span>');
                };
            });

        });





        // ****************************************************************************************************************************
        // Post To GL:
        // ****************************************************************************************************************************

        // Popover (History (per field): DesignationName
        //----------------------------------------------------
        var $elementsHistoryDesignationName = $('.historyPerField.postToGLDesignationName');
        $elementsHistoryDesignationName.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-designationName').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): DesignationId
        //----------------------------------------------------
        var $elementsHistoryDesignationId = $('.historyPerField.postToGLDesignationId');
        $elementsHistoryDesignationId.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-designationId').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });





        // ****************************************************************************************************************************
        // Employee:
        // ****************************************************************************************************************************

        // Popover (History (per field): FirstName
        //----------------------------------------------------
        var $elementsHistoryEmployeeFirstName = $('.historyPerField.employeeFirstName');
        $elementsHistoryEmployeeFirstName.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-firstname').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): MiddleName
        //----------------------------------------------------
        var $elementsHistoryEmployeeMiddleName = $('.historyPerField.employeeMiddleName');
        $elementsHistoryEmployeeMiddleName.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-middlename').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): LastName
        //----------------------------------------------------
        var $elementsHistoryEmployeeLastName = $('.historyPerField.employeeLastName');
        $elementsHistoryEmployeeLastName.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-lastname').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): PreferredName
        //----------------------------------------------------
        var $elementsHistoryEmployeePreferredName = $('.historyPerField.employeePreferredName');
        $elementsHistoryEmployeePreferredName.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-preferredname').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): UAEmailAddress
        //----------------------------------------------------
        var $elementsHistoryEmployeeUAEmailAddress = $('.historyPerField.employeeUAEmailAddress');
        $elementsHistoryEmployeeUAEmailAddress.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-uaEmailAddress').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): EmployeeId
        //----------------------------------------------------
        var $elementsHistoryEmployeeEmployeeId = $('.historyPerField.employeeEmployeeId');
        $elementsHistoryEmployeeEmployeeId.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-employeeId').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): HomeAddressLine1
        //----------------------------------------------------
        var $elementsHistoryEmployeeHomeAddressLine1 = $('.historyPerField.employeeHomeAddressLine1');
        $elementsHistoryEmployeeHomeAddressLine1.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-homeAddressLine1').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): HomeAddressLine2
        //----------------------------------------------------
        var $elementsHistoryEmployeeHomeAddressLine2 = $('.historyPerField.employeeHomeAddressLine2');
        $elementsHistoryEmployeeHomeAddressLine2.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-homeAddressLine2').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): HomeAddressLine3
        //----------------------------------------------------
        var $elementsHistoryEmployeeHomeAddressLine3 = $('.historyPerField.employeeHomeAddressLine3');
        $elementsHistoryEmployeeHomeAddressLine3.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-homeAddressLine3').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): HomeAddressLine4
        //----------------------------------------------------
        var $elementsHistoryEmployeeHomeAddressLine4 = $('.historyPerField.employeeHomeAddressLine4');
        $elementsHistoryEmployeeHomeAddressLine4.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-homeAddressLine4').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): HomeAddressCity
        //----------------------------------------------------
        var $elementsHistoryEmployeeHomeAddressCity = $('.historyPerField.employeeHomeAddressCity');
        $elementsHistoryEmployeeHomeAddressCity.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-homeAddressCity').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): HomeAddressState
        //----------------------------------------------------
        var $elementsHistoryEmployeeHomeAddressState = $('.historyPerField.employeeHomeAddressState');
        $elementsHistoryEmployeeHomeAddressState.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-homeAddressState').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): HomeAddressPostalCode
        //----------------------------------------------------
        var $elementsHistoryEmployeeHomeAddressPostalCode = $('.historyPerField.employeeHomeAddressPostalCode');
        $elementsHistoryEmployeeHomeAddressPostalCode.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-homeAddressPostalCode').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): HomeAddressCountry
        //----------------------------------------------------
        var $elementsHistoryEmployeeHomeAddressCountry = $('.historyPerField.employeeHomeAddressCountry');
        $elementsHistoryEmployeeHomeAddressCountry.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-homeAddressCountry').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): HireDate
        //----------------------------------------------------
        var $elementsHistoryEmployeeHireDate = $('.historyPerField.employeeHireDate');
        $elementsHistoryEmployeeHireDate.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-hireDate').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): TerminationDate
        //----------------------------------------------------
        var $elementsHistoryEmployeeTerminationDate = $('.historyPerField.employeeTerminationDate');
        $elementsHistoryEmployeeTerminationDate.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-terminationDate').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): TerminationDate
        //----------------------------------------------------
        var $elementsHistoryEmployeeUDP = $('.historyPerField.employeeUDP');
        $elementsHistoryEmployeeUDP.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-UDPEmployee').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });




        // ****************************************************************************************************************************
        // Organizational Unit:
        // ****************************************************************************************************************************



        // Popover (History (per field): Organizational Unit Name
        //----------------------------------------------------
        var $elementsHistoryOrgUnitName = $('.historyPerField.orgUnitName');
        $elementsHistoryOrgUnitName.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-orgUnitName').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): Organizational Unit Code
        //----------------------------------------------------
        var $elementsHistoryOrgUnitCode = $('.historyPerField.orgUnitCode');
        $elementsHistoryOrgUnitCode.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-orgUnitCode').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): Organizational Unit Name
        //----------------------------------------------------
        var $elementsHistoryOrgUnitType = $('.historyPerField.orgUnitType');
        $elementsHistoryOrgUnitType.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-orgUnitType').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): Organizational Unit
        //----------------------------------------------------
        var $elementsHistoryOrgUnit = $('.historyPerField.organizationalUnitParentOrgg');
        $elementsHistoryOrgUnit.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-orgUnit').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });

        // Popover (History (per field): Organization
        //----------------------------------------------------
        var $elementsHistoryOrgUnitParent = $('.historyPerField.organizationalUnitParent');
        $elementsHistoryOrgUnitParent.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-orgUnitParent').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });




        // ****************************************************************************************************************************
        // Office Location:
        // ****************************************************************************************************************************

        // Popover (History (per field): Name
        //----------------------------------------------------
        var $elementsHistoryName = $('.historyPerField.name');
        $elementsHistoryName.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-name').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });


        // Popover (History (per field): Building Code
        //----------------------------------------------------
        var $elementsHistoryBuildingCode = $('.historyPerField.buildingCode');
        $elementsHistoryBuildingCode.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-buildingCode').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });


        // Popover (History (per field): Address 1
        //----------------------------------------------------
        var $elementsHistoryAddress1 = $('.historyPerField.address1');
        $elementsHistoryAddress1.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-address1').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });


        // Popover (History (per field): Address 2
        //----------------------------------------------------
        var $elementsHistoryAddress2 = $('.historyPerField.address2');
        $elementsHistoryAddress2.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-address2').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });


        // Popover (History (per field): City
        //----------------------------------------------------
        var $elementsHistoryCity = $('.historyPerField.city');
        $elementsHistoryCity.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-city').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });


        // Popover (History (per field): State
        //----------------------------------------------------
        var $elementsHistoryState = $('.historyPerField.state');
        $elementsHistoryState.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-state').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });


        // Popover (History (per field): Postal Code
        //----------------------------------------------------
        var $elementsHistoryPostalCode = $('.historyPerField.zip');
        $elementsHistoryPostalCode.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-zip').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });


        // Popover (History (per field): Country
        //----------------------------------------------------
        var $elementsHistoryCountry = $('.historyPerField.country');
        $elementsHistoryCountry.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-history-title').html();
                }
                , content: function () {
                    return $('#popover-history-content-country').html();
                }
                , trigger: 'focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-history');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });

            $element.on('show.bs.popover', function (event) {
                var button = $(event.target);
                var popoverButton = $(this);

                var perFieldHistoryStatus = button.data('data-history-content-status');
                var perFieldHistoryDate = button.data('data-history-content-date');
                var perFieldHistoryValue = button.data('data-history-content-value');

                popoverButton.parents('body').find('#popover-history-content-status').text(perFieldHistoryStatus);
                popoverButton.parents('body').find('#popover-history-content-date').text(perFieldHistoryDate);
                popoverButton.parents('body').find('#popover-history-content-value').text(perFieldHistoryValue);
            });
        });








        // Popover (Legend (Record Source)
        //----------------------------------------------------
        var $elementsSourcelegend = $('.infoLegend');
        $elementsSourcelegend.each(function () {

            var $element = $(this);

            // Help: https://getbootstrap.com/docs/3.4/javascript/#js-sanitizer
            var v3DefaultWhiteList = $.fn.popover.Constructor.DEFAULTS.whiteList;
            v3DefaultWhiteList.table = [];
            v3DefaultWhiteList.thead = [];
            v3DefaultWhiteList.th = [];
            v3DefaultWhiteList.tbody = [];
            v3DefaultWhiteList.tr = [];
            v3DefaultWhiteList.td = [];
            v3DefaultWhiteList.input = ['type'];
            v3DefaultWhiteList.label = [];
            v3DefaultWhiteList.i = [];
            v3DefaultWhiteList.i = ['title'];

            $element.popover({
                html: true
                , placement: 'left'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-legend-title').html();
                }
                , content: function () {
                    return $('#popover-legend-content').html();
                }
                , trigger: 'hover click focus'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-legend');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });
        });



        // Popover (Comments)
        //----------------------------------------------------

        var $elementsComments = $('.comments');
        $elementsComments.each(function () {
            var $element = $(this);

            $element.popover({
                html: true
                , placement: 'top'
                , container: $('body') // This is just so the btn-group doesn't get messed up... also makes sorting the z-index issue easier
                , title: function () {
                    return $('#popover-comments-title').html();
                }
                , content: function () {
                    return $('#popover-comments-content').html();
                    //return $($(this).data('contentwrapper')).html();
                }
                , trigger: 'click'
            })
                .data('bs.popover')
                .tip()
                .addClass('popover-comments');

            $element.on('shown.bs.popover', function () {
                var popover = $element.data('bs.popover');
                if (typeof popover !== "undefined") {
                    var $tip = popover.tip();
                    zindex = $tip.css('z-index');

                    $tip.find('.close').bind('click', function () {
                        popover.hide();
                    });

                    $tip.mouseover(function () {
                        $tip.css('z-index', function () {
                            return zindex + 1;
                        });
                    })
                        .mouseout(function () {
                            $tip.css('z-index', function () {
                                return zindex;
                            });
                        });
                }
            });
        });




        //// Tooltip [Ellipsis]
        ////----------------------
        //$(".ellipsis[title]").each(function (i, obj) {

        //    $(this).tooltip({
        //        title: $(this).attr("title")
        //        , container: 'body'
        //        , placement: 'top'
        //        , trigger: 'hover'
        //    });

        //    $('body').on('hidden.bs.tooltip', function () {
        //        var tooltips = $('.tooltip').not('.in');
        //        if (tooltips) {
        //            tooltips.remove();
        //        }
        //    });

        //});


        // Tooltips [Regular]
        //----------------------------------------------------

        var elButton = $('button, i, span.text-danger, [title], img, span');
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


        var elA = $('a');
        $(elA).each(function (i, obj) {
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
        $(elA).each(function (i, obj) {
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


        var brandLogo = $('a.navbar-brand');
        $(brandLogo).each(function (i, obj) {
            $(this).tooltip({
                title: $(this).attr("title")
                , container: 'body'
                , placement: 'bottom'
                , trigger: 'hover'
            });

            $('body').on('hidden.bs.tooltip', function () {
                var tooltips = $('.tooltip').not('.in');
                if (tooltips) {
                    tooltips.remove();
                }
            });
        });

        // Tooltips [Navigation Menu]
        //----------------------------------------------------

        var navElButton = $('.sidebar-menu > button');
        //console.log(navElButton);
        $(navElButton).each(function (i, obj) {
            $(this).tooltip({
                title: $(this).attr("title")
                , container: 'body'
                , placement: 'right'
                , trigger: 'hover'
            });

            $('body').on('hidden.bs.tooltip', function () {
                var tooltips = $('.tooltip').not('.in');
                if (tooltips) {
                    tooltips.remove();
                }
            });
        });

        var navElA = $('.sidebar-menu.navigation ul > li > a');
        //console.log(navElA);
        $(navElA).each(function (i, obj) {
            $(this).tooltip({
                title: $(this).attr("title")
                , container: 'body'
                , placement: 'right'
                , trigger: 'hover'
            });

            $('body').on('hidden.bs.tooltip', function () {
                var tooltips = $('.tooltip').not('.in');
                if (tooltips) {
                    tooltips.remove();
                }
            });
        });



        $("body").tooltip({
            selector: '[rel="tooltip"]'
        });




        // Sidebar Menu Toggle
        // ---------------------------------------------------------------------
        $("#toggle-sidebar").click(function () {
            $(".page-wrapper").toggleClass("toggled");
        });
        
        var alterClass = function () {
            var ww = document.body.clientWidth;
            if (ww < 992) {
                $('.page-wrapper').removeClass('toggled');
            } else if (ww >= 993) {
                $('.page-wrapper').addClass('toggled');
            };
        };
        $(window).resize(function () {
            alterClass();
        });
        //Fire it when the page first loads:
        alterClass();




        // Sidebar Dropdown Menu Toggle
        // ---------------------------------------------------------------------
        $(".sidebar-dropdown > a").click(function () {

            //alert('Single-level');
            $(".sidebar-submenu").slideUp(200);

            if ($(this).parent().hasClass("active")) {
                $(".sidebar-dropdown").removeClass("active");
                $(this).parent().removeClass("active").slideDown(200);
            } else {
                $(".sidebar-dropdown").removeClass("active");
                $(this).next(".sidebar-submenu").slideDown(200);
                $(this).parent().addClass("active");
            }
        });

        // Sidebar Dropdown SUB-Menu Toggle
        // ---------------------------------------------------------------------
        $(".active.sidebar-dropdown .active.sidebar-dropdown > a").click(function () {

            //alert('Double-level');
            //$(".sidebar-submenu").slideUp(200);

            if ($(this).parent(".active.sidebar-dropdown").hasClass("active")) {
                $(".sidebar-dropdown").addClass("active").slideDown(200);
                $(this).parent().removeClass("active").slideDown(200);
            } else {
                $(".sidebar-dropdown").removeClass("active");
                $(this).next(".sidebar-submenu").slideDown(200);
                $(this).parent().addClass("active");
            }
        });




        // On Scroll - Change Hamburger Menu Background Color
        // ---------------------------------------------------------------------
        var $toggleSquare = $('#toggle-sidebar .fa-square');
        var $toggleBars = $('#toggle-sidebar .fa-bars');
        inView('.navbar-header').on('exit', function (el) {
            var color = $(el).attr('data-background-color');
            $toggleSquare.css('color', colors.primaryBlue);
            $toggleBars.css('color', colors.mainBG);
        });
        inView('.navbar-header').on('enter', function (el) {
            var color = $(el).attr('data-background-color');
            $toggleSquare.css('color', colors.neutralCoolGray);
            $toggleBars.css('color', colors.primaryBlue);
        });




        // Date Picker
        // ---------------------------------------------------------------------
        var dateEl = $('.input-group.date');
        var date = new Date();
        var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
        var dbValue = dateEl.val();

        var optComponent = {
            format: 'mm/dd/yyyy'
            , orientation: 'auto top'
            , todayHighlight: true
            , autoclose: true
            , todayBtn: 'linked'
            , showOnFocus: false
        };

        // COMPONENT
        $(dateEl).datepicker(optComponent);
        //$(dateEl).daterangepicker();
        //$(dateEl).datepicker('setDate', today);
        //console.log('Date: ' + dbValue);







        // Date Range Picker
        // ---------------------------------------------------------------------
        $(function () {

            var dateRangeControl = $('#throughputDateRange');
            var dateRangeControlSpan = $('#throughputDateRange span');

            var start = moment().subtract(29, 'days');
            var end = moment();

            function cb(start, end) {
                $(dateRangeControlSpan).html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
            };

            $(dateRangeControl).daterangepicker({
                startDate: start,
                endDate: end,
                opens: 'left', // inverse value -- due to parent container floating right.
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);

            cb(start, end);

        });






        //// Mask Money!
        //// ---------------------------------------------------------------------
        //$(function () {
        //    $('.currency').maskMoney(
        //        {
        //            thousands: ',',
        //            decimal: '.',
        //            prefix: '$',
        //            allowZero: false
        //        }
        //    );
        //});







        // Input Mask!
        // ---------------------------------------------------------------------
        //$(function () {
        //    $('.phone').inputmask('(999) 999-9999');
        //});







        // Editable Table Cells via: X-Editable
        // ------------------------------------------------------------

        // Inline:
        //$.fn.editable.defaults.mode = 'inline';

        //// Custom Buttons (https://github.com/vitalets/x-editable/issues/606):
        //$.fn.editableform.buttons =
        //    '<button type="submit" class="btn btn-primary btn-sm editable-submit">' +
        //    '<i class="fa fa-fw fa-sm fa-check"></i>' +
        //    '</button>' +
        //    '<button type="button" class="btn btn-default btn-sm editable-cancel">' +
        //    '<i class="fa fa-fw fa-sm fa-times"></i>' +
        //    '</button>';

        //var modelAttributesTable = $('#businessGlossaryTable');
        //var editInlineTable = $('#editInlineTable ');


        //// Edit Inline Table
        //var eiNameColumn = $('#editInlineTable tbody tr td:nth-child(1)');
        //$(eiNameColumn).editable({
        //    placement: 'top'
        //    , highlight: colors.secondaryLeaf
        //    , type: 'text'
        //    , pk: 1 // TODO: Need to 'get' / 'set' Primary Key dynamically from DB View.
        //    , title: 'Name'
        //});
        //var eiEmailColumn = $('#editInlineTable tbody tr td:nth-child(4)');
        //$(eiEmailColumn).editable({
        //    placement: 'top'
        //    , highlight: colors.secondaryLeaf
        //    , type: 'email'
        //    , pk: 1 // TODO: Need to 'get' / 'set' Primary Key dynamically from DB View.
        //    , title: 'Email'
        //});



        //// Model Attributes Table
        //var maNameColumn = $('#businessGlossaryTable tbody tr td:nth-child(1)');
        //$(maNameColumn).editable({
        //    placement: 'top'
        //    , highlight: colors.secondaryLeaf
        //    , type: 'text'
        //    , pk: 1 // TODO: Need to 'get' / 'set' Primary Key dynamically from DB View.
        //    , title: 'Name'
        //});
        //var maEmailColumn = $('#businessGlossaryTable tbody tr td:nth-child(2)');
        //$(maEmailColumn).editable({
        //    placement: 'top'
        //    , highlight: colors.secondaryLeaf
        //    , type: 'text'
        //    , pk: 1 // TODO: Need to 'get' / 'set' Primary Key dynamically from DB View.
        //    , title: 'Description'
        //});






        // Sortable List w/ Drag/Drop:
        // https://codepad.co/snippet/sortable-list-with-drag-and-drop
        // ------------------------------------------------------------

        var rowSize = 50; // => container height / number of items
        var container = document.querySelector(".reorder-list-container");
        var listItems = Array.from(document.querySelectorAll(".list-item")); // Array of elements
        var sortables = listItems.map(Sortable); // Array of sortables
        var total = sortables.length;

        //TweenLite.to(container, 0.5, { autoAlpha: 1 });

        function changeIndex(item, to) {

            // Change position in array
            arrayMove(sortables, item.index, to);

            // Change element's position in DOM. Not always necessary. Just showing how.
            if (to === total - 1) {
                container.appendChild(item.element);
            } else {
                var i = item.index > to ? to : to + 1;
                container.insertBefore(item.element, container.children[i]);
            }

            // Set index for each sortable
            sortables.forEach((sortable, index) => sortable.setIndex(index));
        }

        function Sortable(element, index) {

            var content = element.querySelector(".item-content");
            var order = element.querySelector(".order");

            var animation = TweenLite.to(content, 0.3, {
                boxShadow: "rgba(0,0,0,0.2) 0px 8px 22px 0px",
                force3D: true,
                scale: 1.03,
                cursor: "grabbing",
                paused: true
            });

            var dragger = new Draggable(element, {
                onDragStart: downAction,
                onRelease: upAction,
                onDrag: dragAction,
                cursor: "grab",
                type: "y"
            });

            // Public properties and methods
            var sortable = {
                dragger: dragger,
                element: element,
                index: index,
                setIndex: setIndex
            };

            TweenLite.set(element, { y: index * rowSize });

            function setIndex(index) {

                sortable.index = index;
                order.textContent = index + 1;

                // Don't layout if you're dragging
                if (!dragger.isDragging) layout();
            }

            function downAction() {
                animation.play();
                this.update();
            }

            function dragAction() {

                // Calculate the current index based on element's position
                var index = clamp(Math.round(this.y / rowSize), 0, total - 1);

                if (index !== sortable.index) {
                    changeIndex(sortable, index);
                }
            }

            function upAction() {
                animation.reverse();
                layout();
            }

            function layout() {
                TweenLite.to(element, 0.3, { y: sortable.index * rowSize });
            }

            return sortable;
        }

        // Changes an elements's position in array
        function arrayMove(array, from, to) {
            array.splice(to, 0, array.splice(from, 1)[0]);
        }

        // Clamps a value to a min/max
        function clamp(value, a, b) {
            return value < a ? a : (value > b ? b : value);
        }



        // Form Validation (Client-Side):

        //$(".content-container > form").validate({
        //    errorElement: "label",
        //    validClass: 'has-success',
        //    errorClass: 'has-error',
        //    errorPlacement: function (error, element) {
        //        // Add the `help-block` class to the error element
        //        error.addClass("help-block");
        //        element.parents('.form-group').addClass(errorClass).removeClass(validClass);
        //        element.parents('.form-group').addClass('has-feedback');
        //        element.parents('.form-group').find('.form-error').removeClass('hide');

        //        // Add `has-feedback` class to the parent div.form-group
        //        // in order to add icons to inputs
        //        //element.parents(".col-sm-5").addClass("has-feedback");
        //        element.parents('.form-group').addClass('has-error has-feedback').removeClass(validClass);

        //        if (element.prop("type") === "checkbox") {
        //            error.insertAfter(element.parent("label"));
        //        } else {
        //            error.insertAfter(element);
        //        }

        //        // Add the span element, if doesn't exists, and apply the icon classes to it.
        //        if (!element.next("span")[0]) {
        //            $("<span class='glyphicon glyphicon-remove form-control-feedback'></span>").insertAfter(element);
        //        }
        //    },
        //    success: function (label, element) {
        //        // Add the span element, if doesn't exists, and apply the icon classes to it.
        //        if (!$(element).next("span")[0]) {
        //            $("<span class='glyphicon glyphicon-ok form-control-feedback'></span>").insertAfter($(element));
        //        }
        //    },
        //    highlight: function (element, errorClass, validClass) {
        //        $(element).parents(".form-group").addClass(errorClass).removeClass(validClass);
        //        $(element).next("span").addClass("glyphicon-remove").removeClass("glyphicon-ok");
        //    },
        //    unhighlight: function (element, errorClass, validClass) {
        //        $(element).parents(".form-group").addClass(validClass).removeClass(errorClass);
        //        $(element).next("span").addClass("glyphicon-ok").removeClass("glyphicon-remove");
        //    }
        //});



        //// jQuery Validation
        //// ---------------------------------------------------------------------
        //var form = $('.content-container > form')
        //    , formData = $.data(form[0])
        //    , settings = formData.validator.settings
        //    // Store existing event handlers in local variables
        //    , oldErrorPlacement = settings.errorPlacement
        //    , oldSuccess = settings.success;

        //settings.errorPlacement = function (label, element) {
        //    // Call old handler so it can update the HTML
        //    oldErrorPlacement(label, element);

        //    // Add Bootstrap classes to newly added elements
        //    label.parents('.form-group').addClass('has-error has-feedback').removeClass('has-success');
        //    label.parents('.form-group').find('.form-control-feedback').addClass('fa-times').removeClass('fa-star');
        //    label.addClass('text-danger');
        //    label.parents('.form-group').find('.field-validation-error').addClass('hide')
        //    label.parents('.form-group').find('.form-error').removeClass('hide');
        //    label.parents('.form-group').find('.form-error').popover({
        //        html: true
        //        , container: $('body')
        //        , placement: 'top'
        //        , trigger: 'click'
        //        , content: function () {
        //            return $('.field-validation-error').html();
        //        }
        //    })
        //        .data('bs.popover')
        //        .tip()
        //        .addClass('error-form-popover');
        //};

        //// Validate Drop-down form fields.
        //$("select").on("select2:close", function (e) {
        //    $(this).valid();
        //});

        //settings.success = function (label, element) {
        //    // Remove error class from <div class="form-group">, but don't worry about
        //    // validation error messages as the plugin is going to remove it anyway
        //    label.parents('.form-group').removeClass('has-error').addClass('has-success has-feedback');
        //    label.parents('.form-group').find('.form-control-feedback').removeClass('fa-times');
        //    label.parents('.form-group').find('.form-error').addClass('hide');
        //    label.parents('.form-group').find('.form-error').popover('hide'); // TODO: Make this work

        //    // Call old handler to do rest of the work
        //    oldSuccess(label);
        //};


        ////// Get & Store current value:
        ////var editedClass = 'fa-pen-square';
        ////// Check if value has changed:
        ////$('form :input').on('input', function () {
        ////    //$(this).data("val", $(this).val());
        ////    var originalInputValue = $(this).on('focusin', function () {
        ////        console.log("Original value " + $(this).val());
        ////        $(this).data('val', $(this).val());
        ////    });
        ////    var changedValue = $(this).val();
        ////    $('#OriginalValue').html('Original Value: ' + originalInputValue);
        ////    $('#ChangedValue').html('Changed Value: ' + changedValue);
        ////    // If it has changed, set updated icon:
        ////    if (originalInputValue !== changedValue) {
        ////        $(this).parents('.form-group').find('.form-control-feedback').addClass(editedClass);
        ////    } else { // If changed back to original value, set icon back to star
        ////        $(this).parents('.form-group').find('.form-control-feedback').removeClass(editedClass);
        ////    };
        ////});

        //// Get & Store current value:
        //var originalInputValue = $('.content-container > form .form-control').val();
        //var editedClass = 'fa-pen-square';
        //// Check if value has changed:
        //$('#FirstName_Output').on('input', function () {
        //    var changedValue = $(this).val();
        //    $('#ChangedValue').html('Changed Value: ' + changedValue);
        //    // If it has changed, set updated icon:
        //    if (originalInputValue !== changedValue) {
        //        $(this).parents('.form-group').find('.form-control-feedback').addClass(editedClass);
        //    } else { // If changed back to original value, set icon back to star
        //        $(this).parents('.form-group').find('.form-control-feedback').removeClass(editedClass);
        //    };
        //});

        ////$(document).on('focusin', 'input', function () {
        ////    //console.log("Saving value " + $(this).val());
        ////    var originalInputValue = $(this).val();
        ////    $(this).data('val', $(this).val());
        ////    console.log("Original value " + originalInputValue);
        ////}).on('change', 'input', function () {
        ////    var prev = $(this).data('val');
        ////    var current = $(this).val();
        ////    //console.log("Original value " + originalInputValue);
        ////    console.log("Prev value " + prev);
        ////    console.log("New value " + current);
        ////});




    }
));
