namespace Sentry.Domain.MasterDataWebService.Entities
{
    public class Designation
    {
        public string MasterRecordCode { get; set; }

        public string Name { get; set; }

        public string DesignationId { get; set; }

        public string Description { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public Department Department { get; set; }

    }
}
