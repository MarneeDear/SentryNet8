using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.ViewModels
{
    public class PostToGLViewModel : BaseIntegrationViewModel
    {
        public PostToGLViewModel() : base() { }
        
        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        #region Designation

        public string DesignationName { get; set; }
        public string DesignationName_BusinessName { get; set; }
        public string DesignationName_BusinessDescription { get; set; }
        public string DesignationName_Status { get; set; }
        public string DesignationName_Source { get; set; }
        public string DesignationName_Category { get; set; }
        public string DesignationName_OriginalValue { get; set; }
        public int? DesignationName_AttributeId { get; set; }

        public string DesignationId { get; set; }
        public string DesignationId_BusinessName { get; set; }
        public string DesignationId_BusinessDescription { get; set; }
        public string DesignationId_Status { get; set; }
        public string DesignationId_Source { get; set; }
        public int? DesignationId_FieldId { get; set; }
        public string DesignationId_OriginalValue { get; set; }
        public int? DesignationId_AttributeId { get; set; }

        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
        public DateTime? StartDate { get; set; }
        public string StartDate_BusinessName { get; set; }
        public string StartDate_BusinessDescription { get; set; }
        public string StartDate_Status { get; set; }
        public string StartDate_Source { get; set; }
        public int? StartDate_FieldId { get; set; }
        public string StartDate_OriginalValue { get; set; }
        public int? StartDate_AttributeId { get; set; }

        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
        public DateTime? EndDate { get; set; }
        public string EndDate_BusinessName { get; set; }
        public string EndDate_BusinessDescription { get; set; }
        public string EndDate_Status { get; set; }
        public string EndDate_Source { get; set; }
        public int? EndDate_FieldId { get; set; }
        public string EndDate_OriginalValue { get; set; }
        public int? EndDate_AttributeId { get; set; }

        public string DesignationType { get; set; }
        public string DesignationType_BusinessName { get; set; }
        public string DesignationType_BusinessDescription { get; set; }
        public string DesignationType_Status { get; set; }
        public string DesignationType_Source { get; set; }
        public int? DesignationType_FieldId { get; set; }
        public string DesignationType_OriginalValue { get; set; }
        public int? DesignationType_AttributeId { get; set; }

        public string DesignationSubtype { get; set; }
        public string DesignationSubtype_BusinessName { get; set; }
        public string DesignationSubtype_BusinessDescription { get; set; }
        public string DesignationSubtype_Status { get; set; }
        public string DesignationSubtype_Source { get; set; }
        public int? DesignationSubtype_FieldId { get; set; }
        public string DesignationSubtype_OriginalValue { get; set; }
        public int? DesignationSubtype_AttributeId { get; set; }

        public string DesignationStatus { get; set; }
        public string DesignationStatus_BusinessName { get; set; }
        public string DesignationStatus_BusinessDescription { get; set; }
        public string DesignationStatus_Status { get; set; }
        public string DesignationStatus_Source { get; set; }
        public int? DesignationStatus_FieldId { get; set; }
        public string DesignationStatus_OriginalValue { get; set; }
        public int? DesignationStatus_AttributeId { get; set; }

        public string DesignationState { get; set; }
        public string DesignationState_BusinessName { get; set; }
        public string DesignationState_BusinessDescription { get; set; }
        public string DesignationState_Status { get; set; }
        public string DesignationState_Source { get; set; }
        public int? DesignationState_FieldId { get; set; }
        public string DesignationState_OriginalValue { get; set; }
        public int? DesignationState_AttributeId { get; set; }

        public string UADepartment { get; set; }
        public string UADepartment_BusinessName { get; set; }
        public string UADepartment_BusinessDescription { get; set; }
        public string UADepartment_Status { get; set; }
        public string UADepartment_Source { get; set; }
        public int? UADepartment_FieldId { get; set; }
        public string UADepartment_OriginalValue { get; set; }
        public int? UADepartment_AttributeId { get; set; }

        #endregion


        #region Drop-Downs

        //public List<SelectListItem> DesignationTypeList { get; set; }

        #endregion


        public List<PostToGLHistoryViewModel> HistoryData { get; set; }



    }
}
