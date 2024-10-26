using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sentry.WebApp.ViewModels
{
    public class StudentParentViewModel : BaseIntegrationViewModel
    {
        public StudentParentViewModel() : base() { }

        public bool IsReadOnly { get; set; }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public int? FirstName_AttributeId { get; set; }
        public string FirstName_OriginalValue { get; set; }
        public string FirstName_Status { get; set; }
        public string FirstName_Source { get; set; }
        public bool FirstName_IsReadOnly { get; set; }

        public string PreferredName { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public int? PreferredName_AttributeId { get; set; }
        public string PreferredName_OriginalValue { get; set; }
        public string PreferredName_Status { get; set; }
        public string PreferredName_Source { get; set; }
        public bool PreferredName_IsReadOnly { get; set; }

        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public int? LastName_AttributeId { get; set; }
        public string LastName_OriginalValue { get; set; }
        public string LastName_Status { get; set; }
        public string LastName_Source { get; set; }
        public bool LastName_IsReadOnly { get; set; }



        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public int? Suffix_AttributeId { get; set; }
        public string Suffix_OriginalValue { get; set; }
        public string Suffix_Status { get; set; }
        public string Suffix_Source { get; set; }
        public bool Suffix_IsReadOnly { get; set; }

        public string SuffixMasterRecordId { get; set; }
        public string SuffixMasterRecordId_BusinessName { get; set; }
        public string SuffixMasterRecordId_BusinessDescription { get; set; }
        public int? SuffixMasterRecordId_AttributeId { get; set; }
        public string SuffixMasterRecordId_OriginalValue { get; set; }
        public string SuffixMasterRecordId_Status { get; set; }
        public string SuffixMasterRecordId_Source { get; set; }
        public bool SuffixMasterRecordId_IsReadOnly { get; set; }


        public string StudentId { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public int? StudentId_AttributeId { get; set; }
        public string StudentId_OriginalValue { get; set; }
        public string StudentId_Status { get; set; }
        public string StudentId_Source { get; set; }
        public bool StudentId_IsReadOnly { get; set; }



        public string StudentFirstName { get; set; }
        public string StudentFirstName_BusinessName { get; set; }
        public string StudentFirstName_BusinessDescription { get; set; }
        public int? StudentFirstName_AttributeId { get; set; }
        public string StudentFirstName_OriginalValue { get; set; }
        public string StudentFirstName_Status { get; set; }
        public string StudentFirstName_Source { get; set; }
        public bool StudentFirstName_IsReadOnly { get; set; }

        public string StudentLastName { get; set; }
        public string StudentLastName_BusinessName { get; set; }
        public string StudentLastName_BusinessDescription { get; set; }
        public int? StudentLastName_AttributeId { get; set; }
        public string StudentLastName_OriginalValue { get; set; }
        public string StudentLastName_Status { get; set; }
        public string StudentLastName_Source { get; set; }
        public bool StudentLastName_IsReadOnly { get; set; }

        public string StudentMasterRecordId { get; set; }
        public string StudentMasterRecordId_BusinessName { get; set; }
        public string StudentMasterRecordId_BusinessDescription { get; set; }
        public int? StudentMasterRecordId_AttributeId { get; set; }
        public string StudentMasterRecordId_OriginalValue { get; set; }
        public string StudentMasterRecordId_Status { get; set; }
        public string StudentMasterRecordId_Source { get; set; }
        public bool StudentMasterRecordId_IsReadOnly { get; set; }



        public string Relationship { get; set; }
        public string Relationship_BusinessName { get; set; }
        public string Relationship_BusinessDescription { get; set; }
        public int? Relationship_AttributeId { get; set; }
        public string Relationship_OriginalValue { get; set; }
        public string Relationship_Status { get; set; }
        public string Relationship_Source { get; set; }
        public bool Relationship_IsReadOnly { get; set; }

        public string RelationshipMasterRecordId { get; set; }
        public string RelationshipMasterRecordId_BusinessName { get; set; }
        public string RelationshipMasterRecordId_BusinessDescription { get; set; }
        public int? RelationshipMasterRecordId_AttributeId { get; set; }
        public string RelationshipMasterRecordId_OriginalValue { get; set; }
        public string RelationshipMasterRecordId_Status { get; set; }
        public string RelationshipMasterRecordId_Source { get; set; }
        public bool RelationshipMasterRecordId_IsReadOnly { get; set; }



        public string Phone1Number { get; set; }
        public string Phone1Number_BusinessName { get; set; }
        public string Phone1Number_BusinessDescription { get; set; }
        public int? Phone1Number_AttributeId { get; set; }
        public string Phone1Number_OriginalValue { get; set; }
        public string Phone1Number_Status { get; set; }
        public string Phone1Number_Source { get; set; }
        public bool Phone1Number_IsReadOnly { get; set; }

        public string Phone1MasterRecordId { get; set; }
        public string Phone1MasterRecordId_BusinessName { get; set; }
        public string Phone1MasterRecordId_BusinessDescription { get; set; }
        public int? Phone1MasterRecordId_AttributeId { get; set; }
        public string Phone1MasterRecordId_OriginalValue { get; set; }
        public string Phone1MasterRecordId_Status { get; set; }
        public string Phone1MasterRecordId_Source { get; set; }
        public bool Phone1MasterRecordId_IsReadOnly { get; set; }

        public string Phone1Extension { get; set; }
        public string Phone1Extension_BusinessName { get; set; }
        public string Phone1Extension_BusinessDescription { get; set; }
        public int? Phone1Extension_AttributeId { get; set; }
        public string Phone1Extension_OriginalValue { get; set; }
        public string Phone1Extension_Status { get; set; }
        public string Phone1Extension_Source { get; set; }
        public bool Phone1Extension_IsReadOnly { get; set; }

        public string Phone1CountryCode { get; set; }
        public string Phone1CountryCode_BusinessName { get; set; }
        public string Phone1CountryCode_BusinessDescription { get; set; }
        public int? Phone1CountryCode_AttributeId { get; set; }
        public string Phone1CountryCode_OriginalValue { get; set; }
        public string Phone1CountryCode_Status { get; set; }
        public string Phone1CountryCode_Source { get; set; }
        public bool Phone1CountryCode_IsReadOnly { get; set; }

        public string Phone1CountryMasterRecordId { get; set; }
        public string Phone1CountryMasterRecordId_BusinessName { get; set; }
        public string Phone1CountryMasterRecordId_BusinessDescription { get; set; }
        public int? Phone1CountryMasterRecordId_AttributeId { get; set; }
        public string Phone1CountryMasterRecordId_OriginalValue { get; set; }
        public string Phone1CountryMasterRecordId_Status { get; set; }
        public string Phone1CountryMasterRecordId_Source { get; set; }
        public bool Phone1CountryMasterRecordId_IsReadOnly { get; set; }



        public string Phone2Number { get; set; }
        public string Phone2Number_BusinessName { get; set; }
        public string Phone2Number_BusinessDescription { get; set; }
        public int? Phone2Number_AttributeId { get; set; }
        public string Phone2Number_OriginalValue { get; set; }
        public string Phone2Number_Status { get; set; }
        public string Phone2Number_Source { get; set; }
        public bool Phone2Number_IsReadOnly { get; set; }

        public string Phone2MasterRecordId { get; set; }
        public string Phone2MasterRecordId_BusinessName { get; set; }
        public string Phone2MasterRecordId_BusinessDescription { get; set; }
        public int? Phone2MasterRecordId_AttributeId { get; set; }
        public string Phone2MasterRecordId_OriginalValue { get; set; }
        public string Phone2MasterRecordId_Status { get; set; }
        public string Phone2MasterRecordId_Source { get; set; }
        public bool Phone2MasterRecordId_IsReadOnly { get; set; }

        public string Phone2Extension { get; set; }
        public string Phone2Extension_BusinessName { get; set; }
        public string Phone2Extension_BusinessDescription { get; set; }
        public int? Phone2Extension_AttributeId { get; set; }
        public string Phone2Extension_OriginalValue { get; set; }
        public string Phone2Extension_Status { get; set; }
        public string Phone2Extension_Source { get; set; }
        public bool Phone2Extension_IsReadOnly { get; set; }

        public string Phone2CountryCode { get; set; }
        public string Phone2CountryCode_BusinessName { get; set; }
        public string Phone2CountryCode_BusinessDescription { get; set; }
        public int? Phone2CountryCode_AttributeId { get; set; }
        public string Phone2CountryCode_OriginalValue { get; set; }
        public string Phone2CountryCode_Status { get; set; }
        public string Phone2CountryCode_Source { get; set; }
        public bool Phone2CountryCode_IsReadOnly { get; set; }

        public string Phone2CountryMasterRecordId { get; set; }
        public string Phone2CountryMasterRecordId_BusinessName { get; set; }
        public string Phone2CountryMasterRecordId_BusinessDescription { get; set; }
        public int? Phone2CountryMasterRecordId_AttributeId { get; set; }
        public string Phone2CountryMasterRecordId_OriginalValue { get; set; }
        public string Phone2CountryMasterRecordId_Status { get; set; }
        public string Phone2CountryMasterRecordId_Source { get; set; }
        public bool Phone2CountryMasterRecordId_IsReadOnly { get; set; }



        public string EmailAddress1 { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        public int? EmailAddress1_AttributeId { get; set; }
        public string EmailAddress1_OriginalValue { get; set; }
        public string EmailAddress1_Status { get; set; }
        public string EmailAddress1_Source { get; set; }
        public bool EmailAddress1_IsReadOnly { get; set; }

        public string EmailAddress1MasterRecordId { get; set; }
        public string EmailAddress1MasterRecordId_BusinessName { get; set; }
        public string EmailAddress1MasterRecordId_BusinessDescription { get; set; }
        public int? EmailAddress1MasterRecordId_AttributeId { get; set; }
        public string EmailAddress1MasterRecordId_OriginalValue { get; set; }
        public string EmailAddress1MasterRecordId_Status { get; set; }
        public string EmailAddress1MasterRecordId_Source { get; set; }
        public bool EmailAddress1MasterRecordId_IsReadOnly { get; set; }



        public string EmailAddress2 { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        public int? EmailAddress2_AttributeId { get; set; }
        public string EmailAddress2_OriginalValue { get; set; }
        public string EmailAddress2_Status { get; set; }
        public string EmailAddress2_Source { get; set; }
        public bool EmailAddress2_IsReadOnly { get; set; }

        public string EmailAddress2MasterRecordId { get; set; }
        public string EmailAddress2MasterRecordId_BusinessName { get; set; }
        public string EmailAddress2MasterRecordId_BusinessDescription { get; set; }
        public int? EmailAddress2MasterRecordId_AttributeId { get; set; }
        public string EmailAddress2MasterRecordId_OriginalValue { get; set; }
        public string EmailAddress2MasterRecordId_Status { get; set; }
        public string EmailAddress2MasterRecordId_Source { get; set; }
        public bool EmailAddress2MasterRecordId_IsReadOnly { get; set; }

        public string Address1 { get; set; }
        public string Address1_BusinessName { get; set; }
        public string Address1_BusinessDescription { get; set; }
        public int? Address1_AttributeId { get; set; }
        public string Address1_OriginalValue { get; set; }
        public string Address1_Status { get; set; }
        public string Address1_Source { get; set; }
        public bool Address1_IsReadOnly { get; set; }

        public string AddressMasterId { get; set; }
        public string AddressMasterId_BusinessName { get; set; }
        public string AddressMasterdId_BusinessDescription { get; set; }
        public int? AddressMasterId_AttributeId { get; set; }
        public string AddressMasterId_OriginalValue { get; set; }
        public string AddressMasterId_Status { get; set; }
        public string AddressMasterId_Source { get; set; }
        public bool AddressMasterId_IsReadOnly { get; set; }

        public string City { get; set; }
        public string City_BusinessName { get; set; }
        public string City_BusinessDescription { get; set; }
        public int? City_AttributeId { get; set; }
        public string City_OriginalValue { get; set; }
        public string City_Status { get; set; }
        public string City_Source { get; set; }
        public bool City_IsReadOnly { get; set; }

        public string State { get; set; }
        public string State_BusinessName { get; set; }
        public string State_BusinessDescription { get; set; }
        public int? State_AttributeId { get; set; }
        public string State_OriginalValue { get; set; }
        public string State_Status { get; set; }
        public string State_Source { get; set; }
        public bool State_IsReadOnly { get; set; }

        public string StateMasterId { get; set; }
        public string StateMasterId_BusinessName { get; set; }
        public string StateMasterId_BusinessDescription { get; set; }
        public int? StateMasterId_AttributeId { get; set; }
        public string StateMasterId_OriginalValue { get; set; }
        public string StateMasterId_Status { get; set; }
        public string StateMasterId_Source { get; set; }
        public bool StateMasterId_IsReadOnly { get; set; }

        public string PostalCode { get; set; }
        public string PostalCode_BusinessName { get; set; }
        public string PostalCode_BusinessDescription { get; set; }
        public int? PostalCode_AttributeId { get; set; }
        public string PostalCode_OriginalValue { get; set; }
        public string PostalCode_Status { get; set; }
        public string PostalCode_Source { get; set; }
        public bool PostalCode_IsReadOnly { get; set; }

        public string DeliveryPointCode { get; set; }
        public string DeliveryPointCode_BusinessName { get; set; }
        public string DeliveryPointCode_BusinessDescription { get; set; }
        public int? DeliveryPointCode_AttributeId { get; set; }
        public string DeliveryPointCode_OriginalValue { get; set; }
        public string DeliveryPointCode_Status { get; set; }
        public string DeliveryPointCode_Source { get; set; }
        public bool DeliveryPointCode_IsReadOnly { get; set; }

        public string Country { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        public int? Country_AttributeId { get; set; }
        public string Country_OriginalValue { get; set; }
        public string Country_Status { get; set; }
        public string Country_Source { get; set; }
        public bool Country_IsReadOnly { get; set; }

        public string CountryMasterRecordId { get; set; }
        public string CountryMasterRecordId_BusinessName { get; set; }
        public string CountryMasterRecordId_BusinessDescription { get; set; }
        public int? CountryMasterRecordId_AttributeId { get; set; }
        public string CountryMasterRecordId_OriginalValue { get; set; }
        public string CountryMasterRecordId_Status { get; set; }
        public string CountryMasterRecordId_Source { get; set; }
        public bool CountryMasterRecordId_IsReadOnly { get; set; }



        #region Dropdowns
        public List<SelectListItem> TitleList { get; set; }
        public List<SelectListItem> SuffixList { get; set; }
        public List<SelectListItem> StudentList { get; set; }
        public List<SelectListItem> RelationshipList { get; set; }
        public List<SelectListItem> CountryCodeList { get; set; }
        public IEnumerable<SelectListItem> StateList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        #endregion



        public List<StudentParentHistoryViewModel> HistoryData { get; set; }


    }
}
