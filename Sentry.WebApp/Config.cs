using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp
{
    public class MasterDataWebService
    {
        public string Url { get; set; }
    }

    public class LynxWebService
    {
        public string Url { get; set; }
    }

    public class MarketoService
    {
        public string Url { get; set; }
    }

    public class ConnectionStrings
    {
        public string CrmDbContext { get; set; }
    }

    public class LynxCrm
    {
        public string Url { get; set; }
        public string DatabaseName { get; set; }
        public string LynxBatchDescription { get; set; }
        public string LynxBatchTemplateId { get; set; }
        public string LynxBatchDataFormId { get; set; }
    }
    public class UAFServices
    {
        public string BaseUrl { get; set; }
        public string APIKey { get; set; }
    }

    public class UAFForms
    {
        public string UnlockKey { get; set; }
    }

    public class SendTo
    {
        public string Name { get; set; }
        public string Email { get; set; } 
    }   

    public class SendGrid
    {
        public string ApiKey { get; set; }
        public string DisbursementEscalatedApprovalTemplateId { get; set; }
        public string APReviewRejectionTemplateId { get; set; }
        public string NewVendorRequestRejectionTemplate { get; set; }
        public string TransmittalRejectionTemplateId { get; set; }
        public string TransmittalInitializedTemplateId { get; set; }
        public string FundsTransferRejected { get; set; }
        public string FundsTransferApproved { get; set; }
        public string GTSecondaryRejected { get; set; }
        public string GTSeconaryApproved { get; set; }
        public string VendorRejected { get; set; }
        public string VendorApproved { get; set; }
        public string ARStaffGroupEmail { get; set; }
        public string ARStaffGroupEmailName { get; set; }
        public IList<SendTo> DeveloperSendTos { get; set; }
        public IList<SendTo> AdminSendTos { get; set; }
        public bool Development { get; set; }
        public SendTo From { get; set; }          
    }

    public class ReportConfig
    {
        public string Report { get; set; }
        public string ReportName { get; set; }
        public string ReportDisplayName { get; set; }
        //public string ReportGuide { get; set; }
        public int ReportGuideId { get; set; }
        public string AccessLevel { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class BlackbaudResource
    {
        public string BaseUrl { get; set; }
        public string EnvironmentId {get;set;}
    }

    public class Blackbaud
    {
        public BlackbaudResource Project { get; set; }
    }

    public class VendorAttachmentType
    {        
        public string TempSupportingDocumentType { get; set; }
        public string Description { get; set; }
    }

    public class SupportingDocuments
    {
        public IEnumerable<VendorAttachmentType> VendorAttachmentTypes { get; set; }
    }

    public class PowerBiReportFilter
    {
        public string Table { get; set; }
        public string Column { get; set; }
    }

    public class PowerBiReport
    {
        public string ReportDisplayName { get; set; }
        public Guid ReportId { get; set; }
        public int ReportGuideId {  get; set; }
        public PowerBiReportFilter Filter { get; set; }
    }

    public class Config
    {
        public string Version { get; set; }
        public MasterDataWebService MasterDataWebService { get; set; }
        public MarketoService MarketoService { get; set; }
        public LynxWebService LynxWebService { get; set; }
        public LynxCrm LynxCrm { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public UAFServices UAFServices { get; set; }
        public SendGrid SendGrid { get; set; }
        public IEnumerable<ReportConfig> EnhancedReporting { get; set; }
        public Blackbaud Blackbaud { get; set; }
        public string UAFDNDisbursementURL { get; set; }
        public string UAFDNTransmittalURL { get; set; }
        public string UAFDNNewVendorRequestURL { get; set; }
        public string UAFDNFundsTransferURL { get; set; }
        public UAFForms UAFForms { get; set; }
        public IEnumerable<string> HealthCategories { get; set; }
        public IList<string> ValidFileTypes { get;set; }
        public IEnumerable<string> PayeeTypes { get; set; }
        public SupportingDocuments SupportingDocuments { get; set; }
        public string XAPIKey { get; set; }
        public IEnumerable<PowerBiReport> PowerBiReports { get; set; }
    }
}
