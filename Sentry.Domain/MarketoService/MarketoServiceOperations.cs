using Newtonsoft.Json;
using RestSharp;
using Sentry.Domain.AccountsPayable.Entities;
using Sentry.Domain.MasterDataWebService.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Sentry.Domain.Marketo
{
    public class MarketoServiceOperations : Sentry.Domain.WebServiceOperations
    {
        public MarketoServiceOperations(string urlBase, string apiKey) : base(urlBase, apiKey)
        {
            base._urlBase = urlBase;
            base._apiKey = apiKey;
        }

        public async Task Merge(long winningLeadId, long losingLeadId)
        {
            try
            {
                string uri = $"leads/{winningLeadId}?losingLeadId={losingLeadId}";
                var response = await ExecuteRequest(uri, Method.Patch);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error merging losing lead [{losingLeadId}] and winning lead [{winningLeadId}] [{response.StatusCode}] [{response.Content}]");

                }
            }
            catch
            {
                throw;
            }
        }
    }
}
