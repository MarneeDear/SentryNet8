using Newtonsoft.Json;
using Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sentry.Domain.Lynx.WebService
{
    public class LynxWebServiceOperations : Sentry.Domain.WebServiceOperations
    {
        public LynxWebServiceOperations(string urlBase, string apiKey) : base(urlBase, apiKey)
        {
            base._urlBase = urlBase;
            base._apiKey = apiKey;
        }

        public IEnumerable<GiftTransmittal> GetGiftTransmittals()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// https://localhost:44348/gifttransmittal/AAF42527-D963-417E-A1E7-0280384A6BC6
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GiftTransmittal> LoadGiftTransmittal(Guid id)
        {
            try
            {
                string url = $"/gifttransmittal/{id}";
                var response = await ExecuteRequest(url, RestSharp.Method.Get);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<GiftTransmittal>(response.Content);
                }
                else
                {
                    throw new Exception($"Error getting gift transmittal. Error status [{response.StatusDescription}] error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="giftTransmittal"></param>
        /// <returns></returns>
        public async Task<Guid> SaveGiftTransmittal(GiftTransmittal giftTransmittal)
        {
            try
            {
                string url = $"/gifttransmittal";
                var response = await ExecuteRequest(url, RestSharp.Method.Post, JsonConvert.SerializeObject(giftTransmittal));
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<Guid>(response.Content);
                }
                else
                {
                    throw new Exception($"Error saving gift transmittal. Error status [{response.StatusDescription}] error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Guid> DeleteDistribution(GiftTransmittalItemDistribution giftTransmittalItemDistribution)
        {
            giftTransmittalItemDistribution.IsDeleted = true;
            try
            {
                string url = $"/GiftTransmittalItemDistribution/upsert";
                var body = JsonConvert.SerializeObject(giftTransmittalItemDistribution);
                var response = await ExecuteRequest(url, RestSharp.Method.Post, body);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<Guid>(response.Content);
                }
                else
                {
                    throw new Exception($"Error deleting gift transmittal item distribution. ERROR [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// https://localhost:44348/gifttransmittal/SearchByFormId/GT404040
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GiftTransmittal> GetGiftTransmittal(string formNumber)
        {
            try
            {
                string url = $"/gifttransmittal/SearchByFormId/{formNumber}";
                var response = await ExecuteRequest(url, RestSharp.Method.Get);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<GiftTransmittal>(response.Content);
                }
                else
                {
                    throw new Exception($"Error getting gift transmittal. Error status [{response.StatusDescription}] error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// https://localhost:44348/gifttransmittal/SearchByFormId/GT404040
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<GiftTransmittalApproval>> GetGiftTransmittalApprovals(Guid id)
        {
            try
            {
                string url = $"/gifttransmittal/{id}/approval";
                var response = await ExecuteRequest(url, RestSharp.Method.Get);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<List<GiftTransmittalApproval>>(response.Content);
                }
                else
                {
                    throw new Exception($"Error getting gift transmittal. Error status [{response.StatusDescription}] error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task ApproveGiftTransmittal(GiftTransmittalApproval approval)
        {
            try
            {
                string url = $"/gifttransmittal/approval";
                var body = JsonConvert.SerializeObject(approval);
                var response = await ExecuteRequest(url, RestSharp.Method.Post, body);
                if (!response.IsSuccessful)                
                {
                    throw new Exception($"Error approving gift transmittal. Error status [{response.StatusDescription}] error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task ResetGiftTransmittalApprover(GiftTransmittalApproval approvalRecord)
        {
            try
            {
                string url = $"/gifttransmittal/{approvalRecord.GiftTransmittalId}/approvers/reset";
                var body = JsonConvert.SerializeObject(approvalRecord);
                var response = await ExecuteRequest(url, RestSharp.Method.Patch, body);
                if (!response.IsSuccessful)
                {
                    throw new Exception($"Error resetting gift transmittal [{approvalRecord.GiftTransmittalId}] approval code [{approvalRecord.ApprovalStageCode}]. Error status [{response.StatusDescription}] error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task AddSecondaryApprover(Guid giftTransmittalId)
        {
            try
            {
                string url = $"/giftTransmittal/{giftTransmittalId}/secondaryapprover";
                var response = await ExecuteRequest(url, RestSharp.Method.Post);
                if (!response.IsSuccessful)
                {
                    throw new Exception($"Error adding secondary approver for gift transmittal id [{giftTransmittalId}]. Error status [{response.StatusDescription}] error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task RemoveSecondaryApprover(Guid approvalId)
        {
            try
            {
                string url = $"/giftTransmittal/approvals/{approvalId}";
                var response = await ExecuteRequest(url, RestSharp.Method.Delete);
                if (!response.IsSuccessful)
                {
                    throw new Exception($"Error deleting gift transmittal approval [{approvalId}]. Error status [{response.StatusDescription}] error message [{response.ErrorMessage}]");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
