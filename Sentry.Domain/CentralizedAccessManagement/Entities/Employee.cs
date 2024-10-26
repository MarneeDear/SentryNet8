using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sentry.Domain.CentralizedAccessManagement.Entities
{
    public class Employee
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("departmentCode")]
        public string DepartmentCode { get; set; }

        [JsonProperty("departmentName")]
        public string DepartmentName { get; set; }

        [JsonProperty("divisionCode")]
        public string DivisionCode { get; set; }

        [JsonProperty("divisionName")]
        public string DivisionName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("netId")]
        public string NetId { get; set; }
    }

    public class EmployeeResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public Employee Employee { get; set; } 

    }
    public class EmployeeListResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public IEnumerable<Employee> Employee { get; set; }

    }
}
