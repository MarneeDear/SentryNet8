using System;
using System.Collections.Generic;
using System.Linq;

namespace Sentry.WebApp.ViewModels.FundsTransfer
{
    public class FundsTransferListItem
    {
        public long Id { get; set; }
        public string FormNumber { get; set; }
        public string Total { get; set; }
        public string PreparedByName { get; set; }
        public decimal TransferAmount { get; set; }
        public string TransferFrom { get; set; }
        public string TransferTo { get; set; }
        public string Status { get; set; }
        public string ProcessingError { get; set; }
        public string LastApprovedOnDate { get; set; }
        public string RoutingType { get; set; }
        public bool RequiresGeneralCounsel {  get; set; }
        public bool IsPending { get; set; }
        public string PendingComments { get; set; }
        public string UAFStage { get; set; }

        public static explicit operator FundsTransferListItem(Sentry.Domain.AccountsPayable.Entities.FundsTransferSummary entity)
        {
            return new FundsTransferListItem
            {
                Id = entity.Id,
                FormNumber = entity.FormNumber,
                Total = entity.TransferAmount.ToString("C"),
                PreparedByName = $"{entity.PreparedByFirstName} {entity.PreparedByLastName}",
                TransferFrom = entity.FromProjectId,
                TransferTo = entity.ToProjectIds,
                Status = entity.Status,
                ProcessingError = entity.ProcessingError,
                RoutingType = entity.RoutingTypeDescription,
                RequiresGeneralCounsel = entity.RequiresGeneralCounsel,
                IsPending = entity.IsPending,
                PendingComments = entity.PendingComments,
                UAFStage = entity.UAFStage,
                LastApprovedOnDate = (entity.LastApprovedOnDate.HasValue ? entity.LastApprovedOnDate.Value.ToString("MM/dd/yyyy h:mm tt") : null)
            };
        }
    }


    public class FundsTransferListViewModel : BaseIntegrationViewModel
    {
        public FundsTransferListViewModel() : base()
        {

        }

        public string Organization { get; set; }
        public IEnumerable<FundsTransferListItem> FundsTransfers { get; set; }

    }
}
