using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    public class StudentAcademicPlanMatchDetail : BaseDetail
    {
        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public bool FirstName_IncludeInMatch { get; set; }
        public int FirstName_MatchWeight { get; set; }

        public string PreferredName { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public bool PreferredName_IncludeInMatch { get; set; }
        public int PreferredName_MatchWeight { get; set; }

        public string MiddleName { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        public bool MiddleName_IncludeInMatch { get; set; }
        public int MiddleName_MatchWeight { get; set; }

        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public bool LastName_IncludeInMatch { get; set; }
        public int LastName_MatchWeight { get; set; }

        public string MaidenName { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        public bool MaidenName_IncludeInMatch { get; set; }
        public int MaidenName_MatchWeight { get; set; }

        public string StudentId { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public bool StudentId_IncludeInMatch { get; set; }
        public int StudentId_MatchWeight { get; set; }

        public string Title { get; set; }
        public string Title_BusinessName { get; set; }
        public string Title_BusinessDescription { get; set; }
        public bool Title_IncludeInMatch { get; set; }
        public int Title_MatchWeight { get; set; }

        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public bool Suffix_IncludeInMatch { get; set; }
        public int Suffix_MatchWeight { get; set; }

        public string BirthDate { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        public bool BirthDate_IncludeInMatch { get; set; }
        public int BirthDate_MatchWeight { get; set; }

        public string DeceasedDate { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        public bool DeceasedDate_IncludeInMatch { get; set; }
        public int DeceasedDate_MatchWeight { get; set; }

        public string MaritalStatus { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        public bool MaritalStatus_IncludeInMatch { get; set; }
        public int MaritalStatus_MatchWeight { get; set; }

        public string FERPAInformationRelease { get; set; }
        public string FERPAInformationRelease_BusinessName { get; set; }
        public string FERPAInformationRelease_BusinessDescription { get; set; }
        public bool FERPAInformationRelease_IncludeInMatch { get; set; }
        public int FERPAInformationRelease_MatchWeight { get; set; }

        public string CitizenshipCountry { get; set; }
        public string CitizenshipCountry_BusinessName { get; set; }
        public string CitizenshipCountry_BusinessDescription { get; set; }
        public bool CitizenshipCountry_IncludeInMatch { get; set; }
        public int CitizenshipCountry_MatchWeight { get; set; }

        public string Discharged { get; set; }
        public string Discharged_BusinessName { get; set; }
        public string Discharged_BusinessDescription { get; set; }
        public bool Discharged_IncludeInMatch { get; set; }
        public int Discharged_MatchWeight { get; set; }

        public string DischargedAcademicCareerName { get; set; }
        public string DischargedAcademicCareerName_BusinessName { get; set; }
        public string DischargedAcademicCareerName_BusinessDescription { get; set; }
        public bool DischargedAcademicCareerName_IncludeInMatch { get; set; }
        public int DischargedAcademicCareerName_MatchWeight { get; set; }

        public string DischargedAcademicCareerCode { get; set; }
        public string DischargedAcademicCareerCode_BusinessName { get; set; }
        public string DischargedAcademicCareerCode_BusinessDescription { get; set; }
        public bool DischargedAcademicCareerCode_IncludeInMatch { get; set; }
        public int DischargedAcademicCareerCode_MatchWeight { get; set; }

    }
}
