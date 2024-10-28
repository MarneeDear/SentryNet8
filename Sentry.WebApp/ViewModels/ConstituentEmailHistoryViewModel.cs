using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentEmailHistoryViewModel
    {
        public DateTime? HistoryDate { get; set; }



        public string ConstituentSourceSystemRecordId { get; set; }
        public string ConstituentSourceSystemRecordId_Status { get; set; }
        public string ConstituentSourceSystemRecordId_OriginalValue { get; set; }

        public string FirstName { get; set; }
        public string FirstName_Status { get; set; }
        public string FirstName_OriginalValue { get; set; }

        public string LastName { get; set; }
        public string LastName_Status { get; set; }
        public string LastName_OriginalValue { get; set; }

        public string UAPersonId { get; set; }
        public string UAPersonId_Status { get; set; }
        public string UAPersonId_OriginalValue { get; set; }

        public string ConstituentMasterId { get; set; }
        public string ConstituentMasterId_Status { get; set; }
        public string ConstituentMasterId_OriginalValue { get; set; }



        public string EmailAddress { get; set; }
        public string EmailAddress_Status { get; set; }
        public string EmailAddress_OriginalValue { get; set; }

        public string EmailAddressMasterId { get; set; }
        public string EmailAddressMasterId_Status { get; set; }
        public string EmailAddressMasterId_OriginalValue { get; set; }



        public string EmailAddressUseType { get; set; }
        public string EmailAddressUseType_Status { get; set; }
        public string EmailAddressUseType_OriginalValue { get; set; }

        public string EmailAddressUseTypeSourceSystemRecordId { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_Status { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_OriginalValue { get; set; }

        public string EmailAddressUseTypeMasterId { get; set; }
        public string EmailAddressUseTypeMasterId_Status { get; set; }
        public string EmailAddressUseTypeMasterId_OriginalValue { get; set; }

        public bool? EmailIsPrimary { get; set; }
        public string EmailIsPrimary_Status { get; set; }
        public string EmailIsPrimary_OriginalValue { get; set; }



    }
}
