using System;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
	public class OrganizationalUnitSourceDetail : BaseSourceDetail
	{
		public string OrganizationalUnitName_SourceValue { get; set; }
		public string OrganizationalUnitName_CurrentValue { get; set; }
		public string OrganizationalUnitName_Source { get; set; }
		public string OrganizationalUnitName_Status { get; set; }
		public int? OrganizationalUnitName_AttributeId { get; set; }
		public string OrganizationalUnitCode_SourceValue { get; set; }
		public string OrganizationalUnitCode_CurrentValue { get; set; }
		public string OrganizationalUnitCode_Source { get; set; }
		public string OrganizationalUnitCode_Status { get; set; }
		public int? OrganizationalUnitCode_AttributeId { get; set; }
	}
}