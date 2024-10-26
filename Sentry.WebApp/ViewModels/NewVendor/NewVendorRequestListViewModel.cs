using Sentry.WebApp.ViewModels.GiftDisbursements;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels.NewVendor
{
    public class NewVendorRequestListItem
    {
        public string FormNumber { get;set; }
        public string VendorStreetAddress { get;set; }
        public string VendorCity { get;set; }
        public string VendorState { get;set; }
        public string VendorZip { get;set; }
        public string VendorName { get;set; }
        public string SubmittedForApprovalOn { get;set; }
        public string Status { get;set; }
        public long Id { get;set; }
        public string ProcessingError { get;set; }
        public string PaymentType { get;set; }
        public string PrepardedBy { get; set; }

        public static explicit operator NewVendorRequestListItem(Sentry.Domain.AccountsPayable.Entities.NewVendorRequest newVendorRequest)
        {
            return new NewVendorRequestListItem
            {
                Id = newVendorRequest.Id,
                FormNumber = newVendorRequest.FormNumber,
                VendorName = newVendorRequest.VendorName,
                VendorStreetAddress = newVendorRequest.VendorStreetAddress,
                VendorCity = newVendorRequest.VendorCity,
                VendorState = newVendorRequest.VendorState,
                VendorZip = newVendorRequest.VendorZip,
                PaymentType = newVendorRequest.PaymentType,
                ProcessingError = newVendorRequest.ProcessingError,
                SubmittedForApprovalOn = (newVendorRequest.SubmittedForApprovalOn.HasValue ? newVendorRequest.SubmittedForApprovalOn.Value.ToString("MM/dd/yyyy") : null),
                PrepardedBy = $"{newVendorRequest.PreparedByFirstName} {newVendorRequest.PreparedByLastName}"
                
            };
        }
    }
    public class NewVendorRequestListViewModel : BaseIntegrationViewModel
    {
        public NewVendorRequestListViewModel() : base()
        {

        }

        public string Organization { get; set; }
        public IEnumerable<NewVendorRequestListItem> NewVendorRequests { get; set; }        

    }
}
