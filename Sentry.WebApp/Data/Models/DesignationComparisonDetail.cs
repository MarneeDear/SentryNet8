using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    public class DesignationComparisonDetail : BaseComparisonDetail
    {
        public string SourceRecordId { get; set; }

        public string DesignationId { get; set; }
        public string DesignationId_Compare { get; set; }

        public string DesignationName { get; set; }
        public string DesignationName_Compare { get; set; }

        public string Description { get; set; }
        public string Description_Compare { get; set; }

        public string DesignationTypeName { get; set; }
        public string DesignationTypeName_Compare { get; set; }

        public string StartDate { get; set; }
        public DateTime? StartDate_Compare { get; set; }

        public string EndDate { get; set; }
        public DateTime? EndDate_Compare { get; set; }

        public string KFSAccountCode { get; set; }
        public string KFSAccountCode_Compare { get; set; }

        public string VSECategoryName { get; set; }
        public string VSECategoryName_Compare { get; set; }

        public string GLOrganizationName { get; set; }
        public string GLOrganizationName_Compare { get; set; }

        public string DesignationUseTypeName { get; set; }
        public string DesignationUseTypeName_Compare { get; set; }

    }
}
