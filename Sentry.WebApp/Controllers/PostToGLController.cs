using Sentry.WebApp.Authorization.Attributes;
using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sentry.WebApp.Controllers
{
    [AuthorizeFinance]
    public class PostToGLController : IntegrationController
    {
        public PostToGLController(AppDbContext context, 
            DwDbContext dwContext, 
            ILogger<PostToGLController> logger, 
            IConfiguration configuration,
            IDomainService domainService) : base(context, dwContext, logger, configuration, domainService) { }

        #region PostToGL

        /*******************************************************************************************************************************
			* PostToGL
		*******************************************************************************************************************************/

        // GET: PostToGL/PostToGLList
        public IActionResult PostToGLList()
        {
            try
            {
                var model = new PostToGLListViewModel()
                {
                    Title = "Post To GL",
                    PageId = "postToGLPage",
                    ActiveClass = "PostToGL",
                    Message = "Your Post To GL Page",
                    Integration = "Post To GL",
                    IntegrationId = 4,
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                    RemediationList = new List<PostToGLRemediationListItemViewModel>()
                };
                
                model.RemediationList.Add(new PostToGLRemediationListItemViewModel()
                {
                    Id = "-98765687651651651",
                    SystemId = 6,
                    SystemName = "FE",
                    ErrorCategories = "Designation",
                    ErrorCount = 2,
                    IntegrationDate = new DateTime(2019, 9, 15),
                    IntegrationId = 4,
                    CreatedDate = new DateTime(2019, 9, 15),
                    RecordStatus = "Bad",

                    DesignationName = "Campus Rec Men's Volleyball",
                    DesignationId = "20-10-1399",
                    StartDate = new DateTime(2015, 9, 15),
                    EndDate = new DateTime(2022, 9, 22),
                    DesignationType = "Trust-Endowment",
                    DesignationSubtype = "RES (Restricted Purpose)",
                    DesignationStatus = "Open",
                    DesignationState = "Inactive",
                    UADepartment = "1402 - Administration and Athletics"
                });

                model.RemediationList.Add(new PostToGLRemediationListItemViewModel()
                {
                    Id = "-888",
                    SystemId = 6,
                    SystemName = "FE",
                    ErrorCategories = "Designation",
                    ErrorCount = 1,
                    IntegrationDate = new DateTime(2019, 9, 15),
                    IntegrationId = 4,
                    CreatedDate = new DateTime(2019, 9, 15),
                    RecordStatus = "Possible Match",

                    DesignationName = "Campus Rec Men's Volleyballs",
                    DesignationId = "20-10-1399",
                    StartDate = new DateTime(2015, 9, 15),
                    EndDate = new DateTime(2022, 9, 22),
                    DesignationType = "Trust-Endowments",
                    DesignationSubtype = "RES (Restricted Purposes)",
                    DesignationStatus = "Opens",
                    DesignationState = "Inactives",
                    UADepartment = "1402 - Administration and Athletics'"
                });

                return View(model);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
        }

        // GET: PostToGL/PostToGLEdit
        public IActionResult PostToGLEdit(long Id, int SystemId)
        {
            try
            {
                var viewModel = new PostToGLViewModel()
                {
                    Title = "Post To GL Details",
                    PageId = "postToGLPage",
                    ActiveClass = "PostToGL",
                    Message = "Your Post To GL Page",

                    Id = -98765687651651651,
                    System = "FE",
                    SystemId = 6,
                    Integration = "PostToGL",
                    IntegrationId = 4,
                    IntegrationDate = new DateTime(2019, 9, 15),
                    CreatedDate = new DateTime(2019, 9, 22),
                    SourceRecordId = "219876532168465",
                    CreatedOnDT = new DateTime(2019, 9, 22),
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                    HistoryData = new List<PostToGLHistoryViewModel>(),
                    RecordStatus = "Bad",

                    DesignationName = "Campus Rec Men's Volleyball",
                        DesignationName_BusinessName = "Name",
                        DesignationName_BusinessDescription = "The official name for the designation",
                        DesignationName_AttributeId = 3,
                        DesignationName_Status = "Good",
                        DesignationName_Source = "sourceField_DesignationNameId",
                    DesignationId = "20-10-1399",
                        DesignationId_BusinessName = "Designation Id",
                        DesignationId_BusinessDescription = "The Project ID as identified in FE",
                        DesignationId_AttributeId = 3,
                        DesignationId_Status = "Good",
                        DesignationId_Source = "sourceField_DesignationIdId",
                    StartDate = new DateTime(2015, 9, 15),
                        StartDate_BusinessName = "Start Date",
                        StartDate_BusinessDescription = "Start date associated with this designation",
                        StartDate_AttributeId = 3,
                        StartDate_Status = "Good",
                        StartDate_Source = "sourceField_DesignationStartDateId",
                    EndDate = new DateTime(2022, 09, 22),
                        EndDate_BusinessName = "End Date",
                        EndDate_BusinessDescription = "End date associated with this designation",
                        EndDate_AttributeId = 3,
                        EndDate_Status = "Good",
                        EndDate_Source = "sourceField_DesignationEndDateId",
                    DesignationType = "Trust-Endowment",
                        DesignationType_BusinessName = "Designation Type",
                        DesignationType_BusinessDescription = "Identifies the type of type designation",
                        DesignationType_AttributeId = 3,
                        DesignationType_Status = "Good",
                        DesignationType_Source = "sourceField_DesignationTypeId",
                    DesignationSubtype = "RES (Restricted Purpose)",
                        DesignationSubtype_BusinessName = "Designation Subtype",
                        DesignationSubtype_BusinessDescription = "Identifies the subtype of type designation",
                        DesignationSubtype_AttributeId = 3,
                        DesignationSubtype_Status = "Good",
                        DesignationSubtype_Source = "sourceField_DesignationSubtypeId",
                    DesignationStatus = "Open",
                        DesignationStatus_BusinessName = "Designation Status",
                        DesignationStatus_BusinessDescription = "Indicates whether or not the designation is open, closed, or on hold",
                        DesignationStatus_AttributeId = 3,
                        DesignationStatus_Status = "Good",
                        DesignationStatus_Source = "sourceField_DesignationStatusId",
                    DesignationState = "Inactive",
                        DesignationState_BusinessName = "Designation State",
                        DesignationState_BusinessDescription = "Indicates whether the designation is active or inactive",
                        DesignationState_AttributeId = 3,
                        DesignationState_Status = "Good",
                        DesignationState_Source = "sourceField_DesignationStateId",
                    UADepartment = "1402 - Administration and Athletics",
                        UADepartment_BusinessName = "Department",
                        UADepartment_BusinessDescription = "Represents the home department for the designation",
                        UADepartment_AttributeId = 3,
                        UADepartment_Status = "Good",
                        UADepartment_Source = "sourceField_UADepartmentId"
                };

                viewModel = new PostToGLViewModel()
                {
                    Title = "Post To GL Details",
                    PageId = "postToGLPage",
                    ActiveClass = "PostToGL",
                    Message = "Your Post To GL Page",

                    Id = -888,
                    System = "FE",
                    SystemId = 6,
                    Integration = "PostToGL",
                    IntegrationId = 4,
                    IntegrationDate = new DateTime(2019, 9, 15),
                    CreatedDate = new DateTime(2019, 9, 22),
                    SourceRecordId = "987621654984",
                    CreatedOnDT = new DateTime(2019, 9, 22),
                    HistoryData = new List<PostToGLHistoryViewModel>(),
                    RecordStatus = "Bad",

                    DesignationName = "Campus Rec Men's Volleyball",
                        DesignationName_BusinessName = "Designation Name",
                        DesignationName_BusinessDescription = "Description of Designation",
                        DesignationName_AttributeId = 3,
                        DesignationName_Status = "Good",
                        DesignationName_Source = "sourceField_DesignationNameId",
                    DesignationId = "20-10-1399",
                        DesignationId_BusinessName = "Designation Id",
                        DesignationId_BusinessDescription = "The Project ID as identified in FE",
                        DesignationId_AttributeId = 3,
                        DesignationId_Status = "Good",
                        DesignationId_Source = "sourceField_DesignationIdId",
                    StartDate = new DateTime(2015, 9, 15),
                        StartDate_BusinessName = "Start Date",
                        StartDate_BusinessDescription = "Start date associated with this designation",
                        StartDate_AttributeId = 3,
                        StartDate_Status = "Good",
                        StartDate_Source = "sourceField_DesignationStartDateId",
                    EndDate = new DateTime(2022, 9, 22),
                        EndDate_BusinessName = "End Date",
                        EndDate_BusinessDescription = "End date associated with this designation",
                        EndDate_AttributeId = 3,
                        EndDate_Status = "Good",
                        EndDate_Source = "sourceField_DesignationEndDateId",
                    DesignationType = "Trust-Endowment",
                        DesignationType_BusinessName = "Designation Type",
                        DesignationType_BusinessDescription = "Identifies the type of type designation",
                        DesignationType_AttributeId = 3,
                        DesignationType_Status = "Good",
                        DesignationType_Source = "sourceField_DesignationTypeId",
                    DesignationSubtype = "RES (Restricted Purpose)",
                        DesignationSubtype_BusinessName = "Designation Subtype",
                        DesignationSubtype_BusinessDescription = "Identifies the subtype of type designation",
                        DesignationSubtype_AttributeId = 3,
                        DesignationSubtype_Status = "Good",
                        DesignationSubtype_Source = "sourceField_DesignationSubtypeId",
                    DesignationStatus = "Open",
                        DesignationStatus_BusinessName = "Designation Status",
                        DesignationStatus_BusinessDescription = "Indicates whether or not the designation is open, closed, or on hold",
                        DesignationStatus_AttributeId = 3,
                        DesignationStatus_Status = "Good",
                        DesignationStatus_Source = "sourceField_DesignationStatusId",
                    DesignationState = "Inactive",
                        DesignationState_BusinessName = "Designation State",
                        DesignationState_BusinessDescription = "Indicates whether the designation is active or inactive",
                        DesignationState_AttributeId = 3,
                        DesignationState_Status = "Good",
                        DesignationState_Source = "sourceField_DesignationStateId",
                    UADepartment = "1402 - Administration and Athletics",
                        UADepartment_BusinessName = "Department",
                        UADepartment_BusinessDescription = "Represents the home department for the designation",
                        UADepartment_AttributeId = 3,
                        UADepartment_Status = "Good",
                        UADepartment_Source = "sourceField_UADepartmentId"
                };

                viewModel.HistoryData.Add(new PostToGLHistoryViewModel()
                {
                    DesignationName = "Campus Rec Men's Volleyball",
                    DesignationName_Status = "Good",

                    DesignationId = "20-10-1399",
                    DesignationId_Status = "Good",

                    StartDate = new DateTime(2015, 9, 15),
                    StartDate_Status = "Good",

                    EndDate = new DateTime(2015, 9, 15),
                    EndDate_Status = "Good",

                    DesignationType = "Trust-Endowment",
                    DesignationType_Status = "Good",

                    DesignationSubtype = "RES (Restricted Purpose)",
                    DesignationSubtype_Status = "Good",

                    DesignationStatus = "Open",
                    DesignationStatus_Status = "Good",

                    DesignationState = "Inactive",
                    DesignationState_Status = "Good",

                    UADepartment = "1402 - Administration and Athletics",
                    UADepartment_Status = "Good",

                    HistoryDate = new DateTime(2022, 9, 22)
                });

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


        // GET: PostToGL/PostToGLMatch
        public IActionResult PostToGLMatch(long Id, int SystemId)
        {
            try
            {
                var viewModel = new PostToGLMatchViewModel()
                {
                    Id = Id,
                    PageId = "postToGLPage",
                    PageWrapperClass = "toggled",
                    ActiveClass = "PostToGL",
                    Title = "Post To GL Matching",
                    Integration = "PostToGL",
                    IntegrationId = 4,
                    IntegrationDate = new DateTime(2020, 11, 15),
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),

                    DesignationName = "Campus Rec Men's Volleyballss",
                        DesignationName_BusinessName = "Name",
                        DesignationName_BusinessDescription = "The official name for the designation",
                        DesignationName_Weight = 50,
                    DesignationId = "20-10-1399",
                        DesignationId_BusinessName = "Designation Id",
                        DesignationId_BusinessDescription = "The Project ID as identified in FE",
                        DesignationId_Weight = 50,
                    StartDate = new DateTime(2015, 9, 15),
                        StartDate_BusinessName = "Start Date",
                        StartDate_BusinessDescription = "Start date associated with this designation",
                        StartDate_Weight = 50,
                    EndDate = new DateTime(2022, 09, 22),
                        EndDate_BusinessName = "End Date",
                        EndDate_BusinessDescription = "End date associated with this designation",
                        EndDate_Weight = 50,
                    DesignationType = "Trust-Endowment",
                        DesignationType_BusinessName = "Designation Type",
                        DesignationType_BusinessDescription = "Identifies the type of type designation",
                        DesignationType_Weight = 50,
                    DesignationSubtype = "RES (Restricted Purpose)",
                        DesignationSubtype_BusinessName = "Designation Subtype",
                        DesignationSubtype_BusinessDescription = "Identifies the subtype of type designation",
                        DesignationSubtype_Weight = 50,
                    DesignationStatus = "Open",
                        DesignationStatus_BusinessName = "Designation Status",
                        DesignationStatus_BusinessDescription = "Indicates whether or not the designation is open, closed, or on hold",
                        DesignationStatus_Weight = 50,
                    DesignationState = "Inactive",
                        DesignationState_BusinessName = "Designation State",
                        DesignationState_BusinessDescription = "Indicates whether the designation is active or inactive",
                        DesignationState_Weight = 50,
                    UADepartment = "1402 - Administration and Athletics",
                        UADepartment_BusinessName = "Department",
                        UADepartment_BusinessDescription = "Represents the home department for the designation",
                        UADepartment_Weight = 50,

                    PossibleMatches = new List<PostToGLMatchViewModel.PostToGLMatchSummaryViewModel>(),
                    
                    System = "FE",
                    SystemId = 6,
                    SourceRecordId = "219876532168465",
                    CreatedOnDT = new DateTime(2019, 9, 22),

                };

                viewModel.PossibleMatches.Add(new PostToGLMatchViewModel.PostToGLMatchSummaryViewModel()
                {
                    MatchConfidence = 75,
                    MasterId = 321456,

                    DesignationName = "Campus Rec Men's Volleyball",
                    DesignationId = "20-10-1399",
                    StartDate = new DateTime(2015, 9, 15),
                    EndDate = new DateTime(2022, 09, 22),
                    DesignationType = "Trust-Endowment",
                    DesignationSubtype = "RES (Restricted Purpose)",
                    DesignationStatus = "Open",
                    DesignationState = "Inactive",
                    UADepartment = "1402 - Administration and Athletics",
                });
                return View(viewModel);

            }
            catch (Exception ex)
            {
                this.LogException(ex);
                return RedirectToAction("SystemError", "Error");
            }
        }

        // GET: PostToGL/PostToGLCompare
        public IActionResult PostToGLCompare(long Id, int SystemId, int MasterId)
        {
            try
            {
                var viewModel = new PostToGLCompareViewModel()
                {
                    Id = Id,
                    IntegrationId = 4,
                    SystemId = 6,
                    MasterId = 68713,
                    IntegrationDate = new DateTime(2019, 9, 15),
                    System = "FE",
                    SourceRecordId = "976876676rs7dfs6d78s6",
                    SourceRecordId_Compare = "68d6fg4d6f84g6d54fg",

                    DesignationName = "Campus Rec Men's Volleyballss",
                        DesignationName_BusinessName = "Name",
                        DesignationName_BusinessDescription = "The official name for the designation",
                        DesignationName_Compare = "Campus Rec Men's Volleyball",
                    DesignationId = "20-10-1399",
                        DesignationId_BusinessName = "Designation Id",
                        DesignationId_BusinessDescription = "The Project ID as identified in FE",
                        DesignationId_Compare = "20-10-1399",
                    StartDate = new DateTime(2015, 9, 15),
                        StartDate_BusinessName = "Start Date",
                        StartDate_BusinessDescription = "Start date associated with this designation",
                        StartDate_Compare = new DateTime(2015, 9, 15),
                    EndDate = new DateTime(2022, 09, 22),
                        EndDate_BusinessName = "End Date",
                        EndDate_BusinessDescription = "End date associated with this designation",
                        EndDate_Compare = new DateTime(2015, 9, 15),
                    DesignationType = "Trust-Endowment",
                        DesignationType_BusinessName = "Designation Type",
                        DesignationType_BusinessDescription = "Identifies the type of type designation",
                        DesignationType_Compare = "Trust-Endowment",
                    DesignationSubtype = "RES (Restricted Purpose)",
                        DesignationSubtype_BusinessName = "Designation Subtype",
                        DesignationSubtype_BusinessDescription = "Identifies the subtype of type designation",
                        DesignationSubtype_Compare = "RES (Restricted Purpose)",
                    DesignationStatus = "Open",
                        DesignationStatus_BusinessName = "Designation Status",
                        DesignationStatus_BusinessDescription = "Indicates whether or not the designation is open, closed, or on hold",
                        DesignationStatus_Compare = "Open",
                    DesignationState = "Inactive",
                        DesignationState_BusinessName = "Designation State",
                        DesignationState_BusinessDescription = "Indicates whether the designation is active or inactive",
                        DesignationState_Compare = "Inactive",
                    UADepartment = "1402 - Administration and Athletics",
                        UADepartment_BusinessName = "Department",
                        UADepartment_BusinessDescription = "Represents the home department for the designation",
                        UADepartment_Compare = "1402 - Administration and Athletics",
                };

                return PartialView("PostToGLCompare", viewModel);
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

        // GET: PostToGL/PostToGLManualMatch
        public async Task<IActionResult> PostToGLManualMatch(long Id, int IntegrationId, int SystemId, string MasterId, string ChangeAgent)
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

            return RedirectToAction(nameof(PostToGLList));
        }

        // POST: PostToGL/PostToGLSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostToGLSave(PostToGLViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    //_context.ChangeOrganizationalUnitIntegrationRecord(model.SystemId, model.Id, model.OrganizationalUnitName, model.OrganizationalUnitCode, model.OrganizationalUnitType, model.ParentOrganizationalUnitName,
                        //model.ParentOrganizationalUnitCode, model.ParentOrganizationalUnitType, model.ParentOrganizationalUnitMasterId, model.OrganizationName, model.OrganizationCode, model.OrganizationMasterId);
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
                return RedirectToAction(nameof(PostToGLList));
            }
            return View(model);
        }

        // POST: PostToGL/PostToGLRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostToGLRevalidate(PostToGLViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    //_context.ChangeOrganizationalUnitIntegrationRecord(model.SystemId, model.Id, model.OrganizationalUnitName, model.OrganizationalUnitCode, model.OrganizationalUnitType, model.ParentOrganizationalUnitName,
                    //    model.ParentOrganizationalUnitCode, model.ParentOrganizationalUnitType, model.ParentOrganizationalUnitMasterId, model.OrganizationName, model.OrganizationCode, model.OrganizationMasterId);
                    //_context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
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
                return RedirectToAction(nameof(PostToGLList));
            }
            return View(model);
        }

        #endregion

    }
}
