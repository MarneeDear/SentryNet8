using Microsoft.AspNetCore.Mvc.Rendering;
using Sentry.Domain.AccountsPayable.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Sentry.WebApp.ViewModels.FundsTransfer
{
    public class FundsTransferViewModel : BaseIntegrationViewModel, IValidatableObject
    {
        public const string Approved = "Approved";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.Items != null && this.TransferRoutingType != 4)
            {
                foreach (var item in this.Items)

                {
                    string projectId = item.ProjectId.Substring(0, 5);

                    //Checks the first 5 characters of the project id to determine which account number it should have
                    switch (projectId)
                    {
                        case "40-10" when (String.IsNullOrWhiteSpace(item.AccountNumber) || !item.AccountNumber.StartsWith("40")):
                        case "40-11" when (String.IsNullOrWhiteSpace(item.AccountNumber) || !item.AccountNumber.StartsWith("40")):
                            yield return new ValidationResult($"Please enter a valid To account number starting with 40- for project {item.ProjectId}");
                            break;

                        case "40-12" when (String.IsNullOrWhiteSpace(item.AccountNumber) || !item.AccountNumber.StartsWith("30")):
                        case "40-13" when (String.IsNullOrWhiteSpace(item.AccountNumber) || !item.AccountNumber.StartsWith("30")):
                            yield return new ValidationResult($"Please enter a valid To account number starting with 30- for project {item.ProjectId}");
                            break;

                        case "20-10" when (String.IsNullOrWhiteSpace(item.AccountNumber) || !item.AccountNumber.StartsWith("20")):
                        case "20-11" when (String.IsNullOrWhiteSpace(item.AccountNumber) || !item.AccountNumber.StartsWith("20")):
                            yield return new ValidationResult($"Please enter a valid To account number starting with 20- for project {item.ProjectId}");
                            break;

                        case "10-10" when (String.IsNullOrWhiteSpace(item.AccountNumber) || !item.AccountNumber.StartsWith("10")):
                        case "10-11" when (String.IsNullOrWhiteSpace(item.AccountNumber) || !item.AccountNumber.StartsWith("10")):
                            yield return new ValidationResult($"Please enter a valid To account number starting with 10- for project {item.ProjectId}");
                            break;

                    }                 
                }
                if (this.Items
                        .Where(i => !i.Deleted && String.IsNullOrWhiteSpace(i.ProjectId))
                        .Any())
                {
                    yield return new ValidationResult("A To Project is required");
                }

                if (this.Items
                    .Where(i => !i.Deleted && i.Amount <= 0)
                    .Any())
                {
                    yield return new ValidationResult("The Project Amount is required");
                }
            }           

            string fromProjectId = this.ProjectId.Substring(0, 5);

            //Checks the first 5 characters of the project id to determine which account number it should have
            if (this.TransferRoutingType != 4)
            {
                switch (fromProjectId)
                {
                    case "40-10" when (String.IsNullOrWhiteSpace(this.AccountNumber) || !this.AccountNumber.StartsWith("40")):
                    case "40-11" when (String.IsNullOrWhiteSpace(this.AccountNumber) || !this.AccountNumber.StartsWith("40")):
                        yield return new ValidationResult($"Please enter a valid From account number starting with 40- for project {this.ProjectId}");
                        break;

                    case "40-12" when (String.IsNullOrWhiteSpace(this.AccountNumber) || !this.AccountNumber.StartsWith("30")):
                    case "40-13" when (String.IsNullOrWhiteSpace(this.AccountNumber) || !this.AccountNumber.StartsWith("30")):
                        yield return new ValidationResult($"Please enter a valid From account number starting with 30- for project {this.ProjectId}");
                        break;

                    case "20-10" when (String.IsNullOrWhiteSpace(this.AccountNumber) || !this.AccountNumber.StartsWith("20")):
                    case "20-11" when (String.IsNullOrWhiteSpace(this.AccountNumber) || !this.AccountNumber.StartsWith("20")):
                        yield return new ValidationResult($"Please enter a valid From account number starting with 20- for project {this.ProjectId}");
                        break;

                    case "10-10" when (String.IsNullOrWhiteSpace(this.AccountNumber) || !this.AccountNumber.StartsWith("10")):
                    case "10-11" when (String.IsNullOrWhiteSpace(this.AccountNumber) || !this.AccountNumber.StartsWith("10")):
                        yield return new ValidationResult($"Please enter a valid From account number starting with 10- for project {this.ProjectId}");
                        break;

                }

            }

            if (String.IsNullOrWhiteSpace(this.TransferPurpose))
            {
                yield return new ValidationResult("UofA Transfer Purpose is required.");
            }
            
            if(this.TransferRoutingType == null
                || this.TransferRoutingType <= 0 
                || this.TransferRoutingType > 4)
            {
                yield return new ValidationResult("A Routing Type is required");
            }
            if (String.IsNullOrWhiteSpace(this.PostDate))
            {
                yield return new ValidationResult("The Post Date is required");
            }

            yield return ValidationResult.Success;
        }

        public long FundsTransferId { get; set; }
        public string PreparedByEmployeeId { get; set; }
        public string PreparedByEmail { get; set; }
        public string PreparedByFirstName { get; set; }
        public string PreparedByLastName { get; set; }
        public string PreparedByPhone { get; set; }
        public string PreparedByJobTitle { get; set; }
        public string FormNumber { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string Phone { get; set; }
        public string TransferFrom { get; set; }
        public string TransferPurpose { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string ProjectId { get; set; }
        public bool IsRestrictedOrEndowment { get; set; }
        public string ProjectDescription { get; set; }
        public decimal ProjectBalance { get; set; }
        public DateTime? SubmittedforApprovalOn { get; set; }
        public IList<FundsTransferItem> Items { get; set; }
        public IEnumerable<FundsTransferApprover> Approvers { get; set; }
        public IEnumerable<FundsTransferApprover> ApprovalHistory { get; set; }
        public bool IsReadyForProcessing { get; set; }
        public bool AllowProcessing { get; set; }
        public decimal OverallTotal { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowFileUpload { get; set; }
        public int? TransferRoutingType { get; set; }
        //public bool Routed { get; set; }
        //public int? RoutingSelect { get; set; }
        public IEnumerable<SelectListItem> TransferRoutingTypes { get; set; }
        //public bool PreviouslyApprovedByRole { get; set; }
        //public bool PreviouslyApprovedByUser { get; set; }
        public string ApprovingOnBehalfOfRole { get; set; }
        public IList<string> ValidFileTypes { get; set; }
        public SupportingDocuments.SupportingDocumentsListViewModel SupportingDocuments { get; set; }
        public bool AllowApproval { get; set; }
        public bool PreviouslyApprovedByUser { get; set; }
        public bool PreviouslyApprovedByRole { get; set; }
        public string CreatedOn { get; set; }
        public string CampusApproveOn { get; set; }
        public string CampusApprovalStatus { get; set; }
        public bool IsPending { get; set; }
        public string Comments { get; set; }
        public string CCEmails { get; set; }
        public string PendingComments { get; set; }
        public string PostDate { get; set; }
        public string SignatureAuthorityEmployeeId { get; set; }
        public string DesigneeEmployeeId { get; set; }
        public IEnumerable<FundsTransferJournalEntry> FundsTransferJournalEntries { get; set; }
        public string ProcessingError { get; set; }
        public string LineItemDescription { get; set; }
        public string PreviousPage { 
            get {
                if (this.IsReadyForProcessing)
                    return "ReadyForProcessing";
                else
                {
                    if(this.Approvers != null)
                    {
                        if(this.Approvers.Any(a => a.Type == Constants.FTReviewerRole
                                                && !a.Approved.HasValue))
                        {
                            return "Unrouted";
                        }
                        else if (this.Approvers.Any(a => a.Type == Constants.FTApproverRole
                                                && !a.Approved.HasValue))
                        {
                            switch (this.TransferRoutingType)
                            {
                                case 1:
                                    return "Restricted";
                                case 2:
                                    return "Unrestricted";
                                case 3:
                                    return "Endowment";
                                case 4:
                                    return "Gift";
                                default:
                                    break;
                            }
                        }
                        else if (this.Approvers.Any(a => a.Type == Constants.FTGeneralCounselApproverRole)
                                                    && this.Approvers.Where(a => a.Type == Constants.FTGeneralCounselApproverRole
                                                    && !a.Approved.HasValue).Any())
                        {
                            return "GeneralCounsel";
                        }
                    }
                    
                }
                return "";
            } 
        }

        public FundsTransferViewModel()
        {
            this.SupportingDocuments = new SupportingDocuments.SupportingDocumentsListViewModel();
        }

        public static explicit operator FundsTransferViewModel(Sentry.Domain.AccountsPayable.Entities.FundsTransfer fundsTransferRequest)
        {
            var signatureAuthorityEmployeeId = string.Empty;
            var designeeEmployeeId = string.Empty;

            if (fundsTransferRequest.FundsTransferApprovers.Any(a => a.Description == "Signature Authority"))
            {
                signatureAuthorityEmployeeId = fundsTransferRequest.FundsTransferApprovers.First(a => a.Description == "Signature Authority").ApproverEmployeeId;
            }

            if (fundsTransferRequest.FundsTransferApprovers.Any(a => a.Description == "Designee"))
            {
                designeeEmployeeId = fundsTransferRequest.FundsTransferApprovers.First(a => a.Description == "Designee").ApproverEmployeeId;
            }

            return new FundsTransferViewModel
            {
                FundsTransferId = fundsTransferRequest.Id,
                PreparedByEmployeeId = fundsTransferRequest.PreparedBy,
                FormNumber = fundsTransferRequest.FormNumber,
                PreparedByFirstName = fundsTransferRequest.PreparedByFirstName,
                PreparedByLastName = fundsTransferRequest.PreparedByLastName,
                PreparedByEmail = fundsTransferRequest.PreparedByEmail,
                PreparedByJobTitle = fundsTransferRequest.PreparedByJobTitle,
                DivisionCode = fundsTransferRequest.DivisionCode,
                DivisionName = fundsTransferRequest.DivisionName,
                DepartmentCode = fundsTransferRequest.DepartmentCode,
                DepartmentName = fundsTransferRequest.DepartmentName,
                Phone = fundsTransferRequest.Phone,
                AccountNumber = fundsTransferRequest.FromAccountNumber,
                AccountName = fundsTransferRequest.FromAccountName,
                IsRestrictedOrEndowment = fundsTransferRequest.IsRestrictedOrEndowment,
                ProjectId = fundsTransferRequest.FromProjectId,
                ProjectDescription = fundsTransferRequest.FromProjectDescription,
                ProjectBalance = fundsTransferRequest.FromProjectBalance ?? decimal.Zero,
                TransferPurpose = fundsTransferRequest.TransferPurpose,
                SubmittedforApprovalOn = fundsTransferRequest.SubmittedForApprovalOn,
                Items = fundsTransferRequest.FundsTransferItems.Select(i => (FundsTransferItem)i).ToList(),
                OverallTotal = fundsTransferRequest.FundsTransferItems.Sum(i => i.Amount),
                ApprovalHistory = fundsTransferRequest.FundsTransferApprovalHistory.Select(h => (FundsTransferApprover)h),
                Approvers = fundsTransferRequest.FundsTransferApprovers.Select(a => (FundsTransferApprover)a),
                CreatedOn = fundsTransferRequest.CreateDate.ToString("MM/dd/yyyy"),
                IsPending = fundsTransferRequest.IsPending,
                PendingComments = fundsTransferRequest.PendingComments,
                CampusApproveOn = fundsTransferRequest.FundsTransferApprovers
                                    .Where(a => a.Description == "Designee" || a.Description == "Signature Authority" || a.Description == "FT Unrestricted (Campus)")
                                    .OrderByDescending(o => o.ApprovedOn)
                                    .First()
                                    .ApprovedOn
                                    .ToString("MM/dd/yyyy"),
                CampusApprovalStatus = fundsTransferRequest.CampusApprovalStatus,
                PostDate = (fundsTransferRequest.PostDate.HasValue ? fundsTransferRequest.PostDate.Value.ToString("MM/dd/yyyy") : DateTime.Now.ToString("MM/dd/yyyy")),
                TransferRoutingType = fundsTransferRequest.RoutingType,
                SignatureAuthorityEmployeeId = signatureAuthorityEmployeeId,
                DesigneeEmployeeId = designeeEmployeeId, 
                CCEmails = fundsTransferRequest.CCEmails,
                ProcessingError = fundsTransferRequest.ProcessingError,
                LineItemDescription = fundsTransferRequest.FromLineItemDescription

            };
        }
        public static explicit operator Sentry.Domain.AccountsPayable.Entities.FundsTransfer(FundsTransferViewModel model)
        {
            return new Sentry.Domain.AccountsPayable.Entities.FundsTransfer
            {
                Id = model.FundsTransferId,
                PreparedByEmail = model.PreparedByEmail,
                PreparedByFirstName = model.PreparedByFirstName,
                PreparedByLastName = model.PreparedByLastName,
                Phone = model.PreparedByPhone,
                FormNumber = model.FormNumber,
            };
        }
        public static explicit operator Sentry.Domain.AccountsPayable.Entities.UpdateFundsTransfer(FundsTransferViewModel fundsTransferRequest)
        {
            var dPostDate = DateTime.MinValue;
            if (null != fundsTransferRequest.PostDate)
            {
                DateTime.TryParse(fundsTransferRequest.PostDate, out dPostDate);
            }

            return new Sentry.Domain.AccountsPayable.Entities.UpdateFundsTransfer
            {
                Id = fundsTransferRequest.FundsTransferId,
                FormNumber = fundsTransferRequest.FormNumber,
                PreparedByEmployeeId = fundsTransferRequest.PreparedByEmployeeId,
                Phone = fundsTransferRequest.Phone,
                AccountNumber = fundsTransferRequest.AccountNumber,
                IsRestrictedOrEndowment = fundsTransferRequest.IsRestrictedOrEndowment,
                ProjectId = fundsTransferRequest.ProjectId,
                TransferPurpose = fundsTransferRequest.TransferPurpose,
                IsPending = fundsTransferRequest.IsPending,
                PendingComments = fundsTransferRequest.PendingComments,
                PostDate = (dPostDate > DateTime.MinValue ? dPostDate : null),
                TransferRoutingType = fundsTransferRequest.TransferRoutingType.Value,
                SignatureAuthorityEmployeeId = fundsTransferRequest.SignatureAuthorityEmployeeId,
                DesigneeEmployeeId = fundsTransferRequest.DesigneeEmployeeId,
                FundsTransferItems = fundsTransferRequest.Items.Select(i => (Sentry.Domain.AccountsPayable.Entities.FundsTransferItem)i),
                CCEmails = fundsTransferRequest.CCEmails,
                LineItemDescription = fundsTransferRequest.LineItemDescription
            };
        }
    }

    public class FundsTransferItem
    {
        public long? Id { get; set; }
        public long FundsTransferId { get; set; }
        public bool Deleted { get; set; }
        public string ProjectId { get; set; }
        public string ProjectDescription { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public bool IsRestricted { get; set; }
        public bool IsEndowment { get; set; }
        public decimal Amount { get; set; }

        public static explicit operator FundsTransferItem(Sentry.Domain.AccountsPayable.Entities.FundsTransferItem model)
        {
            return new FundsTransferItem
            {
                Id = model.Id,
                FundsTransferId = model.FundsTransferId,
                ProjectId = model.ProjectId,
                ProjectDescription = model.ProjectName,
                AccountNumber = model.AccountNumber,
                AccountName = model.AccountName,
                IsRestricted = model.IsRestricted,
                IsEndowment = model.IsEndowment,
                Amount = model.Amount
            };
        }

        public static explicit operator Sentry.Domain.AccountsPayable.Entities.FundsTransferItem(FundsTransferItem model)
        {
            return new Sentry.Domain.AccountsPayable.Entities.FundsTransferItem
            {
                Id = model.Id,
                FundsTransferId = model.FundsTransferId,
                ProjectId = model.ProjectId,
                AccountNumber = model.AccountNumber,
                IsRestricted = model.IsRestricted,
                IsEndowment = model.IsEndowment,
                Amount = model.Amount
            };
        }
    }

    public class FundsTransferApprover
    {
        public string Type { get; set; }
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime ApprovedOn { get; set; }
        public bool? Approved { get; set; }
        public string Comments { get; set; }

        public static explicit operator FundsTransferApprover(Sentry.Domain.AccountsPayable.Entities.FundsTransferApprover fundsTransferApprover)
        {
            return new FundsTransferApprover
            {
                Type = fundsTransferApprover.Description,
                EmployeeId = fundsTransferApprover.ApproverEmployeeId,
                FirstName = fundsTransferApprover.FirstName,
                LastName = fundsTransferApprover.LastName,
                Email = fundsTransferApprover.Email,
                Approved = fundsTransferApprover.Approved,
                ApprovedOn = fundsTransferApprover.ApprovedOn,
                Comments = fundsTransferApprover.Comments
            };
        }
    }

    public class FundsTransferJournalEntry
    {
        public string Account { get; set; }
        public string PostDate { get; set; }
        public string Journal { get; set; }
        public string JournalReference { get; set; }
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        public string Project { get; set; }
        public string ProjectName { get; set; }
        public string AccountName { get; set; }


        public static explicit operator FundsTransferJournalEntry(Sentry.Domain.AccountsPayable.Entities.FundsTransferJournalEntry entity)
        {
            return new FundsTransferJournalEntry
            {
                Account = entity.Account,
                PostDate = entity.PostDate.ToString("MM/dd/yyyy"),
                Journal = entity.Journal,
                JournalReference = entity.JournalReference,
                Credit = entity.Credit,
                Debit = entity.Debit,
                Project = entity.Project,
                ProjectName = entity.ProjectName,
                AccountName = entity.AccountName
            };
        }

    }

}
