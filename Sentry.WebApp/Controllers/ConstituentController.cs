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
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Sentry.WebApp.Controllers
{

    [AuthorizeRecordsQuality]
    public class ConstituentController : IntegrationController
    {
        public const string READONLY = "Not Provided";
        public ConstituentController(AppDbContext context, 
            DwDbContext dwContext, 
            ILogger<ConstituentController> logger, 
            IConfiguration configuration,
            IDomainService domainService) : base(context, dwContext, logger, configuration, domainService) { }

        #region Dashboard
        // GET: Constituent Dashboard
        public IActionResult Index()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                Title = "Constituent",
                PageId = "constituentDashboardPage",
                ActiveClass = "ConstituentDashboard",
                PageWrapperClass = "toggled",
                //RemediationCounts = GetRemediationCounts(),
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups()
            };

            return View(viewModel);
        }
        #endregion

        #region Group
        // GET: Constituent Group List
        public IActionResult ConstituentGroupList()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                Title = "Constituent Group",
                PageId = "constituentGroupPage",
                ActiveClass = "ConstituentGroup",
                PageWrapperClass = "toggled",
                //RemediationCounts = GetRemediationCounts(),
                User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
            };

            return View(viewModel);
        }
        #endregion

        #region Organization
        // GET: Constituent Organization List
        public IActionResult ConstituentOrganizationList()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                Title = "Constituent Organization",
                PageId = "constituentOrganizationPage",
                ActiveClass = "ConstituentOrganization",
                PageWrapperClass = "toggled",
                User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
            };

            return View(viewModel);
        }
        #endregion

        #region Individual

        /*  *** List View ***
        ******************************************************************/
        #region List
        // GET: Constituent/ConstituentList
        public IActionResult ConstituentList()
        {
            var model = new ConstituentListViewModel()
            {
                Title = "Constituent",
                PageId = "constituentIndividualPage",
                ActiveClass = "Constituent",
                Message = "Your Constituent Page",
                Integration = "Constituent",
                IntegrationId = 6,
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetConstituentList(AjaxDataTableRequest request)
        {
            try
            {
                var constituents = _context.ConstituentsRemediationList.AsQueryable();

                int recordsTotal = constituents.Count();

                var constituentList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? constituents
                                                : constituents.Where(s => s.Name.Contains(request.searchValue) || s.UAPersonId.Contains(request.searchValue) || s.ErrorCategories.Contains(request.searchValue) || s.SystemName.Contains(request.searchValue))
                                            )
                                            .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                            .ToListAsync();

                int recordsFiltered = constituentList.Count();

                var constituentRemediationList = new List<ConstituentRemediationListItemViewModel>();

                foreach (var item in constituentList.Skip(request.start).Take(request.length))
                {
                    constituentRemediationList.Add(new ConstituentRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        SystemName = item.SystemName,
                        ErrorCategories = item.ErrorCategories,
                        ErrorCount = item.ErrorCount,
                        IntegrationDate = item.IntegrationDate,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate,
                        RecordStatus = item.RecordStatus,
                        Name = item.Name,
                        UAPersonId = item.UAPersonId
                    });
                }

                var data = constituentRemediationList.ToList();

                return Json(
                    new {
                        request.draw,
                        recordsFiltered,
                        recordsTotal,
                        data
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve ConstituentList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Edit View ***
        ******************************************************************/
        #region Edit
        private ConstituentViewModel SetupConstituentViewModel(long id, int systemId)
        {
                var history = _context.GetConstituentHistory(systemId, id).OrderByDescending(m => m.RecordDate);
                var constituentDetail = history.First();
                var constituentSource = history.Last();
                //DateTime birthDate;
                //DateTime deceasedDate;

                var viewModel = new ConstituentViewModel()
                {
                    Title = "Constituent",
                    PageId = "constituentIndividualPage",
                    ActiveClass = "Constituent",
                    Message = "Your Constituent Page",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = constituentDetail.Id,
                    System = constituentDetail.SystemName,
                    SystemId = constituentDetail.SystemId,
                    Integration = constituentDetail.IntegrationName,
                    IntegrationId = constituentDetail.IntegrationId,
                    IntegrationDate = constituentDetail.IntegrationDate,
                    CreatedDate = constituentDetail.RecordDate,
                    SourceRecordId = constituentDetail.SourceRecordId,
                    CreatedOnDT = constituentDetail.RecordDate,

                    HistoryData = new List<ConstituentHistoryViewModel>(),
                    RecordStatus = constituentDetail.RecordStatus,

                    FirstName = constituentDetail.FirstName,
                    FirstName_OriginalValue = constituentSource.FirstName,
                    FirstName_BusinessName = "First Name",
                    FirstName_BusinessDescription = constituentDetail.FirstName_BusinessDescription,
                    FirstName_AttributeId = constituentDetail.FirstName_AttributeId,
                    FirstName_Category = constituentDetail.FirstName_Category,
                    FirstName_Status = constituentDetail.FirstName_Status,
                    FirstName_Source = constituentDetail.FirstName_Source,
                    FirstName_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.FirstName_Source }),
                    PreferredName = constituentDetail.PreferredName,
                    PreferredName_OriginalValue = constituentSource.PreferredName,
                    PreferredName_BusinessName = "Preferred Name",
                    PreferredName_BusinessDescription = constituentDetail.PreferredName_BusinessDescription,
                    PreferredName_AttributeId = constituentDetail.PreferredName_AttributeId,
                    PreferredName_Category = constituentDetail.PreferredName_Category,
                    PreferredName_Status = constituentDetail.PreferredName_Status,
                    PreferredName_Source = constituentDetail.PreferredName_Source,
                    PreferredName_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.PreferredName_Source }),
                    MiddleName = constituentDetail.MiddleName,
                    MiddleName_OriginalValue = constituentSource.MiddleName,
                    MiddleName_BusinessName = "Middle Name",
                    MiddleName_BusinessDescription = constituentDetail.MiddleName_BusinessDescription,
                    MiddleName_AttributeId = constituentDetail.MiddleName_AttributeId,
                    MiddleName_Category = constituentDetail.MiddleName_Category,
                    MiddleName_Status = constituentDetail.MiddleName_Status,
                    MiddleName_Source = constituentDetail.MiddleName_Source,
                    MiddleName_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.MiddleName_Source }),
                    LastName = constituentDetail.LastName,
                    LastName_OriginalValue = constituentSource.LastName,
                    LastName_BusinessName = "Last Name",
                    LastName_BusinessDescription = constituentDetail.LastName_BusinessDescription,
                    LastName_AttributeId = constituentDetail.LastName_AttributeId,
                    LastName_Category = constituentDetail.LastName_Category,
                    LastName_Status = constituentDetail.LastName_Status,
                    LastName_Source = constituentDetail.LastName_Source,
                    LastName_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.LastName_Source }),
                    MaidenName = constituentDetail.MaidenName,
                    MaidenName_OriginalValue = constituentSource.MaidenName,
                    MaidenName_BusinessName = "Maiden Name",
                    MaidenName_BusinessDescription = constituentDetail.MaidenName_BusinessDescription,
                    MaidenName_AttributeId = constituentDetail.MaidenName_AttributeId,
                    MaidenName_Category = constituentDetail.MaidenName_Category,
                    MaidenName_Status = constituentDetail.MaidenName_Status,
                    MaidenName_Source = constituentDetail.MaidenName_Source,
                    MaidenName_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.MaidenName_Source }),
                    UAPersonId = constituentDetail.UAPersonId,
                    UAPersonId_OriginalValue = constituentSource.UAPersonId,
                    UAPersonId_BusinessName = constituentDetail.UAPersonId_BusinessName,
                    UAPersonId_BusinessDescription = constituentDetail.UAPersonId_BusinessDescription,
                    UAPersonId_AttributeId = constituentDetail.UAPersonId_AttributeId,
                    UAPersonId_Category = constituentDetail.UAPersonId_Category,
                    UAPersonId_Status = constituentDetail.UAPersonId_Status,
                    UAPersonId_Source = constituentDetail.UAPersonId_Source,
                    UAPersonId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.UAPersonId_Source }),
                    Suffix = constituentDetail.Suffix,
                    Suffix_OriginalValue = constituentSource.Suffix,
                    Suffix_BusinessName = constituentDetail.Suffix_BusinessName,
                    Suffix_BusinessDescription = constituentDetail.Suffix_BusinessDescription,
                    Suffix_AttributeId = constituentDetail.Suffix_AttributeId,
                    Suffix_Category = constituentDetail.Suffix_Category,
                    Suffix_Status = constituentDetail.Suffix_Status,
                    Suffix_Source = constituentDetail.Suffix_Source,
                    SuffixSourceSystemRecordId = constituentDetail.SuffixSourceSystemRecordId,
                    SuffixSourceSystemRecordId_OriginalValue = constituentSource.SuffixSourceSystemRecordId,
                    SuffixSourceSystemRecordId_BusinessName = constituentDetail.SuffixSourceSystemRecordId_BusinessName,
                    SuffixSourceSystemRecordId_BusinessDescription = constituentDetail.SuffixSourceSystemRecordId_BusinessDescription,
                    SuffixSourceSystemRecordId_AttributeId = constituentDetail.SuffixSourceSystemRecordId_AttributeId,
                    SuffixSourceSystemRecordId_Category = constituentDetail.SuffixSourceSystemRecordId_Category,
                    SuffixSourceSystemRecordId_Status = constituentDetail.SuffixSourceSystemRecordId_Status,
                    SuffixSourceSystemRecordId_Source = constituentDetail.SuffixSourceSystemRecordId_Source,
                    SuffixMasterId = constituentDetail.SuffixMasterId,
                    SuffixMasterId_OriginalValue = constituentSource.SuffixMasterId,
                    SuffixMasterId_BusinessName = constituentDetail.SuffixMasterId_BusinessName,
                    SuffixMasterId_BusinessDescription = constituentDetail.SuffixMasterId_BusinessDescription,
                    SuffixMasterId_AttributeId = constituentDetail.SuffixMasterId_AttributeId,
                    SuffixMasterId_Category = constituentDetail.SuffixMasterId_Category,
                    SuffixMasterId_Status = constituentDetail.SuffixMasterId_Status,
                    SuffixMasterId_Source = constituentDetail.SuffixMasterId_Source,
                    SuffixMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.SuffixMasterId_Source, constituentDetail.SuffixSourceSystemRecordId_Source }),
                    BirthDate = constituentDetail.BirthDate,
                    BirthDate_OriginalValue = constituentSource.BirthDate,
                    BirthDate_BusinessName = constituentDetail.BirthDate_BusinessName,
                    BirthDate_BusinessDescription = constituentDetail.BirthDate_BusinessDescription,
                    BirthDate_AttributeId = constituentDetail.BirthDate_AttributeId,
                    BirthDate_Category = constituentDetail.BirthDate_Category,
                    BirthDate_Status = constituentDetail.BirthDate_Status,
                    BirthDate_Source = constituentDetail.BirthDate_Source,
                    BirthDate_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.BirthDate_Source }),
                    DeceasedDate = constituentDetail.DeceasedDate,
                    DeceasedDate_OriginalValue = constituentSource.DeceasedDate,
                    DeceasedDate_BusinessName = constituentDetail.DeceasedDate_BusinessName,
                    DeceasedDate_BusinessDescription = constituentDetail.DeceasedDate_BusinessDescription,
                    DeceasedDate_AttributeId = constituentDetail.DeceasedDate_AttributeId,
                    DeceasedDate_Category = constituentDetail.DeceasedDate_Category,
                    DeceasedDate_Status = constituentDetail.DeceasedDate_Status,
                    DeceasedDate_Source = constituentDetail.DeceasedDate_Source,
                    DeceasedDate_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.DeceasedDate_Source }),
                    MaritalStatus = constituentDetail.MaritalStatus,
                    MaritalStatus_OriginalValue = constituentSource.MaritalStatus,
                    MaritalStatus_BusinessName = constituentDetail.MaritalStatus_BusinessName,
                    MaritalStatus_BusinessDescription = constituentDetail.MaritalStatus_BusinessDescription,
                    MaritalStatus_AttributeId = constituentDetail.MaritalStatus_AttributeId,
                    MaritalStatus_Category = constituentDetail.MaritalStatusMasterId_Category,
                    MaritalStatus_Status = constituentDetail.MaritalStatus_Status,
                    MaritalStatus_Source = constituentDetail.MaritalStatus_Source,
                    MaritalStatusSourceSystemRecordId = constituentDetail.MaritalStatusSourceSystemRecordId,
                    MaritalStatusSourceSystemRecordId_OriginalValue = constituentSource.MaritalStatusSourceSystemRecordId,
                    MaritalStatusSourceSystemRecordId_BusinessName = constituentDetail.MaritalStatusSourceSystemRecordId_BusinessName,
                    MaritalStatusSourceSystemRecordId_BusinessDescription = constituentDetail.MaritalStatusSourceSystemRecordId_BusinessDescription,
                    MaritalStatusSourceSystemRecordId_AttributeId = constituentDetail.MaritalStatusSourceSystemRecordId_AttributeId,
                    MaritalStatusSourceSystemRecordId_Category = constituentDetail.MaritalStatusSourceSystemRecordId_Category,
                    MaritalStatusSourceSystemRecordId_Status = constituentDetail.MaritalStatusSourceSystemRecordId_Status,
                    MaritalStatusSourceSystemRecordId_Source = constituentDetail.MaritalStatusSourceSystemRecordId_Source,
                    MaritalStatusMasterId = constituentDetail.MaritalStatusMasterId,
                    MaritalStatusMasterId_OriginalValue = constituentSource.MaritalStatusMasterId,
                    MaritalStatusMasterId_BusinessName = constituentDetail.MaritalStatusMasterId_BusinessName,
                    MaritalStatusMasterId_BusinessDescription = constituentDetail.MaritalStatusMasterId_BusinessDescription,
                    MaritalStatusMasterId_AttributeId = constituentDetail.MaritalStatusMasterId_AttributeId,
                    MaritalStatusMasterId_Category = constituentDetail.MaritalStatusMasterId_Category,
                    MaritalStatusMasterId_Status = constituentDetail.MaritalStatusMasterId_Status,
                    MaritalStatusMasterId_Source = constituentDetail.MaritalStatusMasterId_Source,
                    MaritalStatusMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.MaritalStatusMasterId_Source, constituentDetail.MaritalStatusSourceSystemRecordId_Source, constituentDetail.MaritalStatus_Source }),
                    Address = constituentDetail.Address,
                    Address_OriginalValue = constituentSource.Address,
                    Address_BusinessName = constituentDetail.Address_BusinessName,
                    Address_BusinessDescription = constituentDetail.Address_BusinessDescription,
                    Address_AttributeId = constituentDetail.Address_AttributeId,
                    Address_Category = constituentDetail.Address_Category,
                    Address_Status = constituentDetail.Address_Status,
                    Address_Source = constituentDetail.Address_Source,
                    AddressSourceSystemRecordId = constituentDetail.AddressSourceSystemRecordId,
                    AddressMasterId = constituentDetail.AddressMasterId,
                    AddressMasterId_OriginalValue = constituentSource.AddressMasterId,
                    AddressMasterId_BusinessName = constituentDetail.AddressMasterId_BusinessName,
                    AddressMasterId_BusinessDescription = constituentDetail.AddressMasterId_BusinessDescription,
                    AddressMasterId_AttributeId = constituentDetail.AddressMasterId_AttributeId,
                    AddressMasterId_Category = constituentDetail.AddressMasterId_Category,
                    AddressMasterId_Status = constituentDetail.AddressMasterId_Status,
                    AddressMasterId_Source = constituentDetail.AddressMasterId_Source,
                    AddressMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.AddressMasterId_Source, constituentDetail.Address_Source }),
                    City = constituentDetail.City,
                    City_OriginalValue = constituentSource.City,
                    City_BusinessName = constituentDetail.City_BusinessName,
                    City_BusinessDescription = constituentDetail.City_BusinessDescription,
                    City_AttributeId = constituentDetail.City_AttributeId,
                    City_Category = constituentDetail.City_Category,
                    City_Status = constituentDetail.City_Status,
                    City_Source = constituentDetail.City_Source,
                    PostalCode = constituentDetail.PostalCode,
                    PostalCode_OriginalValue = constituentSource.PostalCode,
                    PostalCode_BusinessName = constituentDetail.PostalCode_BusinessName,
                    PostalCode_BusinessDescription = constituentDetail.PostalCode_BusinessDescription,
                    PostalCode_AttributeId = constituentDetail.PostalCode_AttributeId,
                    PostalCode_Category = constituentDetail.PostalCode_Category,
                    PostalCode_Status = constituentDetail.PostalCode_Status,
                    PostalCode_Source = constituentDetail.PostalCode_Source,
                    State = constituentDetail.State,
                    State_OriginalValue = constituentSource.State,
                    State_BusinessName = constituentDetail.State_BusinessName,
                    State_BusinessDescription = constituentDetail.State_BusinessDescription,
                    State_AttributeId = constituentDetail.State_AttributeId,
                    State_Category = constituentDetail.State_Category,
                    State_Status = constituentDetail.State_Status,
                    State_Source = constituentDetail.State_Source,
                    StateSourceSystemRecordId = constituentDetail.StateSourceSystemRecordId,
                    StateSourceSystemRecordId_OriginalValue = constituentSource.StateSourceSystemRecordId,
                    StateSourceSystemRecordId_BusinessName = constituentDetail.StateSourceSystemRecordId_BusinessName,
                    StateSourceSystemRecordId_BusinessDescription = constituentDetail.StateSourceSystemRecordId_BusinessDescription,
                    StateSourceSystemRecordId_AttributeId = constituentDetail.StateSourceSystemRecordId_AttributeId,
                    StateSourceSystemRecordId_Category = constituentDetail.StateSourceSystemRecordId_Category,
                    StateSourceSystemRecordId_Status = constituentDetail.StateSourceSystemRecordId_Status,
                    StateSourceSystemRecordId_Source = constituentDetail.StateSourceSystemRecordId_Source,
                    StateMasterId = constituentDetail.StateMasterId,
                    StateMasterId_OriginalValue = constituentSource.StateMasterId,
                    StateMasterId_BusinessName = constituentDetail.StateMasterId_BusinessName,
                    StateMasterId_BusinessDescription = constituentDetail.StateMasterId_BusinessDescription,
                    StateMasterId_AttributeId = constituentDetail.StateMasterId_AttributeId,
                    StateMasterId_Category = constituentDetail.StateMasterId_Category,
                    StateMasterId_Status = constituentDetail.StateMasterId_Status,
                    StateMasterId_Source = constituentDetail.StateMasterId_Source,
                    StateMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.StateMasterId_Source, constituentDetail.StateSourceSystemRecordId_Source, constituentDetail.State_Source }),
                    Country = constituentDetail.Country,
                    Country_OriginalValue = constituentSource.Country,
                    Country_BusinessName = constituentDetail.Country_BusinessName,
                    Country_BusinessDescription = constituentDetail.Country_BusinessDescription,
                    Country_AttributeId = constituentDetail.Country_AttributeId,
                    Country_Category = constituentDetail.Country_Category,
                    Country_Status = constituentDetail.Country_Status,
                    Country_Source = constituentDetail.Country_Source,
                    CountrySourceSystemRecordId = constituentDetail.CountrySourceSystemRecordId,
                    CountrySourceSystemRecordId_OriginalValue = constituentSource.CountrySourceSystemRecordId,
                    CountrySourceSystemRecordId_BusinessName = constituentDetail.CountrySourceSystemRecordId_BusinessName,
                    CountrySourceSystemRecordId_BusinessDescription = constituentDetail.CountrySourceSystemRecordId_BusinessDescription,
                    CountrySourceSystemRecordId_AttributeId = constituentDetail.CountrySourceSystemRecordId_AttributeId,
                    CountrySourceSystemRecordId_Category = constituentDetail.CountrySourceSystemRecordId_Category,
                    CountrySourceSystemRecordId_Status = constituentDetail.CountrySourceSystemRecordId_Status,
                    CountrySourceSystemRecordId_Source = constituentDetail.CountrySourceSystemRecordId_Source,
                    CountryMasterId = constituentDetail.CountryMasterId,
                    CountryMasterId_OriginalValue = constituentSource.CountryMasterId,
                    CountryMasterId_BusinessName = constituentDetail.CountryMasterId_BusinessName,
                    CountryMasterId_BusinessDescription = constituentDetail.CountryMasterId_BusinessDescription,
                    CountryMasterId_AttributeId = constituentDetail.CountryMasterId_AttributeId,
                    CountryMasterId_Category = constituentDetail.CountryMasterId_Category,
                    CountryMasterId_Status = constituentDetail.CountryMasterId_Status,
                    CountryMasterId_Source = constituentDetail.CountryMasterId_Source,
                    CountryMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.CountryMasterId_Source, constituentDetail.CountrySourceSystemRecordId_Source, constituentDetail.Country_Source }),
                    AddressUseType = constituentDetail.AddressUseType,
                    AddressUseType_OriginalValue = constituentSource.AddressUseType,
                    AddressUseType_BusinessName = constituentDetail.AddressUseType_BusinessName,
                    AddressUseType_BusinessDescription = constituentDetail.AddressUseType_BusinessDescription,
                    AddressUseType_AttributeId = constituentDetail.AddressUseType_AttributeId,
                    AddressUseType_Category = constituentDetail.AddressUseType_Category,
                    AddressUseType_Status = constituentDetail.AddressUseType_Status,
                    AddressUseType_Source = constituentDetail.AddressUseType_Source,
                    AddressUseTypeSourceSystemRecordId = constituentDetail.AddressUseTypeSourceSystemRecordId,
                    AddressUseTypeSourceSystemRecordId_OriginalValue = constituentSource.AddressUseTypeSourceSystemRecordId,
                    AddressUseTypeSourceSystemRecordId_BusinessName = constituentDetail.AddressUseTypeSourceSystemRecordId_BusinessName,
                    AddressUseTypeSourceSystemRecordId_BusinessDescription = constituentDetail.AddressUseTypeSourceSystemRecordId_BusinessDescription,
                    AddressUseTypeSourceSystemRecordId_AttributeId = constituentDetail.AddressUseTypeSourceSystemRecordId_AttributeId,
                    AddressUseTypeSourceSystemRecordId_Category = constituentDetail.AddressUseTypeSourceSystemRecordId_Category,
                    AddressUseTypeSourceSystemRecordId_Status = constituentDetail.AddressUseTypeSourceSystemRecordId_Status,
                    AddressUseTypeSourceSystemRecordId_Source = constituentDetail.AddressUseTypeSourceSystemRecordId_Source,
                    AddressUseTypeMasterId = constituentDetail.AddressUseTypeMasterId,
                    AddressUseTypeMasterId_OriginalValue = constituentSource.AddressUseTypeMasterId,
                    AddressUseTypeMasterId_BusinessName = constituentDetail.AddressUseTypeMasterId_BusinessName,
                    AddressUseTypeMasterId_BusinessDescription = constituentDetail.AddressUseTypeMasterId_BusinessDescription,
                    AddressUseTypeMasterId_AttributeId = constituentDetail.AddressUseTypeMasterId_AttributeId,
                    AddressUseTypeMasterId_Category = constituentDetail.AddressUseTypeMasterId_Category,
                    AddressUseTypeMasterId_Status = constituentDetail.AddressUseTypeMasterId_Status,
                    AddressUseTypeMasterId_Source = constituentDetail.AddressUseTypeMasterId_Source,
                    AddressUseTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.AddressUseTypeMasterId_Source, constituentDetail.AddressUseTypeSourceSystemRecordId_Source, constituentDetail.AddressUseType_Source }),
                    AddressIsPrimary = constituentDetail.AddressIsPrimary,
                    AddressIsPrimary_OriginalValue = constituentSource.AddressIsPrimary.ToString(),
                    AddressIsPrimary_BusinessName = constituentDetail.AddressIsPrimary_BusinessName,
                    AddressIsPrimary_BusinessDescription = constituentDetail.AddressIsPrimary_BusinessDescription,
                    AddressIsPrimary_AttributeId = constituentDetail.AddressIsPrimary_AttributeId,
                    AddressIsPrimary_Category = constituentDetail.AddressIsPrimary_Category,
                    AddressIsPrimary_Status = constituentDetail.AddressIsPrimary_Status,
                    AddressIsPrimary_Source = constituentDetail.AddressIsPrimary_Source,
                    EmailAddress = constituentDetail.EmailAddress,
                    EmailAddress_OriginalValue = constituentSource.EmailAddress,
                    EmailAddress_BusinessName = constituentDetail.EmailAddress_BusinessName,
                    EmailAddress_BusinessDescription = constituentDetail.EmailAddress_BusinessDescription,
                    EmailAddress_AttributeId = constituentDetail.EmailAddress_AttributeId,
                    EmailAddress_Category = constituentDetail.EmailAddress_Category,
                    EmailAddress_Status = constituentDetail.EmailAddress_Status,
                    EmailAddress_Source = constituentDetail.EmailAddress_Source,
                    EmailAddressSourceSystemRecordId = constituentDetail.EmailAddressSourceSystemRecordId,
                    EmailAddressMasterId = constituentDetail.EmailAddressMasterId,
                    EmailAddressMasterId_OriginalValue = constituentSource.EmailAddressMasterId,
                    EmailAddressMasterId_BusinessName = constituentDetail.EmailAddressMasterId_BusinessName,
                    EmailAddressMasterId_BusinessDescription = constituentDetail.EmailAddressMasterId_BusinessDescription,
                    EmailAddressMasterId_AttributeId = constituentDetail.EmailAddressMasterId_AttributeId,
                    EmailAddressMasterId_Category = constituentDetail.EmailAddressMasterId_Category,
                    EmailAddressMasterId_Status = constituentDetail.EmailAddressMasterId_Status,
                    EmailAddressMasterId_Source = constituentDetail.EmailAddressMasterId_Source,
                    EmailAddressMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.EmailAddressMasterId_Source, constituentDetail.EmailAddressMasterId_Source, constituentDetail.EmailAddress_Source }),
                    EmailAddressUseType = constituentDetail.EmailAddressUseType,
                    EmailAddressUseType_OriginalValue = constituentSource.EmailAddressUseType,
                    EmailAddressUseType_BusinessName = constituentDetail.EmailAddressUseType_BusinessName,
                    EmailAddressUseType_BusinessDescription = constituentDetail.EmailAddressUseType_BusinessDescription,
                    EmailAddressUseType_AttributeId = constituentDetail.EmailAddressUseType_AttributeId,
                    EmailAddressUseType_Category = constituentDetail.EmailAddressUseType_Category,
                    EmailAddressUseType_Status = constituentDetail.EmailAddressUseType_Status,
                    EmailAddressUseType_Source = constituentDetail.EmailAddressUseType_Source,
                    EmailAddressUseTypeSourceSystemRecordId = constituentDetail.EmailAddressUseTypeSourceSystemRecordId,
                    EmailAddressUseTypeSourceSystemRecordId_OriginalValue = constituentSource.EmailAddressUseTypeSourceSystemRecordId,
                    EmailAddressUseTypeSourceSystemRecordId_BusinessName = constituentDetail.EmailAddressUseTypeSourceSystemRecordId_BusinessName,
                    EmailAddressUseTypeSourceSystemRecordId_BusinessDescription = constituentDetail.EmailAddressUseTypeSourceSystemRecordId_BusinessDescription,
                    EmailAddressUseTypeSourceSystemRecordId_AttributeId = constituentDetail.EmailAddressUseTypeSourceSystemRecordId_AttributeId,
                    EmailAddressUseTypeSourceSystemRecordId_Category = constituentDetail.EmailAddressUseTypeSourceSystemRecordId_Category,
                    EmailAddressUseTypeSourceSystemRecordId_Status = constituentDetail.EmailAddressUseTypeSourceSystemRecordId_Status,
                    EmailAddressUseTypeSourceSystemRecordId_Source = constituentDetail.EmailAddressUseTypeSourceSystemRecordId_Source,
                    EmailAddressUseTypeMasterId = constituentDetail.EmailAddressUseTypeMasterId,
                    EmailAddressUseTypeMasterId_OriginalValue = constituentSource.EmailAddressUseTypeMasterId,
                    EmailAddressUseTypeMasterId_BusinessName = constituentDetail.EmailAddressUseTypeMasterId_BusinessName,
                    EmailAddressUseTypeMasterId_BusinessDescription = constituentDetail.EmailAddressUseTypeMasterId_BusinessDescription,
                    EmailAddressUseTypeMasterId_AttributeId = constituentDetail.EmailAddressUseTypeMasterId_AttributeId,
                    EmailAddressUseTypeMasterId_Category = constituentDetail.EmailAddressUseTypeMasterId_Category,
                    EmailAddressUseTypeMasterId_Status = constituentDetail.EmailAddressUseTypeMasterId_Status,
                    EmailAddressUseTypeMasterId_Source = constituentDetail.EmailAddressUseTypeMasterId_Source,
                    EmailAddressUseTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.EmailAddressUseTypeMasterId_Source, constituentDetail.EmailAddressUseTypeSourceSystemRecordId_Source, constituentDetail.EmailAddressUseType_Source }),
                    EmailIsPrimary = constituentDetail.EmailIsPrimary,
                    EmailIsPrimary_OriginalValue = constituentSource.EmailIsPrimary.ToString(),
                    EmailIsPrimary_BusinessName = constituentDetail.EmailIsPrimary_BusinessName,
                    EmailIsPrimary_BusinessDescription = constituentDetail.EmailIsPrimary_BusinessDescription,
                    EmailIsPrimary_AttributeId = constituentDetail.EmailIsPrimary_AttributeId,
                    EmailIsPrimary_Category = constituentDetail.EmailIsPrimary_Category,
                    EmailIsPrimary_Status = constituentDetail.EmailIsPrimary_Status,
                    EmailIsPrimary_Source = constituentDetail.EmailIsPrimary_Source,
                    PhoneNumber = constituentDetail.PhoneNumber,
                    PhoneNumber_OriginalValue = constituentSource.PhoneNumber,
                    PhoneNumber_BusinessName = constituentDetail.PhoneNumber_BusinessName,
                    PhoneNumber_BusinessDescription = constituentDetail.PhoneNumber_BusinessDescription,
                    PhoneNumber_AttributeId = constituentDetail.PhoneNumber_AttributeId,
                    PhoneNumber_Category = constituentDetail.PhoneNumber_Category,
                    PhoneNumber_Status = constituentDetail.PhoneNumber_Status,
                    PhoneNumber_Source = constituentDetail.PhoneNumber_Source,
                    PhoneNumberSourceSystemRecordId = constituentDetail.PhoneNumberSourceSystemRecordId,
                    PhoneExtension = constituentDetail.PhoneExtension,
                    PhoneExtension_OriginalValue = constituentSource.PhoneExtension,
                    PhoneExtension_BusinessName = constituentDetail.PhoneExtension_BusinessName,
                    PhoneExtension_BusinessDescription = constituentDetail.PhoneExtension_BusinessDescription,
                    PhoneExtension_AttributeId = constituentDetail.PhoneExtension_AttributeId,
                    PhoneExtension_Category = constituentDetail.PhoneExtension_Category,
                    PhoneExtension_Status = constituentDetail.PhoneExtension_Status,
                    PhoneExtension_Source = constituentDetail.PhoneExtension_Source,
                    PhoneCountryCode = constituentDetail.PhoneCountryCode,
                    PhoneCountryCode_OriginalValue = constituentSource.PhoneCountryCode,
                    PhoneCountryCode_BusinessName = constituentDetail.PhoneCountryCode_BusinessName,
                    PhoneCountryCode_BusinessDescription = constituentDetail.PhoneCountryCode_BusinessDescription,
                    PhoneCountryCode_AttributeId = constituentDetail.PhoneCountryCode_AttributeId,
                    PhoneCountryCode_Category = constituentDetail.PhoneCountryCode_Category,
                    PhoneCountryCode_Status = constituentDetail.PhoneCountryCode_Status,
                    PhoneCountryCode_Source = constituentDetail.PhoneCountryCode_Source,
                    PhoneCountrySourceSystemRecordId = constituentDetail.PhoneCountrySourceSystemRecordId,
                    PhoneCountrySourceSystemRecordId_OriginalValue = constituentSource.PhoneCountrySourceSystemRecordId,
                    PhoneCountrySourceSystemRecordId_BusinessName = constituentDetail.PhoneCountrySourceSystemRecordId_BusinessName,
                    PhoneCountrySourceSystemRecordId_BusinessDescription = constituentDetail.PhoneCountrySourceSystemRecordId_BusinessDescription,
                    PhoneCountrySourceSystemRecordId_AttributeId = constituentDetail.PhoneCountrySourceSystemRecordId_AttributeId,
                    PhoneCountrySourceSystemRecordId_Category = constituentDetail.PhoneCountrySourceSystemRecordId_Category,
                    PhoneCountrySourceSystemRecordId_Status = constituentDetail.PhoneCountrySourceSystemRecordId_Status,
                    PhoneCountrySourceSystemRecordId_Source = constituentDetail.PhoneCountrySourceSystemRecordId_Source,
                    PhoneCountryMasterRecordId = constituentDetail.PhoneCountryMasterRecordId,
                    PhoneCountryMasterRecordId_OriginalValue = constituentSource.PhoneCountryMasterRecordId,
                    PhoneCountryMasterRecordId_BusinessName = constituentDetail.PhoneCountryMasterRecordId_BusinessName,
                    PhoneCountryMasterRecordId_BusinessDescription = constituentDetail.PhoneCountryMasterRecordId_BusinessDescription,
                    PhoneCountryMasterRecordId_AttributeId = constituentDetail.PhoneCountryMasterRecordId_AttributeId,
                    PhoneCountryMasterRecordId_Category = constituentDetail.PhoneCountryMasterRecordId_Category,
                    PhoneCountryMasterRecordId_Status = constituentDetail.PhoneCountryMasterRecordId_Status,
                    PhoneCountryMasterRecordId_Source = constituentDetail.PhoneCountryMasterRecordId_Source,
                    PhoneCountryMasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.PhoneCountryMasterRecordId_Source, constituentDetail.PhoneCountrySourceSystemRecordId_Source, constituentDetail.PhoneCountryCode_Source }),
                    PhoneLineType = constituentDetail.PhoneLineType,
                    PhoneLineType_OriginalValue = constituentSource.PhoneLineType,
                    PhoneLineType_BusinessName = constituentDetail.PhoneLineType_BusinessName,
                    PhoneLineType_BusinessDescription = constituentDetail.PhoneLineType_BusinessDescription,
                    PhoneLineType_AttributeId = constituentDetail.PhoneLineType_AttributeId,
                    PhoneLineType_Category = constituentDetail.PhoneLineType_Category,
                    PhoneLineType_Status = constituentDetail.PhoneLineType_Status,
                    PhoneLineType_Source = constituentDetail.PhoneLineType_Source,
                    PhoneLineTypeSourceSystemRecordId = constituentDetail.PhoneLineTypeSourceSystemRecordId,
                    PhoneLineTypeSourceSystemRecordId_OriginalValue = constituentSource.PhoneLineTypeSourceSystemRecordId,
                    PhoneLineTypeSourceSystemRecordId_BusinessName = constituentDetail.PhoneLineTypeSourceSystemRecordId_BusinessName,
                    PhoneLineTypeSourceSystemRecordId_BusinessDescription = constituentDetail.PhoneLineTypeSourceSystemRecordId_BusinessDescription,
                    PhoneLineTypeSourceSystemRecordId_AttributeId = constituentDetail.PhoneLineTypeSourceSystemRecordId_AttributeId,
                    PhoneLineTypeSourceSystemRecordId_Category = constituentDetail.PhoneLineTypeSourceSystemRecordId_Category,
                    PhoneLineTypeSourceSystemRecordId_Status = constituentDetail.PhoneLineTypeSourceSystemRecordId_Status,
                    PhoneLineTypeSourceSystemRecordId_Source = constituentDetail.PhoneLineTypeSourceSystemRecordId_Source,
                    PhoneLineTypeMasterRecordId = constituentDetail.PhoneLineTypeMasterRecordId,
                    PhoneLineTypeMasterRecordId_OriginalValue = constituentSource.PhoneLineTypeMasterRecordId,
                    PhoneLineTypeMasterRecordId_BusinessName = constituentDetail.PhoneLineTypeMasterRecordId_BusinessName,
                    PhoneLineTypeMasterRecordId_BusinessDescription = constituentDetail.PhoneLineTypeMasterRecordId_BusinessDescription,
                    PhoneLineTypeMasterRecordId_AttributeId = constituentDetail.PhoneLineTypeMasterRecordId_AttributeId,
                    PhoneLineTypeMasterRecordId_Category = constituentDetail.PhoneLineTypeMasterRecordId_Category,
                    PhoneLineTypeMasterRecordId_Status = constituentDetail.PhoneLineTypeMasterRecordId_Status,
                    PhoneLineTypeMasterRecordId_Source = constituentDetail.PhoneLineTypeMasterRecordId_Source,
                    PhoneLineTypeMasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.PhoneLineTypeMasterRecordId_Source, constituentDetail.PhoneLineTypeSourceSystemRecordId_Source, constituentDetail.PhoneLineType_Source }),
                    PhoneUseType = constituentDetail.PhoneUseType,
                    PhoneUseType_OriginalValue = constituentSource.PhoneUseType,
                    PhoneUseType_BusinessName = constituentDetail.PhoneUseType_BusinessName,
                    PhoneUseType_BusinessDescription = constituentDetail.PhoneUseType_BusinessDescription,
                    PhoneUseType_AttributeId = constituentDetail.PhoneUseType_AttributeId,
                    PhoneUseType_Category = constituentDetail.PhoneUseType_Category,
                    PhoneUseType_Status = constituentDetail.PhoneUseType_Status,
                    PhoneUseType_Source = constituentDetail.PhoneUseType_Source,
                    PhoneUseTypeSourceSystemRecordId = constituentDetail.PhoneUseTypeSourceSystemRecordId,
                    PhoneUseTypeSourceSystemRecordId_OriginalValue = constituentSource.PhoneUseTypeSourceSystemRecordId,
                    PhoneUseTypeSourceSystemRecordId_BusinessName = constituentDetail.PhoneUseTypeSourceSystemRecordId_BusinessName,
                    PhoneUseTypeSourceSystemRecordId_BusinessDescription = constituentDetail.PhoneUseTypeSourceSystemRecordId_BusinessDescription,
                    PhoneUseTypeSourceSystemRecordId_AttributeId = constituentDetail.PhoneUseTypeSourceSystemRecordId_AttributeId,
                    PhoneUseTypeSourceSystemRecordId_Category = constituentDetail.PhoneUseTypeSourceSystemRecordId_Category,
                    PhoneUseTypeSourceSystemRecordId_Status = constituentDetail.PhoneUseTypeSourceSystemRecordId_Status,
                    PhoneUseTypeSourceSystemRecordId_Source = constituentDetail.PhoneUseTypeSourceSystemRecordId_Source,
                    PhoneUseTypeMasterId = constituentDetail.PhoneUseTypeMasterId,
                    PhoneUseTypeMasterId_OriginalValue = constituentSource.PhoneUseTypeMasterId,
                    PhoneUseTypeMasterId_BusinessName = constituentDetail.PhoneUseTypeMasterId_BusinessName,
                    PhoneUseTypeMasterId_BusinessDescription = constituentDetail.PhoneUseTypeMasterId_BusinessDescription,
                    PhoneUseTypeMasterId_AttributeId = constituentDetail.PhoneUseTypeMasterId_AttributeId,
                    PhoneUseTypeMasterId_Category = constituentDetail.PhoneUseTypeMasterId_Category,
                    PhoneUseTypeMasterId_Status = constituentDetail.PhoneUseTypeMasterId_Status,
                    PhoneUseTypeMasterId_Source = constituentDetail.PhoneUseTypeMasterId_Source,
                    PhoneUseTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { constituentDetail.PhoneUseTypeMasterId_Source, constituentDetail.PhoneUseTypeSourceSystemRecordId_Source, constituentDetail.PhoneUseType_Source }),
                    PhoneMasterId = constituentDetail.PhoneMasterId,
                    PhoneMasterId_OriginalValue = constituentSource.PhoneMasterId,
                    PhoneMasterId_BusinessName = constituentDetail.PhoneMasterId_BusinessName,
                    PhoneMasterId_BusinessDescription = constituentDetail.PhoneMasterId_BusinessDescription,
                    PhoneMasterId_AttributeId = constituentDetail.PhoneMasterId_AttributeId,
                    PhoneMasterId_Category = constituentDetail.PhoneMasterId_Category,
                    PhoneMasterId_Status = constituentDetail.PhoneMasterId_Status,
                    PhoneMasterId_Source = constituentDetail.PhoneMasterId_Source,
                    PhoneIsPrimary = constituentDetail.PhoneIsPrimary,
                    PhoneIsPrimary_OriginalValue = constituentSource.PhoneIsPrimary.ToString(),
                    PhoneIsPrimary_BusinessName = constituentDetail.PhoneIsPrimary_BusinessName,
                    PhoneIsPrimary_BusinessDescription = constituentDetail.PhoneIsPrimary_BusinessDescription,
                    PhoneIsPrimary_AttributeId = constituentDetail.PhoneIsPrimary_AttributeId,
                    PhoneIsPrimary_Category = constituentDetail.PhoneIsPrimary_Category,
                    PhoneIsPrimary_Status = constituentDetail.PhoneIsPrimary_Status,
                    PhoneIsPrimary_Source = constituentDetail.PhoneIsPrimary_Source,

                    TitleList = GetTitleList(),
                    SuffixList = GetSuffixList(),
                    CountryList = GetCountryList(),
                    StateList = GetStateList(),
                    MaritalStatusList = GetMaritalStatusList(),
                    PhoneLineTypeList = GetPhoneLineTypeList(),
                    PhoneUseTypeList = GetPhoneUseTypeList(),
                    EmailAddressUseTypeList = GetEmailAddressUseTypeList(),
                    AddressUseTypeList = GetAddressUseTypeList(),

                };

                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);

                    viewModel.HistoryData.Add(new ConstituentHistoryViewModel()
                    {
                        FirstName = item.FirstName,
                        FirstName_Status = item.FirstName_Status,
                        FirstName_OriginalValue = previousitem.FirstName,
                        PreferredName = item.PreferredName,
                        PreferredName_Status = item.PreferredName_Status,
                        PreferredName_OriginalValue = previousitem.PreferredName,
                        MiddleName = item.MiddleName,
                        MiddleName_Status = item.MiddleName_Status,
                        MiddleName_OriginalValue = previousitem.MiddleName,
                        LastName = item.LastName,
                        LastName_Status = item.LastName_Status,
                        LastName_OriginalValue = previousitem.LastName,
                        MaidenName = item.MaidenName,
                        MaidenName_Status = item.MaidenName_Status,
                        MaidenName_OriginalValue = previousitem.MaidenName,
                        UAPersonId = item.UAPersonId,
                        UAPersonId_Status = item.UAPersonId_Status,
                        UAPersonId_OriginalValue = previousitem.UAPersonId,
                        //ConstituentTitle = item.Title,
                        //ConstituentTitle_Status = item.Title_Status,
                        //ConstituentTitle_OriginalValue = previousitem.Title,
                        //TitleSourceSystemRecordId = item.TitleSourceSystemRecordId,
                        //TitleSourceSystemRecordId_Status = item.TitleSourceSystemRecordId_Status,
                        //TitleSourceSystemRecordId_OriginalValue = previousitem.TitleSourceSystemRecordId,
                        //TitleMasterId = item.TitleMasterId,
                        //TitleMasterId_Status = item.TitleMasterId_Status,
                        //TitleMasterId_OriginalValue = previousitem.TitleMasterId,
                        Suffix = item.Suffix,
                        Suffix_Status = item.Suffix_Status,
                        Suffix_OriginalValue = previousitem.Suffix,
                        SuffixSourceSystemRecordId = item.SuffixSourceSystemRecordId,
                        SuffixSourceSystemRecordId_Status = item.SuffixSourceSystemRecordId_Status,
                        SuffixSourceSystemRecordId_OriginalValue = previousitem.SuffixSourceSystemRecordId,
                        SuffixMasterId = item.SuffixMasterId,
                        SuffixMasterId_Status = item.SuffixMasterId_Status,
                        SuffixMasterId_OriginalValue = previousitem.SuffixMasterId,
                        BirthDate = item.BirthDate,
                        BirthDate_Status = item.BirthDate_Status,
                        BirthDate_OriginalValue = previousitem.BirthDate,
                        DeceasedDate = item.DeceasedDate,
                        DeceasedDate_Status = item.DeceasedDate_Status,
                        DeceasedDate_OriginalValue = previousitem.DeceasedDate,
                        MaritalStatus = item.MaritalStatus,
                        MaritalStatus_Status = item.MaritalStatus_Status,
                        MaritalStatus_OriginalValue = previousitem.MaritalStatus,
                        MaritalStatusSourceSystemRecordId = item.MaritalStatusSourceSystemRecordId,
                        MaritalStatusSourceSystemRecordId_Status = item.MaritalStatusSourceSystemRecordId_Status,
                        MaritalStatusSourceSystemRecordId_OriginalValue = previousitem.MaritalStatusSourceSystemRecordId,
                        MaritalStatusMasterId = item.MaritalStatusMasterId,
                        MaritalStatusMasterId_Status = item.MaritalStatusMasterId_Status,
                        MaritalStatusMasterId_OriginalValue = previousitem.MaritalStatusMasterId,
                        Address = item.Address,
                        Address_Status = item.Address_Status,
                        Address_OriginalValue = previousitem.Address,
                        AddressMasterId = item.AddressMasterId,
                        AddressMasterId_Status = item.AddressMasterId_Status,
                        AddressMasterId_OriginalValue = previousitem.AddressMasterId,
                        City = item.City,
                        City_Status = item.City_Status,
                        City_OriginalValue = previousitem.City,
                        PostalCode = item.PostalCode,
                        PostalCode_Status = item.PostalCode_Status,
                        PostalCode_OriginalValue = previousitem.PostalCode,
                        State = item.State,
                        State_Status = item.State_Status,
                        State_OriginalValue = previousitem.State,
                        StateSourceSystemRecordId = item.StateSourceSystemRecordId,
                        StateSourceSystemRecordId_Status = item.StateSourceSystemRecordId_Status,
                        StateSourceSystemRecordId_OriginalValue = previousitem.StateSourceSystemRecordId,
                        StateMasterId = item.StateMasterId,
                        StateMasterId_Status = item.StateMasterId_Status,
                        StateMasterId_OriginalValue = previousitem.StateMasterId,
                        Country = item.Country,
                        Country_Status = item.Country_Status,
                        Country_OriginalValue = previousitem.Country,
                        CountrySourceSystemRecordId = item.CountrySourceSystemRecordId,
                        CountrySourceSystemRecordId_Status = item.CountrySourceSystemRecordId_Status,
                        CountrySourceSystemRecordId_OriginalValue = previousitem.CountrySourceSystemRecordId,
                        CountryMasterId = item.CountryMasterId,
                        CountryMasterId_Status = item.CountryMasterId_Status,
                        CountryMasterId_OriginalValue = previousitem.CountryMasterId,
                        AddressUseType = item.AddressUseType,
                        AddressUseType_Status = item.AddressUseType_Status,
                        AddressUseType_OriginalValue = previousitem.AddressUseType,
                        AddressUseTypeSourceSystemRecordId = item.AddressUseTypeSourceSystemRecordId,
                        AddressUseTypeSourceSystemRecordId_Status = item.AddressUseTypeSourceSystemRecordId_Status,
                        AddressUseTypeSourceSystemRecordId_OriginalValue = previousitem.AddressUseTypeSourceSystemRecordId,
                        AddressUseTypeMasterId = item.AddressUseTypeMasterId,
                        AddressUseTypeMasterId_Status = item.AddressUseTypeMasterId_Status,
                        AddressUseTypeMasterId_OriginalValue = previousitem.AddressUseTypeMasterId,
                        AddressIsPrimary = item.AddressIsPrimary,
                        AddressIsPrimary_Status = item.AddressIsPrimary_Status,
                        AddressIsPrimary_OriginalValue = previousitem.AddressIsPrimary.ToString(),
                        EmailAddress = item.EmailAddress,
                        EmailAddress_Status = item.EmailAddress_Status,
                        EmailAddress_OriginalValue = previousitem.EmailAddress,
                        EmailAddressMasterId = item.EmailAddressMasterId,
                        EmailAddressMasterId_Status = item.EmailAddressMasterId_Status,
                        EmailAddressMasterId_OriginalValue = previousitem.EmailAddressMasterId,
                        EmailAddressUseType = item.EmailAddressUseType,
                        EmailAddressUseType_Status = item.EmailAddressUseType_Status,
                        EmailAddressUseType_OriginalValue = previousitem.EmailAddressUseType,
                        EmailAddressUseTypeSourceSystemRecordId = item.EmailAddressUseTypeSourceSystemRecordId,
                        EmailAddressUseTypeSourceSystemRecordId_Status = item.EmailAddressUseTypeSourceSystemRecordId_Status,
                        EmailAddressUseTypeSourceSystemRecordId_OriginalValue = previousitem.EmailAddressUseTypeSourceSystemRecordId,
                        EmailAddressUseTypeMasterId = item.EmailAddressUseTypeMasterId,
                        EmailAddressUseTypeMasterId_Status = item.EmailAddressUseTypeMasterId_Status,
                        EmailAddressUseTypeMasterId_OriginalValue = previousitem.EmailAddressUseTypeMasterId,
                        EmailIsPrimary = item.EmailIsPrimary,
                        EmailIsPrimary_Status = item.EmailIsPrimary_Status,
                        EmailIsPrimary_OriginalValue = previousitem.EmailIsPrimary.ToString(),
                        PhoneNumber = item.PhoneNumber,
                        PhoneNumber_Status = item.PhoneNumber_Status,
                        PhoneNumber_OriginalValue = previousitem.PhoneNumber,
                        PhoneExtension = item.PhoneExtension,
                        PhoneExtension_Status = item.PhoneExtension_Status,
                        PhoneExtension_OriginalValue = previousitem.PhoneExtension,
                        PhoneCountryCode = item.PhoneCountryCode,
                        PhoneCountryCode_Status = item.PhoneCountryCode_Status,
                        PhoneCountryCode_OriginalValue = previousitem.PhoneCountryCode,
                        PhoneCountrySourceSystemRecordId = item.PhoneCountrySourceSystemRecordId,
                        PhoneCountrySourceSystemRecordId_Status = item.PhoneCountrySourceSystemRecordId_Status,
                        PhoneCountrySourceSystemRecordId_OriginalValue = previousitem.PhoneCountrySourceSystemRecordId,
                        PhoneCountryMasterId = item.PhoneCountryMasterRecordId,
                        PhoneCountryMasterId_Status = item.PhoneCountryMasterRecordId_Status,
                        PhoneCountryMasterId_OriginalValue = previousitem.PhoneCountryMasterRecordId,
                        PhoneLineType = item.PhoneLineType,
                        PhoneLineType_Status = item.PhoneLineType_Status,
                        PhoneLineType_OriginalValue = previousitem.PhoneLineType,
                        PhoneLineTypeSourceSystemRecordId = item.PhoneLineTypeSourceSystemRecordId,
                        PhoneLineTypeSourceSystemRecordId_Status = item.PhoneLineTypeSourceSystemRecordId_Status,
                        PhoneLineTypeSourceSystemRecordId_OriginalValue = previousitem.PhoneLineTypeSourceSystemRecordId,
                        PhoneLineTypeMasterId = item.PhoneLineTypeMasterRecordId,
                        PhoneLineTypeMasterId_Status = item.PhoneLineTypeMasterRecordId_Status,
                        PhoneLineTypeMasterId_OriginalValue = previousitem.PhoneLineTypeMasterRecordId,
                        PhoneUseType = item.PhoneUseType,
                        PhoneUseType_Status = item.PhoneUseType_Status,
                        PhoneUseType_OriginalValue = previousitem.PhoneUseType,
                        PhoneUseTypeSourceSystemRecordId = item.PhoneUseTypeSourceSystemRecordId,
                        PhoneUseTypeSourceSystemRecordId_Status = item.PhoneUseTypeSourceSystemRecordId_Status,
                        PhoneUseTypeSourceSystemRecordId_OriginalValue = previousitem.PhoneUseTypeSourceSystemRecordId,
                        PhoneUseTypeMasterId = item.PhoneUseTypeMasterId,
                        PhoneUseTypeMasterId_Status = item.PhoneUseTypeMasterId_Status,
                        PhoneUseTypeMasterId_OriginalValue = previousitem.PhoneUseTypeMasterId,
                        PhoneMasterId = item.PhoneMasterId,
                        PhoneMasterId_Status = item.PhoneMasterId_Status,
                        PhoneMasterId_OriginalValue = previousitem.PhoneMasterId,
                        PhoneIsPrimary = item.PhoneIsPrimary,
                        PhoneIsPrimary_Status = item.PhoneIsPrimary_Status,
                        PhoneIsPrimary_OriginalValue = previousitem.PhoneIsPrimary.ToString(),

                        HistoryDate = item.RecordDate
                    });
                }

                return viewModel;           

        }

        // GET: Constituent/ConstituentEdit
        public IActionResult ConstituentEdit(long Id, int SystemId)
        {
            try
            {
                return View(SetupConstituentViewModel(Id, SystemId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve ConstituentEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Match ***
        ******************************************************************/
        #region Match
        // GET: Constituent/ConstituentMatch
        public IActionResult ConstituentMatch(long Id, int SystemId)
        {
            try
            {
                var detail = _context.GetConstituentMatchDetails(SystemId, Id);

                var viewModel = new ConstituentMatchViewModel()
                {
                    PageId = "constituentIndividualPage",
                    PageWrapperClass = "toggled",
                    ActiveClass = "Constituent",
                    Title = "Constituent Matching",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    Id = detail.Id,
                    Integration = detail.IntegrationName,
                    IntegrationId = detail.IntegrationId,
                    IntegrationDate = detail.IntegrationDate,
                    System = detail.SystemName,
                    SystemId = detail.SystemId,
                    SourceRecordId = detail.SourceRecordId,
                    CreatedOnDT = detail.RecordDate,

                    FirstName = detail.FirstName,
                        FirstName_BusinessName = "First Name", //detail.FirstName_BusinessName
                        FirstName_BusinessDescription = detail.FirstName_BusinessDescription,
                        FirstName_Weight = detail.FirstName_MatchWeight,
                    PreferredName = detail.PreferredName,
                        PreferredName_BusinessName = "Preferred Name", //detail.PreferredName_BusinessName
                        PreferredName_BusinessDescription = detail.PreferredName_BusinessDescription,
                        PreferredName_Weight = detail.PreferredName_MatchWeight,
                    MiddleName = detail.MiddleName,
                        MiddleName_BusinessName = "Middle Name", //detail.MiddleName_BusinessName
                        MiddleName_BusinessDescription = detail.MiddleName_BusinessDescription,
                        MiddleName_Weight = detail.MiddleName_MatchWeight,
                    LastName = detail.LastName,
                        LastName_BusinessName = "Last Name", //detail.LastName_BusinessName
                        LastName_BusinessDescription = detail.LastName_BusinessDescription,
                        LastName_Weight = detail.LastName_MatchWeight,
                    MaidenName = detail.MaidenName,
                        MaidenName_BusinessName = "Maiden Name", //detail.MaidenName_BusinessName
                        MaidenName_BusinessDescription = detail.MaidenName_BusinessDescription,
                        MaidenName_Weight = detail.MaidenName_MatchWeight,
                    UAPersonId = detail.UAPersonId,
                        UAPersonId_BusinessName = detail.UAPersonId_BusinessName,
                        UAPersonId_BusinessDescription = detail.UAPersonId_BusinessDescription,
                        UAPersonId_Weight = detail.UAPersonId_MatchWeight,
                    //ConstituentTitle = detail.Title,
                    //    ConstituentTitle_BusinessName = detail.Title_BusinessName,
                    //    ConstituentTitle_BusinessDescription = detail.Title_BusinessDescription,
                    //    ConstituentTitle_Weight = detail.Title_MatchWeight,
                    Suffix = detail.Suffix,
                        Suffix_BusinessName = detail.Suffix_BusinessName,
                        Suffix_BusinessDescription = detail.Suffix_BusinessDescription,
                        Suffix_Weight = detail.Suffix_MatchWeight,
                    BirthDate = detail.BirthDate,
                        BirthDate_BusinessName = detail.BirthDate_BusinessName,
                        BirthDate_BusinessDescription = detail.BirthDate_BusinessDescription,
                        BirthDate_Weight = detail.BirthDate_MatchWeight,
                    DeceasedDate = detail.DeceasedDate,
                        DeceasedDate_BusinessName = detail.DeceasedDate_BusinessName,
                        DeceasedDate_BusinessDescription = detail.DeceasedDate_BusinessDescription,
                        DeceasedDate_Weight = detail.DeceasedDate_MatchWeight,
                    MaritalStatus = detail.MaritalStatus,
                        MaritalStatus_BusinessName = detail.MaritalStatus_BusinessName,
                        MaritalStatus_BusinessDescription = detail.MaritalStatus_BusinessDescription,
                        MaritalStatus_Weight = detail.MaritalStatus_MatchWeight,
                    Address = detail.Address,
                        Address_BusinessName = detail.Address_BusinessName,
                        Address_BusinessDescription = detail.Address_BusinessDescription,
                        Address_Weight = detail.Address_MatchWeight,
                    City = detail.City,
                        City_BusinessName = detail.City_BusinessName,
                        City_BusinessDescription = detail.City_BusinessDescription,
                        City_Weight = detail.City_MatchWeight,
                    PostalCode = detail.PostalCode,
                        PostalCode_BusinessName = detail.PostalCode_BusinessName,
                        PostalCode_BusinessDescription = detail.PostalCode_BusinessDescription,
                        PostalCode_Weight = detail.PostalCode_MatchWeight,
                    State = detail.State,
                        State_BusinessName = detail.State_BusinessName,
                        State_BusinessDescription = detail.State_BusinessDescription,
                        State_Weight = detail.State_MatchWeight,
                    Country = detail.Country,
                        Country_BusinessName = detail.Country_BusinessName,
                        Country_BusinessDescription = detail.Country_BusinessDescription,
                        Country_Weight = detail.Country_MatchWeight,
                    AddressUseType = detail.AddressUseType,
                        AddressUseType_BusinessName = detail.AddressUseType_BusinessName,
                        AddressUseType_BusinessDescription = detail.AddressUseType_BusinessDescription,
                        AddressUseType_Weight = detail.AddressUseType_MatchWeight,
                    AddressIsPrimary = detail.AddressIsPrimary,
                        AddressIsPrimary_BusinessName = detail.AddressIsPrimary_BusinessName,
                        AddressIsPrimary_BusinessDescription = detail.AddressIsPrimary_BusinessDescription,
                        AddressIsPrimary_Weight = detail.AddressIsPrimary_MatchWeight,
                    EmailAddress = detail.EmailAddress,
                        EmailAddress_BusinessName = detail.EmailAddress_BusinessName,
                        EmailAddress_BusinessDescription = detail.EmailAddress_BusinessDescription,
                        EmailAddress_Weight = detail.EmailAddress_MatchWeight,
                    EmailAddressUseType = detail.EmailAddressUseType,
                        EmailAddressUseType_BusinessName = detail.EmailAddressUseType_BusinessName,
                        EmailAddressUseType_BusinessDescription = detail.EmailAddressUseType_BusinessDescription,
                        EmailAddressUseType_Weight = detail.EmailAddressUseType_MatchWeight,
                    EmailIsPrimary = detail.EmailIsPrimary,
                        EmailIsPrimary_BusinessName = detail.EmailIsPrimary_BusinessName,
                        EmailIsPrimary_BusinessDescription = detail.EmailIsPrimary_BusinessDescription,
                        EmailIsPrimary_Weight = detail.EmailIsPrimary_MatchWeight,
                    PhoneNumber = detail.PhoneNumber,
                        PhoneNumber_BusinessName = detail.PhoneNumber_BusinessName,
                        PhoneNumber_BusinessDescription = detail.PhoneNumber_BusinessDescription,
                        PhoneNumber_Weight = detail.PhoneNumber_MatchWeight,
                    PhoneExtension = detail.PhoneExtension,
                        PhoneExtension_BusinessName = detail.PhoneExtension_BusinessName,
                        PhoneExtension_BusinessDescription = detail.PhoneExtension_BusinessDescription,
                        PhoneExtension_Weight = detail.PhoneExtension_MatchWeight,
                    PhoneCountryCode = detail.PhoneCountryCode,
                        PhoneCountryCode_BusinessName = detail.PhoneCountryCode_BusinessName,
                        PhoneCountryCode_BusinessDescription = detail.PhoneCountryCode_BusinessDescription,
                        PhoneCountryCode_Weight = detail.PhoneCountryCode_MatchWeight,
                    PhoneLineType = detail.PhoneLineType,
                        PhoneLineType_BusinessName = detail.PhoneLineType_BusinessName,
                        PhoneLineType_BusinessDescription = detail.PhoneLineType_BusinessDescription,
                        PhoneLineType_Weight = detail.PhoneLineType_MatchWeight,
                    PhoneUseType = detail.PhoneUseType,
                        PhoneUseType_BusinessName = detail.PhoneUseType_BusinessName,
                        PhoneUseType_BusinessDescription = detail.PhoneUseType_BusinessDescription,
                        PhoneUseType_Weight = detail.PhoneUseType_MatchWeight,
                    PhoneIsPrimary = detail.PhoneIsPrimary,
                        PhoneIsPrimary_BusinessName = detail.PhoneIsPrimary_BusinessName,
                        PhoneIsPrimary_BusinessDescription = detail.PhoneIsPrimary_BusinessDescription,
                        PhoneIsPrimary_Weight = detail.PhoneIsPrimary_MatchWeight

                };
                return View(viewModel);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve ConstituentMatch details");
                return RedirectToAction("SystemError", "Error");
            }
        }

        public IActionResult GetPossibleMatchList(long Id, int SystemId)
        {
            try
            {
                var viewModel = new ConstituentPossibleMatchViewModel()
                {
                    PossibleMatches = new List<ConstituentMatchSummaryViewModel>()
                };
                foreach (var possibleMatch in _context.GetConstituentPossibleMatches(SystemId, Id))
                {
                    viewModel.PossibleMatches.Add(new ConstituentMatchSummaryViewModel()
                    {
                        MatchConfidence = possibleMatch.MatchConfidence,
                        MasterId = possibleMatch.MasterId,
                        Name = $"{possibleMatch.FirstName} {possibleMatch.MiddleName} {possibleMatch.LastName}",
                        FirstName = possibleMatch.FirstName,
                        MiddleName = possibleMatch.MiddleName,
                        LastName = possibleMatch.LastName,
                        UAPersonId = possibleMatch.UAPersonId,
                        BirthDate = possibleMatch.BirthDate,
                        AllowMatch = possibleMatch.AllowMatch ? "Allow" : "Disallow"
                    });
                }
                return PartialView("ConstituentPossibleMatchList", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve Constituent GetPossibleMatchList");
                return RedirectToAction("SystemError", "Error");
            }
        }

        #endregion

        /*  *** Compare ***
        ******************************************************************/
        #region Compare

        private string GetIsPrimaryYesNoNotSet(bool? isPrimary)
        {
            return isPrimary.HasValue ? isPrimary.Value ? "Yes" : "No" : "Not Set";
        }
        // GET: Constituent/ConstituentCompare
        public IActionResult ConstituentCompare(long Id, int SystemId, string MasterId)
        {
            try
            {
                var detail = _context.GetConstituentComparisonDetail(SystemId, Id, MasterId);
                //var comparison = _context.GetConstituentComparisonDetail(SystemId, Id, MasterId);

                var viewModel = new ConstituentCompareViewModel()
                {
                    Id = Id,
                    IntegrationId = detail.IntegrationId,
                    SystemId = detail.SystemId,
                    IntegrationName = detail.IntegrationName,
                    SystemName = detail.SystemName,
                    MasterId = MasterId,
                    IntegrationDate = detail.IntegrationDate,
                    System = detail.SystemName,
                    SourceRecordId = detail.SourceRecordId,
                    AllowMatch = detail.AllowMatch,
                    AllowMerge = allowConstituentMerge(detail),
                    FirstName = detail.FirstName,
                        FirstName_BusinessName = detail.FirstName_BusinessName,
                        FirstName_BusinessDescription = detail.FirstName_BusinessDescription,
                        FirstName_Compare = detail.FirstName_Compare,
                        FirstName_IsDifferent = detail.FirstName != detail.FirstName_Compare,
                    PreferredName = detail.PreferredName,
                        PreferredName_BusinessName = detail.PreferredName_BusinessName,
                        PreferredName_BusinessDescription = detail.PreferredName_BusinessDescription,
                        PreferredName_Compare = detail.PreferredName_Compare,
                        PreferredName_IsDifferent = detail.PreferredName != detail.PreferredName_Compare,
                    MiddleName = detail.MiddleName,
                        MiddleName_BusinessName = detail.MiddleName_BusinessName,
                        MiddleName_BusinessDescription = detail.MiddleName_BusinessDescription,
                        MiddleName_Compare = detail.MiddleName_Compare,
                        MiddleName_IsDifferent = detail.MiddleName != detail.MiddleName_Compare,
                    LastName = detail.LastName,
                        LastName_BusinessName = detail.LastName_BusinessName,
                        LastName_BusinessDescription = detail.LastName_BusinessDescription,
                        LastName_Compare = detail.LastName_Compare,
                        LastName_IsDifferent = detail.LastName != detail.LastName_Compare,
                    MaidenName = detail.MaidenName,
                        MaidenName_BusinessName = detail.MaidenName_BusinessName,
                        MaidenName_BusinessDescription = detail.MaidenName_BusinessDescription,
                        MaidenName_Compare = detail.MaidenName_Compare,
                        MaidenName_IsDifferent = detail.MaidenName != detail.MaidenName_Compare,
                    UAPersonId = detail.UAPersonId,
                        UAPersonId_BusinessName = detail.UAPersonId_BusinessName,
                        UAPersonId_BusinessDescription = detail.UAPersonId_BusinessDescription,
                        UAPersonId_Compare = detail.UAPersonId_Compare,
                        UAPersonId_IsDifferent = detail.UAPersonId != detail.UAPersonId_Compare,
                    Suffix = detail.Suffix,
                        Suffix_BusinessName = detail.Suffix_BusinessName,
                        Suffix_BusinessDescription = detail.Suffix_BusinessDescription,
                        Suffix_Compare = detail.Suffix_Compare,
                        Suffix_IsDifferent = detail.Suffix != detail.Suffix_Compare,
                    BirthDate = detail.BirthDate,
                        BirthDate_BusinessName = detail.BirthDate_BusinessName,
                        BirthDate_BusinessDescription = detail.BirthDate_BusinessDescription,
                        BirthDate_Compare = detail.BirthDate_Compare,
                        BirthDate_IsDifferent = detail.BirthDate != detail.BirthDate_Compare,
                    DeceasedDate = detail.DeceasedDate,
                        DeceasedDate_BusinessName = detail.DeceasedDate_BusinessName,
                        DeceasedDate_BusinessDescription = detail.DeceasedDate_BusinessDescription,
                        DeceasedDate_Compare = detail.DeceasedDate_Compare,
                        DeceasedDate_IsDifferent = detail.DeceasedDate != detail.DeceasedDate_Compare,
                    MaritalStatus = detail.MaritalStatus,
                        MaritalStatus_BusinessName = detail.MaritalStatus_BusinessName,
                        MaritalStatus_BusinessDescription = detail.MaritalStatus_BusinessDescription,
                        MaritalStatus_Compare = detail.MaritalStatus_Compare,
                        MaritalStatus_IsDifferent = detail.MaritalStatus != detail.MaritalStatus_Compare,
                    Address = detail.Address,
                        Address_BusinessName = detail.Address_BusinessName,
                        Address_BusinessDescription = detail.Address_BusinessDescription,
                        Address_Compare = detail.Address_Compare,
                        Address_IsDifferent = detail.Address != detail.Address_Compare,
                    City = detail.City,
                        City_BusinessName = detail.City_BusinessName,
                        City_BusinessDescription = detail.City_BusinessDescription,
                        City_Compare = detail.City_Compare,
                        City_IsDifferent = detail.City != detail.City_Compare,
                    State = detail.State,
                        State_BusinessName = detail.State_BusinessName,
                        State_BusinessDescription = detail.State_BusinessDescription,
                        State_Compare = detail.State_Compare,
                        State_IsDifferent = detail.State != detail.State_Compare,
                    PostalCode = detail.PostalCode,
                        PostalCode_BusinessName = detail.PostalCode_BusinessName,
                        PostalCode_BusinessDescription = detail.PostalCode_BusinessDescription,
                        PostalCode_Compare = detail.PostalCode_Compare,
                        PostalCode_IsDifferent = detail.PostalCode != detail.PostalCode_Compare,
                    Country = detail.Country,
                        Country_BusinessName = detail.Country_BusinessName,
                        Country_BusinessDescription = detail.Country_BusinessDescription,
                        Country_Compare = detail.Country_Compare,
                        Country_IsDifferent = detail.Country != detail.Country_Compare,
                    AddressUseType = detail.AddressUseType,
                        AddressUseType_BusinessName = detail.AddressUseType_BusinessName,
                        AddressUseType_BusinessDescription = detail.AddressUseType_BusinessDescription,
                        AddressUseType_Compare = detail.AddressUseType_Compare,
                        AddressUseType_IsDifferent = detail.AddressUseType != detail.AddressUseType_Compare,
                    AddressIsPrimary = detail.AddressIsPrimary,
                        AddressIsPrimaryDisplay = GetIsPrimaryYesNoNotSet(detail.AddressIsPrimary),
                        AddressIsPrimary_BusinessName = detail.AddressIsPrimary_BusinessName,
                        AddressIsPrimary_BusinessDescription = detail.AddressIsPrimary_BusinessDescription,
                        AddressIsPrimary_Compare = detail.AddressIsPrimary_Compare,
                        AddressIsPrimaryDisplay_Compare = GetIsPrimaryYesNoNotSet(detail.AddressIsPrimary_Compare),
                        AddressIsPrimary_IsDifferent = detail.AddressIsPrimary != detail.AddressIsPrimary_Compare,
                    EmailAddress = detail.EmailAddress,
                        EmailAddress_BusinessName = detail.EmailAddress_BusinessName,
                        EmailAddress_BusinessDescription = detail.EmailAddress_BusinessDescription,
                        EmailAddress_Compare = detail.EmailAddress_Compare,
                        EmailAddress_IsDifferent = detail.EmailAddress != detail.EmailAddress_Compare,
                    EmailAddressUseType = detail.EmailAddressUseType,
                        EmailAddressUseType_BusinessName = detail.EmailAddressUseType_BusinessName,
                        EmailAddressUseType_BusinessDescription = detail.EmailAddressUseType_BusinessDescription,
                        EmailAddressUseType_Compare = detail.EmailAddressUseType_Compare,
                        EmailAddressUseType_IsDifferent = detail.EmailAddressUseType != detail.EmailAddressUseType_Compare,
                    EmailIsPrimary = detail.EmailIsPrimary,
                        EmailIsPrimaryDisplay = GetIsPrimaryYesNoNotSet(detail.EmailIsPrimary),
                        EmailIsPrimary_BusinessName = detail.EmailIsPrimary_BusinessName,
                        EmailIsPrimary_BusinessDescription = detail.EmailIsPrimary_BusinessDescription,
                        EmailIsPrimary_Compare = detail.EmailIsPrimary_Compare,
                        EmailIsPrimaryDisplay_Compare = GetIsPrimaryYesNoNotSet(detail.EmailIsPrimary_Compare),
                        EmailIsPrimary_IsDifferent = detail.EmailIsPrimary != detail.EmailIsPrimary_Compare,
                    PhoneNumber = detail.PhoneNumber,
                        PhoneNumber_BusinessName = detail.PhoneNumber_BusinessName,
                        PhoneNumber_BusinessDescription = detail.PhoneNumber_BusinessDescription,
                        PhoneNumber_Compare = detail.PhoneNumber_Compare,
                        PhoneNumber_IsDifferent = detail.PhoneNumber != detail.PhoneNumber_Compare,
                    PhoneExtension = detail.PhoneExtension,
                        PhoneExtension_BusinessName = detail.PhoneExtension_BusinessName,
                        PhoneExtension_BusinessDescription = detail.PhoneExtension_BusinessDescription,
                        PhoneExtension_Compare = detail.PhoneExtension_Compare,
                        PhoneExtension_IsDifferent = detail.PhoneExtension != detail.PhoneExtension_Compare,
                    PhoneCountryCode = detail.PhoneCountryCode,
                        PhoneCountryCode_BusinessName = detail.PhoneCountryCode_BusinessName,
                        PhoneCountryCode_BusinessDescription = detail.PhoneCountryCode_BusinessDescription,
                        PhoneCountryCode_Compare = detail.PhoneCountryCode_Compare,
                        PhoneCountryCode_IsDifferent = detail.PhoneCountryCode != detail.PhoneCountryCode_Compare,
                    PhoneUseType = detail.PhoneUseType,
                        PhoneUseType_BusinessName = detail.PhoneUseType_BusinessName,
                        PhoneUseType_BusinessDescription = detail.PhoneUseType_BusinessDescription,
                        PhoneUseType_Compare = detail.PhoneUseType_Compare,
                        PhoneUseType_IsDifferent = detail.PhoneUseType != detail.PhoneUseType_Compare,
                    PhoneIsPrimary = detail.PhoneIsPrimary,
                        PhoneIsPrimaryDisplay = GetIsPrimaryYesNoNotSet(detail.PhoneIsPrimary),
                        PhoneIsPrimary_BusinessName = detail.PhoneIsPrimary_BusinessName,
                        PhoneIsPrimary_BusinessDescription = detail.PhoneIsPrimary_BusinessDescription,
                        PhoneIsPrimary_Compare = detail.PhoneIsPrimary_Compare,
                        PhoneIsPrimaryDisplay_Compare = GetIsPrimaryYesNoNotSet(detail.PhoneIsPrimary_Compare),
                        PhoneIsPrimary_IsDifferent = detail.PhoneIsPrimary != detail.PhoneIsPrimary_Compare,
                    SystemRecords = detail.SystemRecords.ToList()
                };

                return PartialView("ConstituentCompare", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve ConstituentCompare details");
                return RedirectToAction("SystemError", "Error");
            }
        }

        private bool allowConstituentMerge(ConstituentComparisonDetail detail)
        {
            var allowMerge = false;

            var eligableSystemRecords = detail.SystemRecords.Where(r => r.SystemId == "8");

            if (eligableSystemRecords.Any())
            {
                if (eligableSystemRecords.FirstOrDefault().SystemId == detail.SystemId.ToString() 
                    && eligableSystemRecords.FirstOrDefault().SystemRecordId != detail.SourceRecordId)
                    allowMerge = true;
            }

            return allowMerge;
        }
        #endregion

        /*  *** Manual Match ***
        ******************************************************************/
        #region ManualMatch
        // GET: Constituent/ConstituentManualMatch
        public async Task<IActionResult> ConstituentManualMatch(long Id, int IntegrationId, int SystemId, string MasterId)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, User.Identity.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve ConstituentManualMatch details");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentList));
        }
        #endregion

        /*  *** Manual Merge ***
        ******************************************************************/
        #region ManualMerge
        public async Task<IActionResult> ConstituentManualMerge(string Id, string LosingSourceId, int IntegrationId, int SystemId, long LosingId)
        {
            try
            {
                switch (SystemId)
                {
                    case 8:
                        await _domainService.MarketoService.Merge(Convert.ToInt32(Id), Convert.ToInt32(LosingSourceId));
                        break;
                    default:
                        throw new Exception($"System ID {SystemId} is not eligible for merging");
                }

                this.RemoveIntegrationPossibleMatchRecord(SystemId, IntegrationId, LosingId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting ConstituentManualMerge details");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentList));
        }
        #endregion


        /*  *** Save ***
        ******************************************************************/
        #region Save
        private void SaveConstituent(ConstituentViewModel model)
        {
            //Set IsPrimary to true if Address, EmailAddress, and PhoneNumber are not null
            if (model.Address != null)
            {
                model.AddressIsPrimary = true;
            }
            if (model.EmailAddress != null)
            {
                model.EmailIsPrimary = true;
            }
            if (model.PhoneNumber != null)
            {
                model.PhoneIsPrimary = true;
            }

            _context.ChangeConstituentIntegrationRecord(model.SystemId, model.Id, model.FirstName, model.PreferredName, model.MiddleName, model.LastName, model.MaidenName, model.UAPersonId,
                model.Suffix, model.SuffixSourceSystemRecordId, model.SuffixMasterId,
                model.BirthDate, model.DeceasedDate, model.MaritalStatus, model.MaritalStatusSourceSystemRecordId,
                model.MaritalStatusMasterId, model.Address, model.AddressSourceSystemRecordId, model.AddressMasterId, model.City, model.PostalCode, model.State,
                model.StateSourceSystemRecordId, model.StateMasterId, model.Country, model.CountrySourceSystemRecordId, model.CountryMasterId, model.AddressUseType, model.AddressUseTypeSourceSystemRecordId,
                model.AddressUseTypeMasterId, model.AddressIsPrimary, model.EmailAddress, model.EmailAddressSourceSystemRecordId, model.EmailAddressMasterId, model.EmailAddressUseType, model.EmailAddressUseTypeSourceSystemRecordId, model.EmailAddressUseTypeMasterId,
                model.EmailIsPrimary, model.PhoneNumber, model.PhoneNumberSourceSystemRecordId, model.PhoneExtension, model.PhoneMasterId, model.PhoneCountryCode, model.PhoneCountrySourceSystemRecordId, model.PhoneCountryMasterRecordId, model.PhoneLineType,
                model.PhoneLineTypeSourceSystemRecordId, model.PhoneLineTypeMasterRecordId, model.PhoneUseType, model.PhoneUseTypeSourceSystemRecordId, model.PhoneUseTypeMasterId, model.PhoneIsPrimary, User.Identity.Name);
        }

        // POST: Constituent/ConstituentSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConstituentSave(ConstituentViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    SaveConstituent(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in ConstituentSave");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(ConstituentList));
            }
            return View(model);
        }
        #endregion

        /*  *** Revalidate ***
        ******************************************************************/
        #region Revalidate
        // POST: Constituent/ConstituentRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConstituentRevalidate(ConstituentViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    // Check if Dropdown have MasterId - If they dont, empty out the associated attributes per dropdown before revalidating
                    // Title
                    //if (string.IsNullOrEmpty(model.TitleMasterId) && (!string.IsNullOrEmpty(model.TitleSourceSystemRecordId) || !string.IsNullOrEmpty(model.Title)))
                    //{
                    //    model.TitleSourceSystemRecordId = null;
                    //    model.Title = null;

                    //    model.IsChanged = true;
                    //}

                    // Suffix
                    if (string.IsNullOrEmpty(model.SuffixMasterId) && (!string.IsNullOrEmpty(model.SuffixSourceSystemRecordId) || !string.IsNullOrEmpty(model.Suffix)))
                    {
                        model.SuffixSourceSystemRecordId = null;
                        model.Suffix = null;

                        model.IsChanged = true;
                    }

                    // MaritalStatus
                    if (string.IsNullOrEmpty(model.MaritalStatusMasterId) && (!string.IsNullOrEmpty(model.MaritalStatusSourceSystemRecordId) || !string.IsNullOrEmpty(model.MaritalStatus)))
                    {
                        model.MaritalStatusSourceSystemRecordId = null;
                        model.MaritalStatus = null;

                        model.IsChanged = true;
                    }

                    // State
                    if (string.IsNullOrEmpty(model.StateMasterId) && (!string.IsNullOrEmpty(model.StateSourceSystemRecordId) || !string.IsNullOrEmpty(model.State)))
                    {
                        model.StateSourceSystemRecordId = null;
                        model.State = null;

                        model.IsChanged = true;
                    }

                    // Country
                    if (string.IsNullOrEmpty(model.CountryMasterId) && (!string.IsNullOrEmpty(model.CountrySourceSystemRecordId) || !string.IsNullOrEmpty(model.Country)))
                    {
                        model.CountrySourceSystemRecordId = null;
                        model.Country = null;

                        model.IsChanged = true;
                    }

                    // EmailUseType
                    if (string.IsNullOrEmpty(model.EmailAddressUseTypeMasterId) && (!string.IsNullOrEmpty(model.EmailAddressUseTypeSourceSystemRecordId) || !string.IsNullOrEmpty(model.EmailAddressUseType)))
                    {
                        model.EmailAddressUseTypeSourceSystemRecordId = null;
                        model.EmailAddressUseType = null;

                        model.IsChanged = true;
                    }

                    // PhoneLineType
                    if (string.IsNullOrEmpty(model.PhoneLineTypeMasterRecordId) && (!string.IsNullOrEmpty(model.PhoneLineTypeSourceSystemRecordId) || !string.IsNullOrEmpty(model.PhoneLineType)))
                    {
                        model.PhoneLineTypeSourceSystemRecordId = null;
                        model.PhoneLineType = null;

                        model.IsChanged = true;
                    }

                    // PhoneUseType
                    if (string.IsNullOrEmpty(model.PhoneUseTypeMasterId) && (!string.IsNullOrEmpty(model.PhoneUseTypeSourceSystemRecordId) || !string.IsNullOrEmpty(model.PhoneUseType)))
                    {
                        model.PhoneUseTypeSourceSystemRecordId = null;
                        model.PhoneUseType = null;

                        model.IsChanged = true;
                    }

                    if (model.IsChanged)
                    {
                        _context.ChangeConstituentIntegrationRecord(model.SystemId, model.Id, model.FirstName, model.PreferredName, model.MiddleName, model.LastName, model.MaidenName, model.UAPersonId,
                            model.Suffix, model.SuffixSourceSystemRecordId, model.SuffixMasterId,
                            model.BirthDate, model.DeceasedDate, model.MaritalStatus, model.MaritalStatusSourceSystemRecordId,
                            model.MaritalStatusMasterId, model.Address, model.AddressSourceSystemRecordId, model.AddressMasterId, model.City, model.PostalCode, model.State,
                            model.StateSourceSystemRecordId, model.StateMasterId, model.Country, model.CountrySourceSystemRecordId, model.CountryMasterId, model.AddressUseType, model.AddressUseTypeSourceSystemRecordId,
                            model.AddressUseTypeMasterId, model.AddressIsPrimary, model.EmailAddress, model.EmailAddressSourceSystemRecordId, model.EmailAddressMasterId, model.EmailAddressUseType, model.EmailAddressUseTypeSourceSystemRecordId, model.EmailAddressUseTypeMasterId,
                            model.EmailIsPrimary, model.PhoneNumber, model.PhoneNumberSourceSystemRecordId, model.PhoneExtension, model.PhoneMasterId, model.PhoneCountryCode, model.PhoneCountrySourceSystemRecordId, model.PhoneCountryMasterRecordId, model.PhoneLineType,
                            model.PhoneLineTypeSourceSystemRecordId, model.PhoneLineTypeMasterRecordId, model.PhoneUseType, model.PhoneUseTypeSourceSystemRecordId, model.PhoneUseTypeMasterId, model.PhoneIsPrimary, User.Identity.Name);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in ConstituentRevalidate");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(ConstituentList));
            }
            return View(model);
        }
        #endregion

        /*  *** Ignore ***
        ******************************************************************/
        #region Ignore
        public IActionResult ConstituentIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load ConstituentIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentList));
        }
        #endregion

        #endregion

        #region Phone (Contact)

        /*  *** List View ***
        ******************************************************************/
        #region List
        // GET: Constituent/ConstituentPhoneList
        public IActionResult ConstituentPhoneList()
        {
            var model = new ConstituentPhoneListViewModel()
            {
                Title = "Constituent Phone",
                PageId = "constituentPhonePage",
                ActiveClass = "ConstituentPhone",
                Message = "Your Constituent Phone Page",
                Integration = "Constituent Phone Number",
                IntegrationId = 18,
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetConstituentPhoneList(AjaxDataTableRequest request)
        {
            try
            {
                var constituentPhones = _context.ConstituentPhoneRemediationList.AsQueryable();

                int recordsTotal = constituentPhones.Count();

                var constituentPhoneList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? constituentPhones
                                                : constituentPhones.Where(s => 
                                                s.FirstName.Contains(request.searchValue) ||
                                                s.LastName.Contains(request.searchValue) ||
                                                s.PhoneNumber.Contains(request.searchValue) ||
                                                s.Extension.Contains(request.searchValue) ||
                                                s.Country.Contains(request.searchValue) ||
                                                s.PhoneLineType.Contains(request.searchValue) ||
                                                s.PhoneUseType.Contains(request.searchValue) ||
                                                s.ErrorCategories.Contains(request.searchValue) || s.SystemName.Contains(request.searchValue))
                                            )
                                            .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                            .ToListAsync();

                int recordsFiltered = constituentPhoneList.Count();

                var constituentPhoneRemediationList = new List<ConstituentPhoneRemediationListItemViewModel>();

                foreach (var item in constituentPhoneList.Skip(request.start).Take(request.length))
                {
                    constituentPhoneRemediationList.Add(new ConstituentPhoneRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        SystemName = item.SystemName,
                        ErrorCategories = item.ErrorCategories,
                        ErrorCount = item.ErrorCount,
                        IntegrationDate = item.IntegrationDate,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate,
                        RecordStatus = item.RecordStatus,

                        Name = string.Format("{0} {1}", item.FirstName, item.LastName),
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        PhoneNumber = item.PhoneNumber,
                        Extension = item.Extension,
                        Country = item.Country,
                        PhoneLineType = item.PhoneLineType,
                        PhoneUseType = item.PhoneUseType
                    });
                }

                var data = constituentPhoneRemediationList.ToList();

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
                _logger.LogError(ex, "Unable to retrieve ConstituentPhoneList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Edit View ***
        ******************************************************************/
        #region Edit
        // GET: Student/ConstituentPhoneEdit
        public IActionResult ConstituentPhoneEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetConstituentPhoneHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var integrationDetail = history.First();
                var integrationSource = history.Last();

                var viewModel = new ConstituentPhoneViewModel()
                {
                    Title = "Constituent Phone",
                    PageId = "constituentPhonePage",
                    ActiveClass = "ConstituentPhone",
                    Message = "Your Constituent Phone Page",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = integrationDetail.Id,
                    System = integrationDetail.SystemName,
                    SystemId = integrationDetail.SystemId,
                    Integration = integrationDetail.IntegrationName,
                    IntegrationId = integrationDetail.IntegrationId,
                    IntegrationDate = integrationDetail.IntegrationDate,
                    CreatedDate = integrationDetail.RecordDate,
                    SourceRecordId = integrationDetail.SourceRecordId,
                    CreatedOnDT = integrationDetail.RecordDate,

                    HistoryData = new List<ConstituentPhoneHistoryViewModel>(),

                    #region Constituent Phone Details

                    ConstituentSourceSystemRecordId = integrationDetail.ConstituentSourceSystemRecordId,
                        ConstituentSourceSystemRecordId_BusinessName = integrationDetail.ConstituentSourceSystemRecordId_BusinessName,
                        ConstituentSourceSystemRecordId_BusinessDescription = integrationDetail.ConstituentSourceSystemRecordId_BusinessDescription,
                        ConstituentSourceSystemRecordId_OriginalValue = integrationSource.ConstituentSourceSystemRecordId,
                        ConstituentSourceSystemRecordId_Status = integrationDetail.ConstituentSourceSystemRecordId_Status,
                        ConstituentSourceSystemRecordId_Source = integrationDetail.ConstituentSourceSystemRecordId_Source,
                        ConstituentSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.ConstituentSourceSystemRecordId_Source }),
                    FirstName = integrationDetail.FirstName,
                        FirstName_BusinessName = integrationDetail.FirstName_BusinessName,
                        FirstName_BusinessDescription = integrationDetail.FirstName_BusinessDescription,
                        FirstName_OriginalValue = integrationSource.FirstName,
                        FirstName_Status = integrationDetail.FirstName_Status,
                        FirstName_Source = integrationDetail.FirstName_Source,
                        FirstName_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.FirstName_Source }),
                    LastName = integrationDetail.LastName,
                        LastName_BusinessName = integrationDetail.LastName_BusinessName,
                        LastName_BusinessDescription = integrationDetail.LastName_BusinessDescription,
                        LastName_OriginalValue = integrationSource.LastName,
                        LastName_Status = integrationDetail.LastName_Status,
                        LastName_Source = integrationDetail.LastName_Source,
                        LastName_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.LastName_Source }),
                    UAPersonId = integrationDetail.UAPersonId,
                        UAPersonId_BusinessName = integrationDetail.UAPersonId_BusinessName,
                        UAPersonId_BusinessDescription = integrationDetail.UAPersonId_BusinessDescription,
                        UAPersonId_OriginalValue = integrationSource.UAPersonId,
                        UAPersonId_Status = integrationDetail.UAPersonId_Status,
                        UAPersonId_Source = integrationDetail.UAPersonId_Source,
                        UAPersonId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.UAPersonId_Source }),
                    ConstituentMasterId = integrationDetail.ConstituentMasterId,
                        ConstituentMasterId_BusinessName = integrationDetail.ConstituentMasterId_BusinessName,
                        ConstituentMasterId_BusinessDescription = integrationDetail.ConstituentMasterId_BusinessDescription,
                        ConstituentMasterId_OriginalValue = integrationSource.ConstituentMasterId,
                        ConstituentMasterId_Status = integrationDetail.ConstituentMasterId_Status,
                        ConstituentMasterId_Source = integrationDetail.ConstituentMasterId_Source,
                        ConstituentMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.ConstituentMasterId_Source, integrationDetail.FirstName_Source, integrationDetail.LastName_Source, integrationDetail.UAPersonId_Source, integrationDetail.ConstituentSourceSystemRecordId_Source }),

                    PhoneNumber = integrationDetail.PhoneNumber,
                        PhoneNumber_BusinessName = integrationDetail.PhoneNumber_BusinessName,
                        PhoneNumber_BusinessDescription = integrationDetail.PhoneNumber_BusinessDescription,
                        PhoneNumber_OriginalValue = integrationSource.PhoneNumber,
                        PhoneNumber_Status = integrationDetail.PhoneNumber_Status,
                        PhoneNumber_Source = integrationDetail.PhoneNumber_Source,
                        PhoneNumber_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PhoneNumber_Source }),
                    PhoneMasterId = integrationDetail.PhoneMasterId,
                        PhoneMasterId_BusinessName = integrationDetail.PhoneMasterId_BusinessName,
                        PhoneMasterId_BusinessDescription = integrationDetail.PhoneMasterId_BusinessDescription,
                        PhoneMasterId_OriginalValue = integrationSource.PhoneMasterId,
                        PhoneMasterId_Status = integrationDetail.PhoneMasterId_Status,
                        PhoneMasterId_Source = integrationDetail.PhoneMasterId_Source,
                        PhoneMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PhoneMasterId_Source, integrationDetail.PhoneNumber_Source }),

                    Extension = integrationDetail.PhoneExtension,
                        Extension_BusinessName = integrationDetail.PhoneExtension_BusinessName,
                        Extension_BusinessDescription = integrationDetail.PhoneExtension_BusinessDescription,
                        Extension_OriginalValue = integrationSource.PhoneExtension,
                        Extension_Status = integrationDetail.PhoneExtension_Status,
                        Extension_Source = integrationDetail.PhoneExtension_Source,
                        Extension_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PhoneExtension_Source }),
                        
                    Country = integrationDetail.CountryCode,
                        Country_BusinessName = integrationDetail.CountryCode_BusinessName,
                        Country_BusinessDescription = integrationDetail.CountryCode_BusinessDescription,
                        Country_OriginalValue = integrationSource.CountryCode,
                        Country_Status = integrationDetail.CountryCode_Status,
                        Country_Source = integrationDetail.CountryCode_Source,
                        Country_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.CountryCode_Source }),
                    CountrySourceSystemRecordId = integrationDetail.CountrySourceSystemRecordId,
                        CountrySourceSystemRecordId_BusinessName = integrationDetail.CountrySourceSystemRecordId_BusinessName,
                        CountrySourceSystemRecordId_BusinessDescription = integrationDetail.CountrySourceSystemRecordId_BusinessDescription,
                        CountrySourceSystemRecordId_OriginalValue = integrationSource.CountrySourceSystemRecordId,
                        CountrySourceSystemRecordId_Status = integrationDetail.CountrySourceSystemRecordId_Status,
                        CountrySourceSystemRecordId_Source = integrationDetail.CountrySourceSystemRecordId_Source,
                        CountrySourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.CountrySourceSystemRecordId_Source }),
                    CountryMasterId = integrationDetail.CountryMasterRecordId,
                        CountryMasterId_BusinessName = integrationDetail.CountryMasterRecordId_BusinessName,
                        CountryMasterId_BusinessDescription = integrationDetail.CountryMasterRecordId_BusinessDescription,
                        CountryMasterId_OriginalValue = integrationSource.CountryMasterRecordId,
                        CountryMasterId_Status = integrationDetail.CountryMasterRecordId_Status,
                        CountryMasterId_Source = integrationDetail.CountryMasterRecordId_Source,
                        CountryMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.CountryMasterRecordId_Source, integrationDetail.CountrySourceSystemRecordId_Source, integrationDetail.CountryCode_Source }),
                        
                    PhoneLineType = integrationDetail.PhoneLineType,
                        PhoneLineType_BusinessName = integrationDetail.PhoneLineType_BusinessName,
                        PhoneLineType_BusinessDescription = integrationDetail.PhoneLineType_BusinessDescription,
                        PhoneLineType_OriginalValue = integrationSource.PhoneLineType,
                        PhoneLineType_Status = integrationDetail.PhoneLineType_Status,
                        PhoneLineType_Source = integrationDetail.PhoneLineType_Source,
                        PhoneLineType_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PhoneLineType_Source }),
                    PhoneLineTypeSourceSystemRecordId = integrationDetail.PhoneLineTypeSourceSystemRecordId,
                        PhoneLineTypeSourceSystemRecordId_BusinessName = integrationDetail.PhoneLineTypeSourceSystemRecordId_BusinessName,
                        PhoneLineTypeSourceSystemRecordId_BusinessDescription = integrationDetail.PhoneLineTypeSourceSystemRecordId_BusinessDescription,
                        PhoneLineTypeSourceSystemRecordId_OriginalValue = integrationSource.PhoneLineTypeSourceSystemRecordId,
                        PhoneLineTypeSourceSystemRecordId_Status = integrationDetail.PhoneLineTypeSourceSystemRecordId_Status,
                        PhoneLineTypeSourceSystemRecordId_Source = integrationDetail.PhoneLineTypeSourceSystemRecordId_Source,
                        PhoneLineTypeSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PhoneLineTypeSourceSystemRecordId_Source }),
                    PhoneLineTypeMasterId = integrationDetail.PhoneLineTypeMasterRecordId,
                        PhoneLineTypeMasterId_BusinessName = integrationDetail.PhoneLineTypeMasterRecordId_BusinessName,
                        PhoneLineTypeMasterId_BusinessDescription = integrationDetail.PhoneLineTypeMasterRecordId_BusinessDescription,
                        PhoneLineTypeMasterId_OriginalValue = integrationSource.PhoneLineTypeMasterRecordId,
                        PhoneLineTypeMasterId_Status = integrationDetail.PhoneLineTypeMasterRecordId_Status,
                        PhoneLineTypeMasterId_Source = integrationDetail.PhoneLineTypeMasterRecordId_Source,
                        PhoneLineTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PhoneLineTypeMasterRecordId_Source, integrationDetail.PhoneLineTypeSourceSystemRecordId_Source, integrationDetail.PhoneLineType_Source }),
                        
                    PhoneUseType = integrationDetail.PhoneUseType,
                        PhoneUseType_BusinessName = integrationDetail.PhoneUseType_BusinessName,
                        PhoneUseType_BusinessDescription = integrationDetail.PhoneUseType_BusinessDescription,
                        PhoneUseType_OriginalValue = integrationSource.PhoneUseType,
                        PhoneUseType_Status = integrationDetail.PhoneUseType_Status,
                        PhoneUseType_Source = integrationDetail.PhoneUseType_Source,
                        PhoneUseType_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PhoneUseType_Source }),
                    PhoneUseTypeSourceSystemRecordId = integrationDetail.PhoneUseTypeSourceSystemRecordId,
                        PhoneUseTypeSourceSystemRecordId_BusinessName = integrationDetail.PhoneUseTypeSourceSystemRecordId_BusinessName,
                        PhoneUseTypeSourceSystemRecordId_BusinessDescription = integrationDetail.PhoneUseTypeSourceSystemRecordId_BusinessDescription,
                        PhoneUseTypeSourceSystemRecordId_OriginalValue = integrationSource.PhoneUseTypeSourceSystemRecordId,
                        PhoneUseTypeSourceSystemRecordId_Status = integrationDetail.PhoneUseTypeSourceSystemRecordId_Status,
                        PhoneUseTypeSourceSystemRecordId_Source = integrationDetail.PhoneUseTypeSourceSystemRecordId_Source,
                        PhoneUseTypeSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PhoneUseTypeSourceSystemRecordId_Source }),
                    PhoneUseTypeMasterId = integrationDetail.PhoneUseTypeMasterId,
                        PhoneUseTypeMasterId_BusinessName = integrationDetail.PhoneUseTypeMasterId_BusinessName,
                        PhoneUseTypeMasterId_BusinessDescription = integrationDetail.PhoneUseTypeMasterId_BusinessDescription,
                        PhoneUseTypeMasterId_OriginalValue = integrationSource.PhoneUseTypeMasterId,
                        PhoneUseTypeMasterId_Status = integrationDetail.PhoneUseTypeMasterId_Status,
                        PhoneUseTypeMasterId_Source = integrationDetail.PhoneUseTypeMasterId_Source,
                        PhoneUseTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PhoneUseTypeMasterId_Source, integrationDetail.PhoneUseTypeSourceSystemRecordId_Source, integrationDetail.PhoneUseType_Source }),
                    PhoneIsPrimary = integrationDetail.PhoneIsPrimary,
                        PhoneIsPrimary_BusinessName = integrationDetail.PhoneIsPrimary_BusinessName,
                        PhoneIsPrimary_BusinessDescription = integrationDetail.PhoneIsPrimary_BusinessDescription,
                        PhoneIsPrimary_OriginalValue = integrationSource.PhoneIsPrimary.ToString(),
                        PhoneIsPrimary_Status = integrationDetail.PhoneIsPrimary_Status,
                        PhoneIsPrimary_Source = integrationDetail.PhoneIsPrimary_Source,
                        PhoneIsPrimary_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PhoneIsPrimary_Source }),


                    #endregion

                    #region Dropdowns
                    ConstituentList = GetStudentList(),
                    CountryList = GetCountryDialingCodeList(),
                    PhoneLineTypeList = GetPhoneLineTypeList(),
                    PhoneUseTypeList = GetPhoneUseTypeList(),
                    #endregion
                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);
                    viewModel.HistoryData.Add(new ConstituentPhoneHistoryViewModel()
                    {
                        ConstituentSourceSystemRecordId = item.ConstituentSourceSystemRecordId,
                            ConstituentSourceSystemRecordId_Status = item.ConstituentSourceSystemRecordId_Status,
                            ConstituentSourceSystemRecordId_OriginalValue = previousitem.ConstituentSourceSystemRecordId,
                        ConstituentMasterId = item.ConstituentMasterId,
                            ConstituentMasterId_Status = item.ConstituentMasterId_Status,
                            ConstituentMasterId_OriginalValue = previousitem.ConstituentMasterId,

                        PhoneNumber = item.PhoneNumber,
                            PhoneNumber_Status = item.PhoneNumber_Status,
                            PhoneNumber_OriginalValue = previousitem.PhoneNumber,
                        //PhoneSystemRecordId = item.PhoneSystemRecordId,
                        //    PhoneSystemRecordId_Status = item.PhoneSystemRecordId_Status,
                        //    PhoneSystemRecordId_OriginalValue = previousitem.PhoneSystemRecordId,
                        PhoneMasterId = item.PhoneMasterId,
                            PhoneMasterId_Status = item.PhoneMasterId_Status,
                            PhoneMasterId_OriginalValue = previousitem.PhoneMasterId,

                        Extension = item.PhoneExtension,
                            Extension_Status = item.PhoneExtension_Status,
                            Extension_OriginalValue = previousitem.PhoneExtension,

                        Country = item.CountryCode,
                            Country_Status = item.CountryCode_Status,
                            Country_OriginalValue = previousitem.CountryCode,
                        CountrySourceSystemRecordId = item.CountrySourceSystemRecordId,
                            CountrySourceSystemRecordId_Status = item.CountrySourceSystemRecordId_Status,
                            CountrySourceSystemRecordId_OriginalValue = previousitem.CountrySourceSystemRecordId,
                        CountryMasterId = item.CountryMasterRecordId,
                            CountryMasterId_Status = item.CountryMasterRecordId_Status,
                            CountryMasterId_OriginalValue = previousitem.CountryMasterRecordId,

                        PhoneLineType = item.PhoneLineType,
                            PhoneLineType_Status = item.PhoneLineType_Status,
                            PhoneLineType_OriginalValue = previousitem.PhoneLineType,
                        PhoneLineTypeSourceSystemRecordId = item.PhoneLineTypeSourceSystemRecordId,
                            PhoneLineTypeSourceSystemRecordId_Status = item.PhoneLineTypeSourceSystemRecordId_Status,
                            PhoneLineTypeSourceSystemRecordId_OriginalValue = previousitem.PhoneLineTypeSourceSystemRecordId,
                        PhoneLineTypeMasterId = item.PhoneLineTypeMasterRecordId,
                            PhoneLineTypeMasterId_Status = item.PhoneLineTypeMasterRecordId_Status,
                            PhoneLineTypeMasterId_OriginalValue = previousitem.PhoneLineTypeMasterRecordId,

                        PhoneUseType = item.PhoneUseType,
                            PhoneUseType_Status = item.PhoneUseType_Status,
                            PhoneUseType_OriginalValue = previousitem.PhoneUseType,
                        PhoneUseTypeSourceSystemRecordId = item.PhoneUseTypeSourceSystemRecordId,
                            PhoneUseTypeSourceSystemRecordId_Status = item.PhoneUseTypeSourceSystemRecordId_Status,
                            PhoneUseTypeSourceSystemRecordId_OriginalValue = previousitem.PhoneUseTypeSourceSystemRecordId,
                        PhoneUseTypeMasterId = item.PhoneUseTypeMasterId,
                            PhoneUseTypeMasterId_Status = item.PhoneUseTypeMasterId_Status,
                            PhoneUseTypeMasterId_OriginalValue = previousitem.PhoneUseTypeMasterId,
                        PhoneIsPrimary = item.PhoneIsPrimary,
                            PhoneIsPrimary_Status = item.PhoneIsPrimary_Status,
                            PhoneIsPrimary_OriginalValue = previousitem.PhoneIsPrimary.ToString(),


                        HistoryDate = item.RecordDate
                    });
                };
                #endregion
                //TODO get rid of this logic when function is changed
                if (viewModel.PhoneIsPrimary == null)
                {
                    viewModel.PhoneIsPrimary = viewModel.TempPhoneIsPrimary;
                }
                if (viewModel.PhoneIsPrimary == true)
                {
                    viewModel.TempPhoneIsPrimary = true;
                    viewModel.PhoneIsPrimary = viewModel.TempPhoneIsPrimary;
                }
                if (viewModel.PhoneIsPrimary == false)
                {
                    viewModel.TempPhoneIsPrimary = false;
                    viewModel.PhoneIsPrimary = viewModel.TempPhoneIsPrimary;
                }


                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve ConstituentPhoneEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion


        /* No Match/Compare for Constituent Phone at this time (07/28/2020) */


        /*  *** Phone Save ***
        ******************************************************************/
        #region Phone Save
        // POST: Student/ConstituentPhoneSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConstituentPhoneSave(ConstituentPhoneViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ConstituentPhoneChangeIntegrationRecord(model.SystemId, model.Id,
                        model.ConstituentSourceSystemRecordId, model.FirstName, model.LastName, model.UAPersonId, model.ConstituentMasterId,
                        model.PhoneNumber, model.Extension, model.PhoneMasterId,
                        model.Country, model.CountrySourceSystemRecordId, model.CountryMasterId,
                        model.PhoneLineType, model.PhoneLineTypeSourceSystemRecordId, model.PhoneLineTypeMasterId,
                        model.PhoneUseType, model.PhoneUseTypeSourceSystemRecordId, model.PhoneUseTypeMasterId, model.PhoneIsPrimary,
                        User.Identity.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve ConstituentPhoneSave");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(ConstituentPhoneList));
            }
            else
            {
                model.Title = "Constituent Phone";
                model.PageId = "constituentPhonePage";
                model.ActiveClass = "ConstituentPhone";
                model.Message = "Your Constituent Phone Page";
                model.User = User.Identity.Name;
                model.NavigationGroups = GetNavigationGroups();

                return View(nameof(ConstituentPhoneEdit), model);
            }


        }
        #endregion

        /*  *** Phone Revalidate ***
        ******************************************************************/
        #region Phone Revalidate
        // POST: Student/ConstituentPhoneRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConstituentPhoneRevalidate(ConstituentPhoneViewModel model)
        {
            if (model.IsValid())
            {
                try
                {

                    // Check if Dropdown have MasterId - If they dont, empty out the associated attributes per dropdown before revalidating
                    // ConstituentMasterId
                    if (string.IsNullOrEmpty(model.ConstituentMasterId) && (!string.IsNullOrEmpty(model.ConstituentMasterId)))
                    {
                        model.ConstituentSourceSystemRecordId = null;
                        model.ConstituentMasterId = null;

                        model.IsChanged = true;
                    }

                    // CountryMasterId
                    if (string.IsNullOrEmpty(model.CountryMasterId) && (!string.IsNullOrEmpty(model.CountrySourceSystemRecordId) || !string.IsNullOrEmpty(model.Country)))
                    {
                        model.CountrySourceSystemRecordId = null;
                        model.Country = null;

                        model.IsChanged = true;
                    }

                    // PhoneLineTypeMasterId
                    if (string.IsNullOrEmpty(model.PhoneLineTypeMasterId) && (!string.IsNullOrEmpty(model.PhoneLineTypeSourceSystemRecordId) || !string.IsNullOrEmpty(model.PhoneLineType)))
                    {
                        model.PhoneLineTypeSourceSystemRecordId = null;
                        model.PhoneLineType = null;

                        model.IsChanged = true;
                    }

                    // PhoneUseTypeMasterId
                    if (string.IsNullOrEmpty(model.PhoneUseTypeMasterId) && (!string.IsNullOrEmpty(model.PhoneUseTypeSourceSystemRecordId) || !string.IsNullOrEmpty(model.PhoneUseType)))
                    {
                        model.PhoneUseTypeSourceSystemRecordId = null;
                        model.PhoneUseType = null;

                        model.IsChanged = true;
                    }

                    if (model.IsChanged)
                    {
                        _context.ConstituentPhoneChangeIntegrationRecord(model.SystemId, model.Id,
                            model.ConstituentSourceSystemRecordId, model.FirstName, model.LastName, model.UAPersonId, model.ConstituentMasterId,
                            model.PhoneNumber, model.Extension, model.PhoneMasterId,
                            model.Country, model.CountrySourceSystemRecordId, model.CountryMasterId,
                            model.PhoneLineType, model.PhoneLineTypeSourceSystemRecordId, model.PhoneLineTypeMasterId,
                            model.PhoneUseType, model.PhoneUseTypeSourceSystemRecordId, model.PhoneUseTypeMasterId, model.PhoneIsPrimary,
                            User.Identity.Name);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve ConstituentPhoneRevalidate");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(ConstituentPhoneList));
            }
            return View(model);
        }
        #endregion

        /*  *** Ignore/Remove ***
        ******************************************************************/
        #region Ignore/Remove
        public IActionResult ConstituentPhoneIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load ConstituentPhoneIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentPhoneList));
        }
        #endregion

        #endregion

        #region Email (Contact)

        /*  *** List View ***
        ******************************************************************/
        #region List
        // GET: Constituent/ConstituentEmailList
        public IActionResult ConstituentEmailList()
        {
            var model = new ConstituentEmailListViewModel()
            {
                Title = "Constituent Email",
                PageId = "constituentEmailPage",
                ActiveClass = "ConstituentEmail",
                Message = "Your Constituent Email Page",
                Integration = "Constituent Email",
                IntegrationId = 19,
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetConstituentEmailList(AjaxDataTableRequest request)
        {
            try
            {
                var ConstituentEmails = _context.ConstituentEmailRemediationList.AsQueryable();

                int recordsTotal = ConstituentEmails.Count();

                var ConstituentEmailList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? ConstituentEmails
                                                : ConstituentEmails.Where(s =>
                                                s.FirstName.Contains(request.searchValue) ||
                                                s.LastName.Contains(request.searchValue) ||
                                                s.EmailAddress.Contains(request.searchValue) ||
                                                s.EmailAddressUseType.Contains(request.searchValue) ||
                                                s.ErrorCategories.Contains(request.searchValue) || s.SystemName.Contains(request.searchValue))
                                            )
                                            .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                            .ToListAsync();

                int recordsFiltered = ConstituentEmailList.Count();

                var ConstituentEmailRemediationList = new List<ConstituentEmailRemediationListItemViewModel>();

                foreach (var item in ConstituentEmailList.Skip(request.start).Take(request.length))
                {
                    ConstituentEmailRemediationList.Add(new ConstituentEmailRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        SystemName = item.SystemName,
                        ErrorCategories = item.ErrorCategories,
                        ErrorCount = item.ErrorCount,
                        IntegrationDate = item.IntegrationDate,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate,
                        RecordStatus = item.RecordStatus,

                        Name = string.Format("{0} {1}", item.FirstName, item.LastName),
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        EmailAddress = item.EmailAddress,
                        EmailAddressUseType = item.EmailAddressUseType
                    });
                }

                var data = ConstituentEmailRemediationList.ToList();

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
                _logger.LogError(ex, "Unable to retrieve ConstituentEmailList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Edit View ***
        ******************************************************************/
        #region Edit
        // GET: Student/ConstituentEmailEdit
        public IActionResult ConstituentEmailEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetConstituentEmailHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var integrationDetail = history.First();
                var integrationSource = history.Last();

                var viewModel = new ConstituentEmailViewModel()
                {
                    Title = "Constituent Phone",
                    PageId = "constituentEmailPage",
                    ActiveClass = "ConstituentEmail",
                    Message = "Your Constituent Email Page",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = integrationDetail.Id,
                    System = integrationDetail.SystemName,
                    SystemId = integrationDetail.SystemId,
                    Integration = integrationDetail.IntegrationName,
                    IntegrationId = integrationDetail.IntegrationId,
                    IntegrationDate = integrationDetail.IntegrationDate,
                    CreatedDate = integrationDetail.RecordDate,
                    SourceRecordId = integrationDetail.SourceRecordId,
                    CreatedOnDT = integrationDetail.RecordDate,

                    HistoryData = new List<ConstituentEmailHistoryViewModel>(),

                    #region Constituent Email Details

                    ConstituentSourceSystemRecordId = integrationDetail.ConstituentSourceSystemRecordId,
                        ConstituentSourceSystemRecordId_BusinessName = integrationDetail.ConstituentSourceSystemRecordId_BusinessName,
                        ConstituentSourceSystemRecordId_BusinessDescription = integrationDetail.ConstituentSourceSystemRecordId_BusinessDescription,
                        ConstituentSourceSystemRecordId_OriginalValue = integrationSource.ConstituentSourceSystemRecordId,
                        ConstituentSourceSystemRecordId_Status = integrationDetail.ConstituentSourceSystemRecordId_Status,
                        ConstituentSourceSystemRecordId_Source = integrationDetail.ConstituentSourceSystemRecordId_Source,
                        ConstituentSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.ConstituentSourceSystemRecordId_Source }),
                    FirstName = integrationDetail.FirstName,
                        FirstName_BusinessName = integrationDetail.FirstName_BusinessName,
                        FirstName_BusinessDescription = integrationDetail.FirstName_BusinessDescription,
                        FirstName_OriginalValue = integrationSource.FirstName,
                        FirstName_Status = integrationDetail.FirstName_Status,
                        FirstName_Source = integrationDetail.FirstName_Source,
                        FirstName_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.FirstName_Source }),
                    LastName = integrationDetail.LastName,
                        LastName_BusinessName = integrationDetail.LastName_BusinessName,
                        LastName_BusinessDescription = integrationDetail.LastName_BusinessDescription,
                        LastName_OriginalValue = integrationSource.LastName,
                        LastName_Status = integrationDetail.LastName_Status,
                        LastName_Source = integrationDetail.LastName_Source,
                        LastName_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.LastName_Source }),
                    UAPersonId = integrationDetail.UAPersonId,
                        UAPersonId_BusinessName = integrationDetail.UAPersonId_BusinessName,
                        UAPersonId_BusinessDescription = integrationDetail.UAPersonId_BusinessDescription,
                        UAPersonId_OriginalValue = integrationSource.UAPersonId,
                        UAPersonId_Status = integrationDetail.UAPersonId_Status,
                        UAPersonId_Source = integrationDetail.UAPersonId_Source,
                        UAPersonId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.UAPersonId_Source }),
                    ConstituentMasterId = integrationDetail.ConstituentMasterId,
                        ConstituentMasterId_BusinessName = integrationDetail.ConstituentMasterId_BusinessName,
                        ConstituentMasterId_BusinessDescription = integrationDetail.ConstituentMasterId_BusinessDescription,
                        ConstituentMasterId_OriginalValue = integrationSource.ConstituentMasterId,
                        ConstituentMasterId_Status = integrationDetail.ConstituentMasterId_Status,
                        ConstituentMasterId_Source = integrationDetail.ConstituentMasterId_Source,
                        ConstituentMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.ConstituentMasterId_Source, integrationDetail.FirstName_Source, integrationDetail.LastName_Source, integrationDetail.UAPersonId_Source, integrationDetail.ConstituentSourceSystemRecordId_Source }),

                    EmailAddress = integrationDetail.EmailAddress,
                        EmailAddress_BusinessName = integrationDetail.EmailAddress_BusinessName,
                        EmailAddress_BusinessDescription = integrationDetail.EmailAddress_BusinessDescription,
                        EmailAddress_OriginalValue = integrationSource.EmailAddress,
                        EmailAddress_Status = integrationDetail.EmailAddress_Status,
                        EmailAddress_Source = integrationDetail.EmailAddress_Source,
                        EmailAddress_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.EmailAddress_Source }),
                    EmailAddressMasterId = integrationDetail.EmailAddressMasterId,
                        EmailAddressMasterId_BusinessName = integrationDetail.EmailAddressMasterId_BusinessName,
                        EmailAddressMasterId_BusinessDescription = integrationDetail.EmailAddressMasterId_BusinessDescription,
                        EmailAddressMasterId_OriginalValue = integrationSource.EmailAddressMasterId,
                        EmailAddressMasterId_Status = integrationDetail.EmailAddressMasterId_Status,
                        EmailAddressMasterId_Source = integrationDetail.EmailAddressMasterId_Source,
                        EmailAddressMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.EmailAddressMasterId_Source, integrationDetail.EmailAddress_Source }),

                    EmailAddressUseType = integrationDetail.EmailAddressUseType,
                        EmailAddressUseType_BusinessName = integrationDetail.EmailAddressUseType_BusinessName,
                        EmailAddressUseType_BusinessDescription = integrationDetail.EmailAddressUseType_BusinessDescription,
                        EmailAddressUseType_OriginalValue = integrationSource.EmailAddressUseType,
                        EmailAddressUseType_Status = integrationDetail.EmailAddressUseType_Status,
                        EmailAddressUseType_Source = integrationDetail.EmailAddressUseType_Source,
                        EmailAddressUseType_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.EmailAddressUseType_Source }),
                    EmailAddressUseTypeSourceSystemRecordId = integrationDetail.EmailAddressUseTypeSourceSystemRecordId,
                        EmailAddressUseTypeSourceSystemRecordId_BusinessName = integrationDetail.EmailAddressUseTypeSourceSystemRecordId_BusinessName,
                        EmailAddressUseTypeSourceSystemRecordId_BusinessDescription = integrationDetail.EmailAddressUseTypeSourceSystemRecordId_BusinessDescription,
                        EmailAddressUseTypeSourceSystemRecordId_OriginalValue = integrationSource.EmailAddressUseTypeSourceSystemRecordId,
                        EmailAddressUseTypeSourceSystemRecordId_Status = integrationDetail.EmailAddressUseTypeSourceSystemRecordId_Status,
                        EmailAddressUseTypeSourceSystemRecordId_Source = integrationDetail.EmailAddressUseTypeSourceSystemRecordId_Source,
                        EmailAddressUseTypeSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.EmailAddressUseTypeSourceSystemRecordId_Source, integrationDetail.EmailAddressUseType_Source }),
                    EmailAddressUseTypeMasterId = integrationDetail.EmailAddressUseTypeMasterId,
                        EmailAddressUseTypeMasterId_BusinessName = integrationDetail.EmailAddressUseTypeMasterId_BusinessName,
                        EmailAddressUseTypeMasterId_BusinessDescription = integrationDetail.EmailAddressUseTypeMasterId_BusinessDescription,
                        EmailAddressUseTypeMasterId_OriginalValue = integrationSource.EmailAddressUseTypeMasterId,
                        EmailAddressUseTypeMasterId_Status = integrationDetail.EmailAddressUseTypeMasterId_Status,
                        EmailAddressUseTypeMasterId_Source = integrationDetail.EmailAddressUseTypeMasterId_Source,
                        EmailAddressUseTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.EmailAddressUseTypeMasterId_Source, integrationDetail.EmailAddressUseType_Source }),
                    EmailIsPrimary = integrationDetail.EmailIsPrimary,
                        EmailIsPrimary_BusinessName = integrationDetail.EmailIsPrimary_BusinessName,
                        EmailIsPrimary_BusinessDescription = integrationDetail.EmailIsPrimary_BusinessDescription,
                        EmailIsPrimary_OriginalValue = integrationSource.EmailIsPrimary.ToString(),
                        EmailIsPrimary_Status = integrationDetail.EmailIsPrimary_Status,
                        EmailIsPrimary_Source = integrationDetail.EmailIsPrimary_Source,
                        EmailIsPrimary_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.EmailIsPrimary_Source, integrationDetail.EmailAddressUseType_Source }),

                    #endregion
                       
                    #region Dropdowns
                    ConstituentList = GetStudentList(),
                    EmailAddressUseTypeList = GetEmailAddressUseTypeList(),
                    #endregion
                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);
                    viewModel.HistoryData.Add(new ConstituentEmailHistoryViewModel()
                    {
                        ConstituentSourceSystemRecordId = item.ConstituentSourceSystemRecordId,
                            ConstituentSourceSystemRecordId_Status = item.ConstituentSourceSystemRecordId_Status,
                            ConstituentSourceSystemRecordId_OriginalValue = previousitem.ConstituentSourceSystemRecordId,
                        FirstName = item.FirstName,
                            FirstName_Status = item.FirstName_Status,
                            FirstName_OriginalValue = previousitem.FirstName,
                        LastName = item.LastName,
                            LastName_Status = item.LastName_Status,
                            LastName_OriginalValue = previousitem.LastName,
                        UAPersonId = item.UAPersonId,
                            UAPersonId_Status = item.UAPersonId_Status,
                            UAPersonId_OriginalValue = previousitem.UAPersonId,
                        ConstituentMasterId = item.ConstituentMasterId,
                            ConstituentMasterId_Status = item.ConstituentMasterId_Status,
                            ConstituentMasterId_OriginalValue = previousitem.ConstituentMasterId,

                        EmailAddress = item.EmailAddress,
                            EmailAddress_Status = item.EmailAddress_Status,
                            EmailAddress_OriginalValue = previousitem.EmailAddress,
                        EmailAddressMasterId = item.EmailAddressMasterId,
                            EmailAddressMasterId_Status = item.EmailAddressMasterId_Status,
                            EmailAddressMasterId_OriginalValue = previousitem.EmailAddressMasterId,

                        EmailAddressUseType = item.EmailAddressUseType,
                            EmailAddressUseType_Status = item.EmailAddressUseType_Status,
                            EmailAddressUseType_OriginalValue = previousitem.EmailAddressUseType,
                        EmailAddressUseTypeSourceSystemRecordId = item.EmailAddressUseTypeSourceSystemRecordId,
                            EmailAddressUseTypeSourceSystemRecordId_Status = item.EmailAddressUseTypeSourceSystemRecordId_Status,
                            EmailAddressUseTypeSourceSystemRecordId_OriginalValue = previousitem.EmailAddressUseTypeSourceSystemRecordId,
                        EmailAddressUseTypeMasterId = item.EmailAddressUseTypeMasterId,
                            EmailAddressUseTypeMasterId_Status = item.EmailAddressUseTypeMasterId_Status,
                            EmailAddressUseTypeMasterId_OriginalValue = previousitem.EmailAddressUseTypeMasterId,
                        EmailIsPrimary = item.EmailIsPrimary,
                            EmailIsPrimary_Status = item.EmailIsPrimary_Status,
                            EmailIsPrimary_OriginalValue = previousitem.EmailIsPrimary.ToString(),


                        HistoryDate = item.RecordDate
                    });
                };
                #endregion
                //TODO get rid of this logic when function is changed
                if (viewModel.EmailIsPrimary == null)
                {
                    viewModel.EmailIsPrimary = viewModel.TempEmailIsPrimary;
                }
                if (viewModel.EmailIsPrimary == true)
                {
                    viewModel.TempEmailIsPrimary = true;
                    viewModel.EmailIsPrimary = viewModel.TempEmailIsPrimary;
                }
                if (viewModel.EmailIsPrimary == false)
                {
                    viewModel.TempEmailIsPrimary = false;
                    viewModel.EmailIsPrimary = viewModel.TempEmailIsPrimary;
                }


                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve ConstituentEmailEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion


        /* No Match/Compare for Constituent Phone at this time (07/28/2020) */


        /*  *** Email Save ***
        ******************************************************************/
        #region Email Save
        // POST: Student/ConstituentEmailSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConstituentEmailSave(ConstituentEmailViewModel model)
        {
            if (model.IsValid())
            {
                model.EmailIsPrimary = model.TempEmailIsPrimary;
                try
                {
                    _context.ConstituentEmailChangeIntegrationRecord(model.SystemId, model.Id,
                        model.ConstituentSourceSystemRecordId, model.ConstituentMasterId, model.FirstName, model.LastName, model.UAPersonId,
                        model.EmailAddress, model.EmailAddressMasterId,
                        model.EmailAddressUseType, model.EmailAddressUseTypeSourceSystemRecordId, model.EmailAddressUseTypeMasterId, model.EmailIsPrimary,
                        User.Identity.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve ConstituentEmailSave");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(ConstituentEmailList));
            }
            else
            {
                model.Title = "Constituent Email";
                model.PageId = "constituentEmailPage";
                model.ActiveClass = "ConstituentEmail";
                model.Message = "Your Constituent Email Page";
                model.User = User.Identity.Name;
                model.NavigationGroups = GetNavigationGroups();

                return View(nameof(ConstituentEmailEdit), model);
            }


        }
        #endregion

        /*  *** Email Revalidate ***
        ******************************************************************/
        #region Email Revalidate
        // POST: Student/ConstituentEmailRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConstituentEmailRevalidate(ConstituentEmailViewModel model)
        {
            if (model.IsValid())
            {
                try
                {

                    // Check if Dropdown have MasterId - If they dont, empty out the associated attributes per dropdown before revalidating
                    // ConstituentMasterId
                    if (string.IsNullOrEmpty(model.ConstituentMasterId) && (!string.IsNullOrEmpty(model.ConstituentMasterId)))
                    {
                        model.ConstituentSourceSystemRecordId = null;
                        model.ConstituentMasterId = null;

                        model.IsChanged = true;
                    }

                    // EmailAddressUseTypeMasterId
                    if (string.IsNullOrEmpty(model.EmailAddressUseTypeMasterId) && (!string.IsNullOrEmpty(model.EmailAddressUseTypeSourceSystemRecordId) || !string.IsNullOrEmpty(model.EmailAddressUseType)))
                    {
                        model.EmailAddressUseTypeSourceSystemRecordId = null;
                        model.EmailAddressUseType = null;

                        model.IsChanged = true;
                    }

                    if (model.IsChanged)
                    {
                        _context.ConstituentEmailChangeIntegrationRecord(model.SystemId, model.Id,
                            model.ConstituentSourceSystemRecordId, model.ConstituentMasterId, model.FirstName, model.LastName, model.UAPersonId,
                            model.EmailAddress, model.EmailAddressMasterId,
                            model.EmailAddressUseType, model.EmailAddressUseTypeSourceSystemRecordId, model.EmailAddressUseTypeMasterId, model.EmailIsPrimary,
                            User.Identity.Name);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve ConstituentEmailRevalidate");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(ConstituentEmailList));
            }
            return View(model);
        }
        #endregion

        /*  *** Ignore/Remove ***
        ******************************************************************/
        #region Ignore/Remove
        public IActionResult ConstituentEmailIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load ConstituentEmailIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentEmailList));
        }
        #endregion

        #region Remove Source
        public async Task<IActionResult> ConstituentEmailRemoveSource(string sourceRecordId)
        {
            try
            {
                await _domainService.MasterDataWebService.RemoveEmailFromSource(sourceRecordId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing email from source. Email source system record id [{sourceRecordId}]");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentEmailList));
        }

        public async Task<IActionResult> ConstituentPhoneRemoveSource(string sourceRecordId)
        {
            try
             {
                await _domainService.MasterDataWebService.RemovePhoneFromSource(sourceRecordId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing phone from source. Phone source system record id [{sourceRecordId}]");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentPhoneList));
        }

        public async Task<IActionResult> ConstituentAddressRemoveSource(string sourceRecordId)
        {
            try
            {
                await _domainService.MasterDataWebService.RemoveAddressFromSource(sourceRecordId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing address from source. Address source system record id [{sourceRecordId}]");
                return RedirectToAction("SystemError", "Error");
            }
            
            return RedirectToAction("ConstituentAddressList", "ConstituentAddress");
        }
        public async Task<IActionResult> ConstituentIndividualEmailRemoveSource(long Id, int SystemId, string emailAddressSourceSystemRecordId) //, ConstituentViewModel model)
        {
            try
            {
                var tempModel = SetupConstituentViewModel(Id, SystemId);
                await _domainService.MasterDataWebService.RemoveEmailFromSource(emailAddressSourceSystemRecordId);

                tempModel.EmailAddress = null;
                tempModel.EmailAddressSourceSystemRecordId = null;
                tempModel.EmailAddressMasterId = null;
                tempModel.EmailAddressUseType = null;
                tempModel.EmailAddressUseTypeSourceSystemRecordId = null;
                tempModel.EmailAddressUseTypeMasterId = null;
                tempModel.EmailIsPrimary = null;

                SaveConstituent(tempModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing individual primary email from source. Record id [{Id}]. System Id [{SystemId}] Email source system record id [{emailAddressSourceSystemRecordId}]");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentEdit), new { Id = Id, SystemId = SystemId });
        }

        public async Task<IActionResult> ConstituentIndividualPhoneRemoveSource(long Id, int SystemId, string phoneNumberSourceSystemRecordId) //, ConstituentViewModel model)
        {
            try
            {
                var tempModel = SetupConstituentViewModel(Id, SystemId);
                await _domainService.MasterDataWebService.RemovePhoneFromSource(phoneNumberSourceSystemRecordId);

                tempModel.PhoneNumber = null;
                tempModel.PhoneNumberSourceSystemRecordId = null;
                tempModel.PhoneExtension = null;
                tempModel.PhoneMasterId = null;
                tempModel.PhoneCountryCode = null;
                tempModel.PhoneCountrySourceSystemRecordId = null;
                tempModel.PhoneCountryMasterRecordId = null;
                tempModel.PhoneLineType = null;
                tempModel.PhoneLineTypeSourceSystemRecordId = null;
                tempModel.PhoneLineTypeMasterRecordId = null;
                tempModel.PhoneUseType = null;
                tempModel.PhoneUseTypeSourceSystemRecordId = null;
                tempModel.PhoneUseTypeMasterId = null;
                tempModel.PhoneIsPrimary = null;

                SaveConstituent(tempModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing individual primary phone from source. Record id [{Id}]. System Id [{SystemId}] Phone source system record id [{phoneNumberSourceSystemRecordId}] ");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(ConstituentEdit), new {Id = Id, SystemId = SystemId});
        }

        public async Task<IActionResult> ConstituentIndivdualAddressRemoveSource(long Id, int SystemId, string addressSourceSystemRecordId) //, ConstituentViewModel model)
        {
            try
            {
                var tempModel = SetupConstituentViewModel(Id, SystemId);
                await _domainService.MasterDataWebService.RemoveAddressFromSource(addressSourceSystemRecordId);

                tempModel.Address = null;
                tempModel.AddressSourceSystemRecordId = null;
                tempModel.AddressMasterId = null;
                tempModel.AddressUseType = null;
                tempModel.AddressUseTypeSourceSystemRecordId = null;
                tempModel.AddressUseTypeMasterId = null;
                tempModel.AddressIsPrimary = null;
                tempModel.City = null;
                tempModel.PostalCode = null;
                tempModel.State = null;
                tempModel.StateSourceSystemRecordId = null;
                tempModel.StateMasterId = null;
                tempModel.Country = null;
                tempModel.CountrySourceSystemRecordId = null;
                tempModel.CountryMasterId = null;

                SaveConstituent(tempModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error removing individual primary address from source. Record id [{Id}]. System Id [{SystemId}] Address source system record id [{addressSourceSystemRecordId}]");
                return RedirectToAction("SystemError", "Error");
            }
            return RedirectToAction(nameof(ConstituentEdit), new { Id = Id, SystemId = SystemId });
        }




        #endregion


        #endregion

    }
}