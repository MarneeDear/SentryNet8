using Sentry.WebApp.Data;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sentry.Domain.AccountsPayable.Entities;
using GiftTransmittalDomain = Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal;
using Azure.Identity;

namespace Sentry.WebApp.Controllers
{
    public class IntegrationController : SentryController
    {
        private const string DEFAULT_PLACEHOLDER = "-- Choose {0} --";
        public const string SESSION_ROLE_KEY = "__FinancialRole__";
        public const string ADMIN_SESSION_ROLE_KEY = "__AdminRole__";
        public const string ADMIN_ROLE = "Admin";

        //private const string DEFAULT_NULLPLACEHOLDER = "** No Value **";
        protected readonly IDomainService _domainService;
        //private ModelHelper modelHelper;

        public IntegrationController(AppDbContext context,
            DwDbContext dwContext,
            ILogger<IntegrationController> logger,
            IConfiguration configuration,
            IDomainService domainService) : base(context, dwContext, logger, configuration)
        {
            _domainService = domainService;
        }

        public bool AttributeIsReadOnly(string[] sourceFields)
        {
            foreach (var field in sourceFields)
            {
                if (!string.IsNullOrEmpty(field))
                    return false;
            }

            return true;
        }

        private ModelHelper Helper
        {
            get
            {
                return new ModelHelper(HttpContext);
            }
        }

        #region Security & Roles

        #endregion


        #region Remediation Counts
        private async Task<int> ReadyForProcessingDisbursementCount()
        {
            if (Helper.CurrentFinancialRole == Constants.APReviewerRole || Helper.FinancialRole == ADMIN_ROLE)
            {
                var giftDisbursementsProcessList = await _domainService.AccountsPayableOperations.GetGiftDisbursementsAwaitingProcessing();
                return giftDisbursementsProcessList.GiftDisbursements.Count();
            }

            return 0;
        }
        public async Task<JsonResult> GetCounts()
        {
            var counts = await GetRemediationCounts();
            return new JsonResult(counts);
        }

        protected async Task<IntegrationRemediationCountViewModel> GetRemediationCounts()
        {
            _logger.LogInformation($"User is in role Financial Role [{Helper.FinancialRole}]");                
            var claims = HttpContext.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
            var groupIds = String.Join(",", claims);
            _logger.LogInformation($"[{HttpContext.User.Identity.Name}] [{groupIds}] | Is FT Role: [{Helper.IsFTRole}] | Is FT Reviewer: [{Helper.IsFTReviewerRole}] | Is FT Approver: [{Helper.IsFTApproverRole}]");

            var integrationCountsModel = new IntegrationRemediationCountViewModel();
            _logger.LogInformation("Starting load remediation counts");
            var remediationCounts = _context.GetRemediationCounts();


            if (String.IsNullOrEmpty(Helper.FinancialRole) || Helper.FinancialRole == ADMIN_ROLE)
            {

                integrationCountsModel.OrganizationalUnitCount = remediationCounts.OrganizationalUnitCount;

                integrationCountsModel.OfficeLocationCount = remediationCounts.OfficeLocationCount;
                integrationCountsModel.AcademicCatalogCount = remediationCounts.AcademicCatalogCount;
                integrationCountsModel.EmployeeCount = remediationCounts.EmployeeCount;

                integrationCountsModel.StudentBioDemCount = remediationCounts.StudentBioDemCount;
                integrationCountsModel.StudentEnrollmentCount = remediationCounts.StudentEnrollmentCount;
                integrationCountsModel.StudentDegreeCount = remediationCounts.StudentDegreeCount;
                integrationCountsModel.StudentAcademicPlanCount = remediationCounts.StudentAcademicPlanCount;
                integrationCountsModel.StudentAcademicInvolvementCount = remediationCounts.StudentAcademicInvolvementCount;
                integrationCountsModel.StudentScholarshipCount = remediationCounts.StudentScholarshipCount;
                integrationCountsModel.StudentParentCount = remediationCounts.StudentParentCount;

                integrationCountsModel.ConstituentIndividualCount = remediationCounts.ConstituentIndividualCount;
                integrationCountsModel.ConstituentPhoneCount = remediationCounts.ConstituentPhoneCount;
                integrationCountsModel.ConstituentEmailCount = remediationCounts.ConstituentEmailCount;
                integrationCountsModel.ConstituentAddressCount = remediationCounts.ConstituentAddressCount;
                integrationCountsModel.ScholarshipCount = remediationCounts.StudentScholarshipCount;
                _logger.LogInformation("Finished loading remediation counts");

            }

            if (!String.IsNullOrEmpty(Helper.FinancialRole) || Helper.FinancialRole == ADMIN_ROLE)
            {
                integrationCountsModel.DesignationCount = remediationCounts.DesignationCount;

                var gtCounts = 0;
                var guCounts = 0;
                var initCounts = 0;
                var bursarCounts = 0;
                var preparerCounts = 0;
                var secondaryCounts = 0;
                var gtFinalizeCounts = 0;
                var guFinalizeCounts = 0;

                if (Helper.IsARRole)
                {
                    //GIFT TRANMITTALS COUNTS
                    _logger.LogInformation("Starting gift transmittal counts");

                    var result = _domainService.LynxDataOperations.GetGiftTransmittalCounts();
                    initCounts = result.Where(r => r.CountName == "InitCount").First().Count;

                    if (Helper.IsARSecondaryReviewer)
                    {
                        secondaryCounts = result.Where(r => r.CountName == "SecondaryCount").First().Count;
                    }

                    if (Helper.IsGTRole)
                    {
                        gtCounts = result.Where(r => r.CountName == "GTCount").First().Count;
                        gtFinalizeCounts = result.Where(r => r.CountName == "GTFinalCount").First().Count;
                        bursarCounts = result.Where(r => r.CountName == "BursarCount").First().Count;
                        preparerCounts = result.Where(r => r.CountName == "PreparerCount").First().Count;
                    }

                    if (Helper.IsGURole)
                    {
                        guCounts = result.Where(r => r.CountName == "GUCount").First().Count;
                        guFinalizeCounts = result.Where(r => r.CountName == "GUFinalCount").First().Count;
                    }

                    _logger.LogInformation("Finished gift transmittal counts");
                }

                _logger.LogInformation("Starting gift disbursements integration counts");

                var disbursementResults = new GiftDisbursementList();
                IEnumerable<FundsTransferCount> fundsTransfersCounts = new List<FundsTransferCount>();
                IEnumerable<NewVendorRequest> vendorResults = new List<NewVendorRequest>();

                if (Helper.IsAPRole)
                {
                    disbursementResults = await _domainService.AccountsPayableOperations
                        .GetGiftDisbursementsByRoleId(Constants.APApproverRoleIds[Helper.CurrentFinancialRole]);                    

                    if (Helper.IsAPReviewerRole || Helper.IsAPManagerRole)
                    {
                        vendorResults = await _domainService.AccountsPayableOperations.GetNewVendorRequestsAwaitingApproval();
                    }
                }

                if (Helper.IsFTRole)
                {
                    fundsTransfersCounts = await _domainService.AccountsPayableOperations.GetFundsTransferCounts();

                    if (Helper.IsFTReviewerRole)
                    {
                        
                        integrationCountsModel.UnroutedFundsTransferCount = fundsTransfersCounts
                            .Where(c => c.BucketId == Constants.UnroutedBucketId)
                            .First().Count;

                        integrationCountsModel.ReadyForProcessingFundsTransferCount = fundsTransfersCounts
                            .Where(c => c.BucketId == 0)
                            .First().Count;
                    }

                    if (Helper.IsFTApproverRole)
                    {
                        integrationCountsModel.RestrictedFundsTransferCount = fundsTransfersCounts
                            .Where(c => c.BucketId == Constants.RestrictedBucketId)
                            .First().Count;

                        integrationCountsModel.UnrestrictedFundsTransferCount = fundsTransfersCounts
                            .Where(c => c.BucketId == Constants.UnrestrictedBucketId)
                            .First().Count;

                        integrationCountsModel.EndowmentFundsTransferCount = fundsTransfersCounts
                            .Where(c => c.BucketId == Constants.EndowmentBucketId)
                            .First().Count;

                        integrationCountsModel.GiftFundsTransferCount = fundsTransfersCounts
                            .Where(c => c.BucketId == Constants.GiftBucketId)
                            .First().Count;
                    }

                    if (Helper.IsFTGeneralCounselApproverRole)
                    {
                        integrationCountsModel.GeneralCounselFundsTansferCount = fundsTransfersCounts
                            .Where(c => c.BucketId == Constants.GeneralCounselBucketId)
                            .First().Count;
                    }

                }

                var readyForProcessingCount = await ReadyForProcessingDisbursementCount();
                integrationCountsModel.ETDisbursementCount = disbursementResults.ETCount;
                integrationCountsModel.STDisbursementCount = disbursementResults.STCount;
                integrationCountsModel.EMDisbursementCount = disbursementResults.EMCount;
                integrationCountsModel.ReadyForProcessingDisbursementCount = !Helper.IsAPRole ? 0 : readyForProcessingCount;
                integrationCountsModel.GiftTransmittalCount = gtCounts + guCounts + initCounts + bursarCounts + preparerCounts + secondaryCounts + gtFinalizeCounts + guFinalizeCounts;
                integrationCountsModel.UaCount = guCounts;
                integrationCountsModel.UafCount = gtCounts;
                integrationCountsModel.InitializedCount = initCounts;
                integrationCountsModel.WaitingForBursarCount = bursarCounts;
                integrationCountsModel.WaitingForPreparerCount = preparerCounts;
                integrationCountsModel.SecondaryCount = secondaryCounts;
                integrationCountsModel.GTFinalizeCount = gtFinalizeCounts;
                integrationCountsModel.GUFinalizeCount = guFinalizeCounts;
                integrationCountsModel.NewVendorCount = vendorResults.Count();


                _logger.LogInformation("Finished getting disbursements");

            }

            return integrationCountsModel;
        }
        #endregion


