namespace Sentry.WebApp.ViewModels
{
    public class EmployeeRemediationListItemViewModel : BaseRemediationListItemViewModel
    {

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string UDP { get; set; }
        public string UAPersonId { get; set; }

        public string Type { get; set; }
        public string DataSource { get; set; }
        public string None { get; set; }

    }
}