using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using Sentry.Domain.Forms.Entities;

namespace Sentry.Domain.Forms
{
    public class FormsOperations : Sentry.Domain.WebServiceOperations
    {
        public FormsOperations(string urlBase, string apiKey) : base(urlBase, apiKey)
        {
            base._urlBase = urlBase;
            base._apiKey = apiKey;
        }

        public async Task<string> GetFormNumber(string formPrefix)
        {
            FormResponse result = null;
            try
            {
                string url = $"forms/{formPrefix}";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<FormResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    throw new Exception($"Returned not found");
                else
                {
                    throw new Exception($"Error getting gift disbursement [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.Form.FormNumber;
        }
    }
}
