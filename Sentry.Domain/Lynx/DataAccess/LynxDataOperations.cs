using Sentry.Domain.DataAccess.Entities;
using Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal;
using System;
using System.Collections.Generic;

namespace Sentry.Domain.Lynx.DataAccess
{
    public class LynxDataOperations : DataAccessBase
    {
        public LynxDataOperations(string connectionString)
        {
            base.ConnectionString = connectionString;
        }

        public IEnumerable<Entities.GiftTransmittal.GiftTransmittal> GetGiftTransmittals(bool receivedPhysicalDocuments = true)
        {
            var results = base.GetProcedureResults<Entities.GiftTransmittal.GiftTransmittal>(
                "[UAF].[GetGiftTransmittals]",
                new Dictionary<string, object>()
                {
                    { "@pEmployeeId", "ALL" },
                    { "@pAdminView", 1 },
                    { "@pReceivedPhysicalDocuments", receivedPhysicalDocuments }
                }
            );

            return results;
        }

        public IEnumerable<IntegrationCountsResult> GetGiftTransmittalCounts()
        {
            var result = base.GetProcedureResults<IntegrationCountsResult>(
                "[UAF].[GetGiftTransmittalCounts_all]",
                new Dictionary<string, object>()
                {                    
                }
            );

            return result;
        }

    }
}
