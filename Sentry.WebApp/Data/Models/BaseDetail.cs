using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
	public class BaseDetail
	{
        //[Key]
        [Column("RecordId")]
		public long Id { get; set; }

        public int SystemId { get; set; }

        public string SystemName { get; set; }

        public DateTime? IntegrationDate { get; set; }

        [Key]
        public DateTime RecordDate { get; set; }

        public int IntegrationId { get; set; }

        public string IntegrationName { get; set; }

        public long? LineageKey { get; set; }

		[Column("SystemRecordId")]
		public string SourceRecordId { get; set; }

		public string RecordStatus { get; set; }

        public string SourceRecordUrl(string SystemName, string SourceRecordId, string IntegrationName)
        {
            switch (SystemName.ToUpper())
            {
                case "LYNX":
                    switch (IntegrationName.ToUpper())
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

    }
}