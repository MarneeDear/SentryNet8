// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
// ----------------------------------------------------------------------------

$(function () {
    var models = window["powerbi-client"].models;
    var reportContainer = $("#report-container").get(0);

    const advancedFilter = {
        $schema: "https://powerbi.com/product/schema#advanced",
        target: {
            table: FilterTable,
            column: FilterColumn
        },
        logicalOperator: "And",
        conditions: [
            {
                operator: "Is",
                value: FilterValue
            }
        ],
        filterType: models.FilterType.AdvancedFilter
    }

    const settings = {
        panes: {
            filters: {
                expanded: false,
                visible: false
            }
        }
    }

    $.ajax({
        type: "GET",
        url: "/reports/getembedinfo",
        data: {
            "reportId": ReportId
        },
        success: function (data) {
            embedParams = $.parseJSON(data);
            reportLoadConfig = {
                type: "report",
                tokenType: models.TokenType.Embed,
                accessToken: embedParams.EmbedToken.Token,
                // You can embed different reports as per your need
                embedUrl: embedParams.EmbedReport[0].EmbedUrl,
                // filters: [advancedFilter],
                settings: settings
                // Enable this setting to remove gray shoulders from embedded report
                // settings: {
                //     background: models.BackgroundType.Transparent
                // }
            };

            // Use the token expiry to regenerate Embed token for seamless end user experience
            // Refer https://aka.ms/RefreshEmbedToken
            tokenExpiry = embedParams.EmbedToken.Expiration;

            // Embed Power BI report when Access token and Embed URL are available
            var report = powerbi.embed(reportContainer, reportLoadConfig);
            // let reportFilters = await page.getFilters();
            // report.updateFilters(models.FiltersOperations.RemoveAll);

            // Clear any other loaded handler events
            report.off("loaded");

            // Triggers when a report schema is successfully loaded
            report.on("loaded", function (event) {
                report.getFilters()
                    .then(filters => {
                        if (filters != undefined && filters.length != 0) {
                            filters.push(advancedFilter);
                            return report.setFilters([advancedFilter]);
                        }
                    });
                console.log("Report loaded successful");
            });

            // Clear any other rendered handler events
            report.off("rendered");

            // Triggers when a report is successfully embedded in UI
            report.on("rendered", function () {
                console.log("Report rendered successful");
            });

            // Clear any other error handler events
            report.off("error");

            // Handle embed errors
            report.on("error", function (event) {
                var errorMsg = event.detail;

                // Use errorMsg variable to log error in any destination of choice
                console.error(errorMsg);
                return;
            });
        },
        error: function (err) {

            // Show error container
            var errorContainer = $(".error-container");
            $(".embed-container").hide();
            errorContainer.show();

            // Format error message
            var errMessageHtml = "<strong> Error Details: </strong> <br/>" + err.responseText;
            errMessageHtml = errMessageHtml.split("\n").join("<br/>");

            // Show error message on UI
            errorContainer.append(errMessageHtml);
        }
    });
});