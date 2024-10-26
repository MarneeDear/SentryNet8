using Sentry.WebApp.Data.Models;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class QueueEntryCountsViewModel : BaseViewModel
    {
        public QueueEntryCountsViewModel() : base() { }

        public QueueEntryCountRow[] QueueEntryCountRows { get; set; }
    }

}
