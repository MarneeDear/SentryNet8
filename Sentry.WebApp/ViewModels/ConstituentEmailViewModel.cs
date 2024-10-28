using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentEmailViewModel : BaseIntegrationViewModel
    {
        public ConstituentEmailViewModel() : base() { }

        public bool IsReadOnly { get; set; }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }




        public string ConstituentSourceSystemRecordId { get; set; }
        public string ConstituentSourceSystemRecordId_BusinessName { get; set; }
        public string ConstituentSourceSystemRecordId_BusinessDescription { get; set; }
        public int? ConstituentSourceSystemRecordId_AttributeId { get; set; }
        public string ConstituentSourceSystemRecordId_OriginalValue { get; set; }
        public string ConstituentSourceSystemRecordId_Status { get; set; }
        public string ConstituentSourceSystemRecordId_Source { get; set; }
        public bool ConstituentSourceSystemRecordId_IsReadOnly { get; set; }

        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public int? FirstName_AttributeId { get; set; }
        public string FirstName_OriginalValue { get; set; }
        public string FirstName_Status { get; set; }
        public string FirstName_Source { get; set; }
        public bool FirstName_IsReadOnly { get; set; }

        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public int? LastName_AttributeId { get; set; }
        public string LastName_OriginalValue { get; set; }
        public string LastName_Status { get; set; }
        public string LastName_Source { get; set; }
        public bool LastName_IsReadOnly { get; set; }

        public string UAPersonId { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public int? UAPersonId_AttributeId { get; set; }
        public string UAPersonId_OriginalValue { get; set; }
        public string UAPersonId_Status { get; set; }
        public string UAPersonId_Source { get; set; }
        public bool UAPersonId_IsReadOnly { get; set; }

        public string ConstituentMasterId { get; set; }
        public string ConstituentMasterId_BusinessName { get; set; }
        public string ConstituentMasterId_BusinessDescription { get; set; }
        public int? ConstituentMasterId_AttributeId { get; set; }
        public string ConstituentMasterId_OriginalValue { get; set; }
        public string ConstituentMasterId_Status { get; set; }
        public string ConstituentMasterId_Source { get; set; }
        public bool ConstituentMasterId_IsReadOnly { get; set; }



        public string EmailAddress { get; set; }
        public string EmailAddress_BusinessName { get; set; }
        public string EmailAddress_BusinessDescription { get; set; }
        public int? EmailAddress_AttributeId { get; set; }
        public string EmailAddress_OriginalValue { get; set; }
        public string EmailAddress_Status { get; set; }
        public string EmailAddress_Source { get; set; }
        public bool EmailAddress_IsReadOnly { get; set; }

        public string EmailAddressMasterId { get; set; }
        public string EmailAddressMasterId_BusinessName { get; set; }
        public string EmailAddressMasterId_BusinessDescription { get; set; }
        public int? EmailAddressMasterId_AttributeId { get; set; }
        public string EmailAddressMasterId_OriginalValue { get; set; }
        public string EmailAddressMasterId_Status { get; set; }
        public string EmailAddressMasterId_Source { get; set; }
        public bool EmailAddressMasterId_IsReadOnly { get; set; }



        public string EmailAddressUseType { get; set; }
        public string EmailAddressUseType_BusinessName { get; set; }
        public string EmailAddressUseType_BusinessDescription { get; set; }
        public int? EmailAddressUseType_AttributeId { get; set; }
        public string EmailAddressUseType_OriginalValue { get; set; }
        public string EmailAddressUseType_Status { get; set; }
        public string EmailAddressUseType_Source { get; set; }
        public bool EmailAddressUseType_IsReadOnly { get; set; }

        public string EmailAddressUseTypeSourceSystemRecordId { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_BusinessName { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public int? EmailAddressUseTypeSourceSystemRecordId_AttributeId { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_OriginalValue { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_Status { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_Source { get; set; }
        public bool EmailAddressUseTypeSourceSystemRecordId_IsReadOnly { get; set; }

        public string EmailAddressUseTypeMasterId { get; set; }
        public string EmailAddressUseTypeMasterId_BusinessName { get; set; }
        public string EmailAddressUseTypeMasterId_BusinessDescription { get; set; }
        public int? EmailAddressUseTypeMasterId_AttributeId { get; set; }
        public string EmailAddressUseTypeMasterId_OriginalValue { get; set; }
        public string EmailAddressUseTypeMasterId_Status { get; set; }
        public string EmailAddressUseTypeMasterId_Source { get; set; }
        public bool EmailAddressUseTypeMasterId_IsReadOnly { get; set; }

        #region EmailIsPrimary

        public bool? EmailIsPrimary { get; set; }
        public bool TempEmailIsPrimary { get; set; }
        public string EmailIsPrimary_BusinessName { get; set; }
        public string EmailIsPrimary_BusinessDescription { get; set; }
        public string EmailIsPrimary_Status { get; set; }
        public string EmailIsPrimary_Source { get; set; }
        public string EmailIsPrimary_Category { get; set; }
        public string EmailIsPrimary_OriginalValue { get; set; }
        public int? EmailIsPrimary_AttributeId { get; set; }
        public bool EmailIsPrimary_IsReadOnly { get; set; }

        #endregion



        #region Dropdowns
        public List<SelectListItem> ConstituentList { get; set; }
        public List<SelectListItem> EmailAddressList { get; set; }
        public List<SelectListItem> EmailAddressUseTypeList { get; set; }

        #endregion



        public List<ConstituentEmailHistoryViewModel> HistoryData { get; set; }

    }
}
