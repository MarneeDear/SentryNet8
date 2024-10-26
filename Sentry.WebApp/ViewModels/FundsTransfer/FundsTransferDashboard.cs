using System.Collections;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels.FundsTransfer
{
    public class FundsTransferDashboard : BaseViewModel
    {
        public IEnumerable<FundsTransferListItem> FundsTransfers { get; set; }
    }
}
