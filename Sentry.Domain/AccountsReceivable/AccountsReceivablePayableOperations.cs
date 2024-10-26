using Newtonsoft.Json;
using RestSharp;
using Sentry.Domain.AccountsReceivable.Entities;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Sentry.Domain.AccountsReceivable
{
    public class AccountsReceivableOperations : Sentry.Domain.WebServiceOperations
    {
        public AccountsReceivableOperations(string urlBase, string apiKey) : base(urlBase, apiKey)
        {
            base._urlBase = urlBase;
            base._apiKey = apiKey;
        }

        public async Task QueueUAFTransaction(string formNumber, UAFTransaction transaction)
        {
            try
            {
                string uri = $"ar/uaf/forms/{formNumber}/queue";
                var requestBody = JsonConvert.SerializeObject(transaction);
                var response = await ExecuteRequest(uri, Method.Post, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error queueing transaction for form [{formNumber}] [{response.StatusCode}] [{response.Content}] [{requestBody}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task QueueUATransaction(string formNumber, UATransaction transaction)
        {
            try
            {
                string uri = $"ar/ua/forms/{formNumber}/queue";
                var requestBody = JsonConvert.SerializeObject(transaction);
                var response = await ExecuteRequest(uri, Method.Post, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error queueing transaction for form [{formNumber}] [{response.StatusCode}] [{response.Content}] [{requestBody}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task CreateUAFTransaction(string formNumber, UAFTransaction transaction)
        {
            try
            {
                string uri = $"ar/uaf/forms/{formNumber}/workflows";
                var requestBody = JsonConvert.SerializeObject(transaction);
                var response = await ExecuteRequest(uri, Method.Post, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error creating workflow transaction for form [{formNumber}] [{response.StatusCode}] [{response.Content}] [{requestBody}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task CreateUAFTransactionFundsTransfer(string formNumber, UAFTransaction transaction)
        {
            try
            {
                string uri = $"ar/fundstransfers/{formNumber}/workflow";
                var requestBody = JsonConvert.SerializeObject(transaction);
                var response = await ExecuteRequest(uri, Method.Post, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error creating workflow transaction for form id [{formNumber}] [{response.StatusCode}] [{response.Content}] [{requestBody}]");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
