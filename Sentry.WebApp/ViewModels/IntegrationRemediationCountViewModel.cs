
namespace Sentry.WebApp.ViewModels
{
    public class IntegrationRemediationCountViewModel
    {
        public int OrganizationParentCount
        {
            get { return this.OrganizationalUnitCount + this.OfficeLocationCount + this.AcademicCatalogCount; }
        }
        public int OrganizationalUnitCount { get; set; }
        public int OfficeLocationCount { get; set; }
        public int AcademicCatalogCount { get; set; }

        public int EmployeeCount { get; set; }

        public int StudentCount
        {
            get { return this.StudentBioDemCount + this.StudentEducationalHistoryCount + this.StudentEnrollmentCount + this.StudentDegreeCount + this.StudentAcademicPlanCount + this.StudentAcademicInvolvementCount + this.StudentContactCount + this.StudentScholarshipCount + this.StudentParentCount; }
        }
        public int StudentBioDemCount { get; set; }
        public int StudentEducationalHistoryCount { get; set; }
        public int StudentEnrollmentCount { get; set; }
        public int StudentDegreeCount { get; set; }
        public int StudentAcademicPlanCount { get; set; }
        public int StudentAcademicInvolvementCount { get; set; }
        public int StudentContactCount { get; set; }
        public int StudentScholarshipCount { get; set; }
        public int StudentParentCount { get; set; }

        public int ConstituentCount
        {
            get { return this.ConstituentIndividualCount + this.ConstituentPhoneCount + this.ConstituentEmailCount + this.ConstituentAddressCount; }
        }
        public int ConstituentIndividualCount { get; set; }
        public int ConstituentPhoneCount { get; set; }
        public int ConstituentEmailCount { get; set; }
        public int ConstituentAddressCount { get; set; }

        //public int ConstituentCount { get; set; }

        public int ScholarshipCount { get; set; }

        public int FinanceParentCount
        {
            get { return this.DesignationCount + this.DisbursementsCount + this.GiftTransmittalCount + this.NewVendorCount + this.FundsTransferTotalCount; }
        }
        public int DesignationCount { get; set; }

        public int ETDisbursementCount { get; set; }
        public int STDisbursementCount { get; set; }
        public int EMDisbursementCount { get; set; }
        public int NewVendorCount { get; set; }
        public int ReadyForProcessingDisbursementCount { get; set; }
        public int GiftTransmittalCount { get; set; }
        public int UaCount { get; set; }
        public int UafCount { get; set; }
        public int InitializedCount { get; set; }
        public int WaitingForBursarCount { get; set; }
        public int WaitingForPreparerCount { get; set; }
        public int SecondaryCount { get; set; }
        public int GTFinalizeCount { get; set; }
        public int GUFinalizeCount { get; set; }
        public int DisbursementsCount 
        { 
            get
            {
                return this.ETDisbursementCount + this.STDisbursementCount + this.EMDisbursementCount + this.ReadyForProcessingDisbursementCount;
            }
        }

        public int UnroutedFundsTransferCount { get; set; }
        public int RestrictedFundsTransferCount { get; set; }
        public int UnrestrictedFundsTransferCount { get; set; }
        public int EndowmentFundsTransferCount { get; set; }
        public int GiftFundsTransferCount { get; set; }
        public int GeneralCounselFundsTansferCount { get; set; }
        public int ReadyForProcessingFundsTransferCount {  get; set; }
        public int FundsTransferTotalCount
        {
            get
            {
                return this.UnroutedFundsTransferCount + this.RestrictedFundsTransferCount + this.UnrestrictedFundsTransferCount + this.EndowmentFundsTransferCount + this.GiftFundsTransferCount + this.GeneralCounselFundsTansferCount + this.ReadyForProcessingFundsTransferCount;
            }
        }
    }
}