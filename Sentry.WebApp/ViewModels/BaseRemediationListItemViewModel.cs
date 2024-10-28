using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class BaseRemediationListItemViewModel
    {
        public string Id { get; set; }

        public int? SystemId { get; set; }

        public string SystemName { get; set; }

        public string ErrorCategories { get; set; }

        public int? ErrorCount { get; set; }

        //[DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
        //[DataType(DataType.Date)]
        public DateTime? IntegrationDate { get; set; }

        public int? IntegrationId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string RecordStatus { get; set; }
    }
}
