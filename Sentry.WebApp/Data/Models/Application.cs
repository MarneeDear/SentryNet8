namespace Sentry.WebApp.Data.Models
{

    public class Application
    {
        public string Name { get; set; }
        public string InboundQueueProcessor { get; set; }
        public string OutboundQueueProcessor { get; set; }
    }

}
