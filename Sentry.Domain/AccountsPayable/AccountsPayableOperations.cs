using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Sentry.Domain.AccountsPayable.Entities;
using System.Threading.Tasks;

namespace Sentry.Domain.AccountsPayable
{
    public class AccountsPayableOperations : Sentry.Domain.WebServiceOperations
    {
        public AccountsPayableOperations(string urlBase, string apiKey) : base(urlBase, apiKey)
        {
            base._urlBase = urlBase;
            base._apiKey = apiKey;
        }

        //Update Gift Disbursement
        public async Task UpdateGiftDisbursement(UpdateGiftDisbursement giftDisbursement)
        {
            try
            {
                string uri = $"ap/giftdisbursements/{giftDisbursement.Id}";
                var requestBody = JsonConvert.SerializeObject(giftDisbursement);
                var response = await ExecuteRequest(uri, Method.Put, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error updating gift disbursement [{response.StatusCode}] [{response.Content}] [{requestBody}]");

                }                
            }
            catch
            {
                throw;
            }
        }

        //GET Gift Disbursement
        public async Task<GiftDisbursement> GetGiftDisbursement(long giftDisbursementId)
        {
            GiftDisbursementResponse result = null;

            try
            {
                string url = $"ap/giftdisbursements/{giftDisbursementId}";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<GiftDisbursementResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return new GiftDisbursement();
                else
                {
                    throw new Exception($"Error getting gift disbursement [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.GiftDisbursement;

        }

        public async Task<GiftDisbursement> GetGiftDisbursement(string formNumber)
        {
            GiftDisbursementResponse result = null;

            try
            {
                string url = $"ap/giftdisbursements/search/{formNumber}";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<GiftDisbursementResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return new GiftDisbursement();
                else
                {
                    throw new Exception($"Error getting gift disbursement [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.GiftDisbursement;

        }

        //Get list of gift disbursements
        /// <summary>
        /// Obosolete do not use
        /// </summary>
        /// <returns></returns>
        public async Task<GiftDisbursementList> GetGiftDisbursements()
        {
            GiftDisbursementsResponse result = null;

            try
            {
                string url = $"ap/giftdisbursements/awaitingfsapproval";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<GiftDisbursementsResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    //return Enumerable.Empty<GiftDisbursement>();
                    return new GiftDisbursementList()
                    {
                        ETCount = 0,
                        STCount = 0,
                        EMCount = 0,
                        GiftDisbursements = Enumerable.Empty<GiftDisbursement>()
                    };
                }
                else
                {
                    throw new Exception($"Error getting gift disbursements awaiting financial services approval [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.GiftDisbursementList;
        }

        public async Task<GiftDisbursementList> GetGiftDisbursementsByRoleId(int roleId)
        {
            GiftDisbursementsResponse result = null;

            try
            {
                string url = $"ap/approvers/{roleId}/giftdisbursements";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<GiftDisbursementsResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    //return Enumerable.Empty<GiftDisbursement>();
                    return new GiftDisbursementList()
                    {
                        ETCount = 0,
                        STCount = 0,
                        EMCount = 0,
                        GiftDisbursements = Enumerable.Empty<GiftDisbursement>()
                    };
                }
                else
                {
                    throw new Exception($"Error getting gift disbursements by role [{roleId}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.GiftDisbursementList;
        }

        //Get list of gift disbursements that are ready for processing
        public async Task<GiftDisbursementList> GetGiftDisbursementsAwaitingProcessing()
        {
            GiftDisbursementsResponse result = null;

            try
            {
                string url = $"ap/giftdisbursements/awaitingprocessing";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<GiftDisbursementsResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    //return Enumerable.Empty<GiftDisbursement>();
                    return new GiftDisbursementList()
                    {
                        ETCount = 0,
                        STCount = 0,
                        EMCount = 0,
                        GiftDisbursements = Enumerable.Empty<GiftDisbursement>()
                    };
                }
                else
                {
                    throw new Exception($"Error getting gift disbursements awaiting financial services approval [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.GiftDisbursementList;
        }

        //Approve Gift Disbursement
        public async Task ApproveRejectGiftDisbursement(long giftDisbursementId, string username, UAFApproval approval)
        {
            try
            {
                string uri = $"ap/approvers/uaf/{username}/giftdisbursements/{giftDisbursementId}";
                var requestBody = JsonConvert.SerializeObject(approval);

                var response = await ExecuteRequest(uri, Method.Patch, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error approving gift disbursement for user [{username}] and id [{giftDisbursementId}] [{response.StatusCode}] [{response.Content}]");
                }                
            }
            catch
            {
                throw;
            }
        }

        public async Task ResetGiftDisbursementUAFApprovers(long giftDisbursementId)
        {
            try
            {
                string uri = $"ap/approvers/uaf/giftdisbursements/{giftDisbursementId}/approvers/reset";

                var response = await ExecuteRequest(uri, Method.Patch);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error resetting gift disbursement approvers for disbursement id [{giftDisbursementId}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<long> CreateInvoice(Entities.Invoice.CreateInvoice invoice)
        {
            try
            {
                string uri = $"ap/giftdisbursements/{invoice.DisbursementId}/invoices";
                var requestBody = JsonConvert.SerializeObject(invoice);
                var response = await ExecuteRequest(uri, Method.Post, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error creating invoice for id [{invoice.DisbursementId}] [{response.StatusCode}] [{response.Content}]");
                }
                else
                {

                    var newInvoice = JsonConvert.DeserializeObject<Entities.Invoice.NewInvoiceReponse>(response.Content);
                    return newInvoice.NewInvoice.InvoiceId;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<long> CreateJournalEntryBatch(long fundsTransferId)
        {
            try
            {
                string uri = $"ap/fundstransfers/{fundsTransferId}/journalentrybatch";
                var response = await ExecuteRequest(uri, Method.Post);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error creating journal entry batch for id [{fundsTransferId}] [{response.StatusCode}] [{response.Content}]");
                }
                else
                {

                    var newJournalEntryBatch = JsonConvert.DeserializeObject<Entities.JournalEntryBatch.NewJournalEntryBatchReponse>(response.Content);
                    return newJournalEntryBatch.NewJournalEntryBatch.RecordId;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<GiftDisbursementDebitAccount>> GetDebitAccounts(string projectId)
        {
            GiftDisbursementsDebitAccountResponse result = null;

            try
            {
                string url = $"ap/giftdisbursements/debitaccountnumber/{projectId}";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<GiftDisbursementsDebitAccountResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return Enumerable.Empty<GiftDisbursementDebitAccount>();
                else
                {
                    throw new Exception($"Error getting debit accounts [{response.StatusCode}] [{response.Content}]");
                }

                return result.GiftDisbursementsDebitAccounts;
            }
            catch 
            {
                throw;
            }
        }

        public async Task QueueFormForProcessing(Entities.Invoice.CreateInvoice invoice)
        {
            try
            {
                //giftdisbursements/invoices/queue
                string uri = $"ap/giftdisbursements/invoices/queue";
                var requestBody = JsonConvert.SerializeObject(invoice);
                var response = await ExecuteRequest(uri, Method.Post, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error queueing form for id [{invoice.DisbursementId}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            
        }
        //GET Gift Disbursement
        public async Task<ProjectAccountDetails> GetProjectAccountDetails(GetProjectAccount projectAccount)
        {
            ObjectCodeDetailsResponse result = null;

            try
            {
                string url = $"ap/account/details?accountNumber={projectAccount.accountNumber}&projectid={projectAccount.projectId}&objectCode={projectAccount.objectCode}";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<ObjectCodeDetailsResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return new ProjectAccountDetails();
                else
                {
                    throw new Exception($"Error getting project account details [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.ObjectCodeDetails;

        }

        public async Task<NewVendorRequest> GetNewVendorRequestById(long id)
        {
            NewVendorRequestResponse result = null;

            try
            {
                string url = $"ap/newvendorrequests/{id}";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<NewVendorRequestResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return new NewVendorRequest();
                else
                {
                    throw new Exception($"Error getting gift disbursement [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.NewVendorRequest;

        }

        /// <summary>
        /// Updates New Vendor Request
        /// </summary>
        /// <param name="newVendorRequest"></param>
        /// <returns></returns>
        public async Task UpdateNewVendorRequest(UpdateNewVendorRequest newVendorRequest)
        {
            try
            {
                string uri = $"ap/newvendorrequests/{newVendorRequest.Id}";
                var requestBody = JsonConvert.SerializeObject(newVendorRequest);
                var response = await ExecuteRequest(uri, Method.Put, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error updating new vendor request [{response.StatusCode}] [{response.Content}] [{requestBody}]");

                }
            }
            catch
            {
                throw;
            }
        }

        //Get list of new vendor request that are ready for approval
        public async Task<IEnumerable<NewVendorRequest>> GetNewVendorRequestsAwaitingApproval()
        {
            NewVendorRequestsResponse result = null;

            try
            {
                string url = $"ap/newvendorrequests/awaitingapproval";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<NewVendorRequestsResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception($"Error getting new vendor request awaiting financial services approval [{response.StatusCode}] [{response.Content}]");

                }
                else
                {
                    throw new Exception($"Error getting new vendor request awaiting financial services approval [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.NewVendorRequests;
        }

        //Approve New Vendor Request
        public async Task ApproveRejectNewVendorRequest(long newVendorRequestId, NewVendorRequestApproval approval)
        {
            try
            {
                string uri = $"ap/newvendorrequests/{newVendorRequestId}/approve";
                var requestBody = JsonConvert.SerializeObject(approval);

                var response = await ExecuteRequest(uri, Method.Post, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error approving new vendor request for id [{newVendorRequestId}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task ResetNewVendorRequest(long newVendorRequestId)
        {
            try
            {
                string uri = $"ap/newvendorrequests/{newVendorRequestId}/reset";

                var response = await ExecuteRequest(uri, Method.Patch);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error resetting approval for new vendor request id [{newVendorRequestId}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<long> CreateVendor(long newVendorRequestId)
        {
            try
            {
                string uri = $"ap/newvendorRequests/{newVendorRequestId}";
                var response = await ExecuteRequest(uri, Method.Post);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error creating vendor for id [{newVendorRequestId}] [{response.StatusCode}] [{response.Content}]");
                }
                else
                {
                    var newVendor = JsonConvert.DeserializeObject<Entities.NewVendorRequestResponse>(response.Content);
                    return newVendor.NewVendorRequest.VendorId;
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get funds transfers list
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<FundsTransferSummary>> GetFundsTransfersAwaitingApproval(int roleId)
        {
            FundsTransferListResponse result = null;
            //TODO: Change routing type to be more RESTful
            try
            {
                string url = $"ap/fundsTransfers/{roleId}/awaitingfsapproval";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<FundsTransferListResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception($"Error getting funds transfer awaiting financial services approval [{response.StatusCode}] [{response.Content}]");

                }
                else
                {
                    throw new Exception($"Error getting funds transfer awaiting financial services approval [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.FundsTransferList;
        }

        public async Task<FundsTransfer> GetFundsTransfer(long fundsTransferId)
        {
            FundsTransferResponse result = null;

            try
            {
                string url = $"ap/fundstransfers/{fundsTransferId}";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<FundsTransferResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return new FundsTransfer();
                else
                {
                    throw new Exception($"Error getting funds transfer [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.FundsTransfer;

        }

        public async Task UpdateFundsTransfer(UpdateFundsTransfer fundsTransfer)
        {
            try
            {
                string uri = $"ap/fundsTransfers/{fundsTransfer.Id}";
                var requestBody = JsonConvert.SerializeObject(fundsTransfer);
                var response = await ExecuteRequest(uri, Method.Put, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error updating funds transfer [{response.StatusCode}] [{response.Content}] [{requestBody}]");

                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<FundsTransfer> GetFundsTransfer(string formNumber)
        {
            FundsTransferResponse result = null;

            try
            {
                string url = $"ap/fundsTransfers/search/{formNumber}";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<FundsTransferResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return new FundsTransfer();
                else
                {
                    throw new Exception($"Error getting funds transfer [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.FundsTransfer;

        }

        //GET Routing Types
        public async Task<IEnumerable<TransferRoutingTypes>> GetTransferRoutingTypes()
        {
            TransferRoutingTypesResponse result = null;
            try
            {
                string url = $"ap/fundsTransfers/transferRoutingTypes";
                var response = await ExecuteRequestWithAuthToken(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<TransferRoutingTypesResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    return new List<TransferRoutingTypes>();
                else
                {
                    throw new Exception($"Error getting project types [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.TransferRoutingTypes;
        }

        public async Task<IEnumerable<FundsTransfer>> GetFundsTransfersByRoleId(int roleId)
        {
            //TODO make work for funds transfers
            FundsTransfersResponse result = null;

            try
            {
                string url = $"ap/approvers/{roleId}/fundstransfers";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<FundsTransfersResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<FundsTransfer>();
                }
                else
                {
                    throw new Exception($"Error getting funds transfers by role [{roleId}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.FundsTransferList;
        }

        public async Task<IEnumerable<FundsTransferCount>> GetFundsTransferCounts()
        {
            FundsTransferCountsResponse result = null;

            try
            {
                string url = $"ap/fundstransfers/counts";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<FundsTransferCountsResponse>(response.Content);
                }                
                else
                {
                    throw new Exception($"Error getting funds transfers counts. Response [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.FundsTransferCounts;
        }

        //Get list of gift disbursements that are ready for processing
        public async Task<IEnumerable<FundsTransferSummary>> GetFundsTransfersAwaitingProcessing()
        {
            FundsTransferListResponse result = null;

            try
            {
                //TODO need to refactor the UAF services side
                string url = $"ap/fundstransfers/awaitingprocessing";
                var response = await ExecuteRequest(url, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<FundsTransferListResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<FundsTransferSummary>();
                }
                else
                {
                    throw new Exception($"Error getting funds transfers awaiting financial services approval [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.FundsTransferList;
        }

        public async Task ApproveRejectFundsTransfer(long fundsTransferId, string employeeId, UAFApproval approval)
        {
            try
            {
                string uri = $"ap/approvers/uaf/{employeeId}/fundsTransfers/{fundsTransferId}";
                var requestBody = JsonConvert.SerializeObject(approval);

                var response = await ExecuteRequest(uri, Method.Patch, requestBody);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error approving gift disbursement for user [{employeeId}] and id [{fundsTransferId}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task ResetFundsTransferUAFApprovers(long fundsTransferId)
        {
            try
            {
                string uri = $"ap/approvers/uaf/fundstransfers/{fundsTransferId}/approvers/reset";

                var response = await ExecuteRequest(uri, Method.Patch);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error resetting funds transfer approvers for disbursement id [{fundsTransferId}] [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<FundsTransferJournalEntry>> GetJournalEntryBatchPreview(long fundsTransferId)
        {
            try
            {
                string uri = $"ap/fundstransfers/{fundsTransferId}/journalentrybatch";
                var response = await ExecuteRequest(uri, Method.Get);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error getting journal entry batch preview for id [{fundsTransferId}] [{response.StatusCode}] [{response.Content}]");
                }
                else
                {

                    var batch = JsonConvert.DeserializeObject<Entities.FundsTransferJournalEntriesResponse>(response.Content);
                    return batch.FundsTransferJournalEntries;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
