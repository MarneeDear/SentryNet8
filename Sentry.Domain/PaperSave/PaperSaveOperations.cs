using Newtonsoft.Json;
using Sentry.Domain.PaperSave.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sentry.Domain.PaperSave
{
    public class PaperSaveOperations : WebServiceOperations
    {
        public PaperSaveOperations(string urlBase, string apiKey) : base(urlBase, apiKey)
        {
            base._urlBase = urlBase;
            base._apiKey = apiKey;
        }

        public async Task<IEnumerable<DocumentListItem>> AdvancedSearchDocumentsByFormNumber(string formNumer)
        {
            DocumentListResponse result = null;
            try
            {
                string url = $"ps/documents/advancedsearch/{formNumer}";
                var response = await ExecuteRequestWithAuthToken(url, RestSharp.Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<DocumentListResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new Exception($"Error getting list of documents [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
            return result.Documents;
        }

        /// <summary>
        /// OBOSOLTE use AdvancedSearchDocumentsByFormNumber
        /// </summary>
        /// <param name="formNumer"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DocumentListItem>> SearchDocumentsByFormNumber_obsolete(string formNumer)
        {
            DocumentListResponse result = null;
            try
            {
                string url = $"ps/documents/search/{formNumer}";
                var response = await ExecuteRequestWithAuthToken(url, RestSharp.Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<DocumentListResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new Exception($"Error getting list of documents [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
            return result.Documents;
        }

        public async Task<Document> GetDocumentById(int id)
        {
            DocumentResponse result = null;
            try
            {
                string url = $"ps/documents/{id}";
                var response = await ExecuteRequestWithAuthToken(url, RestSharp.Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<DocumentResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new Exception($"Error getting list of documents [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
            return result.Document;
        }

        /// <summary>
        /// Search by ReportType and filter by DocumentType
        /// </summary>
        /// <param name="reportType"></param>
        /// <returns></returns>
        public async Task<int> GetDocumentIdByReportType(int reportType)
        {
            var result = new DocumentResponse();
            try
            {
                string url = $"ps/reportguides/search/{reportType}";
                var response = await ExecuteRequestWithAuthToken(url, RestSharp.Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<DocumentResponse>(response.Content);
                    return result?.Document.Id ?? 0;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return 0;
                }
                else
                {
                    throw new Exception($"Error getting list of documents [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task UploadDocument(NewDocument document)
        {
            //UploadDocumentResponse result = null;
            try
            {
                string url = $"ps/documents/temp";
                var response = await ExecuteRequest(url, RestSharp.Method.Post, JsonConvert.SerializeObject(document));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //result = JsonConvert.DeserializeObject<UploadDocumentResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception($"Not found");
                }
                else
                {
                    throw new Exception(
                        $"Error creating temp document [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task CreateFinalDocuments(FinalDocument document, string formNumber)
        {
            //UploadDocumentResponse result = null;
            try
            {
                string url = $"ps/documents/forms/{formNumber}";
                var response = await ExecuteRequest(url, RestSharp.Method.Post, JsonConvert.SerializeObject(document));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //result = JsonConvert.DeserializeObject<UploadDocumentResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception($"Not found");
                }
                else
                {
                    throw new Exception(
                        $"Error creating final documenta [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task CreateFinalDocument(FinalDocument document, string formNumber, string documentName)
        {
            //UploadDocumentResponse result = null;
            try
            {
                string url = $"ps/documents/forms/{formNumber}?document={documentName}";
                var response = await ExecuteRequest(url, RestSharp.Method.Post, JsonConvert.SerializeObject(document));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //result = JsonConvert.DeserializeObject<UploadDocumentResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception($"Not found");
                }
                else
                {
                    throw new Exception(
                        $"Error creating final document [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteDocumentById(long id)
        {
            try
            {
                string url = $"ps/documents/{id}";
                var response = await ExecuteRequest(url, RestSharp.Method.Delete);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception($"Not found");
                    }
                    else
                    {
                        throw new Exception(
                            $"Error deleting document id {id} [{response.StatusCode}] [{response.Content}]");
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
