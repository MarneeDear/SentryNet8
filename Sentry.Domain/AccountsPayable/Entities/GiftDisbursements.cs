using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Sentry.Domain.AccountsPayable.Entities
{
    public class GiftDisbursement
    {
        public long Id { get; set; }
        public string Type { get; set; }
        // 2-char form code {EM, ET, ST} (NOT FormType Id)
        public string FormNumber { get; set; }       
        public string PreparedByEmployeeId { get; set; }
        public string PreparedByFirstName { get; set; }
        public string PreparedByLastName { get; set; }
        public string PreparedByEmail { get; set; }
        public string PreparedByJobTitle { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string Purpose { get; set; }
        public string PayeeIs { get; set; }
        public string PayeePaymentType { get; set; }
        public string Fund { get; set; }
        public string PayeePaymentSpecialInstructions { get; set; }
        public string PayeeName { get; set; }
        public string PayeeAddress1 { get; set; }
        public string PayeeAddress2 { get; set; }
        public string PayeeCity { get; set; }
        public string PayeeState { get; set; }
        public string PayeeZip { get; set; }
        public DateTime? PayeeW9Year { get; set; }
        public DateTime? PayeeICAYear { get; set; }
        public string PayeeType { get; set; }
        public string InvoiceDescription { get; set; }
        public string ReviewerNotes { get; set; }
        public long? VendorId { get; set; }
        public long? VendorAddressId { get; set; }
        public bool UAEmployee { get; set; }
        public decimal? RentsAmount { get; set; }
        public decimal? OtherIncomeAmount { get; set; }
        public decimal? NonemployeeCompensationAmount { get; set; }
        public decimal? GrossProceedsPaidToAttorneyAmount { get; set; }
        public decimal? Total1099 { get; set; }

        public decimal Total { get; set; }
        public IEnumerable<GiftDisbursementApprover> GiftDisbursementApprovers { get; set; }
        public IEnumerable<GiftDisbursementApprover> GiftDisbursementApprovalHistory { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? LastApprovedOnDate { get; set; }
        public string ProcessingError { get; set; }
        public bool RejectedByEscalatedApprovers { get; set; }
        public DateTime? SubmittedForApprovalOn { get; set; }
        public bool Pending { get; set; }
        public string PendingComments { get; set; }
        public string CCEmails { get; set; }
        public DateTime? ProcessedOn { get; set; }
        public IEnumerable<GiftDisbursementItem> GiftDisbursementItems { get; set; }
        public IEnumerable<GiftDisbursementStudent> GiftDisbursementStudents { get; set; }
        public IEnumerable<GiftDisbursementDebitAccount> GiftDisbursementDebitAccounts { get; set; }
        public string APReviewerEmployeeId { get; set; }
        public bool PreviouslyRejected { get; set; }
        public bool PreviouslyApprovedByUser { get; set; }
        public bool Resubmitted { get; set; }
    }

    public class UpdateGiftDisbursement
    {
        public long Id { get; set; }

        public string Type { get; set; }
        // 2-char form code {EM, ET, ST} (NOT FormType Id)
        public string ProjectId { get; set; }
        public string UaAccount { get; set; }
        public string PreparedByEmployeeId { get; set; }
        public string DepartmentCode { get; set; }
        public string DivisionCode { get; set; }
        public string Purpose { get; set; }
        public string PayeeIs { get; set; }
        public string PayeePaymentType { get; set; }
        public string PayeePaymentSpecialInstructions { get; set; }
        public string PayeeName { get; set; }
        public string PayeeAddress1 { get; set; }
        public string PayeeAddress2 { get; set; }
        public string PayeeCity { get; set; }
        public string PayeeState { get; set; }
        public string PayeeZip { get; set; }
        public long? VendorId { get; set; }
        public long? VendorAddressId { get; set; }
        public string InvoiceDescription { get; set; }
        public string ReviewerNotes { get; set; }
        public decimal? RentsAmount { get; set; }
        public decimal? OtherIncomeAmount { get; set; }
        public decimal? NonemployeeCompensationAmount { get; set; }
        public decimal? GrossProceedsPaidToAttorneyAmount { get; set; }

        public string SignatureAuthorityEmployeeId { get; set; }
        public string DesigneeEmployeeId { get; set; }
        //public DateTime PostDate { get; set; }
        public DateTime? SubmittedForApprovalOn { get; set; }
        public bool Pending { get; set; }
        public string PendingComments { get; set; }
        public string CCEmails { get; set; }

        public IEnumerable<GiftDisbursementItem> GiftDisbursementItems { get; set; }
    }

    public class GiftDisbursementApprover
    {
        public string Type { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime ApprovedOn { get; set; }
        public bool? Approved { get; set; }
        public string Comments { get; set; }
        public bool ManuallyAdded { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class GiftDisbursementItem
    {
        public long? Id { get; set; }
        public long DisbursementId { get; set; }
        public string ProjectId { get; set; }
        public int ProjectFeId { get; set; }
        public string ProjectName { get; set; }
        public decimal ProjectBalance { get; set; }
        public string UaAccount { get; set; }
        public string ProjectCode { get; set; }
        public string ObjectCode { get; set; }
        public string SubAccountCode { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string DebitAccount { get; set; }
        public bool IsFundraisingExpense { get; set; }
        public string DebitAccountDescription { get; set; }
        public string InvoiceDescription {get; set;}
    }
    public class GiftDisbursementDebitAccount
    {
        public string DebitAccount { get; set; } 
    }

    public class GiftDisbursementStudent
    {
        public long? Id { get; set; }
        public long? DisbursementId { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public int Term { get; set; }
        public string TermName { get; set; }
        public string ScholarshipId { get; set; }
        public string ScholarshipName { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

    public class GiftDisbursementResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public GiftDisbursement GiftDisbursement { get; set; }

    }

    public class GiftDisbursementList
    {
        public int ETCount { get; set; } = 0;
        public int STCount { get; set; } = 0;
        public int EMCount { get; set; } = 0;
        public IEnumerable<GiftDisbursement> GiftDisbursements { get; set; }
    }

    public class GiftDisbursementsResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        //public IEnumerable<GiftDisbursement> GiftDisbursements { get; set; }
        public GiftDisbursementList GiftDisbursementList { get; set; }

    }
    public class GiftDisbursementsDebitAccountResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public IEnumerable<GiftDisbursementDebitAccount> GiftDisbursementsDebitAccounts { get; set; }

    }


    public class CreateGiftDisbursementResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public long GiftDisbursementId { get; set; }

    }

    public class UAFApproval
    {
        //public bool Approved { get; set; }
        //public string ApproverType { get; set; }
        //public string Comments { get; set; }

        public bool Approved { get; set; }
        public int ApproverRoleId { get; set; }
        public string Comments { get; set; }
        public int? ApprovingAsRoleId { get; set; }

    }

    public class GetProjectAccount
    {
        public string accountNumber { get; set; }
        public string projectId { get; set; }
        public string objectCode { get; set; }
    }

    public class ProjectAccountDetails
    {
        public string AccountNumber { get; set; }
        public string AccountDescription { get; set; }
    }

    public class ObjectCodeDetailsResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public ProjectAccountDetails ObjectCodeDetails { get; set; }

    }
}
