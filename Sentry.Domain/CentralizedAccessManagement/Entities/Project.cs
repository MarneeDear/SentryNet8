using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Sentry.Domain.CentralizedAccessManagement.Entities
{
    public class Project
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectPurpose { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string Status { get; set; }
        public string OrganizationCode { get; set; }
    }

    public class ProjectResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public IEnumerable<Project> Projects { get; set; }

    }

    public class ProjectDetails
    {
        public string ProjectId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }//creation_date
        public string Purpose { get; set; }
        public string Notes { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string StudentAward { get; set; }
        public string UAAccountCode { get; set; }
        public DateTime? CurrentBalanceDate { get; set; }
        public decimal? CurrentBalance { get; set; }
        public DateTime? PastValuesDate { get; set; }//ending date
        public decimal PastBalance { get; set; }//ending balance
        public decimal FmvAmount { get; set; }//fair market value
        public string FmvDate { get; set; }//fair market date 
        public decimal HistorticDollarValue { get; set; }
        public decimal ProjectedPayout1 { get; set; }
        public string ProjectedPayoutYear1 { get; set; }
        public decimal ProjectedPayout2 { get; set; }
        public string ProjectedPayoutYear2 { get; set; }
        public bool Scholarship { get; set; }
        public int? FeProjectId { get; set; }
        public string DesignationType { get; set; }
    }

    public class ProjectDetailsResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public ProjectDetails Project { get; set; }

    }
}
