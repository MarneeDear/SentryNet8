using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
	public class OrganizationalUnitPossibleMatch
    {
        [Key]
        public string MasterId { get; set; }
        public int MatchConfidence { get; set; }
        public string OrganizationalUnitName{ get; set; }
        public string OrganizationalUnitCode { get; set; }
        public string OrganizationalUnitType { get; set; }
        public string OrganizationName { get; set; }
    }
}
