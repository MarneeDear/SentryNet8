using System;
using System.Collections.Generic;
using System.Text;

namespace Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal
{
    public class Enums
    {
        public enum FormStatusCodes
        {
            Open = 1,
            Printed = 2
        }

        public enum EFormCode
        {
            GT = 10,
            GU = 11
        }

        public enum UdfFeeExemptionTypes
        {
            Scholarship = 1,
            Other = 2
        }

        public enum BatchTypeCodes
        {
            Cash = 3,
            Check = 1,
            CreditCard = 2,
            Other = 4,
            PledgeGiftCommitment = 5,
            GiftInKind = 6,
            PledgeLegallyBinding = 7,
            Wire = 8
        }


    }
}
