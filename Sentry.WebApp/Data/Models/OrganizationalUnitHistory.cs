using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sentry.WebApp.Data.Models
{
	[Table("OrganizationalUnits_History", Schema = "Integration")]
	public class OrganizationalUnitHistory : BaseHistory
	{
        #region OrganizationalUnit Name
        public string Name { get; set; }

		public string Name_BusinessName { get; set; }

		public string Name_BusinessDescription { get; set; }

		public string Name_Source { get; set; }

		public string Name_Category { get; set; }

		public int Name_AttributeId { get; set; }

		public string Name_Status { get; set; }
        #endregion

        #region OrganizationalUnit Code
        public string Code { get; set; }

		public string Code_BusinessName { get; set; }

		public string Code_BusinessDescription { get; set; }

		public string Code_Source { get; set; }

		public string Code_Category { get; set; }

		public int Code_AttributeId { get; set; }

		public string Code_Status { get; set; }
        #endregion

        #region OrganizationalUnit Type
        public string OrganizationalUnitType { get; set; }

        public string OrganizationalUnitType_BusinessName { get; set; }

        public string OrganizationalUnitType_BusinessDescription { get; set; }

        public string OrganizationalUnitType_Source { get; set; }

        public string OrganizationalUnitType_Category { get; set; }

        public string OrganizationalUnitType_Status { get; set; }
        #endregion

        #region OrganizationalUnit Parent Name
        public string ParentOrganizationalUnitName { get; set; }

		public string ParentOrganizationalUnitName_BusinessName { get; set; }

		public string ParentOrganizationalUnitName_BusinessDescription { get; set; }

		public string ParentOrganizationalUnitName_Source { get; set; }

		public string ParentOrganizationalUnitName_Category { get; set; }

        public string ParentOrganizationalUnitName_Status { get; set; }
        #endregion

        #region OrganizationalUnit Parent Code
        public string ParentOrganizationalUnitCode { get; set; }

        public string ParentOrganizationalUnitCode_BusinessName { get; set; }

        public string ParentOrganizationalUnitCode_BusinessDescription { get; set; }

        public string ParentOrganizationalUnitCode_Source { get; set; }

        public string ParentOrganizationalUnitCode_Category { get; set; }
        #endregion

        #region OrganizationalUnit Parent Type
        public string ParentOrganizationalUnitType { get; set; }

        public string ParentOrganizationalUnitType_BusinessName { get; set; }

        public string ParentOrganizationalUnitType_BusinessDescription { get; set; }

        public string ParentOrganizationalUnitType_Source { get; set; }

        public string ParentOrganizationalUnitType_Category { get; set; }
        #endregion

        #region OrganizationalUnit Parent Master Id
        public string ParentOrganizationalUnitMasterId { get; set; }

		public string ParentOrganizationalUnitMasterId_BusinessName { get; set; }

		public string ParentOrganizationalUnitMasterId_BusinessDescription { get; set; }

		public string ParentOrganizationalUnitMasterId_Source { get; set; }

		public string ParentOrganizationalUnitMasterId_Category { get; set; }

        public string ParentOrganizationalUnitMasterId_Status { get; set; }
        #endregion

        #region Organization Name
        public string OrganizationName { get; set; }

		public string OrganizationName_BusinessName { get; set; }

		public string OrganizationName_BusinessDescription { get; set; }

		public string OrganizationName_Source { get; set; }

		public string OrganizationName_Category { get; set; }

        public string OrganizationName_Status { get; set; }
        #endregion

        #region Organization Code
        public string OrganizationCode { get; set; }

		public string OrganizationCode_BusinessName { get; set; }

		public string OrganizationCode_BusinessDescription { get; set; }

		public string OrganizationCode_Source { get; set; }

		public string OrganizationCode_Category { get; set; }
        #endregion

        #region Organization Master Id
        public string OrganizationMasterId { get; set; }

        public string OrganizationMasterId_Status { get; set; }

        public string OrganizationMasterId_BusinessName { get; set; }

        public string OrganizationMasterId_BusinessDescription { get; set; }

        public string OrganizationMasterId_Source { get; set; }

        public string OrganizationMasterId_Category { get; set; }
        #endregion

	}
}
