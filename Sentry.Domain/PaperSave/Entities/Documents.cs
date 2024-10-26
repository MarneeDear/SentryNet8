using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Sentry.Domain.PaperSave.Entities
{
    public class NewDocument
    {
        //public int Id { get; set; }
        public string FileName { get; set; }
        public string FormNumber { get; set; }
        public string Contents { get; set; }
        public string SupportingDocumentType { get; set; }
    }

    public class FinalDocument
    {
        public string SystemId { get; set; }
        public IDictionary<string, string> DocumentAttributes { get; set; } = new Dictionary<string, string>();
    }
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FormNumber { get; set; }
        public byte[] Contents { get; set; }
        public string MimeType { get; set; }
    }

    public class DocumentListItem
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string SupportingDocumentType { get; set; }
    }

    public class DocumentListResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public IEnumerable<DocumentListItem> Documents { get; set; }

    }

    public class DocumentResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Document Document { get; set; }

    }

    public class UploadDocumentResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }


    }
}
