using Sentry.WebApp.Authorization.Attributes;
using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sentry.WebApp.Controllers
{
    [AuthorizeHumanResources]
    public class ConstituentAddressController : IntegrationController
    {
        public const string READONLY = "Not Provided";
        public ConstituentAddressController(AppDbContext context,
            DwDbContext dwContext,
            ILogger<ConstituentAddressController> logger,
            IConfiguration configuration,
            IDomainService domainService) : base(context, dwContext, logger, configuration, domainService) { }

        #region ConstituentAddress

        /*  *** List View ***
        ******************************************************************/
        #region List
        public IActionResult ConstituentAddressList()
        {
            var model = new ConstituentAddressListViewModel()
            {
                Title = "Constituent Address",
                PageId = "constituentAddressPage",
                ActiveClass = "ConstituentAddress",
                Message = "Your Constituent Address Page",
                Integration = "ConstituentAddress",
                IntegrationId = 3,
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
                RemediationList = new List<ConstituentAddressRemediationListItemViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetConstituentAddressList(AjaxDataTableRequest request)
        {
            try
            {
                var constituentAddresses = _context.ConstituentAddressRemediationList.AsQueryable();

                int recordsTotal = constituentAddresses.Count();

                var list = await (string.IsNullOrEmpty(request.searchValue)
                                                ? constituentAddresses
                                                : constituentAddresses.Where(s => s.FirstName.Contains(request.searchValue) ||
                                                                       s.LastName.Contains(request.searchValue) ||
                                                                       s.Address.Contains(request.searchValue) ||
                                                                       s.AddressUseType.Contains(request.searchValue) ||
                                                                       s.ErrorCategories.Contains(request.searchValue) ||
                                                                       s.SystemName.Contains(request.searchValue))
                                         )
                                         .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                         .ToListAsync();

                int recordsFiltered = list.Count();

                var remediationList = new List<ConstituentAddressRemediationListItemViewModel>();

                foreach (var item in list.Skip(request.start).Take(request.length))
                {
                    remediationList.Add(new ConstituentAddressRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        SystemName = item.SystemName,
                        IntegrationId = item.IntegrationId,
                        IntegrationDate = item.IntegrationDate,
                        ErrorCategories = item.ErrorCategories,
                        ErrorCount = item.ErrorCount,
                        RecordStatus = item.RecordStatus,
                        CreatedDate = item.CreatedDate,

                        Name = string.Format("{0} {1}", item.FirstName, item.LastName),
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Address = item.Address,
                        AddressUseType = item.AddressUseType

                    });
                }

                var data = remediationList.ToList();

                return Json(
                    new
                    {
                        request.draw,
                        recordsFiltered,
                        recordsTotal,
                        data
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve ConstituentAddressList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Edit View ***
        ******************************************************************/
        #region Edit
        public IActionResult ConstituentAddressEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetConstituentAddressHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var detail = history.First();
                var source = history.Last();

                var viewModel = new ConstituentAddressViewModel()
                {
                    Id = detail.Id,
                    Title = "Constituent Address",
                    PageId = "constituentAddressPage",
                    ActiveClass = "ConstituentAddress",
                    PageWrapperClass = "toggled",
                    System = detail.SystemName,
                    SystemId = detail.SystemId,
                    Integration = detail.IntegrationName,
                    IntegrationId = detail.IntegrationId,
                    SourceRecordId = detail.SourceRecordId,
                    RecordStatus = detail.RecordStatus,
                    CreatedOnDT = detail.IntegrationDate,
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),
                    IsChanged = false,
                    HistoryData = new List<ConstituentAddressHistoryViewModel>(),

                    #region Constituent Address Details
                    ConstituentMasterId = detail.ConstituentMasterId,
                    ConstituentMasterId_OriginalValue = source.ConstituentMasterId,
                    ConstituentMasterId_BusinessName = detail.ConstituentMasterId_BusinessName,
                    ConstituentMasterId_BusinessDescription = detail.ConstituentMasterId_BusinessDescription,
                    ConstituentMasterId_AttributeId = detail.ConstituentMasterId_AttributeId,
                    ConstituentMasterId_Category = detail.ConstituentMasterId_Category,
                    ConstituentMasterId_Status = detail.ConstituentMasterId_Status,
                    ConstituentMasterId_Source = detail.ConstituentMasterId_Source,
                    ConstituentMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { detail.ConstituentMasterId_Source }),

                    ConstituentSourceSystemRecordId = detail.ConstituentSourceSystemRecordId,
                    ConstituentSourceSystemRecordId_OriginalValue = source.ConstituentSourceSystemRecordId,
                    ConstituentSourceSystemRecordId_BusinessName = detail.ConstituentSourceSystemRecordId_BusinessName,
                    ConstituentSourceSystemRecordId_BusinessDescription = detail.ConstituentSourceSystemRecordId_BusinessDescription,
                    ConstituentSourceSystemRecordId_AttributeId = detail.ConstituentSourceSystemRecordId_AttributeId,
                    ConstituentSourceSystemRecordId_Category = detail.ConstituentSourceSystemRecordId_Category,
                    ConstituentSourceSystemRecordId_Status = detail.ConstituentSourceSystemRecordId_Status,
                    ConstituentSourceSystemRecordId_Source = detail.ConstituentSourceSystemRecordId_Source,
                    ConstituentSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { detail.ConstituentSourceSystemRecordId_Source }),

                    FirstName = detail.FirstName,
                    FirstName_BusinessName = "First Name",
                    FirstName_BusinessDescription = "The employee's first name",
                    FirstName_Source = detail.FirstName_Source,
                    FirstName_OriginalValue = detail.FirstName,
                    FirstName_Category = detail.FirstName_Category,
                    FirstName_Status = detail.FirstName_Status,
                    FirstName_AttributeId = detail.FirstName_AttributeId,
                    FirstName_IsReadOnly = AttributeIsReadOnly(new string[] { detail.FirstName_Source }),
                    LastName = detail.LastName,
                    LastName_BusinessName = "Last Name",
                    LastName_BusinessDescription = "The employee's last name",
                    LastName_Source = detail.LastName_Source,
                    LastName_OriginalValue = detail.LastName,
                    LastName_Category = detail.LastName_Category,
                    LastName_Status = detail.LastName_Status,
                    LastName_AttributeId = detail.LastName_AttributeId,
                    LastName_IsReadOnly = AttributeIsReadOnly(new string[] { detail.LastName_Source }),

                    UAPersonId = detail.UAPersonId,
                    UAPersonId_OriginalValue = source.UAPersonId,
                    UAPersonId_BusinessName = detail.UAPersonId_BusinessName,
                    UAPersonId_BusinessDescription = detail.UAPersonId_BusinessDescription,
                    UAPersonId_AttributeId = detail.UAPersonId_AttributeId,
                    UAPersonId_Category = detail.UAPersonId_Category,
                    UAPersonId_Status = detail.UAPersonId_Status,
                    UAPersonId_Source = detail.UAPersonId_Source,
                    UAPersonId_IsReadOnly = AttributeIsReadOnly(new string[] { detail.UAPersonId_Source }),


                    Address = detail.Address,
                    Address_OriginalValue = source.Address,
                    Address_BusinessName = detail.Address_BusinessName,
                    Address_BusinessDescription = detail.Address_BusinessDescription,
                    Address_AttributeId = detail.Address_AttributeId,
                    Address_Category = detail.Address_Category,
                    Address_Status = detail.Address_Status,
                    Address_Source = detail.Address_Source,
                    Address_IsReadOnly = AttributeIsReadOnly(new string[] { detail.Address_Source }),

                    AddressMasterId = detail.AddressMasterId,
                    AddressMasterId_OriginalValue = source.AddressMasterId,
                    AddressMasterId_BusinessName = detail.AddressMasterId_BusinessName,
                    AddressMasterId_BusinessDescription = detail.AddressMasterId_BusinessDescription,
                    AddressMasterId_AttributeId = detail.AddressMasterId_AttributeId,
                    AddressMasterId_Category = detail.AddressMasterId_Category,
                    AddressMasterId_Status = detail.AddressMasterId_Status,
                    AddressMasterId_Source = detail.AddressMasterId_Source,
                    AddressMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { detail.AddressMasterId_Source }),

                    City = detail.City,
                    City_OriginalValue = source.City,
                    City_BusinessName = detail.City_BusinessName,
                    City_BusinessDescription = detail.City_BusinessDescription,
                    City_AttributeId = detail.City_AttributeId,
                    City_Category = detail.City_Category,
                    City_Status = detail.City_Status,
                    City_Source = detail.City_Source,
                    City_IsReadOnly = AttributeIsReadOnly(new string[] { detail.City_Source }),

                    PostalCode = detail.PostalCode,
                    PostalCode_OriginalValue = source.PostalCode,
                    PostalCode_BusinessName = detail.PostalCode_BusinessName,
                    PostalCode_BusinessDescription = detail.PostalCode_BusinessDescription,
                    PostalCode_AttributeId = detail.PostalCode_AttributeId,
                    PostalCode_Category = detail.PostalCode_Category,
                    PostalCode_Status = detail.PostalCode_Status,
                    PostalCode_Source = detail.PostalCode_Source,
                    PostalCode_IsReadOnly = AttributeIsReadOnly(new string[] { detail.PostalCode_Source }),

                    DeliveryPointCode = detail.DeliveryPointCode,
                    DeliveryPointCode_OriginalValue = source.DeliveryPointCode,
                    DeliveryPointCode_BusinessName = detail.DeliveryPointCode_BusinessName,
                    DeliveryPointCode_BusinessDescription = detail.DeliveryPointCode_BusinessDescription,
                    DeliveryPointCode_AttributeId = detail.DeliveryPointCode_AttributeId,
                    DeliveryPointCode_Category = detail.DeliveryPointCode_Category,
                    DeliveryPointCode_Status = detail.DeliveryPointCode_Status,
                    DeliveryPointCode_Source = detail.DeliveryPointCode_Source,
                    DeliveryPointCode_IsReadOnly = AttributeIsReadOnly(new string[] { detail.DeliveryPointCode_Source }),

                    State = detail.State,
                    State_OriginalValue = source.State,
                    State_BusinessName = detail.State_BusinessName,
                    State_BusinessDescription = detail.State_BusinessDescription,
                    State_AttributeId = detail.State_AttributeId,
                    State_Category = detail.State_Category,
                    State_Status = detail.State_Status,
                    State_Source = detail.State_Source,
                    State_IsReadOnly = AttributeIsReadOnly(new string[] { detail.State_Source }),

                    StateSourceSystemRecordId = detail.StateSourceSystemRecordId,
                    StateSourceSystemRecordId_OriginalValue = source.StateSourceSystemRecordId,
                    StateSourceSystemRecordId_BusinessName = detail.StateSourceSystemRecordId_BusinessName,
                    StateSourceSystemRecordId_BusinessDescription = detail.StateSourceSystemRecordId_BusinessDescription,
                    StateSourceSystemRecordId_AttributeId = detail.StateSourceSystemRecordId_AttributeId,
                    StateSourceSystemRecordId_Category = detail.StateSourceSystemRecordId_Category,
                    StateSourceSystemRecordId_Status = detail.StateSourceSystemRecordId_Status,
                    StateSourceSystemRecordId_Source = detail.StateSourceSystemRecordId_Source,
                    StateSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { detail.StateSourceSystemRecordId_Source }),

                    StateMasterId = detail.StateMasterId,
                    StateMasterId_OriginalValue = source.StateMasterId,
                    StateMasterId_BusinessName = detail.StateMasterId_BusinessName,
                    StateMasterId_BusinessDescription = detail.StateMasterId_BusinessDescription,
                    StateMasterId_AttributeId = detail.StateMasterId_AttributeId,
                    StateMasterId_Category = detail.StateMasterId_Category,
                    StateMasterId_Status = detail.StateMasterId_Status,
                    StateMasterId_Source = detail.StateMasterId_Source,
                    StateMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { detail.StateMasterId_Source, detail.StateSourceSystemRecordId_Source, detail.State_Source }),

                    Country = detail.Country,
                    Country_OriginalValue = source.Country,
                    Country_BusinessName = detail.Country_BusinessName,
                    Country_BusinessDescription = detail.Country_BusinessDescription,
                    Country_AttributeId = detail.Country_AttributeId,
                    Country_Category = detail.Country_Category,
                    Country_Status = detail.Country_Status,
                    Country_Source = detail.Country_Source,
                    Country_IsReadOnly = AttributeIsReadOnly(new string[] { detail.Country_Source }),

                    CountrySourceSystemRecordId = detail.CountrySourceSystemRecordId,
                    CountrySourceSystemRecordId_OriginalValue = source.CountrySourceSystemRecordId,
                    CountrySourceSystemRecordId_BusinessName = detail.CountrySourceSystemRecordId_BusinessName,
                    CountrySourceSystemRecordId_BusinessDescription = detail.CountrySourceSystemRecordId_BusinessDescription,
                    CountrySourceSystemRecordId_AttributeId = detail.CountrySourceSystemRecordId_AttributeId,
                    CountrySourceSystemRecordId_Category = detail.CountrySourceSystemRecordId_Category,
                    CountrySourceSystemRecordId_Status = detail.CountrySourceSystemRecordId_Status,
                    CountrySourceSystemRecordId_Source = detail.CountrySourceSystemRecordId_Source,
                    CountrySourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { detail.CountrySourceSystemRecordId_Source }),

                    CountryMasterId = detail.CountryMasterId,
                    CountryMasterId_OriginalValue = source.CountryMasterId,
                    CountryMasterId_BusinessName = detail.CountryMasterId_BusinessName,
                    CountryMasterId_BusinessDescription = detail.CountryMasterId_BusinessDescription,
                    CountryMasterId_AttributeId = detail.CountryMasterId_AttributeId,
                    CountryMasterId_Category = detail.CountryMasterId_Category,
                    CountryMasterId_Status = detail.CountryMasterId_Status,
                    CountryMasterId_Source = detail.CountryMasterId_Source,
                    CountryMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { detail.CountryMasterId_Source, detail.CountrySourceSystemRecordId_Source, detail.Country_Source }),

                    AddressUseType = detail.AddressUseType,
                    AddressUseType_BusinessName = detail.AddressUseType_BusinessName,
                    AddressUseType_BusinessDescription = detail.AddressUseType_BusinessDescription,
                    AddressUseType_Source = detail.AddressUseType_Source,
                    AddressUseType_OriginalValue = source.AddressUseType,
                    AddressUseType_Category = detail.AddressUseType_Category,
                    AddressUseType_Status = detail.AddressUseType_Status,
                    AddressUseType_AttributeId = detail.AddressUseType_AttributeId,
                    AddressUseType_IsReadOnly = AttributeIsReadOnly(new string[] { detail.AddressUseType_Source }),

                    AddressUseTypeSourceSystemRecordId = detail.AddressUseTypeSourceSystemRecordId,
                    AddressUseTypeSourceSystemRecordId_BusinessName = detail.AddressUseTypeSourceSystemRecordId_BusinessName,
                    AddressUseTypeSourceSystemRecordId_BusinessDescription = detail.AddressUseTypeSourceSystemRecordId_BusinessDescription,
                    AddressUseTypeSourceSystemRecordId_Source = detail.AddressUseTypeSourceSystemRecordId_Source,
                    AddressUseTypeSourceSystemRecordId_OriginalValue = source.AddressUseTypeSourceSystemRecordId,
                    AddressUseTypeSourceSystemRecordId_Category = detail.AddressUseTypeSourceSystemRecordId_Category,
                    AddressUseTypeSourceSystemRecordId_Status = detail.AddressUseTypeSourceSystemRecordId_Status,
                    AddressUseTypeSourceSystemRecordId_AttributeId = detail.AddressUseTypeSourceSystemRecordId_AttributeId,
                    AddressUseTypeSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { detail.AddressUseTypeSourceSystemRecordId_Source }),


                    AddressUseTypeMasterId = detail.AddressUseTypeMasterId,
                    AddressUseTypeMasterId_BusinessName = detail.AddressUseTypeMasterId_BusinessName,
                    AddressUseTypeMasterId_BusinessDescription = detail.AddressUseTypeMasterId_BusinessDescription,
                    AddressUseTypeMasterId_Source = detail.AddressUseTypeMasterId_Source,
                    AddressUseTypeMasterId_OriginalValue = source.AddressUseTypeMasterId,
                    AddressUseTypeMasterId_Category = detail.AddressUseTypeMasterId_Category,
                    AddressUseTypeMasterId_Status = detail.AddressUseTypeMasterId_Status,
                    AddressUseTypeMasterId_AttributeId = detail.AddressUseTypeMasterId_AttributeId,
                    AddressUseTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { detail.AddressUseTypeMasterId_Source, detail.AddressUseTypeSourceSystemRecordId_Source, detail.AddressUseType_Source }),

                    AddressIsPrimary = detail.AddressIsPrimary,
                    AddressIsPrimary_BusinessName = detail.AddressIsPrimary_BusinessName,
                    AddressIsPrimary_BusinessDescription = detail.AddressIsPrimary_BusinessDescription,
                    AddressIsPrimary_Source = detail.AddressIsPrimary_Source,
                    AddressIsPrimary_OriginalValue = source.AddressIsPrimary.ToString(),
                    AddressIsPrimary_Category = detail.AddressIsPrimary_Category,
                    AddressIsPrimary_Status = detail.AddressIsPrimary_Status,
                    AddressIsPrimary_AttributeId = detail.AddressIsPrimary_AttributeId,
                    AddressIsPrimary_IsReadOnly = AttributeIsReadOnly(new string[] { detail.AddressIsPrimary_Source, detail.AddressUseTypeSourceSystemRecordId_Source, detail.AddressUseType_Source }),

                    #endregion

                    #region Dropdown

                    AddressUseTypeList = GetAddressUseTypeList("Use Type"),
                    CountryList = GetCountryList(),
                    StatesList = GetStateList()
                    //Add state to this

                    #endregion
                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);

                    viewModel.HistoryData.Add(new ConstituentAddressHistoryViewModel()
                    {
                        ConstituentMasterId = item.ConstituentMasterId,
                        ConstituentMasterId_Status = item.ConstituentMasterId_Status,
                        ConstituentSourceSystemRecordId = item.ConstituentSourceSystemRecordId,
                        ConstituentSourceSystemRecordId_Status = item.ConstituentSourceSystemRecordId_Status,
                        FirstName = item.FirstName,
                        FirstName_Status = item.FirstName_Status,
                        LastName = item.LastName,
                        LastName_Status = item.LastName_Status,
                        UAPersonId = item.UAPersonId,
                        UAPersonId_Status = item.UAPersonId_Status,
                        Address = item.Address,
                        Address_Status = item.Address_Status,
                        AddressMasterId = item.AddressMasterId,
                        AddressMasterId_Status = item.AddressMasterId_Status,
                        City = item.City,
                        City_Status = item.City_Status,
                        PostalCode = item.PostalCode,
                        PostalCode_Status = item.PostalCode_Status,
                        DeliveryPointCode = item.DeliveryPointCode,
                        DeliveryPointCode_Status = item.DeliveryPointCode_Status,
                        State = item.State,
                        State_Status = item.State_Status,
                        StateSourceSystemRecordId = item.StateSourceSystemRecordId,
                        StateSourceSystemRecordId_Status = item.StateSourceSystemRecordId_Status,
                        StateMasterId = item.StateMasterId,
                        StateMasterId_Status = item.StateMasterId_Status,
                        Country = item.Country,
                        Country_Status = item.Country_Status,
                        CountrySourceSystemRecordId = item.CountrySourceSystemRecordId,
                        CountrySourceSystemRecordId_Status = item.CountrySourceSystemRecordId_Status,
                        CountryMasterId = item.CountryMasterId,
                        CountryMasterId_Status = item.CountryMasterId_Status,
                        AddressUseType = item.AddressUseType,
                        AddressUseType_Status = item.AddressUseType_Status,
                        AddressUseTypeSourceSystemRecordId = item.AddressUseTypeSourceSystemRecordId,
                        AddressUseTypeSourceSystemRecordId_Status = item.AddressUseTypeSourceSystemRecordId_Status,
                        AddressUseTypeMasterId = item.AddressUseTypeMasterId,
                        AddressUseTypeMasterId_Status = item.AddressUseTypeMasterId_Status,
                        AddressIsPrimary = item.AddressIsPrimary,
                        AddressIsPrimary_Status = item.AddressIsPrimary_Status,


                        HistoryDate = item.RecordDate
                    });
                }
                #endregion

                //TODO get rid of this logic when function is changed
                if (viewModel.AddressIsPrimary == null)
                {
                    viewModel.AddressIsPrimary = viewModel.TempAddressIsPrimary;
                }
                if (viewModel.AddressIsPrimary == true)
                {
                    viewModel.TempAddressIsPrimary = true;
                    viewModel.AddressIsPrimary = viewModel.TempAddressIsPrimary;
                }
                if (viewModel.AddressIsPrimary == false)
                {
                    viewModel.TempAddressIsPrimary = false;
                    viewModel.AddressIsPrimary = viewModel.TempAddressIsPrimary;
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ConstituentAddressEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion



        /*  *** Manual Match ***
        ******************************************************************/
        #region ManualMatch
        public async Task<IActionResult> ConstituentAddressManualMatch(long Id, int IntegrationId, int SystemId, string MasterId)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, User.Identity.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ConstituentAddressManualMatch details");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentAddressList));
        }
        #endregion

        /*  *** Save ***
        ******************************************************************/
        #region Save
        // POST: Employee/EmployeeSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConstituentAddressSave(ConstituentAddressViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ConstituentAddressChangeIntegrationRecord(model.SystemId,
                        model.Id,
                        model.ConstituentSourceSystemRecordId,
                        model.ConstituentMasterId,
                        model.FirstName,
                        model.LastName,
                        model.UAPersonId,
                        model.Address, 
                        model.AddressMasterId,
                        model.City, 
                        model.PostalCode, 
                        model.DeliveryPointCode,
                        model.State, 
                        model.StateSourceSystemRecordId, 
                        model.StateMasterId,
                        model.Country, 
                        model.CountrySourceSystemRecordId, 
                        model.CountryMasterId,
                        model.AddressUseType, 
                        model.AddressUseTypeSourceSystemRecordId, 
                        model.AddressUseTypeMasterId,
                        model.AddressIsPrimary,
                        User.Identity.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in ConstituentAddressSave");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(ConstituentAddressList));
            }
            return View(model);
        }
        #endregion

        /*  *** Revalidate ***
        ******************************************************************/
        #region Revalidate
        // POST: Employee/EmployeeRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConstituentAddressRevalidate(ConstituentAddressViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    if (model.IsChanged)
                    {
                        _context.ConstituentAddressChangeIntegrationRecord(model.SystemId,
                            model.Id,
                            model.ConstituentSourceSystemRecordId,
                            model.ConstituentMasterId,
                            model.FirstName,
                            model.LastName,
                            model.UAPersonId,
                            model.Address,
                            model.AddressMasterId,
                            model.City,
                            model.PostalCode,
                            model.DeliveryPointCode,
                            model.State,
                            model.StateSourceSystemRecordId,
                            model.StateMasterId,
                            model.Country,
                            model.CountrySourceSystemRecordId,
                            model.CountryMasterId,
                            model.AddressUseType,
                            model.AddressUseTypeSourceSystemRecordId,
                            model.AddressUseTypeMasterId,
                            model.AddressIsPrimary,
                            User.Identity.Name);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in ConstituentAddressRevalidate");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(ConstituentAddressList));
            }
            return View(model);
        }
        #endregion

        /*  *** Ignore ***
        ******************************************************************/
        #region Ignore
        public IActionResult ConstituentAddressIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load ConstituentAddressIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentAddressList));
        }
        #endregion

        #endregion

    }
}
