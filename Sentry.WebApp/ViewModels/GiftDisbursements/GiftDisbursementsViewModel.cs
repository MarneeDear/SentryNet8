using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Sentry.Domain.AccountsPayable.Entities;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Newtonsoft.Json;

namespace Sentry.WebApp.ViewModels.GiftDisbursements
{
    public class GiftDisbursementsViewModel : BaseIntegrationViewModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.GiftDisbursementProjects != null)
            {
                foreach (var project in this.GiftDisbursementProjects)
                {
                    if (project.ProjectItems.Where(p => String.IsNullOrEmpty(p.ObjectCode)).Any())
                    {
                        yield return new ValidationResult("The Transaction Object Code is required");
                    }

                    //if (project.ProjectItems.Where(p => String.IsNullOrEmpty(p.Description)).Any())
                    //{
                    //    yield return new ValidationResult("The Transaction Line Item Description is required");
                    //}

                    if (project.ProjectItems.Where(p => String.IsNullOrEmpty(p.DebitAccountNumber)).Any())
                    {
                        yield return new ValidationResult("The Transaction Debit Account # is required");
                    }

                    //var projectItems = 

                    if (project.ProjectItems.Where(p => !String.IsNullOrEmpty(p.DebitAccountNumber) && project.ProjectId.StartsWith("40")).Any())
                    {
                        if (project.ProjectItems.Where(p => !p.DebitAccountNumber.StartsWith("3")).Any())
                        {
                            yield return new ValidationResult($"Please enter a valid debit account number starting with 30- for project {project.ProjectId}");
                        }                        
                    }

                    if (project.ProjectItems.Where(p => !String.IsNullOrEmpty(p.DebitAccountNumber) && project.ProjectId.StartsWith("20")).Any())
                    {
                        if (project.ProjectItems.Where(p => !p.DebitAccountNumber.StartsWith("2")).Any())
                        {
                            yield return new ValidationResult($"Please enter a valid debit account number starting with 20- for project {project.ProjectId}");
                        }
                    }

                    if (Type != Constants.EMForm)
                    {
                        if (project.ProjectItems.Where(p => String.IsNullOrEmpty(p.UaAccount)).Any())
                        {
                            yield return new ValidationResult("The Transaction UA KFS Account is required");
                        }
                    }

                    if (Type != Constants.STForm)
                    {
                        if (project.ProjectItems.Where(p => !p.Deleted && p.Amount <= 0).Any())
                        {
                            yield return new ValidationResult("The Transaction Amount is required");
                        }
                    }
                }
            }

            yield return ValidationResult.Success;
        }

        public bool PreviouslyApprovedByRole { get; set; }
        public bool PreviouslyApprovedByUser { get; set; } 
        public string ApprovingOnBehalfOfRole { get; set; }
        public bool AllowEdit { get; set; }
        public bool IsReadyForProcessing { get; set; }
        public bool AllowProcessing { get; set; }
        public bool AllowApproval { get; set; }
        public string Organization { get; set; }
        public string Type { get; set; }
        public string PreparedByEmployeeId { get; set; }
        public string PreparedByFirstName { get; set; }
        public string PreparedByLastName { get; set; }
        public string PreparedByEmail { get; set; }
        public string PreparedByJobTitle { get; set; }
        public string CampusApproveOn { get; set; }
        public int ProjectsTotalCount { get; set; }
        public int TransactionsTotalCount { get; set; }
        public string CreatedOn { get; set; }

        [Required]
        public string InvoiceDescription { get; set; }
        public string ReviewerNotes { get; set; }
        public long GiftDisbursementId { get; set; }
        public string FormNumber { get; set; }
        public decimal OverallTotal { get; set; }
        public string PostDate { get; set; }
        public decimal? RentsAmount { get; set; }
        public decimal? OtherIncomeAmount { get; set; }
        public decimal? NonemployeeCompensationAmount { get; set; }
        public decimal? GrossProceedsPaidToAttorneyAmount { get; set; }

        public GiftDisbursementFrom DisbursementFrom { get; set; }

        public GiftDisbursementTo DisbursementTo { get; set; }
        public IList<GiftDisbursementStudent> GiftDisbursementStudents { get; set; }
        public IEnumerable<GiftDisbursementApprover> Approvers { get; set; }
        public IEnumerable<GiftDisbursementApprover> ApprovalHistory { get; set; }
        public string SignatureAuthorityEmployeeId { get; set; }
        public string DesigneeEmployeeId { get; set; }
        public DateTime? SubmittedforApprovalOn { get; set; }
        public IList<GiftDisbursementProject> GiftDisbursementProjects { get; set; }

        public SupportingDocuments.SupportingDocumentsListViewModel SupportingDocuments { get; set; }
        public IFormFile SupportingDocument { get; set; }
        public IList<string> ValidFileTypes { get; set; }

        public bool IsPending { get; set; }
        public string Comments { get; set; }
        public string CCEmails { get; set; }
        public string PendingComments { get; set; }

        public GiftDisbursementsViewModel()
        {
            this.SupportingDocuments = new SupportingDocuments.SupportingDocumentsListViewModel();
        }

        public static explicit operator GiftDisbursementsViewModel(GiftDisbursement giftDisbursement)
        {
            var signatureAuthorityEmployeeId = string.Empty;
            var designeeEmployeeId = string.Empty;

            if (giftDisbursement.GiftDisbursementApprovers.Any(a => a.Type == "Signature Authority"))
            {
                signatureAuthorityEmployeeId = giftDisbursement.GiftDisbursementApprovers.First(a => a.Type == "Signature Authority").EmployeeId;
            }

            if (giftDisbursement.GiftDisbursementApprovers.Any(a => a.Type == "Designee"))
            {
                designeeEmployeeId = giftDisbursement.GiftDisbursementApprovers.First(a => a.Type == "Designee").EmployeeId;
            }

            var approvers = giftDisbursement.GiftDisbursementApprovers.Select(a => (GiftDisbursementApprover)a)
                .GroupBy(a => new { a.Name, a.Type }).Select(g => g.First()).OrderByDescending(a => a.ApprovedOn);

            var approvalHistory = giftDisbursement.GiftDisbursementApprovalHistory != null ? giftDisbursement.GiftDisbursementApprovalHistory.Select(a => (GiftDisbursementApprover)a)
                .Where(a => a.Approved != null)
                .GroupBy(a => new { a.Name, a.Type, a.ApprovedOn }).Select(g => g.First()).OrderByDescending(a => a.ApprovedOn) : Enumerable.Empty<GiftDisbursementApprover>();

            return new GiftDisbursementsViewModel()
            {
                Organization = "UAF",
                PreparedByEmployeeId = giftDisbursement.PreparedByEmployeeId,
                PreparedByEmail = giftDisbursement.PreparedByEmail,
                PreparedByFirstName = giftDisbursement.PreparedByFirstName,
                PreparedByLastName = giftDisbursement.PreparedByLastName,
                PreparedByJobTitle = giftDisbursement.PreparedByJobTitle,

                GiftDisbursementId = giftDisbursement.Id,
                Type = giftDisbursement.Type,
                FormNumber = giftDisbursement.FormNumber,
                InvoiceDescription = giftDisbursement.InvoiceDescription,
                ReviewerNotes = giftDisbursement.ReviewerNotes,
                RentsAmount = giftDisbursement.RentsAmount,
                OtherIncomeAmount = giftDisbursement.OtherIncomeAmount,
                NonemployeeCompensationAmount = giftDisbursement.NonemployeeCompensationAmount,
                GrossProceedsPaidToAttorneyAmount = giftDisbursement.GrossProceedsPaidToAttorneyAmount,
                PostDate = DateTime.Now.ToString("MM/dd/yyyy"),
                CreatedOn = giftDisbursement.DateCreated.ToString("MM/dd/yyyy"),
                DisbursementFrom = new GiftDisbursementFrom()
                {
                    CollegeCode = giftDisbursement.DivisionCode,
                    CollegeName = giftDisbursement.DivisionName,
                    DepartmentCode = giftDisbursement.DepartmentCode,
                    DepartmentName = giftDisbursement.DepartmentName,
                    ProjectId = giftDisbursement.GiftDisbursementItems.FirstOrDefault()?.ProjectId,
                    ProjectName = giftDisbursement.GiftDisbursementItems.FirstOrDefault()?.ProjectName,
                    //ProjectBalance = giftDisbursement.GiftDisbursementItems.FirstOrDefault()?.ProjectBalance,
                    Purpose = giftDisbursement.Purpose
                },
                DisbursementTo = new GiftDisbursementTo()
                {
                    UaAccount = giftDisbursement.GiftDisbursementItems.FirstOrDefault()?.UaAccount,
                    Payee = (Payee)giftDisbursement,
                },
                GiftDisbursementStudents = giftDisbursement.GiftDisbursementStudents.Select(s => (GiftDisbursementStudent)s).ToList(),
                SignatureAuthorityEmployeeId = signatureAuthorityEmployeeId,
                DesigneeEmployeeId = designeeEmployeeId,
                IsPending = giftDisbursement.Pending,
                PendingComments = giftDisbursement.PendingComments,
                Approvers = approvers, //giftDisbursement.GiftDisbursementApprovers.Select(a => (GiftDisbursementApprover)a),
                ApprovalHistory = approvalHistory,
                CampusApproveOn = giftDisbursement.GiftDisbursementApprovers
                                    .Where(a => a.Type == "Designee" || a.Type == "Signature Authority")
                                    .OrderByDescending(o => o.ApprovedOn)
                                    .First()
                                    .ApprovedOn
                                    .ToString("MM/dd/yyyy"),
                //GiftDisbursementItems = giftDisbursement.GiftDisbursementItems.Select(i => (GiftDisbursementItem)i).ToList()
                SubmittedforApprovalOn = giftDisbursement.SubmittedForApprovalOn,
                CCEmails = giftDisbursement.CCEmails
            };
        }

        public static explicit operator UpdateGiftDisbursement(GiftDisbursementsViewModel model)
        {
            var items = new List<GiftDisbursementItem>();

            foreach(var project in model.GiftDisbursementProjects)
            {
                foreach (var item in project.ProjectItems)
                {
                    items.Add(new GiftDisbursementItem()
                    {
                        DisbursementId = model.GiftDisbursementId,
                        Id = item.DisbursementItemId,
                        ProjectId = project.ProjectId,
                        ProjectName = project.ProjectName,
                        UaAccount = item.UaAccount,
                        ObjectCode = item.ObjectCode,
                        DebitAccount = item.DebitAccountNumber,
                        IsFundraisingExpense = item.FundraisingExpense,
                        SubAccountCode = item.SubAccountCode ?? String.Empty,
                        ProjectCode = item.ProjectCode ?? String.Empty,
                        Description = item.Description ?? String.Empty,
                        Amount = item.Amount
                    });
                }                
            }

            return new UpdateGiftDisbursement()
            {
                Id = model.GiftDisbursementId,
                Type = model.Type,
                PreparedByEmployeeId = model.PreparedByEmployeeId,
                //PostDate = model.PostDate,
                DepartmentCode = model.DisbursementFrom.DepartmentCode,
                DivisionCode = model.DisbursementFrom.CollegeCode,
                Purpose = model.DisbursementFrom.Purpose,
                PayeeIs = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.PayeeIs : string.Empty,
                PayeeName = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.PayeeName : string.Empty,
                PayeeAddress1 = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.PayeeAddress1 : string.Empty,
                PayeeAddress2 = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.PayeeAddress2 : string.Empty,
                PayeeCity = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.PayeeCity : string.Empty,
                PayeeState = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.PayeeState : string.Empty,
                PayeeZip = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.PayeeZip : string.Empty,
                PayeePaymentType = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.PayeePaymentType : string.Empty,
                PayeePaymentSpecialInstructions = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.PayeeSpecialInstructions : string.Empty,
                VendorId = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.VendorId : null,
                VendorAddressId = model.Type == Constants.EMForm ? model.DisbursementTo.Payee.VendorAddressId : null,
                InvoiceDescription = model.InvoiceDescription,
                ReviewerNotes = model.ReviewerNotes,
                RentsAmount = model.RentsAmount ?? 0, //(model.Type == Constants.EMForm ? model.RentsAmount : 0),
                OtherIncomeAmount = model.OtherIncomeAmount ?? 0, //(model.Type == Constants.EMForm ? model.OtherIncomeAmount : 0),
                NonemployeeCompensationAmount = model.NonemployeeCompensationAmount ?? 0, //(model.Type == Constants.EMForm ? model.NonemployeeCompensationAmount : 0),
                GrossProceedsPaidToAttorneyAmount = model.GrossProceedsPaidToAttorneyAmount ?? 0, //(model.Type == Constants.EMForm ? model.GrossProceedsPaidToAttorneyAmount : 0),
                SignatureAuthorityEmployeeId = model.SignatureAuthorityEmployeeId,
                DesigneeEmployeeId = model.DesigneeEmployeeId,
                GiftDisbursementItems = items,
                SubmittedForApprovalOn = model.SubmittedforApprovalOn,
                Pending = model.IsPending,
                PendingComments = model.PendingComments,
                CCEmails = model.CCEmails,

            };
        }
    }

    public class GiftDisbursementFrom
    {
        public string CollegeCode { get; set; }
        public string CollegeName { get; set; }

        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }

        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        //public decimal ProjectBalance { get; set; }

        public string Purpose { get; set; }

        //public IEnumerable<SelectListItem> Colleges { get; set; }

        //public IEnumerable<SelectListItem> Departments { get; set; }

    }
    public class Payee
    {
        public string PayeeIs { get; set; }
        public string PayeeName { get; set; }
        public string PayeeAddress1 { get; set; }
        public string PayeeAddress2 { get; set; }
        public string PayeeCity { get; set; }
        public string PayeeState { get; set; }
        public string PayeeZip { get; set; }
        public string PayeePaymentType { get; set; } //Check or EFT
        public string PayeeSpecialInstructions { get; set; }
        public string PayeeType { get; set; }
        public string PayeeW9Year { get; set; }
        public string PayeeICAYear { get; set; }
        public bool PayeeICAYearExpired { get; set; }
        public bool? UAEmployee { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public long? VendorId { get; set; }
        public long? VendorAddressId { get; set; }

        public static explicit operator Payee(GiftDisbursement giftDisbursement)
        {
            return new Payee()
            {
                PayeeIs = giftDisbursement.PayeeIs,
                PayeeName = giftDisbursement.PayeeName,
                PayeeAddress1 = giftDisbursement.PayeeAddress1,
                PayeeAddress2 = giftDisbursement.PayeeAddress2,
                PayeeCity = giftDisbursement.PayeeCity,
                PayeeState = giftDisbursement.PayeeState,
                PayeeZip = giftDisbursement.PayeeZip,
                PayeeType = giftDisbursement.PayeeType,
                PayeePaymentType = giftDisbursement.PayeePaymentType,
                PayeeSpecialInstructions = giftDisbursement.PayeePaymentSpecialInstructions,
                VendorId = giftDisbursement.VendorId,
                VendorAddressId = giftDisbursement.VendorAddressId,
                PayeeW9Year = giftDisbursement.PayeeW9Year.HasValue ? giftDisbursement.PayeeW9Year.Value.ToString("MM/dd/yyyy") : "No W-9 on File",
                PayeeICAYear = giftDisbursement.PayeeICAYear.HasValue ? giftDisbursement.PayeeICAYear.Value.ToString("MM/dd/yyyy") : String.Empty,
                PayeeICAYearExpired = giftDisbursement.PayeeICAYear.HasValue ? !(giftDisbursement.PayeeICAYear.Value >= DateTime.Now.AddYears(5)) : true,
                //UAEmployee = giftDisbursement.UAEmployee
            };
        }
    }
    public class GiftDisbursementTo
    {
        public string UaAccount { get; set; }
        public Payee Payee { get; set; }
    }
    //public class GiftDisbursementItem
    //{
    //    public bool Deleted { get; set; } = false;
    //    public long? DisbursementItemId { get; set; }
    //    public string ProjectId { get; set; }
    //    public string ProjectName { get; set; }
    //    public decimal ProjectBalance { get; set; }
    //    public string UaAccount { get; set; }
    //    public string ObjectCode { get; set; }
    //    public string DebitAccountNumber { get; set; }
    //    public bool FundraisingExpense { get; set; }
    //    public string SubAccountCode { get; set; }

    //    public string ProjectCode { get; set; }

    //    public string Description { get; set; }

    //    public decimal Amount { get; set; }

    //    public static explicit operator Sentry.Domain.AccountsPayable.Entities.GiftDisbursementItem(GiftDisbursementItem item)
    //    {
    //        return new Sentry.Domain.AccountsPayable.Entities.GiftDisbursementItem()
    //        {
    //            Id = item.DisbursementItemId,
    //            ProjectId = item.ProjectId,
    //            ProjectName = item.ProjectName,
    //            ProjectBalance = item.ProjectBalance,
    //            UaAccount = item.UaAccount,
    //            ObjectCode = item.ObjectCode,
    //            DebitAccount = item.DebitAccountNumber,
    //            IsFundraisingExpense = item.FundraisingExpense,
    //            SubAccountCode = item.SubAccountCode ?? String.Empty,
    //            ProjectCode = item.ProjectCode ?? String.Empty,
    //            Description = item.Description,
    //            Amount = item.Amount                
    //        };
    //    }

    //    public static explicit operator GiftDisbursementItem(Sentry.Domain.AccountsPayable.Entities.GiftDisbursementItem item)
    //    {
    //        return new GiftDisbursementItem()
    //        {
    //            DisbursementItemId = item.Id,
    //            ProjectId = item.ProjectId,
    //            ProjectName = item.ProjectName,
    //            ProjectBalance = item.ProjectBalance,
    //            UaAccount = item.UaAccount,
    //            ObjectCode = item.ObjectCode,
    //            DebitAccountNumber = item.DebitAccount,
    //            FundraisingExpense = item.IsFundraisingExpense,
    //            SubAccountCode = item.SubAccountCode,
    //            ProjectCode = item.ProjectCode,
    //            Description = item.Description,
    //            Amount = item.Amount,
    //        };
    //    }
    //}

    public class GiftDisbursementApprover
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public DateTime ApprovedOn { get; set; }
        public string Comments { get; set; }
        public bool? Approved { get; set; }
        public DateTime CreatedOn { get; set; }
        public string EmployeeId { get; set; }

        public static explicit operator GiftDisbursementApprover(Sentry.Domain.AccountsPayable.Entities.GiftDisbursementApprover entity)
        {
            return new GiftDisbursementApprover()
            {
                Type = entity.Type,
                Name = $"{entity.FirstName} {entity.LastName}",
                ApprovedOn = entity.ApprovedOn, //TODO move formatting to view == DateTime.MinValue ? String.Empty : entity.ApprovedOn.ToString("MM/dd/yyyy"),
                Comments = entity.Comments,
                Approved = entity.Approved,
                CreatedOn = entity.CreatedOn,
                EmployeeId = entity.EmployeeId
            };
        }
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

        public static explicit operator Sentry.Domain.AccountsPayable.Entities.GiftDisbursementStudent(GiftDisbursementStudent model)
        {
            return new Sentry.Domain.AccountsPayable.Entities.GiftDisbursementStudent()
            {
                Id = model.Id,
                DisbursementId = model.DisbursementId,
                StudentId = model.StudentId,
                StudentName = model.StudentName,
                Term = model.Term,
                ScholarshipId = model.ScholarshipId,
                ScholarshipName = model.ScholarshipName,
                Amount = model.Amount,
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static explicit operator GiftDisbursementStudent(Sentry.Domain.AccountsPayable.Entities.GiftDisbursementStudent entity)
        {
            return new GiftDisbursementStudent()
            {
                Id = entity.Id,
                DisbursementId = entity.DisbursementId,
                StudentId = entity.StudentId,
                StudentName = entity.StudentName,
                Term = entity.Term,
                TermName = entity.TermName,
                ScholarshipId = entity.ScholarshipId,
                ScholarshipName = entity.ScholarshipName,
                Amount = entity.Amount,
                CreatedOn = entity.CreatedOn,
                UpdatedOn = entity.UpdatedOn
            };
        }
    }


    public class BulkProcess
    {
        //[JsonProperty("DisbursementIds")]
        public List<string> DisbursementIds { get; set; }
        //[JsonProperty("PostDate")]
        public DateTime PostDate { get; set; }
        //public string InvoiceDescription { get; set; }
    }

    public class ProjectItem
    {
        public bool Deleted { get; set; } = false;
        public long? DisbursementItemId { get; set; }
        public string UaAccount { get; set; }
        public string ObjectCode { get; set; }

        [MaxLength(8, ErrorMessage = "Debit account must be 8 characters in the format 30-00000 or 20-00000")]
        [MinLength(8, ErrorMessage = "Debit account must be 8 characters in the format 30-00000 or 20-00000")]
        [Required]
        [CustomDebitAccountAllDigitsValidation(ErrorMessage = "Debit account must be 8 characters in the format 30-00000 or 20-00000")]
        public string DebitAccountNumber { get; set; }

        public bool FundraisingExpense { get; set; }
        public string SubAccountCode { get; set; }
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string DebitAccountDescription { get; set; }

        public static explicit operator ProjectItem(Sentry.Domain.AccountsPayable.Entities.GiftDisbursementItem item)
        {
            return new ProjectItem()
            {
                DisbursementItemId = item.Id.Value,
                UaAccount = item.UaAccount,
                ObjectCode = item.ObjectCode,
                DebitAccountNumber = item.DebitAccount,
                FundraisingExpense = item.IsFundraisingExpense,
                SubAccountCode = item.SubAccountCode,
                ProjectCode = item.ProjectCode,
                Description = item.Description,
                Amount = item.Amount,
                DebitAccountDescription = item.DebitAccountDescription
            };
        }
    }

    public class GiftDisbursementProject
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal ProjectBalance { get; set; }
        public string BlackbaudProjectUrl { get; set; }
        public IList<ProjectItem> ProjectItems { get; set; }
    }

}
