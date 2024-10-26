using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    public class BaseComparisonDetail
    {
        [Key]
        public long RecordId { get; set; }

        public int IntegrationId { get; set; }
        public DateTime? IntegrationDate { get; set; }

        public string System { get; set; }
        public int SystemId { get; set; }
    }
}
