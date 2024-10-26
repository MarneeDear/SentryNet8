using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sentry.Domain.AccountsPayable.Entities;
using Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels.GiftTransmittal
{
    public class GiftTransmittalViewModel : BaseIntegrationViewModel, IValidatableObject
    {
        public GiftTransmittalViewModel() : base() { }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Custom validation.
            yield return ValidationResult.Success;
        }

        public int ApproverStage
        {
            get
            {
                if (this.Approvals.Any())
                {
                    var approvers = this.Approvals; // giftTransmittal.GiftTransmittalApprovals;
                    var reviewer = approvers.Where(a => a.ApprovalStageCode == 1).First();
                    //var secondary = approvers.Where(a => a.ApprovalStageCode == 2).First();
                    var final = approvers.Where(a => a.ApprovalStageCode == 3).First();

                    if (!reviewer.ApprovedOn.HasValue)
                    {
                        return 1;
                    }

                    if(approvers.Where(a => a.ApprovalStageCode == 2).Any())
                    {
                        if (!approvers.Where(a => a.ApprovalStageCode == 2).First().ApprovedOn.HasValue)
                        {
                            return 2;
                        }
                    }                    

                    if (!final.ApprovedOn.HasValue)
                    {
                        return 3;
                    }
                }                

                return 0;
            }            
        }


        public bool ExpandDistributions { get; set; } = false;
        public Guid GiftTransmittalId { get; set; }   
        public Guid ItemId { get; set; }
        //public string ObjectCode { get; set; }
        
        //public string ItemDescription { get; set; }
        public string Organization { get; set; }
        public string FormNumber { get; set; }
        public string FormState { get; set; }
        public int EFormCode { get; set; }
        public string EFormName { get; set; }
        public string Comments { get; set; }
        public string CCEmails { get; set; }
        public string ProcessingComments { get; set; }
        public int? FormStatusCode { get; set; }
        public bool ReceivedPhysicalDocuments { get; set; }
        public bool WaitingOnResponseFromBursar { get; set; }
        public bool WaitingOnResponseFromPreparer { get; set; }
        public bool RequireSecondaryApprover { get; set; }
        public string StatusComments { get; set; }
        public DateTime DateAdded { get; set; }
        public PreparedBy PreparedBy { get; set; }
        public Constituent Constituent { get; set; }
        public IList<Approval> Approvals { get; set; }
        public IList<Approval> ApprovalHistory { get; set; }
        public string ApprovalComments { get; set; }
        public bool IncludeRecognitionCredit { get; set; } = false;
        public bool IsDeleted { get; set; }
        public bool? Approved { get; set; }
        public string PostDate { get; set; }
        public Transaction Transaction { get; set; }
        public List<Distribution> Distributions { get; set; }
        public Distribution Distribution { get; set; }
        public DistributionTotals DistributionTotals { get; set; }
        public Optional Optional { get; set; }
        //public Constituent RecognitionCredit { get; set; }
        public bool CheckPayableToTheUniversity { get; set; }
        public string CheckNumber { get; set; }
        public IList<Constituent> GiftTransmittalRecognitionCredits { get; set; }
        public IEnumerable<SelectListItem> Colleges { get; set; }
        public IEnumerable<SelectListItem> TitlesList { get; set; }
        public IEnumerable<SelectListItem> SuffixesList { get; set; }
        public IEnumerable<SelectListItem> CountriesList { get; set; }

        public IEnumerable<SelectListItem> StatesList { get; set; }
        public SupportingDocuments.SupportingDocumentsListViewModel SupportingDocuments { get; set; }
        //public IEnumerable<SelectListItem> BatchTypeCodes { get; set; } = new List<SelectListItem>() { new SelectListItem { Value = "0", Text = "TODO" } };
        public IFormFile SupportingDocument { get; set; }
        public IList<string> ValidFileTypes { get; set; }

        public bool HasProcessingError { get;set; }

        public static explicit operator GiftTransmittalViewModel(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal giftTransmittal)
        {
            var aprovalHistory = giftTransmittal.GiftTransmittalApprovalHistory != null ? giftTransmittal.GiftTransmittalApprovalHistory : Enumerable.Empty<GiftTransmittalApproval>();
            var approvals = giftTransmittal.GiftTransmittalApprovals != null ? giftTransmittal.GiftTransmittalApprovals : Enumerable.Empty<GiftTransmittalApproval>();
            return new GiftTransmittalViewModel()
            {
                GiftTransmittalId = giftTransmittal.Id,
                //Organization = "TODO",
                FormNumber = giftTransmittal.FormNumber,
                EFormCode = giftTransmittal.EFormCode,
                FormState = giftTransmittal.FormState,
                FormStatusCode = giftTransmittal.FormStatusCode,
                PreparedBy = (PreparedBy)giftTransmittal,
                CheckPayableToTheUniversity = giftTransmittal.CheckPayableToUniversity,
                CheckNumber = giftTransmittal.CheckNumber,
                Approvals = approvals
                    //.Where(a => a.ApprovalStageCode != 3)
                    .Select(a => (Approval)a)
                    .OrderBy(a => a.ApproverType)
                    .ToList(),
                ApprovalHistory = aprovalHistory
                    .Where(a => a.ApprovalStageCode != 3)
                    .Where(a => a.ApprovedOn.HasValue)
                    .Select(a => (Approval)a)
                    .OrderByDescending(a => a.ApprovedOn).ToList(),
                //Constituent = (Constituent)giftTransmittal.GiftTransmittalItems.First(),
                GiftTransmittalRecognitionCredits = giftTransmittal.GiftTransmittalItems.First().GiftTransmittalItemRecognitionCredits
                                                                            .Where(c => !c.IsDeleted)
                                                                            .Select(c => (Constituent)c).ToList(),
            Transaction = (Transaction)giftTransmittal,
                Optional = (Optional)giftTransmittal.GiftTransmittalItems.First(),
                ReceivedPhysicalDocuments = giftTransmittal.ReceivedPhysicalDocuments,
                IsDeleted = giftTransmittal.IsDeleted,
                WaitingOnResponseFromBursar = giftTransmittal.WaitingOnResponseFromBursar,
                WaitingOnResponseFromPreparer = giftTransmittal.WaitingOnResponseFromPreparer,
                StatusComments = giftTransmittal.StatusComments,
                ProcessingComments = giftTransmittal.ProcessingComments,
                PostDate = DateTime.Now.ToString("MM/dd/yyyy")
        };
    }
        public static explicit operator Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItem(GiftTransmittalViewModel model)
        {
            IEnumerable<Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemRecognitionCredit> recognitionCredits = new List<Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemRecognitionCredit>();

            if (null != model.GiftTransmittalRecognitionCredits)
            {
                recognitionCredits = model.GiftTransmittalRecognitionCredits.Select(c => (Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemRecognitionCredit)c);
            }

            return new Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItem()
            {
                Id = model.ItemId,
                GiftTransmittalId = model.GiftTransmittalId,
                SequenceNumber = 0, 
                ConstituentId = model.Constituent.ConstituentId,
                ConstituentTitle = model.Constituent.Title,
                ConstituentSuffix = model.Constituent.Suffix,
                ConstituentLookupId = model.Constituent.LookupId,
                ConstituentFirstName = model.Constituent.FirstName,
                ConstituentLastName = model.Constituent.LastName,
                ConstituentMiddleName = model.Constituent.MiddleName,
                ConstituentPrimaryPhone = model.Constituent.PhoneNumber,
                ConstituentPrimaryEmail = model.Constituent.Email,
                ConstituentAddress = model.Constituent.Address.Street,
                ConstituentCity = model.Constituent.Address.City,
                ConstituentState = model.Constituent.Address.State,
                ConstituentPostalCode = model.Constituent.Address.PostalCode,
                ConstituentCountry = model.Constituent.Address.Country,
                ConstituentOrganizationName = model.Constituent.OrganizationName,
                //ObjectCode = model.ObjectCode,
                //ItemDescription = model.ItemDescription,
                Appeal = model.Optional.Appeal,
                Package = model.Optional.Package,
                GiftTransmittalItemRecognitionCredits = recognitionCredits,
                //!model.GiftTransmittalRecognitionCredits.Any()
                //                    ? new List<Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemRecognitionCredit>()
                //                    : model.GiftTransmittalRecognitionCredits.Select(c => (Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemRecognitionCredit)c),
                DateAdded = DateTime.Now, 
                AddedById = Guid.Empty, 
                DateChanged = DateTime.Now, 
                ChangedById = Guid.Empty, 
                IsDeleted = model.IsDeleted,
                GiftTransmittalItemDistributions = model.Distributions.Select(d => (Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemDistribution)d)
            };
        }
        public static explicit operator Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal(GiftTransmittalViewModel model)
        {
            var gtItems = new List<Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItem>()
            {
                (Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItem)model
            };

            var dPostDate = DateTime.MinValue;
            if (null != model.PostDate)
            {
                DateTime.TryParse(model.PostDate, out dPostDate);
            }

            return new Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal()
            {
                Id = model.GiftTransmittalId,
                EFormCode = model.EFormCode,
                FormNumber = model.FormNumber,
                PreparedByName = model.PreparedBy.PreparedByName,
                PreparedByDate = DateTime.Now,
                ContactName = model.PreparedBy.ContactName,
                ContactEmail = model.PreparedBy.ContactEmail,
                ContactPhone = model.PreparedBy.ContactPhoneNumber,
                BatchTypeCode = (byte)model.Transaction.BatchTypeCode,
                BatchTypeOtherDesc = model.Transaction.BatchTypeOtherDesc,
                Comments = model.Transaction.Comments,
                CCEmails = model.CCEmails,
                ProcessingComments = model.ProcessingComments,
                FormStatusCode = (int)model.FormStatusCode,
                DateAdded = model.DateAdded,
                DateChanged = DateTime.Now,
                ReceivedPhysicalDocuments = model.ReceivedPhysicalDocuments,
                WaitingOnResponseFromBursar = model.WaitingOnResponseFromBursar,
                WaitingOnResponseFromPreparer = model.WaitingOnResponseFromPreparer,
                CheckNumber = model.CheckNumber,
                CheckPayableToUniversity = model.CheckPayableToTheUniversity,
                StatusComments = model.StatusComments,
                PostDate = (dPostDate > DateTime.MinValue ? dPostDate : null),
                GiftTransmittalItems = gtItems
            };
        }


    }

    public class PreparedBy
    {
        //public string PreparedById { get; set; }
        public string PreparedByName { get; set; }

        public DateTime PreparedByDate { get; set; }

        public string ContactName { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }

        [Phone]
        public string ContactPhoneNumber { get; set; }

        public string ContactJobTitle { get; set; }

        public string ContactDepartmentCode { get; set; }

        public string ContactDepartmentName { get; set; }

        public string ContactNetId { get; set; }

        public static explicit operator PreparedBy(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal giftTransmittal)
        {
            return new PreparedBy()
            {
                PreparedByName = giftTransmittal.PreparedByName,
                PreparedByDate = giftTransmittal.PreparedByDate,
                ContactName = giftTransmittal.ContactName,
                ContactEmail = giftTransmittal.ContactEmail,
                ContactPhoneNumber = giftTransmittal.ContactPhone
            };
        }
    }
    //public class Approval
    //{
    //    public int ApprovalStatusCode { get; set; }
    //    public string ApprovedByEmployeeId { get; set; }
    //    public string ApprovalComments { get; set; }
    //    public DateTime? ApprovedDate { get; set; }

    //    public static explicit operator Approval(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal giftTransmittal)
    //    {
    //        return new Approval()
    //        {
    //            ApprovalStatusCode = giftTransmittal.ApprovalStatusCode,
    //            ApprovedByEmployeeId = giftTransmittal.ApprovedByEmployeeId,
    //            ApprovalComments = giftTransmittal.ApprovalComments,
    //            ApprovedDate = giftTransmittal.ApprovedDate,
    //        };
    //    }

    //}

    public class Constituent
    {
        //Guid of the recogntion credit record
        public Guid? Id { get; set; }

        public string ConstituentId { get; set; }

        public bool Deleted { get; set; }

        public string LookupId { get; set; }

        public string OrganizationName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Suffix { get; set; }

        public string Title { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public Address Address { get; set; }

        public string ConstituentFor { get; set; } = "Individual";

        public string[] ConstituentForOptions = new[] { "Individual", "Organization" };


        public static explicit operator Constituent(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItem giftTransmittalItem) //, Guid giftTransmitalId)
        {
            return new Constituent()
            {
                
                ConstituentId = giftTransmittalItem.ConstituentId,
                OrganizationName = giftTransmittalItem.ConstituentOrganizationName,
                LookupId = giftTransmittalItem.ConstituentLookupId,
                FirstName = giftTransmittalItem.ConstituentFirstName, //TODO when we change the data structure of the gift transmittal items to include all of the new fields
                LastName = giftTransmittalItem.ConstituentLastName,
                MiddleName = giftTransmittalItem.ConstituentMiddleName,
                Suffix = giftTransmittalItem.ConstituentSuffix,
                Title = giftTransmittalItem.ConstituentTitle,
                PhoneNumber = giftTransmittalItem.ConstituentPrimaryPhone,
                Email = giftTransmittalItem.ConstituentPrimaryEmail,
                Address = new Address()
                {
                    Street = giftTransmittalItem.ConstituentAddress,
                    City = giftTransmittalItem.ConstituentCity,
                    State = giftTransmittalItem.ConstituentState,
                    PostalCode = giftTransmittalItem.ConstituentPostalCode,
                    Country = giftTransmittalItem.ConstituentCountry
                }
            };
        }

        public static explicit operator Constituent(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemRecognitionCredit model) //, Guid giftTransmitalId)
        {
            return new Constituent()
            {
                Id = model.Id,
                ConstituentId = model.ConstituentId,
                LookupId = model.ConstituentLookupId,
                FirstName = model.ConstituentFirstName, //TODO when we change the data structure of the gift transmittal items to include all of the new fields
                LastName = model.ConstituentLastName,
                MiddleName = model.ConstituentMiddleName,
                Suffix = model.ConstituentSuffix,
                Title = model.ConstituentTitle,
                PhoneNumber = model.ConstituentPrimaryPhone,
                Email = model.ConstituentPrimaryEmail,
                Address = new Address()
                {
                    Street = model.ConstituentAddress,
                    City = model.ConstituentCity,
                    State = model.ConstituentState,
                    PostalCode = model.ConstituentPostalCode,
                    Country = model.ConstituentCountry
                }
            };
        }
        public static explicit operator Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemRecognitionCredit(Constituent recognitionCredit)
        {
            return new Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemRecognitionCredit()
            {
                Id = recognitionCredit.Id ?? Guid.Empty,
                IsDeleted = recognitionCredit.Deleted,
                ConstituentId = recognitionCredit.ConstituentId,
                ConstituentLookupId = recognitionCredit.LookupId,
                ConstituentAddress = recognitionCredit.Address.Street,
                ConstituentCity = recognitionCredit.Address.City,
                ConstituentState = recognitionCredit.Address.State,
                ConstituentPostalCode = recognitionCredit.Address.PostalCode,
                ConstituentCountry = recognitionCredit.Address.Country,
                ConstituentFirstName = recognitionCredit.FirstName,
                ConstituentMiddleName = recognitionCredit.MiddleName,
                ConstituentLastName = recognitionCredit.LastName,
                ConstituentTitle = recognitionCredit.Title,
                ConstituentSuffix = recognitionCredit.Suffix,
                ConstituentPrimaryPhone = recognitionCredit.PhoneNumber,
                ConstituentPrimaryEmail = recognitionCredit.Email
            };
        }
    }

    public class Transaction
    {
        public int BatchTypeCode { get; set; }
        public string BatchType { get; set; }

        public string BatchTypeOtherDesc { get; set; }

        public string Comments { get; set; }

        public static explicit operator Transaction(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal giftTransmittal) 
        {
            return new Transaction()
            {
               BatchTypeCode = giftTransmittal.BatchTypeCode,
               BatchTypeOtherDesc = giftTransmittal.BatchTypeOtherDesc,
               BatchType = giftTransmittal.BatchType,
               Comments = giftTransmittal.Comments
            };
        }

    }

    public class Distribution
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string Designation { get; set; }

        public string FundAccount { get; set; }

        [Required]
        public decimal Amount { get; set; }
        //[Required]
        public string ObjectCode_Amount { get; set; }

        public decimal? BenefitAmount { get; set; }
        public string ObjectCode_Benefit { get; set; }

        public decimal? ReceiptAmount { get; set; }

        public decimal? UdfFeeAmount { get; set; }

        public bool IsPledge { get; set; }

        public bool IsUdfExempt { get; set; }

        public int? UdfFeeExemptionId { get; set; }

        public string UdfFeeExemptionOtherDesc { get; set; }
        public string LineItemDescription { get; set; }


        public DateTime? DateAdded { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsNewFund { get; set; }

        public string ProjectDescription { get; set; }

        public decimal? UdfFeeDevelopment { get; set; }

        public decimal? UdfFeePresident { get; set; }

        public decimal? UdfFeeDean { get; set; }

        public string UdfDeanProject { get; set; }

        public DistributionTotals DistributionTotals { get; set; }

        public static explicit operator Distribution(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemDistribution giftTransmittalItemDistribution) 
        {
            return new Distribution()
            {
                Id = giftTransmittalItemDistribution.Id,
                ItemId = giftTransmittalItemDistribution.GiftTransmittalItemId,
                Designation = String.Empty, //TODO what is the difference between Designation and Fund Account
                FundAccount = giftTransmittalItemDistribution.FundAccount,
                Amount = giftTransmittalItemDistribution.Amount,
                ObjectCode_Amount = giftTransmittalItemDistribution.AmountObjectCode ?? String.Empty,
                BenefitAmount = giftTransmittalItemDistribution.BenefitAmount,
                ObjectCode_Benefit = giftTransmittalItemDistribution.BenefitAmountObjectCode ?? String.Empty,
                ReceiptAmount = giftTransmittalItemDistribution.ReceiptAmount,
                UdfFeeAmount = giftTransmittalItemDistribution.UdfFeeAmount,
                IsPledge = giftTransmittalItemDistribution.IsPledge,
                IsUdfExempt = giftTransmittalItemDistribution.IsUdfExempt,
                UdfFeeExemptionId = giftTransmittalItemDistribution.UdfFeeExemptionId,
                UdfFeeExemptionOtherDesc = giftTransmittalItemDistribution.UdfFeeExemptionOtherDesc,
                LineItemDescription = giftTransmittalItemDistribution.LineItemDescription,
                DateAdded = giftTransmittalItemDistribution.DateAdded,
                IsDeleted = giftTransmittalItemDistribution.IsDeleted,
                IsNewFund = giftTransmittalItemDistribution.IsNewFund,
                ProjectDescription = String.Empty, //TODO
                UdfFeeDevelopment = giftTransmittalItemDistribution.UdfFeeDevelopment,
                UdfFeePresident = giftTransmittalItemDistribution.UdfFeePresident,
                UdfFeeDean = giftTransmittalItemDistribution.UdfFeeDean,
                UdfDeanProject = giftTransmittalItemDistribution.UdfDeanProject
            };
        }
        public static explicit operator Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemDistribution(Distribution model)
        {
            return new Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemDistribution()
            {
                Id = model.Id,
                GiftTransmittalItemId = model.ItemId,                
                FundAccount = model.FundAccount,
                Amount = model.Amount,
                AmountObjectCode = model.ObjectCode_Amount ?? String.Empty,
                BenefitAmount = model.BenefitAmount ?? decimal.Zero,
                BenefitAmountObjectCode = model.ObjectCode_Benefit ?? String.Empty,
                ReceiptAmount = model.ReceiptAmount ?? decimal.Zero,
                UdfFeeAmount = model.UdfFeeAmount ?? decimal.Zero,
                IsPledge = model.IsPledge,
                IsUdfExempt = model.IsUdfExempt,
                UdfFeeExemptionId = model.UdfFeeExemptionId ?? 0,
                UdfFeeExemptionOtherDesc = model.UdfFeeExemptionOtherDesc,
                LineItemDescription = model.LineItemDescription,
                IsNewFund = model.IsNewFund,
                UdfFeeDevelopment = model.UdfFeeDevelopment,
                UdfFeePresident = model.UdfFeePresident,
                UdfFeeDean = model.UdfFeeDean,
                UdfDeanProject = model.UdfDeanProject,
                IsDeleted = model.IsDeleted
            };

        }


    }

    public class Approval
    {
        public string ApproverType
        {
            get
            {
                switch (this.ApprovalStageCode)
                {
                    case 1:
                        return "Reviewer";
                    case 2:
                        return "Secondary Reviewer";
                    case 3:
                        return "Final Reviewer";
                    default:
                        return "";
                }
            }
        }

        public Guid? Id { get; set; }
        public byte ApprovalStageCode { get; set; }
        public string ApproverEmployeeId { get; set; }
        public string ApproverFirstName { get; set; }
        public string ApproverLastName { get; set; }
        public string ApproverUserName { get; set; }
        public bool Approved { get; set; }
        public string Comments { get; set; }
        public DateTime? ApprovedOn { get; set; }

        public static explicit operator Approval(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalApproval approval)
        {
            return new Approval()
            {
                Id = approval.Id,
                ApprovalStageCode = approval.ApprovalStageCode,
                ApproverEmployeeId = approval.ApproverEmployeeId,
                ApproverFirstName = approval.ApproverFirstName,
                ApproverLastName = approval.ApproverLastName,
                ApproverUserName = approval.ApproverUserName,
                Approved = approval.Approved,
                Comments = approval.Comments,   
                ApprovedOn = approval.ApprovedOn
            };
        }

    }

    public class Optional
    {
        public string Appeal { get; set; }

        public string Package { get; set; }
        public static explicit operator Optional(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItem giftTransmittalItem)
        {
            return new Optional()
            {
                Appeal = giftTransmittalItem.Appeal,
                Package = giftTransmittalItem.Package
            };
        }
    }

    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public static explicit operator Sentry.Domain.MasterDataWebService.Entities.MailingAddress(Address address)
        {
            return new Sentry.Domain.MasterDataWebService.Entities.MailingAddress()
            {
                Address1 = address.Street,
                Address2 = String.Empty, //TODO
                City = address.City,
                StateCode = address.State,
                PostalCode = address.PostalCode,
                CountryCode = address.Country,
                MasterRecordId = String.Empty //TODO
            };
        }
    }

    public class DistributionTotals
    {        
        public Guid Id { get; set; }

        public int? ItemCount { get; set; }

        public decimal? GiftAmountTotal { get; set; }

        public decimal? BenefitAmountTotal { get; set; }

        public decimal? ReceiptAmountTotal { get; set; }

        public decimal? UDFFeeAmountTotal { get; set; }
       
        public decimal? UDFFeeDevelopmentAmountTotal { get; set; }

        public decimal? UDFFeePresidentAmountTotal { get; set; }

        public decimal UDFFeeDeanAmountTotal { get; set; }
    }

    public class FundData
    {
        public string id { get; set; }
        public int index { get; set; }
        public string organization { get; set; }
    }
    public class SelectData
    {
        public string collegeid { get; set; }
        public string id { get; set; }
        public int index { get; set; }
        public string organization { get; set; }
    }

    public class DeleteData
    {
        public Guid giftTransmittalId { get; set; }
        public Guid itemId { get; set; }
        public Guid distributionId { get; set; }
    }

    public class BursarPDF
    {
        public string MakerOfCheck { get; set; }
        public string FormNumber { get; set; }
        public string CheckNumber { get; set; }
        public PreparedBy PreparedBy { get; set; }
        public IList<Distribution> Distributions { get; set; }
    }

    #region Enums

    public enum GTBatchTypeCodes
    {
        Check = 1,
        [Display(Name = "Credit Card")]
        CreditCard = 2,
        Cash = 3,
        Other = 4,
        [Display(Name = "Pledge/Gift Commitment")]
        PledgeGiftCommitment = 5,
        [Display(Name = "Gift In-Kind ")]
        GiftInKind = 6,
        //[Display(Name = "Pledge - Legally Binding")]
        //PledgeLegallyBinding = 7,
        Wire = 8,
        Property = 9,
        Stock = 10
    }

    public enum GUBatchTypeCodes
    {
        [Display(Name = "Credit Card")]
        CreditCard = 2,
        Other = 4,
        [Display(Name = "Gift In-Kind")]
        GiftInKind = 6,
        Property = 9,
        Stock = 10,
        [Display(Name = "Cash/Check")]
        CashCheck = 11,
        Pledge = 12,
        [Display(Name = "Planned Gift")]
        PlannedGift = 13,
        [Display(Name = "Recurring Gift")]
        RecurringGift = 14,
        Membership = 15
    }

    //public enum FormStatusCodes
    //{
    //    Open = 1,
    //    Printed = 2
    //}

    public enum UdfFeeExemptionTypes
    {
        [Display(Name = "Scholarship (must be awarded within 12 mos)")]
        Scholarship = 1,
        Other = 2
    }

    //TODO with a list from MDS like we do in the rest of Sentry
    public enum PersonalSuffixCodes
    {
        Suffix,
        Mr,
        Miss,
        Mrs,
        Ms
    }

    //TODO with a list from MDS like we do in the rest of Sentry
    public enum PersonalTitleCodes
    {
        Title,
        Dr,
        JD,
        PhD
    }

    //TODO with a list from MDS like we do in the rest of Sentry
    public enum Countries
    {
        Country,
        USA,
        Denmark,
        [Display(Name = "Ivory Coast")]
        IvoryCoast,
        Chile
    }

    #endregion Enums

}
