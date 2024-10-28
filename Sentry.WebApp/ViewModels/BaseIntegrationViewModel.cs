using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.ViewModels
{
    public class BaseIntegrationViewModel : BaseViewModel
    {
        public BaseIntegrationViewModel() : base() { }

        public bool IsChanged { get; set; }

        public long Id { get; set; }
        public string System { get; set; }
        public int SystemId { get; set; }
        public string Integration { get; set; }
        public int IntegrationId { get; set; }
        public string RecordStatus { get; set; }
        public string SourceRecordId { get; set; }
        public string ChangeAgent { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? CreatedOnDT { get; set; }

        public override bool IsValid()
        {
            return base.IsValid();
        }

        public string SourceRecordUrl
        {
            get
            {
                if (this.System != null)
                {
                    switch (this.System.ToUpper())
                    {
                        case "LYNX":
                            switch (this.Integration.ToUpper())
                            {
                                case "EMPLOYEE":
                                case "STUDENT":
                                case "CONSTITUENT":
                                case "PARENT":
                                    return $"https://lynx.uafoundation.org/bbappfx/webui/webshellpage.aspx?databasename=BBInfinity#pageType=p&pageId=88159265-2b7e-4c7b-82a2-119d01ecd40f&recordId={SourceRecordId}";
                                case "DESIGNATION":
                                    return $"https://lynx.uafoundation.org/bbappfx/webui/webshellpage.aspx?databasename=BBInfinity#pageType=p&pageId=158571a0-52d3-4a27-9d30-0dbbfa9f1386&recordId={SourceRecordId}";
                                default:
                                    return string.Empty;
                            }
                        case "MARKETO":
                            return $"https://app-sj28.marketo.com/leadDatabase/loadLeadDetail?leadId={SourceRecordId}";
                        default:
                            return string.Empty;
                    }
                }

                return String.Empty;
            }
        }
    }
}
