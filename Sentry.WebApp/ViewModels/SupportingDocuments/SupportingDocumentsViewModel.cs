using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels.SupportingDocuments
{
    public class SupportingDocument
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string FormNumber { get; set; }
        public string SupportingDocumentType { get; set; }
        public string SupportingDocumentTypeDisplay { get; set; }

        public static explicit operator SupportingDocument(Sentry.Domain.PaperSave.Entities.DocumentListItem document)
        {
            string supportingDocumentTypeDisplay = string.Empty; //TODO: Make this work dynamically
            switch (document.SupportingDocumentType)
            {
                case "EFT Authorization":
                    supportingDocumentTypeDisplay = "Voided Check or Bank Letter";
                    break;
                case "W9":
                    supportingDocumentTypeDisplay = "W9";
                    break;
                case "Independent Contractor Agreement":
                    supportingDocumentTypeDisplay = "Independent Contractor Agreement";
                    break;
                default:
                    break;
            };
            return new SupportingDocument()
            {
                Id = document.Id,
                FileName = document.FileName,
                SupportingDocumentType = document.SupportingDocumentType,
                SupportingDocumentTypeDisplay = supportingDocumentTypeDisplay
            };
        }

    }

    public class SupportingDocumentsListViewModel
    {
        public IEnumerable<SupportingDocument> SupportingDocuments { get; set; }
        public string Error { get; set; }
        public bool PreventDelete { get; set; }
        public SupportingDocumentsListViewModel()
        {
            this.SupportingDocuments = new List<SupportingDocument>();
        }
    }

    public class SupportingDocumentUpload
    {
        public IFormFile SupportingDocument { get; set; }
    }
}
