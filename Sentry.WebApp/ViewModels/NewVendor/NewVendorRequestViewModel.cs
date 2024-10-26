using Sentry.WebApp.ViewModels.Administration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sentry.Domain.AccountsPayable.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sentry.WebApp.ViewModels.NewVendor
{

    public class NewVendorRequestViewModel : BaseIntegrationViewModel, IValidatableObject
    {
        private bool phoneFormatIsValid(string phone)
        {
            if (!String.IsNullOrWhiteSpace(phone))
            {
                var count = phone.Count(x => Char.IsDigit(x));
                if (count != 10)
                {
                    return false;
                }

                return true;
            }

            return true;
        }

        private bool taxIdIsValid(string taxId)
        {
            if (!String.IsNullOrWhiteSpace(taxId))
            {
                if (VendorType == "Individual")
                {
                    //Check SSN format
                    return Regex.IsMatch(taxId, "^\\d{3}-\\d{2}-\\d{4}$");
                }
                else
                {
                    return Regex.IsMatch(taxId, "^\\d{2}-?\\d{7}$");
                }
            }

            return true;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Custom validation.
            if (String.IsNullOrWhiteSpace(this.BusinessContactMethod) &&
                String.IsNullOrWhiteSpace(this.CellularContactMethod) &&
                String.IsNullOrWhiteSpace(this.EmailContactMethod) &&
                String.IsNullOrWhiteSpace(this.HomeContactMethod))
            {
                yield return new ValidationResult("At least one Contact Method is required");
            }            

            var notCorrectFormat = "is not in the correct format.";

            if (!phoneFormatIsValid(BusinessContactMethod))
            {
                yield return new ValidationResult($"Business Phone {notCorrectFormat}");
            }

            if (!phoneFormatIsValid(CellularContactMethod))
            {
                yield return new ValidationResult($"Cellular Phone {notCorrectFormat}");
            }

            if (!phoneFormatIsValid(HomeContactMethod))
            {
                yield return new ValidationResult($"Home Phone {notCorrectFormat}");
            }

            if (!taxIdIsValid(TaxId))
            {
                yield return new ValidationResult($"Tax ID {notCorrectFormat}");
            }

            yield return ValidationResult.Success;
        }

        public long NewVendorRequestId { get; set; }
        public long VendorId { get; set; }
        public string PreparedByEmployeeId { get; set; }
        public string PreparedByEmail { get; set; }
        public string PreparedByFirstName { get; set; }
        public string PreparedByLastName { get; set; }
        public string PreparedByPhone { get; set; }
        public string FormNumber { get; set; }
        [Required]
        public string VendorName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        public string BusinessContactFirstName { get; set; }
        public string BusinessContactLastName { get; set; }
        public string AttachmentType { get; set; }
        public IEnumerable<SelectListItem> AttatchmentTypes { get; set; }
        [Phone(ErrorMessage = "Business Phone is not a valid phone number.")]
        [MaxLength(12)]
        public string BusinessContactMethod { get; set; }
        [Phone(ErrorMessage = "Home Phone is not a valid phone number.")]
        [MaxLength(12)]
        public string HomeContactMethod { get; set; }
        [Phone(ErrorMessage = "Cellular Phone is not a valid phone number.")]
        [MaxLength(12)]
        public string CellularContactMethod { get; set; }
        public string EmailContactMethod { get; set; }
        public string Comments { get; set; }
        public string ApproveRejectComments { get; set; }
        public string Organization { get; set; }
        public bool EFTStatus { get; set; }
        public string PayFromAccount { get; set; }
        public bool PaymentOption { get; set; }
        public IList<string> ValidFileTypes { get; set; }

        public string VendorType { get; set; } = "Individual";

        public string TaxId { get; set; }
        public string PayeeName { get; set; }
        public string Status { get; set; }
        public bool Issue1099 { get; set; }
        public string PaymentType { get; set; }       
        public IEnumerable<SelectListItem> StatesList { get; set; }
        [Required]
        public string PayeeType { get; set; }
        public string PaymentTypes {  get; set; }
        public IEnumerable<SelectListItem> PayeeTypes { get; set; }
        public string ICAYear { get; set; }
        public string W9Year { get; set; }

        public SupportingDocuments.SupportingDocumentsListViewModel SupportingDocuments { get; set; }
        public IFormFile SupportingDocument { get; set; }

        public static explicit operator NewVendorRequestViewModel(NewVendorRequest entity)
        {

            return new NewVendorRequestViewModel()
            {
                NewVendorRequestId = entity.Id,
                PreparedByEmployeeId = entity.PreparedByEmployeeId,
                PreparedByEmail = entity.PreparedByEmail,
                PreparedByFirstName = entity.PreparedByFirstName,
                PreparedByLastName = entity.PreparedByLastName,
                PreparedByPhone = entity.PreparedByPhone,
                FormNumber = entity.FormNumber,
                VendorName = entity.VendorName,
                TaxId = entity.EIN,
                VendorType = entity.VendorType ?? "Individual",
                Address = entity.VendorStreetAddress,
                City = entity.VendorCity,
                State = entity.VendorState,
                ZipCode = entity.VendorZip,
                BusinessContactFirstName = entity.BusinessContactFirstName,
                BusinessContactLastName = entity.BusinessContactLastName,
                BusinessContactMethod = entity.BusinessContactMethod.HasValue ? entity.BusinessContactMethod.Value.ToString("###-###-####") : String.Empty,
                CellularContactMethod = entity.CellularContactMethod.HasValue ? entity.CellularContactMethod.Value.ToString("###-###-####") : String.Empty,
                HomeContactMethod = entity.HomeContactMethod.HasValue ? entity.HomeContactMethod.Value.ToString("###-###-####") : String.Empty,
                EmailContactMethod = entity.EmailContactMethod,
                ICAYear = entity.IcaYear.HasValue ? entity.IcaYear.Value.ToString("MM/dd/yyyy") : String.Empty,
                W9Year = entity.W9Year.HasValue ? entity.W9Year.Value.ToString("MM/dd/yyyy") : String.Empty,
                Issue1099 = entity.Issue1099,
                PayeeName = entity.PayeeName,
                PayeeType = entity.PayeeType,
                //PaymentOption = newVendorRequest.PaymentOption,
                PaymentType = entity.PaymentType,
                Comments = entity.Comments,
                ApproveRejectComments = entity.ApproveRejectComments, 
            };
        }
        public static explicit operator NewVendorRequest(NewVendorRequestViewModel model)
        {
            return new NewVendorRequest
            {
                Id = model.NewVendorRequestId,
                PreparedByEmail = model.PreparedByEmail,
                PreparedByFirstName = model.PreparedByFirstName,
                PreparedByLastName = model.PreparedByLastName,
                PreparedByPhone = model.PreparedByPhone,
                FormNumber = model.FormNumber,               
            };
        }
        public static explicit operator UpdateNewVendorRequest(NewVendorRequestViewModel model)
        {

            var icaYear = DateTime.MinValue;
            var w9Year = DateTime.MinValue;

            if (!String.IsNullOrWhiteSpace(model.ICAYear))
                DateTime.TryParse(model.ICAYear, out icaYear);

            if (!String.IsNullOrWhiteSpace(model.W9Year))
                DateTime.TryParse(model.W9Year, out w9Year);

            return new UpdateNewVendorRequest
            {
                Id = model.NewVendorRequestId,
                PreparedByEmployeeId = model.PreparedByEmployeeId,
                VendorName = model.VendorName,
                VendorStreetAddress = model.Address,
                VendorCity = model.City,
                VendorState = model.State,
                VendorZip = model.ZipCode,
                VendorType = model.VendorType,
                EIN = model.TaxId,
                PayeeName = model.PayeeName,
                BusinessContactFirstName = model.BusinessContactFirstName,
                BusinessContactLastName = model.BusinessContactLastName,
                BusinessContactMethod = !String.IsNullOrEmpty(model.BusinessContactMethod) ? Regex.Replace(model.BusinessContactMethod, "[^0-9.]", "") : null, //Remove non dgits before saving so we can format the way we want when displaying;
                CellularContactMethod = !String.IsNullOrEmpty(model.CellularContactMethod) ? Regex.Replace(model.CellularContactMethod, "[^0-9.]", "") : null,
                HomeContactMethod = !String.IsNullOrEmpty(model.HomeContactMethod) ? Regex.Replace(model.HomeContactMethod, "[^0-9.]", "") : null,
                EmailContactMethod = model.EmailContactMethod,
                EFTStatus = model.EFTStatus,               
                PaymentOption = model.PaymentOption,
                PayeeType = model.PayeeType,
                PaymentType = model.PaymentType,
                Issue1099 = model.Issue1099,
                Status = model.Status,
                IcaYear = icaYear > DateTime.MinValue ? icaYear : null,
                W9Year = w9Year > DateTime.MinValue ? w9Year : null,
                ApproveRejectComments = model.ApproveRejectComments,
                Comments = model.Comments
            };
        }
    }

    public class VendorType
    {
        public string Individual { get; set; }
        public string Organization { get; set; }
        public string VendorTypeFor { get; set; } = "Individual";

        public string[] VendorTypeOptions = new[] { "Individual", "Organization" };

        public VendorType() 
        {
            this.VendorTypeOptions = new[] { "Individual", "Organization" }; 
        }
    }
}
