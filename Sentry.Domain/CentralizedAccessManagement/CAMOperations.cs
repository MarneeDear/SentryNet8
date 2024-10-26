using System;
using System.Collections.Generic;
using System.Linq;
using Sentry.Domain.CentralizedAccessManagement.Entities;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Sentry.Domain.CentralizedAccessManagement
{
    public class Operations : WebServiceOperations
    {

        public Operations(string urlBase, string apiKey) : base(urlBase, apiKey)
        {
           base._urlBase = urlBase;
           base._apiKey = apiKey;
        }

        public async Task<UserDetails> GetUserDetails(string userName)
        {
            UserDetailsReponse result = null;
            try
            {
                string requestUrl = $"access/users/{userName}";
                var response = await ExecuteRequestWithAuthToken(requestUrl, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                    result = JsonConvert.DeserializeObject<UserDetailsReponse>(response.Content);
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    //throw new ArgumentException("User not found");
                    return null;
                else
                {
                    throw new Exception($"Error getting user access from UAF Services Error [{response.StatusCode}] [{response.ErrorMessage}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.UserDetails;
        }

        public async Task<IEnumerable<AuthorizedApprover>> GetDivisionDesignees(string divisionCode)
        {
            AuthorizedApproverResponse result = null;

            try
            {
                string url = $"access/divisions/{divisionCode}/designees";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<AuthorizedApproverResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return null; // Enumerable.Empty<Employee>();
                else
                {
                    throw new Exception($"Error getting division designees [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.Approvers;
        }


        /// <summary>
        /// Get a list of divisions
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<OrganizationalUnit>> GetDivisions()
        {
            OrganizationalUnitReponse result = null;
            try
            {
                string url = "access/divisions";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<OrganizationalUnitReponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return Enumerable.Empty<OrganizationalUnit>();
                else
                {
                    throw new Exception($"Error getting divisions [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.OrganizationalUnits;

        }

        /// <summary>
        /// Get list of departments that roll up to the division
        /// </summary>
        /// <param name="divisionCode"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrganizationalUnit>> GetDepartments(string divisionCode)
        {
            OrganizationalUnitReponse result = null;
            try
            {
                string url = $"access/divisions/{divisionCode}/departments";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<OrganizationalUnitReponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return Enumerable.Empty<OrganizationalUnit>();
                else
                {
                    throw new Exception($"Error getting departments [{divisionCode}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.OrganizationalUnits;

        }

        //GET Projects by department
        public async Task<IEnumerable<Project>> GetProjectsByDepartment(string departmentCode)
        {
            ProjectResponse result = null;
            try
            {
                string url = $"departments/{departmentCode}/projects";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<ProjectResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return Enumerable.Empty<Project>();
                else
                {
                    throw new Exception($"Error getting projects by department [{departmentCode}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.Projects;
        }

        //GET Projects by projectId
        public async Task<ProjectDetails> GetProjectByProjectId(string projectId)
        {
            ProjectDetailsResponse result = null;
            try
            {
                string url = $"projects/{projectId}";
                var response = await ExecuteRequestWithAuthToken(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<ProjectDetailsResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return new ProjectDetails();
                else
                {
                    throw new Exception($"Error getting project details by Project ID [{projectId}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.Project;
        }

        //GET signature authorities by department
        public async Task<IEnumerable<Approver>> GetSignatureAuthoritiesByDepartment(string departmentCode)
        {
            ApproverReponse result = null;
            try
            {
                string url = $"departments/{departmentCode}/signatureauthorities";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<ApproverReponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return Enumerable.Empty<Approver>();
                else
                {
                    throw new Exception($"Error getting signature authorities by department [{departmentCode}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.Approvers;
        }

        //GET designees by division
        public async Task<IEnumerable<Approver>> GetDesigneesByDivision(string divisionCode)
        {
            ApproverReponse result = null;
            try
            {
                string url = $"divisions/{divisionCode}/designees";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<ApproverReponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return Enumerable.Empty<Approver>();
                else
                {
                    throw new Exception($"Error getting signature authorities by department [{divisionCode}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.Approvers;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="designeeEmployeeId"></param>
        /// <param name="divisionCode"></param>
        /// <param name="departmentCode"></param>
        /// <returns></returns>
        public async Task<SignatureAuthorities> GetDesigneeDepartmentSignatureAuthorities(string designeeEmployeeId, 
            string divisionCode, 
            string departmentCode)
        {
            SignatureAuthorityResponse result = null;
            try
            {
                string url = $"access/designees/{designeeEmployeeId}/divisions/{divisionCode}/departments/{departmentCode}/signatureauthorities";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<SignatureAuthorityResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;
                else
                {
                    throw new Exception($"Error getting request by approver by id [{designeeEmployeeId}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.SignatureAuthorities;
        }

        /// <summary>
        /// Searches for employess that match the given criteria -- employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<Employee> FindEmployeesByEmployeeId(string employeeId)
        {
            EmployeeResponse result = null;

            try
            {
                string url = $"access/employees?employeeId={employeeId}";
                var response = await ExecuteRequestWithAuthToken(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<EmployeeResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;  //Enumerable.Empty<Employee>();
                else
                {
                    throw new Exception($"Error getting employees Error [{response.StatusCode}] [{response.ErrorMessage}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.Employee;
        }
        /// <summary>
        /// Searches for employess that match the given criteria -- employee name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> FindEmployeesByName(string term)
        {
            EmployeeListResponse result = null;

            try
            {
                string url = $"access/employees?name={term}";
                var response = await ExecuteRequestWithAuthToken(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<EmployeeListResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;  //Enumerable.Empty<Employee>();
                else
                {
                    throw new Exception($"Error getting employees Error [{response.StatusCode}] [{response.ErrorMessage}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.Employee;
        }

    }
}
