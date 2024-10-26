using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sentry.Domain.CentralizedAccessManagement.Entities
{
    public class PermissionDetails
    {
        public bool IsDesignee { get; set; }
        public bool IsScholarshipOnly { get; set; }
        public bool IsSignatureAuthority { get; set; }
        public bool DivisionLevel { get; set; }
    }
    public class OrganizationalUnitPermission
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public PermissionDetails Details { get; set; }
    }

    public class UserPermission
    {
        public Employee User { get; set; }
        public PermissionDetails Permissions { get; set; }
    }

    public class DesigneeOrganizationalUnitUserPermissions
    {
        public OrganizationalUnit OrganizationalUnit { get; set; }
        public IEnumerable<UserPermission> UserPermissions { get; set; }
    }

    public class UserAccess
    {
        public Employee User { get; set; }
        public IEnumerable<OrganizationalUnitPermission> Permissions { get; set; }

    }

    public class DesgineeUserAccess
    {
        public IEnumerable<DesigneeOrganizationalUnitUserPermissions> DepartmentUserPermissions { get; set; }
        public IEnumerable<DesigneeOrganizationalUnitUserPermissions> DivisionUserPermissions { get; set; }
    }

    public class UserAccessResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        //public IEnumerable<UserAccess> UserAccess { get; set; }
        public DesgineeUserAccess DesgineeUserAccess { get; set; }

    }

    #region Signature Authorities

    public class SignatureAuthority
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public bool DivisionLevel { get; set; }
    }

    public class SignatureAuthorities
    {
        public int Count { get; set; }
        public IEnumerable<SignatureAuthority> Users { get; set; }
    }

    public class SignatureAuthorityResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public SignatureAuthorities SignatureAuthorities { get; set; }
    }

    #endregion
}
