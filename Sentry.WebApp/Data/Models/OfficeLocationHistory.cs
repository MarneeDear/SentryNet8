using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
	[Table("OfficeLocations_History", Schema = "Integration")]
	public class OfficeLocationHistory
	{
		[Key]
		public long? RecordId { get; set; }

		public string SystemName { get; set; }

		public int SystemId { get; set; }

		public DateTime IntegrationDate { get; set; }

		public string IntegrationName { get; set; }

		public int IntegrationId { get; set; }

		public long? LineageKey { get; set; }

		public string SystemRecordId { get; set; }

		public string Name { get; set; }

		public string Name_BusinessName { get; set; }

		public string Name_BusinessDescription { get; set; }

		public string Name_Source { get; set; }

		public string Name_Category { get; set; }

		public int Name_AttributeId { get; set; }

		public string Name_Status { get; set; }

		public string BuildingCode { get; set; }

		public string BuildingCode_BusinessName { get; set; }

		public string BuildingCode_BusinessDescription { get; set; }

		public string BuildingCode_Source { get; set; }

		public string BuildingCode_Category { get; set; }

		public int? BuildingCode_AttributeId { get; set; }

		public string BuildingCode_Status { get; set; }

		public string Address1 { get; set; }

		public string Address1_BusinessName { get; set; }

		public string Address1_BusinessDescription { get; set; }

		public string Address1_Source { get; set; }

		public string Address1_Category { get; set; }

		public int Address1_AttributeId { get; set; }

		public string Address1_Status { get; set; }

		public string Address2 { get; set; }

		public string Address2_BusinessName { get; set; }

		public string Address2_BusinessDescription { get; set; }

		public string Address2_Source { get; set; }

		public string Address2_Category { get; set; }

		public int Address2_AttributeId { get; set; }

		public string Address2_Status { get; set; }

		public string City { get; set; }

		public string City_BusinessName { get; set; }

		public string City_BusinessDescription { get; set; }

		public string City_Source { get; set; }

		public string City_Category { get; set; }

		public int City_AttributeId { get; set; }

		public string City_Status { get; set; }

		public string State { get; set; }

		public string State_BusinessName { get; set; }

		public string State_BusinessDescription { get; set; }

		public string State_Source { get; set; }

		public string State_Category { get; set; }

		public int? State_AttributeId { get; set; }

		public string State_Status { get; set; }

		public string PostalCode { get; set; }

		public string PostalCode_BusinessName { get; set; }

		public string PostalCode_BusinessDescription { get; set; }

		public string PostalCode_Source { get; set; }

		public string PostalCode_Category { get; set; }

		public int PostalCode_AttributeId { get; set; }

		public string PostalCode_Status { get; set; }

		public string Country { get; set; }

		public string Country_BusinessName { get; set; }

		public string Country_BusinessDescription { get; set; }

		public string Country_Source { get; set; }

		public string Country_Category { get; set; }

		public int Country_AttributeId { get; set; }

		public string Country_Status { get; set; }

		public DateTime? RecordDate { get; set; }

		public string RecordStatus { get; set; }
	}
}
