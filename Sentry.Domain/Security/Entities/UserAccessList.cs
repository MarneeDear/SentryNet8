using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace Sentry.Domain.Security.Entities
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
    public class UserPermissionsResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public UserPermissions UserPermissions { get; set; }
    }

    public class OrganizationalUnitPermission
    {
        public long Id { get; set; }
        public string ParentCode { get; set; }
        public string ParentName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool DivisionLevel { get; set; }
        public OrganizationalUnitPermissionsDetails Details { get; set; }
    }

    public class OrganizationalUnitPermissionsDetails
    {
        public bool IsDesignee { get; set; }
        public bool IsScholarshipOnly { get; set; }
        public bool IsSignatureAuthority { get; set; }
        public bool DivisionLevel { get; set; }
    }

    public class UserPermissions
    {
        [JsonProperty("organizations")]
        public IEnumerable<OrganizationalUnitPermission> OrganizationalUnitPermissions { get; set; }        
    }

    //public class UserPermissions_obsolte
    //{
    //    [JsonProperty("organizations")]
    //    public OrganizationPermission OrganizationPermissions { get; set; }

    //    [JsonProperty("funds")]
    //    public FundCollection Funds { get; set; }

    //    [JsonProperty("projects")]
    //    public ProjectCollection Projects { get; set; }

    //    public class OrganizationPermission
    //    {
    //        [JsonProperty("code")]
    //        public string Code { get; set; }

    //        [JsonProperty("name")]
    //        public string Name { get; set; }

    //        [JsonProperty("isDesignee")]
    //        public bool IsDesignee { get; set; }

    //        [JsonProperty("isScholarshipOnly")]
    //        public bool IsScholarshipOnly { get; set; }

    //        [JsonProperty("isSignatureAuthority")]
    //        public bool IsSignatureAuthority { get; set; }
    //    }

    //    public class OrganizationCollection : List<OrganizationPermission>, IEnumerable<SqlDataRecord>
    //    {
    //        public OrganizationCollection(IEnumerable<OrganizationPermission> organizations) : base(organizations)
    //        {
    //        }

    //        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
    //        {
    //            var sqlRow = new SqlDataRecord(
    //                new SqlMetaData("OrganizationalUnitCode", SqlDbType.VarChar, 4),
    //                new SqlMetaData("OrganizationalUnitName", SqlDbType.VarChar, 100),
    //                new SqlMetaData("IsScholarshipOnly", SqlDbType.Bit),
    //                new SqlMetaData("IsDesignee", SqlDbType.Bit),
    //                new SqlMetaData("IsSignatureAuthority", SqlDbType.Bit)
    //            );

    //            foreach (OrganizationPermission org in this)
    //            {
    //                sqlRow.SetString(0, org.Code);
    //                sqlRow.SetString(1, org.Name);
    //                sqlRow.SetBoolean(2, org.IsScholarshipOnly);
    //                sqlRow.SetBoolean(3, org.IsDesignee);
    //                sqlRow.SetBoolean(4, org.IsSignatureAuthority);

    //                yield return sqlRow;
    //            }
    //        }
    //    }

    //    public class Project
    //    {
    //        [JsonProperty("id")]
    //        public string Id { get; set; }

    //        [JsonProperty("description")]
    //        public string Description { get; set; }

    //        [JsonProperty("organizationalUnitCode")]
    //        public string OrganizationalUnitCode { get; set; }

    //        [JsonProperty("organizationalUnitCodeName")]
    //        public string OrganizationalUnitName { get; set; }
    //    }

    //    public class ProjectCollection : List<Project>, IEnumerable<SqlDataRecord>
    //    {
    //        public ProjectCollection(IEnumerable<Project> projects) : base(projects)
    //        {
    //        }

    //        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
    //        {
    //            var sqlRow = new SqlDataRecord(
    //                new SqlMetaData("ProjectID", SqlDbType.VarChar, 10),
    //                new SqlMetaData("Description", SqlDbType.VarChar, 60),
    //                new SqlMetaData("OrganizationalUnitCode", SqlDbType.VarChar, 4),
    //                new SqlMetaData("OrganizationalUnitName", SqlDbType.VarChar, 4)
    //            );

    //            foreach (Project proj in this)
    //            {
    //                sqlRow.SetString(0, proj.Id);
    //                sqlRow.SetString(1, proj.Description);
    //                sqlRow.SetString(2, proj.OrganizationalUnitCode);
    //                sqlRow.SetString(3, proj.OrganizationalUnitName);

    //                yield return sqlRow;
    //            }
    //        }
    //    }

    //    public class Fund
    //    {
    //        [JsonProperty("id")]
    //        public string Id { get; set; }

    //        [JsonProperty("description")]
    //        public string Description { get; set; }
    //    }

    //    public class FundCollection : List<Fund>, IEnumerable<SqlDataRecord>
    //    {
    //        public FundCollection(IEnumerable<Fund> funds) : base(funds)
    //        {
    //        }

    //        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
    //        {
    //            var sqlRow = new SqlDataRecord(
    //                new SqlMetaData("FundID", SqlDbType.VarChar, 2),
    //                new SqlMetaData("Description", SqlDbType.VarChar, 60)
    //            );

    //            foreach (Fund fund in this)
    //            {
    //                sqlRow.SetString(0, fund.Id);
    //                sqlRow.SetString(1, fund.Description);

    //                yield return sqlRow;
    //            }
    //        }
    //    }
    //}
}
