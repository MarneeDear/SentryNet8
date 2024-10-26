using Sentry.Domain.DataAccess.Entities;
using System;
using System.Data;

namespace Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal
{
    public class GiftTransmittal : EntityBase
    {
        internal GiftTransmittal(IDataRecord record)
        {
            Id = GetValueOrDefault<string>(record, 0);
            ItemId = GetValueOrDefault<string>(record, 1);
            FormNumber = GetValueOrDefault<string>(record, 2);
            FormCode = GetValueOrDefault<string>(record, 3);
            DateAdded = GetValueOrDefault<DateTime>(record, 4);
            ConstituentLookupId = GetValueOrDefault<string>(record, 5);
            ConstituentName = GetValueOrDefault<string>(record, 6);
            Amount = GetValueOrDefault<decimal>(record, 7);
            Status = GetValueOrDefault<string>(record, 9);
            PreparedBy = GetValueOrDefault<string>(record, 10);
            BatchTypeCode = GetValueOrDefault<int>(record, 11);
            BatchType = GetValueOrDefault<string>(record, 12);
            PreparedByFirstName = GetValueOrDefault<string>(record, 15);
            PreparedByLastName = GetValueOrDefault<string>(record, 16);
            ConstituentOrganizationName = GetValueOrDefault<string>(record, 17);
            ApprovalStatusCode = GetValueOrDefault<int>(record, 18);
            ApprovalStatus = GetValueOrDefault<string>(record, 19);
            WaitingOnResponseFromBursar = GetValueOrDefault<bool>(record, 21);
            WaitingOnResponseFromPreparer = GetValueOrDefault<bool>(record, 22);
            CurrentApprovalStage = GetValueOrDefault<int>(record, 23);
            SecondaryApprovalStatus = GetValueOrDefault<string>(record, 24);
            PostDate = GetValueOrDefault<DateTime>(record, 26);
            HasProcessingError = GetValueOrDefault<bool>(record, 27);

        }

        public string Id { get; set; }
        public string ItemId { get; set; }
        public string FormNumber { get; set; }
        public string FormCode { get; set; }
        public DateTime DateAdded { get; set; }
        public string ConstituentLookupId { get; set; }
        public string ConstituentName { get; set; }
        public string ConstituentOrganizationName { get; set; }
        //public string FundAccount { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string PreparedBy { get; set; }
        public string PreparedByFirstName { get; set; }
        public string PreparedByLastName { get; set; }
        public int BatchTypeCode { get; set; }
        public string BatchType { get; set; }
        public int ApprovalStatusCode { get; set; }
        public string ApprovalStatus { get; set; }
        public bool WaitingOnResponseFromBursar { get; set; }
        public bool WaitingOnResponseFromPreparer { get; set; }
        public int CurrentApprovalStage { get; set; }

        public string SecondaryApprovalStatus { get; set; }
        public DateTime PostDate { get; set; }
        public bool HasProcessingError { get; set; }

    }

}
