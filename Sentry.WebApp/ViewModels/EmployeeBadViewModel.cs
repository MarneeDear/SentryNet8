using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.ViewModels
{
    public class EmployeeBadViewModel : BaseIntegrationViewModel
    {
        public EmployeeBadViewModel() : base() { }

        [Display(Name = "Business Email:")]
        [Required(ErrorMessage = "Business Email is required")]
        [EmailAddress]
        [StringLength(60, MinimumLength = 3)]
        public string BusinessEmail_Output { get; set; }

        public string BusinessEmail_Reason { get; set; }

        public string BusinessEmail_Source { get; set; }

        public string BusinessEmail_Status { get; set; }

        public string DOB_Output { get; set; }

        public string DOB_Reason { get; set; }

        public string DOB_Source { get; set; }

        public string DOB_Status { get; set; }

        [Display(Name = "Employee ID:")]
        [Required(ErrorMessage = "Employee ID is required.")]
        [StringLength(60, MinimumLength = 3)]
        public string Employee_ID_Output { get; set; }

        public string Employee_ID_Reason { get; set; }

        public string Employee_ID_Source { get; set; }

        public string Employee_ID_Status { get; set; }

        public string Employee_Type_Output { get; set; }

        public string Employee_Type_Reason { get; set; }

        public string Employee_Type_Source { get; set; }

        public string Employee_Type_Status { get; set; }

        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(60, MinimumLength = 3)]
        public string FirstName_Output { get; set; }

        public string FirstName_Reason { get; set; }

        public string FirstName_Source { get; set; }

        public string FirstName_Status { get; set; }

        [Display(Name = "Home Address 1:")]
        [Required(ErrorMessage = "Home Address is required.")]
        [StringLength(60, MinimumLength = 3)]
        public string Home_Addr1_Output { get; set; }

        public string Home_Addr1_Reason { get; set; }

        public string Home_Addr1_Source { get; set; }

        public string Home_Addr1_Status { get; set; }

        [Display(Name = "Home Address 2:")]
        public string Home_Addr2_Output { get; set; }

        public string Home_Addr2_Reason { get; set; }

        public string Home_Addr2_Source { get; set; }

        public string Home_Addr2_Status { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "City is required.")]
        [StringLength(60, MinimumLength = 3)]
        public string Home_City_Output { get; set; }

        public string Home_City_Reason { get; set; }

        public string Home_City_Source { get; set; }

        public string Home_City_Status { get; set; }

        public string Home_Country_Output { get; set; }

        public string Home_Country_Reason { get; set; }

        public string Home_Country_Source { get; set; }

        public string Home_Country_Status { get; set; }

        [Display(Name = "Zip:")]
        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Postal Code.")]
        public string Home_PostalCode_Output { get; set; }

        public string Home_PostalCode_Reason { get; set; }

        public string Home_PostalCode_Source { get; set; }

        public string Home_PostalCode_Status { get; set; }

        public string Home_State_Output { get; set; }

        public string Home_State_Reason { get; set; }

        public string Home_State_Source { get; set; }

        public string Home_State_Status { get; set; }

        [Display(Name = "Department:")]
        [Required(ErrorMessage = "Department is required.")]
        [StringLength(100, MinimumLength = 5)]
        public string Job_Department_Output { get; set; }

        public string Job_Department_Reason { get; set; }

        public string Job_Department_Source { get; set; }

        public string Job_Department_Status { get; set; }

        [Display(Name = "Organization:")]
        [Required(ErrorMessage = "Organization is required.")]
        [StringLength(100, MinimumLength = 5)]
        public string Job_Organization_Output { get; set; }

        public string Job_Organization_Reason { get; set; }

        public string Job_Organization_Source { get; set; }

        public string Job_Organization_Status { get; set; }

        public string Job_Division_Output { get; set; }

        public string Job_Division_Reason { get; set; }

        public string Job_Division_Source { get; set; }

        public string Job_Division_Status { get; set; }

        public string Job_EndDate_Output { get; set; }

        public string Job_EndDate_Reason { get; set; }

        public string Job_EndDate_Source { get; set; }

        public string Job_EndDate_Status { get; set; }

        public string Job_Location_Output { get; set; }

        public string Job_Location_Reason { get; set; }

        public string Job_Location_Source { get; set; }

        public string Job_Location_Status { get; set; }

        [Display(Name = "Manager's Name:")]
        [StringLength(80, MinimumLength = 3)]
        [Required(ErrorMessage = "Manager's Name is required.")]
        public string Job_Manager_Output { get; set; }

        public string Job_Manager_Reason { get; set; }

        public string Job_Manager_Source { get; set; }

        public string Job_Manager_Status { get; set; }

        [Display(Name = "Start Date:")]
        public string Job_StartDate_Output { get; set; }

        public string Job_StartDate_Reason { get; set; }

        public string Job_StartDate_Source { get; set; }

        public string Job_StartDate_Status { get; set; }

        [Display(Name = "Job Title:")]
        [StringLength(120, MinimumLength = 3)]
        [Required(ErrorMessage = "Job Title is required.")]
        public string Job_Title_Output { get; set; }

        public string Job_Title_Reason { get; set; }

        public string Job_Title_Source { get; set; }

        public string Job_Title_Status { get; set; }

        [Display(Name = "Last Name:")]
        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName_Output { get; set; }

        public string LastName_Reason { get; set; }

        public string LastName_Source { get; set; }

        public string LastName_Status { get; set; }

        public string MiddleName_Output { get; set; }

        public string MiddleName_Reason { get; set; }

        public string MiddleName_Source { get; set; }

        public string MiddleName_Status { get; set; }

        [Display(Name = "Network Username:")]
        [StringLength(30, MinimumLength = 5)]
        [Required(ErrorMessage = "Network Username is required.")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Network Username.")]
        public string NetworkID_Output { get; set; }

        public string NetworkID_Reason { get; set; }

        public string NetworkID_Source { get; set; }

        public string NetworkID_Status { get; set; }

        [Display(Name = "Business Address 1:")]
        [Required(ErrorMessage = "Business Address is required.")]
        [StringLength(60, MinimumLength = 3)]
        public string Office_Addr1_Output { get; set; }

        public string Office_Addr1_Reason { get; set; }

        public string Office_Addr1_Source { get; set; }

        public string Office_Addr1_Status { get; set; }

        [Display(Name = "Business Address 2:")]
        public string Office_Addr2_Output { get; set; }

        public string Office_Addr2_Reason { get; set; }

        public string Office_Addr2_Source { get; set; }

        public string Office_Addr2_Status { get; set; }

        [Display(Name = "City:")]
        [Required(ErrorMessage = "City is required.")]
        [StringLength(60, MinimumLength = 3)]
        public string Office_City_Output { get; set; }

        public string Office_City_Reason { get; set; }

        public string Office_City_Source { get; set; }

        public string Office_City_Status { get; set; }

        public string Office_Country_Output { get; set; }

        public string Office_Country_Reason { get; set; }

        public string Office_Country_Source { get; set; }

        public string Office_Country_Status { get; set; }

        [Display(Name = "Office Location Name:")]
        public string Office_Location_Output { get; set; }

        public string Office_Location_Reason { get; set; }

        public string Office_Location_Source { get; set; }

        public string Office_Location_Status { get; set; }

        public string Office_Name_Output { get; set; }

        public string Office_Name_Reason { get; set; }

        public string Office_Name_Source { get; set; }

        public string Office_Name_Status { get; set; }

        [Display(Name = "Office Number:")]
        [StringLength(10, MinimumLength = 2)]
        [RegularExpression("([a-zA-Z0-9_]+)", ErrorMessage = "Please enter valid Office Number.")]
        public string Office_Number_Output { get; set; }

        public string Office_Number_Reason { get; set; }

        public string Office_Number_Source { get; set; }

        public string Office_Number_Status { get; set; }

        public string Office_Phone_Output { get; set; }

        public string Office_Phone_Reason { get; set; }

        public string Office_Phone_Source { get; set; }

        public string Office_Phone_Status { get; set; }

        public string Office_Phone_Type_Output { get; set; }

        public string Office_Phone_Type_Reason { get; set; }

        public string Office_Phone_Type_Source { get; set; }

        public string Office_Phone_Type_Status { get; set; }

        [Display(Name = "Zip:")]
        [Required(ErrorMessage = "Postal Code is required")]
        [StringLength(10, MinimumLength = 5)]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Postal Code.")]
        public string Office_PostalCode_Output { get; set; }

        public string Office_PostalCode_Reason { get; set; }

        public string Office_PostalCode_Source { get; set; }

        public string Office_PostalCode_Status { get; set; }

        [Display(Name = "State:")]
        public string Office_State_Output { get; set; }

        public string Office_State_Reason { get; set; }

        public string Office_State_Source { get; set; }

        public string Office_State_Status { get; set; }

        public string Personal_Email_Output { get; set; }

        public string Personal_Email_Reason { get; set; }

        public string Personal_Email_Source { get; set; }

        public string Personal_Email_Status { get; set; }

        public string Personal_Phone_Output { get; set; }

        public string Personal_Phone_Reason { get; set; }

        public string Personal_Phone_Source { get; set; }

        public string Personal_Phone_Status { get; set; }

        public string Personal_Phone_Type_Output { get; set; }

        public string Personal_Phone_Type_Reason { get; set; }

        public string Personal_Phone_Type_Source { get; set; }

        public string Personal_Phone_Type_Status { get; set; }

        [Display(Name = "Preferred First Name:")]
        public string PreferredName_Output { get; set; }

        public string PreferredName_Reason { get; set; }

        public string PreferredName_Source { get; set; }

        public string PreferredName_Status { get; set; }

        public string SystemRecordId_Source { get; set; }

        public string SystemRecordId_Status { get; set; }

        public string SystemRecordId_Output { get; set; }

        public string SystemRecordId_Reason { get; set; }

        public SelectListItem[] organizations { get; set; }
        public SelectListItem[] departments { get; set; }

        //public List<EmployeeBad> EmployeesBadList { get; set; }
    }

}