using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("StudentAcademicInvolvementType", Schema = "MDS")]
    public class StudentAcademicInvolvementType
    {

        public string AcademicInvolvementTypeName { get; set; }

        [Key]
        public string AcademicInvolvementTypeCode { get; set; }
    }
}
