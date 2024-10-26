using Newtonsoft.Json;
namespace Sentry.Domain.CentralizedAccessManagement.Entities
{
    public class UserDetails
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; }
        [JsonProperty("secureId")]
        public string SecureId { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }
        [JsonProperty("departmentCode")]
        public string DepartmentCode { get; set; }
        [JsonProperty("departmentName")]
        public string DepartmentName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
    public class UserDetailsReponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public UserDetails UserDetails { get; set; }
    }
}
