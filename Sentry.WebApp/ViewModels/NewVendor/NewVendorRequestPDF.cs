using Sentry.Domain.AccountsPayable.Entities;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels.NewVendor
{
    public class NewVendorRequestPDF
    {
        public long NewVendorRequestId { get; set; }
        public DateTime SubmittedOnDate { get; set; }
    }
    public class NewVendorFormCreate
    {
        public long NewVendorRequestId { get; set; }
        public long VendorId { get; set; }
    }

    public class PDFViewModelSimplified
    {
        public string FormNumber { get; set; }

        public string PreparedByName { get; set; }

        public string PreparedByEmail { get; set; }

        public string PreparedByPhone { get; set; }

        public string VendorName { get; set; }
        public string VendorStreetAddress { get; set; }
        public string VendorCity { get; set; }
        public string VendorState { get; set; }
        public string VendorZip { get; set; }
        public string VendorType { get; set; }
        public string BusinessContactFirstName { get; set; }
        public string BusinessContactLastName { get; set; }
        public string PaymentType { get; set; }
        public string PayeeType { get; set; }
        public string ICAYear { get; set; }
        public string W9Year { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public string BusinessContactMethod { get; set; }
        public string CellularContactMethod { get; set; }
        public string EmailContactMethod { get; set; }
        public string HomeContactMethod { get; set; }
        public string Email { get;set; }
        public string SubmittedOnDate { get; set; }
        public IEnumerable<string> SupportingDocuments { get; set; }

        public static explicit operator PDFViewModelSimplified (NewVendorRequest newVendorRequest)
        {
            return new PDFViewModelSimplified
            {
                FormNumber = newVendorRequest.FormNumber,
                PreparedByName = $"{newVendorRequest.PreparedByFirstName} {newVendorRequest.PreparedByLastName}",
                PreparedByEmail = newVendorRequest.PreparedByEmail,
                PreparedByPhone = newVendorRequest.PreparedByPhone,
                VendorName = newVendorRequest.VendorName,
                VendorStreetAddress = newVendorRequest.VendorStreetAddress,
                VendorCity = newVendorRequest.VendorCity,
                VendorState = newVendorRequest.VendorState,
                VendorZip = newVendorRequest.VendorZip,
                HomeContactMethod = newVendorRequest.HomeContactMethod.HasValue ? newVendorRequest.HomeContactMethod.Value.ToString("###-###-####") : String.Empty,
                BusinessContactMethod = newVendorRequest.BusinessContactMethod.HasValue ? newVendorRequest.BusinessContactMethod.Value.ToString("###-###-####") : String.Empty,
                CellularContactMethod = newVendorRequest.CellularContactMethod.HasValue ? newVendorRequest.CellularContactMethod.Value.ToString("###-###-####") : String.Empty,
                EmailContactMethod = newVendorRequest.EmailContactMethod,
                PaymentType = newVendorRequest.PaymentType,
                VendorType = newVendorRequest.VendorType,
                PayeeType = newVendorRequest.PayeeType,
                ICAYear = newVendorRequest.IcaYear.HasValue ? newVendorRequest.IcaYear.Value.ToString("MM/dd/yyyy") : String.Empty,
                W9Year = newVendorRequest.W9Year.HasValue ? newVendorRequest.W9Year.Value.ToString("MM/dd/yyyy") : String.Empty
            };
        }
    }
}
