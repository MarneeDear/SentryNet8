using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("Designations_List", Schema = "Integration")]
    public class DesignationRemediationList : BaseRemediationList
    {
        [Column("DesignationName")]
        public string DesignationName { get; internal set; }

        [Column("DesignationId")]
        public string DesignationId { get; internal set; }
    }
}