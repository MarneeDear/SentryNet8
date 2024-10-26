using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class IntegrationsStatusesViewModel : BaseViewModel
    {
        public IntegrationsStatusesViewModel() : base() { }

        public string IterateOver { get; set; }

        public string Server { get; set; }

        public string Integration { get; set; }

        public string System { get; set; }

        public IntegrationStatusData[] IntegrationStatuses { get; set; }

        public List<SelectListItem> Systems { get; set; }

        public List<SelectListItem> Integrations { get; set; }

        public int SystemCount { get; set; }

        public int IntegrationCount { get; set; }
    }


    public class IntegrationStatusData
    {
        public string SystemName { get; set; }

        public string IntegrationName { get; set; }

        public IntegrationTrigger StagingTableTrigger { get; set; }

        public IntegrationTrigger GoodTableTrigger { get; set; }

        public IntegrationTrigger BadTableTrigger { get; set; }

    }

    public class IntegrationTrigger
    {
        public string Schema { get; set; }

        public string Table { get; set; }

        public TriggerState CurrentState { get; set; }

        public bool ActionIsEnable { get; set; }

        public string IsChecked { get; set; }
    }

    public enum TriggerState
    {
        Disabled = 0,
        Enabled = 1
    }
}
