using System;
using System.Collections.Generic;
using System.Text;

namespace Sentry.Domain.AccountsReceivable.Entities
{
    public class UAFTransaction
    {
        public string ConstituentName { get; set; }
        public decimal GiftTotal { get; set; }
        public decimal ReceiptTotal { get; set; }
        public IEnumerable<string> Projects { get; set; }
        public string TransactionType { get; set; }
        public DateTime PostDate { get; set; }
        public Guid GiftTransmittalId { get; set; }
        public long? FundsTransferId { get; set; }
    }

    public class UATransaction
    {
        public DateTime PostDate { get; set; }
        public string TransactionType { get; set; }
        public bool HasProcessingError { get;set; }
        public Guid GiftTransmittalId { get; set; }


    }
}
