using Sentry.Domain.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal
{
    public class IntegrationCountsResult : EntityBase
    {
        internal IntegrationCountsResult(IDataRecord record)
        {
            CountName = GetValueOrDefault<string>(record, 0);
            Count = GetValueOrDefault<int>(record, 1);

        }

        public string CountName { get; set; }
        public int Count { get; set; }
    }
}
