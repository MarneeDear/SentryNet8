using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
	[Table("OfficeLocations_Detail", Schema = "Integration")]
	public class OfficeLocationDetail
	{
        [Column("RecordId")]
		public long? Id { get; set; }

        public int SystemId { get; set; }

        public string SystemName { get; set; }

        public DateTime? IntegrationDate { get; set; }

        public int IntegrationId { get; set; }

        public string IntegrationName { get; set; }

        public long? LineageKey { get; set; }

		[Column("SystemRecordId")]
		public string SourceRecordId { get; set; }

		public string RecordStatus { get; set; }

		#region Name

		[Column("Name")]
        public string Name { get; set; }

        [Column("Name_BusinessName")]
        public string Name_BusinessName { get; set; }

        [Column("Name_BusinessDescription")]
        public string Name_BusinessDescription { get; set; }

        [Column("Name_Source")]
        public string Name_Source { get; set; }

        [Column("Name_Category")]
        public string Name_Category { get; set; }

        [Column("Name_Status")]
        public string Name_Status { get; set; }

        [Column("Name_AttributeId")]
        public int? Name_FieldId { get; set; }

        #endregion

        #region BuildingCode

        [Column("BuildingCode")]
        public string BuildingCode { get; set; }

        [Column("BuildingCode_BusinessName")]
        public string BuildingCode_BusinessName { get; set; }

        [Column("BuildingCode_BusinessDescription")]
        public string BuildingCode_BusinessDescription { get; set; }

        [Column("BuildingCode_Source")]
        public string BuildingCode_Source { get; set; }

        [Column("BuildingCode_Category")]
        public string BuildingCode_Category { get; set; }

        [Column("BuildingCode_Status")]
        public string BuildingCode_Status { get; set; }

        [Column("BuildingCode_AttributeId")]
        public int? BuildingCode_FieldId { get; set; }

        #endregion

        #region Address 1

        [Column("Address1")]
        public string Address1 { get; set; }

        [Column("Address1_BusinessName")]
        public string Address1_BusinessName { get; set; }

        [Column("Address1_BusinessDescription")]
        public string Address1_BusinessDescription { get; set; }

        [Column("Address1_Source")]
        public string Address1_Source { get; set; }

        [Column("Address1_Category")]
        public string Address1_Category { get; set; }

        [Column("Address1_Status")]
        public string Address1_Status { get; set; }

        [Column("Address1_AttributeId")]
        public int? Address1_FieldId { get; set; }

        #endregion

        #region Address 2

        [Column("Address2")]
        public string Address2 { get; set; }

        [Column("Address2_BusinessName")]
        public string Address2_BusinessName { get; set; }

        [Column("Address2_BusinessDescription")]
        public string Address2_BusinessDescription { get; set; }

        [Column("Address2_Source")]
        public string Address2_Source { get; set; }

        [Column("Address2_Category")]
        public string Address2_Category { get; set; }

        [Column("Address2_Status")]
        public string Address2_Status { get; set; }

        [Column("Address2_AttributeId")]
        public int? Address2_FieldId { get; set; }

        #endregion

        #region City

        [Column("City")]
        public string City { get; set; }

        [Column("City_BusinessName")]
        public string City_BusinessName { get; set; }

        [Column("City_BusinessDescription")]
        public string City_BusinessDescription { get; set; }

        [Column("City_Source")]
        public string City_Source { get; set; }

        [Column("City_Category")]
        public string City_Category { get; set; }

        [Column("City_Status")]
        public string City_Status { get; set; }

        [Column("City_AttributeId")]
        public int? City_FieldId { get; set; }

        #endregion

        #region State

        [Column("State")]
        public string State { get; set; }

        [Column("State_BusinessName")]
        public string State_BusinessName { get; set; }

        [Column("State_BusinessDescription")]
        public string State_BusinessDescription { get; set; }

        [Column("State_Source")]
        public string State_Source { get; set; }

        [Column("State_Category")]
        public string State_Category { get; set; }

        [Column("State_Status")]
        public string State_Status { get; set; }

        [Column("State_AttributeId")]
        public int? State_FieldId { get; set; }

        #endregion

        #region State Master Id

        [Column("StateMasterId")]
        public int? StateMasterId { get; set; }

        [Column("StateMasterId_BusinessName")]
        public string StateMasterId_BusinessName { get; set; }

        [Column("StateMasterId_BusinessDescription")]
        public string StateMasterId_BusinessDescription { get; set; }

        [Column("StateMasterId_Source")]
        public string StateMasterId_Source { get; set; }

        [Column("StateMasterId_Category")]
        public string StateMasterId_Category { get; set; }

        [Column("StateMasterId_Status")]
        public string StateMasterId_Status { get; set; }

        [Column("StateMasterId_AttributeId")]
        public int? StateMasterId_FieldId { get; set; }

        #endregion

        #region Postal Code

        [Column("PostalCode")]
        public string PostalCode { get; set; }

        [Column("PostalCode_BusinessName")]
        public string PostalCode_BusinessName { get; set; }

        [Column("PostalCode_BusinessDescription")]
        public string PostalCode_BusinessDescription { get; set; }

        [Column("PostalCode_Source")]
        public string PostalCode_Source { get; set; }

        [Column("PostalCode_Category")]
        public string PostalCode_Category { get; set; }

        [Column("PostalCode_Status")]
        public string PostalCode_Status { get; set; }

        [Column("PostalCode_AttributeId")]
        public int? PostalCode_FieldId { get; set; }

        #endregion

        #region Country

        [Column("Country")]
        public string Country { get; set; }

        [Column("Country_BusinessName")]
        public string Country_BusinessName { get; set; }

        [Column("Country_BusinessDescription")]
        public string Country_BusinessDescription { get; set; }

        [Column("Country_Source")]
        public string Country_Source { get; set; }

        [Column("Country_Category")]
        public string Country_Category { get; set; }

        [Column("Country_Status")]
        public string Country_Status { get; set; }

		[Column("Country_AttributeId")]
        public int? Country_FieldId { get; set; }

        #endregion

        #region Country Master Id

        [Column("CountryMasterId")]
        public int? CountryMasterId { get; set; }

        [Column("CountryMasterId_BusinessName")]
        public string CountryMasterId_BusinessName { get; set; }

        [Column("CountryMasterId_BusinessDescription")]
        public string CountryMasterId_BusinessDescription { get; set; }

        [Column("CountryMasterId_Source")]
        public string CountryMasterId_Source { get; set; }

        [Column("CountryMasterId_Category")]
        public string CountryMasterId_Category { get; set; }

        [Column("CountryMasterId_Status")]
        public string CountryMasterId_Status { get; set; }

        [Column("CountryMasterId_AttributeId")]
        public int? CountryMasterId_FieldId { get; set; }

        #endregion
	}
}