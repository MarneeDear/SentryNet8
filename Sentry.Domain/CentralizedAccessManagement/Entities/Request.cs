using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Sentry.Domain.CentralizedAccessManagement.Entities
{
    public class SuccessfulCreate
    {
        [JsonProperty("requestId")]
        public long RequestId { get; set; }
        //public IEnumerable<string> OrganizationalUnitCodes { get; set; }
    }

    public class AuthorizedApprover
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class AuthorizedApproverResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public IEnumerable<AuthorizedApprover> Approvers { get; set; }

    }


    public class RequestDetail
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isScholarshipOnly")]
        public bool IsScholarshipOnly { get; set; }

        [JsonProperty("isDesignee")]
        public bool IsDesignee { get; set; }

        [JsonProperty("isSignatureAuthority")]
        public bool IsSignatureAuthority { get; set; }

    }
    public class Request
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("requestTypeId")]
        public int RequestTypeId { get; set; }

        [JsonProperty("isScholarshipOnly")]
        public bool IsScholarshipOnly { get; set; }

        [JsonProperty("requestType")]
        public string RequestType { get; set; }

        [JsonProperty("divisionCode")]
        public string DivisionCode { get; set; }

        [JsonProperty("divisionName")]
        public string DivisionName { get; set; }

        [JsonProperty("requestedByFirstName")]
        public string RequestedByFirstName { get; set; }

        [JsonProperty("requestedByLastName")]
        public string RequestedByLastName { get; set; }
      
        [JsonProperty("requestedByEmail")]
        public string RequestedByEmail { get; set; }

        [JsonProperty("requestedByDepartmentCode")]
        public string RequestedByDepartmentCode { get; set; }

        [JsonProperty("requestedByDepartmentName")]
        public string RequestedByDepartmentName { get; set; }

        [JsonProperty("firstName")]
        public string EmployeeFirstName { get; set; }

        [JsonProperty("lastName")]
        public string EmployeeLastName { get; set; }

        [JsonProperty("email")]
        public string EmployeeEmail { get; set; }

        [JsonProperty("employeeId")]
        public string EmployeeEmployeeId { get; set; }

        [JsonProperty("phone")]
        public string EmployeePhone { get; set; }

        [JsonProperty("jobTitle")]
        public string EmployeeJobTitle { get; set; }

        [JsonProperty("departmentCode")]
        public string EmployeeDepartmentCode { get; set; }

        [JsonProperty("departmentName")]
        public string EmployeeDepartmentName { get; set; }

        [JsonProperty("approvedOn")]
        public DateTime? ApprovedOn { get; set; }

        [JsonProperty("deniedOn")]
        public DateTime? DeniedOn { get; set; }

        [JsonProperty("approverEmployeeId")]
        public string ApproverEmployeeId { get; set; }

        [JsonProperty("approverName")]
        public string ApproverName { get; set; }

        [JsonProperty("requestorComments")]
        public string RequestorComments { get; set; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("requestDetails")]
        public IEnumerable<RequestDetail> RequestDetails { get; set; }

    }

    public class SuccessfulCreateRequestResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public SuccessfulCreate SuccessfulCreateRequest { get; set; }
    }

    public class RequestResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public Request Request { get; set; }

    }

    public class OrganizationalUnitPermissionRequest
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        public string Name { get; set; }

        [JsonProperty("isScholarshipOnly")]
        public bool IsScholarshipOnly { get; set; }

    }

    public enum RequestType
    {
        ProjectReporting = 1,
        DesigneeRequest = 2,
        SignatureAuthority = 3
    }

    public class NewRequest
    {
        [JsonProperty("requestTypeId")]
        public RequestType RequestTypeId { get; set; }

        [JsonProperty("requestForEmployeeId")]
        public string RequestForEmployeeId { get; set; }

        [JsonProperty("requestedByEmployeeId")]
        public string RequestedByEmployeeId { get; set; }

        [JsonProperty("divisionCode")]
        public string DivisionCode { get; set; }

        [JsonProperty("isScholarshipOnly")]
        public bool IsScholarshipOnly { get; set; }

        [JsonProperty("organizationalUnits")]
        public IEnumerable<OrganizationalUnitPermissionRequest> OrganizationalUnits { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

    }

    public class ApproveRequest
    {
        [JsonProperty("approved")]
        public bool Approved { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }
    }


}
