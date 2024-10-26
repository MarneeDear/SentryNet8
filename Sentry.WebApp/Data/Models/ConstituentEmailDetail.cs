using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    public class ConstituentEmailDetail : BaseDetail
    {
        //No key attribute here because we can use the BaseDetail

        #region ConstituentSourceSystemRecordId
        public string ConstituentSourceSystemRecordId { get; set; }
        public string ConstituentSourceSystemRecordId_BusinessName { get; set; }
        public string ConstituentSourceSystemRecordId_BusinessDescription { get; set; }
        public string ConstituentSourceSystemRecordId_Status { get; set; }
        public string ConstituentSourceSystemRecordId_Source { get; set; }
        public string ConstituentSourceSystemRecordId_Category { get; set; }
        public int? ConstituentSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region FirstName
        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public string FirstName_Status { get; set; }
        public string FirstName_Source { get; set; }
        public string FirstName_Category { get; set; }
        public int? FirstName_AttributeId { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public string LastName_Status { get; set; }
        public string LastName_Source { get; set; }
        public string LastName_Category { get; set; }
        public int? LastName_AttributeId { get; set; }
        #endregion

        #region UAPersonId
        public string UAPersonId { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public string UAPersonId_Status { get; set; }
        public string UAPersonId_Source { get; set; }
        public string UAPersonId_Category { get; set; }
        public int? UAPersonId_AttributeId { get; set; }
        #endregion

        #region ConstituentMasterId
        public string ConstituentMasterId { get; set; }
        public string ConstituentMasterId_BusinessName { get; set; }
        public string ConstituentMasterId_BusinessDescription { get; set; }
        public string ConstituentMasterId_Status { get; set; }
        public string ConstituentMasterId_Source { get; set; }
        public string ConstituentMasterId_Category { get; set; }
        public int? ConstituentMasterId_AttributeId { get; set; }
        #endregion

        

        #region EmailAddress
        public string EmailAddress { get; set; }
        public string EmailAddress_BusinessName { get; set; }
        public string EmailAddress_BusinessDescription { get; set; }
        public string EmailAddress_Status { get; set; }
        public string EmailAddress_Source { get; set; }
        public string EmailAddress_Category { get; set; }
        public int? EmailAddress_AttributeId { get; set; }
        #endregion

        #region EmailAddressMasterId
        public string EmailAddressMasterId { get; set; }
        public string EmailAddressMasterId_BusinessName { get; set; }
        public string EmailAddressMasterId_BusinessDescription { get; set; }
        public string EmailAddressMasterId_Status { get; set; }
        public string EmailAddressMasterId_Source { get; set; }
        public string EmailAddressMasterId_Category { get; set; }
        public int? EmailAddressMasterId_AttributeId { get; set; }
        #endregion



        #region EmailAddressUseType
        public string EmailAddressUseType { get; set; }
        public string EmailAddressUseType_BusinessName { get; set; }
        public string EmailAddressUseType_BusinessDescription { get; set; }
        public string EmailAddressUseType_Status { get; set; }
        public string EmailAddressUseType_Source { get; set; }
        public string EmailAddressUseType_Category { get; set; }
        public int? EmailAddressUseType_AttributeId { get; set; }
        #endregion

        #region EmailAddressUseTypeSourceSystemRecordId
        public string EmailAddressUseTypeSourceSystemRecordId { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_BusinessName { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_Status { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_Source { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_Category { get; set; }
        public int? EmailAddressUseTypeSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region EmailAddressUseTypeMasterId
        public string EmailAddressUseTypeMasterId { get; set; }
        public string EmailAddressUseTypeMasterId_BusinessName { get; set; }
        public string EmailAddressUseTypeMasterId_BusinessDescription { get; set; }
        public string EmailAddressUseTypeMasterId_Status { get; set; }
        public string EmailAddressUseTypeMasterId_Source { get; set; }
        public string EmailAddressUseTypeMasterId_Category { get; set; }
        public int? EmailAddressUseTypeMasterId_AttributeId { get; set; }
        #endregion
        #region EmailIsPrimary

        public bool? EmailIsPrimary { get; set; } = false;

        public string EmailIsPrimary_BusinessName { get; set; }

        public string EmailIsPrimary_BusinessDescription { get; set; }

        public string EmailIsPrimary_Source { get; set; }

        public string EmailIsPrimary_Category { get; set; }

        public string EmailIsPrimary_Status { get; set; }

        public int? EmailIsPrimary_AttributeId { get; set; }

        #endregion


    }
}
