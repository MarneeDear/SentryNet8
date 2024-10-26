using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Sentry.Domain.AccountsPayable.Entities
{
    public class FundsTransfer
    {
        public long Id { get; set; }
        public string PreparedBy { get; set; }
        public string PreparedByFirstName { get; set; }
        public string PreparedByLastName { get; set; }
        public string PreparedByEmail { get; set; }
        public string PreparedByJobTitle { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string Phone { get; set; }
        public string TransferPurpose { get; set; }
        public string FormNumber { get; set; } //TF         
        public bool IsRestrictedOrEndowment { get; set; }
        public string FromAccountNumber { get; set; }
        public string FromAccountName { get; set; }
        public string FromProjectId { get; set; }
        public string FromProjectDescription { get; set; }
        public decimal? FromProjectBalance { get; set; }
        public string FromLineItemDescription { get; set; }
        public DateTime CreateDate { get; set; }
        public string CampusApprovalStatus { get; set; }
        public DateTime? SubmittedForApprovalOn { get; set; }
        public bool IsPending { get; set; }
        public string PendingComments { get; set; }
        public int RoutingType { get; set; }
        public IEnumerable<FundsTransferApprover> FundsTransferApprovers { get; set; }
        public IEnumerable<FundsTransferApprover> FundsTransferApprovalHistory { get; set; }
        public IEnumerable<FundsTransferItem> FundsTransferItems { get; set; }
        public string ProcessingError { get; set; }
        public DateTime? PostDate { get; set; }
        public string CCEmails { get; set; }
        
    }

    public class FundsTransferItem
    {
        public long? Id { get; set; }
        public long FundsTransferId { get; set; }
        public decimal Amount { get; set; }
        public bool IsRestricted { get; set; }
        public bool IsEndowment { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

    public class FundsTransferApprover
    {
        public string Description { get; set; }
        public string ApproverEmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime ApprovedOn { get; set; }
        public bool? Approved { get; set; }
        public string Comments { get; set; }

    }

    public class FundsTransferTo
    {
        public string ProjectId { get; set; }
        public string AccountNumber { get; set; }
        public bool IsRestricted { get; set; }
    }

    public class FundsTransferFrom
    {
        public string ProjectId { get; set; }
        public string AccountNumber { get; set; }
        public bool IsRestricted { get; set; }
    }


    public class FundsTransfersResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public IEnumerable<FundsTransfer> FundsTransferList { get; set; }

    }

    public class FundsTransferResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public FundsTransfer FundsTransfer { get; set; }

    }

    public class UpdateFundsTransfer
    {
        public long? Id { get; set; }
        public string PreparedByEmployeeId { get; set; }
        public string Phone { get; set; }
        public string TransferPurpose { get; set; }
        public string FormNumber { get; set; } //TF 
        public long FundsTransferId { get; set; }
        public bool IsRestrictedOrEndowment { get; set; }
        public string AccountNumber { get; set; }
        public IEnumerable<FundsTransferItem> FundsTransferItems { get; set; }
        public string ProjectId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? PostDate { get; set; }
        public int TransferRoutingType { get; set; }
        public bool IsPending { get; set; }
        public string PendingComments { get; set; }
        public string SignatureAuthorityEmployeeId { get; set; }
        public string DesigneeEmployeeId { get; set; }
        public string CCEmails { get; set; }
        public string LineItemDescription { get; set; }
    }

    public class TransferRoutingTypes
    {
        public int Id { get; set; }
        public string RoutingTypeDescription { get; set; }
    }

    public class TransferRoutingTypesResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public IEnumerable<TransferRoutingTypes> TransferRoutingTypes { get; set; }

    }

    public class FundsTransferSummary
    {
        public long Id { get; set; }
        public string PreparedByFirstName { get; set; }
        public string PreparedByLastName { get; set; }
        public string Status { get; set; }
        public string FormNumber { get; set; } //TF         
        public string FromProjectId { get; set; }
        public string ToProjectIds { get; set; }
        public DateTime SubmittedForApprovalOn { get; set; }
        public decimal TransferAmount { get; set; }
        public bool IsRestrictedOrEndownment { get; set; }
        public int RoutingType { get; set; }
        public string RoutingTypeDescription { get; set; }
        public bool RequiresGeneralCounsel {  get; set; }
        public bool IsPending { get; set; }
        public string PendingComments { get; set; }
        public DateTime? LastApprovedOnDate { get; set; }
        public string ProcessingError { get; set; }
        public string UAFStage { get; set; }
    }

    public class FundsTransferListResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public IEnumerable<FundsTransferSummary> FundsTransferList { get; set; }

    }

    public class FundsTransferCountsResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public IEnumerable<FundsTransferCount> FundsTransferCounts { get; set; }

    }

    public class FundsTransferCount
    {
        public int BucketId { get; set; }
        public string BucketName { get; set; }
        public int Count { get; set; }

    }

    public class FundsTransferJournalEntriesResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public IEnumerable<FundsTransferJournalEntry> FundsTransferJournalEntries { get; set; }
    }

    public class FundsTransferJournalEntry
    {
        public string Account { get; set; }
        public DateTime PostDate { get; set; }
        public string Encumbrance { get; set; } = "Regular";
        public string Journal { get; set; }
        public string JournalReference { get; set; }
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        public string Project { get; set; }
        public string ProjectName { get; set; }
        public string AccountName { get; set; }

    }

}
