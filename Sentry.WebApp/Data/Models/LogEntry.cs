using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class LogEntry
    {
        public int MessageCount { get; set; }

        public string MessageText { get; set; }

        public string Logger { get; set; }

        public string Exception { get; set; }

    }
}
