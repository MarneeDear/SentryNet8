using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels.Reports
{
    public class PowerBiReportViewModel : BaseViewModel
    {
        public Guid ReportId { get; set; }
        public string ReportDisplayName { get; set; }
        public string FilterTable { get; set; }
        public string FilterColumn { get; set; }
        public string FilterValue { get; set; }
    }
    public class ReportViewModel : BaseViewModel
    {
        public string TableauId { get; set; }
        public string Environment { get; set; }
        public string ReportName { get; set; }
        public string ReportGuide { get; set; }
        public string ReportDisplayName { get; set; }
        public int ReportType { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }

    public class ReportItem
    {
        public string Report { get; set; }
        public string DisplayName { get; set; }
        public int ReportGuideId { get; set; }
    }

    public class ReportListViewModel : BaseViewModel
    {
        //List of reports
        public IEnumerable<ReportItem> ReportItems { get; set; }
        public IEnumerable<ReportItem> PowerBiItems { get; set; }
    }
}
