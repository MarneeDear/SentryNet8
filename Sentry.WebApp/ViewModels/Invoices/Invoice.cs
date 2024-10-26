using Sentry.Domain.AccountsPayable.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels.Invoices
{
    public class InvoiceCreate
    {
        public long DisbursementId { get; set; }
        public DateTime PostDate { get; set; }
    }

    public class InvoiceFormCreate
    {
        public long DisbursementId { get; set; }
        public long InvoiceId { get; set; }
    }

    public class Transaction
    {

    }

    public class GiftDisbursementFrom
    {
        public string CollegeCode { get; set; }
        public string CollegeName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Purpose { get; set; }

    }

    public class GiftDisbursementTo
    {
        public string UaAccount { get; set; }
        public Payee Payee { get; set; }
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
        public bool UAEmployee { get; set; }

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
                PayeePaymentType = giftDisbursement.PayeePaymentType,
                PayeeSpecialInstructions = giftDisbursement.PayeePaymentSpecialInstructions,
                UAEmployee = giftDisbursement.UAEmployee
            };
        }


    }

    public class GiftDisbursementApprover
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public DateTime ApprovedOn { get; set; }
        public string Comments { get; set; }
        public bool? Approved { get; set; }

        public static explicit operator GiftDisbursementApprover(Sentry.Domain.AccountsPayable.Entities.GiftDisbursementApprover entity)
        {
            return new GiftDisbursementApprover()
            {
                Type = entity.Type,
                Name = $"{entity.FirstName} {entity.LastName}",
                ApprovedOn = entity.ApprovedOn, //== DateTime.MinValue ? String.Empty : entity.ApprovedOn.ToString("MM/dd/yyyy"),
                Comments = entity.Comments,
                Approved = entity.Approved
            };
        }

    }

    public class GiftDisbursementProject
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal ProjectBalance { get; set; }
        public IEnumerable<ProjectItem> ProjectItems { get; set; }
    }

    public class ProjectItem
    {
        public long DisbursementItemId { get; set; }
        public string UaAccount { get; set; }
        public string ObjectCode { get; set; }
        public string DebitAccountNumber { get; set; }
        public bool FundraisingExpense { get; set; }
        public string SubAccountCode { get; set; }
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

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
                Amount = item.Amount
            };
        }


    }


    public class PDFViewModelSimplified
    {
        public string PreparedByFirstName { get; set; }
        public string PreparedByLastName { get; set; }
        public string PreparedByEmail { get; set; }
        public string PreparedByJobTitle { get; set; }
        public int ProjectsTotalCount { get; set; }
        public int TransactionsTotalCount { get; set; }
        public string CreatedOn { get; set; }
        public string InvoiceDescription { get; set; }
        public string FormNumber { get; set; }
        public string Type { get; set; }
        public decimal OverallTotal { get; set; }
        public string PostDate { get; set; }
        public decimal? RentsAmount { get; set; }
        public decimal? OtherIncomeAmount { get; set; }
        public decimal? NonemployeeCompensationAmount { get; set; }
        public decimal? GrossProceedsPaidToAttorneyAmount { get; set; }

        public GiftDisbursementFrom DisbursementFrom { get; set; }
        public GiftDisbursementTo DisbursementTo { get; set; }
        public IEnumerable<GiftDisbursementApprover> Approvers { get; set; }
        public IEnumerable<GiftDisbursementApprover> ApprovalHistory { get; set; }

        public IList<GiftDisbursementProject> GiftDisbursementProjects { get; set; }

        public IEnumerable<string> SupportingDocuments { get; set; }

        public static explicit operator PDFViewModelSimplified(GiftDisbursement giftDisbursement)
        {
            var approvers = giftDisbursement.GiftDisbursementApprovers.Select(a => (GiftDisbursementApprover)a)
                .GroupBy(a => new { a.Name, a.Type }).Select(g => g.First());
            var approvalHistory = giftDisbursement.GiftDisbursementApprovalHistory != null ? giftDisbursement.GiftDisbursementApprovalHistory.Select(a => (GiftDisbursementApprover)a)
                .Where(a => a.Approved != null)
                .GroupBy(a => new { a.Name, a.Type, a.ApprovedOn }).Select(g => g.First()).OrderByDescending(a => a.ApprovedOn) : Enumerable.Empty<GiftDisbursementApprover>();


            //SETUP PROJECTS
            //model.GiftDisbursementProjects = new List<GiftDisbursementProject>();
            var gdProjects = new List<GiftDisbursementProject>();

            var items = giftDisbursement.GiftDisbursementItems;

            var projects = items.Select(i => i.ProjectId).Distinct();
            //model.GiftDisbursementItems.Select(i => i.ProjectId).Distinct();
            foreach (var project in projects)
            {
                var projectItems = items.Where(i => i.ProjectId == project);
                var reviewProject = new GiftDisbursementProject()
                {
                    ProjectId = projectItems.First().ProjectId,
                    ProjectName = projectItems.First().ProjectName,
                    ProjectBalance = projectItems.First().ProjectBalance,
                    ProjectItems = projectItems.Select(i => (ProjectItem)i).ToList(),
                    //BlackbaudProjectUrl = $"{_config.Blackbaud.Project.BaseUrl}/{projectItems.First().ProjectFeId}?envid={_config.Blackbaud.Project.EnvironmentId}"
                };
                gdProjects.Add(reviewProject);
            }

            return new PDFViewModelSimplified()
            {
                PreparedByEmail = giftDisbursement.PreparedByEmail,
                PreparedByFirstName = giftDisbursement.PreparedByFirstName,
                PreparedByLastName = giftDisbursement.PreparedByLastName,
                PreparedByJobTitle = giftDisbursement.PreparedByJobTitle,
                Type = giftDisbursement.Type,
                FormNumber = giftDisbursement.FormNumber,
                InvoiceDescription = giftDisbursement.InvoiceDescription,
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
                Approvers = approvers,
                ApprovalHistory = approvalHistory,
                GiftDisbursementProjects = gdProjects

            };
        }
    }
}
