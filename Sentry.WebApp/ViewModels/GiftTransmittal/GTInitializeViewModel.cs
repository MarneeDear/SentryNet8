using Microsoft.AspNetCore.Http;
using Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels.GiftTransmittal
{
    public class GTInitializeViewModel : BaseIntegrationViewModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {            

             if (ProjectDistribution.Amount <= 0)
            {
                yield return new ValidationResult("The Amount is required");
            }   
            
                yield return ValidationResult.Success;
        }

        //public GTInitializeViewModel() : base() 
        //{
        //    this.ProjectDistributions = new List<ProjectDistribution>();
        //    //this.SupportingDocuments.SupportingDocuments = new List<SupportingDocuments.SupportingDocument>();
        //}
        [Required (ErrorMessage = "The Preparer is required")]
        public string PreparedByEmployeeId { get; set; }
        //public string PreparedByName { get; set; }
        //public string EFormName { get; set; }
        public Guid GiftTransmittalId { get; set; }
        public Guid GiftTransmittalItemId { get; set; }
        public Guid GiftTransmittalDistributionId { get; set; }
        //public bool ReceivedPhysicalDocuments { get; set; } = true;
        //public byte BatchTypeCode { get; set; }
        public string FormNumber { get; set; }
        public int FormType { get; set; }
        public string Organization { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PreparerName { get; set; }        
        public InitializedConstituent InitializedConstituent { get; set; }
        public ProjectDistribution ProjectDistribution { get; set; }
        public SupportingDocuments.SupportingDocumentsListViewModel SupportingDocuments { get; set; }
        public IFormFile SupportingDocument { get; set; }
        public IList<string> ValidFileTypes { get; set; }

        public static explicit operator GTInitializeViewModel(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal giftTransmittal)
        {

            var hasItem = giftTransmittal.GiftTransmittalItems.Any();
            var hasDistribution = hasItem ? giftTransmittal.GiftTransmittalItems.First().GiftTransmittalItemDistributions.Any() : false;

            return new GTInitializeViewModel()
            {
                PreparedByEmployeeId = giftTransmittal.PreparedByName,
                Organization = giftTransmittal.FormNumber.StartsWith(Constants.GTForm) ? "uaf" : "ua",
                GiftTransmittalId = giftTransmittal.Id,
                GiftTransmittalItemId = hasItem ? giftTransmittal.GiftTransmittalItems.First().Id : Guid.Empty,
                GiftTransmittalDistributionId = hasDistribution ? giftTransmittal.GiftTransmittalItems.First().GiftTransmittalItemDistributions.First().Id : Guid.Empty,
                FormNumber = giftTransmittal.FormNumber,
                FormType = giftTransmittal.EFormCode,
                Email = giftTransmittal.ContactEmail,
                Phone = giftTransmittal.ContactPhone,
                PreparerName = giftTransmittal.ContactName,
                InitializedConstituent = new InitializedConstituent()
                {
                    FirstName = hasItem ? giftTransmittal.GiftTransmittalItems.First().ConstituentFirstName : String.Empty,
                    LastName = hasItem ? giftTransmittal.GiftTransmittalItems.First().ConstituentLastName : String.Empty,
                    OrganizationName = hasItem ? giftTransmittal.GiftTransmittalItems.First().ConstituentOrganizationName : String.Empty
                },
                ProjectDistribution = new ProjectDistribution()
                {
                    ProjectId = hasDistribution ? giftTransmittal.GiftTransmittalItems.First().GiftTransmittalItemDistributions.First().FundAccount : String.Empty,
                    Amount =  giftTransmittal.GiftTransmittalItems.First().GiftTransmittalItemDistributions.First().Amount 
                }

            };
        }
        public static explicit operator Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal(GTInitializeViewModel model)
        {
            return new Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal()
            {
                Id = model.GiftTransmittalId,
                EFormCode = model.FormNumber.StartsWith("GU") ? 11 : 10, 
                FormNumber = model.FormNumber,
                PreparedByName = model.PreparedByEmployeeId,
                PreparedByDate = DateTime.Now,
                ContactName = model.PreparerName,
                ContactEmail = model.Email,
                ContactPhone = model.Phone,
                BatchTypeCode = 1, //CHECK
                FormStatusCode = 3, //INITIALIZED                
                ReceivedPhysicalDocuments = true,
                GiftTransmittalItems = new List<Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItem>()
                {
                    
                    new Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItem()
                    {                        
                        Id = model.GiftTransmittalItemId, 
                        GiftTransmittalId = model.GiftTransmittalId,                        
                        ConstituentFirstName = model.InitializedConstituent.FirstName,
                        ConstituentLastName = model.InitializedConstituent.LastName,
                        ConstituentOrganizationName = model.InitializedConstituent.OrganizationName,

                        GiftTransmittalItemDistributions = new List<Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemDistribution>()
                        {
                            new Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalItemDistribution()
                            {
                                Id = model.GiftTransmittalDistributionId,
                                FundAccount = model.ProjectDistribution.ProjectId,
                                Amount = model.ProjectDistribution.Amount ,
                                UdfFeeDevelopment = model.ProjectDistribution.Amount * .04m,
                                UdfFeePresident = model.ProjectDistribution.Amount * .01m,
                                UdfFeeDean = model.ProjectDistribution.Amount * .01m,
                                UdfFeeAmount = model.ProjectDistribution.Amount * .06m

                            }
                        }
                    }
                }
            };
        }



    }
    public class ProjectDistribution
    {
        
        public string ProjectId { get; set; }        
        public decimal Amount { get; set; }
        public decimal UdfDevelopmentAmount { get; set; }
        public decimal UdfPresidentAmount { get; set; }
        public decimal UdfDeanAmount { get; set; }
        public string DeanProjectId { get; set; }

    }

    public class InitializedConstituent
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string ConstituentFor { get; set; } = "Individual";

        public string[] ConstituentForOptions = new[] { "Individual", "Organization" };

        public InitializedConstituent()
        {
            this.ConstituentForOptions = new[] { "Individual", "Organization" };

        }

    }


}
