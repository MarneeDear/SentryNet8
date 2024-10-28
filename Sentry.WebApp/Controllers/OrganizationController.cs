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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sentry.WebApp.Controllers
{

    [AuthorizeHumanResources]
    public class OrganizationController : IntegrationController
    {
       public OrganizationController(AppDbContext context, 
           DwDbContext dwContext, 
           ILogger<OrganizationController> logger, 
           IConfiguration configuration,
           IDomainService domainService) : base(context, dwContext, logger, configuration, domainService) { }

        #region Organizational Hierarchy
        /*******************************************************************************************************************************
			* Organizational Hierarchy
		*******************************************************************************************************************************/

        public async Task<IActionResult> GetOrganizationalUnitTreeData()
        {
            try
            {
                var organizationalUnitTreeNodes = await _context.OrganizationalUnitTreeItems.OrderBy(o => o.parentOrgUnit).ThenBy(o => o.name).ToListAsync();
                List<OrganizationalUnitTreeItem> organizationalUnitTree = new List<OrganizationalUnitTreeItem>();
                foreach (OrganizationalUnitTreeItem node in organizationalUnitTreeNodes.Where(s => s.parentOrgUnit == null))
                {
                    AddChildNodes(node, organizationalUnitTreeNodes);
                    organizationalUnitTree.Add(node);
                }
                string jsonData = JsonConvert.SerializeObject(organizationalUnitTree);
                jsonData = jsonData.Replace("\"parent\":null,", string.Empty);

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message); //RedirectToAction("SystemError", "Error");
            }
        }

        private void AddChildNodes(OrganizationalUnitTreeItem topNode, List<OrganizationalUnitTreeItem> nodeList)
        {
            List<OrganizationalUnitTreeItem> childNodes = new List<OrganizationalUnitTreeItem>();
            foreach (OrganizationalUnitTreeItem node in nodeList)
            {
                //if (node.parentOrgUnit == topNode.code)
                if (node.parent == topNode.code)
                {
                    AddChildNodes(node, nodeList);
                    node.a_attr = new A_attr() { rel = "tooltip", title = $"Code: {node.code} // Org Unit Type: {node.orgUnitType}" };
                    childNodes.Add(node);
                }
            }
            topNode.children = childNodes;
        }

        public IActionResult OrganizationalHierarchy()
        {
            try
            {
                var model = new OrganizationalHierarchyListViewModel()
                {
                    PageId = "organizationalHierarchyPage",
                    PageWrapperClass = "toggled",
                    ActiveClass = "active",
                    Title = "Organizational Hierarchy",
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                };

                return View(model);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        // GET: Organization/OrganizationalHierarchyEdit
        public IActionResult OrganizationalHierarchyEdit(long Id, int SystemId)
        {
            try
            {
                var viewModel = new OrganizationalHierarchyViewModel()
                {
                    Id = -999,
                    System = "UAIR",
                    SystemId = 5,
                    Integration = "",
                    IntegrationId = 9,
                    IntegrationDate = new DateTime(2019, 9, 15),
                    CreatedDate = new DateTime(2019, 9, 22),
                    SourceRecordId = "219876532168465",
                    CreatedOnDT = new DateTime(2019, 9, 22),
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                    RecordStatus = "Good",

                    Title = "Organizational Hierarchy Details",
                    PageId = "organizationalHierarchyPage",
                    ActiveClass = "organizationalHierarchy",
                    Message = "Your Organizational Hierarchy Page",
                    PageWrapperClass = "toggled",
                    OuId = 901,
                    OuName = "University of Arizona",
                        OuName_BusinessName = "Unit Name",
                        OuName_BusinessDescription = "Name of Organizational Unit",
                        OuName_AttributeId = "sourceField_NameId",
                        OuName_Status = "Good",
                        OuName_Source = "UAIR",
                    //OuTitle = OuCode + " // " + OrgUnitType,
                    OuIcon = "fa fa-folder",
                    OuCode = "545",
                        OuCode_BusinessName = "MDS Code",
                        OuCode_BusinessDescription = "Master Data Code",
                        OuCode_AttributeId = "sourceField_CodeId",
                        OuCode_Status = "Good",
                        OuCode_Source = "UAIR",
                        OuCode_Date = new DateTime(2019, 9, 22),
                    OrgUnitCode = "SVPS",
                        OrgUnitCode_BusinessName = "Organizational Unit Code",
                        OrgUnitCode_BusinessDescription = "Code associated with the Organizational Unit",
                        OrgUnitCode_AttributeId = "sourceField_OrgUnitCodeId",
                        OrgUnitCode_Status = "Bad",
                        OrgUnitCode_Source = "UAIR",
                        OrgUnitCode_Date = new DateTime(2019, 9, 22),
                    OrgUnitType = "Executive",
                        OrgUnitType_BusinessName = "Organizational Unit Type",
                        OrgUnitType_BusinessDescription = "Type of Organizational Unit",
                        OrgUnitType_AttributeId = "sourceField_OrgUnitTypeId",
                        OrgUnitType_Status = "Changed",
                        OrgUnitType_Source = "UAIR",
                        OrgUnitType_Date = new DateTime(2019, 9, 22),
                    Org = "Executive",
                        Org_BusinessName = "Organization",
                        Org_BusinessDescription = "Type of Organizational Unit",
                        Org_AttributeId = "sourceField_OrgId",
                        Org_Status = "Good",
                        Org_Source = "UAIR",
                        Org_Date = new DateTime(2019, 9, 22),
                    ParentOrgUnit = "Executive",
                        ParentOrgUnit_BusinessName = "Parent Organizational Unit",
                        ParentOrgUnit_BusinessDescription = "Parent of the Organizational Unit",
                        ParentOrgUnit_AttributeId = "sourceField_ParentOrgUnitId",
                        ParentOrgUnit_Status = "Good",
                        ParentOrgUnit_Source = "UAIR",
                        ParentOrgUnit_Date = new DateTime(2019, 9, 22),
                    Avatar = "../img/image-placeholder-ua.jpg"
                };

                viewModel = new OrganizationalHierarchyViewModel()
                {
                    Id = -888,
                    System = "UAIR",
                    SystemId = 5,
                    Integration = "",
                    IntegrationId = 9,
                    IntegrationDate = new DateTime(2019, 9, 15),
                    CreatedDate = new DateTime(2019, 9, 22),
                    SourceRecordId = "987621654984",
                    CreatedOnDT = new DateTime(2019, 9, 22),
                    RecordStatus = "Bad",

                    Title = "Organizational Hierarchy Details",
                    PageId = "organizationalHierarchyPage",
                    ActiveClass = "organizationalHierarchy",
                    Message = "Your Organizational Hierarchy Page",
                    PageWrapperClass = "toggled",
                    OuId = 801,
                    OuName = "University of Arizona",
                        OuName_BusinessName = "Unit Name",
                        OuName_BusinessDescription = "Name of Organizational Unit",
                        OuName_AttributeId = "sourceField_NameId",
                        OuName_Status = "Good",
                        OuName_Source = "UAIR",
                    //OuTitle = OuCode + " // " + OrgUnitType,
                    OuIcon = "fa fa-folder",
                    OuCode = "545",
                        OuCode_BusinessName = "MDS Code",
                        OuCode_BusinessDescription = "Master Data Code",
                        OuCode_AttributeId = "sourceField_CodeId",
                        OuCode_Status = "Good",
                        OuCode_Source = "UAIR",
                        OuCode_Date = new DateTime(2019, 9, 22),
                    OrgUnitCode = "SVPS",
                        OrgUnitCode_BusinessName = "Organizational Unit Code",
                        OrgUnitCode_BusinessDescription = "Code associated with the Organizational Unit",
                        OrgUnitCode_AttributeId = "sourceField_OrgUnitCodeId",
                        OrgUnitCode_Status = "Bad",
                        OrgUnitCode_Source = "UAIR",
                        OrgUnitCode_Date = new DateTime(2019, 9, 22),
                    OrgUnitType = "Executive",
                        OrgUnitType_BusinessName = "Organizational Unit Type",
                        OrgUnitType_BusinessDescription = "Type of Organizational Unit",
                        OrgUnitType_AttributeId = "sourceField_OrgUnitTypeId",
                        OrgUnitType_Status = "Good",
                        OrgUnitType_Source = "UAIR",
                        OrgUnitType_Date = new DateTime(2019, 9, 22),
                    Org = "Executive",
                        Org_BusinessName = "Organization",
                        Org_BusinessDescription = "Type of Organizational Unit",
                        Org_AttributeId = "sourceField_OrgId",
                        Org_Status = "Changed",
                        Org_Source = "UAIR",
                        Org_Date = new DateTime(2019, 9, 22),
                    ParentOrgUnit = "Executive",
                        ParentOrgUnit_BusinessName = "Parent Organizational Unit",
                        ParentOrgUnit_BusinessDescription = "Parent of the Organizational Unit",
                        ParentOrgUnit_AttributeId = "sourceField_ParentOrgUnitId",
                        ParentOrgUnit_Status = "Good",
                        ParentOrgUnit_Source = "UAIR",
                        ParentOrgUnit_Date = new DateTime(2019, 9, 22),
                    Avatar = "../img/image-placeholder-ua.jpg"
                };

                //return View(viewModel);
                return PartialView("OrganizationalHierarchyEdit", viewModel);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        #endregion


        #region Office Location
        /*******************************************************************************************************************************
			* Office Location
		*******************************************************************************************************************************/
        public async Task<IActionResult> OfficeLocationList()
        {
            try
            {
                var model = new OfficeLocationListViewModel()
                {
                    Title = "Office Location",
                    PageId = "officeLocationPage",
                    ActiveClass = "Organization",
                    Message = "Your Office Location Page",
                    Integration = "Office Location",
                    IntegrationId = 1,
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                    RemediationList = new List<OfficeLocationRemediationListItemViewModel>()
                };

                var list = await _context.OfficeLocationRemediationList.ToListAsync();
                foreach (var item in list)
                {
                    model.RemediationList.Add(new OfficeLocationRemediationListItemViewModel()
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
                        BuildingCode = item.BuildingCode,
                        City = item.City
                    });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        // GET: OfficeLocations/OfficeLocationEdit/5
        public async Task<IActionResult> OfficeLocationEdit(long Id)
        {
            try
            {
                var officeLocationDetail = await _context.OfficeLocationDetails.Where(e => e.Id == Id).SingleAsync();
                var sourceData = _context.OfficeLocationSourceData(officeLocationDetail.SystemId, Id);

                var viewModel = new OfficeLocationViewModel()
                {
                    Id = Id,
                    PageId = "officeLocationPage",
                    PageWrapperClass = "toggled",
                    ActiveClass = "Organization",
                    Title = "Office Location Details",
                    System = officeLocationDetail.SystemName,
                    SystemId = officeLocationDetail.SystemId,
                    Integration = officeLocationDetail.IntegrationName,
                    IntegrationId = officeLocationDetail.IntegrationId,
                    SourceRecordId = officeLocationDetail.SourceRecordId,
                    RecordStatus = officeLocationDetail.RecordStatus,
                    CreatedOnDT = officeLocationDetail.IntegrationDate,
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                    HistoryData = new List<OfficeLocationHistoryViewModel>(),

                    Name = officeLocationDetail.Name,
                    Name_Status = officeLocationDetail.Name_Status,
                    Name_OriginalValue = sourceData.Name_SourceValue,
                    Name_BusinessName = officeLocationDetail.Name_BusinessName,
                    Name_BusinessDescription = officeLocationDetail.Name_BusinessDescription,
                    Name_Source = officeLocationDetail.Name_Source,
                    Name_FieldId = officeLocationDetail.Name_FieldId,

                    BuildingCode = officeLocationDetail.BuildingCode,
                    BuildingCode_Status = officeLocationDetail.BuildingCode_Status,
                    BuildingCode_OriginalValue = sourceData.BuildingCode_SourceValue,
                    BuildingCode_BusinessName = officeLocationDetail.BuildingCode_BusinessName,
                    BuildingCode_BusinessDescription = officeLocationDetail.BuildingCode_BusinessDescription,
                    BuildingCode_Source = officeLocationDetail.BuildingCode_Source,
                    BuildingCode_FieldId = officeLocationDetail.BuildingCode_FieldId,

                    Address1 = officeLocationDetail.Address1,
                    Address1_Status = officeLocationDetail.Address1_Status,
                    Address1_OriginalValue = sourceData.Address1_SourceValue,
                    Address1_BusinessName = officeLocationDetail.Address1_BusinessName,
                    Address1_BusinessDescription = officeLocationDetail.Address1_BusinessDescription,
                    Address1_Source = officeLocationDetail.Address1_Source,
                    Address1_FieldId = officeLocationDetail.Address1_FieldId,

                    Address2 = officeLocationDetail.Address2,
                    Address2_Status = officeLocationDetail.Address2_Status,
                    Address2_OriginalValue = sourceData.Address2_SourceValue,
                    Address2_BusinessName = officeLocationDetail.Address2_BusinessName,
                    Address2_BusinessDescription = officeLocationDetail.Address2_BusinessDescription,
                    Address2_Source = officeLocationDetail.Address2_Source,
                    Address2_FieldId = officeLocationDetail.Address2_FieldId,

                    City = officeLocationDetail.City,
                    City_Status = officeLocationDetail.City_Status,
                    City_OriginalValue = sourceData.City_SourceValue,
                    City_BusinessName = officeLocationDetail.City_BusinessName,
                    City_BusinessDescription = officeLocationDetail.City_BusinessDescription,
                    City_Source = officeLocationDetail.City_Source,
                    City_FieldId = officeLocationDetail.City_FieldId,

                    State = officeLocationDetail.State,
                    State_Status = officeLocationDetail.State_Status,
                    State_OriginalValue = sourceData.State_SourceValue,
                    State_BusinessName = officeLocationDetail.State_BusinessName,
                    State_BusinessDescription = officeLocationDetail.State_BusinessDescription,
                    State_Source = officeLocationDetail.State_Source,
                    State_FieldId = officeLocationDetail.State_FieldId,

                    StateMasterId = officeLocationDetail.StateMasterId,
                    StateMasterId_Status = officeLocationDetail.StateMasterId_Status,
                    StateMasterId_OriginalValue = sourceData.StateMasterId_SourceValue,
                    StateMasterId_BusinessName = officeLocationDetail.StateMasterId_BusinessName,
                    StateMasterId_BusinessDescription = officeLocationDetail.StateMasterId_BusinessDescription,
                    StateMasterId_Source = officeLocationDetail.StateMasterId_Source,
                    StateMasterId_FieldId = officeLocationDetail.StateMasterId_FieldId,

                    PostalCode = officeLocationDetail.PostalCode,
                    PostalCode_Status = officeLocationDetail.PostalCode_Status,
                    PostalCode_OriginalValue = sourceData.PostalCode_SourceValue,
                    PostalCode_BusinessName = officeLocationDetail.PostalCode_BusinessName,
                    PostalCode_BusinessDescription = officeLocationDetail.PostalCode_BusinessDescription,
                    PostalCode_Source = officeLocationDetail.PostalCode_Source,
                    PostalCode_FieldId = officeLocationDetail.PostalCode_FieldId,

                    Country = officeLocationDetail.Country,
                    Country_Status = officeLocationDetail.Country_Status,
                    Country_OriginalValue = sourceData.Country_SourceValue,
                    Country_BusinessName = officeLocationDetail.Country_BusinessName,
                    Country_BusinessDescription = officeLocationDetail.Country_BusinessDescription,
                    Country_Source = officeLocationDetail.Country_Source,
                    Country_FieldId = officeLocationDetail.Country_FieldId,

                    CountryMasterId = officeLocationDetail.CountryMasterId,
                    CountryMasterId_Status = officeLocationDetail.CountryMasterId_Status,
                    CountryMasterId_OriginalValue = sourceData.CountryMasterId_SourceValue,
                    CountryMasterId_BusinessName = officeLocationDetail.CountryMasterId_BusinessName,
                    CountryMasterId_BusinessDescription = officeLocationDetail.CountryMasterId_BusinessDescription,
                    CountryMasterId_Source = officeLocationDetail.CountryMasterId_Source,
                    CountryMasterId_FieldId = officeLocationDetail.CountryMasterId_FieldId,
                };

                var historyData = await _context.OfficeLocationHistories.Where(s => s.RecordId == Id && s.SystemId == officeLocationDetail.SystemId).OrderByDescending(s => s.RecordDate).ToListAsync();
                foreach (OfficeLocationHistory row in historyData)
                {
                    viewModel.HistoryData.Add(new OfficeLocationHistoryViewModel()
                    {
                        Name = row.Name,
                        Name_Status = row.Name_Status,
                        BuildingCode = row.BuildingCode,
                        BuildingCode_Status = row.BuildingCode_Status,
                        Address1 = row.Address1,
                        Address1_Status = row.Address1_Status,
                        Address2 = row.Address2,
                        Address2_Status = row.Address2_Status,
                        City = row.City,
                        City_Status = row.City_Status,
                        State = row.State,
                        State_Status = row.State_Status,
                        PostalCode = row.PostalCode,
                        PostalCode_Status = row.PostalCode_Status,
                        Country = row.Country,
                        Country_Status = row.Country_Status,
                        HistoryDate = row.RecordDate
                    });
                }

                viewModel.StateList = GetStateList().ToList();
                viewModel.CountryList = GetCountryList().ToList();

                return View(viewModel);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        // POST: OfficeLocations/OfficeLocationsEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OfficeLocationSave(OfficeLocationViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeOfficeLocationIntegrationRecord(model.SystemId, model.Id, model.Name, model.BuildingCode, model.Address1, model.Address2, model.City, model.State, model.StateMasterId, model.PostalCode, model.Country, model.CountryMasterId);
                }
                catch (Exception ex)
                {
                    this.LogException(ex);
                    return RedirectToAction("SystemError", "Error");
                }
                //catch (Exception)
                //{
                //    throw;
                //}
                return RedirectToAction(nameof(OfficeLocationList));
            }
            return View(model);
        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OfficeLocationRevalidate(OfficeLocationViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeOfficeLocationIntegrationRecord(model.SystemId, model.Id, model.Name, model.BuildingCode, model.Address1, model.Address2, model.City, model.State, model.StateMasterId, model.PostalCode, model.Country, model.CountryMasterId);
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    this.LogException(ex);
                    return RedirectToAction("SystemError", "Error");
                }
                //catch (Exception)
                //{
                //    throw;
                //}
                return RedirectToAction(nameof(OfficeLocationList));
            }
            return View(model);
        }

        // GET:
        public async Task<IActionResult> OfficeLocationMatch(long Id)
        {
            try
            {
                var officeLocationDetail = await _context.OfficeLocationDetails.Where(e => e.Id == Id).SingleAsync();

                var viewModel = new OfficeLocationMatchViewModel()
                {
                    Id = Id,
                    PageId = "officeLocationPage",
                    PageWrapperClass = "toggled",
                    ActiveClass = "Organization",
                    Title = "Office Location Matching",
                    Integration = officeLocationDetail.IntegrationName,
                    IntegrationId = officeLocationDetail.IntegrationId,
                    IntegrationDate = officeLocationDetail.IntegrationDate,
                    Name = officeLocationDetail.Name,
                    Name_Weight = 50,
                    BuildingCode = officeLocationDetail.BuildingCode,
                    BuildingCode_Weight = 50,
                    Address1 = officeLocationDetail.Address1,
                    Address2 = officeLocationDetail.Address2,
                    City = officeLocationDetail.City,
                    State = officeLocationDetail.State,
                    PostalCode = officeLocationDetail.PostalCode,
                    Country = officeLocationDetail.Country,
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                    PossibleMatches = new List<OfficeLocationMatchSummaryViewModel>(),

                    CreatedOnDT = officeLocationDetail.IntegrationDate,
                    System = officeLocationDetail.SystemName,
                    SystemId = officeLocationDetail.SystemId,
                    SourceRecordId = officeLocationDetail.SourceRecordId
                };

                var PossibleMatches = _context.GetOfficeLocationPossibleMatches(officeLocationDetail.SystemId, Id);

                foreach (OfficeLocationPossibleMatch olpm in PossibleMatches)
                {
                    viewModel.PossibleMatches.Add(new OfficeLocationMatchSummaryViewModel()
                    {
                        MatchConfidence = olpm.MatchConfidence,
                        Name = olpm.OfficeLocationName,
                        BuildingCode = olpm.OfficeLocationCode,
                        MasterId = olpm.MasterId
                    });
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        // GET:
        public async Task<IActionResult> OfficeLocationCompare(long? Id, int SystemId, int MasterId)
        {
            try
            {
                var comparison = await _context.GetOfficeLocationComparisonDetail(SystemId, Id.Value, MasterId);
                var viewModel = new OfficeLocationCompareViewModel()
                {
                    Id = Id,
                    IntegrationId = 1,
                    SystemId = SystemId,
                    MasterId = MasterId,
                    IntegrationDate = comparison.IntegrationDate,
                    System = comparison.System,
                    SourceRecordId = comparison.SourceRecordId,
                    SourceRecordId_Compare = comparison.SourceRecordId_Compare,
                    Name = comparison.Name,
                    BuildingCode = comparison.BuildingCode,
                    Address1 = comparison.Address1,
                    Address2 = comparison.Address2,
                    City = comparison.City,
                    State = comparison.State,
                    PostalCode = comparison.PostalCode,
                    Country = comparison.Country,
                    Name_Compare = comparison.Name_Compare,
                    BuildingCode_Compare = comparison.BuildingCode_Compare,
                    Address1_Compare = comparison.Address1_Compare,
                    Address2_Compare = comparison.Address2_Compare,
                    City_Compare = comparison.City_Compare,
                    State_Compare = comparison.State_Compare,
                    PostalCode_Compare = comparison.PostalCode_Compare,
                    Country_Compare = comparison.Country_Compare
                };
                return PartialView("OfficeLocationCompare", viewModel);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        // GET:
        public async Task<IActionResult> OfficeLocationManualMatch(long Id, int IntegrationId, int SystemId, string MasterId, string ChangeAgent)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, ChangeAgent);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
            //catch (Exception)
            //{
            //    throw;
            //}
            return RedirectToAction(nameof(OfficeLocationList));
        }
        #endregion


        #region Organizational Unit
        /*******************************************************************************************************************************
			* Organizational Unit
		*******************************************************************************************************************************/

        // GET: Organization/OrganizationalUnitList
        public async Task<IActionResult> OrganizationalUnitList()
        {
            try
            {
                var model = new OrganizationalUnitListViewModel()
                {
                    Title = "Organizational Unit",
                    PageId = "organizationalUnitPage",
                    ActiveClass = "Organization",
                    Message = "Your Organizational Unit Page",
                    Integration = "Organizational Unit",
                    IntegrationId = 1,
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                    RemediationList = new List<OrganizationalUnitRemediationListItemViewModel>()
                };

			    var list = await _context.OrganizationalUnitsRemediationList.ToListAsync();
			
				foreach (var item in list)
				{
					model.RemediationList.Add(new OrganizationalUnitRemediationListItemViewModel()
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
						OrganizationalUnitType = item.OrganizationalUnitTypeName,
						OrganizationalUnitName = item.OrganizationName,
						OrganizationalUnitCode = item.OrganizationalUnitId
                    });
				}
			
                return View(model);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
        }

        // GET: Organization/OrganizationalUnitEdit
        public async Task<IActionResult> OrganizationalUnitEdit(long Id, int SystemId)
        {
            try
            {
                OrganizationalUnitDetail organizationalUnitDetails = await _context.OrganizationalUnitDetails.Where(e => e.Id == Id && e.SystemId == SystemId).SingleAsync();
				//var sourceData = _context.OrganizationalUnitDetails.Where(e => e.Id == Id).SingleAsync();

				var viewModel = new OrganizationalUnitViewModel()
				{
					Id = organizationalUnitDetails.Id,
					Title = "Organizational Unit Details",
					PageId = "organizationalUnitPage",
					ActiveClass = "Organization",
					Message = "Your Organizational Unit Page",
					PageWrapperClass = "toggled",
					System = organizationalUnitDetails.SystemName,
					SystemId = organizationalUnitDetails.SystemId,
					//Integration = "Organizational Unit",
                    Integration = organizationalUnitDetails.IntegrationName,
                    IntegrationId = organizationalUnitDetails.IntegrationId,
					SourceRecordId = organizationalUnitDetails.SourceRecordId,
					RecordStatus = organizationalUnitDetails.RecordStatus,
					CreatedOnDT = organizationalUnitDetails.IntegrationDate,
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                    HistoryData = new List<OrganizationalUnitHistoryViewModel>(),
					
					// Organizational Unit (Name):
					OrganizationalUnitName = organizationalUnitDetails.Name,
					    OrganizationalUnitName_Status = organizationalUnitDetails.Name_Status,
					    OrganizationalUnitName_BusinessName = organizationalUnitDetails.Name_BusinessName,
					    OrganizationalUnitName_BusinessDescription = organizationalUnitDetails.Name_BusinessDescription,
					    OrganizationalUnitName_Source = organizationalUnitDetails.Name_Source,
                        OrganizationalUnitName_AttributeId = organizationalUnitDetails.Name_AttributeId,
					// Organizational Unit Code (Code):
					OrganizationalUnitCode = organizationalUnitDetails.Code,
					    OrganizationalUnitCode_Status = organizationalUnitDetails.Code_Status,
					    OrganizationalUnitCode_BusinessName = organizationalUnitDetails.Code_BusinessName,
					    OrganizationalUnitCode_BusinessDescription = organizationalUnitDetails.Code_BusinessDescription,
					    OrganizationalUnitCode_Source = organizationalUnitDetails.Code_Source,
                        OrganizationalUnitCode_AttributeId = organizationalUnitDetails.Code_AttributeId,
                    // Organizational Unit Type (OrganizationalUnitType):
                    OrganizationalUnitType = organizationalUnitDetails.OrganizationalUnitType,
					    OrganizationalUnitType_Status = organizationalUnitDetails.OrganizationalUnitType_Status,
					    OrganizationalUnitType_BusinessName = organizationalUnitDetails.OrganizationalUnitType_BusinessName,
					    OrganizationalUnitType_BusinessDescription = organizationalUnitDetails.OrganizationalUnitType_BusinessDescription,
					    OrganizationalUnitType_Source = organizationalUnitDetails.OrganizationalUnitType_Source,
                        OrganizationalUnitType_AttributeId = organizationalUnitDetails.OrganizationalUnitType_AttributeId,
                    // Parent Organizational Unit Name (OrganizationalUnitParent):
                    ParentOrganizationalUnitName = organizationalUnitDetails.ParentOrganizationalUnitName,
                        ParentOrganizationalUnitName_Status = organizationalUnitDetails.ParentOrganizationalUnitName_Status,
                        ParentOrganizationalUnitName_BusinessName = organizationalUnitDetails.ParentOrganizationalUnitName_BusinessName,
                        ParentOrganizationalUnitName_BusinessDescription = organizationalUnitDetails.ParentOrganizationalUnitName_BusinessDescription,
                        ParentOrganizationalUnitName_Source = organizationalUnitDetails.ParentOrganizationalUnitName_Source,
                        ParentOrganizationalUnitName_AttributeId = organizationalUnitDetails.ParentOrganizationalUnitName_AttributeId,
                    // Parent Organizational Unit Code (OrganizationalUnitParentCode):
                    ParentOrganizationalUnitCode = organizationalUnitDetails.ParentOrganizationalUnitType,
                        ParentOrganizationalUnitCode_Status = organizationalUnitDetails.ParentOrganizationalUnitType_Status,
                        ParentOrganizationalUnitCode_BusinessName = organizationalUnitDetails.ParentOrganizationalUnitType_BusinessName,
                        ParentOrganizationalUnitCode_BusinessDescription = organizationalUnitDetails.ParentOrganizationalUnitType_BusinessDescription,
                        ParentOrganizationalUnitCode_Source = organizationalUnitDetails.ParentOrganizationalUnitType_Source,
                        ParentOrganizationalUnitCode_AttributeId = organizationalUnitDetails.ParentOrganizationalUnitType_AttributeId,
                    // Parent Organizational Unit Type (OrganizationalUnitParentType):
                    ParentOrganizationalUnitType = organizationalUnitDetails.ParentOrganizationalUnitType,
                        ParentOrganizationalUnitType_Status = organizationalUnitDetails.ParentOrganizationalUnitType_Status,
                        ParentOrganizationalUnitType_BusinessName = organizationalUnitDetails.ParentOrganizationalUnitType_BusinessName,
                        ParentOrganizationalUnitType_BusinessDescription = organizationalUnitDetails.ParentOrganizationalUnitType_BusinessDescription,
                        ParentOrganizationalUnitType_Source = organizationalUnitDetails.ParentOrganizationalUnitType_Source,
                        ParentOrganizationalUnitType_AttributeId = organizationalUnitDetails.ParentOrganizationalUnitType_AttributeId,
                    // Parent Organizational Unit MasterId (OrganizationalUnitParentMasterId):
                    ParentOrganizationalUnitMasterId = organizationalUnitDetails.ParentOrganizationalUnitMasterId,
                        ParentOrganizationalUnitMasterId_Status = organizationalUnitDetails.ParentOrganizationalUnitMasterId_Status,
                        ParentOrganizationalUnitMasterId_BusinessName = organizationalUnitDetails.ParentOrganizationalUnitMasterId_BusinessName,
                        ParentOrganizationalUnitMasterId_BusinessDescription = organizationalUnitDetails.ParentOrganizationalUnitMasterId_BusinessDescription,
                        ParentOrganizationalUnitMasterId_Source = organizationalUnitDetails.ParentOrganizationalUnitMasterId_Source,
                        ParentOrganizationalUnitMasterId_AttributeId = organizationalUnitDetails.ParentOrganizationalUnitMasterId_AttributeId,
                    // Organization (OrganizationName):
                    OrganizationName = organizationalUnitDetails.OrganizationName,
                        OrganizationName_Status = organizationalUnitDetails.OrganizationName_Status,
                        OrganizationName_BusinessName = organizationalUnitDetails.OrganizationName_BusinessName,
                        OrganizationName_BusinessDescription = organizationalUnitDetails.OrganizationName_BusinessDescription,
                        OrganizationName_Source = organizationalUnitDetails.OrganizationName_Source,
                        OrganizationName_AttributeId = organizationalUnitDetails.OrganizationName_AttributeId,
                    // Organization Code (OrganizationCode):
                    OrganizationCode = organizationalUnitDetails.OrganizationCode,
                        OrganizationCode_Status = organizationalUnitDetails.OrganizationCode_Status,
                        OrganizationCode_BusinessName = organizationalUnitDetails.OrganizationCode_BusinessName,
                        OrganizationCode_BusinessDescription = organizationalUnitDetails.OrganizationCode_BusinessDescription,
                        OrganizationCode_Source = organizationalUnitDetails.OrganizationCode_Source,
                        OrganizationCode_AttributeId = organizationalUnitDetails.OrganizationCode_AttributeId,
                    // Organization Master Id (OrganizationMasterId):
                    OrganizationMasterId = organizationalUnitDetails.OrganizationMasterId,
                        OrganizationMasterId_Status = organizationalUnitDetails.OrganizationMasterId_Status,
                        OrganizationMasterId_BusinessName = organizationalUnitDetails.OrganizationMasterId_BusinessName,
                        OrganizationMasterId_BusinessDescription = organizationalUnitDetails.OrganizationMasterId_BusinessDescription,
                        OrganizationMasterId_Source = organizationalUnitDetails.OrganizationMasterId_Source,
                        OrganizationMasterId_AttributeId = organizationalUnitDetails.OrganizationMasterId_AttributeId,

                    OrganizationList = GetOrganizationList().ToList(),
					OrganizationalUnitParentList = GetOrganizationalUnitList().ToList(),
                    //uncomment when MDS.OrganizationalUnitTypes exists
                    //OrganizationalUnitTypeList = (await GetOrganizationalUnitTypeList()).ToList(),

                };

				var historyData = await _context.OrganizationalUnitHistories.Where(s => s.RecordId == Id && s.SystemId == organizationalUnitDetails.SystemId).OrderByDescending(s => s.RecordDate).ToListAsync();
				foreach (OrganizationalUnitHistory row in historyData)
				{
					viewModel.HistoryData.Add(new OrganizationalUnitHistoryViewModel()
					{
						OrganizationalUnitName = row.Name,
						OrganizationalUnitName_Status = row.Name_Status,

						OrganizationalUnitCode = row.Code,
						OrganizationalUnitCode_Status = row.Code_Status,

						OrganizationalUnitType = row.OrganizationalUnitType,
						OrganizationalUnitType_Status = row.OrganizationalUnitType_Status,

                        OrganizationMasterId = row.OrganizationMasterId,
                        OrganizationMasterId_Status = row.OrganizationMasterId_Status,

                        OrganizationCode = row.OrganizationCode,
                        OrganizationCode_Status = row.OrganizationMasterId_Status,

                        ParentOrganizationalUnitMasterId = row.ParentOrganizationalUnitMasterId,
                        ParentOrganizationalUnitMasterId_Status = row.ParentOrganizationalUnitMasterId_Status,

                        HistoryDate = row.RecordDate
					});
				}

                return View(viewModel);
            }
            //catch (Exception ex)
            //{
            //    this.LogException(ex);
            //    return RedirectToAction("SystemError", "Error");
            //}
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Organization/OrganizationalUnitMatch
        public async Task<IActionResult> OrganizationalUnitMatch(long Id, int SystemId)
        {
            try
            {
				var organizationalUnitDetail = await _context.OrganizationalUnitDetails.Where(e => e.Id == Id && e.SystemId == SystemId).SingleAsync();

				var viewModel = new OrganizationalUnitMatchViewModel()
                {
                    Id = Id,
                    PageId = "organizationalUnitPage",
                    PageWrapperClass = "toggled",
                    ActiveClass = "Organization",
                    Title = "Organizational Unit Matching",
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
					
                    Integration = organizationalUnitDetail.IntegrationName,
                    IntegrationId = organizationalUnitDetail.IntegrationId,
                    IntegrationDate = organizationalUnitDetail.IntegrationDate,

                    OrganizationalUnitName = organizationalUnitDetail.Name,
                    OrganizationalUnitName_BusinessName = organizationalUnitDetail.Name_BusinessName,
                    OrganizationalUnitName_BusinessDescription = organizationalUnitDetail.Name_BusinessDescription,
                    OrganizationalUnitName_Weight = 50, // organizationalUnitDetail.Name_MatchWeight,

                    OrganizationalUnitCode = organizationalUnitDetail.Code,
                    OrganizationalUnitCode_BusinessName = organizationalUnitDetail.Code_BusinessName,
                    OrganizationalUnitCode_BusinessDescription = organizationalUnitDetail.Code_BusinessDescription,
                    OrganizationalUnitCode_Weight = 50, // organizationalUnitDetail.Code_MatchWeight,

                    OrganizationalUnitType = organizationalUnitDetail.OrganizationalUnitType,
                    OrganizationalUnitType_BusinessName = organizationalUnitDetail.OrganizationalUnitType_BusinessName,
                    OrganizationalUnitType_BusinessDescription = organizationalUnitDetail.OrganizationalUnitType_BusinessDescription,
                    OrganizationalUnitType_Weight = 50, // organizationalUnitDetail.OrganizationalUnitType_MatchWeight,

                    Organization = organizationalUnitDetail.OrganizationName,
                    Organization_BusinessName = organizationalUnitDetail.OrganizationName_BusinessName,
                    Organization_BusinessDescription = organizationalUnitDetail.OrganizationName_BusinessDescription,
                    Organization_Weight = 50, // organizationalUnitDetail.OrganizationName_MatchWeight,

                    PossibleMatches = new List<OrganizationalUnitMatchSummaryViewModel>(),

                    CreatedOnDT = organizationalUnitDetail.IntegrationDate,
                    System = organizationalUnitDetail.SystemName,
					SystemId = organizationalUnitDetail.SystemId,
                    SourceRecordId = organizationalUnitDetail.SourceRecordId
				};

				var PossibleMatches = _context.GetOrganizationalUnitPossibleMatches(organizationalUnitDetail.SystemId, Id);

				foreach (OrganizationalUnitPossibleMatch pm in PossibleMatches)
				{
					viewModel.PossibleMatches.Add(new OrganizationalUnitMatchSummaryViewModel()
					{
						MatchConfidence = pm.MatchConfidence,
						OrganizationalUnitName = pm.OrganizationalUnitName,
						OrganizationalUnitCode = pm.OrganizationalUnitCode,
                        OrganizationalUnitType = pm.OrganizationalUnitType,
                        OrganizationName = pm.OrganizationName,

						MasterId = pm.MasterId
					});
				}
				return View(viewModel);

            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
        }

        // GET: Organization/OrganizationalUnitCompare
        public async Task<IActionResult> OrganizationalUnitCompare(long Id, int SystemId, int MasterId)
        {
            try
            {
                var comparison = await _context.GetOrganizationalUnitComparisonDetail(SystemId, Id, MasterId);

                var viewModel = new OrganizationalUnitCompareViewModel()
                {
                    Id = Id,
                    IntegrationId = 2,
                    SystemId = comparison.SystemId,
                    MasterId = MasterId,
                    IntegrationDate = comparison.IntegrationDate,
					System = comparison.System,
					SourceRecordId = comparison.SourceRecordId,
					SourceRecordId_Compare = comparison.SourceRecordId_Compare,
					OrganizationalUnitName = comparison.OrganizationalUnitName,
                        //OrganizationalUnitName_BusinessName = comparison.OrganizationalUnitName_BusinessName,
                        //OrganizationalUnitName_BusinessDescription = comparison.OrganizationalUnitName_BusinessDescription,
                    OrganizationalUnitCode = comparison.OrganizationalUnitCode,
                        //OrganizationalUnitCode_BusinessName = comparison.OrganizationalUnitCode_BusinessName,
                        //OrganizationalUnitCode_BusinessDescription = comparison.OrganizationalUnitCode_BusinessDescription,
                    OrganizationalUnitType = comparison.OrganizationalUnitType,
                        //OrganizationalUnitType_BusinessName = comparison.OrganizationalUnitCode_BusinessName,
                        //OrganizationalUnitType_BusinessDescription = comparison.OrganizationalUnitCode_BusinessDescription,
                    OrganizationName = comparison.OrganizationName,
                        //OrganizationName_BusinessName = comparison.OrganizationName_BusinessName,
                        //OrganizationName_BusinessDescription = comparison.OrganizationName_BusinessDescription,
                    OrganizationalUnitName_Compare = comparison.OrganizationalUnitName_Compare,
                    OrganizationalUnitCode_Compare = comparison.OrganizationalUnitCode_Compare,
                    OrganizationalUnitType_Compare = comparison.OrganizationalUnitType_Compare,
                    OrganizationName_Compare = comparison.OrganizationName_Compare
                };
                
                return PartialView("OrganizationalUnitCompare", viewModel);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        // GET: Organization/OrganizationalUnitCompare
		public async Task<IActionResult> OrganizationalUnitManualMatch(long Id, int SystemId, int IntegrationId, string MasterId, string ChangeAgent)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, ChangeAgent);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(OrganizationalUnitList));
        }

        // POST: Organization/OrganizationalUnitSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrganizationalUnitSave(OrganizationalUnitViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeOrganizationalUnitIntegrationRecord(model.SystemId, model.Id, model.OrganizationalUnitName, model.OrganizationalUnitCode, model.OrganizationalUnitType, model.ParentOrganizationalUnitName, 
                        model.ParentOrganizationalUnitCode, model.ParentOrganizationalUnitType, model.ParentOrganizationalUnitMasterId, model.OrganizationName, model.OrganizationCode, model.OrganizationMasterId);
                }
                catch (Exception ex)
                {
                    this.LogException(ex);
                    return RedirectToAction("SystemError", "Error");
                }
                //catch (Exception)
                //{
                //    throw;
                //}
                return RedirectToAction(nameof(OrganizationalUnitList));
            }
            return View(model);
        }

        // POST: Organization/OrganizationalUnitRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OrganizationalUnitRevalidate(OrganizationalUnitViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeOrganizationalUnitIntegrationRecord(model.SystemId, model.Id, model.OrganizationalUnitName, model.OrganizationalUnitCode, model.OrganizationalUnitType, model.ParentOrganizationalUnitName,
                        model.ParentOrganizationalUnitCode, model.ParentOrganizationalUnitType, model.ParentOrganizationalUnitMasterId, model.OrganizationName, model.OrganizationCode, model.OrganizationMasterId);
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    this.LogException(ex);
                    return RedirectToAction("SystemError", "Error");
                }
                //catch (Exception)
                //{
                //    throw;
                //}
                return RedirectToAction(nameof(OrganizationalUnitList));
            }
            return View(model);
		}

        #endregion


        #region Academic Catalog

        /*  *** List View ***
        ******************************************************************/
        #region Academic Catalog List
        // GET: Organization/AcademicCatalogList
        public IActionResult AcademicCatalogList()
        {
            var model = new StudentAcademicCatalogListViewModel()
            {
                Title = "Academic Catalog",
                PageId = "academicCatalogPage",
                ActiveClass = "Organization",
                Message = "Your Academic Catalog Page",
                Integration = "AcademicCatalog",
                IntegrationId = 12,
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
                RemediationList = new List<StudentAcademicCatalogRemediationListItemViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetAcademicCatalogList(AjaxDataTableRequest request)
        {
            try
            {
                var catalogs = _context.StudentAcademicCatalogRemediationList.AsQueryable();

                int recordsTotal = catalogs.Count();

                var catalogList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? catalogs
                                                : catalogs.Where(s => s.DegreeType.Contains(request.searchValue) ||
                                                                      s.AcademicCareer.Contains(request.searchValue) ||
                                                                      s.Department.Contains(request.searchValue) ||
                                                                      s.ErrorCategories.Contains(request.searchValue) ||
                                                                      s.SystemName.Contains(request.searchValue))
                                        )
                                        .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                        .ToListAsync();

                int recordsFiltered = catalogList.Count();

                var studentAcademicCatalogRemediationList = new List<StudentAcademicCatalogRemediationListItemViewModel>();

                foreach (var item in catalogList.Skip(request.start).Take(request.length))
                {
                    studentAcademicCatalogRemediationList.Add(new StudentAcademicCatalogRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        RecordStatus = item.RecordStatus,

                        DegreeType = item.DegreeType,
                        AcademicCareer = item.AcademicCareer,
                        Department = item.Department,

                        ErrorCategories = item.ErrorCategories,
                        SystemName = item.SystemName,
                        IntegrationDate = item.IntegrationDate,
                        ErrorCount = item.ErrorCount,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate
                    });
                }

                var data = studentAcademicCatalogRemediationList.ToList();

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
                _logger.LogError(ex, "Unable to retrieve AcademicCatalogList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Edit View ***
        ******************************************************************/
        #region Academic Catalog Edit
        // GET: Organization/AcademicCatalogEdit
        public IActionResult AcademicCatalogEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetStudentAcademicCatalogHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var studentAcademicCatalogDetail = history.First();
                var studentAcademicCatalogSource = history.Last();

                var viewModel = new StudentAcademicCatalogViewModel()
                {
                    Title = "Student",
                    PageId = "academicCatalogPage",
                    ActiveClass = "Organization",
                    Message = "Your Student Page",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = studentAcademicCatalogDetail.Id,
                    System = studentAcademicCatalogDetail.SystemName,
                    SystemId = studentAcademicCatalogDetail.SystemId,
                    Integration = studentAcademicCatalogDetail.IntegrationName,
                    IntegrationId = studentAcademicCatalogDetail.IntegrationId,
                    IntegrationDate = studentAcademicCatalogDetail.IntegrationDate,
                    CreatedDate = studentAcademicCatalogDetail.RecordDate,
                    SourceRecordId = studentAcademicCatalogDetail.SourceRecordId,
                    CreatedOnDT = studentAcademicCatalogDetail.RecordDate,

                    HistoryData = new List<StudentAcademicCatalogHistoryViewModel>(),
                    RecordStatus = studentAcademicCatalogDetail.RecordStatus,

                    #region Detail Attributes

                    DegreeTypeName = studentAcademicCatalogDetail.DegreeTypeName,
                    DegreeTypeName_BusinessName = studentAcademicCatalogDetail.DegreeTypeName_BusinessName,
                    DegreeTypeName_BusinessDescription = studentAcademicCatalogDetail.DegreeTypeName_BusinessDescription,
                    DegreeTypeName_AttributeId = studentAcademicCatalogDetail.DegreeTypeName_AttributeId,
                    DegreeTypeName_OriginalValue = studentAcademicCatalogSource.DegreeTypeName,
                    DegreeTypeName_Status = studentAcademicCatalogDetail.DegreeTypeName_Status,
                    DegreeTypeName_Source = studentAcademicCatalogDetail.DegreeTypeName_Source,
                    DegreeTypeName_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.DegreeTypeName_Source }),
                    DegreeTypeSourceSystemRecordId = studentAcademicCatalogDetail.DegreeTypeSourceSystemRecordId,
                    DegreeTypeSourceSystemRecordId_BusinessName = studentAcademicCatalogDetail.DegreeTypeSourceSystemRecordId_BusinessName,
                    DegreeTypeSourceSystemRecordId_BusinessDescription = studentAcademicCatalogDetail.DegreeTypeSourceSystemRecordId_BusinessDescription,
                    DegreeTypeSourceSystemRecordId_AttributeId = studentAcademicCatalogDetail.DegreeTypeSourceSystemRecordId_AttributeId,
                    DegreeTypeSourceSystemRecordId_OriginalValue = studentAcademicCatalogSource.DegreeTypeSourceSystemRecordId,
                    DegreeTypeSourceSystemRecordId_Status = studentAcademicCatalogDetail.DegreeTypeSourceSystemRecordId_Status,
                    DegreeTypeSourceSystemRecordId_Source = studentAcademicCatalogDetail.DegreeTypeSourceSystemRecordId_Source,
                    DegreeTypeSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.DegreeTypeSourceSystemRecordId_Source }),
                    DegreeTypeMasterId = studentAcademicCatalogDetail.DegreeTypeMasterId,
                    DegreeTypeMasterId_BusinessName = studentAcademicCatalogDetail.DegreeTypeMasterId_BusinessName,
                    DegreeTypeMasterId_BusinessDescription = studentAcademicCatalogDetail.DegreeTypeMasterId_BusinessDescription,
                    DegreeTypeMasterId_AttributeId = studentAcademicCatalogDetail.DegreeTypeMasterId_AttributeId,
                    DegreeTypeMasterId_OriginalValue = studentAcademicCatalogSource.DegreeTypeMasterId,
                    DegreeTypeMasterId_Status = studentAcademicCatalogDetail.DegreeTypeMasterId_Status,
                    DegreeTypeMasterId_Source = studentAcademicCatalogDetail.DegreeTypeMasterId_Source,
                    DegreeTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.DegreeTypeMasterId_Source, studentAcademicCatalogDetail.DegreeTypeSourceSystemRecordId_Source, studentAcademicCatalogDetail.DegreeTypeName_Source }),

                    AcademicCareerName = studentAcademicCatalogDetail.AcademicCareerName,
                    AcademicCareerName_BusinessName = studentAcademicCatalogDetail.AcademicCareerName_BusinessName,
                    AcademicCareerName_BusinessDescription = studentAcademicCatalogDetail.AcademicCareerName_BusinessDescription,
                    AcademicCareerName_AttributeId = studentAcademicCatalogDetail.AcademicCareerName_AttributeId,
                    AcademicCareerName_OriginalValue = studentAcademicCatalogSource.AcademicCareerName,
                    AcademicCareerName_Status = studentAcademicCatalogDetail.AcademicCareerName_Status,
                    AcademicCareerName_Source = studentAcademicCatalogDetail.AcademicCareerName_Source,
                    AcademicCareerName_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.AcademicCareerName_Source }),
                    AcademicCareerSourceSystemRecordId = studentAcademicCatalogDetail.AcademicCareerSourceSystemRecordId,
                    AcademicCareerSourceSystemRecordId_BusinessName = studentAcademicCatalogDetail.AcademicCareerSourceSystemRecordId_BusinessName,
                    AcademicCareerSourceSystemRecordId_BusinessDescription = studentAcademicCatalogDetail.AcademicCareerSourceSystemRecordId_BusinessDescription,
                    AcademicCareerSourceSystemRecordId_AttributeId = studentAcademicCatalogDetail.AcademicCareerSourceSystemRecordId_AttributeId,
                    AcademicCareerSourceSystemRecordId_OriginalValue = studentAcademicCatalogSource.AcademicCareerSourceSystemRecordId,
                    AcademicCareerSourceSystemRecordId_Status = studentAcademicCatalogDetail.AcademicCareerSourceSystemRecordId_Status,
                    AcademicCareerSourceSystemRecordId_Source = studentAcademicCatalogDetail.AcademicCareerSourceSystemRecordId_Source,
                    AcademicCareerSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.AcademicCareerSourceSystemRecordId_Source }),
                    AcademicCareerMasterId = studentAcademicCatalogDetail.AcademicCareerMasterId,
                    AcademicCareerMasterId_BusinessName = studentAcademicCatalogDetail.AcademicCareerMasterId_BusinessName,
                    AcademicCareerMasterId_BusinessDescription = studentAcademicCatalogDetail.AcademicCareerMasterId_BusinessDescription,
                    AcademicCareerMasterId_AttributeId = studentAcademicCatalogDetail.AcademicCareerMasterId_AttributeId,
                    AcademicCareerMasterId_OriginalValue = studentAcademicCatalogSource.AcademicCareerMasterId,
                    AcademicCareerMasterId_Status = studentAcademicCatalogDetail.AcademicCareerMasterId_Status,
                    AcademicCareerMasterId_Source = studentAcademicCatalogDetail.AcademicCareerMasterId_Source,
                    AcademicCareerMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.AcademicCareerMasterId_Source, studentAcademicCatalogDetail.AcademicCareerSourceSystemRecordId_Source, studentAcademicCatalogDetail.AcademicCareerName_Source }),

                    AcademicProgramName = studentAcademicCatalogDetail.AcademicProgramName,
                    AcademicProgramName_BusinessName = studentAcademicCatalogDetail.AcademicProgramName_BusinessName,
                    AcademicProgramName_BusinessDescription = studentAcademicCatalogDetail.AcademicProgramName_BusinessDescription,
                    AcademicProgramName_AttributeId = studentAcademicCatalogDetail.AcademicProgramName_AttributeId,
                    AcademicProgramName_OriginalValue = studentAcademicCatalogSource.AcademicProgramName,
                    AcademicProgramName_Status = studentAcademicCatalogDetail.AcademicProgramName_Status,
                    AcademicProgramName_Source = studentAcademicCatalogDetail.AcademicProgramName_Source,
                    AcademicProgramName_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.AcademicProgramName_Source }),
                    AcademicProgramSourceSystemRecordId = studentAcademicCatalogDetail.AcademicProgramSourceSystemRecordId,
                    AcademicProgramSourceSystemRecordId_BusinessName = studentAcademicCatalogDetail.AcademicProgramSourceSystemRecordId_BusinessName,
                    AcademicProgramSourceSystemRecordId_BusinessDescription = studentAcademicCatalogDetail.AcademicProgramSourceSystemRecordId_BusinessDescription,
                    AcademicProgramSourceSystemRecordId_AttributeId = studentAcademicCatalogDetail.AcademicProgramSourceSystemRecordId_AttributeId,
                    AcademicProgramSourceSystemRecordId_OriginalValue = studentAcademicCatalogSource.AcademicProgramSourceSystemRecordId,
                    AcademicProgramSourceSystemRecordId_Status = studentAcademicCatalogDetail.AcademicProgramSourceSystemRecordId_Status,
                    AcademicProgramSourceSystemRecordId_Source = studentAcademicCatalogDetail.AcademicProgramSourceSystemRecordId_Source,
                    AcademicProgramSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.AcademicProgramSourceSystemRecordId_Source }),
                    AcademicProgramMasterId = studentAcademicCatalogDetail.AcademicProgramMasterId,
                    AcademicProgramMasterId_BusinessName = studentAcademicCatalogDetail.AcademicProgramMasterId_BusinessName,
                    AcademicProgramMasterId_BusinessDescription = studentAcademicCatalogDetail.AcademicProgramMasterId_BusinessDescription,
                    AcademicProgramMasterId_AttributeId = studentAcademicCatalogDetail.AcademicProgramMasterId_AttributeId,
                    AcademicProgramMasterId_OriginalValue = studentAcademicCatalogSource.AcademicProgramMasterId,
                    AcademicProgramMasterId_Status = studentAcademicCatalogDetail.AcademicProgramMasterId_Status,
                    AcademicProgramMasterId_Source = studentAcademicCatalogDetail.AcademicProgramMasterId_Source,
                    AcademicProgramMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.AcademicProgramMasterId_Source, studentAcademicCatalogDetail.AcademicProgramSourceSystemRecordId_Source, studentAcademicCatalogDetail.AcademicProgramName_Source }),

                    AcademicPlanName = studentAcademicCatalogDetail.AcademicPlanName,
                    AcademicPlanName_BusinessName = studentAcademicCatalogDetail.AcademicPlanName_BusinessName,
                    AcademicPlanName_BusinessDescription = studentAcademicCatalogDetail.AcademicPlanName_BusinessDescription,
                    AcademicPlanName_AttributeId = studentAcademicCatalogDetail.AcademicPlanName_AttributeId,
                    AcademicPlanName_OriginalValue = studentAcademicCatalogSource.AcademicPlanName,
                    AcademicPlanName_Status = studentAcademicCatalogDetail.AcademicPlanName_Status,
                    AcademicPlanName_Source = studentAcademicCatalogDetail.AcademicPlanName_Source,
                    AcademicPlanName_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.AcademicPlanName_Source }),

                    DepartmentName = studentAcademicCatalogDetail.DepartmentName,
                    DepartmentName_BusinessName = studentAcademicCatalogDetail.DepartmentName_BusinessName,
                    DepartmentName_BusinessDescription = studentAcademicCatalogDetail.DepartmentName_BusinessDescription,
                    DepartmentName_AttributeId = studentAcademicCatalogDetail.DepartmentName_AttributeId,
                    DepartmentName_OriginalValue = studentAcademicCatalogSource.DepartmentName,
                    DepartmentName_Status = studentAcademicCatalogDetail.DepartmentName_Status,
                    DepartmentName_Source = studentAcademicCatalogDetail.DepartmentName_Source,
                    DepartmentName_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.DepartmentName_Source }),
                    DepartmentCode = studentAcademicCatalogDetail.DepartmentCode,
                    DepartmentCode_BusinessName = studentAcademicCatalogDetail.DepartmentCode_BusinessName,
                    DepartmentCode_BusinessDescription = studentAcademicCatalogDetail.DepartmentCode_BusinessDescription,
                    DepartmentCode_AttributeId = studentAcademicCatalogDetail.DepartmentCode_AttributeId,
                    DepartmentCode_OriginalValue = studentAcademicCatalogSource.DepartmentCode,
                    DepartmentCode_Status = studentAcademicCatalogDetail.DepartmentCode_Status,
                    DepartmentCode_Source = studentAcademicCatalogDetail.DepartmentCode_Source,
                    DepartmentCode_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.DepartmentCode_Source }),
                    DepartmentMasterId = studentAcademicCatalogDetail.DepartmentMasterId,
                    DepartmentMasterId_BusinessName = studentAcademicCatalogDetail.DepartmentMasterId_BusinessName,
                    DepartmentMasterId_BusinessDescription = studentAcademicCatalogDetail.DepartmentMasterId_BusinessDescription,
                    DepartmentMasterId_AttributeId = studentAcademicCatalogDetail.DepartmentMasterId_AttributeId,
                    DepartmentMasterId_OriginalValue = studentAcademicCatalogSource.DepartmentMasterId,
                    DepartmentMasterId_Status = studentAcademicCatalogDetail.DepartmentMasterId_Status,
                    DepartmentMasterId_Source = studentAcademicCatalogDetail.DepartmentMasterId_Source,
                    DepartmentMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.DepartmentMasterId_Source, studentAcademicCatalogDetail.DepartmentCode_Source, studentAcademicCatalogDetail.DepartmentName_Source }),

                    AcademicPlanTypeName = studentAcademicCatalogDetail.AcademicPlanTypeName,
                    AcademicPlanTypeName_BusinessName = studentAcademicCatalogDetail.AcademicPlanTypeName_BusinessName,
                    AcademicPlanTypeName_BusinessDescription = studentAcademicCatalogDetail.AcademicPlanTypeName_BusinessDescription,
                    AcademicPlanTypeName_AttributeId = studentAcademicCatalogDetail.AcademicPlanTypeName_AttributeId,
                    AcademicPlanTypeName_OriginalValue = studentAcademicCatalogSource.AcademicPlanTypeName,
                    AcademicPlanTypeName_Status = studentAcademicCatalogDetail.AcademicPlanTypeName_Status,
                    AcademicPlanTypeName_Source = studentAcademicCatalogDetail.AcademicPlanTypeName_Source,
                    AcademicPlanTypeName_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.AcademicPlanTypeName_Source }),
                    AcademicPlanTypeSourceSystemRecordId = studentAcademicCatalogDetail.AcademicPlanTypeSourceSystemRecordId,
                    AcademicPlanTypeSourceSystemRecordId_BusinessName = studentAcademicCatalogDetail.AcademicPlanTypeSourceSystemRecordId_BusinessName,
                    AcademicPlanTypeSourceSystemRecordId_BusinessDescription = studentAcademicCatalogDetail.AcademicPlanTypeSourceSystemRecordId_BusinessDescription,
                    AcademicPlanTypeSourceSystemRecordId_AttributeId = studentAcademicCatalogDetail.AcademicPlanTypeSourceSystemRecordId_AttributeId,
                    AcademicPlanTypeSourceSystemRecordId_OriginalValue = studentAcademicCatalogSource.AcademicPlanTypeSourceSystemRecordId,
                    AcademicPlanTypeSourceSystemRecordId_Status = studentAcademicCatalogDetail.AcademicPlanTypeSourceSystemRecordId_Status,
                    AcademicPlanTypeSourceSystemRecordId_Source = studentAcademicCatalogDetail.AcademicPlanTypeSourceSystemRecordId_Source,
                    AcademicPlanTypeSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.AcademicPlanTypeSourceSystemRecordId_Source }),
                    AcademicPlanTypeMasterId = studentAcademicCatalogDetail.AcademicPlanTypeMasterId,
                    AcademicPlanTypeMasterId_BusinessName = studentAcademicCatalogDetail.AcademicPlanTypeMasterId_BusinessName,
                    AcademicPlanTypeMasterId_BusinessDescription = studentAcademicCatalogDetail.AcademicPlanTypeMasterId_BusinessDescription,
                    AcademicPlanTypeMasterId_AttributeId = studentAcademicCatalogDetail.AcademicPlanTypeMasterId_AttributeId,
                    AcademicPlanTypeMasterId_OriginalValue = studentAcademicCatalogSource.AcademicPlanTypeMasterId,
                    AcademicPlanTypeMasterId_Status = studentAcademicCatalogDetail.AcademicPlanTypeMasterId_Status,
                    AcademicPlanTypeMasterId_Source = studentAcademicCatalogDetail.AcademicPlanTypeMasterId_Source,
                    AcademicPlanTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.AcademicPlanTypeMasterId_Source, studentAcademicCatalogDetail.AcademicPlanTypeSourceSystemRecordId_Source, studentAcademicCatalogDetail.AcademicPlanTypeName_Source }),

                    TranscriptDescription = studentAcademicCatalogDetail.TranscriptDescription,
                    TranscriptDescription_BusinessName = studentAcademicCatalogDetail.TranscriptDescription_BusinessName,
                    TranscriptDescription_BusinessDescription = studentAcademicCatalogDetail.TranscriptDescription_BusinessDescription,
                    TranscriptDescription_AttributeId = studentAcademicCatalogDetail.TranscriptDescription_AttributeId,
                    TranscriptDescription_OriginalValue = studentAcademicCatalogSource.TranscriptDescription,
                    TranscriptDescription_Status = studentAcademicCatalogDetail.TranscriptDescription_Status,
                    TranscriptDescription_Source = studentAcademicCatalogDetail.TranscriptDescription_Source,
                    TranscriptDescription_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicCatalogDetail.TranscriptDescription_Source }),

                    DegreeTypeList = GetStudentAcademicCatalogDegreeTypeList(),
                    AcademicCareerList = GetDischargedAcademicCareerList(),
                    AcademicProgramList = GetStudentAcademicCatalogAcademicProgramList(),
                    AcademicPlanList = GetStudentAcademicCatalogAcademicPlanList(),
                    DepartmentList = GetStudentAcademicCatalogDepartmentList(),
                    AcademicPlanTypeList = GetStudentAcademicCatalogAcademicPlanTypeList(),

                    #endregion

                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);

                    viewModel.HistoryData.Add(new StudentAcademicCatalogHistoryViewModel()
                    {
                        DegreeTypeName = item.DegreeTypeName,
                        DegreeTypeName_Status = item.DegreeTypeName_Status,
                        DegreeTypeName_OriginalValue = previousitem.DegreeTypeName,
                        DegreeTypeSourceSystemRecordId = item.DegreeTypeSourceSystemRecordId,
                        DegreeTypeSourceSystemRecordId_Status = item.DegreeTypeSourceSystemRecordId_Status,
                        DegreeTypeSourceSystemRecordId_OriginalValue = previousitem.DegreeTypeSourceSystemRecordId,
                        DegreeTypeMasterId = item.DegreeTypeMasterId,
                        DegreeTypeMasterId_Status = item.DegreeTypeMasterId_Status,
                        DegreeTypeMasterId_OriginalValue = previousitem.DegreeTypeMasterId,
                        AcademicCareerName = item.AcademicCareerName,
                        AcademicCareerName_Status = item.AcademicCareerName_Status,
                        AcademicCareerName_OriginalValue = previousitem.AcademicCareerName,
                        AcademicCareerSourceSystemRecordId = item.AcademicCareerSourceSystemRecordId,
                        AcademicCareerSourceSystemRecordId_Status = item.AcademicCareerSourceSystemRecordId_Status,
                        AcademicCareerSourceSystemRecordId_OriginalValue = previousitem.AcademicCareerSourceSystemRecordId,
                        AcademicCareerMasterId = item.AcademicCareerMasterId,
                        AcademicCareerMasterId_Status = item.AcademicCareerMasterId_Status,
                        AcademicCareerMasterId_OriginalValue = previousitem.AcademicCareerMasterId,
                        AcademicProgramName = item.AcademicProgramName,
                        AcademicProgramName_Status = item.AcademicProgramName_Status,
                        AcademicProgramName_OriginalValue = previousitem.AcademicProgramName,
                        AcademicProgramSourceSystemRecordId = item.AcademicProgramSourceSystemRecordId,
                        AcademicProgramSourceSystemRecordId_Status = item.AcademicProgramSourceSystemRecordId_Status,
                        AcademicProgramSourceSystemRecordId_OriginalValue = previousitem.AcademicProgramSourceSystemRecordId,
                        AcademicProgramMasterId = item.AcademicProgramMasterId,
                        AcademicProgramMasterId_Status = item.AcademicProgramMasterId_Status,
                        AcademicProgramMasterId_OriginalValue = previousitem.AcademicProgramMasterId,
                        AcademicPlanName = item.AcademicPlanName,
                        AcademicPlanName_Status = item.AcademicPlanName_Status,
                        AcademicPlanName_OriginalValue = previousitem.AcademicPlanName,
                        DepartmentName = item.DepartmentName,
                        DepartmentName_Status = item.DepartmentName_Status,
                        DepartmentName_OriginalValue = previousitem.DepartmentName,
                        DepartmentCode = item.DepartmentCode,
                        DepartmentCode_Status = item.DepartmentCode_Status,
                        DepartmentCode_OriginalValue = previousitem.DepartmentCode,
                        DepartmentMasterId = item.DepartmentMasterId,
                        DepartmentMasterId_Status = item.DepartmentMasterId_Status,
                        DepartmentMasterId_OriginalValue = previousitem.DepartmentMasterId,
                        AcademicPlanTypeName = item.AcademicPlanTypeName,
                        AcademicPlanTypeName_Status = item.AcademicPlanTypeName_Status,
                        AcademicPlanTypeName_OriginalValue = previousitem.AcademicPlanTypeName,
                        AcademicPlanTypeSourceSystemRecordId = item.AcademicPlanTypeSourceSystemRecordId,
                        AcademicPlanTypeSourceSystemRecordId_Status = item.AcademicPlanTypeSourceSystemRecordId_Status,
                        AcademicPlanTypeSourceSystemRecordId_OriginalValue = previousitem.AcademicPlanTypeSourceSystemRecordId,
                        AcademicPlanTypeMasterId = item.AcademicPlanTypeMasterId,
                        AcademicPlanTypeMasterId_Status = item.AcademicPlanTypeMasterId_Status,
                        AcademicPlanTypeMasterId_OriginalValue = previousitem.AcademicPlanTypeMasterId,
                        TranscriptDescription = item.TranscriptDescription,
                        TranscriptDescription_Status = item.TranscriptDescription_Status,
                        TranscriptDescription_OriginalValue = previousitem.TranscriptDescription,

                        HistoryDate = item.RecordDate
                    });
                }
                #endregion

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve AcademicCatalogEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion


        /* No Match/Compare for Academic Catalog at this time (04/06/2020) */

        /*  *** Save ***
        ******************************************************************/
        #region Academic Catalog Save
        // POST: Organization/AcademicCatalogSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AcademicCatalogSave(StudentAcademicCatalogViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeStudentAcademicCatalogIntegrationRecord(model.SystemId, model.Id, model.DegreeTypeName, model.DegreeTypeSourceSystemRecordId, model.DegreeTypeMasterId,
                        model.AcademicCareerName, model.AcademicCareerSourceSystemRecordId, model.AcademicCareerMasterId,
                        model.AcademicProgramName, model.AcademicProgramSourceSystemRecordId, model.AcademicProgramMasterId,
                        model.AcademicPlanName,
                        model.DepartmentName, model.DepartmentCode, model.DepartmentMasterId,
                        model.AcademicPlanTypeName, model.AcademicPlanTypeSourceSystemRecordId, model.AcademicPlanTypeMasterId,
                        model.TranscriptDescription,
                        User.Identity.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve AcademicCatalogSave");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(AcademicCatalogList));
            }
            else
            {
                model.Title = "Student";
                model.PageId = "studentAcademicCatalogPage";
                model.ActiveClass = "Student";
                model.Message = "Your Student Page";
                model.User = User.Identity.Name;
                model.NavigationGroups = GetNavigationGroups();

                return View(nameof(AcademicCatalogEdit), model);
            }


        }
        #endregion

        /*  *** Revalidate ***
        ******************************************************************/
        #region Academic Catalog Revalidate
        // POST: Organization/AcademicCatalogRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AcademicCatalogRevalidate(StudentAcademicCatalogViewModel model)
        {
            if (model.IsValid())
            {
                try
                {

                    // Check if Dropdown have MasterId - If they dont, empty out the associated attributes per dropdown before revalidating
                    // DegreeTypeName
                    if (string.IsNullOrEmpty(model.DegreeTypeMasterId) && (!string.IsNullOrEmpty(model.DegreeTypeSourceSystemRecordId) || !string.IsNullOrEmpty(model.DegreeTypeName)))
                    {
                        model.DegreeTypeSourceSystemRecordId = null;
                        model.DegreeTypeName = null;

                        model.IsChanged = true;
                    }

                    // AcademicCareerName
                    if (string.IsNullOrEmpty(model.AcademicCareerMasterId) && (!string.IsNullOrEmpty(model.AcademicCareerSourceSystemRecordId) || !string.IsNullOrEmpty(model.AcademicCareerName)))
                    {
                        model.AcademicCareerSourceSystemRecordId = null;
                        model.AcademicCareerName = null;

                        model.IsChanged = true;
                    }

                    // AcademicProgramMasterId
                    if (string.IsNullOrEmpty(model.AcademicProgramMasterId) && (!string.IsNullOrEmpty(model.AcademicProgramSourceSystemRecordId) || !string.IsNullOrEmpty(model.AcademicProgramName)))
                    {
                        model.AcademicProgramSourceSystemRecordId = null;
                        model.AcademicProgramName = null;

                        model.IsChanged = true;
                    }

                    // DepartmentMasterId
                    if (string.IsNullOrEmpty(model.DepartmentMasterId) && (!string.IsNullOrEmpty(model.DepartmentCode) || !string.IsNullOrEmpty(model.DepartmentName)))
                    {
                        model.DepartmentCode = null;
                        model.DepartmentName = null;

                        model.IsChanged = true;
                    }

                    // AcademicPlanTypeMasterId
                    if (string.IsNullOrEmpty(model.AcademicPlanTypeMasterId) && (!string.IsNullOrEmpty(model.AcademicPlanTypeSourceSystemRecordId) || !string.IsNullOrEmpty(model.AcademicPlanTypeName)))
                    {
                        model.AcademicPlanTypeSourceSystemRecordId = null;
                        model.AcademicPlanTypeName = null;

                        model.IsChanged = true;
                    }

                    if (model.IsChanged)
                    {
                        _context.ChangeStudentAcademicCatalogIntegrationRecord(model.SystemId, model.Id, model.DegreeTypeName, model.DegreeTypeSourceSystemRecordId, model.DegreeTypeMasterId,
                            model.AcademicCareerName, model.AcademicCareerSourceSystemRecordId, model.AcademicCareerMasterId,
                            model.AcademicProgramName, model.AcademicProgramSourceSystemRecordId, model.AcademicProgramMasterId,
                            model.AcademicPlanName,
                            model.DepartmentName, model.DepartmentCode, model.DepartmentMasterId,
                            model.AcademicPlanTypeName, model.AcademicPlanTypeSourceSystemRecordId, model.AcademicPlanTypeMasterId,
                            model.TranscriptDescription,
                            User.Identity.Name);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve AcademicCatalogRevalidate");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(AcademicCatalogList));
            }
            return View(model);
        }
        #endregion

        /*  *** Remove/Ignore ***
        ******************************************************************/
        #region Remove/Ignore
        public IActionResult AcademicCatalogIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load AcademicCatalogIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(AcademicCatalogList));
        }
        #endregion

        #endregion
    }
}