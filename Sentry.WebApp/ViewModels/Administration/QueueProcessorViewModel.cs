using Sentry.WebApp.Data.Models;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class QueueProcessorViewModel : BaseViewModel
    {
        public QueueProcessorViewModel() : base() { }

        public string ServerName { get; set; }
        public List<ApplicationQueueProcessor> ApplicationQueueProcessors { get; set; }
    }

    public class ApplicationQueueProcessor
    {
        public string Name { get; set; }

        public QueueProcessor InboundQueueProcessor { get; set; }

        public QueueProcessor OutboundQueueProcessor { get; set; }

    }

    public class QueueProcessor
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public QueueProcessorStatus Status { get; set; }
    }

    public enum QueueProcessorStatus
    {
        Stopped = 1,
        StartPending = 2,
        StopPending = 3,
        Running = 4,
        ContinuePending = 5,
        PausePending = 6,
        Paused = 7
    }
}
