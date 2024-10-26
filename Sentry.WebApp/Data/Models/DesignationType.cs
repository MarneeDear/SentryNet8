using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("DesignationTypes", Schema = "MDS")]
    public class DesignationType
    {
        //TODOD no masterId
        public string DesignationTypeName { get; set; }

        [Key]
        public string DesignationTypeCode { get; set; }
    }
}
