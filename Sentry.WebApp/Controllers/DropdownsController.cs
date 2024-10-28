using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sentry.WebApp.Data;
using Microsoft.Extensions.Logging;
using Sentry.WebApp.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Configuration;

namespace Sentry.WebApp.Controllers
{
    public class DropdownSearchParameters
    {
        public string Term { get; set; }
        public string _type { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class Select2ParamsKFSAccount
    {
        public string term { get; set; }
        public string KFSAccountMasterId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class Select2ParamsAcademicCareer
    {
        public string Term { get; set; }
        public string DischargedAcademicCareerMasterId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class DropdownsController : SentryController
    {
        public DropdownsController(AppDbContext context, DwDbContext dwContext, ILogger<DropdownsController> logger, IConfiguration configuration) : base(context, dwContext, logger, configuration) { }

        // Student: Students
        public IActionResult GetStudents(DropdownSearchParameters select2Params)
        {
            var studentList = _context.StudentNames.Where(s => s.Name.Contains(select2Params.Term) || s.StudentId.Contains(select2Params.Term))
                                    .Select(n => new
                                    {
                                        Id = n.StudentMasterId,
                                        Text = string.Format("{0}{1}", n.Name, string.IsNullOrEmpty(n.StudentId) ? "" : $" ({n.StudentId})"),
                                        StudentId = n.StudentId
                                    });

            _logger.LogTrace($"Request to GetStudents with search string [{select2Params.Term}] returned [{studentList.Count()}] rows.");

            return new JsonResult(studentList);
        }

        // Student: Constituents
        public IActionResult GetConstituents(DropdownSearchParameters select2Params)
        {
            var constituentList = _context.PersonNames.Where(s => s.LastName.Contains(select2Params.Term) || s.UAPersonId.Contains(select2Params.Term))
                                    .Select(n => new
                                    {
                                        Id = n.MasterRecordId,
                                        Text = string.Format("{0}{1}", n.Name, string.IsNullOrEmpty(n.UAPersonId) ? "" : $" ({n.UAPersonId})"),
                                        UAPersonId = n.UAPersonId
                                    });

            _logger.LogTrace($"Request to GetConstituents with search string [{select2Params.Term}] returned [{constituentList.Count()}] rows.");

            return new JsonResult(constituentList);
        }

        // Student: Educational Institution
        public IActionResult GetEducationalInstitutions(DropdownSearchParameters select2Params)
        {
            var educationalInstitutionList = _context.EducationalInstitutions.Where(s => s.Name.Contains(select2Params.Term) || s.EducationalInstitutionMasterId.Contains(select2Params.Term))
                                    .Select(n => new
                                    {
                                        Id = n.EducationalInstitutionMasterId,
                                        Text = string.Format("{0}", n.Name),
                                        StudentId = n.EducationalInstitutionMasterId
                                    });

            _logger.LogTrace($"Request to GetEducationalInstitutions with search string [{select2Params.Term}] returned [{educationalInstitutionList.Count()}] rows.");

            return new JsonResult(educationalInstitutionList);
        }

        // Student: Academic Catalog - DegreeTypeMasterId
        public IActionResult GetDegreeTypes(DropdownSearchParameters select2Params)
        {
            var degreeList = _context.StudentAcademicCatalogDegreeTypes.Where(s => s.AcademicCatalogDegreeTypeName.Contains(select2Params.Term) || s.AcademicCatalogDegreeTypeCode.Contains(select2Params.Term))
                                    .Select(n => new
                                    {
                                        Id = n.AcademicCatalogDegreeTypeCode, //MasterId
                                        Text = string.Format("{0}", n.AcademicCatalogDegreeTypeName),
                                        //Text = string.Format("{0}{1}", n.AcademicCatalogDegreeTypeName, string.IsNullOrEmpty(n.AcademicCatalogDegreeTypeCode) ? "" : $" ({n.AcademicCatalogDegreeTypeCode})"),
                                        Code = n.AcademicCatalogDegreeTypeCode
                                    });

            _logger.LogTrace($"Request to GetDegreeTypes with search string [{select2Params.Term}] returned [{degreeList.Count()}] rows.");

            return new JsonResult(degreeList);
        }

        // Student: Academic Catalog - AcademicPlanMasterId
        public IActionResult GetAcademicPlans(DropdownSearchParameters select2Params)
        {
            var planList = _context.StudentAcademicCatalogAcademicPlans.Where(s => s.Name.Contains(select2Params.Term) || s.MasterRecordId.Contains(select2Params.Term))
                                    .Select(n => new
                                    {
                                        Id = n.MasterRecordId,
                                        Text = string.Format("{0}", n.Name)
                                    });

            _logger.LogTrace($"Request to GetAcademicPlans with search string [{select2Params.Term}] returned [{planList.Count()}] rows.");

            return new JsonResult(planList);
        }

        // Student: AcademicSubplanMasterId
        public IActionResult GetAcademicSubplans(DropdownSearchParameters select2Params)
        {
            var planList = _context.StudentAcademicSubplans.Where(s => s.AcademicSubplanName.Contains(select2Params.Term) || s.MasterRecordId.Contains(select2Params.Term))
                                    .Select(n => new
                                    {
                                        Id = n.MasterRecordId,
                                        Text = string.Format("{0}", n.AcademicSubplanName)
                                    });

            _logger.LogTrace($"Request to GetAcademicSubplans with search string [{select2Params.Term}] returned [{planList.Count()}] rows.");

            return new JsonResult(planList);
        }

        // Student: Academic Catalog - DepartmentMasterId
        public IActionResult GetDepartments(DropdownSearchParameters select2Params)
        {
            var departmentList = _context.Departments.Where(s => s.Name.Contains(select2Params.Term) || s.Id.Contains(select2Params.Term))
                                    .Select(n => new
                                    {
                                        Id = n.Id,
                                        Text = string.Format("{0}", n.Name)
                                    });

            _logger.LogTrace($"Request to GetDepartments with search string [{select2Params.Term}] returned [{departmentList.Count()}] rows.");

            return new JsonResult(departmentList);
        }

        // Student: Enrollment - CampusMasterId
        public IActionResult GetCampuses(DropdownSearchParameters select2Params)
        {
            var departmentList = _context.Departments.Where(s => s.Name.Contains(select2Params.Term) || s.Id.Contains(select2Params.Term))
                                    .Select(n => new
                                    {
                                        Id = n.Id,
                                        Text = string.Format("{0}", n.Name)
                                    });

            _logger.LogTrace($"Request to GetCampuses with search string [{select2Params.Term}] returned [{departmentList.Count()}] rows.");

            return new JsonResult(departmentList);
        }

        // Academic Calendar
        public IActionResult GetAcademicCalendarEntries(DropdownSearchParameters paramters)
        {
            var entries = _context.AcademicCalendarEntries.Where(s => s.Name.Contains(paramters.Term) || s.AcademicCareer.Contains(paramters.Term))
                                  .Select(n => new
                                  {
                                      n.Id,
                                      Text = $"{n.Name} ({n.AcademicCareer})",
                                      n.AcademicCareer,
                                      n.AcademicTermCode
                                  })
                                  .OrderBy(o => o.AcademicTermCode)
                                  .ThenByDescending(o => o.AcademicCareer);

            _logger.LogTrace($"Request to GetAcademicCalendarEntries with search string [{paramters.Term}] returned [{entries.Count()}] rows.");

            return new JsonResult(entries);
        }

        // Designation: KFS Account
        public IActionResult GetKFSAccounts(Select2ParamsKFSAccount select2ParamsKFSAccount)
        {
           var kfsAccountList = _context.KFSAccounts
                                        .Where(s => s.KFSAccountCode.Contains(select2ParamsKFSAccount.term))
                                        .OrderBy(o => o.KFSAccountCode);

            _logger.LogTrace($"Request to GetKFSAccounts with search string [{select2ParamsKFSAccount.KFSAccountMasterId}] returned [{kfsAccountList.Count()}] rows.");

            return new JsonResult(kfsAccountList);
        }
    }
}
