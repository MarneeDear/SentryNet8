namespace Sentry.Domain.MasterDataWebService.Entities
{
    public class Department
    {
        public string MasterRecordCode { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public Organization Organization { get; set; }

    }
}
