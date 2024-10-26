using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sentry.WebApp.ViewModels.GiftDisbursements
{
    public class GiftDisbursementsListItem
    {
        public string DateCreated { get; set; }
        public string FormNumber { get; set; }
        public string PreparedByName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectId { get; set; }
        public string PayeeName { get; set; }
        public string PayeePaymentType { get; set; }
        public string PayeeSpecialInstructions { get; set; }
        public string Total { get; set; }
        public long GiftDisbursementId { get; set; }
        //public string Error { get; set; }
        public string ProcessingError { get; set; }
        public bool Rejected { get; set; }
        public string Fund { get; set; }
        public bool IsPending { get; set; }
        public string LastApprovedOnDate { get; set; }
        public decimal? Total1099 { get; set; }
        public bool Has1099TotalValue { get; set; }
        public string PendingComments { get; set; }
        public string APReviewerEmployeeId { get; set; }
        public string ReviewerNotes { get; set; }
        public bool PreviouslyRejected { get; set; }
        public bool PreviouslyApprovedByUser { get; set; }
        public IEnumerable<GiftDisbursementApprover> GiftDisbursementApprovalHistory { get; set; }
        public string RowStatusClass { get; set; }
        public bool Resubmitted { get; set; }

        public static explicit operator GiftDisbursementsListItem(Sentry.Domain.AccountsPayable.Entities.GiftDisbursement giftDisbursement)
        {
            string sLastApproved = String.Empty;
            var approvalHistory = giftDisbursement.GiftDisbursementApprovalHistory;

            if (giftDisbursement.LastApprovedOnDate.HasValue)
            { 
                var lastApprovedOnDate = (DateTime)giftDisbursement.LastApprovedOnDate;
                sLastApproved = lastApprovedOnDate.ToString("MM/dd/yyyy");
            }

            return new GiftDisbursementsListItem()
            {
                DateCreated = giftDisbursement.DateCreated.ToString("MM/dd/yyyy"),
                FormNumber = giftDisbursement.FormNumber,
                DepartmentCode = giftDisbursement.DepartmentCode,
                DepartmentName = giftDisbursement.DepartmentName,
                PayeeName = giftDisbursement.PayeeName,
                PayeePaymentType = giftDisbursement.PayeePaymentType,
                Fund = giftDisbursement.Fund,
                PayeeSpecialInstructions = giftDisbursement.PayeePaymentSpecialInstructions,
                Total = giftDisbursement.Total.ToString("C"),
                PreparedByName = $"{giftDisbursement.PreparedByFirstName} {giftDisbursement.PreparedByLastName}",
                GiftDisbursementId = giftDisbursement.Id,
                //Error = giftDisbursement.Error,
                ProcessingError = giftDisbursement.ProcessingError,
                Rejected = giftDisbursement.RejectedByEscalatedApprovers,
                IsPending = giftDisbursement.Pending,
                LastApprovedOnDate = sLastApproved,
                Total1099 = giftDisbursement.Total1099,
                Has1099TotalValue = (giftDisbursement.Total1099 > decimal.Zero ? true : false),
                PendingComments = giftDisbursement.PendingComments,
                ReviewerNotes = giftDisbursement.ReviewerNotes,
                APReviewerEmployeeId = giftDisbursement.APReviewerEmployeeId,
                PreviouslyRejected = giftDisbursement.PreviouslyRejected,
                PreviouslyApprovedByUser = giftDisbursement.PreviouslyApprovedByUser,
                Resubmitted = giftDisbursement.Resubmitted,
                GiftDisbursementApprovalHistory = (approvalHistory != null && approvalHistory.Any() ? approvalHistory.Select(h => (GiftDisbursementApprover)h) : Enumerable.Empty<GiftDisbursementApprover>())
            };
        }
    }

    public class GiftDisbursementsListViewModel : BaseIntegrationViewModel
    {
        public GiftDisbursementsListViewModel() : base()
        {

        }

        public string Organization { get; set; }
        public IEnumerable<GiftDisbursementsListItem> Disbursements { get; set; }
        public string ListType { get; set; }

    }    
}
