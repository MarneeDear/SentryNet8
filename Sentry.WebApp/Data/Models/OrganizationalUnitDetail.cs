using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
	[Table("OrganizationalUnits_Detail", Schema = "Integration")]
	public class OrganizationalUnitDetail : BaseDetail
	{

        #region Name

        public string Name { get; set; }

        public string Name_BusinessName { get; set; }

        public string Name_BusinessDescription { get; set; }

        public string Name_Source { get; set; }

        public string Name_Category { get; set; }

        public string Name_Status { get; set; }

        public int? Name_AttributeId { get; set; }

        #endregion

        #region Code

        public string Code { get; set; }

        public string Code_BusinessName { get; set; }

        public string Code_BusinessDescription { get; set; }

        public string Code_Source { get; set; }

        public string Code_Category { get; set; }

        public string Code_Status { get; set; }

        public int? Code_AttributeId { get; set; }

        #endregion

        #region Organizational Unit Type

        public string OrganizationalUnitType { get; set; }

        public string OrganizationalUnitType_BusinessName { get; set; }

        public string OrganizationalUnitType_BusinessDescription { get; set; }

        public string OrganizationalUnitType_Source { get; set; }

        public string OrganizationalUnitType_Category { get; set; }

        public string OrganizationalUnitType_Status { get; set; }

        public int? OrganizationalUnitType_AttributeId { get; set; }

        #endregion


        #region Parent Organizational Unit Name
        public string ParentOrganizationalUnitName { get; set; }
        
        public string ParentOrganizationalUnitName_BusinessName { get; set; }

        public string ParentOrganizationalUnitName_BusinessDescription { get; set; }

        public string ParentOrganizationalUnitName_Source { get; set; }

        public string ParentOrganizationalUnitName_Category { get; set; }

        public string ParentOrganizationalUnitName_Status { get; set; }

        public int? ParentOrganizationalUnitName_AttributeId { get; set; }
        #endregion

        #region Parent Organizational Unit Code

        public string ParentOrganizationalUnitCode { get; set; }

        public string ParentOrganizationalUnitCode_BusinessName { get; set; }

        public string ParentOrganizationalUnitCode_BusinessDescription { get; set; }

        public string ParentOrganizationalUnitCode_Source { get; set; }

        public string ParentOrganizationalUnitCode_Category { get; set; }

        public string ParentOrganizationalUnitCode_Status { get; set; }

        public int? ParentOrganizationalUnitCode_AttributeId { get; set; }

        #endregion

        #region Parent Organizational Unit Type

        public string ParentOrganizationalUnitType { get; set; }

        public string ParentOrganizationalUnitType_BusinessName { get; set; }

        public string ParentOrganizationalUnitType_BusinessDescription { get; set; }

        public string ParentOrganizationalUnitType_Source { get; set; }

        public string ParentOrganizationalUnitType_Category { get; set; }

        public string ParentOrganizationalUnitType_Status { get; set; }

        public int? ParentOrganizationalUnitType_AttributeId { get; set; }

        #endregion

        #region Parent Organizational Unit Master Id
        public string ParentOrganizationalUnitMasterId { get; set; }

        public string ParentOrganizationalUnitMasterId_BusinessName { get; set; }

        public string ParentOrganizationalUnitMasterId_BusinessDescription { get; set; }

        public string ParentOrganizationalUnitMasterId_Source { get; set; }

        public string ParentOrganizationalUnitMasterId_Category { get; set; }

        public string ParentOrganizationalUnitMasterId_Status { get; set; }

        public int? ParentOrganizationalUnitMasterId_AttributeId { get; set; }
        #endregion

        
        #region Organization Name

        public string OrganizationName { get; set; }
        
        public string OrganizationName_BusinessName { get; set; }

        public string OrganizationName_BusinessDescription { get; set; }

        public string OrganizationName_Source { get; set; }

        public string OrganizationName_Category { get; set; }

        public string OrganizationName_Status { get; set; }

        
        public int? OrganizationName_AttributeId { get; set; }

        #endregion

        #region Organization Code

        public string OrganizationCode { get; set; }

        public string OrganizationCode_BusinessName { get; set; }

        public string OrganizationCode_BusinessDescription { get; set; }

        public string OrganizationCode_Source { get; set; }

        public string OrganizationCode_Category { get; set; }

        public string OrganizationCode_Status { get; set; }

        public int? OrganizationCode_AttributeId { get; set; }

        #endregion

        #region Organization Master Id
        public string OrganizationMasterId { get; set; }

        public string OrganizationMasterId_BusinessName { get; set; }

        public string OrganizationMasterId_BusinessDescription { get; set; }

        public string OrganizationMasterId_Source { get; set; }

        public string OrganizationMasterId_Category { get; set; }

        public string OrganizationMasterId_Status { get; set; }

        public int? OrganizationMasterId_AttributeId { get; set; }

        #endregion
    }
}