using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sentry.Domain.AccountsPayable.Entities
{
    public class NewVendorRequest
    {
        public long Id { get; set; }
        public long VendorId { get; set; }
        public string PreparedByEmployeeId { get; set; }
        public string PreparedByFirstName { get; set; }
        public string PreparedByLastName { get; set; }
        public string PreparedByEmail { get; set; }
        public string PreparedByPhone { get; set; }
        public string FormNumber { get; set; }
        public string VendorName { get; set; }
        public string VendorStreetAddress { get; set; }
        public string VendorCity { get; set; }
        public string VendorState { get; set; }
        public string VendorZip { get; set; }
        public string BusinessContactFirstName { get; set; }
        public string BusinessContactLastName { get; set; }
        public string PaymentType { get; set; }        
        public string PayeeName { get; set; }
        public bool Issue1099 { get; set; }
        public string PayFromAccount { get; set; }
        //public string PaymentOption { get; set; } = "OnePerInvoice";
        public bool? EFTStatus { get; set; }
        public string VendorType { get; set; }
        public string EIN { get; set; }
        public string Status { get; set; }
        public long? BusinessContactMethod { get; set; }
        public long? CellularContactMethod { get; set; }
        public long? HomeContactMethod { get; set; }
        public string EmailContactMethod { get; set; }
        public DateTime? IcaYear { get; set; }
        public string PayeeType { get; set; }
        public DateTime? SubmittedForApprovalOn { get; set; }
        public DateTime? W9Year { get; set; }
        public string Comments { get; set; }
        public string ApproveRejectComments { get; set; }
        public IEnumerable<NewVendorRequestAttachment> Attachments { get; set; }
        public string ProcessingError { get; set; }

    }
    public class NewVendorRequestAttachment
    {
        public long Id { get; set; }
        public int NewVendorRequestId { get; set; }
        public string Type { get; set; }
        public string Comments { get; set; }
    }
    public class CreateNewVendorRequest
    {
        public long NewVendorRequestId { get; set; }
    }
    public class CreateVendor
    {
        public long NewVendorRequestId { get; set; }
        public long VendorId { get; set; }
    }

    public class UpdateNewVendorRequest
    {
        public long? Id { get; set; }
        public string PreparedByEmployeeId { get; set; }
        public string FormNumber { get; set; }
        public string VendorName { get; set; }
        public string VendorStreetAddress { get; set; }
        public string VendorCity { get; set; }
        public string VendorState { get; set; }
        public string VendorZip { get; set; }
        public string BusinessContactFirstName { get; set; }
        public string BusinessContactLastName { get; set; }
        public string PaymentType { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Email { get; set; }
        public string PayeeName { get; set; }
        public bool Issue1099 { get; set; }        
        public bool PaymentOption { get; set; }
        public bool? EFTStatus { get; set; }
        public string VendorType { get; set; }
        public string EIN { get; set; }
        public string Status { get; set; }
        public string BusinessContactMethod { get; set; }
        public string CellularContactMethod { get; set; }
        public string EmailContactMethod { get; set; }
        public string HomeContactMethod { get; set; }
        public DateTime? IcaYear { get; set; }
        public string PayeeType { get; set; }
        public DateTime? W9Year { get; set; }
        public IEnumerable<NewVendorRequestAttachment> Attachments { get; set; }
        public string Comments { get; set; }
        public string ApproveRejectComments { get; set; }

    }
    public class NewVendorRequestResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]

        public NewVendorRequest NewVendorRequest { get; set; }

    }
    public class NewVendorRequestsResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public IEnumerable<NewVendorRequest> NewVendorRequests { get; set; }

    }
    public class NewVendorRequestApproval
    {
        public bool? Approved { get; set; }
        public string EmployeeId { get; set; }
        public string Comments { get; set; }
        public string Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime ApprovedOn { get; set; }
        public bool ManuallyAdded { get; set; }
    }
}
