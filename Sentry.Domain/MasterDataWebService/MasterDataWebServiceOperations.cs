using Newtonsoft.Json;
using Sentry.Domain.MasterDataWebService.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sentry.Domain.MDM
{
    public class MasterDataWebServiceOperations : Sentry.Domain.WebServiceOperations
    {
        public MasterDataWebServiceOperations(string urlBase, string apiKey) : base(urlBase, apiKey)
        {
            base._urlBase = urlBase;
            base._apiKey = apiKey;
        }

        public async Task<IEnumerable<College>> GetColleges()
        {
            try
            {
                string url = "organization/college";
                var response = await ExecuteRequest(url, RestSharp.Method.Get);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<College>>(response.Content);
                }
                else
                {
                    throw new Exception($"Error getting colleges. ERROR [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Department>> GetDepartments(string collegeId)
        {
            try
            {
                string url = $"organization/ua/department/{collegeId}";
                var response = await ExecuteRequest(url, RestSharp.Method.Get);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Department>>(response.Content);
                }
                else
                {
                    throw new Exception($"Error getting departments. ERROR [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Designation>> GetDesignations(string organization, string departmentId)
        {
            try
            {
                string url = $"designation/{organization}/departmentId/{departmentId}";
                var response = await ExecuteRequest(url, RestSharp.Method.Get);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<Designation>>(response.Content);
                }
                else
                {
                    throw new Exception($"Error getting designations. ERROR [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Designation> GetDesignation(string organization, string designationId)
        {
            try
            {
                string url = $"designation/{organization}/designationId/{designationId}";
                var response = await ExecuteRequest(url, RestSharp.Method.Get);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<Designation>(response.Content);
                }
                else
                {
                    return new Designation();
                    //throw new Exception($"Error getting designation. ERROR [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool?> CheckIfDesignationIsBFE(string designationId)
        {
            try
            {
                string url = $"designation/designationIsBFE/{designationId}";
                var response = await ExecuteRequest(url, RestSharp.Method.Get);
                if (response.IsSuccessful)
                {
                    return Convert.ToBoolean(response.Content);
                }
                else
                {
                    throw new Exception($"Error getting BFE. ERROR [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public bool ValidateEmail(string email)
        {
            //TODO use the MDM web service
            return true;
        }

        public bool ValidatePhone(string phone)
        {
            //TODO use the MDM web service
            return true;
        }

        public bool ValidateMailingAddress(MailingAddress mailingAddress)
        {
            //TODO use the MDM web service
            return true;
        }

        public async Task<bool> RemoveEmailFromSource(string systemRecordId)
        {
            try
            {
                string url = $"lynx/Email/RemoveBySourceId/{systemRecordId}";
                var response = await ExecuteRequest(url, RestSharp.Method.Post, null);

                if (response.IsSuccessful)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error removing email from source. error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }


        }

        public async Task<bool> RemovePhoneFromSource(string systemRecordId)
        {
            try
            {
                string url = $"lynx/phone/removeBySourceId/{systemRecordId}";
                var response = await ExecuteRequest(url, RestSharp.Method.Post, null);

                if (response.IsSuccessful)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error removing phone from source. error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> RemoveAddressFromSource(string systemRecordId)
        {
            try
            {
                string url = $"lynx/address/removeBySourceId/{systemRecordId}";
                var response = await ExecuteRequest(url, RestSharp.Method.Post, null);

                if (response.IsSuccessful)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Error removing email from source. error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }

        }


    }
}
