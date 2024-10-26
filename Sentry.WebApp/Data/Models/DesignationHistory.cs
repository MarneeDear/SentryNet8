using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sentry.WebApp.Data.Models
{
    //[Table("Designations_Base", Schema = "Integration")]
    public class DesignationHistory : BaseHistory
    {
        public string DesignationId { get; set; }
        public string DesignationId_Status { get; set; }
        public int? DesignationId_AttributeId { get; set; }


        public string DesignationName { get; set; }
        public string DesignationName_Status { get; set; }
        public int? DesignationName_AttributeId { get; set; }


        public string StartDate { get; set; }
        public string StartDate_Status { get; set; }
        public int? StartDate_AttributeId { get; set; }


        public string EndDate { get; set; }
        public string EndDate_Status { get; set; }
        public int? EndDate_AttributeId { get; set; }


        public string DesignationTypeName { get; set; }
        public string DesignationTypeName_Status { get; set; }
        public int? DesignationTypeName_AttributeId { get; set; }

        public string DesignationTypeMasterId { get; set; }
        public string DesignationTypeMasterId_Status { get; set; }
        public int? DesignationTypeMasterId_AttributeId { get; set; }
    }
}
