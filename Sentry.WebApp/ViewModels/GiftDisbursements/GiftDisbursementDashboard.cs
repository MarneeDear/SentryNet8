using Sentry.Domain.AccountsPayable.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels.GiftDisbursements
{
    public class GiftDisbursementDashboard : BaseViewModel
    {
        public GiftDisbursementList AssociateVicePresidentDisbursements { get; set; }
        public GiftDisbursementList VicePresidentDisbursements { get; set; }
        public GiftDisbursementList CFODisbursements { get; set; }
        public GiftDisbursementList GeneralCounselDisbursements { get; set; }
        public GiftDisbursementList ApReviewerDisbursements { get; set; }
        public GiftDisbursementList ScholarshipDisbursements { get; set; }
        public GiftDisbursementList ApManagerDisbursements { get; set; }
    }
}
