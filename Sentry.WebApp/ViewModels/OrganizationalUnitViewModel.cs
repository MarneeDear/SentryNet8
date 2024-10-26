using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class OrganizationalUnitViewModel : BaseIntegrationViewModel
	{

		public OrganizationalUnitViewModel() : base() { }


        #region OrganizationalUnit Name
        public string OrganizationalUnitName { get; set; }

		public string OrganizationalUnitName_Status { get; set; }

		public string OrganizationalUnitName_BusinessName { get; set; }

		public string OrganizationalUnitName_BusinessDescription { get; set; }

		public string OrganizationalUnitName_Source { get; set; }

		public string OrganizationalUnitName_OriginalValue { get; set; }

        public int? OrganizationalUnitName_AttributeId { get; set; }
        #endregion

        #region OrganizationalUnit Code
        public string OrganizationalUnitCode { get; set; }

		public string OrganizationalUnitCode_Status { get; set; }

		public string OrganizationalUnitCode_BusinessName { get; set; }

		public string OrganizationalUnitCode_BusinessDescription { get; set; }

		public string OrganizationalUnitCode_Source { get; set; }

		public string OrganizationalUnitCode_OriginalValue { get; set; }

        public int? OrganizationalUnitCode_AttributeId { get; set; }
        #endregion

        #region OrganizationalUnit Type
        public string OrganizationalUnitType { get; set; }

        public string OrganizationalUnitType_Status { get; set; }

        public string OrganizationalUnitType_BusinessName { get; set; }

        public string OrganizationalUnitType_BusinessDescription { get; set; }

        public string OrganizationalUnitType_Source { get; set; }

        public string OrganizationalUnitType_OriginalValue { get; set; }

        public int? OrganizationalUnitType_AttributeId { get; set; }
        #endregion


        #region Parent OrganizationalUnit Name
        public string ParentOrganizationalUnitName { get; set; }

		public string ParentOrganizationalUnitName_Status { get; set; }
        
        public string ParentOrganizationalUnitName_BusinessName { get; set; }

		public string ParentOrganizationalUnitName_BusinessDescription { get; set; }

		public string ParentOrganizationalUnitName_Source { get; set; }

		public string ParentOrganizationalUnitName_OriginalValue { get; set; }

        public int? ParentOrganizationalUnitName_AttributeId { get; set; }
        #endregion

        #region Parent OrganizationalUnit Code

        public string ParentOrganizationalUnitCode { get; set; }

        public string ParentOrganizationalUnitCode_Status { get; set; }

        public string ParentOrganizationalUnitCode_BusinessName { get; set; }

        public string ParentOrganizationalUnitCode_BusinessDescription { get; set; }

        public string ParentOrganizationalUnitCode_Source { get; set; }

        public string ParentOrganizationalUnitCode_OriginalValue { get; set; }

        public int? ParentOrganizationalUnitCode_AttributeId { get; set; }

        #endregion

        #region Parent OrganizationalUnit Type

        public string ParentOrganizationalUnitType { get; set; }

        public string ParentOrganizationalUnitType_Status { get; set; }

        public string ParentOrganizationalUnitType_BusinessName { get; set; }

        public string ParentOrganizationalUnitType_BusinessDescription { get; set; }

        public string ParentOrganizationalUnitType_Source { get; set; }

        public int? ParentOrganizationalUnitType_FieldId { get; set; }

        public string ParentOrganizationalUnitType_OriginalValue { get; set; }

        public int? ParentOrganizationalUnitType_AttributeId { get; set; }

        #endregion

        #region Parent OrganizationalUnit Master Id
        public string ParentOrganizationalUnitMasterId { get; set; }

        public string ParentOrganizationalUnitMasterId_Status { get; set; }

        public string ParentOrganizationalUnitMasterId_BusinessName { get; set; }

        public string ParentOrganizationalUnitMasterId_BusinessDescription { get; set; }

        public string ParentOrganizationalUnitMasterId_Source { get; set; }

        public string ParentOrganizationalUnitMasterId_OriginalValue { get; set; }

        public int? ParentOrganizationalUnitMasterId_AttributeId { get; set; }
        #endregion


        #region Organization Name
        public string OrganizationName { get; set; }

		public string OrganizationName_Status { get; set; }
        
        public string OrganizationName_BusinessName { get; set; }

		public string OrganizationName_BusinessDescription { get; set; }

		public string OrganizationName_Source { get; set; }

		public string OrganizationName_OriginalValue { get; set; }

        public int? OrganizationName_AttributeId { get; set; }
        #endregion

        #region Organization Code
        public string OrganizationCode { get; set; }

        public string OrganizationCode_Status { get; set; }

        public string OrganizationCode_BusinessName { get; set; }

        public string OrganizationCode_BusinessDescription { get; set; }

        public string OrganizationCode_Source { get; set; }

        public string OrganizationCode_OriginalValue { get; set; }

        public int? OrganizationCode_AttributeId { get; set; }
        #endregion

        #region Organization Master Id
        public string OrganizationMasterId { get; set; }

        public string OrganizationMasterId_Status { get; set; }

        public string OrganizationMasterId_BusinessName { get; set; }

        public string OrganizationMasterId_BusinessDescription { get; set; }

        public string OrganizationMasterId_Source { get; set; }

        public string OrganizationMasterId_OriginalValue { get; set; }

        public int? OrganizationMasterId_AttributeId { get; set; }
        #endregion

        #region Drop-Downs
        // Organizations
        public List<SelectListItem> OrganizationList { get; set; }

        // organizational Unit Parents
		public List<SelectListItem> OrganizationalUnitParentList { get; set; }

        // organizational Unit Type
        //public List<SelectListItem> OrganizationalUnitTypeList { get; set; }
        #endregion


        public List<OrganizationalUnitHistoryViewModel> HistoryData { get; set; }

		public List<OrganizationalUnitRemediationListItemViewModel> RemediationList { get; set; }

		public override bool IsValid()
		{
			return base.IsValid();
		}
	}
}
