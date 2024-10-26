using System;
using System.Collections.Generic;
using System.Text;

namespace Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal
{
    public class GiftTransmittalItemDistribution
    {
        public Guid Id { get; set; }
        public Guid GiftTransmittalItemId { get; set; }
        public int SequenceNumber { get; set; }
        public string FundAccount { get; set; }
        public decimal Amount { get; set; }
        public string AmountObjectCode { get; set; }
        public decimal BenefitAmount { get; set; }
        public string BenefitAmountObjectCode { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal UdfFeeAmount { get; set; }
        public bool IsPledge { get; set; }
        public bool IsUdfExempt { get; set; }
        public int UdfFeeExemptionId { get; set; }
        public string UdfFeeExemptionOtherDesc { get; set; }
        public string LineItemDescription { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid AddedById { get; set; }
        public DateTime DateChanged { get; set; }
        public Guid ChangedById { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsNewFund { get; set; }
        public decimal? UdfFeeDevelopment { get; set; }
        public decimal? UdfFeePresident { get; set; }
        public decimal? UdfFeeDean { get; set; }
        public string UdfDeanProject { get; set; }

    }

    public class GiftTransmittalItemRecognitionCredit
    {
        public Guid Id { get; set; }
        public Guid GiftTransmittalItemId { get; set; }
        public bool IsDeleted { get; set; }
        public string ConstituentId { get; set; }
        public string ConstituentLookupId { get; set; }
        public string ConstituentFirstName { get; set; }
        public string ConstituentLastName { get; set; }
        public string ConstituentMiddleName { get; set; }
        public string ConstituentTitle { get; set; }
        public string ConstituentSuffix { get; set; }
        public string ConstituentPrimaryPhone { get; set; }
        public string ConstituentPrimaryEmail { get; set; }
        public string ConstituentAddress { get; set; }
        public string ConstituentCity { get; set; }
        public string ConstituentState { get; set; }
        public string ConstituentPostalCode { get; set; }
        public string ConstituentCountry { get; set; }

    }

    public class GiftTransmittalItem
    {
        public Guid Id { get; set; }
        public Guid GiftTransmittalId { get; set; }
        //public string ObjectCode { get; set; }
        //public string ItemDescription { get; set; }
        public int SequenceNumber { get; set; }
        public string ConstituentId { get; set; }
        public string ConstituentLookupId { get; set; }
        public string ConstituentFirstName { get; set; }
        public string ConstituentLastName { get; set; }
        public string ConstituentMiddleName { get; set; }
        public string ConstituentTitle { get; set; }
        public string ConstituentSuffix { get; set; }
        public string ConstituentPrimaryPhone { get; set; }
        public string ConstituentPrimaryEmail { get; set; }
        public string ConstituentAddress { get; set; }
        public string ConstituentCity { get; set; }
        public string ConstituentState { get; set; }
        public string ConstituentPostalCode { get; set; }
        public string ConstituentCountry { get; set; }
        public string ConstituentOrganizationName { get; set; }
        public string Appeal { get; set; }
        public string Package { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid AddedById { get; set; }
        public DateTime DateChanged { get; set; }
        public Guid ChangedById { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<GiftTransmittalItemDistribution> GiftTransmittalItemDistributions { get; set; }
        public IEnumerable<GiftTransmittalItemRecognitionCredit> GiftTransmittalItemRecognitionCredits { get; set; }
    }

    public class GiftTransmittalApproval
    {
        public Guid? Id { get; set; }
        public Guid GiftTransmittalId { get; set; }
        public byte ApprovalStageCode { get; set; }
        public string ApproverEmployeeId { get; set; }
        public string ApproverFirstName { get; set; }
        public string ApproverLastName { get; set; }
        public string ApproverUserName { get; set; }
        public bool Approved { get; set; }
        public string Comments { get; set; }
        public DateTime? ApprovedOn { get; set; }

    }

    public class GiftTransmittal
    {
        public Guid Id { get; set; }
        public int EFormCode { get; set; }
        public string FormNumber { get; set; }
        public string PreparedByName { get; set; } //EMPLOYEE ID
        public DateTime PreparedByDate { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public byte BatchTypeCode { get; set; }
        public string BatchType { get; set; }
        public string BatchTypeOtherDesc { get; set; }
        public string Comments { get; set; }
        public string CCEmails { get; set; }
        public string ProcessingComments { get; set; }
        public int FormStatusCode { get; set; }
        public string FormState { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid AddedById { get; set; }
        public DateTime DateChanged { get; set; }
        public Guid LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
        //public int ApprovalStatusCode { get; set; }
        //public string ApprovedByEmployeeId { get; set; }
        //public string /*ApprovalComments*/ { get; set; }
        //public DateTime? ApprovedDate { get; set; }

        public bool ReceivedPhysicalDocuments { get; set; }
        public bool WaitingOnResponseFromBursar { get; set; }
        public bool WaitingOnResponseFromPreparer { get; set; }
        public bool CheckPayableToUniversity { get; set; }
        public string CheckNumber { get; set; }
        public string StatusComments { get; set; }
        public DateTime? PostDate { get; set; }
        public bool HasProcessingError { get;set; }
        public IEnumerable<GiftTransmittalItem> GiftTransmittalItems { get; set; }
        public IEnumerable<GiftTransmittalApproval> GiftTransmittalApprovals { get; set; }
        public IEnumerable<GiftTransmittalApproval> GiftTransmittalApprovalHistory { get; set; }
    }
    
}