        #region State

        public IEnumerable<SelectListItem> GetStateList()
        {
            return BuildStateList("State");
        }

        public IEnumerable<SelectListItem> GetStateList(string selectedStateCode)
        {
            return BuildStateList("State", selectedStateCode);
        }

        protected IEnumerable<SelectListItem> BuildStateList(string DefaultDisplayName, string selectedStateCode = "")
        {
            List<SelectListItem> states = _context.States.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Code,
                Selected = String.IsNullOrEmpty(selectedStateCode) ? false : s.Code.Equals(selectedStateCode)
            }).ToList();

            states.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return states;
        }
        public IActionResult GetState(string Id)
        {
            var states = _context.States.Single(t => t.Code == Id);
            var constituentAddressStateDetails = new ConstituentStateDetailViewModel()
            {
                MasterRecordId = states.Code,
                State = states.Name,
                SourceSystemRecordId = null
            }; return new JsonResult(constituentAddressStateDetails);
        }

        #endregion

        #region Country

        public List<SelectListItem> GetCountryList()
        {
            return BuildCountryList("Country");
        }

        public List<SelectListItem> GetCountryList(string selectedCountryCode)
        {
            return BuildCountryList("Country", selectedCountryCode);
        }


        protected List<SelectListItem> BuildCountryList(string DefaultDisplayName, string selectedCountryCode = "")
        {
            List<SelectListItem> status = _context.Countries
                .OrderBy(m => m.Name)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Code,
                    Selected = String.IsNullOrWhiteSpace(selectedCountryCode) ? false : s.Code.Equals(selectedCountryCode)
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetCountry(string Id)
        {
            var Countries = _context.Countries.Single(t => t.Code == Id);

            var CountryDetails = new CountryDetailViewModel()
            {
                Name = Countries.Name,
                Code = Id,
                MasterRecordId = Countries.Code,
                SourceSystemRecordId = null
            };

            return new JsonResult(CountryDetails);
        }

        #endregion

        #region CountryDialingCode (Phone)

        public List<SelectListItem> GetCountryDialingCodeList()
        {
            return GetCountryDialingCodeList("Country Code");
        }

        protected List<SelectListItem> GetCountryDialingCodeList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.CountryDialingCodes
                .OrderBy(m => m.DialingCodeName)
                .Select(s => new SelectListItem()
                {
                    Text = $"{s.DialingCodeName} ({s.CountryDisplayName})",
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetCountryDialingCode(string Id)
        {
            var CountryDialingCodes = _context.CountryDialingCodes.Single(t => t.MasterRecordId == Id);

            var CountryDialingCodeDetails = new CountryDialingCodeDetailViewModel()
            {
                Name = CountryDialingCodes.DialingCodeName,
                MasterRecordId = Id,
                CountrySourceSystemRecordId = null
            };

            return new JsonResult(CountryDialingCodeDetails);
        }

        #endregion

        #region Organization

        protected IEnumerable<SelectListItem> GetOrganizationList()
        {
            return GetOrganizationList("Organization");
        }

        protected IEnumerable<SelectListItem> GetOrganizationList(string DefaultDisplayName)
        {
            List<SelectListItem> organizations = _context.Organizations
                .Where(o => o.Name == "The University of Arizona")
                .Select(s => new SelectListItem()
            {
                Value = s.Code,
                Text = s.Name
            }).ToList();

            organizations.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return organizations;
        }

        #endregion

        #region Organizational Unit

        protected IEnumerable<SelectListItem> GetOrganizationalUnitList()
        {
            return GetOrganizationalUnitList("Organizational Unit");
        }

        protected IEnumerable<SelectListItem> GetOrganizationalUnitList(string DefaultDisplayName)
        {
            List<SelectListItem> organizationalUnitList = _context.OrganizationalUnitParents.Select(s => new SelectListItem()
            {
                Value = s.Code,
                Text = s.Name
            }).ToList();

            organizationalUnitList.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return organizationalUnitList;
        }

        #endregion

        #region Department

        protected IEnumerable<SelectListItem> GetDepartmentList()
        {
            return GetDepartmentList("Department");
        }

        protected IEnumerable<SelectListItem> GetDepartmentList(string DefaultDisplayName)
        {
            List<SelectListItem> organizationalUnitList = _context.OrganizationalUnits.Where(m => m.Type == "Department").Select(s => new SelectListItem()
            {
                Value = s.Code,
                Text = $"{s.OrganizationalUnitCode} - {s.Name}"
            }).ToList();

            organizationalUnitList.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return organizationalUnitList;
        }

        protected List<SelectListItem> GetUADepartmentList()
        {
            return GetUADepartmentList(string.Format(DEFAULT_PLACEHOLDER, "Department"));
        }

        protected List<SelectListItem> GetUADepartmentList(string DefaultDisplayName)
        {
            List<SelectListItem> organizationalUnitList = _context.OrganizationalUnits
                .Where(m => m.Type == "Department" && m.OrganizationMasterRecordId == "1")
                .OrderBy(m => m.Name)
                .Select(s => new SelectListItem()
                {
                    Text = $"{s.Name} {s.Code}",
                    Value = s.Code
                }).ToList();

            organizationalUnitList.Insert(0, new SelectListItem() { Value = string.Empty, Text = DefaultDisplayName });

            return organizationalUnitList;
        }

        #endregion

        #region Designation Type

        protected IEnumerable<SelectListItem> GetDesignationTypeList()
        {
            return GetDesignationTypeList("Designation Type");
        }

        protected IEnumerable<SelectListItem> GetDesignationTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> types = _context.DesignationTypes.Select(s => new SelectListItem()
            {
                Value = s.DesignationTypeCode,
                Text = s.DesignationTypeName
            }).ToList();

            types.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return types;
        }

        public IActionResult GetDesignationTypeMasterId(string Id)
        {
            var designationTypes = _context.DesignationTypes.Single(d => d.DesignationTypeCode == Id);

            var designationTypeDetails = new DesignationTypeDetailViewModel()
            {
                DesignationTypeCode = Id,
                DesignationTypeName = designationTypes.DesignationTypeName,
                DesignationTypeSourceSystemRecordId = null
            };

            return new JsonResult(designationTypeDetails);
        }

        #endregion

        #region VSECategoryList

        public List<SelectListItem> GetVSECategoryList()
        {
            return GetVSECategoryList("VSE Category");
        }

        protected List<SelectListItem> GetVSECategoryList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.VSECategories
                .OrderBy(m => m.Id)
                .Select(s => new SelectListItem()
                {
                    Text = s.VSECategoryName,
                    Value = s.Id
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetVSECategory(string Id)
        {
            var VSECategories = _context.VSECategories.Single(t => t.Id == Id);
            var VSECategoryDetails = new VSECategoryDetailViewModel()
            {
                VSECategoryName = VSECategories.VSECategoryName,
                VSECategorySourceSystemRecordId = null,
                Id = VSECategories.Id
            };

            return new JsonResult(VSECategoryDetails);
        }

        #endregion

        #region GLOrganizationList

        public List<SelectListItem> GetGLOrganizationList()
        {
            return GetGLOrganizationList("Organization");
        }

        protected List<SelectListItem> GetGLOrganizationList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.Organizations
                .Where(s => s.Abbreviation != null)
                .OrderBy(m => m.Id)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Code
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        #endregion

        #region DesignationUseTypeList

        public List<SelectListItem> GetDesignationUseTypeList()
        {
            return GetDesignationUseTypeList("Use Type");
        }

        protected List<SelectListItem> GetDesignationUseTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.DesignationUseTypes
                .OrderBy(m => m.Name)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Code
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        public IActionResult GetDesignationUseTypeMasterId(string Id)
        {
            var designationUseTypes = _context.DesignationUseTypes.Single(d => d.Code == Id);

            var designationUseTypeDetails = new DesignationUseTypeDetailViewModel()
            {
                Code = Id,
                Name = designationUseTypes.Name
            };

            return new JsonResult(designationUseTypeDetails);
        }

        #endregion

        #region Designation Status

        protected IEnumerable<SelectListItem> GetDesignationStatusList()
        {
            return GetDesignationStatusList("Designation Status");
        }

        protected IEnumerable<SelectListItem> GetDesignationStatusList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.DesignationStatus.Select(s => new SelectListItem()
            {
                Value = s.DesignationStatusMasterId,
                Text = s.DesignationStatusName
            }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        #endregion

        #region Managers

        protected IEnumerable<SelectListItem> GetManagerList()
        {
            return GetManagerList("Manager");
        }

        protected IEnumerable<SelectListItem> GetManagerList(string DefaultDisplayName)
        {
            List<SelectListItem> managerList = _context.Employees.Select(s => new SelectListItem()
            {
                Value = s.EmployeeCode,
                Text = s.Name
            }).ToList();

            managerList.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return managerList;
        }

        #endregion

        #region EmployeeType

        protected IEnumerable<SelectListItem> GetEmployeeTypeList()
        {
            return GetEmployeeTypeList("Employee Type");
        }

        protected IEnumerable<SelectListItem> GetEmployeeTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> types = _context.EmployeeTypes.Select(s => new SelectListItem()
            {
                Value = s.Id,
                Text = s.EmployeeTypeName
            }).ToList();

            types.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return types;
        }

        public IActionResult GetEmployeeTypeMasterId(string Id)
        {
            var employeeTypes = _context.EmployeeTypes.Single(d => d.Id == Id);

            var employeeTypeDetails = new EmployeeTypeDetailViewModel()
            {
                EmployeeTypeMasterId = Id,
                EmployeeTypeName = employeeTypes.EmployeeTypeName,
                EmployeeTypeSourceSystemRecordId = null
            };

            return new JsonResult(employeeTypeDetails);
        }

        #endregion

        #region Employee MasterId
        public IActionResult GetEmployeeDetailsByMasterId(string Id)
        {
            var employee = _context.Employees.Single(s => s.EmployeeCode == Id);
            var employeeDetails = new EmployeeDetailViewModel()
            {
                EmployeeCode = employee.EmployeeCode,
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName
            };

            return new JsonResult(employeeDetails);
        }
        #endregion

        #region Organization MasterId
        public IActionResult GetOrganizationDetailsByMasterId(string Id)
        {
            var organizations = _context.Organizations.Single(d => d.Code == Id);

            var organizationDetails = new OrganizationDetailViewModel()
            {
                OrganizationMasterId = Id,
                OrganizationName = organizations.Name,
                OrganizationCode = organizations.Abbreviation
            };

            return new JsonResult(organizationDetails);
        }
        #endregion

        #region Organizational Unit MasterId
        public IActionResult GetOrganizationalUnitDetailsByMasterId(string Id)
        {
            try
            {
                var department = _context.OrganizationalUnits.Single(d => d.Code == Id);

                var organizationalUnitDetails = new OrganizationalUnitDetailViewModel()
                {
                    OrganizationalUnitMasterId = Id,
                    OrganizationalUnitName = department.Name,
                    OrganizationalUnitCode = department.OrganizationalUnitCode,
                    OrganizationalUnitType = department.Type
                };

                return new JsonResult(organizationalUnitDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unable to retrieve organizational unit [{Id}]");
                throw;
            }
        }
        #endregion

        #region Student Enrollment Campus

        protected IEnumerable<SelectListItem> GetStudentEnrollmentCampusList()
        {
            return GetStudentEnrollmentCampusList("Campus");
        }

        protected IEnumerable<SelectListItem> GetStudentEnrollmentCampusList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.StudentEnrollmentCampus.Select(s => new SelectListItem()
            {
                Text = s.EnrollmentCampusName,
                Value = s.EnrollmentCampusCode
            }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        protected IEnumerable<SelectListItem> GetStudentEnrollmentLocationList()
        {
            return GetStudentEnrollmentLocationList(string.Format(DEFAULT_PLACEHOLDER, "Location"));
        }

        protected IEnumerable<SelectListItem> GetStudentEnrollmentLocationList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.StudentEnrollmentLocations.Select(s => new SelectListItem()
            {
                Text = s.EnrollmentLocationName,
                Value = s.EnrollmentLocationCode
            }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = DefaultDisplayName });

            return status;
        }

        #endregion

        #region Student Enrollment (dropdown for Academic Plan)

        public List<SelectListItem> GetStudentEnrollmentDropdownList(string StudentMasterId)
        {
            return GetStudentEnrollmentList("Enrollment", StudentMasterId);
        }

        protected List<SelectListItem> GetStudentEnrollmentList(string DefaultDisplayName, string StudentMasterId)
        {
            List<SelectListItem> status = _context.GetStudentEnrollments(StudentMasterId)
                .OrderBy(m => m.TermName)
                .Select(s => new SelectListItem()
                {
                    Text = $"{s.TermName} - {s.CampusName} ({s.AcademicCareerName})",
                    Value = $"{s.TermMasterId}|{s.CampusMasterId}"
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetStudentEnrollment(string Id, string StudentMasterId)
        {
            StudentEnrollmentDetailViewModel studentEnrollmentDetails = null;

            foreach (var enrollment in _context.GetStudentEnrollments(StudentMasterId))
            {
                if (Id == $"{enrollment.TermMasterId}|{enrollment.CampusMasterId}")
                {
                    studentEnrollmentDetails = new StudentEnrollmentDetailViewModel()
                    {
                        MasterRecordId = Id,
                        CampusMasterId = enrollment.CampusMasterId,
                        TermName = enrollment.TermName,
                        TermMasterId = enrollment.TermMasterId,
                        TermSourceSystemRecordId = null
                    };

                    break;
                }
            }

            return new JsonResult(studentEnrollmentDetails);
        }

        #endregion

        #region Academic Level

        public List<SelectListItem> GetAcademicLevelList()
        {
            return GetAcademicLevelList("Academic Level");
        }

        protected List<SelectListItem> GetAcademicLevelList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.AcademicLevels
                .OrderBy(m => m.AcademicLevelName)
                .Select(s => new SelectListItem()
                {
                    Text = s.AcademicLevelName,
                    Value = s.AcademicLevelSourceSystemRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetAcademicLevel(string Id)
        {
            var academicLevels = _context.AcademicLevels.Single(t => t.AcademicLevelSourceSystemRecordId == Id);

            var studentNameDetails = new AcademicLevelDetailViewModel()
            {
                AcademicLevelMasterId = Id,
                AcademicLevelName = academicLevels.AcademicLevelName,
                AcademicLevelSourceSystemRecordId = null
            };

            return new JsonResult(studentNameDetails);
        }

        #endregion

        #region Academic Plan Status

        public List<SelectListItem> GetAcademicPlanStatusList()
        {
            return GetAcademicPlanStatusList("Academic Plan Status");
        }

        protected List<SelectListItem> GetAcademicPlanStatusList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.AcademicPlanStatuses
                .OrderBy(m => m.AcademicPlanStatus)
                .Select(s => new SelectListItem()
                {
                    Text = s.AcademicPlanStatus,
                    Value = s.AcademicPlanStatusMasterId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetAcademicPlanStatus(string Id)
        {
            var academicPlanStatuses = _context.AcademicPlanStatuses.Single(t => t.AcademicPlanStatusMasterId == Id);

            var studentAcademicPlanStatusDetails = new StudentAcademicPlanStatusDetailViewModel()
            {
                StudentAcademicPlanStatusMasterId = Id,
                StudentAcademicPlanStatus = academicPlanStatuses.AcademicPlanStatus,
                StudentAcademicPlanStatusSourceSystemRecordId = null
            };

            return new JsonResult(studentAcademicPlanStatusDetails);
        }

        #endregion

        #region Campus

        public List<SelectListItem> GetCampusList()
        {
            return GetCampusList("Campus");
        }

        protected List<SelectListItem> GetCampusList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.Campuses
                .OrderBy(m => m.CampusName)
                .Select(s => new SelectListItem()
                {
                    Text = s.CampusName,
                    Value = s.CampusMasterId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetCampus(string Id)
        {
            var campuses = _context.Campuses.Single(t => t.CampusMasterId == Id);

            var campusDetails = new CampusDetailViewModel()
            {
                CampusMasterId = Id,
                CampusName = campuses.CampusName,
                CampusSourceSystemRecordId = null
            };

            return new JsonResult(campusDetails);
        }

        #endregion

        #region Student Academic Invovement

        #region Type

        // Used for instantiating the List of Types:
        public IEnumerable<SelectListItem> GetStudentAcademicInvolvementTypeList()
        {
            return GetStudentAcademicInvolvementTypeList("Type");
        }

        protected IEnumerable<SelectListItem> GetStudentAcademicInvolvementTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.StudentAcademicInvolvementTypes.Select(s => new SelectListItem()
            {
                Text = s.AcademicInvolvementTypeName,
                Value = s.AcademicInvolvementTypeCode
            }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetStudentAcademicInvolvementTypeMasterId(string Id)
        {
            var studentAcademicInvolvementTypes = _context.StudentAcademicInvolvementTypes.Single(t => t.AcademicInvolvementTypeCode == Id);

            var studentAcademicInvolvementTypeDetails = new StudentAcademicInvolvementTypeDetailViewModel()
            {
                AcademicInvolvementTypeCode = Id,
                AcademicInvolvementTypeName = studentAcademicInvolvementTypes.AcademicInvolvementTypeName
            };

            return new JsonResult(studentAcademicInvolvementTypeDetails);
        }

        #endregion

        #region Name

        // Used for instantiating the List of Names:
        public IEnumerable<SelectListItem> GetStudentAcademicInvolvementNameList()
        {
            return GetStudentAcademicInvolvementNameList("Type");
        }

        protected IEnumerable<SelectListItem> GetStudentAcademicInvolvementNameList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.StudentAcademicInvolvementNames.Select(s => new SelectListItem()
            {
                Text = s.AcademicInvolvementNameName,
                Value = s.AcademicInvolvementNameCode
            }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetStudentAcademicInvolvementNameMasterId(string Id)
        {
            var studentAcademicInvolvementNames = _context.StudentAcademicInvolvementNames.Single(t => t.AcademicInvolvementNameCode == Id);

            var studentAcademicInvolvementNameDetails = new StudentAcademicInvolvementNameDetailViewModel()
            {
                AcademicInvolvementNameCode = Id,
                AcademicInvolvementNameName = studentAcademicInvolvementNames.AcademicInvolvementNameName
            };

            return new JsonResult(studentAcademicInvolvementNameDetails);
        }

        #endregion

        #region Term

        // Used for instantiating the List of Terms:
        public IEnumerable<SelectListItem> GetStudentAcademicInvolvementTermList()
        {
            return GetStudentAcademicInvolvementTermList("Term");
        }

        protected IEnumerable<SelectListItem> GetStudentAcademicInvolvementTermList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.StudentAcademicInvolvementTerms.Select(s => new SelectListItem()
            {
                Text = s.AcademicInvolvementTermName,
                Value = s.AcademicInvolvementTermCode
            }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetStudentAcademicInvolvementTermMasterId(string Id)
        {
            var studentAcademicInvolvementTerms = _context.StudentAcademicInvolvementTerms.Single(t => t.AcademicInvolvementTermCode == Id);

            var studentAcademicInvolvementTermDetails = new StudentAcademicInvolvementTermDetailViewModel()
            {
                AcademicInvolvementTermCode = Id,
                AcademicInvolvementTermName = studentAcademicInvolvementTerms.AcademicInvolvementTermName
            };

            return new JsonResult(studentAcademicInvolvementTermDetails);
        }

        #endregion

        #endregion

        #region Student Academic Catalog (expand for more)

        #region DegreeType

        // Used for instantiating the List of Types:
        public List<SelectListItem> GetStudentAcademicCatalogDegreeTypeList()
        {
            return GetStudentAcademicCatalogDegreeTypeList("Degree Type");
        }

        protected List<SelectListItem> GetStudentAcademicCatalogDegreeTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.StudentAcademicCatalogDegreeTypes
                .OrderBy(m => m.AcademicCatalogDegreeTypeName)
                .Select(s => new SelectListItem()
                {
                    Text = s.AcademicCatalogDegreeTypeName,
                    Value = s.AcademicCatalogDegreeTypeCode
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetStudentAcademicCatalogDegreeType(string Id)
        {
            var studentAcademicCatalogDegreeTypes = _context.StudentAcademicCatalogDegreeTypes.Single(t => t.AcademicCatalogDegreeTypeCode == Id);

            var studentAcademicCatalogDegreeTypeDetails = new StudentAcademicCatalogDegreeTypeDetailViewModel()
            {
                AcademicCatalogDegreeTypeName = studentAcademicCatalogDegreeTypes.AcademicCatalogDegreeTypeName,
                AcademicCatalogDegreeTypeCode = Id
            };

            return new JsonResult(studentAcademicCatalogDegreeTypeDetails);
        }

        #endregion

        #region AcademicProgram Dropdown

        // Used for instantiating the List of Types:
        public List<SelectListItem> GetStudentAcademicCatalogAcademicProgramList()
        {
            return GetStudentAcademicCatalogAcademicProgramList("Academic Program");
        }

        protected List<SelectListItem> GetStudentAcademicCatalogAcademicProgramList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.StudentAcademicCatalogAcademicPrograms
                .OrderBy(m => m.Name)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetStudentAcademicCatalogAcademicProgram(string Id)
        {
            var studentAcademicCatalogAcademicPrograms = _context.StudentAcademicCatalogAcademicPrograms.Single(t => t.MasterRecordId == Id);

            var studentAcademicCatalogAcademicProgramDetails = new StudentAcademicCatalogAcademicProgramDetailViewModel()
            {
                Name = studentAcademicCatalogAcademicPrograms.Name,
                MasterRecordId = Id,
                AcademicProgramSourceSystemRecordId = null
            };

            return new JsonResult(studentAcademicCatalogAcademicProgramDetails);
        }

        #endregion

        #region AcademicPlan Dropdown

        // Used for instantiating the List of Types:
        public List<SelectListItem> GetStudentAcademicCatalogAcademicPlanList()
        {
            return GetStudentAcademicCatalogAcademicPlanList("Academic Plan");
        }

        protected List<SelectListItem> GetStudentAcademicCatalogAcademicPlanList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.StudentAcademicCatalogAcademicPlans
                .OrderBy(m => m.Name)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetStudentAcademicCatalogAcademicPlan(string Id)
        {
            var studentAcademicCatalogAcademicPlans = _context.StudentAcademicCatalogAcademicPlans.Single(t => t.MasterRecordId == Id);

            var studentAcademicCatalogAcademicPlanDetails = new StudentAcademicCatalogAcademicPlanDetailViewModel()
            {
                Name = studentAcademicCatalogAcademicPlans.Name,
                MasterRecordId = Id,
                AcademicPlanSourceSystemRecordId = null
            };

            return new JsonResult(studentAcademicCatalogAcademicPlanDetails);
        }

        #endregion

        #region AcademicSubplan Dropdown

        // Used for instantiating the List of Types:
        public List<SelectListItem> GetAcademicSubplanList()
        {
            return GetAcademicSubplanList("Academic Subplan");
        }

        protected List<SelectListItem> GetAcademicSubplanList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.AcademicSubplans
                .OrderBy(m => m.AcademicSubplanName)
                .Select(s => new SelectListItem()
                {
                    Text = s.AcademicSubplanName,
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetAcademicSubplan(string Id)
        {
            var studentAcademicSubplans = _context.AcademicSubplans.Single(t => t.MasterRecordId == Id);

            var studentAcademicSubplanDetails = new AcademicSubplanDetailViewModel()
            {
                Name = studentAcademicSubplans.AcademicSubplanName,
                AcademicSubplanSourceSystemRecordId = null,
                MasterRecordId = Id
            };

            return new JsonResult(studentAcademicSubplanDetails);
        }

        #endregion

        #region AcademicPlanType

        // Used for instantiating the List of Types:
        public List<SelectListItem> GetStudentAcademicCatalogAcademicPlanTypeList()
        {
            return GetStudentAcademicCatalogAcademicPlanTypeList("Academic Plan Type");
        }

        protected List<SelectListItem> GetStudentAcademicCatalogAcademicPlanTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.StudentAcademicCatalogAcademicPlanTypes
                .OrderBy(m => m.Name)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetStudentAcademicCatalogAcademicPlanType(string Id)
        {
            var studentAcademicCatalogAcademicPlanTypes = _context.StudentAcademicCatalogAcademicPlanTypes.Single(t => t.MasterRecordId == Id);

            var studentAcademicCatalogAcademicPlanTypeDetails = new StudentAcademicCatalogAcademicPlanTypeDetailViewModel()
            {
                Name = studentAcademicCatalogAcademicPlanTypes.Name,
                MasterRecordId = Id
            };

            return new JsonResult(studentAcademicCatalogAcademicPlanTypeDetails);
        }

        #endregion

        #region AC Department

        // Used for instantiating the List of Types:
        public List<SelectListItem> GetStudentAcademicCatalogDepartmentList()
        {
            return GetStudentAcademicCatalogDepartmentList("Department");
        }

        protected List<SelectListItem> GetStudentAcademicCatalogDepartmentList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.OrganizationalUnits
                .Where(m => m.OrganizationName == "The University of Arizona" && m.Type == "Department")
                .OrderBy(m => m.Name)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Code
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetStudentAcademicCatalogDepartment(string Id)
        {
            var studentAcademicCatalogAcademicPlanTypes = _context.OrganizationalUnits.Single(t => t.Code == Id);

            var studentAcademicCatalogAcademicPlanTypeDetails = new StudentAcademicCatalogAcademicPlanTypeDetailViewModel()
            {
                Name = studentAcademicCatalogAcademicPlanTypes.Name,
                MasterRecordId = Id,
                Code = studentAcademicCatalogAcademicPlanTypes.OrganizationalUnitCode
            };

            return new JsonResult(studentAcademicCatalogAcademicPlanTypeDetails);
        }

        #endregion

        #endregion

        #region Student

        public List<SelectListItem> GetStudentList()
        {
            return GetStudentList("Student");
        }

        protected List<SelectListItem> GetStudentList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.StudentNames
                .OrderBy(m => m.Name)
                .Select(s => new SelectListItem()
                {
                    Text = $"{s.Name} {s.StudentId}",
                    Value = s.StudentMasterId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetStudent(string Id)
        {
            var studentNames = _context.StudentNames.Single(t => t.StudentMasterId == Id);

            var studentNameDetails = new StudentNameDetailViewModel()
            {
                StudentMasterId = Id,
                FirstName = studentNames.FirstName,
                MiddleName = studentNames.MiddleName,
                LastName = studentNames.LastName,
                Name = studentNames.Name,
                StudentFirstName = studentNames.FirstName,
                StudentLastName = studentNames.LastName,
                StudentId = studentNames.StudentId,
                StudentSourceSystemRecordId = null
            };

            return new JsonResult(studentNameDetails);
        }

        #endregion

        #region Constituent

        public List<SelectListItem> GetConstituentList()
        {
            return GetConstituentList("Constituent");
        }

        protected List<SelectListItem> GetConstituentList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.PersonNames
                .OrderBy(m => m.LastName)
                .Select(s => new SelectListItem()
                {
                    Text = $"{s.LastName}, {s.FirstName} ({s.UAPersonId})",
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetConstituent(string Id)
        {
            var constituentNames = _context.PersonNames.Single(t => t.MasterRecordId == Id);

            var constituentNameDetails = new ConstituentNameDetailViewModel()
            {
                MasterRecordId = Id,
                FirstName = constituentNames.FirstName,
                LastName = constituentNames.LastName,
                UAPersonId = constituentNames.UAPersonId,
                ConstituentSourceSystemRecordId = null
            };

            return new JsonResult(constituentNameDetails);
        }

        #endregion

        #region Educational Institution

        public List<SelectListItem> GetEducationalInstitutionList()
        {
            return GetEducationalInstitutionList("Educational Institution");
        }

        protected List<SelectListItem> GetEducationalInstitutionList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.EducationalInstitutions
                .Where(e => e.Type == "University")
                .OrderBy(m => m.Name)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.EducationalInstitutionMasterId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetEducationalInstitution(string Id)
        {
            var educationalInstitutions = _context.EducationalInstitutions.Single(t => t.EducationalInstitutionMasterId == Id);

            var educationalInstitutionDetails = new EducationalInstitutionDetailViewModel()
            {
                EducationalInstitutionMasterId = Id,
                EducationalInstitution = educationalInstitutions.Name,
                EducationalInstitutionSourceSystemRecordId = null
            };

            return new JsonResult(educationalInstitutionDetails);
        }

        #endregion

        #region Suffix

        public List<SelectListItem> GetSuffixList()
        {
            return BuildSuffixList("Suffix");
        }

        public List<SelectListItem> GetSuffixList(string selectedSuffixCode)
        {
            return BuildSuffixList("Suffix", selectedSuffixCode);
        }

        protected List<SelectListItem> BuildSuffixList(string DefaultDisplayName, string selectedSuffixCode = "")
        {
            List<SelectListItem> status = _context.Suffixes
                .OrderBy(m => m.Name)
                .Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Code,
                    Selected = String.IsNullOrWhiteSpace(selectedSuffixCode) ? false : s.Code.Equals(selectedSuffixCode)
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetSuffix(string Id)
        {
            var suffixes = _context.Suffixes.Single(t => t.Code == Id);

            var suffixDetails = new SuffixDetailViewModel()
            {
                SuffixName = suffixes.Name,
                SuffixCode = Id,
                SuffixSourceSystemRecordId = null
            };

            return new JsonResult(suffixDetails);
        }

        #endregion

        #region Title

        public List<SelectListItem> GetTitleList()
        {
            return BuildTitleList("Title");
        }

        public List<SelectListItem> GetTitleList(string selectedTitleCode)
        {
            return BuildTitleList("Title", selectedTitleCode);
        }

        protected List<SelectListItem> BuildTitleList(string DefaultDisplayName, string selectedTitleCode = "")
        {
            List<SelectListItem> status = _context.Titles
                .OrderBy(m => m.Title_Name)
                .Select(s => new SelectListItem()
                {
                    Text = s.Title_Name,
                    Value = s.Title_Code,
                    Selected = String.IsNullOrEmpty(selectedTitleCode) ? false : s.Title_Code.Equals(selectedTitleCode)
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetTitle(string Id)
        {
            var titles = _context.Titles.Single(t => t.Title_Code == Id);

            var titleDetails = new TitleDetailViewModel()
            {
                TitleName = titles.Title_Name,
                TitleCode = Id,
                TitleSourceSystemRecordId = null
            };

            return new JsonResult(titleDetails);
        }

        #endregion

        #region MaritalStatus

        public List<SelectListItem> GetMaritalStatusList()
        {
            return GetMaritalStatusList("Marital Status");
        }

        protected List<SelectListItem> GetMaritalStatusList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.MaritalStatuses
                .OrderBy(m => m.MaritalStatus_Name)
                .Select(s => new SelectListItem()
                {
                    Text = s.MaritalStatus_Name,
                    Value = s.MaritalStatus_Code
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetMaritalStatus(string Id)
        {
            var MaritalStatuses = _context.MaritalStatuses.Single(t => t.MaritalStatus_Code == Id);

            var MaritalStatusDetails = new MaritalStatusDetailViewModel()
            {
                MaritalStatus_Name = MaritalStatuses.MaritalStatus_Name,
                MaritalStatus_Code = Id,
                MaritalStatusSourceSystemRecordId = null
            };

            return new JsonResult(MaritalStatusDetails);
        }

        #endregion

        #region Honor List

        public List<SelectListItem> GetHonorList()
        {
            return GetHonorList("Honor");
        }

        protected List<SelectListItem> GetHonorList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.Honors
                .OrderBy(m => m.HonorName)
                .Select(s => new SelectListItem()
                {
                    Text = s.HonorName,
                    Value = s.HonorMasterId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetHonor(string Id)
        {
            var Honors = _context.Honors.Single(t => t.HonorMasterId == Id);

            var HonorDetails = new HonorDetailViewModel()
            {
                HonorName = Honors.HonorName,
                HonorMasterId = Id,
                HonorSourceSystemRecordId = null
            };

            return new JsonResult(HonorDetails);
        }

        #endregion

        #region DegreeStatus

        public List<SelectListItem> GetDegreeStatusList()
        {
            return GetDegreeStatusList("Degree Status");
        }

        protected List<SelectListItem> GetDegreeStatusList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.DegreeStatuses
                .OrderBy(m => m.StatusName)
                .Select(s => new SelectListItem()
                {
                    Text = s.StatusName,
                    Value = s.StatusMasterId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetDegreeStatus(string Id)
        {
            var DegreeStatus = _context.DegreeStatuses.Single(t => t.StatusMasterId == Id);

            var DegreeStatusDetails = new DegreeStatusDetailViewModel()
            {
                DegreeStatusName = DegreeStatus.StatusName,
                DegreeStatusMasterId = Id,
                DegreeStatusSourceSystemRecordId = null
            };

            return new JsonResult(DegreeStatusDetails);
        }

        #endregion

        #region DegreeType

        public List<SelectListItem> GetDegreeTypeList()
        {
            return GetDegreeTypeList("Degree Type");
        }

        protected List<SelectListItem> GetDegreeTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.DegreeTypes
                .OrderBy(m => m.DegreeTypeName)
                .Select(s => new SelectListItem()
                {
                    Text = s.DegreeTypeName,
                    Value = s.DegreeTypeCode
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetDegreeType(string Id)
        {
            var DegreeType = _context.DegreeTypes.Single(t => t.DegreeTypeCode == Id);

            var DegreeTypeDetails = new DegreeTypeDetailViewModel()
            {
                DegreeTypeName = DegreeType.DegreeTypeName,
                DegreeTypeCode = Id,
                DegreeStatusSourceSystemRecordId = null
            };

            return new JsonResult(DegreeTypeDetails);
        }

        #endregion

        #region Academic Career

        public List<SelectListItem> GetDischargedAcademicCareerList()
        {
            return GetDischargedAcademicCareerList("Academic Career");
        }

        protected List<SelectListItem> GetDischargedAcademicCareerList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.AcademicCareers
                //.OrderBy(m => m.AcademicYear)
                .Select(s => new SelectListItem()
                {
                    Text = $"{s.Name}",
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetAcademicCareer(string Id)
        {
            var AcademicCareers = _context.AcademicCareers.Single(t => t.MasterRecordId == Id);

            var AcademicCareerDetails = new AcademicCareerDetailViewModel()
            {
                AcademicCareerName = AcademicCareers.Name,
                //AcademicCareerCode = AcademicCareers.AcademicCareerCode,
                AcademicCareerMasterId = AcademicCareers.MasterRecordId,
                AcademicCareerSourceSystemRecordId = null
            };

            return new JsonResult(AcademicCareerDetails);
        }

        #endregion

        #region Academic Term

        public List<SelectListItem> GetAcademicTermList()
        {
            return GetAcademicTermList("Academic Term");
        }

        protected List<SelectListItem> GetAcademicTermList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.AcademicTerms.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.AcademicTermMasterId
            }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        public IActionResult GetAcedemicCalendarEntry(string Id)
        {
            var term = _context.AcademicCalendarEntries.Single(t => t.Id == Id);

            var dischargeTermDetails = new AcademicCalendarDetailViewModel()
            {
                TermMasterId = Id,
                TermName = term.Name,
                AcademicCareerName = term.AcademicCareer,
                AcademicCareerCode = null,
                AcademicTermCode = term.AcademicTermCode
            };

            return new JsonResult(dischargeTermDetails);
        }

        #endregion

        #region Designation

        public List<SelectListItem> GetDesignationList()
        {
            return GetDesignationList("Designation");
        }

        protected List<SelectListItem> GetDesignationList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.Designations
                .OrderBy(m => m.DesignationId)
                .Select(s => new SelectListItem()
                {
                    Text = $"{s.DesignationId} {s.DesignationName}",
                    Value = s.DesignationMasterId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        public IActionResult GetDesignation(string Id)
        {
            var designation = _context.Designations.Single(t => t.DesignationMasterId == Id);

            var designationDetails = new DesignationDetailViewModel()
            {
                DesignationMasterId = Id,
                DesignationName = designation.DesignationName,
                KFSAccount = designation.KFSAccount
            };

            return new JsonResult(designationDetails);
        }

        #endregion

        #region Scholarship

        public List<SelectListItem> GetScholarshipList()
        {
            return GetScholarshipList("Scholarship");
        }

        protected List<SelectListItem> GetScholarshipList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.Scholarships.Select(s => new SelectListItem()
            {
                Text = s.ScholarshipName,
                Value = s.ScholarshipMasterId
            }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        public IActionResult GetScholarship(string Id)
        {
            var scholarship = _context.Scholarships.Single(t => t.ScholarshipMasterId == Id);

            var scholarshipDetails = new ScholarshipDetailViewModel()
            {
                ScholarshipMasterId = Id,
                ScholarshipName = scholarship.ScholarshipName,
                UACode = scholarship.UACode
            };

            return new JsonResult(scholarshipDetails);
        }

        #endregion

        #region Term Code

        public IActionResult GetTermCode(string AcademicYear, string Term)
        {
            var termCode = new TermCodeDetailViewModel()
            {
                TermCode = null
            };

            if ((!string.IsNullOrEmpty(AcademicYear) && !string.IsNullOrEmpty(Term)) && Regex.IsMatch(AcademicYear, "^(19|20)[0-9][0-9]") && Regex.IsMatch(Term, @"^[0-5]$"))
            {
                termCode.TermCode = AcademicYear.Substring(0, 1) + AcademicYear.Substring(2, 2) + Term;
            }

            return new JsonResult(termCode);
        }

        #endregion

        #region PhoneLineType

        public List<SelectListItem> GetPhoneLineTypeList()
        {
            return GetPhoneLineTypeList("Line Type");
        }

        protected List<SelectListItem> GetPhoneLineTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.PhoneLineTypes
                .OrderBy(m => m.LineType)
                .Select(s => new SelectListItem()
                {
                    Text = s.LineType,
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetPhoneLineType(string Id)
        {
            var lineTypes = _context.PhoneLineTypes.Single(t => t.MasterRecordId == Id);

            var lineTypeDetails = new PhoneLineTypeDetailViewModel()
            {
                MasterRecordId = Id,
                LineType = lineTypes.LineType,
                PhoneLineTypeSourceSystemRecordId = null
            };

            return new JsonResult(lineTypeDetails);
        }

        #endregion

        #region PhoneUseType

        public List<SelectListItem> GetPhoneUseTypeList()
        {
            return GetPhoneUseTypeList("Use Type");
        }

        protected List<SelectListItem> GetPhoneUseTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.PhoneUseTypes
                .OrderBy(m => m.UseType)
                .Select(s => new SelectListItem()
                {
                    Text = s.UseType,
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetPhoneUseType(string Id)
        {
            var phoneUseTypes = _context.PhoneUseTypes.Single(t => t.MasterRecordId == Id);

            var useTypeDetails = new PhoneUseTypeDetailViewModel()
            {
                MasterRecordId = Id,
                PhoneUseType = phoneUseTypes.UseType,
                PhoneUseTypeSourceSystemRecordId = null
            };

            return new JsonResult(useTypeDetails);
        }

        #endregion

        #region EmaileUseType

        public List<SelectListItem> GetEmailAddressUseTypeList()
        {
            return GetEmailAddressUseTypeList("Use Type");
        }

        protected List<SelectListItem> GetEmailAddressUseTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.EmailUseTypes
                .OrderBy(m => m.EmailAddressUseType)
                .Select(s => new SelectListItem()
                {
                    Text = s.EmailAddressUseType,
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetEmailAddressUseType(string Id)
        {
            var useTypes = _context.EmailUseTypes.Single(t => t.MasterRecordId == Id);

            var useTypeDetails = new EmailUseTypeDetailViewModel()
            {
                MasterRecordId = Id,
                EmailAddressUseType = useTypes.EmailAddressUseType,
                EmailAddressUseTypeSourceSystemRecordId = null
            };

            return new JsonResult(useTypeDetails);
        }

        #endregion

        #region AddressUseType

        public List<SelectListItem> GetAddressUseTypeList()
        {
            return GetAddressUseTypeList("Use Type");
        }

        protected List<SelectListItem> GetAddressUseTypeList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.AddressUseTypes
                .OrderBy(m => m.UseType)
                .Select(s => new SelectListItem()
                {
                    Text = s.UseType,
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        public IActionResult GetAddressUseType(string Id)
        {
            var addressUseType = _context.AddressUseTypes.Single(t => t.MasterRecordId == Id);

            var constituentAddressUseTypeDetails = new ConstituentAddressUseTypeDetailViewModel()
            {
                MasterRecordId = Id,
                UseType = addressUseType.UseType,
                SourceSystemRecordId = null
            };

            return new JsonResult(constituentAddressUseTypeDetails);
        }

        #endregion


        #region Relationship

        public List<SelectListItem> GetRelationshipList()
        {
            return GetRelationshipList("Relationship");
        }

        protected List<SelectListItem> GetRelationshipList(string DefaultDisplayName)
        {
            List<SelectListItem> status = _context.Relationships
                .OrderBy(m => m.RelationshipType)
                .Select(s => new SelectListItem()
                {
                    Text = s.RelationshipType,
                    Value = s.MasterRecordId
                }).ToList();

            status.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return status;
        }

        // Used for passing the dropdown value to the Form Post (via JS):
        public IActionResult GetRelationship(string Id)
        {
            var useTypes = _context.Relationships.Single(t => t.MasterRecordId == Id);

            var useTypeDetails = new RelationshipDetailViewModel()
            {
                MasterRecordId = Id,
                Relationship = useTypes.RelationshipType
            };

            return new JsonResult(useTypeDetails);
        }

        #endregion

        #region Remove Integration Record

        protected void RemoveIntegrationRecord(int SystemId, int IntegrationId, long RecordId)
        {
            try
            {
                int returnValue = _context.RemoveIntegrationRecord(SystemId, IntegrationId, RecordId);
                if (returnValue.Equals(1))
                {
                    _logger.LogDebug($"Successfully removed record for SystemId [{SystemId}], IntegrationId [{IntegrationId}], RecordId [{RecordId}].");
                }
                else
                {
                    _logger.LogWarning($"RemoveIntegrationRecord returned {returnValue} for SystemId [{SystemId}], IntegrationId [{IntegrationId}], RecordId [{RecordId}].");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FAILURE attempting to remove record for SystemId [{SystemId}], IntegrationId [{IntegrationId}], RecordId [{RecordId}].");
                throw;
            }
        }

        //IntegrationRecordManualMerge
        protected void RemoveIntegrationPossibleMatchRecord(int SystemId, int IntegrationId, long RecordId)
        {
            try
            {
                int returnValue = _context.RemoveIntegrationPossibleMatchRecord(SystemId, IntegrationId, RecordId);
                if (returnValue.Equals(1))
                {
                    _logger.LogDebug($"Successfully removed record for SystemId [{SystemId}], IntegrationId [{IntegrationId}], RecordId [{RecordId}].");
                }
                else
                {
                    _logger.LogWarning($"RemoveIntegrationPossibleMatchRecord returned {returnValue} for SystemId [{SystemId}], IntegrationId [{IntegrationId}], RecordId [{RecordId}].");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FAILURE attempting to remove record for SystemId [{SystemId}], IntegrationId [{IntegrationId}], RecordId [{RecordId}].");
                throw;
            }
        }

        #endregion

        #region Metrics

        public IActionResult GetIntegrationThroughputMetrics()
        {

            var metrics = new List<IntegrationThroughputMetricsViewModel>();
            foreach (var metric in _dwContext.GetThroughputMetrics().OrderBy(m => m.Date))
            {
                metrics.Add(new IntegrationThroughputMetricsViewModel()
                {
                    date = metric.Date,
                    processed = metric.Processed,
                    bad = metric.Bad,
                    possibleMatch = metric.PossibleMatch
                });
            }

            return new JsonResult(metrics.ToArray());
        }

        public IActionResult GetDataQualityMetrics()
        {
            var metrics = new List<IntegrationDataQualityMetricsViewModel>();
            foreach (var metric in _dwContext.GetDataQualityMetrics().OrderBy(m => m.Date))
            {
                metrics.Add(new IntegrationDataQualityMetricsViewModel()
                {
                    date = metric.Date,
                    bad = metric.Bad,
                    good = metric.Good,
                    enriched = metric.Enriched
                });
            };

            return new JsonResult(metrics.ToArray());
        }

        #endregion

        #region Integrations


        protected List<SelectListItem> LoadIntegrationsList(string DefaultDisplayName)
        {
            List<SelectListItem> integrations = _context.IntegrationsStatuses.Select(s => new SelectListItem()
            {
                Text = s.IntegrationName,
                Value = s.IntegrationName
            })
            .Distinct()
            .ToList();

            integrations.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return integrations;
        }

        protected List<SelectListItem> LoadIntegrationSystemsList(string DefaultDisplayName)
        {
            List<SelectListItem> systems = _context.IntegrationsStatuses.Select(s => new SelectListItem()
            {
                Text = s.SystemName,
                Value = s.SystemName
            })
            .Distinct()
            .ToList();

            systems.Insert(0, new SelectListItem() { Value = string.Empty, Text = string.Format(DEFAULT_PLACEHOLDER, DefaultDisplayName) });

            return systems;
        }

        #endregion

    }
}
