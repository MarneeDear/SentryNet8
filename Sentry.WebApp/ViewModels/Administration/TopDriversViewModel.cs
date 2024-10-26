using Sentry.WebApp.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class TopDriversViewModel : BaseViewModel
    {
        public TopDriversViewModel() : base() { }

        public IEnumerable<LogEntry> TopDrivers { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeFrame TimeFrame { get; set; }

        public List<SelectListItem> TimeFrames { get; set; }

    }

    public enum TimeFrame
    {
        None,
        Last7Days,
        ThisMonth,
        Last30Days,
        YearToDate,
        All
    }



}
