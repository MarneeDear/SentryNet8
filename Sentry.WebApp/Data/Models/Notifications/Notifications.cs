using System.Collections.Generic;

namespace Sentry.WebApp.Data.Models.Notifications
{
    public class EscalatedApprovalDetails
    {     
        public string form_id { get; set; }
        public string amount { get; set; }
        public string approve_link { get; set; }
    }
    public class NewVendorRequestApprovalDetails
    {
        public string form_id { get; set; }
        public string amount { get; set; }
        public string approve_link { get; set; }
    }

    public class APReviewRejectionDetails
    {
        //public string submitter_name { get; set; }
        //public string submitter_email { get; set; }        
        public string uafdn_link { get; set; }
        public string comments { get; set; }
        public string form_id { get; set; }
    }

    public class GiftTransmittalRejectionDetails
    {
        public string uafdn_link { get; set; }
        public string comments { get; set; }
        public string form_id { get; set; }
    }

    public class NewVendorRequestRejectionDetails
    {
        public string uafdn_link { get; set; }
        public string comments { get; set; }
        public string form_id { get; set; }
    }

    public class FundsTransferApprovalDetails
    {
        public string form_id { get; set; }
        public string amount { get; set; }
        public string approve_link { get; set; }
    }

    public class FundsTransferRejectionDetails
    {
        public string uafdn_link { get; set; }
        public string comments { get; set; }
        public string form_id { get; set; }
    }

    public class SendTo
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }

    //public class Notification
    //{
    //    public IEnumerable<SendTo> SendToList { get; set; }
    //    //public IEnumerable<SendTo> CCsList { get; set; }
    //    public string TemplateId { get; set; }
    //    public string NotificationType { get; set; }
    //    public object NotificationDetails { get; set; }
    //}

    public class GiftTransmittalInitializedDetails
    {
        public string uafdn_link { get; set; }
        public string form_id { get; set; }

    }
}
