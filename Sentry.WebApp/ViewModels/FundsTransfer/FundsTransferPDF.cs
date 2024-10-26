using Sentry.Domain.AccountsPayable.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Sentry.WebApp.ViewModels.FundsTransfer
{
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
        public string PostDate { get; set; }
        public string TransferPurpose { get; set; }
        public string AccountNumber { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public decimal ProjectBalance { get; set; }
        public string LineItemDescription { get; set; }
        public IList<FundsTransferItem> Items { get; set; }
        public IEnumerable<FundsTransferApprover> Approvers { get; set; }
        public IEnumerable<FundsTransferApprover> ApprovalHistory { get; set; }

        public IEnumerable<string> SupportingDocuments { get; set; }

        public static explicit operator PDFViewModelSimplified(Sentry.Domain.AccountsPayable.Entities.FundsTransfer fundsTransfer)
        {
            var approvers = fundsTransfer.FundsTransferApprovers.Select(a => (FundsTransferApprover)a)
                .GroupBy(a => new { a.FirstName, a.LastName, a.Type }).Select(g => g.First());
            var approvalHistory = fundsTransfer.FundsTransferApprovalHistory != null ? fundsTransfer.FundsTransferApprovalHistory.Select(a => (FundsTransferApprover)a)
                .Where(a => a.Approved != null)
                .GroupBy(a => new { a.FirstName, a.LastName, a.Type, a.ApprovedOn }).Select(g => g.First()).OrderByDescending(a => a.ApprovedOn) : Enumerable.Empty<FundsTransferApprover>();

            return new PDFViewModelSimplified()
            {
                PreparedByEmail = fundsTransfer.PreparedByEmail,
                PreparedByFirstName = fundsTransfer.PreparedByFirstName,
                PreparedByLastName = fundsTransfer.PreparedByLastName,
                PreparedByJobTitle = fundsTransfer.PreparedByJobTitle,
                FormNumber = fundsTransfer.FormNumber,
                PostDate = fundsTransfer.PostDate.HasValue ? fundsTransfer.PostDate.Value.ToString("MM/dd/yyyy") : DateTime.Now.ToString("MM/dd/yyyy"),
                CreatedOn = fundsTransfer.CreateDate.ToString("MM/dd/yyyy"),
                ProjectId = fundsTransfer.FromProjectId,
                AccountNumber = fundsTransfer.FromAccountNumber,
                TransferPurpose = fundsTransfer.TransferPurpose,
                LineItemDescription = fundsTransfer.FromLineItemDescription,
                Approvers = approvers,
                ApprovalHistory = approvalHistory,
                Items = fundsTransfer.FundsTransferItems.Select(i => (FundsTransferItem)i).ToList()

            };
        }
    }
}
