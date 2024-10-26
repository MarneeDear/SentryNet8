using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels.GiftTransmittal
{
    public class GiftTransmittalListItem
    {

        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string DateCreated { get; set; }
        public string FormNumber { get; set; }
        //public string FormState { get; set; }
        public string Constituent { get; set; }
        public string Total { get; set; }
        public string BatchType { get; set; }
        public string PreparedBy { get; set; }
        public bool WaitingOnResponseFromBursar { get; set; }
        public bool WaitingOnResponseFromPreparer { get; set; }
        public string Status { get; set; }
        public int CurrentApprovalStage { get; set; }
        public string SecondaryApproverStatus { get; set; }
        public string PostDate { get; set; }
        public bool HasProcessingError { get; set; }

        public static explicit operator GiftTransmittalListItem(Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal.GiftTransmittal giftTransmittal)
        {
            if (giftTransmittal.ItemId != null)
            {
                return new GiftTransmittalListItem()
                {
                    Id = new Guid(giftTransmittal.Id),
                    ItemId = new Guid(giftTransmittal.ItemId),
                    DateCreated = giftTransmittal.DateAdded.ToString("MM/dd/yyyy"),
                    FormNumber = giftTransmittal.FormNumber,
                    BatchType = giftTransmittal.BatchType,
                    PreparedBy = $"{giftTransmittal.PreparedByFirstName} {giftTransmittal.PreparedByLastName}",
                    //FormState = giftTransmittal.Status,
                    Constituent = String.IsNullOrWhiteSpace(giftTransmittal.ConstituentOrganizationName) ? giftTransmittal.ConstituentName : giftTransmittal.ConstituentOrganizationName,
                    Total = giftTransmittal.Amount.ToString("C"),
                    WaitingOnResponseFromBursar = giftTransmittal.WaitingOnResponseFromBursar,
                    WaitingOnResponseFromPreparer = giftTransmittal.WaitingOnResponseFromPreparer,
                    Status = giftTransmittal.Status,
                    CurrentApprovalStage = giftTransmittal.CurrentApprovalStage,
                    SecondaryApproverStatus = giftTransmittal.SecondaryApprovalStatus,
                    PostDate = giftTransmittal.PostDate.ToString("MM/dd/yyyy"),
                    HasProcessingError = giftTransmittal.HasProcessingError
                };
            }
            else
            {
                //This is a bad gift transmittal. does not have any items. cant be edited.
                //TODO cleanup?
                return new GiftTransmittalListItem()
                {
                    Id = new Guid(giftTransmittal.Id),
                    ItemId = Guid.Empty,
                    DateCreated = giftTransmittal.DateAdded.ToString("MM/dd/yyyy"),
                    FormNumber = giftTransmittal.FormNumber,
                    BatchType = giftTransmittal.BatchType,
                    PreparedBy = giftTransmittal.PreparedBy,
                    //FormState = "ERROR",
                    Constituent = "ERROR",
                    Total = "ERROR",
                };
            }
        }
    }
    public class GiftTransmittalListViewModel : BaseIntegrationViewModel
    {
        public GiftTransmittalListViewModel() : base() { }
        public string Organization { get; set; }        
        public IEnumerable<GiftTransmittalListItem> GiftTransmittals { get; set; }
        public bool DisplayRequiringPhysicalDocuments { get; set; } = false;
        public string ListType { get; set; }
    }
}
