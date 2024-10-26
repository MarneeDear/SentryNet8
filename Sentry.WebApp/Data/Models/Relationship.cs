using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("PersonRelationshipTypes", Schema = "MDS")]
    public class Relationship
    {
        [Key]
        [Column("MasterRecordId")]
        public string MasterRecordId { get; set; }

        [Column("RelationshipType")]
        public string RelationshipType { get; set; }
    }
}
